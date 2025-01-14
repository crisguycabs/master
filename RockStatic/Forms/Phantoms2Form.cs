﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RockStatic
{
    /// <summary>
    /// Ventana que presenta la información necesaria para el modelo simplificado de phantoms, solo densidad y numero atomico efectivo
    /// </summary>
    public partial class Phantoms2Form : Form
    {
        #region variables de clase

        /// <summary>
        /// Referencia al MainForm padre
        /// </summary>
        public MainForm padre;

        /// <summary>
        /// Referencia al form NewProjectForm que invoca
        /// </summary>
        public NewProjectForm newProjectForm;

        Point lastClick;

        /// <summary>
        /// Cadena que indica si la ventana se llamo desde MainForm (main) o desde SegmentacionForm (seg)
        /// </summary>
        public string quienLlamo = "main";

        #endregion

        public Phantoms2Form()
        {
            InitializeComponent();
        }

        private void lblTitulo_DoubleClick(object sender, EventArgs e)
        {
            CentrarForm();
        }

        /// <summary>
        /// Se centra el Form con respecto al MDI parent
        /// </summary>
        public void CentrarForm()
        {
            //this.Location = new System.Drawing.Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2, (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
            this.Location = new System.Drawing.Point((MdiParent.Width - this.Width) / 2, (int)((MdiParent.Height - this.Height) * 0.8 / 2));
        }

        private void btnSubir_MouseEnter(object sender, EventArgs e)
        {
            ((System.Windows.Forms.Button)(sender)).ForeColor = Color.White;
        }

        private void btnSubir_MouseLeave(object sender, EventArgs e)
        {
            ((System.Windows.Forms.Button)(sender)).ForeColor = Color.Black;
        }

        private void Phantoms2Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.padre.CerrarPhantom2Form();
        }

        private void lblTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            lastClick = e.Location;
        }

        private void lblTitulo_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastClick.X;
                this.Top += e.Y - lastClick.Y;
            }
        }

        private void Phantoms2Form_Load(object sender, EventArgs e)
        {
            if (quienLlamo == "main")
            {
                // se carga la información a mostrar en pantalla desde NewProjectForm

                numDensP1.Value = (decimal)newProjectForm.tempPhantom1.densidad;
                numDensP2.Value = (decimal)newProjectForm.tempPhantom2.densidad;
                numDensP3.Value = (decimal)newProjectForm.tempPhantom3.densidad;

                numZeffP1.Value = (decimal)newProjectForm.tempPhantom1.zeff;
                numZeffP2.Value = (decimal)newProjectForm.tempPhantom2.zeff;
                numZeffP3.Value = (decimal)newProjectForm.tempPhantom3.zeff;


                txtP1.Text = newProjectForm.tempPhantom1.nombre;
                txtP2.Text = newProjectForm.tempPhantom2.nombre;
                txtP3.Text = newProjectForm.tempPhantom3.nombre;
            }
            else
            {
                numDensP1.Value = (decimal)padre.actual.phantom1.densidad;
                numDensP2.Value = (decimal)padre.actual.phantom2.densidad;
                numDensP3.Value = (decimal)padre.actual.phantom3.densidad;

                numZeffP1.Value = (decimal)padre.actual.phantom1.zeff;
                numZeffP2.Value = (decimal)padre.actual.phantom2.zeff;
                numZeffP3.Value = (decimal)padre.actual.phantom3.zeff;


                txtP1.Text = padre.actual.phantom1.nombre;
                txtP2.Text = padre.actual.phantom2.nombre;
                txtP3.Text = padre.actual.phantom3.nombre;
            }
        }

        public void btnCerrar_Click(object sender, EventArgs e)
        {
            if (quienLlamo == "main")
            {
                // se guarda la informacion modificada y se cierra el form

                newProjectForm.tempPhantom1.densidad = (double)numDensP1.Value;
                newProjectForm.tempPhantom2.densidad = (double)numDensP2.Value;
                newProjectForm.tempPhantom3.densidad = (double)numDensP3.Value;

                newProjectForm.tempPhantom1.zeff = (double)numZeffP1.Value;
                newProjectForm.tempPhantom2.zeff = (double)numZeffP2.Value;
                newProjectForm.tempPhantom3.zeff = (double)numZeffP3.Value;

                newProjectForm.tempPhantom1.nombre = txtP1.Text;
                newProjectForm.tempPhantom2.nombre = txtP2.Text;
                newProjectForm.tempPhantom3.nombre = txtP3.Text;
            }
            else
            {
                // se guarda la informacion modificada y se cierra el form

                padre.actual.phantom1.densidad = (double)numDensP1.Value;
                padre.actual.phantom2.densidad = (double)numDensP2.Value;
                padre.actual.phantom3.densidad = (double)numDensP3.Value;

                padre.actual.phantom1.zeff = (double)numZeffP1.Value;
                padre.actual.phantom2.zeff = (double)numZeffP2.Value;
                padre.actual.phantom3.zeff = (double)numZeffP3.Value;

                padre.actual.phantom1.nombre = txtP1.Text;
                padre.actual.phantom2.nombre = txtP2.Text;
                padre.actual.phantom3.nombre = txtP3.Text;
            }

            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Phantoms2Form_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.Green, 2), this.DisplayRectangle);       
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void txtP1_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblTitulo_Click(object sender, EventArgs e)
        {

        }
    }
}
