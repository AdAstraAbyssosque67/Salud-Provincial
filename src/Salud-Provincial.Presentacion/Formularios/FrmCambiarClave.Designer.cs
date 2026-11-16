using System.Drawing;
using System.Windows.Forms;

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
            panelEncabezado = new Panel();
            lblTitulo = new Label();
            lblClaveActual = new Label();
            txtClaveActual = new TextBox();
            lblClaveNueva = new Label();
            txtClaveNueva = new TextBox();
            lblConfirmar = new Label();
            txtConfirmar = new TextBox();
            btnAceptar = new Button();
            btnCancelar = new Button();
            panelEncabezado.SuspendLayout();
            SuspendLayout();
            // 
            // panelEncabezado
            // 
            panelEncabezado.BackColor = Color.FromArgb(31, 181, 197);
            panelEncabezado.Controls.Add(lblTitulo);
            panelEncabezado.Dock = DockStyle.Top;
            panelEncabezado.Location = new Point(0, 0);
            panelEncabezado.Name = "panelEncabezado";
            panelEncabezado.Size = new Size(420, 56);
            panelEncabezado.TabIndex = 8;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 14F);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.Location = new Point(16, 12);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(190, 32);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Cambiar contraseña";
            // 
            // lblClaveActual
            // 
            lblClaveActual.AutoSize = true;
            lblClaveActual.Location = new Point(20, 84);
            lblClaveActual.Name = "lblClaveActual";
            lblClaveActual.Size = new Size(100, 20);
            lblClaveActual.TabIndex = 0;
            lblClaveActual.Text = "Clave actual:";
            // 
            // txtClaveActual
            // 
            txtClaveActual.Location = new Point(175, 81);
            txtClaveActual.Name = "txtClaveActual";
            txtClaveActual.PasswordChar = '*';
            txtClaveActual.Size = new Size(225, 27);
            txtClaveActual.TabIndex = 1;
            // 
            // lblClaveNueva
            // 
            lblClaveNueva.AutoSize = true;
            lblClaveNueva.Location = new Point(20, 124);
            lblClaveNueva.Name = "lblClaveNueva";
            lblClaveNueva.Size = new Size(100, 20);
            lblClaveNueva.TabIndex = 2;
            lblClaveNueva.Text = "Clave nueva:";
            // 
            // txtClaveNueva
            // 
            txtClaveNueva.Location = new Point(175, 121);
            txtClaveNueva.Name = "txtClaveNueva";
            txtClaveNueva.PasswordChar = '*';
            txtClaveNueva.Size = new Size(225, 27);
            txtClaveNueva.TabIndex = 3;
            // 
            // lblConfirmar
            // 
            lblConfirmar.AutoSize = true;
            lblConfirmar.Location = new Point(20, 164);
            lblConfirmar.Name = "lblConfirmar";
            lblConfirmar.Size = new Size(125, 20);
            lblConfirmar.TabIndex = 4;
            lblConfirmar.Text = "Confirmar clave:";
            // 
            // txtConfirmar
            // 
            txtConfirmar.Location = new Point(175, 161);
            txtConfirmar.Name = "txtConfirmar";
            txtConfirmar.PasswordChar = '*';
            txtConfirmar.Size = new Size(225, 27);
            txtConfirmar.TabIndex = 5;
            // 
            // btnAceptar
            // 
            btnAceptar.Location = new Point(175, 214);
            btnAceptar.Name = "btnAceptar";
            btnAceptar.Size = new Size(110, 36);
            btnAceptar.TabIndex = 6;
            btnAceptar.Text = "Aceptar";
            btnAceptar.UseVisualStyleBackColor = true;
            btnAceptar.Click += btnAceptar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.DialogResult = DialogResult.Cancel;
            btnCancelar.Location = new Point(290, 214);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(110, 36);
            btnCancelar.TabIndex = 7;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            // 
            // FrmCambiarClave
            // 
            AcceptButton = btnAceptar;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            CancelButton = btnCancelar;
            ClientSize = new Size(420, 274);
            Controls.Add(btnCancelar);
            Controls.Add(btnAceptar);
            Controls.Add(txtConfirmar);
            Controls.Add(lblConfirmar);
            Controls.Add(txtClaveNueva);
            Controls.Add(lblClaveNueva);
            Controls.Add(txtClaveActual);
            Controls.Add(lblClaveActual);
            Controls.Add(panelEncabezado);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmCambiarClave";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Cambiar contraseña";
            panelEncabezado.ResumeLayout(false);
            panelEncabezado.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panelEncabezado;
        private Label lblTitulo;
        private Label lblClaveActual;
        private TextBox txtClaveActual;
        private Label lblClaveNueva;
        private TextBox txtClaveNueva;
        private Label lblConfirmar;
        private TextBox txtConfirmar;
        private Button btnAceptar;
        private Button btnCancelar;
    }
}
