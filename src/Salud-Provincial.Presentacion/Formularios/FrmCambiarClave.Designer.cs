using System.Drawing;
using System.Windows.Forms;
using Salud_Provincial.Presentacion.Controles;

namespace Salud_Provincial.Presentacion
{
    partial class FrmCambiarClave
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblTitulo = new Label();
            lblClaveActual = new Label();
            txtClaveActual = new TextBox();
            lblClaveNueva = new Label();
            txtClaveNueva = new TextBox();
            lblConfirmar = new Label();
            txtConfirmar = new TextBox();
            btnAceptar = new BotonPildora();
            btnCancelar = new BotonPildora();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI Semibold", 16F, FontStyle.Regular, GraphicsUnit.Point);
            lblTitulo.ForeColor = Color.FromArgb(26, 32, 44);
            lblTitulo.Location = new Point(26, 22);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(230, 37);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Cambiar contraseña";
            // 
            // lblClaveActual
            // 
            lblClaveActual.AutoSize = true;
            lblClaveActual.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lblClaveActual.ForeColor = Color.FromArgb(113, 128, 150);
            lblClaveActual.Location = new Point(27, 80);
            lblClaveActual.Name = "lblClaveActual";
            lblClaveActual.Size = new Size(87, 20);
            lblClaveActual.TabIndex = 1;
            lblClaveActual.Text = "Clave actual";
            // 
            // txtClaveActual
            // 
            txtClaveActual.BorderStyle = BorderStyle.FixedSingle;
            txtClaveActual.Location = new Point(26, 104);
            txtClaveActual.Name = "txtClaveActual";
            txtClaveActual.PasswordChar = '*';
            txtClaveActual.Size = new Size(328, 27);
            txtClaveActual.TabIndex = 0;
            // 
            // lblClaveNueva
            // 
            lblClaveNueva.AutoSize = true;
            lblClaveNueva.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lblClaveNueva.ForeColor = Color.FromArgb(113, 128, 150);
            lblClaveNueva.Location = new Point(27, 146);
            lblClaveNueva.Name = "lblClaveNueva";
            lblClaveNueva.Size = new Size(87, 20);
            lblClaveNueva.TabIndex = 3;
            lblClaveNueva.Text = "Clave nueva";
            // 
            // txtClaveNueva
            // 
            txtClaveNueva.BorderStyle = BorderStyle.FixedSingle;
            txtClaveNueva.Location = new Point(26, 170);
            txtClaveNueva.Name = "txtClaveNueva";
            txtClaveNueva.PasswordChar = '*';
            txtClaveNueva.Size = new Size(328, 27);
            txtClaveNueva.TabIndex = 1;
            // 
            // lblConfirmar
            // 
            lblConfirmar.AutoSize = true;
            lblConfirmar.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lblConfirmar.ForeColor = Color.FromArgb(113, 128, 150);
            lblConfirmar.Location = new Point(27, 212);
            lblConfirmar.Name = "lblConfirmar";
            lblConfirmar.Size = new Size(110, 20);
            lblConfirmar.TabIndex = 5;
            lblConfirmar.Text = "Confirmar clave";
            // 
            // txtConfirmar
            // 
            txtConfirmar.BorderStyle = BorderStyle.FixedSingle;
            txtConfirmar.Location = new Point(26, 236);
            txtConfirmar.Name = "txtConfirmar";
            txtConfirmar.PasswordChar = '*';
            txtConfirmar.Size = new Size(328, 27);
            txtConfirmar.TabIndex = 2;
            // 
            // btnAceptar
            // 
            btnAceptar.Location = new Point(26, 292);
            btnAceptar.Name = "btnAceptar";
            btnAceptar.Size = new Size(160, 44);
            btnAceptar.TabIndex = 3;
            btnAceptar.Text = "Aceptar";
            btnAceptar.UseVisualStyleBackColor = false;
            btnAceptar.Click += btnAceptar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.DialogResult = DialogResult.Cancel;
            btnCancelar.Location = new Point(194, 292);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(160, 44);
            btnCancelar.TabIndex = 4;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            // 
            // FrmCambiarClave
            // 
            AcceptButton = btnAceptar;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            CancelButton = btnCancelar;
            ClientSize = new Size(380, 362);
            Controls.Add(btnCancelar);
            Controls.Add(btnAceptar);
            Controls.Add(txtConfirmar);
            Controls.Add(lblConfirmar);
            Controls.Add(txtClaveNueva);
            Controls.Add(lblClaveNueva);
            Controls.Add(txtClaveActual);
            Controls.Add(lblClaveActual);
            Controls.Add(lblTitulo);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmCambiarClave";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Cambiar contraseña";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitulo;
        private Label lblClaveActual;
        private TextBox txtClaveActual;
        private Label lblClaveNueva;
        private TextBox txtClaveNueva;
        private Label lblConfirmar;
        private TextBox txtConfirmar;
        private BotonPildora btnAceptar;
        private BotonPildora btnCancelar;
    }
}
