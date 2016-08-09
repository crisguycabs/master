namespace RockStatic
{
    partial class NewProjectForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewProjectForm));
            this.label1 = new System.Windows.Forms.Label();
            this.txtNewName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSelHigh = new System.Windows.Forms.Button();
            this.btnCheckHigh = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSelLow = new System.Windows.Forms.Button();
            this.btnCheckLow = new System.Windows.Forms.Button();
            this.btnCrear = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.lblError = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.radPhantoms = new System.Windows.Forms.RadioButton();
            this.radNoPhantoms = new System.Windows.Forms.RadioButton();
            this.btnPhantoms = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.pictLow = new System.Windows.Forms.PictureBox();
            this.pictHigh = new System.Windows.Forms.PictureBox();
            this.btnPhantoms2 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.numHead = new System.Windows.Forms.NumericUpDown();
            this.numTail = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lstUnidades = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictLow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictHigh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHead)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTail)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nombre:";
            // 
            // txtNewName
            // 
            this.txtNewName.Location = new System.Drawing.Point(75, 40);
            this.txtNewName.Name = "txtNewName";
            this.txtNewName.Size = new System.Drawing.Size(100, 22);
            this.txtNewName.TabIndex = 1;
            this.txtNewName.Text = "NewProject";
            this.txtNewName.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "HIGH Energy Dicom:";
            // 
            // btnSelHigh
            // 
            this.btnSelHigh.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSelHigh.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnSelHigh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkSeaGreen;
            this.btnSelHigh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Green;
            this.btnSelHigh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelHigh.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelHigh.Location = new System.Drawing.Point(143, 83);
            this.btnSelHigh.Name = "btnSelHigh";
            this.btnSelHigh.Size = new System.Drawing.Size(80, 23);
            this.btnSelHigh.TabIndex = 2;
            this.btnSelHigh.Text = "Seleccionar";
            this.btnSelHigh.UseVisualStyleBackColor = false;
            this.btnSelHigh.Click += new System.EventHandler(this.btnSelHigh_Click);
            this.btnSelHigh.MouseEnter += new System.EventHandler(this.btnSelHigh_MouseEnter);
            this.btnSelHigh.MouseLeave += new System.EventHandler(this.btnSelHigh_MouseLeave);
            // 
            // btnCheckHigh
            // 
            this.btnCheckHigh.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCheckHigh.Enabled = false;
            this.btnCheckHigh.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnCheckHigh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkSeaGreen;
            this.btnCheckHigh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Green;
            this.btnCheckHigh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckHigh.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheckHigh.Location = new System.Drawing.Point(229, 83);
            this.btnCheckHigh.Name = "btnCheckHigh";
            this.btnCheckHigh.Size = new System.Drawing.Size(80, 23);
            this.btnCheckHigh.TabIndex = 4;
            this.btnCheckHigh.Text = "Revisar";
            this.btnCheckHigh.UseVisualStyleBackColor = false;
            this.btnCheckHigh.Click += new System.EventHandler(this.btnCheckHigh_Click);
            this.btnCheckHigh.MouseEnter += new System.EventHandler(this.btnSelHigh_MouseEnter);
            this.btnCheckHigh.MouseLeave += new System.EventHandler(this.btnSelHigh_MouseLeave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 14);
            this.label3.TabIndex = 3;
            this.label3.Text = "LOW Energy Dicom:";
            // 
            // btnSelLow
            // 
            this.btnSelLow.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSelLow.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnSelLow.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkSeaGreen;
            this.btnSelLow.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Green;
            this.btnSelLow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelLow.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelLow.Location = new System.Drawing.Point(143, 115);
            this.btnSelLow.Name = "btnSelLow";
            this.btnSelLow.Size = new System.Drawing.Size(80, 23);
            this.btnSelLow.TabIndex = 3;
            this.btnSelLow.Text = "Seleccionar";
            this.btnSelLow.UseVisualStyleBackColor = false;
            this.btnSelLow.Click += new System.EventHandler(this.btnSelLow_Click);
            this.btnSelLow.MouseEnter += new System.EventHandler(this.btnSelHigh_MouseEnter);
            this.btnSelLow.MouseLeave += new System.EventHandler(this.btnSelHigh_MouseLeave);
            // 
            // btnCheckLow
            // 
            this.btnCheckLow.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCheckLow.Enabled = false;
            this.btnCheckLow.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnCheckLow.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkSeaGreen;
            this.btnCheckLow.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Green;
            this.btnCheckLow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckLow.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheckLow.Location = new System.Drawing.Point(229, 115);
            this.btnCheckLow.Name = "btnCheckLow";
            this.btnCheckLow.Size = new System.Drawing.Size(80, 23);
            this.btnCheckLow.TabIndex = 4;
            this.btnCheckLow.Text = "Revisar";
            this.btnCheckLow.UseVisualStyleBackColor = false;
            this.btnCheckLow.Click += new System.EventHandler(this.btnCheckLow_Click);
            this.btnCheckLow.MouseEnter += new System.EventHandler(this.btnSelHigh_MouseEnter);
            this.btnCheckLow.MouseLeave += new System.EventHandler(this.btnSelHigh_MouseLeave);
            // 
            // btnCrear
            // 
            this.btnCrear.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCrear.Enabled = false;
            this.btnCrear.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnCrear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkSeaGreen;
            this.btnCrear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Green;
            this.btnCrear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCrear.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCrear.Location = new System.Drawing.Point(274, 343);
            this.btnCrear.Name = "btnCrear";
            this.btnCrear.Size = new System.Drawing.Size(68, 23);
            this.btnCrear.TabIndex = 4;
            this.btnCrear.Text = "Crear";
            this.btnCrear.UseVisualStyleBackColor = false;
            this.btnCrear.Click += new System.EventHandler(this.btnCrear_Click);
            this.btnCrear.MouseEnter += new System.EventHandler(this.btnSelHigh_MouseEnter);
            this.btnCrear.MouseLeave += new System.EventHandler(this.btnSelHigh_MouseLeave);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCancelar.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnCancelar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkSeaGreen;
            this.btnCancelar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Green;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(200, 343);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(68, 23);
            this.btnCancelar.TabIndex = 5;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            this.btnCancelar.MouseEnter += new System.EventHandler(this.btnSelHigh_MouseEnter);
            this.btnCancelar.MouseLeave += new System.EventHandler(this.btnSelHigh_MouseLeave);
            // 
            // lblError
            // 
            this.lblError.ForeColor = System.Drawing.Color.Red;
            this.lblError.Location = new System.Drawing.Point(10, 147);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(328, 32);
            this.lblError.TabIndex = 6;
            this.lblError.Text = "El numero de elementos dicom HIGH no coincide con el numero de elementos dicom LO" +
    "W";
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Green;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Eras Bold ITC", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(348, 30);
            this.lblTitle.TabIndex = 7;
            this.lblTitle.Text = "NUEVO PROYECTO";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTitle.DoubleClick += new System.EventHandler(this.lblTitle_DoubleClick);
            this.lblTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblTitle_MouseDown);
            this.lblTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblTitle_MouseMove);
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.Location = new System.Drawing.Point(12, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(330, 2);
            this.label4.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.Location = new System.Drawing.Point(12, 184);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(330, 2);
            this.label5.TabIndex = 9;
            // 
            // radPhantoms
            // 
            this.radPhantoms.Checked = true;
            this.radPhantoms.Location = new System.Drawing.Point(12, 189);
            this.radPhantoms.Name = "radPhantoms";
            this.radPhantoms.Size = new System.Drawing.Size(224, 36);
            this.radPhantoms.TabIndex = 10;
            this.radPhantoms.TabStop = true;
            this.radPhantoms.Text = "Los Dicom contienen información de los Phantoms";
            this.radPhantoms.UseVisualStyleBackColor = true;
            this.radPhantoms.CheckedChanged += new System.EventHandler(this.radPhantoms_CheckedChanged);
            // 
            // radNoPhantoms
            // 
            this.radNoPhantoms.Location = new System.Drawing.Point(12, 223);
            this.radNoPhantoms.Name = "radNoPhantoms";
            this.radNoPhantoms.Size = new System.Drawing.Size(224, 36);
            this.radNoPhantoms.TabIndex = 11;
            this.radNoPhantoms.Text = "Los Dicom no contienen información de los Phantoms";
            this.radNoPhantoms.UseVisualStyleBackColor = true;
            // 
            // btnPhantoms
            // 
            this.btnPhantoms.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnPhantoms.Enabled = false;
            this.btnPhantoms.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnPhantoms.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkSeaGreen;
            this.btnPhantoms.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Green;
            this.btnPhantoms.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPhantoms.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPhantoms.Location = new System.Drawing.Point(258, 230);
            this.btnPhantoms.Name = "btnPhantoms";
            this.btnPhantoms.Size = new System.Drawing.Size(80, 23);
            this.btnPhantoms.TabIndex = 12;
            this.btnPhantoms.Text = "Seleccionar";
            this.btnPhantoms.UseVisualStyleBackColor = false;
            this.btnPhantoms.Click += new System.EventHandler(this.btnPhantoms_Click);
            this.btnPhantoms.MouseEnter += new System.EventHandler(this.btnSelHigh_MouseEnter);
            this.btnPhantoms.MouseLeave += new System.EventHandler(this.btnSelHigh_MouseLeave);
            // 
            // label6
            // 
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label6.Location = new System.Drawing.Point(10, 264);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(330, 2);
            this.label6.TabIndex = 13;
            // 
            // pictLow
            // 
            this.pictLow.Image = global::RockStatic.Properties.Resources.redX;
            this.pictLow.Location = new System.Drawing.Point(315, 115);
            this.pictLow.Name = "pictLow";
            this.pictLow.Size = new System.Drawing.Size(23, 23);
            this.pictLow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictLow.TabIndex = 5;
            this.pictLow.TabStop = false;
            // 
            // pictHigh
            // 
            this.pictHigh.Image = global::RockStatic.Properties.Resources.redX;
            this.pictHigh.Location = new System.Drawing.Point(315, 83);
            this.pictHigh.Name = "pictHigh";
            this.pictHigh.Size = new System.Drawing.Size(23, 23);
            this.pictHigh.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictHigh.TabIndex = 5;
            this.pictHigh.TabStop = false;
            // 
            // btnPhantoms2
            // 
            this.btnPhantoms2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnPhantoms2.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnPhantoms2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkSeaGreen;
            this.btnPhantoms2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Green;
            this.btnPhantoms2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPhantoms2.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPhantoms2.Location = new System.Drawing.Point(258, 196);
            this.btnPhantoms2.Name = "btnPhantoms2";
            this.btnPhantoms2.Size = new System.Drawing.Size(80, 23);
            this.btnPhantoms2.TabIndex = 14;
            this.btnPhantoms2.Text = "Seleccionar";
            this.btnPhantoms2.UseVisualStyleBackColor = false;
            this.btnPhantoms2.Click += new System.EventHandler(this.btnPhantoms2_Click);
            this.btnPhantoms2.MouseEnter += new System.EventHandler(this.btnSelHigh_MouseEnter);
            this.btnPhantoms2.MouseLeave += new System.EventHandler(this.btnSelHigh_MouseLeave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 277);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(156, 14);
            this.label7.TabIndex = 0;
            this.label7.Text = "Profundidad de la muestra:";
            // 
            // numHead
            // 
            this.numHead.Location = new System.Drawing.Point(69, 303);
            this.numHead.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.numHead.Name = "numHead";
            this.numHead.Size = new System.Drawing.Size(84, 22);
            this.numHead.TabIndex = 15;
            this.numHead.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // numTail
            // 
            this.numTail.Location = new System.Drawing.Point(254, 303);
            this.numTail.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.numTail.Name = "numTail";
            this.numTail.Size = new System.Drawing.Size(84, 22);
            this.numTail.TabIndex = 16;
            this.numTail.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numTail.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(14, 307);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 14);
            this.label8.TabIndex = 17;
            this.label8.Text = "Cabeza:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(214, 307);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(34, 14);
            this.label9.TabIndex = 18;
            this.label9.Text = "Cola:";
            // 
            // label10
            // 
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label10.Location = new System.Drawing.Point(10, 333);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(330, 2);
            this.label10.TabIndex = 19;
            // 
            // lstUnidades
            // 
            this.lstUnidades.FormattingEnabled = true;
            this.lstUnidades.Items.AddRange(new object[] {
            "km",
            "ft"});
            this.lstUnidades.Location = new System.Drawing.Point(169, 273);
            this.lstUnidades.Name = "lstUnidades";
            this.lstUnidades.Size = new System.Drawing.Size(43, 22);
            this.lstUnidades.TabIndex = 20;
            // 
            // NewProjectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(348, 373);
            this.ControlBox = false;
            this.Controls.Add(this.lstUnidades);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.numTail);
            this.Controls.Add(this.numHead);
            this.Controls.Add(this.btnPhantoms2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnPhantoms);
            this.Controls.Add(this.radNoPhantoms);
            this.Controls.Add(this.radPhantoms);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.pictLow);
            this.Controls.Add(this.pictHigh);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnCrear);
            this.Controls.Add(this.btnCheckLow);
            this.Controls.Add(this.btnCheckHigh);
            this.Controls.Add(this.btnSelLow);
            this.Controls.Add(this.btnSelHigh);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtNewName);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NewProjectForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NUEVO PROYECTO";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.NewProjectForm_FormClosed);
            this.Load += new System.EventHandler(this.NewProjectForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.NewProjectForm_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.pictLow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictHigh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHead)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTail)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNewName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSelHigh;
        private System.Windows.Forms.PictureBox pictHigh;
        private System.Windows.Forms.Button btnCheckHigh;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSelLow;
        private System.Windows.Forms.Button btnCheckLow;
        private System.Windows.Forms.PictureBox pictLow;
        private System.Windows.Forms.Button btnCrear;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton radPhantoms;
        private System.Windows.Forms.RadioButton radNoPhantoms;
        private System.Windows.Forms.Button btnPhantoms;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnPhantoms2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numHead;
        private System.Windows.Forms.NumericUpDown numTail;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox lstUnidades;
    }
}