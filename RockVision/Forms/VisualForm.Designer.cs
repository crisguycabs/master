﻿namespace RockVision
{
    partial class VisualForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VisualForm));
            this.lblTitulo = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tab2D = new System.Windows.Forms.TabPage();
            this.chkUmbral = new System.Windows.Forms.CheckBox();
            this.chkNorm = new System.Windows.Forms.CheckBox();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.pictHor = new System.Windows.Forms.PictureBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.numHmax = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.numHmin = new System.Windows.Forms.NumericUpDown();
            this.lblHmin = new System.Windows.Forms.Label();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.rangeHist = new Zzzz.ZzzzRangeBar();
            this.labelCorte = new System.Windows.Forms.Label();
            this.trackCorte = new System.Windows.Forms.TrackBar();
            this.labelSlide = new System.Windows.Forms.Label();
            this.trackBar = new System.Windows.Forms.TrackBar();
            this.groupUmbral = new System.Windows.Forms.GroupBox();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnBorrar = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.rangoMin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rangoMax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.color = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupNorm = new System.Windows.Forms.GroupBox();
            this.pictGradiente = new System.Windows.Forms.PictureBox();
            this.numNmax = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.numNmin = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.rangeBar = new Zzzz.ZzzzRangeBar();
            this.pictTrans = new System.Windows.Forms.PictureBox();
            this.tab3D = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.btnReset = new System.Windows.Forms.Button();
            this.trackBar3 = new System.Windows.Forms.TrackBar();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.renderWindowControl1 = new Kitware.VTK.RenderWindowControl();
            this.tabControl1.SuspendLayout();
            this.tab2D.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictHor)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numHmax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHmin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackCorte)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
            this.groupUmbral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.groupNorm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictGradiente)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNmax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNmin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictTrans)).BeginInit();
            this.tab3D.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
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
            this.lblTitulo.Size = new System.Drawing.Size(1200, 30);
            this.lblTitulo.TabIndex = 19;
            this.lblTitulo.Text = "BIENVENIDO";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTitulo.DoubleClick += new System.EventHandler(this.lblTitulo_DoubleClick);
            this.lblTitulo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblTitulo_MouseDown);
            this.lblTitulo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblTitulo_MouseMove);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tab2D);
            this.tabControl1.Controls.Add(this.tab3D);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 30);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1200, 620);
            this.tabControl1.TabIndex = 20;
            // 
            // tab2D
            // 
            this.tab2D.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tab2D.Controls.Add(this.chkUmbral);
            this.tab2D.Controls.Add(this.chkNorm);
            this.tab2D.Controls.Add(this.btnCerrar);
            this.tab2D.Controls.Add(this.pictHor);
            this.tab2D.Controls.Add(this.groupBox3);
            this.tab2D.Controls.Add(this.labelCorte);
            this.tab2D.Controls.Add(this.trackCorte);
            this.tab2D.Controls.Add(this.labelSlide);
            this.tab2D.Controls.Add(this.trackBar);
            this.tab2D.Controls.Add(this.groupUmbral);
            this.tab2D.Controls.Add(this.groupNorm);
            this.tab2D.Controls.Add(this.pictTrans);
            this.tab2D.Location = new System.Drawing.Point(4, 23);
            this.tab2D.Name = "tab2D";
            this.tab2D.Padding = new System.Windows.Forms.Padding(3);
            this.tab2D.Size = new System.Drawing.Size(1192, 593);
            this.tab2D.TabIndex = 0;
            this.tab2D.Text = "Visualizacion 2D";
            this.tab2D.Click += new System.EventHandler(this.tab2D_Click);
            // 
            // chkUmbral
            // 
            this.chkUmbral.AutoSize = true;
            this.chkUmbral.Location = new System.Drawing.Point(365, 399);
            this.chkUmbral.Name = "chkUmbral";
            this.chkUmbral.Size = new System.Drawing.Size(150, 18);
            this.chkUmbral.TabIndex = 26;
            this.chkUmbral.Text = "Segmentar Histograma";
            this.chkUmbral.UseVisualStyleBackColor = true;
            this.chkUmbral.CheckedChanged += new System.EventHandler(this.chkUmbral_CheckedChanged);
            // 
            // chkNorm
            // 
            this.chkNorm.AutoSize = true;
            this.chkNorm.Checked = true;
            this.chkNorm.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkNorm.Location = new System.Drawing.Point(365, 260);
            this.chkNorm.Name = "chkNorm";
            this.chkNorm.Size = new System.Drawing.Size(86, 18);
            this.chkNorm.TabIndex = 25;
            this.chkNorm.Text = "Normalizar";
            this.chkNorm.UseVisualStyleBackColor = true;
            this.chkNorm.CheckedChanged += new System.EventHandler(this.chkNorm_CheckedChanged);
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCerrar.FlatAppearance.BorderColor = System.Drawing.Color.RoyalBlue;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.Location = new System.Drawing.Point(655, 563);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(75, 23);
            this.btnCerrar.TabIndex = 13;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            this.btnCerrar.MouseEnter += new System.EventHandler(this.btnSubir_MouseEnter);
            this.btnCerrar.MouseLeave += new System.EventHandler(this.btnSubir_MouseLeave);
            // 
            // pictHor
            // 
            this.pictHor.Location = new System.Drawing.Point(8, 416);
            this.pictHor.Name = "pictHor";
            this.pictHor.Size = new System.Drawing.Size(350, 84);
            this.pictHor.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictHor.TabIndex = 24;
            this.pictHor.TabStop = false;
            this.pictHor.Paint += new System.Windows.Forms.PaintEventHandler(this.pictHor_Paint);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.numHmax);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.numHmin);
            this.groupBox3.Controls.Add(this.lblHmin);
            this.groupBox3.Controls.Add(this.chart1);
            this.groupBox3.Controls.Add(this.rangeHist);
            this.groupBox3.Location = new System.Drawing.Point(365, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(365, 242);
            this.groupBox3.TabIndex = 23;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "HISTOGRAMA CT";
            // 
            // numHmax
            // 
            this.numHmax.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numHmax.Location = new System.Drawing.Point(191, 213);
            this.numHmax.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.numHmax.Name = "numHmax";
            this.numHmax.Size = new System.Drawing.Size(57, 22);
            this.numHmax.TabIndex = 24;
            this.numHmax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numHmax.Value = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.numHmax.ValueChanged += new System.EventHandler(this.numHmax_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(137, 215);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 14);
            this.label1.TabIndex = 23;
            this.label1.Text = "Maximo:";
            // 
            // numHmin
            // 
            this.numHmin.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numHmin.Location = new System.Drawing.Point(63, 213);
            this.numHmin.Name = "numHmin";
            this.numHmin.Size = new System.Drawing.Size(57, 22);
            this.numHmin.TabIndex = 22;
            this.numHmin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numHmin.ValueChanged += new System.EventHandler(this.numHmin_ValueChanged);
            // 
            // lblHmin
            // 
            this.lblHmin.AutoSize = true;
            this.lblHmin.Location = new System.Drawing.Point(9, 215);
            this.lblHmin.Name = "lblHmin";
            this.lblHmin.Size = new System.Drawing.Size(55, 14);
            this.lblHmin.TabIndex = 10;
            this.lblHmin.Text = "Minimo: ";
            // 
            // chart1
            // 
            chartArea1.AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
            chartArea1.AxisX.LabelStyle.Enabled = false;
            chartArea1.AxisX.MajorGrid.Enabled = false;
            chartArea1.AxisX.MajorTickMark.Enabled = false;
            chartArea1.AxisY.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
            chartArea1.Name = "ChartArea1";
            chartArea1.Position.Auto = false;
            chartArea1.Position.Height = 100F;
            chartArea1.Position.Width = 100F;
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Enabled = false;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(8, 20);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Area;
            series1.Color = System.Drawing.Color.Green;
            series1.Legend = "Legend1";
            series1.Name = "Series2";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Area;
            series2.Color = System.Drawing.Color.CornflowerBlue;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(350, 156);
            this.chart1.TabIndex = 16;
            this.chart1.Text = "chart1";
            // 
            // rangeHist
            // 
            this.rangeHist.DivisionNum = 1;
            this.rangeHist.Font = new System.Drawing.Font("Microsoft Sans Serif", 2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rangeHist.HeightOfBar = 5;
            this.rangeHist.HeightOfMark = 15;
            this.rangeHist.HeightOfTick = 2;
            this.rangeHist.InnerColor = System.Drawing.Color.Green;
            this.rangeHist.Location = new System.Drawing.Point(7, 174);
            this.rangeHist.Name = "rangeHist";
            this.rangeHist.Orientation = Zzzz.ZzzzRangeBar.RangeBarOrientation.horizontal;
            this.rangeHist.RangeMaximum = 10;
            this.rangeHist.RangeMinimum = 10;
            this.rangeHist.ScaleOrientation = Zzzz.ZzzzRangeBar.TopBottomOrientation.bottom;
            this.rangeHist.Size = new System.Drawing.Size(350, 40);
            this.rangeHist.TabIndex = 21;
            this.rangeHist.TotalMaximum = 100;
            this.rangeHist.TotalMinimum = 10;
            this.rangeHist.RangeChanging += new Zzzz.ZzzzRangeBar.RangeChangedEventHandler(this.rangeHist_RangeChanging);
            // 
            // labelCorte
            // 
            this.labelCorte.Location = new System.Drawing.Point(208, 536);
            this.labelCorte.Name = "labelCorte";
            this.labelCorte.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelCorte.Size = new System.Drawing.Size(150, 13);
            this.labelCorte.TabIndex = 21;
            this.labelCorte.Text = "Corte 0 de 0";
            this.labelCorte.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // trackCorte
            // 
            this.trackCorte.Location = new System.Drawing.Point(8, 505);
            this.trackCorte.Name = "trackCorte";
            this.trackCorte.Size = new System.Drawing.Size(350, 45);
            this.trackCorte.TabIndex = 20;
            this.trackCorte.Scroll += new System.EventHandler(this.trackCorte_Scroll);
            // 
            // labelSlide
            // 
            this.labelSlide.Location = new System.Drawing.Point(209, 393);
            this.labelSlide.Name = "labelSlide";
            this.labelSlide.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelSlide.Size = new System.Drawing.Size(150, 13);
            this.labelSlide.TabIndex = 21;
            this.labelSlide.Text = "Slide 0 de 0";
            this.labelSlide.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // trackBar
            // 
            this.trackBar.Location = new System.Drawing.Point(9, 362);
            this.trackBar.Name = "trackBar";
            this.trackBar.Size = new System.Drawing.Size(350, 45);
            this.trackBar.TabIndex = 20;
            this.trackBar.Scroll += new System.EventHandler(this.trackBar_Scroll);
            // 
            // groupUmbral
            // 
            this.groupUmbral.Controls.Add(this.btnLimpiar);
            this.groupUmbral.Controls.Add(this.btnBorrar);
            this.groupUmbral.Controls.Add(this.btnAgregar);
            this.groupUmbral.Controls.Add(this.dataGrid);
            this.groupUmbral.Enabled = false;
            this.groupUmbral.Location = new System.Drawing.Point(365, 412);
            this.groupUmbral.Name = "groupUmbral";
            this.groupUmbral.Size = new System.Drawing.Size(365, 140);
            this.groupUmbral.TabIndex = 18;
            this.groupUmbral.TabStop = false;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.FlatAppearance.BorderColor = System.Drawing.Color.RoyalBlue;
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.Location = new System.Drawing.Point(281, 77);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(75, 23);
            this.btnLimpiar.TabIndex = 13;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            this.btnLimpiar.MouseEnter += new System.EventHandler(this.btnSubir_MouseEnter);
            this.btnLimpiar.MouseLeave += new System.EventHandler(this.btnSubir_MouseLeave);
            // 
            // btnBorrar
            // 
            this.btnBorrar.FlatAppearance.BorderColor = System.Drawing.Color.RoyalBlue;
            this.btnBorrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBorrar.Location = new System.Drawing.Point(281, 48);
            this.btnBorrar.Name = "btnBorrar";
            this.btnBorrar.Size = new System.Drawing.Size(75, 23);
            this.btnBorrar.TabIndex = 13;
            this.btnBorrar.Text = "Borrar";
            this.btnBorrar.UseVisualStyleBackColor = true;
            this.btnBorrar.Click += new System.EventHandler(this.btnBorrar_Click);
            this.btnBorrar.MouseEnter += new System.EventHandler(this.btnSubir_MouseEnter);
            this.btnBorrar.MouseLeave += new System.EventHandler(this.btnSubir_MouseLeave);
            // 
            // btnAgregar
            // 
            this.btnAgregar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnAgregar.FlatAppearance.BorderColor = System.Drawing.Color.RoyalBlue;
            this.btnAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregar.Location = new System.Drawing.Point(281, 19);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(75, 23);
            this.btnAgregar.TabIndex = 13;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = false;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            this.btnAgregar.MouseEnter += new System.EventHandler(this.btnSubir_MouseEnter);
            this.btnAgregar.MouseLeave += new System.EventHandler(this.btnSubir_MouseLeave);
            // 
            // dataGrid
            // 
            this.dataGrid.AllowUserToAddRows = false;
            this.dataGrid.AllowUserToDeleteRows = false;
            this.dataGrid.AllowUserToResizeColumns = false;
            this.dataGrid.AllowUserToResizeRows = false;
            this.dataGrid.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.rangoMin,
            this.rangoMax,
            this.color});
            this.dataGrid.Location = new System.Drawing.Point(9, 19);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.Size = new System.Drawing.Size(266, 117);
            this.dataGrid.TabIndex = 12;
            this.dataGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_CellClick);
            this.dataGrid.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGrid_CellMouseClick);
            this.dataGrid.RowValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_RowValidated);
            // 
            // rangoMin
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.rangoMin.DefaultCellStyle = dataGridViewCellStyle1;
            this.rangoMin.HeaderText = "Minimo";
            this.rangoMin.Name = "rangoMin";
            this.rangoMin.Width = 75;
            // 
            // rangoMax
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.rangoMax.DefaultCellStyle = dataGridViewCellStyle2;
            this.rangoMax.HeaderText = "Maximo";
            this.rangoMax.Name = "rangoMax";
            this.rangoMax.Width = 75;
            // 
            // color
            // 
            this.color.HeaderText = "Color";
            this.color.Name = "color";
            this.color.Width = 55;
            // 
            // groupNorm
            // 
            this.groupNorm.Controls.Add(this.pictGradiente);
            this.groupNorm.Controls.Add(this.numNmax);
            this.groupNorm.Controls.Add(this.label5);
            this.groupNorm.Controls.Add(this.numNmin);
            this.groupNorm.Controls.Add(this.label6);
            this.groupNorm.Controls.Add(this.rangeBar);
            this.groupNorm.Location = new System.Drawing.Point(365, 273);
            this.groupNorm.Name = "groupNorm";
            this.groupNorm.Size = new System.Drawing.Size(365, 115);
            this.groupNorm.TabIndex = 17;
            this.groupNorm.TabStop = false;
            // 
            // pictGradiente
            // 
            this.pictGradiente.Location = new System.Drawing.Point(7, 19);
            this.pictGradiente.Name = "pictGradiente";
            this.pictGradiente.Size = new System.Drawing.Size(350, 32);
            this.pictGradiente.TabIndex = 20;
            this.pictGradiente.TabStop = false;
            // 
            // numNmax
            // 
            this.numNmax.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numNmax.Location = new System.Drawing.Point(191, 87);
            this.numNmax.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.numNmax.Name = "numNmax";
            this.numNmax.Size = new System.Drawing.Size(57, 22);
            this.numNmax.TabIndex = 28;
            this.numNmax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numNmax.Value = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.numNmax.ValueChanged += new System.EventHandler(this.numNmax_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(137, 89);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 14);
            this.label5.TabIndex = 27;
            this.label5.Text = "Maximo:";
            // 
            // numNmin
            // 
            this.numNmin.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numNmin.Location = new System.Drawing.Point(63, 87);
            this.numNmin.Name = "numNmin";
            this.numNmin.Size = new System.Drawing.Size(57, 22);
            this.numNmin.TabIndex = 26;
            this.numNmin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numNmin.ValueChanged += new System.EventHandler(this.numNmin_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 89);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 14);
            this.label6.TabIndex = 25;
            this.label6.Text = "Minimo: ";
            // 
            // rangeBar
            // 
            this.rangeBar.DivisionNum = 1;
            this.rangeBar.Font = new System.Drawing.Font("Microsoft Sans Serif", 2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rangeBar.HeightOfBar = 5;
            this.rangeBar.HeightOfMark = 15;
            this.rangeBar.HeightOfTick = 2;
            this.rangeBar.InnerColor = System.Drawing.Color.Green;
            this.rangeBar.Location = new System.Drawing.Point(7, 46);
            this.rangeBar.Name = "rangeBar";
            this.rangeBar.Orientation = Zzzz.ZzzzRangeBar.RangeBarOrientation.horizontal;
            this.rangeBar.RangeMaximum = 10;
            this.rangeBar.RangeMinimum = 10;
            this.rangeBar.ScaleOrientation = Zzzz.ZzzzRangeBar.TopBottomOrientation.bottom;
            this.rangeBar.Size = new System.Drawing.Size(350, 40);
            this.rangeBar.TabIndex = 1;
            this.rangeBar.TotalMaximum = 100;
            this.rangeBar.TotalMinimum = 10;
            this.rangeBar.RangeChanging += new Zzzz.ZzzzRangeBar.RangeChangedEventHandler(this.rangeBar_RangeChanging);
            // 
            // pictTrans
            // 
            this.pictTrans.Location = new System.Drawing.Point(8, 6);
            this.pictTrans.Name = "pictTrans";
            this.pictTrans.Size = new System.Drawing.Size(350, 350);
            this.pictTrans.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictTrans.TabIndex = 15;
            this.pictTrans.TabStop = false;
            this.pictTrans.Paint += new System.Windows.Forms.PaintEventHandler(this.pictTrans_Paint);
            // 
            // tab3D
            // 
            this.tab3D.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tab3D.Controls.Add(this.groupBox1);
            this.tab3D.Controls.Add(this.renderWindowControl1);
            this.tab3D.Location = new System.Drawing.Point(4, 23);
            this.tab3D.Name = "tab3D";
            this.tab3D.Padding = new System.Windows.Forms.Padding(3);
            this.tab3D.Size = new System.Drawing.Size(1192, 593);
            this.tab3D.TabIndex = 1;
            this.tab3D.Text = "Visualizacion3D";
            this.tab3D.Click += new System.EventHandler(this.tab3D_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.trackBar1);
            this.groupBox1.Controls.Add(this.btnReset);
            this.groupBox1.Controls.Add(this.trackBar3);
            this.groupBox1.Controls.Add(this.trackBar2);
            this.groupBox1.Location = new System.Drawing.Point(8, 505);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(571, 77);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Rotacion";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(390, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 14);
            this.label4.TabIndex = 17;
            this.label4.Text = "Eje Z";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(230, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 14);
            this.label3.TabIndex = 16;
            this.label3.Text = "Eje Y";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(70, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 14);
            this.label2.TabIndex = 15;
            this.label2.Text = "Eje X";
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(6, 20);
            this.trackBar1.Maximum = 360;
            this.trackBar1.Minimum = -360;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(160, 45);
            this.trackBar1.TabIndex = 11;
            // 
            // btnReset
            // 
            this.btnReset.FlatAppearance.BorderColor = System.Drawing.Color.RoyalBlue;
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.Location = new System.Drawing.Point(490, 20);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 10;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            // 
            // trackBar3
            // 
            this.trackBar3.Location = new System.Drawing.Point(326, 20);
            this.trackBar3.Maximum = 360;
            this.trackBar3.Minimum = -360;
            this.trackBar3.Name = "trackBar3";
            this.trackBar3.Size = new System.Drawing.Size(160, 45);
            this.trackBar3.TabIndex = 13;
            // 
            // trackBar2
            // 
            this.trackBar2.Location = new System.Drawing.Point(166, 20);
            this.trackBar2.Maximum = 360;
            this.trackBar2.Minimum = -360;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(160, 45);
            this.trackBar2.TabIndex = 12;
            // 
            // renderWindowControl1
            // 
            this.renderWindowControl1.AddTestActors = false;
            this.renderWindowControl1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.renderWindowControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.renderWindowControl1.Location = new System.Drawing.Point(8, 6);
            this.renderWindowControl1.Name = "renderWindowControl1";
            this.renderWindowControl1.Size = new System.Drawing.Size(571, 489);
            this.renderWindowControl1.TabIndex = 1;
            this.renderWindowControl1.TestText = null;
            // 
            // VisualForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(1200, 650);
            this.ControlBox = false;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.lblTitulo);
            this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "VisualForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Visualizacion de Cores";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.VisualForm_FormClosed);
            this.Load += new System.EventHandler(this.VisualForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tab2D.ResumeLayout(false);
            this.tab2D.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictHor)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numHmax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHmin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackCorte)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).EndInit();
            this.groupUmbral.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.groupNorm.ResumeLayout(false);
            this.groupNorm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictGradiente)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNmax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNmin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictTrans)).EndInit();
            this.tab3D.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tab2D;
        private System.Windows.Forms.TabPage tab3D;
        private System.Windows.Forms.GroupBox groupUmbral;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnBorrar;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.GroupBox groupNorm;
        private Zzzz.ZzzzRangeBar rangeBar;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.PictureBox pictTrans;
        private System.Windows.Forms.TrackBar trackBar;
        private System.Windows.Forms.Label labelSlide;
        private System.Windows.Forms.PictureBox pictGradiente;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NumericUpDown numHmax;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numHmin;
        private System.Windows.Forms.Label lblHmin;
        private System.Windows.Forms.PictureBox pictHor;
        private System.Windows.Forms.Label labelCorte;
        private System.Windows.Forms.TrackBar trackCorte;
        private System.Windows.Forms.CheckBox chkNorm;
        private System.Windows.Forms.CheckBox chkUmbral;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.DataGridViewTextBoxColumn rangoMin;
        private System.Windows.Forms.DataGridViewTextBoxColumn rangoMax;
        private System.Windows.Forms.DataGridViewTextBoxColumn color;
        private Kitware.VTK.RenderWindowControl renderWindowControl1;
        private System.Windows.Forms.TrackBar trackBar3;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private Zzzz.ZzzzRangeBar rangeHist;
        private System.Windows.Forms.NumericUpDown numNmax;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numNmin;
        private System.Windows.Forms.Label label6;
    }
}