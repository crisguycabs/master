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
    /// Ventana que presenta la información necesaria para el modelo completo de phantoms
    /// </summary>
    public partial class PhantomsForm : Form
    {
        #region variables de clase

        /// <summary>
        /// Referencia al MainForm padre
        /// </summary>
        public MainForm padre;

        /// <summary>
        /// Referencia al form NewProjectForm que invoca
        /// </summary>
        public NewProjectForm newProjectForm;

        Point lastClick;

        #endregion

        public PhantomsForm()
        {
            InitializeComponent();
        }

        private void PhantomsForm_Load(object sender, EventArgs e)
        {
            // se agrega transparencia a las series
            chartHigh.Series[0].Color = Color.FromArgb(126, Color.Blue);
            chartHigh.Series[1].Color = Color.FromArgb(126, Color.Green);
            chartHigh.Series[2].Color = Color.FromArgb(126, Color.Red);
            chartLow.Series[0].Color = Color.FromArgb(126, Color.Blue);
            chartLow.Series[1].Color = Color.FromArgb(126, Color.Green);
            chartLow.Series[2].Color = Color.FromArgb(126, Color.Red);

            // se toman los valores de los phantoms desde el NewProjectForm
            numMeanHighP1.Value = (decimal)newProjectForm.tempPhantom1.mediaHigh;
            numMeanHighP2.Value = (decimal)newProjectForm.tempPhantom2.mediaHigh;
            numMeanHighP3.Value = (decimal)newProjectForm.tempPhantom3.mediaHigh;

            numMeanLowP1.Value = (decimal)newProjectForm.tempPhantom1.mediaLow;
            numMeanLowP2.Value = (decimal)newProjectForm.tempPhantom2.mediaLow;
            numMeanLowP3.Value = (decimal)newProjectForm.tempPhantom3.mediaLow;

            numDesvHighP1.Value = (decimal)newProjectForm.tempPhantom1.desvHigh;
            numDesvHighP2.Value = (decimal)newProjectForm.tempPhantom2.desvHigh;
            numDesvHighP3.Value = (decimal)newProjectForm.tempPhantom3.desvHigh;

            numDesvLowP1.Value = (decimal)newProjectForm.tempPhantom1.desvLow;
            numDesvLowP2.Value = (decimal)newProjectForm.tempPhantom2.desvLow;
            numDesvLowP3.Value = (decimal)newProjectForm.tempPhantom3.desvLow;

            numDensP1.Value = (decimal)newProjectForm.tempPhantom1.densidad;
            numDensP2.Value = (decimal)newProjectForm.tempPhantom2.densidad;
            numDensP3.Value = (decimal)newProjectForm.tempPhantom3.densidad;

            numZeffP1.Value = (decimal)newProjectForm.tempPhantom1.zeff;
            numZeffP2.Value = (decimal)newProjectForm.tempPhantom2.zeff;
            numZeffP3.Value = (decimal)newProjectForm.tempPhantom3.zeff;

            GenerarCurvas();
        }

        private void btnSubir_MouseEnter(object sender, EventArgs e)
        {
            ((System.Windows.Forms.Button)(sender)).ForeColor = Color.White;
        }

        private void btnSubir_MouseLeave(object sender, EventArgs e)
        {
            ((System.Windows.Forms.Button)(sender)).ForeColor = Color.Black;
        }

        private void PhantomsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.padre.CerrarPhantomForm();
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

        /// <summary>
        /// Se guardan los cambios y se cierra el Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnCerrar_Click(object sender, EventArgs e)
        {
            newProjectForm.tempPhantom1.mediaHigh = (double)numMeanHighP1.Value;
            newProjectForm.tempPhantom2.mediaHigh = (double)numMeanHighP2.Value;
            newProjectForm.tempPhantom3.mediaHigh = (double)numMeanHighP3.Value;

            newProjectForm.tempPhantom1.mediaLow = (double)numMeanLowP1.Value;
            newProjectForm.tempPhantom2.mediaLow = (double)numMeanLowP2.Value;
            newProjectForm.tempPhantom3.mediaLow = (double)numMeanLowP3.Value;

            newProjectForm.tempPhantom1.desvHigh = (double)numDesvHighP1.Value;
            newProjectForm.tempPhantom2.desvHigh = (double)numDesvHighP2.Value;
            newProjectForm.tempPhantom3.desvHigh = (double)numDesvHighP3.Value;

            newProjectForm.tempPhantom1.desvLow = (double)numDesvLowP1.Value;
            newProjectForm.tempPhantom2.desvLow = (double)numDesvLowP2.Value;
            newProjectForm.tempPhantom3.desvLow = (double)numDesvLowP3.Value;

            newProjectForm.tempPhantom1.densidad = (double)numDensP1.Value;
            newProjectForm.tempPhantom2.densidad = (double)numDensP2.Value;
            newProjectForm.tempPhantom3.densidad = (double)numDensP3.Value;

            newProjectForm.tempPhantom1.zeff = (double)numZeffP1.Value;
            newProjectForm.tempPhantom2.zeff = (double)numZeffP2.Value;
            newProjectForm.tempPhantom3.zeff = (double)numZeffP3.Value;

            this.Close();
        }

        private void GenerarCurvas()
        {
            List<double> campanay = null;
            List<double> campanax = null;
            double mean;
            double desv;
            double minimo;
            double maximo;

            // Phantom 1 High
            mean = Convert.ToDouble(numMeanHighP1.Value);
            desv = Convert.ToDouble(numDesvHighP1.Value);
            campanax = GenerarCampanaX(mean, desv);
            campanay = GenerarCampanaY(mean, desv, campanax);
            chartHigh.Series[0].Points.Clear();
            for (int i = 0; i < campanay.Count; i++)
                chartHigh.Series[0].Points.AddXY(campanax[i], campanay[i]);
            minimo = campanax.Min();
            maximo = campanax.Max();

            // Phantom 2 High
            mean = Convert.ToDouble(numMeanHighP2.Value);
            desv = Convert.ToDouble(numDesvHighP2.Value);
            campanax = GenerarCampanaX(mean, desv);
            campanay = GenerarCampanaY(mean, desv, campanax);
            chartHigh.Series[1].Points.Clear();
            for (int i = 0; i < campanay.Count; i++)
                chartHigh.Series[1].Points.AddXY(campanax[i], campanay[i]);
            if(campanax.Min()<minimo)
                minimo = campanax.Min();
            if (campanax.Max() > maximo)
                maximo = campanax.Max();

            // Phantom 3 High
            mean = Convert.ToDouble(numMeanHighP3.Value);
            desv = Convert.ToDouble(numDesvHighP3.Value);
            campanax = GenerarCampanaX(mean, desv);
            campanay = GenerarCampanaY(mean, desv, campanax);
            chartHigh.Series[2].Points.Clear();
            for (int i = 0; i < campanay.Count; i++)
                chartHigh.Series[2].Points.AddXY(campanax[i], campanay[i]);
            if (campanax.Min() < minimo)
                minimo = campanax.Min();
            if (campanax.Max() > maximo)
                maximo = campanax.Max();

            // Phantom 1 Low
            mean = Convert.ToDouble(numMeanLowP1.Value);
            desv = Convert.ToDouble(numDesvLowP1.Value);
            campanax = GenerarCampanaX(mean, desv);
            campanay = GenerarCampanaY(mean, desv, campanax);
            chartLow.Series[0].Points.Clear();
            for (int i = 0; i < campanay.Count; i++)
                chartLow.Series[0].Points.AddXY(campanax[i], campanay[i]);
            if (campanax.Min() < minimo)
                minimo = campanax.Min();
            if (campanax.Max() > maximo)
                maximo = campanax.Max();

            // Phantom 2 Low
            mean = Convert.ToDouble(numMeanLowP2.Value);
            desv = Convert.ToDouble(numDesvLowP2.Value);
            campanax = GenerarCampanaX(mean, desv);
            campanay = GenerarCampanaY(mean, desv, campanax);
            chartLow.Series[1].Points.Clear();
            for (int i = 0; i < campanay.Count; i++)
                chartLow.Series[1].Points.AddXY(campanax[i], campanay[i]);
            if (campanax.Min() < minimo)
                minimo = campanax.Min();
            if (campanax.Max() > maximo)
                maximo = campanax.Max();

            // Phantom 3 Low
            mean = Convert.ToDouble(numMeanLowP3.Value);
            desv = Convert.ToDouble(numDesvLowP3.Value);
            campanax = GenerarCampanaX(mean, desv);
            campanay = GenerarCampanaY(mean, desv, campanax);
            chartLow.Series[2].Points.Clear();
            for (int i = 0; i < campanay.Count; i++)
                chartLow.Series[2].Points.AddXY(campanax[i], campanay[i]);
            if (campanax.Min() < minimo)
                minimo = campanax.Min();
            if (campanax.Max() > maximo)
                maximo = campanax.Max();

            chartHigh.ChartAreas[0].AxisX.Minimum = minimo;
            chartHigh.ChartAreas[0].AxisX.Maximum = maximo;
            chartLow.ChartAreas[0].AxisX.Minimum = minimo;
            chartLow.ChartAreas[0].AxisX.Maximum = maximo;
        }

        /// <summary>
        /// Genera un vector de 101 elementos con los elementos Y que forman una campana de Gauss
        /// </summary>
        /// <param name="media">Valor de la media de la distribucion normal</param>
        /// <param name="desv">Valor de la desvStd de la distribucion normal</param>
        /// <returns></returns>
        private List<double> GenerarCampanaY(double media, double desv, List<double> x)
        {
            List<double> campanay = new List<double>();

            double a=1/(desv*Math.Sqrt(2*Math.PI));
            double b;

            for (int i = 0; i < 101; i++)
            {
                b = Math.Pow((x[i] - media) / desv, 2) * -0.5;
                campanay.Add(a * Math.Exp(b));                
            }

            return campanay;
        }

        private List<double> GenerarCampanaX(double media, double desv)
        {
            List<double> campanax = new List<double>();

            double step = 8 * desv / 100;
            double valor = media - 4 * desv;

            for (int i = 0; i < 101; i++)
            {
                campanax.Add(valor);
                valor = valor + step;
            }

            return campanax;
        }

        private void numMeanHighP1_ValueChanged(object sender, EventArgs e)
        {
            GenerarCurvas();
        }

        /// <summary>
        /// Se centra el Form con respecto al MDI parent
        /// </summary>
        public void CentrarForm()
        {
            //this.Location = new System.Drawing.Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2, (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
            this.Location = new System.Drawing.Point((MdiParent.Width - this.Width) / 2, (int)((MdiParent.Height - this.Height) * 0.8 / 2));
        }

        private void lblTitle_DoubleClick(object sender, EventArgs e)
        {
            CentrarForm();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PhantomsForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.Green, 2), this.DisplayRectangle);       
        }     
    }
}
