namespace RockStatic
{
    partial class CurvasForm
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
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CurvasForm));
            this.lblTitulo = new System.Windows.Forms.Label();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.btnExportar = new System.Windows.Forms.Button();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.BackColor = System.Drawing.Color.Green;
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitulo.Font = new System.Drawing.Font("Eras Bold ITC", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblTitulo.Location = new System.Drawing.Point(0, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(506, 32);
            this.lblTitulo.TabIndex = 8;
            this.lblTitulo.Text = "BIENVENIDO!";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTitulo.DoubleClick += new System.EventHandler(this.lblTitulo_DoubleClick);
            this.lblTitulo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblTitulo_MouseDown);
            this.lblTitulo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblTitulo_MouseMove);
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCerrar.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnCerrar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkSeaGreen;
            this.btnCerrar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Green;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.Font = new System.Drawing.Font("Calibri", 9F);
            this.btnCerrar.Location = new System.Drawing.Point(430, 669);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(70, 25);
            this.btnCerrar.TabIndex = 11;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // btnExportar
            // 
            this.btnExportar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnExportar.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnExportar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkSeaGreen;
            this.btnExportar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Green;
            this.btnExportar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportar.Font = new System.Drawing.Font("Calibri", 9F);
            this.btnExportar.Location = new System.Drawing.Point(354, 669);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(70, 25);
            this.btnExportar.TabIndex = 11;
            this.btnExportar.Text = "Exportar";
            this.btnExportar.UseVisualStyleBackColor = false;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // chart1
            // 
            chartArea1.AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.Silver;
            chartArea1.AxisX.MajorTickMark.Enabled = false;
            chartArea1.AxisX2.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
            chartArea1.AxisX2.MajorTickMark.Enabled = false;
            chartArea1.AxisX2.ScaleView.Zoomable = false;
            chartArea1.AxisY.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
            chartArea1.AxisY.IsReversed = true;
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.Silver;
            chartArea1.AxisY.MajorTickMark.Enabled = false;
            chartArea1.AxisY.ScaleView.Zoomable = false;
            chartArea1.AxisY.ScrollBar.Enabled = false;
            chartArea1.AxisY.Title = "PROFUNDIDAD";
            chartArea1.AxisY.TitleFont = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisY2.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
            chartArea1.AxisY2.ScaleView.Zoomable = false;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Location = new System.Drawing.Point(0, 19);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series1.LabelBorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            series1.MarkerColor = System.Drawing.Color.DodgerBlue;
            series1.MarkerSize = 3;
            series1.Name = "Deff";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(250, 674);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            title1.Font = new System.Drawing.Font("Eras Bold ITC", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title1.ForeColor = System.Drawing.Color.Green;
            title1.Name = "Deff";
            title1.Text = "Deff";
            this.chart1.Titles.Add(title1);
            this.chart1.Click += new System.EventHandler(this.chart1_Click);
            // 
            // chart2
            // 
            chartArea2.AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
            chartArea2.AxisX.MajorGrid.LineColor = System.Drawing.Color.Silver;
            chartArea2.AxisX.MajorTickMark.Enabled = false;
            chartArea2.AxisX.ScaleView.Zoomable = false;
            chartArea2.AxisX.ScrollBar.Enabled = false;
            chartArea2.AxisX2.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
            chartArea2.AxisX2.MajorTickMark.Enabled = false;
            chartArea2.AxisX2.ScaleView.Zoomable = false;
            chartArea2.AxisX2.ScrollBar.Enabled = false;
            chartArea2.AxisY.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
            chartArea2.AxisY.IsReversed = true;
            chartArea2.AxisY.MajorGrid.LineColor = System.Drawing.Color.Silver;
            chartArea2.AxisY.MajorTickMark.Enabled = false;
            chartArea2.AxisY.ScaleView.Zoomable = false;
            chartArea2.AxisY.ScrollBar.Enabled = false;
            chartArea2.AxisY.Title = "PROFUNDIDAD";
            chartArea2.AxisY.TitleFont = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea2.AxisY2.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
            chartArea2.AxisY2.ScaleView.Zoomable = false;
            chartArea2.AxisY2.ScrollBar.Enabled = false;
            chartArea2.Name = "ChartArea1";
            this.chart2.ChartAreas.Add(chartArea2);
            this.chart2.Location = new System.Drawing.Point(250, 19);
            this.chart2.Name = "chart2";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series2.MarkerColor = System.Drawing.Color.Crimson;
            series2.MarkerSize = 3;
            series2.Name = "Zeff";
            this.chart2.Series.Add(series2);
            this.chart2.Size = new System.Drawing.Size(250, 674);
            this.chart2.TabIndex = 12;
            this.chart2.Text = "chart2";
            title2.Font = new System.Drawing.Font("Eras Bold ITC", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title2.ForeColor = System.Drawing.Color.Green;
            title2.Name = "Deff";
            title2.Text = "Zeff";
            this.chart2.Titles.Add(title2);
            this.chart2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.chart2_MouseClick);
            // 
            // CurvasForm
            // 
            this.AcceptButton = this.btnExportar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(506, 700);
            this.ControlBox = false;
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.btnExportar);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.chart2);
            this.Controls.Add(this.chart1);
            this.Font = new System.Drawing.Font("Calibri", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CurvasForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CurvasForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CurvasForm_FormClosed);
            this.Load += new System.EventHandler(this.CurvasForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Button btnExportar;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
    }
}