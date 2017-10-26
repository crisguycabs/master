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
        /// Variable para el almacenar los valores de Peff
        /// </summary>
        public List<double> PEF = new List<double>();

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

            List<double> meanP1high = new List<double>();
            List<double> desvP1high = new List<double>();
            List<double> meanP2high = new List<double>();
            List<double> desvP2high = new List<double>();
            List<double> meanP3high = new List<double>();
            List<double> desvP3high = new List<double>();

            List<double> meanP1low = new List<double>();
            List<double> desvP1low = new List<double>();
            List<double> meanP2low = new List<double>();
            List<double> desvP2low = new List<double>();
            List<double> meanP3low = new List<double>();
            List<double> desvP3low = new List<double>();

            List<double> meanCorehigh = new List<double>();
            List<double> meanCorelow = new List<double>();
            List<double> desvCorehigh = new List<double>();
            List<double> desvCorelow = new List<double>();


            if (padre.actual.phantomEnDicom)
            {
                // existe informacion de phantoms en los dicom, se toma la info de la segmentacion transversal

                // para el phantom1 High
                for (int i = 0; i < padre.actual.datacuboHigh.dataCube.Count; i++)
                {
                    List<double> numerosCT = new List<double>();
                    for (int j = 0; j < padre.actual.datacuboHigh.dataCube[i].segPhantom1.Count; j++)
                    {
                        if (padre.actual.datacuboHigh.dataCube[i].segPhantom1[j] > 0) numerosCT.Add((double)padre.actual.datacuboHigh.dataCube[i].segPhantom1[j]);
                    }

                    meanP1high.Add(MathNet.Numerics.Statistics.Statistics.Mean(numerosCT));
                    desvP1high.Add(MathNet.Numerics.Statistics.Statistics.StandardDeviation(numerosCT));
                }

                // para el phantom1 Low                
                for (int i = 0; i < padre.actual.datacuboLow.dataCube.Count; i++)
                {
                    List<double> numerosCT = new List<double>();
                    for (int j = 0; j < padre.actual.datacuboLow.dataCube[i].segPhantom1.Count; j++)
                    {
                        if (padre.actual.datacuboLow.dataCube[i].segPhantom1[j] > 0) numerosCT.Add((double)padre.actual.datacuboLow.dataCube[i].segPhantom1[j]);
                    }

                    meanP1low.Add(MathNet.Numerics.Statistics.Statistics.Mean(numerosCT));
                    desvP1low.Add(MathNet.Numerics.Statistics.Statistics.StandardDeviation(numerosCT));
                }

                // para el phantom2 High

                for (int i = 0; i < padre.actual.datacuboHigh.dataCube.Count; i++)
                {
                    List<double> numerosCT = new List<double>();
                    for (int j = 0; j < padre.actual.datacuboHigh.dataCube[i].segPhantom2.Count; j++)
                    {
                        if (padre.actual.datacuboHigh.dataCube[i].segPhantom2[j] > 0) numerosCT.Add((double)padre.actual.datacuboHigh.dataCube[i].segPhantom2[j]);
                    }

                    meanP2high.Add(MathNet.Numerics.Statistics.Statistics.Mean(numerosCT));
                    desvP2high.Add(MathNet.Numerics.Statistics.Statistics.StandardDeviation(numerosCT));
                }
                // para el phantom2 Low

                for (int i = 0; i < padre.actual.datacuboLow.dataCube.Count; i++)
                {
                    List<double> numerosCT = new List<double>();
                    for (int j = 0; j < padre.actual.datacuboLow.dataCube[i].segPhantom2.Count; j++)
                    {
                        if (padre.actual.datacuboLow.dataCube[i].segPhantom2[j] > 0) numerosCT.Add((double)padre.actual.datacuboLow.dataCube[i].segPhantom2[j]);
                    }

                    meanP2low.Add(MathNet.Numerics.Statistics.Statistics.Mean(numerosCT));
                    desvP2low.Add(MathNet.Numerics.Statistics.Statistics.StandardDeviation(numerosCT));
                }

                // para el phantom3 High

                for (int i = 0; i < padre.actual.datacuboHigh.dataCube.Count; i++)
                {
                    List<double> numerosCT = new List<double>();
                    for (int j = 0; j < padre.actual.datacuboHigh.dataCube[i].segPhantom3.Count; j++)
                    {
                        if (padre.actual.datacuboHigh.dataCube[i].segPhantom3[j] > 0) numerosCT.Add((double)padre.actual.datacuboHigh.dataCube[i].segPhantom3[j]);
                    }

                    meanP3high.Add(MathNet.Numerics.Statistics.Statistics.Mean(numerosCT));
                    desvP3high.Add(MathNet.Numerics.Statistics.Statistics.StandardDeviation(numerosCT));
                }
                // para el phantom3 Low

                for (int i = 0; i < padre.actual.datacuboLow.dataCube.Count; i++)
                {
                    List<double> numerosCT = new List<double>();
                    for (int j = 0; j < padre.actual.datacuboLow.dataCube[i].segPhantom3.Count; j++)
                    {
                        if (padre.actual.datacuboLow.dataCube[i].segPhantom3[j] > 0) numerosCT.Add((double)padre.actual.datacuboLow.dataCube[i].segPhantom3[j]);
                    }

                    meanP3low.Add(MathNet.Numerics.Statistics.Statistics.Mean(numerosCT));
                    desvP3low.Add(MathNet.Numerics.Statistics.Statistics.StandardDeviation(numerosCT));
                }
            }
            else
            {
                // se preparan los generadores de CT aleatorios para cada phantom
                for (int i = 0; i < padre.actual.datacuboLow.dataCube.Count; i++)
                {
                    meanP1high.Add(padre.actual.phantom1.mediaHigh);
                    desvP1high.Add(padre.actual.phantom1.desvHigh);
                    meanP2high.Add(padre.actual.phantom2.mediaHigh);
                    desvP2high.Add(padre.actual.phantom2.desvHigh);
                    meanP3high.Add(padre.actual.phantom3.mediaHigh);
                    desvP3high.Add(padre.actual.phantom3.desvHigh);

                    meanP1low.Add(padre.actual.phantom1.mediaLow);
                    desvP1low.Add(padre.actual.phantom1.desvLow);
                    meanP2low.Add(padre.actual.phantom2.mediaLow);
                    desvP2low.Add(padre.actual.phantom2.desvLow);
                    meanP3low.Add(padre.actual.phantom3.mediaLow);
                    desvP3low.Add(padre.actual.phantom3.desvLow);
                }
                                
            }

            // se preparan los vectores para densidad y zeff
            this.Dfm = new double[padre.actual.datacuboHigh.dataCube.Count];
            this.Zfme = new double[padre.actual.datacuboHigh.dataCube.Count];
            this.Pefm = new double[padre.actual.datacuboHigh.dataCube.Count];

            double ctP1High, ctP2High, ctP3High, ctP1Low, ctP2Low, ctP3Low;
            double A, B, C, DE, D, DF;

            List<double> Df, Zf, Zeff, Pef;
            int jkindex = 0;
            int iarea;

            // se empieza a recorrer cada slide que se encuentre dentro de las areas de interes
            // para cada slide se genera una colección de datos aleatorios simulando cada phantom, y se estima la media para cada phantom en cada slide
            bool slide = false;
            for (int i = 0; i < padre.actual.datacuboHigh.dataCube.Count; i++)
            {
                slide = false;
                iarea = -1;

                for (int j = 0; j < padre.actual.areasInteresCore.Count; j++)
                {
                    // se busca si este slide esta dentro de al menos un area de interes
                    if ((i >= padre.actual.areasInteresCore[j].ini) & (i <= padre.actual.areasInteresCore[j].fin))
                    {
                        slide = true;
                        iarea = j;
                    }
                }

                if (slide)
                {
                    // el slide pertenece al menos a un area de interes, por tanto se procede a calcular la densidad y zeff para este slide

                    ctP1High = meanP1high[i];
                    ctP1Low = meanP1low[i];
                    ctP2High = meanP2high[i];
                    ctP2Low = meanP2low[i];
                    ctP3High = meanP3high[i];
                    ctP3Low = meanP3low[i];
                    


                    // se resuelve el sistema lineal para obtener las constantes A,B,C,D,E,F
                    var matriz = MathNet.Numerics.LinearAlgebra.Matrix<double>.Build.DenseOfArray(new double[,] { { ctP1Low, -ctP1High, 1 }, { ctP2Low, -ctP2High, 1 }, { ctP3Low, -ctP3High, 1 } });
                    //var matriz = MathNet.Numerics.LinearAlgebra.Matrix<double>.Build.DenseOfArray(new double[,] { { ctP1Low, -ctP1High, 1 }, { ctP2Low, -ctP2High, 1 }, { ctP3Low, -ctP3High, 1 } });
                    var sol = MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Dense(new double[] { padre.actual.phantom1.densidad, padre.actual.phantom2.densidad, padre.actual.phantom3.densidad });
                    var x = matriz.Solve(sol);

                    A = x[0];
                    B = x[1];
                    C = x[2];

                    var matriz2 = MathNet.Numerics.LinearAlgebra.Matrix<double>.Build.DenseOfArray(new double[,] { { ctP1Low, -ctP1High, -1 }, { ctP2Low, -ctP2High, -1 }, { ctP3Low, -ctP3High, -1 } });
                    //var matriz = MathNet.Numerics.LinearAlgebra.Matrix<double>.Build.DenseOfArray(new double[,] { { ctP1Low, -ctP1High, 1 }, { ctP2Low, -ctP2High, 1 }, { ctP3Low, -ctP3High, 1 } });
                    var sol2 = MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Dense(new double[] { Math.Pow(padre.actual.phantom1.zeff,3.6)* padre.actual.phantom1.densidad, Math.Pow(padre.actual.phantom2.zeff, 3.6) * padre.actual.phantom2.densidad, Math.Pow(padre.actual.phantom3.zeff, 3.6) * padre.actual.phantom3.densidad });
                    var x2 = matriz.Solve(sol);

                    DE = x2[0];
                    D = x2[1];
                    DF = x2[2];
                    
                    // se empieza a recorrer cada voxel, en la segmentacion del actual i-slide, se revisa que este dentro del area de interes

                    Df = new List<double>();
                    Zf = new List<double>();
                    Zeff = new List<double>();
                    Pef = new List<double>();


                    
                    double tDf, tZf, tZeff, tPef, pb,dens;

                    // dado que se recorre fila a fila, entonces el indice j corresponde al eje Y y el indice k al eje X
                        List<double> numerosCTlow = new List<double>();
                        List<double> numerosCThigh = new List<double>();
                        for (int k = 0; k < padre.actual.datacuboHigh.widthSegCore; k++)
                        {
                            // se calcula la media para ca core
                            if (padre.actual.datacuboHigh.dataCube[i].segCore[k] > 0) numerosCThigh.Add((double)padre.actual.datacuboHigh.dataCube[i].segCore[k]);
                            if (padre.actual.datacuboLow.dataCube[i].segCore[k] > 0) numerosCTlow.Add((double)padre.actual.datacuboLow.dataCube[i].segCore[k]);
                            
                        }

                        meanCorehigh.Add(MathNet.Numerics.Statistics.Statistics.Mean(numerosCThigh));
                        desvCorehigh.Add(MathNet.Numerics.Statistics.Statistics.StandardDeviation(numerosCThigh));

                        meanCorelow.Add(MathNet.Numerics.Statistics.Statistics.Mean(numerosCTlow));
                        desvCorelow.Add(MathNet.Numerics.Statistics.Statistics.StandardDeviation(numerosCTlow));

                        tDf = A * meanCorelow[jkindex]  - B * meanCorehigh[jkindex] + C;
                                //Df.Add(tDf);

                                //pb=(1.0704 * tDf - 0.1883);
                                      //dens = 0.9342 * pb + 0.1759;
                                tZf = Math.Pow(((DE * meanCorelow[jkindex] - D * meanCorehigh[jkindex] - DF)/ tDf),1/3.6);
                                //Zf.Add(tZf);
                                //tZeff = Math.Pow(Math.Pow((tZf / (0.9342 * tDf + 0.1759)), 10), 1 / 36);
                               // tZeff = Math.Pow((tZf / dens), 1/3.6)*10;
                                //Zeff.Add(tZeff);

                                tPef = Math.Pow((tZf / 10), 3.6);
                                //Pef.Add(tPef);
                              
                        
                    Dfm[jkindex] = tDf;
                    Zfme[jkindex] = tZf;
                    Pefm[jkindex] = tPef;
                    jkindex++;
                    }
                    // stats = new MathNet.Numerics.Statistics.DescriptiveStatistics(Df);
                    // Dfm[i] = stats.Mean;

                    // stats = new MathNet.Numerics.Statistics.DescriptiveStatistics(Zeff);
                    // Zfme[i] = stats.Mean;

                    //stats = new MathNet.Numerics.Statistics.DescriptiveStatistics(Pef);
                    // Pefm[i] = stats.Mean;
                
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
            chart3.ChartAreas[0].AxisY.Title = "PROFUNDIDAD (" + padre.actual.unidadProfundidad + ")";
            
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "#.##";
            chart2.ChartAreas[0].AxisX.LabelStyle.Format = "#.##";
            // chart3.ChartAreas[0].AxisX.LabelStyle.Format = "#.#######";
            //chart3.ChartAreas[0].AxisX.LabelStyle.Format = "#.##D+0";
            chart3.ChartAreas[0].AxisX.LabelStyle.Format = "#.##";

            List<double> D = new List<double>();
            List<double> Z = new List<double>();
            List<double> P = new List<double>();


            PEF = new List<double>();            
            // se cargan los puntos en el chart
            for (int i = 0; i < padre.actual.datacuboHigh.dataCube.Count;i++)
            {
                PEF.Add((Math.Pow(Zfme[i] / Convert.ToDouble(10), 3.6)));
                if (Dfm[i] > -1)
                {
                    chart1.Series[0].Points.AddXY(Dfm[i], profundidad[i]);
                    D.Add(Dfm[i]);
                }
                if (Zfme[i] > -1)
                {
                    chart2.Series[0].Points.AddXY(Zfme[i], profundidad[i]);
                    Z.Add(Zfme[i]);


                    chart3.Series[0].Points.AddXY((Math.Pow(Zfme[i] / Convert.ToDouble(10), 3.6)), profundidad[i]);
                    P.Add((Math.Pow(Zfme[i] / Convert.ToDouble(10), 3.6)));
                }
                
            }

            chart2.Series[0].Color = Color.Red;
            chart3.Series[0].Color = Color.DarkGreen;

            if (D.Count < 1) D.Add(0);
            if (Z.Count < 1) Z.Add(0);
            if (P.Count < 1) P.Add(0);

            // se modifican los intervalos del eje
            chart1.ChartAreas[0].AxisX.Minimum = chart1.ChartAreas[0].AxisX2.Minimum=D.Min()*0.95;
            chart1.ChartAreas[0].AxisX.Maximum = chart1.ChartAreas[0].AxisX2.Maximum=D.Max()*1.05;
            chart2.ChartAreas[0].AxisX.Minimum = chart2.ChartAreas[0].AxisX2.Minimum = Z.Min() * 0.95;
            chart2.ChartAreas[0].AxisX.Maximum = chart2.ChartAreas[0].AxisX2.Maximum=Z.Max()*1.05;

            chart3.ChartAreas[0].AxisX.Minimum = chart3.ChartAreas[0].AxisX2.Minimum = P.Min() * 0.95;
            chart3.ChartAreas[0].AxisX.Maximum = chart3.ChartAreas[0].AxisX2.Maximum = P.Max() * 1.05;


            chart1.ChartAreas[0].AxisX.Interval = chart1.ChartAreas[0].AxisX2.Interval = (chart1.ChartAreas[0].AxisX.Maximum - chart1.ChartAreas[0].AxisX.Minimum) / 4;
            chart2.ChartAreas[0].AxisX.Interval = chart2.ChartAreas[0].AxisX2.Interval = (chart2.ChartAreas[0].AxisX.Maximum - chart2.ChartAreas[0].AxisX.Minimum) / 4;

            chart3.ChartAreas[0].AxisX.Interval = chart3.ChartAreas[0].AxisX2.Interval = (chart3.ChartAreas[0].AxisX.Maximum - chart3.ChartAreas[0].AxisX.Minimum) / 4;

            chart1.ChartAreas[0].AxisY.Minimum = profundidad[0];
            chart1.ChartAreas[0].AxisY.Maximum = profundidad[profundidad.Length-1];
            chart2.ChartAreas[0].AxisY.Minimum = profundidad[0];
            chart2.ChartAreas[0].AxisY.Maximum = profundidad[profundidad.Length - 1];

            chart3.ChartAreas[0].AxisY.Minimum = profundidad[0];
            chart3.ChartAreas[0].AxisY.Maximum = profundidad[profundidad.Length - 1];

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
                chart3.SaveImage(saveFile.FileName + "-peff.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

                System.IO.StreamWriter sw = new System.IO.StreamWriter(saveFile.FileName + ".txt");
                sw.WriteLine("PROFUNDIDAD\tDENSIDA\tZEFF\tPEFF");
                for (int i = 0; i < profundidad.Length; i++) sw.WriteLine(profundidad[i] + "\t" + Dfm[i] + "\t" + Zfme[i] + "\t" + PEF[i]);
                sw.Close();

                MessageBox.Show("Exportación de resultados exitosa", "Fin de Exportación", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void chart2_MouseClick(object sender, MouseEventArgs e)
        {
            btnExportar.Focus();
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void chart3_Click(object sender, EventArgs e)
        {

        }

        private void chart2_Click(object sender, EventArgs e)
        {

        }
    }
}
