# Salud-Provincial

Sistema para centralizar y estandarizar la información de hospitales y centros de atención de las provincias del país, desarrollado como **Examen Parcial de Arquitectura de Software (ISW-114)** en la Universidad Central del Este (UCE).

## Problema

El Ministerio de Salud desconoce el volumen de información que se maneja en cada provincia, y los datos quedan aislados en "islas". Este sistema permite que:

- **Cada provincia gestione su propia información**, sin poder ver ni modificar la de otras provincias.
- **Un administrador nacional** consulte y administre la información de todas las provincias.
- **Nuevas provincias se agreguen desde la interfaz**, sin cambiar el código.

## Arquitectura

| Aspecto | Decisión |
|---|---|
| Multi-Tenant | **Patrón Pool**: una sola base de datos y un solo esquema; la columna `ProvinciaId` separa la información de cada provincia |
| Capas | **N Capas**: Presentación, Lógica de Negocio, Acceso a Datos y Entidades |
| Patrones | Singleton (conexión y sesión), Repository, inyección de dependencias por interfaces |
| Datos | SQL Server |
| Interfaz | C# con Windows Forms |

### Estructura de la solución

```
Salud-Provincial.sln
├── Salud-Provincial.Entidades     Modelos (Provincia, Usuario, TipoEstablecimiento, Establecimiento)
├── Salud-Provincial.AccesoDatos   Conexión (Singleton) y repositorios
├── Salud-Provincial.Negocio       Servicios, validaciones, sesión (Singleton) y reglas de permisos
├── Salud-Provincial.Presentacion  Formularios de Windows Forms
└── BaseDatos/SaludDB.sql          Script de la base de datos
```

Las referencias van en un solo sentido: `Presentacion → Negocio → AccesoDatos → Entidades`. La capa de Presentación no accede directamente a la base de datos.

### Modelo de datos

`Provincias` · `Usuarios` · `Tipos_Establecimientos` · `Establecimientos`

Además, dos tablas de auditoría: `Auditoria` (bitácora de cambios) y `BitacoraAccesos` (inicios de sesión).

## Buenas prácticas aplicadas

- **Borrado lógico:** los registros se desactivan (`Activo = 0`), nunca se eliminan. Un trigger bloquea el `DELETE` físico.
- **Auditoría:** todas las tablas registran quién creó y modificó cada registro, y una bitácora inmutable guarda los valores anteriores y nuevos.
- **Aislamiento por provincia:** el filtro se decide en la capa de Negocio según el rol y la provincia del usuario en sesión, no según lo que envíe el formulario.
- **Consultas parametrizadas** para evitar inyección SQL.
- **Contraseñas con hash**, nunca en texto plano.

## Requisitos

- Visual Studio 2022 o superior con la carga de trabajo de **desarrollo de escritorio con .NET**
- SQL Server (Express o LocalDB)
- SQL Server Management Studio (opcional)

## Puesta en marcha

1. Clona el repositorio:
   ```
   git clone https://github.com/AdAstraAbyssosque67/Salud-Provincial.git
   ```
2. Ejecuta `BaseDatos/SaludDB.sql` en SQL Server. Crea la base `SaludDB`, las tablas, los triggers de auditoría y datos de prueba.
3. Abre `Salud-Provincial.sln` en Visual Studio.
4. Ajusta la cadena de conexión en el `App.config` del proyecto de Presentación (disponible a partir de la Parte 3).
5. Establece `Salud-Provincial.Presentacion` como proyecto de inicio y ejecuta.

### Usuarios de prueba

Solo para desarrollo. Las claves de prueba están indicadas en el encabezado de datos de prueba del script `SaludDB.sql`.

| Usuario | Rol |
|---|---|
| `admin.nacional` | Administrador nacional |
| `usuario.dn` | Provincial: Distrito Nacional |
| `usuario.lro` | Provincial: La Romana |
| `usuario.stg` | Provincial: Santiago |

## Avance del proyecto

| Parte | Contenido | Estado |
|---|---|---|
| 1 | Script de base de datos (Patrón Pool, auditoría, borrado lógico) | Completada |
| 2 | Estructura de la solución y capa de Entidades | En curso |
| 3 | Capa de Acceso a Datos (Singleton de conexión y repositorios) | Pendiente |
| 4 | Capa de Lógica de Negocio (servicios, sesión, permisos) | Pendiente |
| 5 | Capa de Presentación (login y CRUD) | Pendiente |
| 6 | Pruebas de aislamiento y documentación | Pendiente |

## Autor

José Garrido Mesa · Ingeniería de Software · Universidad Central del Este (UCE)
