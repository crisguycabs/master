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
            this.pictLow = new System.Windows.Forms.PictureBox();
            this.pictHigh = new System.Windows.Forms.PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictLow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictHigh)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nombre:";
            // 
            // txtNewName
            // 
            this.txtNewName.Location = new System.Drawing.Point(75, 38);
            this.txtNewName.Name = "txtNewName";
            this.txtNewName.Size = new System.Drawing.Size(100, 22);
            this.txtNewName.TabIndex = 2;
            this.txtNewName.Text = "NewProject";
            this.txtNewName.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "HIGH Energy Dycom";
            // 
            // btnSelHigh
            // 
            this.btnSelHigh.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSelHigh.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnSelHigh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkSeaGreen;
            this.btnSelHigh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Green;
            this.btnSelHigh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelHigh.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelHigh.Location = new System.Drawing.Point(143, 70);
            this.btnSelHigh.Name = "btnSelHigh";
            this.btnSelHigh.Size = new System.Drawing.Size(80, 23);
            this.btnSelHigh.TabIndex = 4;
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
            this.btnCheckHigh.Location = new System.Drawing.Point(258, 70);
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
            this.label3.Location = new System.Drawing.Point(10, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 14);
            this.label3.TabIndex = 3;
            this.label3.Text = "LOW Energy Dycom";
            // 
            // btnSelLow
            // 
            this.btnSelLow.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSelLow.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnSelLow.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkSeaGreen;
            this.btnSelLow.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Green;
            this.btnSelLow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelLow.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelLow.Location = new System.Drawing.Point(143, 102);
            this.btnSelLow.Name = "btnSelLow";
            this.btnSelLow.Size = new System.Drawing.Size(80, 23);
            this.btnSelLow.TabIndex = 4;
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
            this.btnCheckLow.Location = new System.Drawing.Point(258, 102);
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
            this.btnCrear.Location = new System.Drawing.Point(270, 178);
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
            this.btnCancelar.Location = new System.Drawing.Point(196, 178);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(68, 23);
            this.btnCancelar.TabIndex = 4;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            this.btnCancelar.MouseEnter += new System.EventHandler(this.btnSelHigh_MouseEnter);
            this.btnCancelar.MouseLeave += new System.EventHandler(this.btnSelHigh_MouseLeave);
            // 
            // lblError
            // 
            this.lblError.ForeColor = System.Drawing.Color.Red;
            this.lblError.Location = new System.Drawing.Point(10, 138);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(328, 32);
            this.lblError.TabIndex = 6;
            this.lblError.Text = "El numero de elementos dycom HIGH no coincide con el numero de elementos dycom LO" +
    "W";
            // 
            // pictLow
            // 
            this.pictLow.Image = global::RockStatic.Properties.Resources.redX;
            this.pictLow.Location = new System.Drawing.Point(229, 102);
            this.pictLow.Name = "pictLow";
            this.pictLow.Size = new System.Drawing.Size(23, 23);
            this.pictLow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictLow.TabIndex = 5;
            this.pictLow.TabStop = false;
            // 
            // pictHigh
            // 
            this.pictHigh.Image = global::RockStatic.Properties.Resources.redX;
            this.pictHigh.Location = new System.Drawing.Point(229, 70);
            this.pictHigh.Name = "pictHigh";
            this.pictHigh.Size = new System.Drawing.Size(23, 23);
            this.pictHigh.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictHigh.TabIndex = 5;
            this.pictHigh.TabStop = false;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Green;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Eras Bold ITC", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(347, 30);
            this.lblTitle.TabIndex = 7;
            this.lblTitle.Text = "NUEVO PROYECTO";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTitle.DoubleClick += new System.EventHandler(this.lblTitle_DoubleClick);
            this.lblTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblTitle_MouseDown);
            this.lblTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblTitle_MouseMove);
            // 
            // NewProjectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(347, 210);
            this.ControlBox = false;
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
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NewProjectForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NUEVO PROYECTO";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.NewProjectForm_FormClosed);
            this.Load += new System.EventHandler(this.NewProjectForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictLow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictHigh)).EndInit();
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
    }
}