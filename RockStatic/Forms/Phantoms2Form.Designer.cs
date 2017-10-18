namespace RockStatic
{
    partial class Phantoms2Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Phantoms2Form));
            this.lblTitulo = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtP1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.numDensP1 = new System.Windows.Forms.NumericUpDown();
            this.label22 = new System.Windows.Forms.Label();
            this.numZeffP1 = new System.Windows.Forms.NumericUpDown();
            this.label23 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtP2 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.numDensP2 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.numZeffP2 = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtP3 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.numDensP3 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numZeffP3 = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDensP1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numZeffP1)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDensP2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numZeffP2)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDensP3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numZeffP3)).BeginInit();
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
            this.lblTitulo.Size = new System.Drawing.Size(750, 30);
            this.lblTitulo.TabIndex = 9;
            this.lblTitulo.Text = "MODELO DE PHANTOMS";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTitulo.Click += new System.EventHandler(this.lblTitulo_Click);
            this.lblTitulo.DoubleClick += new System.EventHandler(this.lblTitulo_DoubleClick);
            this.lblTitulo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblTitulo_MouseDown);
            this.lblTitulo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblTitulo_MouseMove);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtP1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.numDensP1);
            this.groupBox1.Controls.Add(this.label22);
            this.groupBox1.Controls.Add(this.numZeffP1);
            this.groupBox1.Controls.Add(this.label23);
            this.groupBox1.Location = new System.Drawing.Point(4, 52);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(234, 109);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "MATERIAL IZQUIERDO";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // txtP1
            // 
            this.txtP1.Location = new System.Drawing.Point(112, 72);
            this.txtP1.Name = "txtP1";
            this.txtP1.Size = new System.Drawing.Size(113, 20);
            this.txtP1.TabIndex = 22;
            this.txtP1.Text = "Cuarzo";
            this.txtP1.TextChanged += new System.EventHandler(this.txtP1_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 78);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "Nombre del material:";
            // 
            // numDensP1
            // 
            this.numDensP1.DecimalPlaces = 2;
            this.numDensP1.Location = new System.Drawing.Point(112, 20);
            this.numDensP1.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.numDensP1.Name = "numDensP1";
            this.numDensP1.Size = new System.Drawing.Size(71, 20);
            this.numDensP1.TabIndex = 20;
            this.numDensP1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numDensP1.Value = new decimal(new int[] {
            22,
            0,
            0,
            65536});
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(5, 26);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(55, 13);
            this.label22.TabIndex = 19;
            this.label22.Text = "Densidad:";
            // 
            // numZeffP1
            // 
            this.numZeffP1.DecimalPlaces = 2;
            this.numZeffP1.Location = new System.Drawing.Point(112, 46);
            this.numZeffP1.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.numZeffP1.Name = "numZeffP1";
            this.numZeffP1.Size = new System.Drawing.Size(71, 20);
            this.numZeffP1.TabIndex = 18;
            this.numZeffP1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numZeffP1.Value = new decimal(new int[] {
            117842,
            0,
            0,
            262144});
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(5, 53);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(29, 13);
            this.label23.TabIndex = 17;
            this.label23.Text = "Zeff:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtP2);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.numDensP2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.numZeffP2);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(255, 52);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(234, 109);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "MATERIAL DEL CENTRO";
            // 
            // txtP2
            // 
            this.txtP2.Location = new System.Drawing.Point(112, 72);
            this.txtP2.Name = "txtP2";
            this.txtP2.Size = new System.Drawing.Size(113, 20);
            this.txtP2.TabIndex = 22;
            this.txtP2.Text = "Agua";
            this.txtP2.TextChanged += new System.EventHandler(this.txtP1_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 78);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(103, 13);
            this.label6.TabIndex = 22;
            this.label6.Text = "Nombre del material:";
            // 
            // numDensP2
            // 
            this.numDensP2.DecimalPlaces = 2;
            this.numDensP2.Location = new System.Drawing.Point(112, 20);
            this.numDensP2.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.numDensP2.Name = "numDensP2";
            this.numDensP2.Size = new System.Drawing.Size(71, 20);
            this.numDensP2.TabIndex = 20;
            this.numDensP2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numDensP2.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Densidad:";
            // 
            // numZeffP2
            // 
            this.numZeffP2.DecimalPlaces = 2;
            this.numZeffP2.Location = new System.Drawing.Point(112, 46);
            this.numZeffP2.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.numZeffP2.Name = "numZeffP2";
            this.numZeffP2.Size = new System.Drawing.Size(71, 20);
            this.numZeffP2.TabIndex = 18;
            this.numZeffP2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numZeffP2.Value = new decimal(new int[] {
            75195,
            0,
            0,
            262144});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Zeff:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtP3);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.numDensP3);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.numZeffP3);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Location = new System.Drawing.Point(508, 52);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(234, 109);
            this.groupBox3.TabIndex = 21;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "MATERIAL DERECHO";
            // 
            // txtP3
            // 
            this.txtP3.Location = new System.Drawing.Point(112, 71);
            this.txtP3.Name = "txtP3";
            this.txtP3.Size = new System.Drawing.Size(113, 20);
            this.txtP3.TabIndex = 22;
            this.txtP3.Text = "Teflon";
            this.txtP3.TextChanged += new System.EventHandler(this.txtP1_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 78);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(103, 13);
            this.label7.TabIndex = 23;
            this.label7.Text = "Nombre del material:";
            // 
            // numDensP3
            // 
            this.numDensP3.DecimalPlaces = 2;
            this.numDensP3.Location = new System.Drawing.Point(112, 22);
            this.numDensP3.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.numDensP3.Name = "numDensP3";
            this.numDensP3.Size = new System.Drawing.Size(71, 20);
            this.numDensP3.TabIndex = 20;
            this.numDensP3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numDensP3.Value = new decimal(new int[] {
            216,
            0,
            0,
            131072});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Densidad:";
            // 
            // numZeffP3
            // 
            this.numZeffP3.DecimalPlaces = 2;
            this.numZeffP3.Location = new System.Drawing.Point(112, 45);
            this.numZeffP3.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.numZeffP3.Name = "numZeffP3";
            this.numZeffP3.Size = new System.Drawing.Size(71, 20);
            this.numZeffP3.TabIndex = 18;
            this.numZeffP3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numZeffP3.Value = new decimal(new int[] {
            870,
            0,
            0,
            131072});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Zeff:";
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCancelar.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnCancelar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkSeaGreen;
            this.btnCancelar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Green;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(368, 179);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(68, 23);
            this.btnCancelar.TabIndex = 22;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            this.btnCancelar.MouseEnter += new System.EventHandler(this.btnSubir_MouseEnter);
            this.btnCancelar.MouseLeave += new System.EventHandler(this.btnSubir_MouseLeave);
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCerrar.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnCerrar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkSeaGreen;
            this.btnCerrar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Green;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.Location = new System.Drawing.Point(291, 179);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(68, 23);
            this.btnCerrar.TabIndex = 23;
            this.btnCerrar.Text = "Guardar";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            this.btnCerrar.MouseEnter += new System.EventHandler(this.btnSubir_MouseEnter);
            this.btnCerrar.MouseLeave += new System.EventHandler(this.btnSubir_MouseLeave);
            // 
            // Phantoms2Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(750, 216);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Phantoms2Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phantoms2Form";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Phantoms2Form_FormClosed);
            this.Load += new System.EventHandler(this.Phantoms2Form_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Phantoms2Form_Paint);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDensP1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numZeffP1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDensP2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numZeffP2)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDensP3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numZeffP3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown numDensP1;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.NumericUpDown numZeffP1;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown numDensP2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numZeffP2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NumericUpDown numDensP3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numZeffP3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtP1;
        private System.Windows.Forms.TextBox txtP2;
        private System.Windows.Forms.TextBox txtP3;
    }
}