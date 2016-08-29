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
    /// <summary>
    /// Ventana que contiene la información de derechos de autor de la aplicación ROCKSTATIC
    /// </summary>
    public partial class AboutForm : Form
    {
        public MainForm padre;

        Point lastClick;

        /// <summary>
        /// Initialize Component
        /// </summary>
        public AboutForm()
        {
            InitializeComponent();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Informa al MainForm padre que se ha cerrado la ventana
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AboutForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.padre.abiertoAboutForm = false;
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

        private void AboutForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.Green, 2), this.DisplayRectangle);
        }
    }
}
