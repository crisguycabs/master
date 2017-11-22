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
    public partial class GetRMN : Form
    {
        #region Variables de diseñador

        public string rutaFID;

        public string rutaFIDstd;

        public MainForm padre;

        Point lastClick;

        #endregion

        public GetRMN()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Se selecciona el archivo FID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFID_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFID = new OpenFileDialog();

            openFID.Title = "Seleccione el archivo FID a cargar";
            openFID.Filter = "Archivo FID (.dps)|*.dps";

            if (openFID.ShowDialog() == DialogResult.OK)
            {
                rutaFID = openFID.FileName;
                txtFid.Text = rutaFID;
            }
        }

        public string CorregirDecimal(string cadena)
        {
            string caracter = Convert.ToString((double)3 / (double)2);
            cadena = cadena.Replace('.', caracter[1]);
            cadena = cadena.Replace(',', caracter[1]);

            return cadena;

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            // primero se leen los archivos de texto plano, y se prepara para recibir un error

            string line = "";
            string[] line2 = null;

            // FID
            try
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(rutaFID);

                line = sr.ReadLine();
                line2 = line.Split('\t');
                padre.fid = Convert.ToDouble(CorregirDecimal(line2[2]));
            }
            catch
            {
                MessageBox.Show("Error al leer el archivo FID", "Error de lectura", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }


            // FIDstd
            try
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(rutaFIDstd);

                line = sr.ReadLine();
                line2 = line.Split('\t');
                padre.fidstd = Convert.ToDouble(CorregirDecimal(line2[2]));
            }
            catch
            {
                MessageBox.Show("Error al leer el archivo FID estándar", "Error de lectura", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }

            padre.vstd = Convert.ToDouble(numVstd.Value);

            padre.vroca = Convert.ToDouble(numVroca.Value);

            try
            {
                padre.porRMN = padre.PorosidadRMN(padre.fid, padre.vstd, padre.fidstd, padre.vroca);
                this.padre.proyectoDForm.DibujarPorosidadRMN();
                this.DialogResult = DialogResult.OK;
                this.Close();                
            }
            catch
            {
                MessageBox.Show("No fue posible calcular la porosidad de la muestra usando las mediciones RMN", "Error al estimar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.Cancel;
            }            

            // txtPorosidad.Text = Convert.ToString(padre.porRMN);
        }

        private void btnFIDstd_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFID = new OpenFileDialog();

            openFID.Title = "Seleccione el archivo FID estándar a cargar";
            openFID.Filter = "Archivo FID (.dps)|*.dps";

            if (openFID.ShowDialog() == DialogResult.OK)
            {
                rutaFIDstd = openFID.FileName;
                txtFidstd.Text = rutaFIDstd;
            }
        }

        private void txtFidstd_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_DoubleClick(object sender, EventArgs e)
        {
            CentrarForm();
        }

        public void CentrarForm()
        {
            //this.Location = new System.Drawing.Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2, (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
            this.Location = new System.Drawing.Point((MdiParent.Width - this.Width) / 2, (int)((MdiParent.Height - this.Height) * 0.8 / 2));
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

        private void GetRMN_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.RoyalBlue, 2), this.DisplayRectangle); 
        }

        private void GetRMN_FormClosed(object sender, FormClosedEventArgs e)
        {
            padre.CerrarGetRMNForm();
        }        
    }
}
