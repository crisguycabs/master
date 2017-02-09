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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numRadio = new System.Windows.Forms.NumericUpDown();
            this.btnLeft = new System.Windows.Forms.Button();
            this.btnRight = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackElementos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictElemento)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRadio)).BeginInit();
            this.SuspendLayout();
            // 
            // txtCounter
            // 
            this.txtCounter.AutoSize = true;
            this.txtCounter.Location = new System.Drawing.Point(448, 544);
            this.txtCounter.Name = "txtCounter";
            this.txtCounter.Size = new System.Drawing.Size(42, 14);
            this.txtCounter.TabIndex = 16;
            this.txtCounter.Text = "label1";
            // 
            // trackElementos
            // 
            this.trackElementos.Location = new System.Drawing.Point(2, 528);
            this.trackElementos.Name = "trackElementos";
            this.trackElementos.Size = new System.Drawing.Size(445, 45);
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
            this.lblTitulo.Size = new System.Drawing.Size(827, 30);
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
            this.btnDelete.Location = new System.Drawing.Point(583, 544);
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
            this.btnCancelar.Location = new System.Drawing.Point(744, 544);
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
            this.btnCerrar.Location = new System.Drawing.Point(664, 544);
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
            this.lstElementos.Location = new System.Drawing.Point(499, 44);
            this.lstElementos.Name = "lstElementos";
            this.lstElementos.Size = new System.Drawing.Size(321, 394);
            this.lstElementos.TabIndex = 2;
            this.lstElementos.DoubleClick += new System.EventHandler(this.lstElementos_DoubleClick);
            // 
            // pictElemento
            // 
            this.pictElemento.Location = new System.Drawing.Point(10, 44);
            this.pictElemento.Name = "pictElemento";
            this.pictElemento.Size = new System.Drawing.Size(480, 480);
            this.pictElemento.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictElemento.TabIndex = 9;
            this.pictElemento.TabStop = false;
            this.pictElemento.Paint += new System.Windows.Forms.PaintEventHandler(this.pictElemento_Paint);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.numRadio);
            this.groupBox1.Controls.Add(this.btnLeft);
            this.groupBox1.Controls.Add(this.btnRight);
            this.groupBox1.Controls.Add(this.btnDown);
            this.groupBox1.Controls.Add(this.btnUp);
            this.groupBox1.Location = new System.Drawing.Point(499, 439);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(321, 98);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Segmentacion";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(182, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 14);
            this.label1.TabIndex = 10;
            this.label1.Text = "Radio:";
            // 
            // numRadio
            // 
            this.numRadio.Font = new System.Drawing.Font("Lucida Console", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numRadio.Location = new System.Drawing.Point(225, 43);
            this.numRadio.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numRadio.Name = "numRadio";
            this.numRadio.Size = new System.Drawing.Size(51, 18);
            this.numRadio.TabIndex = 13;
            this.numRadio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numRadio.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRadio.ValueChanged += new System.EventHandler(this.numRadio_ValueChanged);
            // 
            // btnLeft
            // 
            this.btnLeft.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnLeft.FlatAppearance.BorderColor = System.Drawing.Color.RoyalBlue;
            this.btnLeft.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnLeft.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnLeft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLeft.Location = new System.Drawing.Point(95, 43);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(20, 20);
            this.btnLeft.TabIndex = 11;
            this.btnLeft.UseVisualStyleBackColor = false;
            this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
            // 
            // btnRight
            // 
            this.btnRight.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnRight.FlatAppearance.BorderColor = System.Drawing.Color.RoyalBlue;
            this.btnRight.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnRight.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRight.Location = new System.Drawing.Point(139, 43);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(20, 20);
            this.btnRight.TabIndex = 12;
            this.btnRight.UseVisualStyleBackColor = false;
            this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
            // 
            // btnDown
            // 
            this.btnDown.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnDown.FlatAppearance.BorderColor = System.Drawing.Color.RoyalBlue;
            this.btnDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDown.Location = new System.Drawing.Point(117, 65);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(20, 20);
            this.btnDown.TabIndex = 9;
            this.btnDown.UseVisualStyleBackColor = false;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnUp
            // 
            this.btnUp.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnUp.FlatAppearance.BorderColor = System.Drawing.Color.RoyalBlue;
            this.btnUp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnUp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUp.Location = new System.Drawing.Point(117, 21);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(20, 20);
            this.btnUp.TabIndex = 8;
            this.btnUp.UseVisualStyleBackColor = false;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // NewProjectVForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(827, 577);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
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
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRadio)).EndInit();
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
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numRadio;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnUp;
    }
}