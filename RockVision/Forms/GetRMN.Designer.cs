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
            this.txtFid = new System.Windows.Forms.TextBox();
            this.txtFidstd = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnFID = new System.Windows.Forms.Button();
            this.btnFIDstd = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.numVstd = new System.Windows.Forms.NumericUpDown();
            this.numVroca = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numVstd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVroca)).BeginInit();
            this.SuspendLayout();
            // 
            // txtFid
            // 
            this.txtFid.Location = new System.Drawing.Point(120, 14);
            this.txtFid.Name = "txtFid";
            this.txtFid.Size = new System.Drawing.Size(217, 20);
            this.txtFid.TabIndex = 0;
            // 
            // txtFidstd
            // 
            this.txtFidstd.Location = new System.Drawing.Point(120, 42);
            this.txtFidstd.Name = "txtFidstd";
            this.txtFidstd.Size = new System.Drawing.Size(217, 20);
            this.txtFidstd.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Archivo FID:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Archivo FID estándar";
            // 
            // btnFID
            // 
            this.btnFID.Location = new System.Drawing.Point(343, 14);
            this.btnFID.Name = "btnFID";
            this.btnFID.Size = new System.Drawing.Size(75, 23);
            this.btnFID.TabIndex = 3;
            this.btnFID.Text = "Seleccionar";
            this.btnFID.UseVisualStyleBackColor = true;
            this.btnFID.Click += new System.EventHandler(this.btnFID_Click);
            // 
            // btnFIDstd
            // 
            this.btnFIDstd.Location = new System.Drawing.Point(343, 40);
            this.btnFIDstd.Name = "btnFIDstd";
            this.btnFIDstd.Size = new System.Drawing.Size(75, 23);
            this.btnFIDstd.TabIndex = 3;
            this.btnFIDstd.Text = "Seleccionar";
            this.btnFIDstd.UseVisualStyleBackColor = true;
            this.btnFIDstd.Click += new System.EventHandler(this.btnFIDstd_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(343, 124);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // numVstd
            // 
            this.numVstd.DecimalPlaces = 2;
            this.numVstd.Location = new System.Drawing.Point(120, 69);
            this.numVstd.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numVstd.Name = "numVstd";
            this.numVstd.Size = new System.Drawing.Size(120, 20);
            this.numVstd.TabIndex = 4;
            this.numVstd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // numVroca
            // 
            this.numVroca.DecimalPlaces = 2;
            this.numVroca.Location = new System.Drawing.Point(120, 95);
            this.numVroca.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numVroca.Name = "numVroca";
            this.numVroca.Size = new System.Drawing.Size(120, 20);
            this.numVroca.TabIndex = 4;
            this.numVroca.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Volumen estándar (??):";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(122, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Volumen de la roca (??):";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(262, 124);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "Calcular";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // GetRMN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 157);
            this.Controls.Add(this.numVroca);
            this.Controls.Add(this.numVstd);
            this.Controls.Add(this.btnFIDstd);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnFID);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtFidstd);
            this.Controls.Add(this.txtFid);
            this.Name = "GetRMN";
            this.Text = "GetRMN";
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
        private System.Windows.Forms.Button btnFID;
        private System.Windows.Forms.Button btnFIDstd;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.NumericUpDown numVstd;
        private System.Windows.Forms.NumericUpDown numVroca;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnOk;
    }
}