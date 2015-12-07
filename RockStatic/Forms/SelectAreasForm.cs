using System;
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
    public partial class SelectAreasForm : Form
    {
        #region variables de disenador

        /// <summary>
        /// Referencia al MainForm padre
        /// </summary>
        public MainForm padre;

        List<byte[]> elementosCore;
        List<byte[]> elementosP1;
        List<byte[]> elementosP2;
        List<byte[]> elementosP3;

        #endregion

        Point lastClick;

        public SelectAreasForm()
        {
            InitializeComponent();
        }

        public void CentrarForm()
        {
            //this.Location = new System.Drawing.Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2, (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
            this.Location = new System.Drawing.Point((MdiParent.Width - this.Width) / 2, (int)((MdiParent.Height - this.Height) *0.1/ 2));
        }

        private void label4_DoubleClick(object sender, EventArgs e)
        {
            CentrarForm();            
        }

        private void label4_MouseDown(object sender, MouseEventArgs e)
        {
            lastClick = e.Location;
        }

        private void label4_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastClick.X;
                this.Top += e.Y - lastClick.Y;
            }
        }

        private void SelectAreasForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.padre.CerrarSelectAreasForm();
        }

        private void btnCerrar_MouseEnter(object sender, EventArgs e)
        {
            ((System.Windows.Forms.Button)(sender)).ForeColor = Color.White;
        }

        private void btnCerrar_MouseLeave(object sender, EventArgs e)
        {
            ((System.Windows.Forms.Button)(sender)).ForeColor = Color.Black;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void SelectAreasForm_Load(object sender, EventArgs e)
        {
            // se pintan algunos elementos
            elementosCore=padre.actual.GetSegCoreTransHigh();
            elementosP1 = padre.actual.GetSegPhantom1TransHigh();
            elementosP2 = padre.actual.GetSegPhantom2TransHigh();
            elementosP3 = padre.actual.GetSegPhantom3TransHigh();

            // se prepara la barra
            this.trackElementos.Minimum = 0;
            this.trackElementos.Maximum = elementosCore.Count - 1;
            this.trackElementos.Value = 0;

            this.pictCore.Image = MainForm.Byte2image(elementosCore[0]);
            this.pictP1.Image = MainForm.Byte2image(elementosP1[0]);
            this.pictP2.Image = MainForm.Byte2image(elementosP2[0]);
            this.pictP3.Image = MainForm.Byte2image(elementosP3[0]);
        }

        private void trackElementos_Scroll(object sender, EventArgs e)
        {
            int n = trackElementos.Value;

            this.pictCore.Image = MainForm.Byte2image(elementosCore[n]);
            this.pictP1.Image = MainForm.Byte2image(elementosP1[n]);
            this.pictP2.Image = MainForm.Byte2image(elementosP2[n]);
            this.pictP3.Image = MainForm.Byte2image(elementosP3[n]);
        }
    }
}
