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
    public partial class CurvasForm : Form
    {
        #region variables de clase

        Point lastClick;

        /// <summary>
        /// Referencia al MainForm
        /// </summary>
        public MainForm padre;

        /// <summary>
        /// valores de densidad estimada, un valor por cada slide
        /// </summary>
        public double[] densidad;

        /// <summary>
        /// valores de zeff estimado, un valor por cada slide
        /// </summary>
        public double[] zeff;

        #endregion

        public CurvasForm()
        {
            InitializeComponent();
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

        private void CurvasForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.padre.CerrarCurvasForm();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CurvasForm_Load(object sender, EventArgs e)
        {
            // se toma la informacion de segmentacion vs areas de interes y se estiman las propiedades petrofisicas estaticas

            // se preparan los generados de numeros aleatorios
            var phantom1High = new MathNet.Numerics.Distributions.Normal(padre.actual.phantom1.mediaHigh, padre.actual.phantom1.desvHigh);
            var phantom1Low = new MathNet.Numerics.Distributions.Normal(padre.actual.phantom1.mediaLow, padre.actual.phantom1.desvLow);

            var phantom2High = new MathNet.Numerics.Distributions.Normal(padre.actual.phantom2.mediaHigh, padre.actual.phantom2.desvHigh);
            var phantom2Low = new MathNet.Numerics.Distributions.Normal(padre.actual.phantom2.mediaLow, padre.actual.phantom2.desvLow);

            var phantom3High = new MathNet.Numerics.Distributions.Normal(padre.actual.phantom3.mediaHigh, padre.actual.phantom3.desvHigh);
            var phantom3Low = new MathNet.Numerics.Distributions.Normal(padre.actual.phantom3.mediaLow, padre.actual.phantom3.desvLow);

            // se prepara un vector de valores CT high y low para cada phantom
            double[] temp = new double[padre.actual.datacuboHigh.dataCube[0].segCore.Count];
            
            phantom1High.Samples(temp);
            ushort[] ctP1High = new ushort[temp.Length];
            for (int i = 0; i < temp.Length; i++) ctP1High[i] = (ushort)temp[i];

            phantom2High.Samples(temp);
            ushort[] ctP2High = new ushort[temp.Length];
            for (int i = 0; i < temp.Length; i++) ctP2High[i] = (ushort)temp[i];

            phantom3High.Samples(temp);
            ushort[] ctP3High = new ushort[temp.Length];
            for (int i = 0; i < temp.Length; i++) ctP3High[i] = (ushort)temp[i];

            phantom1Low.Samples(temp);
            ushort[] ctP1Low = new ushort[temp.Length];
            for (int i = 0; i < temp.Length; i++) ctP1Low[i] = (ushort)temp[i];

            phantom2Low.Samples(temp);
            ushort[] ctP2Low = new ushort[temp.Length];
            for (int i = 0; i < temp.Length; i++) ctP2Low[i] = (ushort)temp[i];

            phantom3Low.Samples(temp);
            ushort[] ctP3Low = new ushort[temp.Length];
            for (int i = 0; i < temp.Length; i++) ctP3Low[i] = (ushort)temp[i];

            // se preparan los vectores para densidad y zeff
            this.densidad = new double[temp.Length];
            this.zeff = new double[temp.Length];

            // se llenan los vectores con valores -1... si el valor es -1 entonces no se grafica
            
        }
    }
}
