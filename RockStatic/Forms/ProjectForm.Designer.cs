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
            this.lblSeg = new System.Windows.Forms.Label();
            this.pictSegHigh = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSegHigh = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pictAreasHigh = new System.Windows.Forms.PictureBox();
            this.btnAreasHigh = new System.Windows.Forms.Button();
            this.lblArea = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
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
            this.grpCurvas = new System.Windows.Forms.GroupBox();
            this.btnCurvas = new System.Windows.Forms.Button();
            this.lblCurvas = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictSegHigh)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictAreasHigh)).BeginInit();
            this.menuProject.SuspendLayout();
            this.grpCurvas.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSeg
            // 
            this.lblSeg.Location = new System.Drawing.Point(6, 20);
            this.lblSeg.Name = "lblSeg";
            this.lblSeg.Size = new System.Drawing.Size(223, 33);
            this.lblSeg.TabIndex = 7;
            this.lblSeg.Text = "Aun no ha realizado la segmentacion de los slides";
            // 
            // pictSegHigh
            // 
            this.pictSegHigh.Image = global::RockStatic.Properties.Resources.redX;
            this.pictSegHigh.Location = new System.Drawing.Point(114, 47);
            this.pictSegHigh.Name = "pictSegHigh";
            this.pictSegHigh.Size = new System.Drawing.Size(23, 23);
            this.pictSegHigh.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictSegHigh.TabIndex = 8;
            this.pictSegHigh.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pictSegHigh);
            this.groupBox1.Controls.Add(this.btnSegHigh);
            this.groupBox1.Controls.Add(this.lblSeg);
            this.groupBox1.Location = new System.Drawing.Point(10, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(233, 79);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SEGMENTACION";
            // 
            // btnSegHigh
            // 
            this.btnSegHigh.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSegHigh.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnSegHigh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkSeaGreen;
            this.btnSegHigh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Green;
            this.btnSegHigh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSegHigh.Location = new System.Drawing.Point(145, 47);
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
            this.groupBox2.Controls.Add(this.pictAreasHigh);
            this.groupBox2.Controls.Add(this.btnAreasHigh);
            this.groupBox2.Controls.Add(this.lblArea);
            this.groupBox2.Location = new System.Drawing.Point(10, 129);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(233, 79);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "AREAS DE INTERES";
            // 
            // pictAreasHigh
            // 
            this.pictAreasHigh.Image = global::RockStatic.Properties.Resources.redX;
            this.pictAreasHigh.Location = new System.Drawing.Point(114, 47);
            this.pictAreasHigh.Name = "pictAreasHigh";
            this.pictAreasHigh.Size = new System.Drawing.Size(23, 23);
            this.pictAreasHigh.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictAreasHigh.TabIndex = 8;
            this.pictAreasHigh.TabStop = false;
            // 
            // btnAreasHigh
            // 
            this.btnAreasHigh.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnAreasHigh.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnAreasHigh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkSeaGreen;
            this.btnAreasHigh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Green;
            this.btnAreasHigh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAreasHigh.Location = new System.Drawing.Point(145, 47);
            this.btnAreasHigh.Name = "btnAreasHigh";
            this.btnAreasHigh.Size = new System.Drawing.Size(80, 23);
            this.btnAreasHigh.TabIndex = 9;
            this.btnAreasHigh.Text = "Seleccionar";
            this.btnAreasHigh.UseVisualStyleBackColor = false;
            this.btnAreasHigh.Click += new System.EventHandler(this.btnAreasHigh_Click);
            this.btnAreasHigh.MouseEnter += new System.EventHandler(this.btnSegHigh_MouseEnter);
            this.btnAreasHigh.MouseLeave += new System.EventHandler(this.btnSegHigh_MouseLeave);
            // 
            // lblArea
            // 
            this.lblArea.Location = new System.Drawing.Point(6, 20);
            this.lblArea.Name = "lblArea";
            this.lblArea.Size = new System.Drawing.Size(219, 38);
            this.lblArea.TabIndex = 7;
            this.lblArea.Text = "Aun no ha seleccionado las areas de interes";
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(252, 43);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(2, 165);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCerrar.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnCerrar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkSeaGreen;
            this.btnCerrar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Green;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.Location = new System.Drawing.Point(424, 185);
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
            this.btnSave.Location = new System.Drawing.Point(348, 185);
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
            this.lblProyecto.Size = new System.Drawing.Size(505, 30);
            this.lblProyecto.TabIndex = 13;
            this.lblProyecto.Text = "NOMBRE DE PROYECTO";
            this.lblProyecto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblProyecto.DoubleClick += new System.EventHandler(this.lblProyecto_DoubleClick);
            this.lblProyecto.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblProyecto_MouseDown);
            this.lblProyecto.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblProyecto_MouseMove);
            // 
            // grpCurvas
            // 
            this.grpCurvas.Controls.Add(this.btnCurvas);
            this.grpCurvas.Controls.Add(this.lblCurvas);
            this.grpCurvas.Location = new System.Drawing.Point(261, 41);
            this.grpCurvas.Name = "grpCurvas";
            this.grpCurvas.Size = new System.Drawing.Size(233, 79);
            this.grpCurvas.TabIndex = 10;
            this.grpCurvas.TabStop = false;
            this.grpCurvas.Text = "PROPIEDADES PETROFISICAS";
            // 
            // btnCurvas
            // 
            this.btnCurvas.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCurvas.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnCurvas.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkSeaGreen;
            this.btnCurvas.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Green;
            this.btnCurvas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCurvas.Location = new System.Drawing.Point(145, 47);
            this.btnCurvas.Name = "btnCurvas";
            this.btnCurvas.Size = new System.Drawing.Size(80, 23);
            this.btnCurvas.TabIndex = 9;
            this.btnCurvas.Text = "Estimar";
            this.btnCurvas.UseVisualStyleBackColor = false;
            this.btnCurvas.Click += new System.EventHandler(this.btnCurvas_Click);
            this.btnCurvas.MouseEnter += new System.EventHandler(this.btnSegHigh_MouseEnter);
            this.btnCurvas.MouseLeave += new System.EventHandler(this.btnSegHigh_MouseLeave);
            // 
            // lblCurvas
            // 
            this.lblCurvas.Location = new System.Drawing.Point(6, 20);
            this.lblCurvas.Name = "lblCurvas";
            this.lblCurvas.Size = new System.Drawing.Size(223, 33);
            this.lblCurvas.TabIndex = 7;
            this.lblCurvas.Text = "Aun no ha realizado la segmentacion de los slides";
            // 
            // ProjectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(505, 217);
            this.ControlBox = false;
            this.Controls.Add(this.grpCurvas);
            this.Controls.Add(this.lblProyecto);
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
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ProjectForm_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.pictSegHigh)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictAreasHigh)).EndInit();
            this.menuProject.ResumeLayout(false);
            this.menuProject.PerformLayout();
            this.grpCurvas.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSeg;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSegHigh;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnAreasHigh;
        private System.Windows.Forms.Label lblArea;
        private System.Windows.Forms.GroupBox groupBox3;
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
        public System.Windows.Forms.PictureBox pictSegHigh;
        public System.Windows.Forms.PictureBox pictAreasHigh;
        private System.Windows.Forms.GroupBox grpCurvas;
        private System.Windows.Forms.Button btnCurvas;
        private System.Windows.Forms.Label lblCurvas;
    }
}