namespace RockVision
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VisualForm));
            this.lblTitulo = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tab2D = new System.Windows.Forms.TabPage();
            this.tab3D = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnBorrar = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.rangoMin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rangoMax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.color = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chkHabilitar = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rangeBar = new Zzzz.ZzzzRangeBar();
            this.labelMin = new System.Windows.Forms.Label();
            this.labelMax = new System.Windows.Forms.Label();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.picTrans = new System.Windows.Forms.PictureBox();
            this.trackBar = new System.Windows.Forms.TrackBar();
            this.labelSlide = new System.Windows.Forms.Label();
            this.pictGradiente = new System.Windows.Forms.PictureBox();
            this.tabControl1.SuspendLayout();
            this.tab2D.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTrans)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictGradiente)).BeginInit();
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
            this.tab2D.Controls.Add(this.labelSlide);
            this.tab2D.Controls.Add(this.trackBar);
            this.tab2D.Controls.Add(this.groupBox2);
            this.tab2D.Controls.Add(this.groupBox1);
            this.tab2D.Controls.Add(this.chart1);
            this.tab2D.Controls.Add(this.picTrans);
            this.tab2D.Location = new System.Drawing.Point(4, 23);
            this.tab2D.Name = "tab2D";
            this.tab2D.Padding = new System.Windows.Forms.Padding(3);
            this.tab2D.Size = new System.Drawing.Size(1192, 593);
            this.tab2D.TabIndex = 0;
            this.tab2D.Text = "Visualizacion 2D";
            // 
            // tab3D
            // 
            this.tab3D.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tab3D.Location = new System.Drawing.Point(4, 23);
            this.tab3D.Name = "tab3D";
            this.tab3D.Padding = new System.Windows.Forms.Padding(3);
            this.tab3D.Size = new System.Drawing.Size(1192, 593);
            this.tab3D.TabIndex = 1;
            this.tab3D.Text = "Visualizacion3D";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnLimpiar);
            this.groupBox2.Controls.Add(this.btnBorrar);
            this.groupBox2.Controls.Add(this.btnAgregar);
            this.groupBox2.Controls.Add(this.dataGrid);
            this.groupBox2.Controls.Add(this.chkHabilitar);
            this.groupBox2.Location = new System.Drawing.Point(365, 333);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(365, 140);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "UMBRALIZAR";
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(271, 77);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(75, 23);
            this.btnLimpiar.TabIndex = 13;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            // 
            // btnBorrar
            // 
            this.btnBorrar.Location = new System.Drawing.Point(271, 48);
            this.btnBorrar.Name = "btnBorrar";
            this.btnBorrar.Size = new System.Drawing.Size(75, 23);
            this.btnBorrar.TabIndex = 13;
            this.btnBorrar.Text = "Borrar";
            this.btnBorrar.UseVisualStyleBackColor = true;
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(271, 19);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(75, 23);
            this.btnAgregar.TabIndex = 13;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
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
            this.dataGrid.Size = new System.Drawing.Size(236, 117);
            this.dataGrid.TabIndex = 12;
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
            this.color.Width = 45;
            // 
            // chkHabilitar
            // 
            this.chkHabilitar.AutoSize = true;
            this.chkHabilitar.Location = new System.Drawing.Point(276, 109);
            this.chkHabilitar.Name = "chkHabilitar";
            this.chkHabilitar.Size = new System.Drawing.Size(75, 18);
            this.chkHabilitar.TabIndex = 11;
            this.chkHabilitar.Text = "Habilitar";
            this.chkHabilitar.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pictGradiente);
            this.groupBox1.Controls.Add(this.rangeBar);
            this.groupBox1.Controls.Add(this.labelMin);
            this.groupBox1.Controls.Add(this.labelMax);
            this.groupBox1.Location = new System.Drawing.Point(365, 210);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(365, 118);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "NORMALIZAR";
            // 
            // rangeBar
            // 
            this.rangeBar.DivisionNum = 1;
            this.rangeBar.Font = new System.Drawing.Font("Microsoft Sans Serif", 2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rangeBar.HeightOfBar = 5;
            this.rangeBar.HeightOfMark = 15;
            this.rangeBar.HeightOfTick = 2;
            this.rangeBar.InnerColor = System.Drawing.Color.Green;
            this.rangeBar.Location = new System.Drawing.Point(9, 53);
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
            // labelMin
            // 
            this.labelMin.AutoSize = true;
            this.labelMin.Location = new System.Drawing.Point(6, 96);
            this.labelMin.Name = "labelMin";
            this.labelMin.Size = new System.Drawing.Size(42, 14);
            this.labelMin.TabIndex = 10;
            this.labelMin.Text = "label1";
            // 
            // labelMax
            // 
            this.labelMax.AutoSize = true;
            this.labelMax.Location = new System.Drawing.Point(118, 96);
            this.labelMax.Name = "labelMax";
            this.labelMax.Size = new System.Drawing.Size(42, 14);
            this.labelMax.TabIndex = 9;
            this.labelMax.Text = "label1";
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
            this.chart1.Location = new System.Drawing.Point(372, 6);
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
            // picTrans
            // 
            this.picTrans.Location = new System.Drawing.Point(8, 6);
            this.picTrans.Name = "picTrans";
            this.picTrans.Size = new System.Drawing.Size(350, 350);
            this.picTrans.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picTrans.TabIndex = 15;
            this.picTrans.TabStop = false;
            // 
            // trackBar
            // 
            this.trackBar.Location = new System.Drawing.Point(9, 362);
            this.trackBar.Name = "trackBar";
            this.trackBar.Size = new System.Drawing.Size(350, 45);
            this.trackBar.TabIndex = 20;
            this.trackBar.Scroll += new System.EventHandler(this.trackBar_Scroll);
            // 
            // labelSlide
            // 
            this.labelSlide.Location = new System.Drawing.Point(209, 408);
            this.labelSlide.Name = "labelSlide";
            this.labelSlide.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelSlide.Size = new System.Drawing.Size(150, 13);
            this.labelSlide.TabIndex = 21;
            this.labelSlide.Text = "Slide 0 de 0";
            this.labelSlide.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pictGradiente
            // 
            this.pictGradiente.Location = new System.Drawing.Point(7, 19);
            this.pictGradiente.Name = "pictGradiente";
            this.pictGradiente.Size = new System.Drawing.Size(350, 32);
            this.pictGradiente.TabIndex = 20;
            this.pictGradiente.TabStop = false;
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
            this.MaximumSize = new System.Drawing.Size(1200, 650);
            this.MinimumSize = new System.Drawing.Size(1200, 650);
            this.Name = "VisualForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VISUALIZACION";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.VisualForm_FormClosed);
            this.Load += new System.EventHandler(this.VisualForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tab2D.ResumeLayout(false);
            this.tab2D.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTrans)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictGradiente)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tab2D;
        private System.Windows.Forms.TabPage tab3D;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnBorrar;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn rangoMin;
        private System.Windows.Forms.DataGridViewTextBoxColumn rangoMax;
        private System.Windows.Forms.DataGridViewTextBoxColumn color;
        private System.Windows.Forms.CheckBox chkHabilitar;
        private System.Windows.Forms.GroupBox groupBox1;
        private Zzzz.ZzzzRangeBar rangeBar;
        private System.Windows.Forms.Label labelMin;
        private System.Windows.Forms.Label labelMax;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.PictureBox picTrans;
        private System.Windows.Forms.TrackBar trackBar;
        private System.Windows.Forms.Label labelSlide;
        private System.Windows.Forms.PictureBox pictGradiente;
    }
}