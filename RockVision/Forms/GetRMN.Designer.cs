namespace RockVision
{
    partial class GetRMN
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GetRMN));
            this.txtFid = new System.Windows.Forms.TextBox();
            this.txtFidstd = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numVstd = new System.Windows.Forms.NumericUpDown();
            this.numVroca = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnFID = new System.Windows.Forms.Button();
            this.btnFIDstd = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numVstd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVroca)).BeginInit();
            this.SuspendLayout();
            // 
            // txtFid
            // 
            this.txtFid.Location = new System.Drawing.Point(156, 42);
            this.txtFid.Name = "txtFid";
            this.txtFid.Size = new System.Drawing.Size(217, 22);
            this.txtFid.TabIndex = 0;
            // 
            // txtFidstd
            // 
            this.txtFidstd.Location = new System.Drawing.Point(156, 72);
            this.txtFidstd.Name = "txtFidstd";
            this.txtFidstd.Size = new System.Drawing.Size(217, 22);
            this.txtFidstd.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "Archivo FID:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "Archivo FID estándar";
            // 
            // numVstd
            // 
            this.numVstd.DecimalPlaces = 2;
            this.numVstd.Location = new System.Drawing.Point(156, 101);
            this.numVstd.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numVstd.Name = "numVstd";
            this.numVstd.Size = new System.Drawing.Size(120, 22);
            this.numVstd.TabIndex = 3;
            this.numVstd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // numVroca
            // 
            this.numVroca.DecimalPlaces = 2;
            this.numVroca.Location = new System.Drawing.Point(156, 129);
            this.numVroca.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numVroca.Name = "numVroca";
            this.numVroca.Size = new System.Drawing.Size(120, 22);
            this.numVroca.TabIndex = 4;
            this.numVroca.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(135, 14);
            this.label3.TabIndex = 1;
            this.label3.Text = "Volumen estándar (ml):";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 131);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(140, 14);
            this.label4.TabIndex = 1;
            this.label4.Text = "Volumen de la roca (ml):";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.RoyalBlue;
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Font = new System.Drawing.Font("Eras Bold ITC", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(479, 30);
            this.label5.TabIndex = 19;
            this.label5.Text = "INFORMACION RMN";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label5.DoubleClick += new System.EventHandler(this.label5_DoubleClick);
            this.label5.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblTitulo_MouseDown);
            this.label5.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblTitulo_MouseMove);
            // 
            // btnFID
            // 
            this.btnFID.FlatAppearance.BorderColor = System.Drawing.Color.RoyalBlue;
            this.btnFID.FlatAppearance.MouseDownBackColor = System.Drawing.Color.CornflowerBlue;
            this.btnFID.FlatAppearance.MouseOverBackColor = System.Drawing.Color.RoyalBlue;
            this.btnFID.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFID.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFID.Location = new System.Drawing.Point(390, 42);
            this.btnFID.Name = "btnFID";
            this.btnFID.Size = new System.Drawing.Size(80, 25);
            this.btnFID.TabIndex = 1;
            this.btnFID.Text = "Seleccionar";
            this.btnFID.UseVisualStyleBackColor = true;
            this.btnFID.Click += new System.EventHandler(this.btnFID_Click);
            // 
            // btnFIDstd
            // 
            this.btnFIDstd.FlatAppearance.BorderColor = System.Drawing.Color.RoyalBlue;
            this.btnFIDstd.FlatAppearance.MouseDownBackColor = System.Drawing.Color.CornflowerBlue;
            this.btnFIDstd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.RoyalBlue;
            this.btnFIDstd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFIDstd.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFIDstd.Location = new System.Drawing.Point(390, 70);
            this.btnFIDstd.Name = "btnFIDstd";
            this.btnFIDstd.Size = new System.Drawing.Size(80, 25);
            this.btnFIDstd.TabIndex = 2;
            this.btnFIDstd.Text = "Seleccionar";
            this.btnFIDstd.UseVisualStyleBackColor = true;
            this.btnFIDstd.Click += new System.EventHandler(this.btnFIDstd_Click);
            // 
            // btnOk
            // 
            this.btnOk.FlatAppearance.BorderColor = System.Drawing.Color.RoyalBlue;
            this.btnOk.FlatAppearance.MouseDownBackColor = System.Drawing.Color.CornflowerBlue;
            this.btnOk.FlatAppearance.MouseOverBackColor = System.Drawing.Color.RoyalBlue;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.Location = new System.Drawing.Point(304, 160);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(80, 25);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "Calcular";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.RoyalBlue;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.CornflowerBlue;
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.RoyalBlue;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(390, 159);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 25);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // GetRMN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(479, 194);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnFIDstd);
            this.Controls.Add(this.btnFID);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.numVroca);
            this.Controls.Add(this.numVstd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtFidstd);
            this.Controls.Add(this.txtFid);
            this.Font = new System.Drawing.Font("Calibri", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GetRMN";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GetRMN";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GetRMN_FormClosed);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.GetRMN_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.numVstd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVroca)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFid;
        private System.Windows.Forms.TextBox txtFidstd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numVstd;
        private System.Windows.Forms.NumericUpDown numVroca;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnFID;
        private System.Windows.Forms.Button btnFIDstd;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
    }
}