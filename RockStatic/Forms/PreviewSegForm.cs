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
    /// Ventana que presenta al usuario el resultado preliminar de la segmentacion de los elementos core y phantoms
    /// </summary>
    public partial class PreviewSegForm : Form
    {
        #region variables de disenador

        /// <summary>
        /// Referencia al MainForm padre
        /// </summary>
        public MainForm padre;

        /// <summary>
        /// Corte del core a mostrar
        /// </summary>
        public Bitmap core;

        /// <summary>
        /// Corte del phantom p1
        /// </summary>
        public Bitmap p1;

        /// <summary>
        /// Corte del phantom p1
        /// </summary>
        public Bitmap p2;

        /// <summary>
        /// Corte del phantom p1
        /// </summary>
        public Bitmap p3;

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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PreviewSegForm_FormClosed_1(object sender, FormClosedEventArgs e)
        {
            this.padre.CerrarPreviewSegForm();
        }

        private void pictCore_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.Green, 2), this.DisplayRectangle);       
        }

        private void PreviewSegForm_Load(object sender, EventArgs e)
        {
            // se montan en los PictureBox las imagenes cortadas previamente desde SegmentacionForm
            pictCore.Image = core;
            pictP1.Image = p1;
            pictP2.Image = p2;
            pictP3.Image = p3;
        }        
    }
}
