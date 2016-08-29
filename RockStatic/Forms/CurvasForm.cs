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
    /// Ventana que presenta las curvas de densidad y numero atomico efectivo estimadas a partir de la data CT cargada
    /// </summary>
    public partial class CurvasForm : Form
    {
        #region variables de clase

        Point lastClick;

        /// <summary>
        /// Referencia al MainForm
        /// </summary>
        public MainForm padre;

        /// <summary>
        /// Variable intermedia para el calculo de propiedades estaticas
        /// </summary>
        public double[] Dfm;

        /// <summary>
        /// Variable intermedia para el calculo de propiedades estaticas
        /// </summary>
        public double[] Zfme;

        /// <summary>
        /// Variable intermedia para el calculo de propiedades estaticas
        /// </summary>
        public double[] Pefm;

        /// <summary>
        /// Vector con los valores de profundidad para las propiedades estaticas estimadas
        /// </summary>
        public double[] profundidad;

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

        /// <summary>
        /// Se estiman las propiedades estáticas en función de la profundidad del core 
        /// </summary>
        public void Estimar()
        {
            // se toma la informacion de segmentacion vs areas de interes y se estiman las propiedades petrofisicas estaticas

            // se preparan los generadores de CT aleatorios para cada phantom
            var phantom1High = new MathNet.Numerics.Distributions.Normal(padre.actual.phantom1.mediaHigh, padre.actual.phantom1.desvHigh);
            var phantom1Low = new MathNet.Numerics.Distributions.Normal(padre.actual.phantom1.mediaLow, padre.actual.phantom1.desvLow);

            var phantom2High = new MathNet.Numerics.Distributions.Normal(padre.actual.phantom2.mediaHigh, padre.actual.phantom2.desvHigh);
            var phantom2Low = new MathNet.Numerics.Distributions.Normal(padre.actual.phantom2.mediaLow, padre.actual.phantom2.desvLow);

            var phantom3High = new MathNet.Numerics.Distributions.Normal(padre.actual.phantom3.mediaHigh, padre.actual.phantom3.desvHigh);
            var phantom3Low = new MathNet.Numerics.Distributions.Normal(padre.actual.phantom3.mediaLow, padre.actual.phantom3.desvLow);

            // se prepara un vector de valores CT high y low para cada phantom
            // este vector representa un slide y solo se guarda un valor promedio del slide
            double[] temp = new double[padre.actual.datacuboHigh.dataCube[0].segCore.Count];
            MathNet.Numerics.Statistics.DescriptiveStatistics stats;

            // se preparan los vectores para densidad y zeff
            this.Dfm = new double[padre.actual.datacuboHigh.dataCube.Count];
            this.Zfme = new double[padre.actual.datacuboHigh.dataCube.Count];
            this.Pefm = new double[padre.actual.datacuboHigh.dataCube.Count];

            double ctP1High, ctP2High, ctP3High, ctP1Low, ctP2Low, ctP3Low;
            double A, B, C, D, E, F;

            List<double> Df, Zf, Zeff, Pef;

            int iarea;

            // se empieza a recorrer cada slide que se encuentre dentro de las areas de interes
            // para cada slide se genera una colección de datos aleatorios simulando cada phantom, y se estima la media para cada phantom en cada slide
            bool slide = false;
            for (int i = 0; i < padre.actual.datacuboHigh.dataCube.Count; i++)
            {
                slide = false;
                iarea = -1;

                for (int j = 0; j < padre.actual.areasCore.Count; j++)
                {
                    // se busca si este slide esta dentro de al menos un area de interes
                    if ((i >= padre.actual.areasCore[j].ini) & (i <= padre.actual.areasCore[j].fin))
                    {
                        slide = true;
                        iarea = j;
                    }
                }

                if (slide)
                {
                    // el slide pertenece al menos a un area de interes, por tanto se procede a calcular la densidad y zeff para este slide

                    // se generan los valores CT para cada phantom y se toma su media
                    phantom1High.Samples(temp);
                    stats = new MathNet.Numerics.Statistics.DescriptiveStatistics(temp);
                    ctP1High = stats.Mean;

                    phantom2High.Samples(temp);
                    stats = new MathNet.Numerics.Statistics.DescriptiveStatistics(temp);
                    ctP2High = stats.Mean;

                    phantom3High.Samples(temp);
                    stats = new MathNet.Numerics.Statistics.DescriptiveStatistics(temp);
                    ctP3High = stats.Mean;

                    phantom1Low.Samples(temp);
                    stats = new MathNet.Numerics.Statistics.DescriptiveStatistics(temp);
                    ctP1Low = stats.Mean;

                    phantom2Low.Samples(temp);
                    stats = new MathNet.Numerics.Statistics.DescriptiveStatistics(temp);
                    ctP2Low = stats.Mean;

                    phantom3Low.Samples(temp);
                    stats = new MathNet.Numerics.Statistics.DescriptiveStatistics(temp);
                    ctP3Low = stats.Mean;

                    // se resuelve el sistema lineal para obtener las constantes A,B,C,D,E,F
                    var matriz = MathNet.Numerics.LinearAlgebra.Matrix<double>.Build.DenseOfArray(new double[,] { { ctP1Low, ctP1High, 1 }, { ctP2Low, ctP2High, 1 }, { ctP3Low, ctP3High, 1 } });
                    var sol = MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Dense(new double[] { padre.actual.phantom1.densidad, padre.actual.phantom2.densidad, padre.actual.phantom3.densidad });
                    var x = matriz.Solve(sol);

                    A = x[0];
                    B = x[1];
                    C = x[2];

                    sol = MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Dense(new double[] { padre.actual.phantom1.zeff, padre.actual.phantom2.zeff, padre.actual.phantom3.zeff });
                    x = matriz.Solve(sol);

                    D = x[0];
                    E = x[1];
                    F = x[2];

                    // se empieza a recorrer cada voxel, en la segmentacion del actual i-slide, se revisa que este dentro del area de interes

                    Df = new List<double>();
                    Zf = new List<double>();
                    Zeff = new List<double>();
                    Pef = new List<double>();

                    int jkindex = 0;
                    double dx;
                    double dy;

                    double tDf, tZf, tZeff, tPef;

                    // dado que se recorre fila a fila, entonces el indice j corresponde al eje Y y el indice k al eje X
                    for (int j = 0; j < padre.actual.datacuboHigh.widthSeg; j++)
                    {
                        for (int k = 0; k < padre.actual.datacuboHigh.widthSeg; k++)
                        {
                            // se calcula la distancia de la posicion (j,k) al centro del area de interes
                            // si la distancia es menor que el radio entonces se agrega al calculo, sino no

                            dx = k - padre.actual.areasCore[iarea].x;
                            dx = dx * dx;
                            dy = j - padre.actual.areasCore[iarea].y;
                            dy = dy * dy;
                            if (Math.Sqrt(dx + dy) <= padre.actual.datacuboHigh.widthSeg)
                            {
                                // la coordenada (j,k) esta dentro del area de interes
                                // se calculan las propiedades estaticas

                                tDf = A * padre.actual.datacuboLow.dataCube[i].segCore[jkindex] + B * padre.actual.datacuboHigh.dataCube[i].segCore[jkindex] + C;
                                Df.Add(tDf);

                                tZf = D * padre.actual.datacuboLow.dataCube[i].segCore[jkindex] + E * padre.actual.datacuboHigh.dataCube[i].segCore[jkindex] + F;
                                Zf.Add(tZf);

                                //tZeff = Math.Pow(Math.Pow((tZf / (0.9342 * tDf + 0.1759)), 10), 1 / 36);
                                tZeff = Math.Pow((tZf / (0.9342 * tDf + 0.1759)), 1/3.6);
                                Zeff.Add(tZeff);

                                tPef = Math.Pow(Math.Pow((tZeff / 10), 36), 0.1);
                                Pef.Add(tPef);
                            }


                            jkindex++;
                        }
                    }

                    stats = new MathNet.Numerics.Statistics.DescriptiveStatistics(Df);
                    Dfm[i] = stats.Mean;

                    stats = new MathNet.Numerics.Statistics.DescriptiveStatistics(Zeff);
                    Zfme[i] = stats.Mean;

                    stats = new MathNet.Numerics.Statistics.DescriptiveStatistics(Pef);
                    Pefm[i] = stats.Mean;
                }
                else
                {
                    // se llenan los vectores de densidad y zeff con valores -1... si el valor es -1 entonces no se grafica
                    this.Dfm[i] = -1;
                    this.Zfme[i] = -1;
                    this.Pefm[i] = -1;
                }
            }

            DateTime fin = DateTime.Now;
        }

        private void CurvasForm_Load(object sender, EventArgs e)
        {
            // se preparan los valores de profundidad
            profundidad = new double[padre.actual.datacuboHigh.dataCube.Count];
            double paso = Convert.ToDouble(padre.actual.tail - padre.actual.head) / Convert.ToDouble(profundidad.Length - 1);

            for (int i = 0; i < profundidad.Length;i++) profundidad[i]=padre.actual.head+(i*paso);
            
            // se preparan los nombres de los ejes
            chart1.ChartAreas[0].AxisY.Title="PROFUNDIDAD (" + padre.actual.unidadProfundidad + ")";
            chart2.ChartAreas[0].AxisY.Title="PROFUNDIDAD (" + padre.actual.unidadProfundidad + ")";
            
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "#.##";
            chart2.ChartAreas[0].AxisX.LabelStyle.Format = "#.##";

            List<double> D = new List<double>();
            List<double> Z = new List<double>();
            // se cargan los puntos en el chart
            for (int i = 0; i < padre.actual.datacuboHigh.dataCube.Count;i++)
            {
                if (Dfm[i] > -1)
                {
                    chart1.Series[0].Points.AddXY(Dfm[i], profundidad[i]);
                    D.Add(Dfm[i]);
                }
                if (Zfme[i] > -1)
                {
                    chart2.Series[0].Points.AddXY(Zfme[i], profundidad[i]);
                    Z.Add(Zfme[i]);
                }
            }

            // se modifican los intervalos del eje
            chart1.ChartAreas[0].AxisX.Minimum = chart1.ChartAreas[0].AxisX2.Minimum=D.Min()*0.99;
            chart1.ChartAreas[0].AxisX.Maximum = chart1.ChartAreas[0].AxisX2.Maximum=D.Max()*1.01;
            chart2.ChartAreas[0].AxisX.Minimum = chart2.ChartAreas[0].AxisX2.Minimum = Z.Min() * 0.99;
            chart2.ChartAreas[0].AxisX.Maximum = chart2.ChartAreas[0].AxisX2.Maximum=Z.Max()*1.01;

            chart1.ChartAreas[0].AxisX.Interval = chart1.ChartAreas[0].AxisX2.Interval = (chart1.ChartAreas[0].AxisX.Maximum - chart1.ChartAreas[0].AxisX.Minimum) / 4;
            chart2.ChartAreas[0].AxisX.Interval = chart2.ChartAreas[0].AxisX2.Interval = (chart2.ChartAreas[0].AxisX.Maximum - chart2.ChartAreas[0].AxisX.Minimum) / 4;
        }        

        private void btnExportar_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.FileName = padre.actual.name;
            saveFile.FilterIndex = 1;
            saveFile.RestoreDirectory = true;

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                chart1.SaveImage(saveFile.FileName + "-densidad.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                chart2.SaveImage(saveFile.FileName + "-zeff.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

                System.IO.StreamWriter sw = new System.IO.StreamWriter(saveFile.FileName + ".txt");
                sw.WriteLine("PROFUNDIDAD\tDENSIDA\tZEFF");
                for (int i = 0; i < profundidad.Length; i++) sw.WriteLine(profundidad[i] + "\t" + Dfm[i] + "\t" + Zfme[i]);
                sw.Close();
            }
        }
    }
}
