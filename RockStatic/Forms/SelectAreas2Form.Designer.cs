namespace RockStatic
{
    partial class SelectAreas2Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectAreas2Form));
            this.label4 = new System.Windows.Forms.Label();
            this.pictCore = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numActual = new System.Windows.Forms.NumericUpDown();
            this.lblActual = new System.Windows.Forms.Label();
            this.trackCortes = new System.Windows.Forms.TrackBar();
            this.grpPhantoms = new System.Windows.Forms.GroupBox();
            this.pictPhantom3 = new System.Windows.Forms.PictureBox();
            this.pictPhantom2 = new System.Windows.Forms.PictureBox();
            this.pictPhantom1 = new System.Windows.Forms.PictureBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnSelTrans = new System.Windows.Forms.Button();
            this.P3 = new System.Windows.Forms.Label();
            this.P2 = new System.Windows.Forms.Label();
            this.P1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictCore)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numActual)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackCortes)).BeginInit();
            this.grpPhantoms.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictPhantom3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictPhantom2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictPhantom1)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Green;
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Font = new System.Drawing.Font("Eras Bold ITC", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(886, 32);
            this.label4.TabIndex = 8;
            this.label4.Text = "SELECCION DE AREAS";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblTitulo_MouseDown);
            this.label4.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblTitulo_MouseMove);
            // 
            // pictCore
            // 
            this.pictCore.BackColor = System.Drawing.Color.Black;
            this.pictCore.Location = new System.Drawing.Point(10, 22);
            this.pictCore.Name = "pictCore";
            this.pictCore.Size = new System.Drawing.Size(800, 350);
            this.pictCore.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictCore.TabIndex = 9;
            this.pictCore.TabStop = false;
            this.pictCore.Paint += new System.Windows.Forms.PaintEventHandler(this.pictCore_Paint);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numActual);
            this.groupBox1.Controls.Add(this.lblActual);
            this.groupBox1.Controls.Add(this.trackCortes);
            this.groupBox1.Controls.Add(this.pictCore);
            this.groupBox1.Location = new System.Drawing.Point(12, 39);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(866, 411);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "CORE";
            // 
            // numActual
            // 
            this.numActual.Location = new System.Drawing.Point(810, 380);
            this.numActual.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numActual.Name = "numActual";
            this.numActual.Size = new System.Drawing.Size(41, 22);
            this.numActual.TabIndex = 30;
            this.numActual.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numActual.Value = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numActual.ValueChanged += new System.EventHandler(this.numActual_ValueChanged);
            // 
            // lblActual
            // 
            this.lblActual.AutoSize = true;
            this.lblActual.Location = new System.Drawing.Point(730, 384);
            this.lblActual.Name = "lblActual";
            this.lblActual.Size = new System.Drawing.Size(78, 14);
            this.lblActual.TabIndex = 29;
            this.lblActual.Text = "Slide actual: ";
            // 
            // trackCortes
            // 
            this.trackCortes.Location = new System.Drawing.Point(816, 21);
            this.trackCortes.Name = "trackCortes";
            this.trackCortes.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackCortes.Size = new System.Drawing.Size(45, 351);
            this.trackCortes.TabIndex = 11;
            this.trackCortes.Scroll += new System.EventHandler(this.trackCortes_Scroll);
            // 
            // grpPhantoms
            // 
            this.grpPhantoms.Controls.Add(this.P1);
            this.grpPhantoms.Controls.Add(this.P2);
            this.grpPhantoms.Controls.Add(this.P3);
            this.grpPhantoms.Controls.Add(this.pictPhantom3);
            this.grpPhantoms.Controls.Add(this.pictPhantom2);
            this.grpPhantoms.Controls.Add(this.pictPhantom1);
            this.grpPhantoms.Location = new System.Drawing.Point(13, 445);
            this.grpPhantoms.Name = "grpPhantoms";
            this.grpPhantoms.Size = new System.Drawing.Size(866, 217);
            this.grpPhantoms.TabIndex = 11;
            this.grpPhantoms.TabStop = false;
            this.grpPhantoms.Text = "PHANTOMS";
            // 
            // pictPhantom3
            // 
            this.pictPhantom3.BackColor = System.Drawing.Color.Black;
            this.pictPhantom3.Location = new System.Drawing.Point(28, 164);
            this.pictPhantom3.Name = "pictPhantom3";
            this.pictPhantom3.Size = new System.Drawing.Size(800, 45);
            this.pictPhantom3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictPhantom3.TabIndex = 9;
            this.pictPhantom3.TabStop = false;
            // 
            // pictPhantom2
            // 
            this.pictPhantom2.BackColor = System.Drawing.Color.Black;
            this.pictPhantom2.Location = new System.Drawing.Point(28, 99);
            this.pictPhantom2.Name = "pictPhantom2";
            this.pictPhantom2.Size = new System.Drawing.Size(800, 45);
            this.pictPhantom2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictPhantom2.TabIndex = 9;
            this.pictPhantom2.TabStop = false;
            // 
            // pictPhantom1
            // 
            this.pictPhantom1.BackColor = System.Drawing.Color.Black;
            this.pictPhantom1.Location = new System.Drawing.Point(28, 34);
            this.pictPhantom1.Name = "pictPhantom1";
            this.pictPhantom1.Size = new System.Drawing.Size(800, 45);
            this.pictPhantom1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictPhantom1.TabIndex = 9;
            this.pictPhantom1.TabStop = false;
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCancelar.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnCancelar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkSeaGreen;
            this.btnCancelar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Green;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Location = new System.Drawing.Point(804, 663);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(70, 23);
            this.btnCancelar.TabIndex = 12;
            this.btnCancelar.Text = "Cerrar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnSelTrans
            // 
            this.btnSelTrans.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSelTrans.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnSelTrans.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkSeaGreen;
            this.btnSelTrans.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Green;
            this.btnSelTrans.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelTrans.Location = new System.Drawing.Point(669, 663);
            this.btnSelTrans.Name = "btnSelTrans";
            this.btnSelTrans.Size = new System.Drawing.Size(129, 23);
            this.btnSelTrans.TabIndex = 13;
            this.btnSelTrans.Text = "Seg Transversal";
            this.btnSelTrans.UseVisualStyleBackColor = false;
            this.btnSelTrans.Click += new System.EventHandler(this.btnSelTrans_Click);
            // 
            // P3
            // 
            this.P3.AutoSize = true;
            this.P3.Location = new System.Drawing.Point(34, 147);
            this.P3.Name = "P3";
            this.P3.Size = new System.Drawing.Size(69, 14);
            this.P3.TabIndex = 10;
            this.P3.Text = "PHANTOM 3";
            // 
            // P2
            // 
            this.P2.AutoSize = true;
            this.P2.Location = new System.Drawing.Point(34, 82);
            this.P2.Name = "P2";
            this.P2.Size = new System.Drawing.Size(69, 14);
            this.P2.TabIndex = 11;
            this.P2.Text = "PHANTOM 2";
            // 
            // P1
            // 
            this.P1.AutoSize = true;
            this.P1.Location = new System.Drawing.Point(34, 17);
            this.P1.Name = "P1";
            this.P1.Size = new System.Drawing.Size(69, 14);
            this.P1.TabIndex = 12;
            this.P1.Text = "PHANTOM 1";
            // 
            // SelectAreas2Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(886, 689);
            this.Controls.Add(this.btnSelTrans);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.grpPhantoms);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label4);
            this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "SelectAreas2Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SelectAreas2Form";
            this.Load += new System.EventHandler(this.SelectAreas2Form_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.SelectAreas2Form_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.pictCore)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numActual)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackCortes)).EndInit();
            this.grpPhantoms.ResumeLayout(false);
            this.grpPhantoms.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictPhantom3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictPhantom2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictPhantom1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox grpPhantoms;
        private System.Windows.Forms.PictureBox pictPhantom3;
        private System.Windows.Forms.PictureBox pictPhantom2;
        private System.Windows.Forms.PictureBox pictPhantom1;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.TrackBar trackCortes;
        private System.Windows.Forms.NumericUpDown numActual;
        private System.Windows.Forms.Label lblActual;
        private System.Windows.Forms.Button btnSelTrans;
        public System.Windows.Forms.PictureBox pictCore;
        private System.Windows.Forms.Label P1;
        private System.Windows.Forms.Label P2;
        private System.Windows.Forms.Label P3;
    }
}