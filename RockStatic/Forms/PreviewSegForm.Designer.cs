namespace RockStatic
{
    partial class PreviewSegForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreviewSegForm));
            this.label4 = new System.Windows.Forms.Label();
            this.pictCore = new System.Windows.Forms.PictureBox();
            this.pictP1 = new System.Windows.Forms.PictureBox();
            this.pictP3 = new System.Windows.Forms.PictureBox();
            this.pictP2 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grpPhantoms = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictCore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictP1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictP3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictP2)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.grpPhantoms.SuspendLayout();
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
            this.label4.Size = new System.Drawing.Size(374, 30);
            this.label4.TabIndex = 7;
            this.label4.Text = "PREVISUALIZACION SEGMENTACION ";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.DoubleClick += new System.EventHandler(this.label4_DoubleClick);
            this.label4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label4_MouseDown);
            this.label4.MouseMove += new System.Windows.Forms.MouseEventHandler(this.label4_MouseMove);
            // 
            // pictCore
            // 
            this.pictCore.Location = new System.Drawing.Point(6, 16);
            this.pictCore.Name = "pictCore";
            this.pictCore.Size = new System.Drawing.Size(250, 250);
            this.pictCore.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictCore.TabIndex = 8;
            this.pictCore.TabStop = false;
            this.pictCore.Paint += new System.Windows.Forms.PaintEventHandler(this.pictCore_Paint);
            // 
            // pictP1
            // 
            this.pictP1.Location = new System.Drawing.Point(6, 16);
            this.pictP1.Name = "pictP1";
            this.pictP1.Size = new System.Drawing.Size(80, 80);
            this.pictP1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictP1.TabIndex = 8;
            this.pictP1.TabStop = false;
            this.pictP1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictCore_Paint);
            // 
            // pictP3
            // 
            this.pictP3.Location = new System.Drawing.Point(6, 187);
            this.pictP3.Name = "pictP3";
            this.pictP3.Size = new System.Drawing.Size(80, 80);
            this.pictP3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictP3.TabIndex = 8;
            this.pictP3.TabStop = false;
            this.pictP3.Paint += new System.Windows.Forms.PaintEventHandler(this.pictCore_Paint);
            // 
            // pictP2
            // 
            this.pictP2.Location = new System.Drawing.Point(6, 102);
            this.pictP2.Name = "pictP2";
            this.pictP2.Size = new System.Drawing.Size(80, 80);
            this.pictP2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictP2.TabIndex = 8;
            this.pictP2.TabStop = false;
            this.pictP2.Paint += new System.Windows.Forms.PaintEventHandler(this.pictCore_Paint);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pictCore);
            this.groupBox1.Location = new System.Drawing.Point(4, 34);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(262, 273);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Core";
            // 
            // grpPhantoms
            // 
            this.grpPhantoms.Controls.Add(this.pictP1);
            this.grpPhantoms.Controls.Add(this.pictP2);
            this.grpPhantoms.Controls.Add(this.pictP3);
            this.grpPhantoms.Location = new System.Drawing.Point(273, 34);
            this.grpPhantoms.Name = "grpPhantoms";
            this.grpPhantoms.Size = new System.Drawing.Size(92, 273);
            this.grpPhantoms.TabIndex = 10;
            this.grpPhantoms.TabStop = false;
            this.grpPhantoms.Text = "Phantoms";
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkSeaGreen;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Green;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(273, 313);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(92, 23);
            this.btnClose.TabIndex = 11;
            this.btnClose.Text = "Cerrar";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // PreviewSegForm
            // 
            this.AcceptButton = this.btnClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(374, 345);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.grpPhantoms);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label4);
            this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "PreviewSegForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Previsualizacion Segmentacion";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PreviewSegForm_FormClosed_1);
            this.Load += new System.EventHandler(this.PreviewSegForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.pictCore_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.pictCore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictP1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictP3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictP2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.grpPhantoms.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictCore;
        private System.Windows.Forms.PictureBox pictP1;
        private System.Windows.Forms.PictureBox pictP3;
        private System.Windows.Forms.PictureBox pictP2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnClose;
        public System.Windows.Forms.GroupBox grpPhantoms;
    }
}