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
    public partial class SegmentacionForm : Form
    {
        #region variables de clase

        /// <summary>
        /// Referencia al MainForm padre
        /// </summary>
        public MainForm padre;

        /// <summary>
        /// Contador para clicks
        /// </summary>
        int countClick;

        /// <summary>
        /// Array que guarda las coordenadas de los clicks necesarios para dibujar un circulo
        /// </summary>
        CCirculo[] tempClicks;

        #endregion

        Point lastClick;

        public SegmentacionForm()
        {
            InitializeComponent();
        }

        private void CheckForm_Load(object sender, EventArgs e)
        {
            SetForm();
        }

        public void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Se llena el ListBox, se reestablece el TrackBar
        /// </summary>
        public void SetForm()
        {
            // se reestablece el TrackBar
            trackElementos.Minimum = 1;
            trackElementos.Maximum = padre.actual.GetHigh().Count;
            trackElementos.Value = 1;
            
            // se pinta la primera imagen
            pictElemento.Image = MainForm.Byte2image(padre.actual.GetHigh()[0]);

            CentrarForm();
        }

        public static void SetPicture(string filename, PictureBox pb)
        {
            try
            {
                Image currentImage;
                byte[] imageBytes = System.IO.File.ReadAllBytes(filename);
                using (System.IO.MemoryStream msImage = new System.IO.MemoryStream(imageBytes))
                {
                    currentImage = Image.FromStream(msImage);
                    if (pb.InvokeRequired)
                    {
                        pb.Invoke(new MethodInvoker(
                        delegate()
                        {
                            pb.Image = currentImage;
                        }));
                    }
                    else
                    {
                        pb.Image = currentImage;
                    }
                }
            }
            catch 
            {
                
            }
        }

        public static Image NonLockingOpen(string filename)
        {
            Image result;

            #region Save file to byte array

            long size = (new System.IO.FileInfo(filename)).Length;
            System.IO.FileStream fs = new System.IO.FileStream(filename, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            byte[] data = new byte[size];
            try
            {
                fs.Read(data, 0, (int)size);
            }
            finally
            {
                fs.Close();
                fs.Dispose();
            }

            #endregion

            #region Convert bytes to image

            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            ms.Write(data, 0, (int)size);
            result = new Bitmap(ms);
            ms.Close();

            #endregion

            return result;
        }

        private void trackElementos_ValueChanged(object sender, EventArgs e)
        {
            pictElemento.Image = null;
            pictElemento.Image = MainForm.Byte2image(padre.actual.GetHigh()[trackElementos.Value - 1]);                    
        }

        /// <summary>
        /// Se pasa una ruta completa y se extrae el nombre del archivo
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string GetNameFile(string file)
        {
            string name = "";
            bool sw = true;
            int i = file.Length;

            while (sw)
            {
                i--;
                if (file[i] == '\\') sw = false;
                else name = file[i] + name;
            }
            
            return name;
        }        

        private void SegmentacionForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.padre.CerrarSegmentacionForm();
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

        public void CentrarForm()
        {
            // dado que ahora el area de trabajo es TODO el MdiParent, entonces solo se debe tomar el area del MdiParent para calcular la posicion inicial del MdiChild
            this.Location = new System.Drawing.Point((MdiParent.Width - this.Width) / 2, (int)((MdiParent.Height - this.Height) * 0.9 / 2) - 52);
        }

        private void lblTitulo_DoubleClick(object sender, EventArgs e)
        {
            CentrarForm();
        }

        private void btnSubir_MouseEnter(object sender, EventArgs e)
        {
            ((System.Windows.Forms.Button)(sender)).ForeColor = Color.White;
        }

        private void btnSubir_MouseLeave(object sender, EventArgs e)
        {
            ((System.Windows.Forms.Button)(sender)).ForeColor = Color.Black;
        }

        /// <summary>
        /// Se resetea el conteo de clicks
        /// </summary>
        public void ResetCountClick()
        {
            countClick = 0;
            tempClicks = new CCirculo[3];

            lblPunto1.Visible = false;
            lblPunto2.Visible = false;
            btnCancel.Enabled = false;            
        }

        private void radAuto_CheckedChanged(object sender, EventArgs e)
        {
            if (radAuto.Checked) radAuto.ForeColor = Color.White;
            else radAuto.ForeColor = Color.Green;

            grpAuto.Enabled = radAuto.Checked;
            ResetCountClick();
        }

        private void radManual_CheckedChanged(object sender, EventArgs e)
        {
            if (radManual.Checked) radManual.ForeColor = Color.White;
            else radManual.ForeColor = Color.Green;

            grpManual.Enabled = radManual.Checked;
            ResetCountClick();
        }        
    }
}
