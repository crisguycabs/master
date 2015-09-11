namespace RockStatic
{
    partial class ProjectForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectForm));
            this.label1 = new System.Windows.Forms.Label();
            this.pictSegHigh = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pictSegLow = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSegLow = new System.Windows.Forms.Button();
            this.btnSegHigh = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnAreasLow = new System.Windows.Forms.Button();
            this.btnAreasHigh = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.pictAreasLow = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pictAreasHigh = new System.Windows.Forms.PictureBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.menuProject = new System.Windows.Forms.MenuStrip();
            this.proyectoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.segmentarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.elementosHighToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.elementosLowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.areasDeInteresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.elementosHighToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.elementosLowToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cerrarProyectoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblProyecto = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictSegHigh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictSegLow)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictAreasLow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictAreasHigh)).BeginInit();
            this.menuProject.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 14);
            this.label1.TabIndex = 7;
            this.label1.Text = "Elementos HIGH";
            // 
            // pictSegHigh
            // 
            this.pictSegHigh.Image = global::RockStatic.Properties.Resources.redX;
            this.pictSegHigh.Location = new System.Drawing.Point(114, 20);
            this.pictSegHigh.Name = "pictSegHigh";
            this.pictSegHigh.Size = new System.Drawing.Size(23, 23);
            this.pictSegHigh.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictSegHigh.TabIndex = 8;
            this.pictSegHigh.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 14);
            this.label2.TabIndex = 7;
            this.label2.Text = "Elementos LOW";
            // 
            // pictSegLow
            // 
            this.pictSegLow.Image = global::RockStatic.Properties.Resources.redX;
            this.pictSegLow.Location = new System.Drawing.Point(114, 46);
            this.pictSegLow.Name = "pictSegLow";
            this.pictSegLow.Size = new System.Drawing.Size(23, 23);
            this.pictSegLow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictSegLow.TabIndex = 8;
            this.pictSegLow.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSegLow);
            this.groupBox1.Controls.Add(this.btnSegHigh);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.pictSegLow);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.pictSegHigh);
            this.groupBox1.Location = new System.Drawing.Point(10, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(233, 79);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SEGMENTACION";
            // 
            // btnSegLow
            // 
            this.btnSegLow.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSegLow.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnSegLow.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkSeaGreen;
            this.btnSegLow.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Green;
            this.btnSegLow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSegLow.Location = new System.Drawing.Point(145, 46);
            this.btnSegLow.Name = "btnSegLow";
            this.btnSegLow.Size = new System.Drawing.Size(80, 23);
            this.btnSegLow.TabIndex = 9;
            this.btnSegLow.Text = "Segmentar";
            this.btnSegLow.UseVisualStyleBackColor = false;
            this.btnSegLow.Click += new System.EventHandler(this.btnSegLow_Click);
            this.btnSegLow.MouseEnter += new System.EventHandler(this.btnSegHigh_MouseEnter);
            this.btnSegLow.MouseLeave += new System.EventHandler(this.btnSegHigh_MouseLeave);
            // 
            // btnSegHigh
            // 
            this.btnSegHigh.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSegHigh.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnSegHigh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkSeaGreen;
            this.btnSegHigh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Green;
            this.btnSegHigh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSegHigh.Location = new System.Drawing.Point(145, 20);
            this.btnSegHigh.Name = "btnSegHigh";
            this.btnSegHigh.Size = new System.Drawing.Size(80, 23);
            this.btnSegHigh.TabIndex = 9;
            this.btnSegHigh.Text = "Segmentar";
            this.btnSegHigh.UseVisualStyleBackColor = false;
            this.btnSegHigh.Click += new System.EventHandler(this.btnSegHigh_Click);
            this.btnSegHigh.MouseEnter += new System.EventHandler(this.btnSegHigh_MouseEnter);
            this.btnSegHigh.MouseLeave += new System.EventHandler(this.btnSegHigh_MouseLeave);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnAreasLow);
            this.groupBox2.Controls.Add(this.btnAreasHigh);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.pictAreasLow);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.pictAreasHigh);
            this.groupBox2.Location = new System.Drawing.Point(10, 129);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(233, 79);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "AREAS DE INTERES";
            // 
            // btnAreasLow
            // 
            this.btnAreasLow.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnAreasLow.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnAreasLow.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkSeaGreen;
            this.btnAreasLow.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Green;
            this.btnAreasLow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAreasLow.Location = new System.Drawing.Point(145, 46);
            this.btnAreasLow.Name = "btnAreasLow";
            this.btnAreasLow.Size = new System.Drawing.Size(80, 23);
            this.btnAreasLow.TabIndex = 9;
            this.btnAreasLow.Text = "Seleccionar";
            this.btnAreasLow.UseVisualStyleBackColor = false;
            this.btnAreasLow.Click += new System.EventHandler(this.btnAreasLow_Click);
            this.btnAreasLow.MouseEnter += new System.EventHandler(this.btnSegHigh_MouseEnter);
            this.btnAreasLow.MouseLeave += new System.EventHandler(this.btnSegHigh_MouseLeave);
            // 
            // btnAreasHigh
            // 
            this.btnAreasHigh.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnAreasHigh.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnAreasHigh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkSeaGreen;
            this.btnAreasHigh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Green;
            this.btnAreasHigh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAreasHigh.Location = new System.Drawing.Point(145, 20);
            this.btnAreasHigh.Name = "btnAreasHigh";
            this.btnAreasHigh.Size = new System.Drawing.Size(80, 23);
            this.btnAreasHigh.TabIndex = 9;
            this.btnAreasHigh.Text = "Seleccionar";
            this.btnAreasHigh.UseVisualStyleBackColor = false;
            this.btnAreasHigh.Click += new System.EventHandler(this.btnAreasHigh_Click);
            this.btnAreasHigh.MouseEnter += new System.EventHandler(this.btnSegHigh_MouseEnter);
            this.btnAreasHigh.MouseLeave += new System.EventHandler(this.btnSegHigh_MouseLeave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 14);
            this.label3.TabIndex = 7;
            this.label3.Text = "Elementos HIGH";
            // 
            // pictAreasLow
            // 
            this.pictAreasLow.Image = global::RockStatic.Properties.Resources.redX;
            this.pictAreasLow.Location = new System.Drawing.Point(114, 46);
            this.pictAreasLow.Name = "pictAreasLow";
            this.pictAreasLow.Size = new System.Drawing.Size(23, 23);
            this.pictAreasLow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictAreasLow.TabIndex = 8;
            this.pictAreasLow.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 14);
            this.label4.TabIndex = 7;
            this.label4.Text = "Elementos LOW";
            // 
            // pictAreasHigh
            // 
            this.pictAreasHigh.Image = global::RockStatic.Properties.Resources.redX;
            this.pictAreasHigh.Location = new System.Drawing.Point(114, 20);
            this.pictAreasHigh.Name = "pictAreasHigh";
            this.pictAreasHigh.Size = new System.Drawing.Size(23, 23);
            this.pictAreasHigh.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictAreasHigh.TabIndex = 8;
            this.pictAreasHigh.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(252, 43);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(2, 165);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Location = new System.Drawing.Point(261, 41);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(200, 136);
            this.groupBox4.TabIndex = 11;
            this.groupBox4.TabStop = false;
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCerrar.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnCerrar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkSeaGreen;
            this.btnCerrar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Green;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.Location = new System.Drawing.Point(391, 185);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(70, 23);
            this.btnCerrar.TabIndex = 9;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            this.btnCerrar.MouseEnter += new System.EventHandler(this.btnSegHigh_MouseEnter);
            this.btnCerrar.MouseLeave += new System.EventHandler(this.btnSegHigh_MouseLeave);
            // 
            // btnSave
            // 
            this.btnSave.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSave.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkSeaGreen;
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Green;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Location = new System.Drawing.Point(315, 185);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(70, 23);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Guardar";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btnSave.MouseEnter += new System.EventHandler(this.btnSegHigh_MouseEnter);
            this.btnSave.MouseLeave += new System.EventHandler(this.btnSegHigh_MouseLeave);
            // 
            // menuProject
            // 
            this.menuProject.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.proyectoToolStripMenuItem});
            this.menuProject.Location = new System.Drawing.Point(0, 0);
            this.menuProject.MdiWindowListItem = this.proyectoToolStripMenuItem;
            this.menuProject.Name = "menuProject";
            this.menuProject.Size = new System.Drawing.Size(470, 24);
            this.menuProject.TabIndex = 12;
            this.menuProject.Text = "menuStrip1";
            this.menuProject.Visible = false;
            // 
            // proyectoToolStripMenuItem
            // 
            this.proyectoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.segmentarToolStripMenuItem,
            this.areasDeInteresToolStripMenuItem,
            this.toolStripSeparator1,
            this.cerrarProyectoToolStripMenuItem});
            this.proyectoToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.proyectoToolStripMenuItem.MergeIndex = 1;
            this.proyectoToolStripMenuItem.Name = "proyectoToolStripMenuItem";
            this.proyectoToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.proyectoToolStripMenuItem.Text = "&Proyecto";
            // 
            // segmentarToolStripMenuItem
            // 
            this.segmentarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.elementosHighToolStripMenuItem,
            this.elementosLowToolStripMenuItem});
            this.segmentarToolStripMenuItem.Name = "segmentarToolStripMenuItem";
            this.segmentarToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.segmentarToolStripMenuItem.Text = "&Segmentar";
            // 
            // elementosHighToolStripMenuItem
            // 
            this.elementosHighToolStripMenuItem.Name = "elementosHighToolStripMenuItem";
            this.elementosHighToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.elementosHighToolStripMenuItem.Text = "Elementos &High";
            // 
            // elementosLowToolStripMenuItem
            // 
            this.elementosLowToolStripMenuItem.Name = "elementosLowToolStripMenuItem";
            this.elementosLowToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.elementosLowToolStripMenuItem.Text = "Elementos &Low";
            // 
            // areasDeInteresToolStripMenuItem
            // 
            this.areasDeInteresToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.elementosHighToolStripMenuItem1,
            this.elementosLowToolStripMenuItem1});
            this.areasDeInteresToolStripMenuItem.Name = "areasDeInteresToolStripMenuItem";
            this.areasDeInteresToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.areasDeInteresToolStripMenuItem.Text = "&Areas de Interes";
            // 
            // elementosHighToolStripMenuItem1
            // 
            this.elementosHighToolStripMenuItem1.Name = "elementosHighToolStripMenuItem1";
            this.elementosHighToolStripMenuItem1.Size = new System.Drawing.Size(158, 22);
            this.elementosHighToolStripMenuItem1.Text = "Elementos &High";
            // 
            // elementosLowToolStripMenuItem1
            // 
            this.elementosLowToolStripMenuItem1.Name = "elementosLowToolStripMenuItem1";
            this.elementosLowToolStripMenuItem1.Size = new System.Drawing.Size(158, 22);
            this.elementosLowToolStripMenuItem1.Text = "Elementos &Low";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(154, 6);
            // 
            // cerrarProyectoToolStripMenuItem
            // 
            this.cerrarProyectoToolStripMenuItem.Name = "cerrarProyectoToolStripMenuItem";
            this.cerrarProyectoToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.cerrarProyectoToolStripMenuItem.Text = "&Cerrar Proyecto";
            // 
            // lblProyecto
            // 
            this.lblProyecto.BackColor = System.Drawing.Color.Green;
            this.lblProyecto.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblProyecto.Font = new System.Drawing.Font("Eras Bold ITC", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProyecto.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblProyecto.Location = new System.Drawing.Point(0, 0);
            this.lblProyecto.Name = "lblProyecto";
            this.lblProyecto.Size = new System.Drawing.Size(470, 30);
            this.lblProyecto.TabIndex = 13;
            this.lblProyecto.Text = "NOMBRE DE PROYECTO";
            this.lblProyecto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblProyecto.DoubleClick += new System.EventHandler(this.lblProyecto_DoubleClick);
            this.lblProyecto.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblProyecto_MouseDown);
            this.lblProyecto.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblProyecto_MouseMove);
            // 
            // ProjectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(470, 217);
            this.ControlBox = false;
            this.Controls.Add(this.lblProyecto);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuProject);
            this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuProject;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProjectForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NOMBRE DE PROYECTO";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ProjectForm_FormClosed);
            this.Load += new System.EventHandler(this.ProjectForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictSegHigh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictSegLow)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictAreasLow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictAreasHigh)).EndInit();
            this.menuProject.ResumeLayout(false);
            this.menuProject.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictSegHigh;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictSegLow;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSegLow;
        private System.Windows.Forms.Button btnSegHigh;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnAreasLow;
        private System.Windows.Forms.Button btnAreasHigh;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictAreasLow;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictAreasHigh;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.MenuStrip menuProject;
        private System.Windows.Forms.ToolStripMenuItem proyectoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem segmentarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem elementosHighToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem elementosLowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem areasDeInteresToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem elementosHighToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem elementosLowToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem cerrarProyectoToolStripMenuItem;
        private System.Windows.Forms.Label lblProyecto;
    }
}