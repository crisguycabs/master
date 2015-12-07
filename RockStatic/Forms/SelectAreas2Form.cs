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
    public partial class SelectAreas2Form : Form
    {
        #region variables de disenador

        /// <summary>
        /// Referencia al MainForm padre
        /// </summary>
        public MainForm padre;

        #endregion

        Point lastClick;

        public SelectAreas2Form()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SelectAreas2Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.padre.CerrarSelectAreasForm();
        }

        private void SelectAreas2Form_Load(object sender, EventArgs e)
        {
            List<byte[]> core = padre.actual.GetSegCoreHorHigh();
            List<byte[]> p1 = padre.actual.GetSegPhantom1HorHigh();
            List<byte[]> p2 = padre.actual.GetSegPhantom2HorHigh();
            List<byte[]> p3 = padre.actual.GetSegPhantom3HorHigh();

            pictCore.Image = MainForm.Byte2image(core[0]);
            pictPhantom1.Image = MainForm.Byte2image(p1[0]);
            pictPhantom2.Image = MainForm.Byte2image(p2[0]);
            pictPhantom3.Image = MainForm.Byte2image(p3[0]);

            rangeBar.TotalMaximum = padre.actual.count-1;
            rangeBar.TotalMinimum = 0;
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
    }
}
