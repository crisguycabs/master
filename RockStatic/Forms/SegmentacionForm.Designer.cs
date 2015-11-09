namespace RockStatic
{
    partial class SegmentacionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SegmentacionForm));
            this.trackElementos = new System.Windows.Forms.TrackBar();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.pictElemento = new System.Windows.Forms.PictureBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.grpAuto = new System.Windows.Forms.GroupBox();
            this.num2 = new System.Windows.Forms.NumericUpDown();
            this.num1 = new System.Windows.Forms.NumericUpDown();
            this.track2 = new System.Windows.Forms.TrackBar();
            this.track1 = new System.Windows.Forms.TrackBar();
            this.radAuto = new System.Windows.Forms.RadioButton();
            this.radManual = new System.Windows.Forms.RadioButton();
            this.grpBox = new System.Windows.Forms.GroupBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPreview = new System.Windows.Forms.Button();
            this.btnClean = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.numRadio = new System.Windows.Forms.NumericUpDown();
            this.btnLeft = new System.Windows.Forms.Button();
            this.btnRight = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.lstElementos = new System.Windows.Forms.ListBox();
            this.grpManual = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblPunto2 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblPunto1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.pictSmall = new System.Windows.Forms.PictureBox();
            this.menuSegmentacion = new System.Windows.Forms.MenuStrip();
            this.segmentacionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.segmentacionAutomaticaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.segmentacionManualToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.previsualizarToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.guardarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cancelarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnPlano = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackElementos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictElemento)).BeginInit();
            this.grpAuto.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.track2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.track1)).BeginInit();
            this.grpBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRadio)).BeginInit();
            this.grpManual.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictSmall)).BeginInit();
            this.menuSegmentacion.SuspendLayout();
            this.SuspendLayout();
            // 
            // trackElementos
            // 
            this.trackElementos.Location = new System.Drawing.Point(2, 603);
            this.trackElementos.Name = "trackElementos";
            this.trackElementos.Size = new System.Drawing.Size(550, 45);
            this.trackElementos.TabIndex = 1;
            this.trackElementos.ValueChanged += new System.EventHandler(this.trackElementos_ValueChanged);
            // 
            // lblTitulo
            // 
            this.lblTitulo.BackColor = System.Drawing.Color.Green;
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitulo.Font = new System.Drawing.Font("Eras Bold ITC", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblTitulo.Location = new System.Drawing.Point(0, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(760, 30);
            this.lblTitulo.TabIndex = 7;
            this.lblTitulo.Text = "BIENVENIDO!";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTitulo.Click += new System.EventHandler(this.lblTitulo_Click);
            this.lblTitulo.DoubleClick += new System.EventHandler(this.lblTitulo_DoubleClick);
            this.lblTitulo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblTitulo_MouseDown);
            this.lblTitulo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblTitulo_MouseMove);
            // 
            // pictElemento
            // 
            this.pictElemento.Location = new System.Drawing.Point(10, 42);
            this.pictElemento.Name = "pictElemento";
            this.pictElemento.Size = new System.Drawing.Size(534, 555);
            this.pictElemento.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictElemento.TabIndex = 0;
            this.pictElemento.TabStop = false;
            this.pictElemento.Paint += new System.Windows.Forms.PaintEventHandler(this.pictElemento_Paint);
            this.pictElemento.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictElemento_MouseClick);
            this.pictElemento.MouseEnter += new System.EventHandler(this.pictElemento_MouseEnter);
            this.pictElemento.MouseLeave += new System.EventHandler(this.pictElemento_MouseLeave);
            this.pictElemento.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictElemento_MouseMove);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCancelar.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnCancelar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkSeaGreen;
            this.btnCancelar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Green;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Location = new System.Drawing.Point(10, 16);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 25);
            this.btnCancelar.TabIndex = 3;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            this.btnCancelar.MouseEnter += new System.EventHandler(this.btnSubir_MouseEnter);
            this.btnCancelar.MouseLeave += new System.EventHandler(this.btnSubir_MouseLeave);
            // 
            // grpAuto
            // 
            this.grpAuto.Controls.Add(this.num2);
            this.grpAuto.Controls.Add(this.num1);
            this.grpAuto.Controls.Add(this.track2);
            this.grpAuto.Controls.Add(this.track1);
            this.grpAuto.Enabled = false;
            this.grpAuto.Location = new System.Drawing.Point(553, 199);
            this.grpAuto.Name = "grpAuto";
            this.grpAuto.Size = new System.Drawing.Size(199, 93);
            this.grpAuto.TabIndex = 8;
            this.grpAuto.TabStop = false;
            // 
            // num2
            // 
            this.num2.Location = new System.Drawing.Point(147, 46);
            this.num2.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.num2.Name = "num2";
            this.num2.Size = new System.Drawing.Size(45, 22);
            this.num2.TabIndex = 18;
            this.num2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.num2.Value = new decimal(new int[] {
            115,
            0,
            0,
            0});
            this.num2.ValueChanged += new System.EventHandler(this.num2_ValueChanged);
            // 
            // num1
            // 
            this.num1.Location = new System.Drawing.Point(147, 13);
            this.num1.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.num1.Name = "num1";
            this.num1.Size = new System.Drawing.Size(45, 22);
            this.num1.TabIndex = 18;
            this.num1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.num1.Value = new decimal(new int[] {
            51,
            0,
            0,
            0});
            this.num1.ValueChanged += new System.EventHandler(this.num1_ValueChanged);
            // 
            // track2
            // 
            this.track2.Location = new System.Drawing.Point(2, 46);
            this.track2.Maximum = 255;
            this.track2.Name = "track2";
            this.track2.Size = new System.Drawing.Size(141, 45);
            this.track2.TabIndex = 9;
            this.track2.TickFrequency = 32;
            this.track2.Value = 115;
            this.track2.ValueChanged += new System.EventHandler(this.track2_ValueChanged);
            // 
            // track1
            // 
            this.track1.Location = new System.Drawing.Point(2, 13);
            this.track1.Maximum = 255;
            this.track1.Name = "track1";
            this.track1.Size = new System.Drawing.Size(141, 45);
            this.track1.TabIndex = 10;
            this.track1.TickFrequency = 32;
            this.track1.Value = 51;
            this.track1.ValueChanged += new System.EventHandler(this.track1_ValueChanged);
            // 
            // radAuto
            // 
            this.radAuto.Appearance = System.Windows.Forms.Appearance.Button;
            this.radAuto.BackColor = System.Drawing.Color.White;
            this.radAuto.FlatAppearance.CheckedBackColor = System.Drawing.Color.Green;
            this.radAuto.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkSeaGreen;
            this.radAuto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radAuto.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radAuto.ForeColor = System.Drawing.Color.Green;
            this.radAuto.Location = new System.Drawing.Point(561, 173);
            this.radAuto.Name = "radAuto";
            this.radAuto.Size = new System.Drawing.Size(85, 26);
            this.radAuto.TabIndex = 13;
            this.radAuto.Text = "AUTOMATICA";
            this.radAuto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radAuto.UseVisualStyleBackColor = false;
            this.radAuto.CheckedChanged += new System.EventHandler(this.radAuto_CheckedChanged);
            // 
            // radManual
            // 
            this.radManual.Appearance = System.Windows.Forms.Appearance.Button;
            this.radManual.BackColor = System.Drawing.Color.White;
            this.radManual.Checked = true;
            this.radManual.FlatAppearance.CheckedBackColor = System.Drawing.Color.Green;
            this.radManual.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkSeaGreen;
            this.radManual.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radManual.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radManual.ForeColor = System.Drawing.Color.White;
            this.radManual.Location = new System.Drawing.Point(658, 172);
            this.radManual.Name = "radManual";
            this.radManual.Size = new System.Drawing.Size(86, 26);
            this.radManual.TabIndex = 13;
            this.radManual.TabStop = true;
            this.radManual.Text = "MANUAL";
            this.radManual.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radManual.UseVisualStyleBackColor = false;
            this.radManual.CheckedChanged += new System.EventHandler(this.radManual_CheckedChanged);
            // 
            // grpBox
            // 
            this.grpBox.Controls.Add(this.btnPlano);
            this.grpBox.Controls.Add(this.btnTest);
            this.grpBox.Controls.Add(this.label1);
            this.grpBox.Controls.Add(this.btnPreview);
            this.grpBox.Controls.Add(this.btnClean);
            this.grpBox.Controls.Add(this.btnDelete);
            this.grpBox.Controls.Add(this.numRadio);
            this.grpBox.Controls.Add(this.btnLeft);
            this.grpBox.Controls.Add(this.btnRight);
            this.grpBox.Controls.Add(this.btnDown);
            this.grpBox.Controls.Add(this.btnUp);
            this.grpBox.Controls.Add(this.lstElementos);
            this.grpBox.Location = new System.Drawing.Point(553, 389);
            this.grpBox.Name = "grpBox";
            this.grpBox.Size = new System.Drawing.Size(199, 194);
            this.grpBox.TabIndex = 14;
            this.grpBox.TabStop = false;
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(10, 164);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(39, 23);
            this.btnTest.TabIndex = 9;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(113, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 14);
            this.label1.TabIndex = 6;
            this.label1.Text = "h:";
            // 
            // btnPreview
            // 
            this.btnPreview.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnPreview.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkSeaGreen;
            this.btnPreview.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Green;
            this.btnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPreview.Location = new System.Drawing.Point(114, 164);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(75, 23);
            this.btnPreview.TabIndex = 8;
            this.btnPreview.Text = "Preliminar";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // btnClean
            // 
            this.btnClean.Enabled = false;
            this.btnClean.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnClean.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkSeaGreen;
            this.btnClean.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Green;
            this.btnClean.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClean.Location = new System.Drawing.Point(114, 135);
            this.btnClean.Name = "btnClean";
            this.btnClean.Size = new System.Drawing.Size(75, 23);
            this.btnClean.TabIndex = 8;
            this.btnClean.Text = "Limpiar";
            this.btnClean.UseVisualStyleBackColor = true;
            this.btnClean.Click += new System.EventHandler(this.btnClean_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Enabled = false;
            this.btnDelete.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnDelete.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkSeaGreen;
            this.btnDelete.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Green;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Location = new System.Drawing.Point(114, 106);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 8;
            this.btnDelete.Text = "Borrar";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // numRadio
            // 
            this.numRadio.Font = new System.Drawing.Font("Lucida Console", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numRadio.Location = new System.Drawing.Point(138, 81);
            this.numRadio.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numRadio.Name = "numRadio";
            this.numRadio.Size = new System.Drawing.Size(51, 18);
            this.numRadio.TabIndex = 7;
            this.numRadio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numRadio.ValueChanged += new System.EventHandler(this.numRadio_ValueChanged);
            // 
            // btnLeft
            // 
            this.btnLeft.BackColor = System.Drawing.Color.Green;
            this.btnLeft.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnLeft.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnLeft.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnLeft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLeft.Location = new System.Drawing.Point(29, 110);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(20, 20);
            this.btnLeft.TabIndex = 6;
            this.btnLeft.UseVisualStyleBackColor = false;
            this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
            // 
            // btnRight
            // 
            this.btnRight.BackColor = System.Drawing.Color.Green;
            this.btnRight.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnRight.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnRight.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRight.Location = new System.Drawing.Point(73, 110);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(20, 20);
            this.btnRight.TabIndex = 6;
            this.btnRight.UseVisualStyleBackColor = false;
            this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
            // 
            // btnDown
            // 
            this.btnDown.BackColor = System.Drawing.Color.Green;
            this.btnDown.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDown.Location = new System.Drawing.Point(51, 132);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(20, 20);
            this.btnDown.TabIndex = 5;
            this.btnDown.UseVisualStyleBackColor = false;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnUp
            // 
            this.btnUp.BackColor = System.Drawing.Color.Green;
            this.btnUp.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnUp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnUp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUp.Location = new System.Drawing.Point(51, 88);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(20, 20);
            this.btnUp.TabIndex = 4;
            this.btnUp.UseVisualStyleBackColor = false;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // lstElementos
            // 
            this.lstElementos.Font = new System.Drawing.Font("Lucida Console", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstElementos.FormattingEnabled = true;
            this.lstElementos.ItemHeight = 11;
            this.lstElementos.Location = new System.Drawing.Point(9, 16);
            this.lstElementos.Name = "lstElementos";
            this.lstElementos.Size = new System.Drawing.Size(180, 59);
            this.lstElementos.TabIndex = 3;
            this.lstElementos.SelectedIndexChanged += new System.EventHandler(this.lstElementos_SelectedIndexChanged);
            // 
            // grpManual
            // 
            this.grpManual.Controls.Add(this.btnCancel);
            this.grpManual.Controls.Add(this.lblPunto2);
            this.grpManual.Controls.Add(this.label2);
            this.grpManual.Controls.Add(this.lblPunto1);
            this.grpManual.Location = new System.Drawing.Point(553, 293);
            this.grpManual.Name = "grpManual";
            this.grpManual.Size = new System.Drawing.Size(199, 95);
            this.grpManual.TabIndex = 15;
            this.grpManual.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Enabled = false;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Green;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(117, 64);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            this.btnCancel.MouseEnter += new System.EventHandler(this.btnSubir_MouseEnter);
            this.btnCancel.MouseLeave += new System.EventHandler(this.btnSubir_MouseLeave);
            // 
            // lblPunto2
            // 
            this.lblPunto2.AutoSize = true;
            this.lblPunto2.Location = new System.Drawing.Point(60, 69);
            this.lblPunto2.Name = "lblPunto2";
            this.lblPunto2.Size = new System.Drawing.Size(22, 14);
            this.lblPunto2.TabIndex = 1;
            this.lblPunto2.Text = "2...";
            this.lblPunto2.Visible = false;
            // 
            // label2
            // 
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(6, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(188, 47);
            this.label2.TabIndex = 0;
            this.label2.Text = "Seleccione los phantom y core marcando tres puntos para cada elemento";
            // 
            // lblPunto1
            // 
            this.lblPunto1.AutoSize = true;
            this.lblPunto1.Location = new System.Drawing.Point(7, 69);
            this.lblPunto1.Name = "lblPunto1";
            this.lblPunto1.Size = new System.Drawing.Size(56, 14);
            this.lblPunto1.TabIndex = 0;
            this.lblPunto1.Text = "Punto 1...";
            this.lblPunto1.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.btnCancelar);
            this.groupBox1.Location = new System.Drawing.Point(553, 584);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(199, 50);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkSeaGreen;
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Green;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Location = new System.Drawing.Point(114, 16);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 25);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Guardar";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btnSave.MouseEnter += new System.EventHandler(this.btnSubir_MouseEnter);
            this.btnSave.MouseLeave += new System.EventHandler(this.btnSubir_MouseLeave);
            // 
            // pictSmall
            // 
            this.pictSmall.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictSmall.Location = new System.Drawing.Point(590, 42);
            this.pictSmall.Name = "pictSmall";
            this.pictSmall.Size = new System.Drawing.Size(124, 124);
            this.pictSmall.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictSmall.TabIndex = 17;
            this.pictSmall.TabStop = false;
            this.pictSmall.Paint += new System.Windows.Forms.PaintEventHandler(this.pictSmall_Paint);
            // 
            // menuSegmentacion
            // 
            this.menuSegmentacion.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuSegmentacion.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.segmentacionToolStripMenuItem});
            this.menuSegmentacion.Location = new System.Drawing.Point(0, 0);
            this.menuSegmentacion.MdiWindowListItem = this.segmentacionToolStripMenuItem;
            this.menuSegmentacion.Name = "menuSegmentacion";
            this.menuSegmentacion.Size = new System.Drawing.Size(760, 24);
            this.menuSegmentacion.TabIndex = 18;
            this.menuSegmentacion.Text = "menuStrip1";
            this.menuSegmentacion.Visible = false;
            // 
            // segmentacionToolStripMenuItem
            // 
            this.segmentacionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.segmentacionAutomaticaToolStripMenuItem,
            this.segmentacionManualToolStripMenuItem,
            this.toolStripSeparator2,
            this.previsualizarToolStripMenuItem1,
            this.toolStripSeparator1,
            this.guardarToolStripMenuItem,
            this.cancelarToolStripMenuItem});
            this.segmentacionToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.segmentacionToolStripMenuItem.MergeIndex = 1;
            this.segmentacionToolStripMenuItem.Name = "segmentacionToolStripMenuItem";
            this.segmentacionToolStripMenuItem.Size = new System.Drawing.Size(95, 20);
            this.segmentacionToolStripMenuItem.Text = "&Segmentacion";
            // 
            // segmentacionAutomaticaToolStripMenuItem
            // 
            this.segmentacionAutomaticaToolStripMenuItem.Name = "segmentacionAutomaticaToolStripMenuItem";
            this.segmentacionAutomaticaToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.segmentacionAutomaticaToolStripMenuItem.Text = "Segmentacion &Automatica";
            this.segmentacionAutomaticaToolStripMenuItem.Click += new System.EventHandler(this.segmentacionAutomaticaToolStripMenuItem_Click);
            // 
            // segmentacionManualToolStripMenuItem
            // 
            this.segmentacionManualToolStripMenuItem.Name = "segmentacionManualToolStripMenuItem";
            this.segmentacionManualToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.segmentacionManualToolStripMenuItem.Text = "Segmentacion &Manual";
            this.segmentacionManualToolStripMenuItem.Click += new System.EventHandler(this.segmentacionManualToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(212, 6);
            // 
            // previsualizarToolStripMenuItem1
            // 
            this.previsualizarToolStripMenuItem1.Name = "previsualizarToolStripMenuItem1";
            this.previsualizarToolStripMenuItem1.Size = new System.Drawing.Size(215, 22);
            this.previsualizarToolStripMenuItem1.Text = "&Previsualizar";
            this.previsualizarToolStripMenuItem1.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(212, 6);
            // 
            // guardarToolStripMenuItem
            // 
            this.guardarToolStripMenuItem.Name = "guardarToolStripMenuItem";
            this.guardarToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.guardarToolStripMenuItem.Text = "&Guardar";
            this.guardarToolStripMenuItem.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cancelarToolStripMenuItem
            // 
            this.cancelarToolStripMenuItem.Name = "cancelarToolStripMenuItem";
            this.cancelarToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.cancelarToolStripMenuItem.Text = "&Cancelar";
            this.cancelarToolStripMenuItem.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnPlano
            // 
            this.btnPlano.Location = new System.Drawing.Point(55, 164);
            this.btnPlano.Name = "btnPlano";
            this.btnPlano.Size = new System.Drawing.Size(39, 23);
            this.btnPlano.TabIndex = 9;
            this.btnPlano.Text = "Plan";
            this.btnPlano.UseVisualStyleBackColor = true;
            this.btnPlano.Click += new System.EventHandler(this.btnPlano_Click);
            // 
            // SegmentacionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(760, 650);
            this.ControlBox = false;
            this.Controls.Add(this.pictSmall);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpManual);
            this.Controls.Add(this.grpBox);
            this.Controls.Add(this.radManual);
            this.Controls.Add(this.radAuto);
            this.Controls.Add(this.grpAuto);
            this.Controls.Add(this.trackElementos);
            this.Controls.Add(this.pictElemento);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.menuSegmentacion);
            this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuSegmentacion;
            this.Name = "SegmentacionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "REVISAR ELEMENTOS";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SegmentacionForm_FormClosed);
            this.Load += new System.EventHandler(this.CheckForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackElementos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictElemento)).EndInit();
            this.grpAuto.ResumeLayout(false);
            this.grpAuto.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.track2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.track1)).EndInit();
            this.grpBox.ResumeLayout(false);
            this.grpBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRadio)).EndInit();
            this.grpManual.ResumeLayout(false);
            this.grpManual.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictSmall)).EndInit();
            this.menuSegmentacion.ResumeLayout(false);
            this.menuSegmentacion.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictElemento;
        private System.Windows.Forms.TrackBar trackElementos;
        public System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.GroupBox grpAuto;
        private System.Windows.Forms.TrackBar track2;
        private System.Windows.Forms.RadioButton radAuto;
        private System.Windows.Forms.TrackBar track1;
        private System.Windows.Forms.RadioButton radManual;
        private System.Windows.Forms.GroupBox grpBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClean;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.NumericUpDown numRadio;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.ListBox lstElementos;
        private System.Windows.Forms.GroupBox grpManual;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblPunto2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblPunto1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.PictureBox pictSmall;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.NumericUpDown num2;
        private System.Windows.Forms.NumericUpDown num1;
        private System.Windows.Forms.MenuStrip menuSegmentacion;
        private System.Windows.Forms.ToolStripMenuItem segmentacionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem segmentacionAutomaticaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem segmentacionManualToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem guardarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cancelarToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem previsualizarToolStripMenuItem1;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnPlano;
    }
}