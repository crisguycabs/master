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
            this.trackElementos.TabIndex = 1;
            this.trackElementos.ValueChanged += new System.EventHandler(this.trackElementos_ValueChanged);
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
            this.lblTitulo.Text = "LISTA DE DICOMS A CARGAR";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTitulo.DoubleClick += new System.EventHandler(this.lblTitulo_DoubleClick);
            this.lblTitulo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblTitulo_MouseDown);
            this.lblTitulo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblTitulo_MouseMove);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnDelete.FlatAppearance.BorderColor = System.Drawing.Color.RoyalBlue;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Location = new System.Drawing.Point(584, 592);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 25);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Eliminar";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            this.btnDelete.MouseEnter += new System.EventHandler(this.btnSubir_MouseEnter);
            this.btnDelete.MouseLeave += new System.EventHandler(this.btnSubir_MouseLeave);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCancelar.FlatAppearance.BorderColor = System.Drawing.Color.RoyalBlue;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Location = new System.Drawing.Point(745, 592);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 25);
            this.btnCancelar.TabIndex = 5;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            this.btnCancelar.MouseEnter += new System.EventHandler(this.btnSubir_MouseEnter);
            this.btnCancelar.MouseLeave += new System.EventHandler(this.btnSubir_MouseLeave);
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCerrar.FlatAppearance.BorderColor = System.Drawing.Color.RoyalBlue;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.Location = new System.Drawing.Point(665, 592);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(75, 25);
            this.btnCerrar.TabIndex = 4;
            this.btnCerrar.Text = "Crear";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            this.btnCerrar.MouseEnter += new System.EventHandler(this.btnSubir_MouseEnter);
            this.btnCerrar.MouseLeave += new System.EventHandler(this.btnSubir_MouseLeave);
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
            this.lstElementos.TabIndex = 2;
            this.lstElementos.DoubleClick += new System.EventHandler(this.lstElementos_DoubleClick);
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
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.NewProjectVForm_FormClosed);
            this.Load += new System.EventHandler(this.NewProjectVForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.NewProjectVForm_Paint);
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