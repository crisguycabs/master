namespace RockVision
{
    partial class NewProjectDForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewProjectDForm));
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lstCTtemp = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCTRo = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numCTo = new System.Windows.Forms.NumericUpDown();
            this.numCTw = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSelCTRo = new System.Windows.Forms.Button();
            this.btnSelCTRw = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCTRw = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnAddCTtemp = new System.Windows.Forms.Button();
            this.btnDelCTtemp = new System.Windows.Forms.Button();
            this.btnUpCTtemp = new System.Windows.Forms.Button();
            this.btnDownCTtemp = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnCrear = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCTw)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
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
            this.lblTitulo.Size = new System.Drawing.Size(595, 30);
            this.lblTitulo.TabIndex = 16;
            this.lblTitulo.Text = "LISTA DE DICOMS A CARGAR";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTitulo.DoubleClick += new System.EventHandler(this.lblTitulo_DoubleClick);
            this.lblTitulo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblTitulo_MouseDown);
            this.lblTitulo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblTitulo_MouseMove);
            // 
            // lstCTtemp
            // 
            this.lstCTtemp.FormattingEnabled = true;
            this.lstCTtemp.ItemHeight = 14;
            this.lstCTtemp.Location = new System.Drawing.Point(9, 26);
            this.lstCTtemp.Name = "lstCTtemp";
            this.lstCTtemp.Size = new System.Drawing.Size(558, 144);
            this.lstCTtemp.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 14);
            this.label1.TabIndex = 19;
            this.label1.Text = "Valor CT del crudo: ";
            // 
            // txtCTRo
            // 
            this.txtCTRo.Location = new System.Drawing.Point(9, 50);
            this.txtCTRo.Name = "txtCTRo";
            this.txtCTRo.Size = new System.Drawing.Size(558, 22);
            this.txtCTRo.TabIndex = 20;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numCTw);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.numCTo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 44);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(576, 51);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "VALORES DE REFERENCIA";
            // 
            // numCTo
            // 
            this.numCTo.DecimalPlaces = 3;
            this.numCTo.Location = new System.Drawing.Point(117, 19);
            this.numCTo.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numCTo.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.numCTo.Name = "numCTo";
            this.numCTo.Size = new System.Drawing.Size(81, 22);
            this.numCTo.TabIndex = 20;
            this.numCTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numCTo.Value = new decimal(new int[] {
            2363041,
            0,
            0,
            262144});
            // 
            // numCTw
            // 
            this.numCTw.DecimalPlaces = 3;
            this.numCTw.Location = new System.Drawing.Point(434, 17);
            this.numCTw.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numCTw.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.numCTw.Name = "numCTw";
            this.numCTw.Size = new System.Drawing.Size(81, 22);
            this.numCTw.TabIndex = 22;
            this.numCTw.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numCTw.Value = new decimal(new int[] {
            2241164,
            0,
            0,
            196608});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(305, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 14);
            this.label2.TabIndex = 21;
            this.label2.Text = "Valor CT agua dopada:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnSelCTRw);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtCTRw);
            this.groupBox2.Controls.Add(this.btnSelCTRo);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtCTRo);
            this.groupBox2.Location = new System.Drawing.Point(12, 103);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(576, 141);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "ESTADOS DE REFERENCIA";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(138, 14);
            this.label3.TabIndex = 19;
            this.label3.Text = "Roca saturada de crudo:";
            // 
            // btnSelCTRo
            // 
            this.btnSelCTRo.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSelCTRo.FlatAppearance.BorderColor = System.Drawing.Color.RoyalBlue;
            this.btnSelCTRo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelCTRo.Location = new System.Drawing.Point(190, 19);
            this.btnSelCTRo.Name = "btnSelCTRo";
            this.btnSelCTRo.Size = new System.Drawing.Size(81, 25);
            this.btnSelCTRo.TabIndex = 21;
            this.btnSelCTRo.Text = "Seleccionar";
            this.btnSelCTRo.UseVisualStyleBackColor = false;
            this.btnSelCTRo.Click += new System.EventHandler(this.btnSelCTRo_Click);
            this.btnSelCTRo.MouseEnter += new System.EventHandler(this.btnSubir_MouseEnter);
            this.btnSelCTRo.MouseLeave += new System.EventHandler(this.btnSubir_MouseLeave);
            // 
            // btnSelCTRw
            // 
            this.btnSelCTRw.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSelCTRw.FlatAppearance.BorderColor = System.Drawing.Color.RoyalBlue;
            this.btnSelCTRw.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelCTRw.Location = new System.Drawing.Point(191, 79);
            this.btnSelCTRw.Name = "btnSelCTRw";
            this.btnSelCTRw.Size = new System.Drawing.Size(81, 25);
            this.btnSelCTRw.TabIndex = 24;
            this.btnSelCTRw.Text = "Seleccionar";
            this.btnSelCTRw.UseVisualStyleBackColor = false;
            this.btnSelCTRw.Click += new System.EventHandler(this.btnSelCTRw_Click);
            this.btnSelCTRw.MouseEnter += new System.EventHandler(this.btnSubir_MouseEnter);
            this.btnSelCTRw.MouseLeave += new System.EventHandler(this.btnSubir_MouseLeave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(180, 14);
            this.label4.TabIndex = 22;
            this.label4.Text = "Roca saturada de agua dopada:";
            // 
            // txtCTRw
            // 
            this.txtCTRw.Location = new System.Drawing.Point(10, 110);
            this.txtCTRw.Name = "txtCTRw";
            this.txtCTRw.Size = new System.Drawing.Size(558, 22);
            this.txtCTRw.TabIndex = 23;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnDownCTtemp);
            this.groupBox3.Controls.Add(this.btnUpCTtemp);
            this.groupBox3.Controls.Add(this.btnDelCTtemp);
            this.groupBox3.Controls.Add(this.btnAddCTtemp);
            this.groupBox3.Controls.Add(this.lstCTtemp);
            this.groupBox3.Location = new System.Drawing.Point(12, 252);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(576, 213);
            this.groupBox3.TabIndex = 23;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "CUBOS DE DATOS TEMPORALES";
            // 
            // btnAddCTtemp
            // 
            this.btnAddCTtemp.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnAddCTtemp.FlatAppearance.BorderColor = System.Drawing.Color.RoyalBlue;
            this.btnAddCTtemp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddCTtemp.Location = new System.Drawing.Point(9, 176);
            this.btnAddCTtemp.Name = "btnAddCTtemp";
            this.btnAddCTtemp.Size = new System.Drawing.Size(81, 25);
            this.btnAddCTtemp.TabIndex = 25;
            this.btnAddCTtemp.Text = "Agregar";
            this.btnAddCTtemp.UseVisualStyleBackColor = false;
            this.btnAddCTtemp.Click += new System.EventHandler(this.btnAddCTtemp_Click);
            this.btnAddCTtemp.MouseEnter += new System.EventHandler(this.btnSubir_MouseEnter);
            this.btnAddCTtemp.MouseLeave += new System.EventHandler(this.btnSubir_MouseLeave);
            // 
            // btnDelCTtemp
            // 
            this.btnDelCTtemp.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnDelCTtemp.FlatAppearance.BorderColor = System.Drawing.Color.RoyalBlue;
            this.btnDelCTtemp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelCTtemp.Location = new System.Drawing.Point(96, 176);
            this.btnDelCTtemp.Name = "btnDelCTtemp";
            this.btnDelCTtemp.Size = new System.Drawing.Size(81, 25);
            this.btnDelCTtemp.TabIndex = 26;
            this.btnDelCTtemp.Text = "Eliminar";
            this.btnDelCTtemp.UseVisualStyleBackColor = false;
            this.btnDelCTtemp.Click += new System.EventHandler(this.btnDelCTtemp_Click);
            this.btnDelCTtemp.MouseEnter += new System.EventHandler(this.btnSubir_MouseEnter);
            this.btnDelCTtemp.MouseLeave += new System.EventHandler(this.btnSubir_MouseLeave);
            // 
            // btnUpCTtemp
            // 
            this.btnUpCTtemp.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnUpCTtemp.FlatAppearance.BorderColor = System.Drawing.Color.RoyalBlue;
            this.btnUpCTtemp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpCTtemp.Location = new System.Drawing.Point(400, 176);
            this.btnUpCTtemp.Name = "btnUpCTtemp";
            this.btnUpCTtemp.Size = new System.Drawing.Size(81, 25);
            this.btnUpCTtemp.TabIndex = 27;
            this.btnUpCTtemp.Text = "Subir";
            this.btnUpCTtemp.UseVisualStyleBackColor = false;
            this.btnUpCTtemp.Click += new System.EventHandler(this.btnUpCTtemp_Click);
            this.btnUpCTtemp.MouseEnter += new System.EventHandler(this.btnSubir_MouseEnter);
            this.btnUpCTtemp.MouseLeave += new System.EventHandler(this.btnSubir_MouseLeave);
            // 
            // btnDownCTtemp
            // 
            this.btnDownCTtemp.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnDownCTtemp.FlatAppearance.BorderColor = System.Drawing.Color.RoyalBlue;
            this.btnDownCTtemp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDownCTtemp.Location = new System.Drawing.Point(487, 176);
            this.btnDownCTtemp.Name = "btnDownCTtemp";
            this.btnDownCTtemp.Size = new System.Drawing.Size(81, 25);
            this.btnDownCTtemp.TabIndex = 28;
            this.btnDownCTtemp.Text = "Bajar";
            this.btnDownCTtemp.UseVisualStyleBackColor = false;
            this.btnDownCTtemp.Click += new System.EventHandler(this.btnDownCTtemp_Click);
            this.btnDownCTtemp.MouseEnter += new System.EventHandler(this.btnSubir_MouseEnter);
            this.btnDownCTtemp.MouseLeave += new System.EventHandler(this.btnSubir_MouseLeave);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCancelar.FlatAppearance.BorderColor = System.Drawing.Color.RoyalBlue;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Location = new System.Drawing.Point(513, 471);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 25);
            this.btnCancelar.TabIndex = 25;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            this.btnCancelar.MouseEnter += new System.EventHandler(this.btnSubir_MouseEnter);
            this.btnCancelar.MouseLeave += new System.EventHandler(this.btnSubir_MouseLeave);
            // 
            // btnCrear
            // 
            this.btnCrear.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCrear.FlatAppearance.BorderColor = System.Drawing.Color.RoyalBlue;
            this.btnCrear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCrear.Location = new System.Drawing.Point(433, 471);
            this.btnCrear.Name = "btnCrear";
            this.btnCrear.Size = new System.Drawing.Size(75, 25);
            this.btnCrear.TabIndex = 24;
            this.btnCrear.Text = "Continuar";
            this.btnCrear.UseVisualStyleBackColor = false;
            this.btnCrear.Click += new System.EventHandler(this.btnCrear_Click);
            this.btnCrear.MouseEnter += new System.EventHandler(this.btnSubir_MouseEnter);
            this.btnCrear.MouseLeave += new System.EventHandler(this.btnSubir_MouseLeave);
            // 
            // NewProjectDForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(595, 505);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnCrear);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblTitulo);
            this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NewProjectDForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "REVISAR ELEMENTOS NUEVO PROYECTO DE CARACTERIZACIÓN DINÁMICA";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.NewProjectDForm_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCTw)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.ListBox lstCTtemp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCTRo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown numCTo;
        private System.Windows.Forms.NumericUpDown numCTw;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSelCTRw;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCTRw;
        private System.Windows.Forms.Button btnSelCTRo;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnDownCTtemp;
        private System.Windows.Forms.Button btnUpCTtemp;
        private System.Windows.Forms.Button btnDelCTtemp;
        private System.Windows.Forms.Button btnAddCTtemp;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnCrear;
    }
}