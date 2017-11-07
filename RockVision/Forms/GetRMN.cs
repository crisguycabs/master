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
            openFID.Filter="Archivo FID (.dps)|*.dps";

            if (openFID.ShowDialog() == DialogResult.OK)
            {
                rutaFID = openFID.FileName;
                txtFid.Text = rutaFID;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            // primero se leen los archivos de texto plano, y se prepara para recibir un error

            string line="";
            string[] line2=null;

            // FID
            try
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(rutaFID);

                line = sr.ReadLine();
                line2 = line.Split('\t');
                padre.fid = Convert.ToDouble(line2[2]);
            }
            catch
            {
                MessageBox.Show("Error al leer el archivo FID", "Error de lectura", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }

            
            // FIDstd
            try
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(rutaFIDstd);

                line = sr.ReadLine();
                line2 = line.Split('\t');
                padre.fidstd = Convert.ToDouble(line2[2]);
            }
            catch
            {
                MessageBox.Show("Error al leer el archivo FID estándar", "Error de lectura", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }

            padre.vstd = Convert.ToDouble(numVstd.Value);

            padre.vroca = Convert.ToDouble(numVroca.Value);
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
    }
}
