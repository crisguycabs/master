using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RockVision
{
    public partial class HomeForm : Form
    {
        #region variables de diseñador

        /// <summary>
        /// Referencia al MainForm padre
        /// </summary>
        public MainForm padre;

        Point lastClick;

        #endregion

        public HomeForm()
        {
            InitializeComponent();
        }

        public void CentrarForm()
        {
            //this.Location = new System.Drawing.Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2, (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
            this.Location = new System.Drawing.Point((MdiParent.Width - this.Width) / 2, (int)((MdiParent.Height - this.Height) * 0.8 / 2));
        }

        private void label4_DoubleClick(object sender, EventArgs e)
        {
            CentrarForm();
        }

        private void btnNewv_Click(object sender, EventArgs e)
        {
            // se escogen los dicom que se quieren visualizar
            padre.NuevoProyectoV();
        }

        private void HomeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.padre.CerrarHomeForm();
        }

        private void btnNew_MouseEnter(object sender, EventArgs e)
        {
            ((System.Windows.Forms.Button)(sender)).ForeColor = Color.White;
        }

        private void btnNew_MouseLeave(object sender, EventArgs e)
        {
            ((System.Windows.Forms.Button)(sender)).ForeColor = Control.DefaultForeColor;
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

        private void HomeForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.RoyalBlue, 2), this.DisplayRectangle); 
        }
    }
}
