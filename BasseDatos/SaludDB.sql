/* =====================================================================
   SaludDB - Ministerio de Salud                          (version 2)
   Arquitectura Multi-Tenant: PATRON POOL
   (una sola base de datos, un solo esquema, discriminador ProvinciaId)

   Preparado para auditoria:
   - Columnas de trazabilidad en todas las tablas (CreadoPor, FechaCreacion,
     ModificadoPor, FechaModificacion).
   - Borrado logico obligatorio: el DELETE fisico esta bloqueado por trigger.
   - Tabla Auditoria: bitacora inmutable de INSERT / UPDATE / DESACTIVAR /
     REACTIVAR con valores anteriores y nuevos (JSON), usuario y provincia.
   - Tabla BitacoraAccesos: intentos de inicio de sesion (exitosos y fallidos).
   - Un registro nunca puede cambiar de provincia.

   Las provincias NO estan escritas en el codigo: son filas de la tabla
   Provincias, por lo que el admin nacional puede agregar otras desde la
   interfaz. El script se puede ejecutar varias veces sin duplicar datos.

   Si ya ejecutaste la version 1, borra la base y vuelve a ejecutar este
   script (solo desarrollo):
     USE master;
     ALTER DATABASE SaludDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
     DROP DATABASE SaludDB;
   ===================================================================== */

IF DB_ID('SaludDB') IS NULL
    CREATE DATABASE SaludDB;
GO

USE SaludDB;
GO

/* ---------------------------------------------------------------------
   1. PROVINCIAS (el tenant)
   --------------------------------------------------------------------- */
IF OBJECT_ID('dbo.Provincias', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Provincias
    (
        ProvinciaId       INT IDENTITY(1,1) NOT NULL,
        Codigo            VARCHAR(10)       NOT NULL,
        Nombre            NVARCHAR(100)     NOT NULL,
        Activo            BIT               NOT NULL CONSTRAINT DF_Provincias_Activo        DEFAULT (1),
        CreadoPor         INT               NULL,
        FechaCreacion     DATETIME2(0)      NOT NULL CONSTRAINT DF_Provincias_FechaCreacion DEFAULT (SYSDATETIME()),
        ModificadoPor     INT               NULL,
        FechaModificacion DATETIME2(0)      NULL,

        CONSTRAINT PK_Provincias        PRIMARY KEY (ProvinciaId),
        CONSTRAINT UQ_Provincias_Codigo UNIQUE (Codigo),
        CONSTRAINT UQ_Provincias_Nombre UNIQUE (Nombre)
    );
END
GO

/* ---------------------------------------------------------------------
   2. TIPOS_ESTABLECIMIENTOS
   Catalogo nacional compartido por todas las provincias (sin ProvinciaId
   a proposito: estandariza la informacion a nivel pais).
   --------------------------------------------------------------------- */
IF OBJECT_ID('dbo.Tipos_Establecimientos', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Tipos_Establecimientos
    (
        TipoEstablecimientoId INT IDENTITY(1,1) NOT NULL,
        Nombre                NVARCHAR(100)     NOT NULL,
        Descripcion           NVARCHAR(250)     NULL,
        Activo                BIT               NOT NULL CONSTRAINT DF_Tipos_Activo        DEFAULT (1),
        CreadoPor             INT               NULL,
        FechaCreacion         DATETIME2(0)      NOT NULL CONSTRAINT DF_Tipos_FechaCreacion DEFAULT (SYSDATETIME()),
        ModificadoPor         INT               NULL,
        FechaModificacion     DATETIME2(0)      NULL,

        CONSTRAINT PK_Tipos_Establecimientos PRIMARY KEY (TipoEstablecimientoId),
        CONSTRAINT UQ_Tipos_Nombre           UNIQUE (Nombre)
    );
END
GO

/* ---------------------------------------------------------------------
   3. USUARIOS
   Rol 'AdminNacional' -> ProvinciaId NULL (ve todas las provincias).
   Rol 'Provincial'    -> ProvinciaId obligatorio (solo ve la suya).
   ClaveHash: SHA-256 (32 bytes), nunca la clave en texto plano.
   --------------------------------------------------------------------- */
IF OBJECT_ID('dbo.Usuarios', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Usuarios
    (
        UsuarioId         INT IDENTITY(1,1) NOT NULL,
        NombreUsuario     NVARCHAR(50)      NOT NULL,
        ClaveHash         VARBINARY(32)     NOT NULL,
        NombreCompleto    NVARCHAR(150)     NOT NULL,
        Rol               VARCHAR(20)       NOT NULL,
        ProvinciaId       INT               NULL,
        Activo            BIT               NOT NULL CONSTRAINT DF_Usuarios_Activo        DEFAULT (1),
        CreadoPor         INT               NULL,
        FechaCreacion     DATETIME2(0)      NOT NULL CONSTRAINT DF_Usuarios_FechaCreacion DEFAULT (SYSDATETIME()),
        ModificadoPor     INT               NULL,
        FechaModificacion DATETIME2(0)      NULL,

        CONSTRAINT PK_Usuarios        PRIMARY KEY (UsuarioId),
        CONSTRAINT UQ_Usuarios_Nombre UNIQUE (NombreUsuario),
        CONSTRAINT FK_Usuarios_Provincias
            FOREIGN KEY (ProvinciaId) REFERENCES dbo.Provincias (ProvinciaId),
        CONSTRAINT CK_Usuarios_Rol
            CHECK (Rol IN ('AdminNacional', 'Provincial')),
        CONSTRAINT CK_Usuarios_Rol_Provincia
            CHECK (   (Rol = 'AdminNacional' AND ProvinciaId IS NULL)
                   OR (Rol = 'Provincial'    AND ProvinciaId IS NOT NULL))
    );

    CREATE INDEX IX_Usuarios_ProvinciaId ON dbo.Usuarios (ProvinciaId);
END
GO

/* ---------------------------------------------------------------------
   4. ESTABLECIMIENTOS (datos del tenant)
   ProvinciaId es obligatorio: es el discriminador del Patron Pool.
   El nombre es unico dentro de cada provincia.
   --------------------------------------------------------------------- */
IF OBJECT_ID('dbo.Establecimientos', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Establecimientos
    (
        EstablecimientoId     INT IDENTITY(1,1) NOT NULL,
        Nombre                NVARCHAR(150)     NOT NULL,
        Direccion             NVARCHAR(250)     NULL,
        Telefono              VARCHAR(20)       NULL,
        TipoEstablecimientoId INT               NOT NULL,
        ProvinciaId           INT               NOT NULL,
        Activo                BIT               NOT NULL CONSTRAINT DF_Estab_Activo        DEFAULT (1),
        CreadoPor             INT               NULL,
        FechaCreacion         DATETIME2(0)      NOT NULL CONSTRAINT DF_Estab_FechaCreacion DEFAULT (SYSDATETIME()),
        ModificadoPor         INT               NULL,
        FechaModificacion     DATETIME2(0)      NULL,

        CONSTRAINT PK_Establecimientos PRIMARY KEY (EstablecimientoId),
        CONSTRAINT FK_Estab_Tipos
            FOREIGN KEY (TipoEstablecimientoId) REFERENCES dbo.Tipos_Establecimientos (TipoEstablecimientoId),
        CONSTRAINT FK_Estab_Provincias
            FOREIGN KEY (ProvinciaId) REFERENCES dbo.Provincias (ProvinciaId),
        CONSTRAINT UQ_Estab_Provincia_Nombre UNIQUE (ProvinciaId, Nombre)
    );

    CREATE INDEX IX_Establecimientos_ProvinciaId ON dbo.Establecimientos (ProvinciaId);
    CREATE INDEX IX_Establecimientos_Tipo        ON dbo.Establecimientos (TipoEstablecimientoId);
END
GO

/* ---------------------------------------------------------------------
   5. AUDITORIA (bitacora de cambios, inmutable)
   Se llena automaticamente con los triggers TR_*_Auditoria.
   ProvinciaId permite que cada provincia consulte solo su propia bitacora.
   --------------------------------------------------------------------- */
IF OBJECT_ID('dbo.Auditoria', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Auditoria
    (
        AuditoriaId       BIGINT IDENTITY(1,1) NOT NULL,
        FechaHora         DATETIME2(3)         NOT NULL CONSTRAINT DF_Auditoria_Fecha  DEFAULT (SYSDATETIME()),
        Tabla             NVARCHAR(128)        NOT NULL,
        RegistroId        INT                  NOT NULL,
        Accion            VARCHAR(20)          NOT NULL,
        UsuarioId         INT                  NULL,
        ProvinciaId       INT                  NULL,
        ValoresAnteriores NVARCHAR(MAX)        NULL,
        ValoresNuevos     NVARCHAR(MAX)        NULL,
        Equipo            NVARCHAR(128)        NULL     CONSTRAINT DF_Auditoria_Equipo DEFAULT (HOST_NAME()),

        CONSTRAINT PK_Auditoria PRIMARY KEY (AuditoriaId),
        CONSTRAINT CK_Auditoria_Accion
            CHECK (Accion IN ('INSERT', 'UPDATE', 'DESACTIVAR', 'REACTIVAR')),
        CONSTRAINT FK_Auditoria_Usuarios
            FOREIGN KEY (UsuarioId)   REFERENCES dbo.Usuarios (UsuarioId),
        CONSTRAINT FK_Auditoria_Provincias
            FOREIGN KEY (ProvinciaId) REFERENCES dbo.Provincias (ProvinciaId)
    );

    CREATE INDEX IX_Auditoria_Registro  ON dbo.Auditoria (Tabla, RegistroId);
    CREATE INDEX IX_Auditoria_Usuario   ON dbo.Auditoria (UsuarioId);
    CREATE INDEX IX_Auditoria_Provincia ON dbo.Auditoria (ProvinciaId, FechaHora);
END
GO

/* ---------------------------------------------------------------------
   6. BITACORA DE ACCESOS (inicios de sesion, inmutable)
   La escribe la capa de Negocio en cada intento de login.
   --------------------------------------------------------------------- */
IF OBJECT_ID('dbo.BitacoraAccesos', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.BitacoraAccesos
    (
        AccesoId      BIGINT IDENTITY(1,1) NOT NULL,
        FechaHora     DATETIME2(3)         NOT NULL CONSTRAINT DF_Accesos_Fecha  DEFAULT (SYSDATETIME()),
        NombreUsuario NVARCHAR(50)         NOT NULL,
        UsuarioId     INT                  NULL,
        Exitoso       BIT                  NOT NULL,
        Motivo        NVARCHAR(100)        NULL,
        Equipo        NVARCHAR(128)        NULL     CONSTRAINT DF_Accesos_Equipo DEFAULT (HOST_NAME()),

        CONSTRAINT PK_BitacoraAccesos PRIMARY KEY (AccesoId),
        CONSTRAINT FK_Accesos_Usuarios
            FOREIGN KEY (UsuarioId) REFERENCES dbo.Usuarios (UsuarioId)
    );

    CREATE INDEX IX_Accesos_Usuario ON dbo.BitacoraAccesos (NombreUsuario, FechaHora);
END
GO

/* ---------------------------------------------------------------------
   7. FK de CreadoPor / ModificadoPor hacia Usuarios
   Se agregan al final porque Usuarios depende de Provincias (evita el
   ciclo de creacion). Se aplican a las 4 tablas de negocio.
   --------------------------------------------------------------------- */
DECLARE @tablas TABLE (Nombre SYSNAME);
INSERT INTO @tablas VALUES ('Provincias'), ('Tipos_Establecimientos'), ('Usuarios'), ('Establecimientos');

DECLARE @t SYSNAME, @sql NVARCHAR(MAX);
DECLARE cur CURSOR LOCAL FAST_FORWARD FOR SELECT Nombre FROM @tablas;
OPEN cur;
FETCH NEXT FROM cur INTO @t;
WHILE @@FETCH_STATUS = 0
BEGIN
    IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_' + @t + '_CreadoPor')
    BEGIN
        SET @sql = N'ALTER TABLE dbo.' + QUOTENAME(@t) + N' ADD CONSTRAINT ' + QUOTENAME('FK_' + @t + '_CreadoPor')
                 + N' FOREIGN KEY (CreadoPor) REFERENCES dbo.Usuarios (UsuarioId);';
        EXEC sp_executesql @sql;
    END

    IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_' + @t + '_ModificadoPor')
    BEGIN
        SET @sql = N'ALTER TABLE dbo.' + QUOTENAME(@t) + N' ADD CONSTRAINT ' + QUOTENAME('FK_' + @t + '_ModificadoPor')
                 + N' FOREIGN KEY (ModificadoPor) REFERENCES dbo.Usuarios (UsuarioId);';
        EXEC sp_executesql @sql;
    END

    FETCH NEXT FROM cur INTO @t;
END
CLOSE cur;
DEALLOCATE cur;
GO

/* =====================================================================
   TRIGGERS DE AUDITORIA Y PROTECCION
   Regla para la capa de Acceso a Datos:
     - En cada INSERT enviar CreadoPor.
     - En cada UPDATE enviar ModificadoPor y FechaModificacion = SYSDATETIME().
     - Desactivar = UPDATE ... SET Activo = 0 (nunca DELETE).
   ===================================================================== */

/* ---- Provincias ---------------------------------------------------- */
CREATE OR ALTER TRIGGER dbo.TR_Provincias_Auditoria
ON dbo.Provincias
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM deleted) AND NOT UPDATE(ModificadoPor)
        THROW 50002, 'Toda modificacion debe indicar ModificadoPor (auditoria).', 1;

    INSERT INTO dbo.Auditoria (Tabla, RegistroId, Accion, UsuarioId, ProvinciaId, ValoresAnteriores, ValoresNuevos)
    SELECT 'Provincias', i.ProvinciaId,
           CASE WHEN d.ProvinciaId IS NULL THEN 'INSERT'
                WHEN d.Activo = 1 AND i.Activo = 0 THEN 'DESACTIVAR'
                WHEN d.Activo = 0 AND i.Activo = 1 THEN 'REACTIVAR'
                ELSE 'UPDATE' END,
           CASE WHEN d.ProvinciaId IS NULL THEN i.CreadoPor ELSE i.ModificadoPor END,
           i.ProvinciaId,
           CASE WHEN d.ProvinciaId IS NULL THEN NULL
                ELSE (SELECT d.Codigo, d.Nombre, d.Activo FOR JSON PATH, WITHOUT_ARRAY_WRAPPER) END,
           (SELECT i.Codigo, i.Nombre, i.Activo FOR JSON PATH, WITHOUT_ARRAY_WRAPPER)
    FROM inserted i
    LEFT JOIN deleted d ON d.ProvinciaId = i.ProvinciaId;
END
GO

CREATE OR ALTER TRIGGER dbo.TR_Provincias_NoEliminar
ON dbo.Provincias
INSTEAD OF DELETE
AS
BEGIN
    SET NOCOUNT ON;
    THROW 50001, 'No se permite eliminar registros. Use el borrado logico (Activo = 0).', 1;
END
GO

/* ---- Tipos_Establecimientos --------------------------------------- */
CREATE OR ALTER TRIGGER dbo.TR_Tipos_Establecimientos_Auditoria
ON dbo.Tipos_Establecimientos
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM deleted) AND NOT UPDATE(ModificadoPor)
        THROW 50002, 'Toda modificacion debe indicar ModificadoPor (auditoria).', 1;

    INSERT INTO dbo.Auditoria (Tabla, RegistroId, Accion, UsuarioId, ProvinciaId, ValoresAnteriores, ValoresNuevos)
    SELECT 'Tipos_Establecimientos', i.TipoEstablecimientoId,
           CASE WHEN d.TipoEstablecimientoId IS NULL THEN 'INSERT'
                WHEN d.Activo = 1 AND i.Activo = 0 THEN 'DESACTIVAR'
                WHEN d.Activo = 0 AND i.Activo = 1 THEN 'REACTIVAR'
                ELSE 'UPDATE' END,
           CASE WHEN d.TipoEstablecimientoId IS NULL THEN i.CreadoPor ELSE i.ModificadoPor END,
           NULL,
           CASE WHEN d.TipoEstablecimientoId IS NULL THEN NULL
                ELSE (SELECT d.Nombre, d.Descripcion, d.Activo FOR JSON PATH, WITHOUT_ARRAY_WRAPPER) END,
           (SELECT i.Nombre, i.Descripcion, i.Activo FOR JSON PATH, WITHOUT_ARRAY_WRAPPER)
    FROM inserted i
    LEFT JOIN deleted d ON d.TipoEstablecimientoId = i.TipoEstablecimientoId;
END
GO

CREATE OR ALTER TRIGGER dbo.TR_Tipos_Establecimientos_NoEliminar
ON dbo.Tipos_Establecimientos
INSTEAD OF DELETE
AS
BEGIN
    SET NOCOUNT ON;
    THROW 50001, 'No se permite eliminar registros. Use el borrado logico (Activo = 0).', 1;
END
GO

/* ---- Usuarios (la clave NUNCA se guarda en la bitacora) ------------ */
CREATE OR ALTER TRIGGER dbo.TR_Usuarios_Auditoria
ON dbo.Usuarios
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM deleted) AND NOT UPDATE(ModificadoPor)
        THROW 50002, 'Toda modificacion debe indicar ModificadoPor (auditoria).', 1;

    INSERT INTO dbo.Auditoria (Tabla, RegistroId, Accion, UsuarioId, ProvinciaId, ValoresAnteriores, ValoresNuevos)
    SELECT 'Usuarios', i.UsuarioId,
           CASE WHEN d.UsuarioId IS NULL THEN 'INSERT'
                WHEN d.Activo = 1 AND i.Activo = 0 THEN 'DESACTIVAR'
                WHEN d.Activo = 0 AND i.Activo = 1 THEN 'REACTIVAR'
                ELSE 'UPDATE' END,
           CASE WHEN d.UsuarioId IS NULL THEN i.CreadoPor ELSE i.ModificadoPor END,
           i.ProvinciaId,
           CASE WHEN d.UsuarioId IS NULL THEN NULL
                ELSE (SELECT d.NombreUsuario, d.NombreCompleto, d.Rol, d.ProvinciaId, d.Activo
                      FOR JSON PATH, WITHOUT_ARRAY_WRAPPER) END,
           (SELECT i.NombreUsuario, i.NombreCompleto, i.Rol, i.ProvinciaId, i.Activo,
                   CAST(CASE WHEN d.UsuarioId IS NULL OR d.ClaveHash <> i.ClaveHash THEN 1 ELSE 0 END AS BIT) AS ClaveCambiada
            FOR JSON PATH, WITHOUT_ARRAY_WRAPPER)
    FROM inserted i
    LEFT JOIN deleted d ON d.UsuarioId = i.UsuarioId;
END
GO

CREATE OR ALTER TRIGGER dbo.TR_Usuarios_NoEliminar
ON dbo.Usuarios
INSTEAD OF DELETE
AS
BEGIN
    SET NOCOUNT ON;
    THROW 50001, 'No se permite eliminar registros. Use el borrado logico (Activo = 0).', 1;
END
GO

/* ---- Establecimientos (ademas: no se puede cambiar de provincia) --- */
CREATE OR ALTER TRIGGER dbo.TR_Establecimientos_Auditoria
ON dbo.Establecimientos
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM deleted) AND NOT UPDATE(ModificadoPor)
        THROW 50002, 'Toda modificacion debe indicar ModificadoPor (auditoria).', 1;

    IF UPDATE(ProvinciaId)
       AND EXISTS (SELECT 1
                   FROM inserted i
                   JOIN deleted d ON d.EstablecimientoId = i.EstablecimientoId
                   WHERE d.ProvinciaId <> i.ProvinciaId)
        THROW 50003, 'No se permite cambiar la provincia de un establecimiento.', 1;

    INSERT INTO dbo.Auditoria (Tabla, RegistroId, Accion, UsuarioId, ProvinciaId, ValoresAnteriores, ValoresNuevos)
    SELECT 'Establecimientos', i.EstablecimientoId,
           CASE WHEN d.EstablecimientoId IS NULL THEN 'INSERT'
                WHEN d.Activo = 1 AND i.Activo = 0 THEN 'DESACTIVAR'
                WHEN d.Activo = 0 AND i.Activo = 1 THEN 'REACTIVAR'
                ELSE 'UPDATE' END,
           CASE WHEN d.EstablecimientoId IS NULL THEN i.CreadoPor ELSE i.ModificadoPor END,
           i.ProvinciaId,
           CASE WHEN d.EstablecimientoId IS NULL THEN NULL
                ELSE (SELECT d.Nombre, d.Direccion, d.Telefono, d.TipoEstablecimientoId, d.ProvinciaId, d.Activo
                      FOR JSON PATH, WITHOUT_ARRAY_WRAPPER) END,
           (SELECT i.Nombre, i.Direccion, i.Telefono, i.TipoEstablecimientoId, i.ProvinciaId, i.Activo
            FOR JSON PATH, WITHOUT_ARRAY_WRAPPER)
    FROM inserted i
    LEFT JOIN deleted d ON d.EstablecimientoId = i.EstablecimientoId;
END
GO

CREATE OR ALTER TRIGGER dbo.TR_Establecimientos_NoEliminar
ON dbo.Establecimientos
INSTEAD OF DELETE
AS
BEGIN
    SET NOCOUNT ON;
    THROW 50001, 'No se permite eliminar registros. Use el borrado logico (Activo = 0).', 1;
END
GO

/* ---- Las bitacoras son inmutables (solo se puede insertar) --------- */
CREATE OR ALTER TRIGGER dbo.TR_Auditoria_Inmutable
ON dbo.Auditoria
INSTEAD OF UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;
    THROW 50004, 'La bitacora de auditoria es de solo insercion.', 1;
END
GO

CREATE OR ALTER TRIGGER dbo.TR_BitacoraAccesos_Inmutable
ON dbo.BitacoraAccesos
INSTEAD OF UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;
    THROW 50004, 'La bitacora de accesos es de solo insercion.', 1;
END
GO

/* =====================================================================
   DATOS DE PRUEBA (solo para desarrollo)
   Claves de prueba (hash SHA-256 de texto ASCII/UTF-8):
     admin.nacional -> Admin123*
     usuario.dn     -> Prov123*
     usuario.lro    -> Prov123*
     usuario.stg    -> Prov123*
   Solo 3 provincias de ejemplo: las demas se agregan desde la interfaz
   con el rol AdminNacional. La carga inicial queda en Auditoria con
   UsuarioId NULL (origen: script del sistema).
   ===================================================================== */

-- Provincias
INSERT INTO dbo.Provincias (Codigo, Nombre)
SELECT v.Codigo, v.Nombre
FROM (VALUES ('DN',  N'Distrito Nacional'),
             ('LRO', N'La Romana'),
             ('STG', N'Santiago')) AS v (Codigo, Nombre)
WHERE NOT EXISTS (SELECT 1 FROM dbo.Provincias p WHERE p.Codigo = v.Codigo);
GO

-- Tipos de establecimiento
INSERT INTO dbo.Tipos_Establecimientos (Nombre, Descripcion)
SELECT v.Nombre, v.Descripcion
FROM (VALUES (N'Hospital General',            N'Hospital de atencion general'),
             (N'Hospital Especializado',      N'Hospital de una especialidad medica'),
             (N'Clinica',                     N'Centro de atencion privado o mixto'),
             (N'Centro de Atencion Primaria', N'Unidad de atencion primaria (UNAP)'),
             (N'Policlinico',                 N'Centro de consultas y servicios ambulatorios'),
             (N'Laboratorio Clinico',         N'Centro de analisis y diagnostico')) AS v (Nombre, Descripcion)
WHERE NOT EXISTS (SELECT 1 FROM dbo.Tipos_Establecimientos t WHERE t.Nombre = v.Nombre);
GO

-- Usuarios
INSERT INTO dbo.Usuarios (NombreUsuario, ClaveHash, NombreCompleto, Rol, ProvinciaId)
SELECT v.NombreUsuario, v.ClaveHash, v.NombreCompleto, v.Rol,
       (SELECT p.ProvinciaId FROM dbo.Provincias p WHERE p.Codigo = v.CodigoProvincia)
FROM (VALUES
        (N'admin.nacional', HASHBYTES('SHA2_256', 'Admin123*'), N'Administrador Nacional',    'AdminNacional', CAST(NULL AS VARCHAR(10))),
        (N'usuario.dn',     HASHBYTES('SHA2_256', 'Prov123*'),  N'Usuario Distrito Nacional', 'Provincial',    'DN'),
        (N'usuario.lro',    HASHBYTES('SHA2_256', 'Prov123*'),  N'Usuario La Romana',         'Provincial',    'LRO'),
        (N'usuario.stg',    HASHBYTES('SHA2_256', 'Prov123*'),  N'Usuario Santiago',          'Provincial',    'STG')
     ) AS v (NombreUsuario, ClaveHash, NombreCompleto, Rol, CodigoProvincia)
WHERE NOT EXISTS (SELECT 1 FROM dbo.Usuarios u WHERE u.NombreUsuario = v.NombreUsuario);
GO

-- Establecimientos de ejemplo
INSERT INTO dbo.Establecimientos (Nombre, Direccion, Telefono, TipoEstablecimientoId, ProvinciaId)
SELECT v.Nombre, v.Direccion, v.Telefono,
       (SELECT t.TipoEstablecimientoId FROM dbo.Tipos_Establecimientos t WHERE t.Nombre = v.Tipo),
       (SELECT p.ProvinciaId FROM dbo.Provincias p WHERE p.Codigo = v.CodigoProvincia)
FROM (VALUES
        (N'Hospital General de Distrito Nacional',  N'Av. Principal #100', '809-555-0101', N'Hospital General',            'DN'),
        (N'Centro de Atencion Primaria Los Prados', N'Calle 5 #12',        '809-555-0102', N'Centro de Atencion Primaria', 'DN'),
        (N'Hospital General de La Romana',          N'Av. Libertad #45',   '809-555-0201', N'Hospital General',            'LRO'),
        (N'Policlinico Villa Hermosa',              N'Calle Duarte #8',    '809-555-0202', N'Policlinico',                 'LRO'),
        (N'Hospital General de Santiago',           N'Av. Central #300',   '809-555-0301', N'Hospital General',            'STG'),
        (N'Laboratorio Clinico Cibao',              N'Calle del Sol #77',  '809-555-0302', N'Laboratorio Clinico',         'STG')
     ) AS v (Nombre, Direccion, Telefono, Tipo, CodigoProvincia)
WHERE NOT EXISTS (
    SELECT 1 FROM dbo.Establecimientos e
    JOIN dbo.Provincias p ON p.ProvinciaId = e.ProvinciaId
    WHERE e.Nombre = v.Nombre AND p.Codigo = v.CodigoProvincia);
GO

/* =====================================================================
   VERIFICACION RAPIDA
   ===================================================================== */
-- 1) Vista del admin nacional: todas las provincias
SELECT p.Nombre AS Provincia, e.Nombre AS Establecimiento, t.Nombre AS Tipo, e.Activo
FROM dbo.Establecimientos e
JOIN dbo.Provincias p ON p.ProvinciaId = e.ProvinciaId
JOIN dbo.Tipos_Establecimientos t ON t.TipoEstablecimientoId = e.TipoEstablecimientoId
ORDER BY p.Nombre, e.Nombre;

-- 2) Vista de un usuario provincial (ejemplo: La Romana): solo su provincia
SELECT e.Nombre AS Establecimiento
FROM dbo.Establecimientos e
JOIN dbo.Provincias p ON p.ProvinciaId = e.ProvinciaId
WHERE p.Codigo = 'LRO' AND e.Activo = 1;

-- 3) Ultimos movimientos de la bitacora
SELECT TOP (20) AuditoriaId, FechaHora, Tabla, RegistroId, Accion, UsuarioId, ProvinciaId
FROM dbo.Auditoria
ORDER BY AuditoriaId DESC;

/* Pruebas de proteccion (descomenta una a una; cada una debe dar error):
   DELETE FROM dbo.Establecimientos;                                   -- 50001: no se elimina
   UPDATE dbo.Establecimientos SET Nombre = N'X';                      -- 50002: falta ModificadoPor
   UPDATE dbo.Establecimientos SET ProvinciaId = 1, ModificadoPor = 1; -- 50003: no cambia de provincia
   UPDATE dbo.Auditoria SET Accion = 'INSERT';                         -- 50004: bitacora inmutable
*/
GO
