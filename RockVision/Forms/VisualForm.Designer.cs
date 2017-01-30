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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VisualForm));
            this.lblTitulo = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tab2D = new System.Windows.Forms.TabPage();
            this.tab3D = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tab2D;
        private System.Windows.Forms.TabPage tab3D;
    }
}