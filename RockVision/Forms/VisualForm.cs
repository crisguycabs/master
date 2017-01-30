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
    public partial class VisualForm : Form
    {
        #region variales de diseñador

        /// <summary>
        /// Referencia al MainForm padre
        /// </summary>
        public MainForm padre;

        Point lastClick;

        #endregion


        public VisualForm()
        {
            InitializeComponent();
        }

        private void VisualForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            padre.CerrarVisualForm();
        }

        /// <summary>
        /// Centra el Form en medio del MDI parent
        /// </summary>
        public void CentrarForm()
        {
            //this.Location = new System.Drawing.Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2, (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
            this.Location = new System.Drawing.Point((MdiParent.Width - this.Width) / 2, (int)((MdiParent.Height - this.Height) * 0.8 / 2));
        }

        private void lblTitulo_DoubleClick(object sender, EventArgs e)
        {
            CentrarForm();
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

        private void VisualForm_Load(object sender, EventArgs e)
        {
            
        }
    }
}
