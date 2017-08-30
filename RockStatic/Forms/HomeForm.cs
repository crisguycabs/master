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
    /// Ventana que funciona como Home al momento de ejecutar ROCKSTATIC
    /// </summary>
    public partial class HomeForm : Form
    {
        #region variables de disenador

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

        private void btnNew_MouseEnter(object sender, EventArgs e)
        {
            btnNew.ForeColor = Color.White;            
        }

        private void btnNew_MouseLeave(object sender, EventArgs e)
        {
            btnNew.ForeColor = Control.DefaultForeColor;
        }

        private void btnOpen_MouseEnter(object sender, EventArgs e)
        {
            btnOpen.ForeColor = Color.White;       
        }

        private void btnOpen_MouseLeave(object sender, EventArgs e)
        {
            btnOpen.ForeColor = Control.DefaultForeColor;
        }

        private void HomeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.padre.CerrarHomeForm();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            this.padre.NuevoProyecto();
            this.Close();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (this.padre.SeleccionarProyecto()) this.Close();            
        }

        private void HomeForm_MouseDown(object sender, MouseEventArgs e)
        {
            lastClick = e.Location;
        }

        private void HomeForm_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button==MouseButtons.Left)
            {
                this.Left += e.X - lastClick.X;
                this.Top += e.Y - lastClick.Y;
            }
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

        private void HomeForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.Green, 2), this.DisplayRectangle);       
        }

        private void HomeForm_Load(object sender, EventArgs e)
        {

        }    
    }
}
