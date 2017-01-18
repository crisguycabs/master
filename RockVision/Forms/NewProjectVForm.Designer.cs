namespace RockVision
{
    partial class NewProjectVForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewProjectVForm));
            this.txtCounter = new System.Windows.Forms.Label();
            this.trackElementos = new System.Windows.Forms.TrackBar();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.lstElementos = new System.Windows.Forms.ListBox();
            this.pictElemento = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackElementos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictElemento)).BeginInit();
            this.SuspendLayout();
            // 
            // txtCounter
            // 
            this.txtCounter.AutoSize = true;
            this.txtCounter.Location = new System.Drawing.Point(500, 592);
            this.txtCounter.Name = "txtCounter";
            this.txtCounter.Size = new System.Drawing.Size(42, 14);
            this.txtCounter.TabIndex = 16;
            this.txtCounter.Text = "label1";
            // 
            // trackElementos
            // 
            this.trackElementos.Location = new System.Drawing.Point(2, 576);
            this.trackElementos.Name = "trackElementos";
            this.trackElementos.Size = new System.Drawing.Size(495, 45);
            this.trackElementos.TabIndex = 10;
            // 
            // lblTitulo
            // 
            this.lblTitulo.BackColor = System.Drawing.Color.RoyalBlue;
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitulo.Font = new System.Drawing.Font("Eras Bold ITC", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblTitulo.Location = new System.Drawing.Point(0, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(828, 30);
            this.lblTitulo.TabIndex = 15;
            this.lblTitulo.Text = "BIENVENIDO!";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnDelete.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Location = new System.Drawing.Point(584, 592);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 25);
            this.btnDelete.TabIndex = 12;
            this.btnDelete.Text = "Eliminar";
            this.btnDelete.UseVisualStyleBackColor = false;
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCancelar.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Location = new System.Drawing.Point(745, 592);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 25);
            this.btnCancelar.TabIndex = 13;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCerrar.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.Location = new System.Drawing.Point(665, 592);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(75, 25);
            this.btnCerrar.TabIndex = 14;
            this.btnCerrar.Text = "Guardar";
            this.btnCerrar.UseVisualStyleBackColor = false;
            // 
            // lstElementos
            // 
            this.lstElementos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstElementos.FormattingEnabled = true;
            this.lstElementos.HorizontalScrollbar = true;
            this.lstElementos.ItemHeight = 14;
            this.lstElementos.Location = new System.Drawing.Point(500, 44);
            this.lstElementos.Name = "lstElementos";
            this.lstElementos.Size = new System.Drawing.Size(321, 534);
            this.lstElementos.TabIndex = 11;
            // 
            // pictElemento
            // 
            this.pictElemento.Location = new System.Drawing.Point(10, 44);
            this.pictElemento.Name = "pictElemento";
            this.pictElemento.Size = new System.Drawing.Size(480, 536);
            this.pictElemento.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictElemento.TabIndex = 9;
            this.pictElemento.TabStop = false;
            // 
            // NewProjectVForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(828, 623);
            this.ControlBox = false;
            this.Controls.Add(this.txtCounter);
            this.Controls.Add(this.trackElementos);
            this.Controls.Add(this.pictElemento);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.lstElementos);
            this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NewProjectVForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "REVISAR ELEMENTOS NUEVO PROYECTO DE VISUALIZACION";
            ((System.ComponentModel.ISupportInitialize)(this.trackElementos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictElemento)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label txtCounter;
        private System.Windows.Forms.TrackBar trackElementos;
        private System.Windows.Forms.PictureBox pictElemento;
        public System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.ListBox lstElementos;
    }
}