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
    public partial class PreviewSegForm : Form
    {
        #region variables de disenador

        /// <summary>
        /// Referencia al MainForm padre
        /// </summary>
        public MainForm padre;

        #endregion

        Point lastClick;

        public PreviewSegForm()
        {
            InitializeComponent();
        }

        private void PreviewSegForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.padre.CerrarPreviewSegForm();
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

        public void CentrarForm()
        {
            //this.Location = new System.Drawing.Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2, (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
            this.Location = new System.Drawing.Point((MdiParent.Width - this.Width) / 2, (int)((MdiParent.Height - this.Height) * 0.8 / 2));
        }

        private void label4_DoubleClick(object sender, EventArgs e)
        {
            CentrarForm();
        }
    }
}
