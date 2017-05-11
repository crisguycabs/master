namespace RockVision
{
    partial class CheckForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CheckForm));
            this.lblTitulo = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numRadio = new System.Windows.Forms.NumericUpDown();
            this.btnLeft = new System.Windows.Forms.Button();
            this.btnRight = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.txtCounter = new System.Windows.Forms.Label();
            this.trackElementos = new System.Windows.Forms.TrackBar();
            this.pictElemento = new System.Windows.Forms.PictureBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numFin = new System.Windows.Forms.NumericUpDown();
            this.numIni = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbDatacubo = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.trackContraste = new System.Windows.Forms.TrackBar();
            this.trackBrillo = new System.Windows.Forms.TrackBar();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRadio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackElementos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictElemento)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numIni)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackContraste)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBrillo)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.BackColor = System.Drawing.Color.RoyalBlue;
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitulo.Font = new System.Drawing.Font("Eras Bold ITC", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblTitulo.Location = new System.Drawing.Point(0, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(690, 30);
            this.lblTitulo.TabIndex = 17;
            this.lblTitulo.Text = "LISTA DE DICOMS A CARGAR";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTitulo.DoubleClick += new System.EventHandler(this.lblTitulo_DoubleClick);
            this.lblTitulo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblTitulo_MouseDown);
            this.lblTitulo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblTitulo_MouseMove);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.numRadio);
            this.groupBox1.Controls.Add(this.btnLeft);
            this.groupBox1.Controls.Add(this.btnRight);
            this.groupBox1.Controls.Add(this.btnDown);
            this.groupBox1.Controls.Add(this.btnUp);
            this.groupBox1.Location = new System.Drawing.Point(476, 221);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(204, 98);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SEGMENTACION";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(99, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 14);
            this.label1.TabIndex = 10;
            this.label1.Text = "Radio:";
            // 
            // numRadio
            // 
            this.numRadio.Font = new System.Drawing.Font("Lucida Console", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numRadio.Location = new System.Drawing.Point(142, 43);
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
            this.btnLeft.Location = new System.Drawing.Point(12, 43);
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
            this.btnRight.Location = new System.Drawing.Point(56, 43);
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
            this.btnDown.Location = new System.Drawing.Point(34, 65);
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
            this.btnUp.Location = new System.Drawing.Point(34, 21);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(20, 20);
            this.btnUp.TabIndex = 8;
            this.btnUp.UseVisualStyleBackColor = false;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // txtCounter
            // 
            this.txtCounter.AutoSize = true;
            this.txtCounter.Location = new System.Drawing.Point(404, 541);
            this.txtCounter.Name = "txtCounter";
            this.txtCounter.Size = new System.Drawing.Size(63, 14);
            this.txtCounter.TabIndex = 22;
            this.txtCounter.Text = "999 de 999";
            // 
            // trackElementos
            // 
            this.trackElementos.Location = new System.Drawing.Point(0, 533);
            this.trackElementos.Name = "trackElementos";
            this.trackElementos.Size = new System.Drawing.Size(402, 45);
            this.trackElementos.TabIndex = 18;
            this.trackElementos.ValueChanged += new System.EventHandler(this.trackElementos_ValueChanged);
            // 
            // pictElemento
            // 
            this.pictElemento.Location = new System.Drawing.Point(10, 38);
            this.pictElemento.Name = "pictElemento";
            this.pictElemento.Size = new System.Drawing.Size(460, 460);
            this.pictElemento.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictElemento.TabIndex = 21;
            this.pictElemento.TabStop = false;
            this.pictElemento.Paint += new System.Windows.Forms.PaintEventHandler(this.pictElemento_Paint);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCancelar.FlatAppearance.BorderColor = System.Drawing.Color.RoyalBlue;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Location = new System.Drawing.Point(605, 536);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 25);
            this.btnCancelar.TabIndex = 20;
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
            this.btnCerrar.Location = new System.Drawing.Point(525, 536);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(75, 25);
            this.btnCerrar.TabIndex = 19;
            this.btnCerrar.Text = "Crear";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            this.btnCerrar.MouseEnter += new System.EventHandler(this.btnSubir_MouseEnter);
            this.btnCerrar.MouseLeave += new System.EventHandler(this.btnSubir_MouseLeave);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numFin);
            this.groupBox2.Controls.Add(this.numIni);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(476, 327);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(204, 100);
            this.groupBox2.TabIndex = 24;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "DICOMS A USAR";
            // 
            // numFin
            // 
            this.numFin.Location = new System.Drawing.Point(106, 59);
            this.numFin.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numFin.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numFin.Name = "numFin";
            this.numFin.Size = new System.Drawing.Size(41, 22);
            this.numFin.TabIndex = 24;
            this.numFin.Value = new decimal(new int[] {
            999,
            0,
            0,
            0});
            // 
            // numIni
            // 
            this.numIni.Location = new System.Drawing.Point(106, 31);
            this.numIni.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numIni.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numIni.Name = "numIni";
            this.numIni.Size = new System.Drawing.Size(41, 22);
            this.numIni.TabIndex = 0;
            this.numIni.Value = new decimal(new int[] {
            999,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 14);
            this.label3.TabIndex = 25;
            this.label3.Text = "Segundo DICOM: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 14);
            this.label2.TabIndex = 23;
            this.label2.Text = "Primer DICOM: ";
            // 
            // cmbDatacubo
            // 
            this.cmbDatacubo.FormattingEnabled = true;
            this.cmbDatacubo.Location = new System.Drawing.Point(10, 505);
            this.cmbDatacubo.Name = "cmbDatacubo";
            this.cmbDatacubo.Size = new System.Drawing.Size(161, 22);
            this.cmbDatacubo.TabIndex = 25;
            this.cmbDatacubo.SelectedIndexChanged += new System.EventHandler(this.cmbDatacubo_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.trackContraste);
            this.groupBox3.Controls.Add(this.trackBrillo);
            this.groupBox3.Location = new System.Drawing.Point(476, 38);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(204, 173);
            this.groupBox3.TabIndex = 26;
            this.groupBox3.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(73, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 14);
            this.label4.TabIndex = 10;
            this.label4.Text = "Contraste";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(84, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 14);
            this.label5.TabIndex = 9;
            this.label5.Text = "Brillo";
            // 
            // trackContraste
            // 
            this.trackContraste.LargeChange = 50;
            this.trackContraste.Location = new System.Drawing.Point(10, 66);
            this.trackContraste.Maximum = 100;
            this.trackContraste.Minimum = -100;
            this.trackContraste.Name = "trackContraste";
            this.trackContraste.Size = new System.Drawing.Size(185, 45);
            this.trackContraste.SmallChange = 10;
            this.trackContraste.TabIndex = 8;
            this.trackContraste.TickFrequency = 20;
            this.trackContraste.Scroll += new System.EventHandler(this.trackContraste_Scroll);
            // 
            // trackBrillo
            // 
            this.trackBrillo.LargeChange = 50;
            this.trackBrillo.Location = new System.Drawing.Point(10, 15);
            this.trackBrillo.Maximum = 255;
            this.trackBrillo.Minimum = -255;
            this.trackBrillo.Name = "trackBrillo";
            this.trackBrillo.Size = new System.Drawing.Size(185, 45);
            this.trackBrillo.SmallChange = 10;
            this.trackBrillo.TabIndex = 7;
            this.trackBrillo.TickFrequency = 32;
            this.trackBrillo.Scroll += new System.EventHandler(this.trackBrillo_Scroll);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.RoyalBlue;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(58, 128);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(89, 25);
            this.button1.TabIndex = 20;
            this.button1.Text = "Reestablecer";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // CheckForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(690, 573);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.cmbDatacubo);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtCounter);
            this.Controls.Add(this.trackElementos);
            this.Controls.Add(this.pictElemento);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.lblTitulo);
            this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CheckForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "REVISAR LOS ELEMENTOS DICOM CARGADOS";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CheckForm_FormClosing);
            this.Load += new System.EventHandler(this.CheckForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRadio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackElementos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictElemento)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numIni)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackContraste)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBrillo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numRadio;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Label txtCounter;
        private System.Windows.Forms.TrackBar trackElementos;
        private System.Windows.Forms.PictureBox pictElemento;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmbDatacubo;
        private System.Windows.Forms.NumericUpDown numFin;
        private System.Windows.Forms.NumericUpDown numIni;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TrackBar trackContraste;
        private System.Windows.Forms.TrackBar trackBrillo;
    }
}