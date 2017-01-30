using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace RockVision
{
    public partial class VisualForm : Form
    {
        #region variales de diseñador

        /// <summary>
        /// Referencia al MainForm padre
        /// </summary>
        public MainForm padre;

        /// <summary>
        /// rango de valores CT de todos los dicom
        /// </summary>
        double rango;

        Point lastClick;

        /// <summary>
        /// Trackbar emergente para la segmentacion
        /// </summary>
        System.Windows.Forms.TrackBar barPopup;

        bool controlValidar = true;

        /// <summary>
        /// serie para el chat
        /// </summary>
        int[] serieAll;

        /// <summary>
        /// serie para el chart
        /// </summary>
        System.Windows.Forms.DataVisualization.Charting.Series serieChartAll;

        int maxValAll = 0;

        /// <summary>
        /// Cuadro de dialogo para los colores
        /// </summary>
        ColorDialog colorDialog1;

        int filaActual = 0;

        int columnaActual = 0;

        List<CUmbralCT> umbral = new List<CUmbralCT>();

        bool popup = false;

        int maxPixelValue;    // Updated July 2012
        int minPixelValue;

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

        public void ResetChart()
        {
            // se hace un duplicado de la serie 0
            System.Windows.Forms.DataVisualization.Charting.Series serie0 = new System.Windows.Forms.DataVisualization.Charting.Series();
            serie0 = chart1.Series[0];

            // se limpian las series
            foreach (var series in chart1.Series)
            {
                series.Points.Clear();
            }
            chart1.Series.Clear();

            // se prepara la serie0
            serie0.Points.AddXY(minPixelValue, 0);
            serie0.Points.AddXY(rangeBar.RangeMinimum, 0);
            serie0.Points.AddXY(rangeBar.RangeMinimum, maxValAll);
            serie0.Points.AddXY(rangeBar.RangeMaximum, maxValAll);
            serie0.Points.AddXY(rangeBar.RangeMaximum, 0);
            serie0.Points.AddXY(maxPixelValue, 0);

            // se vuelve a llenar la serieChartAll
            for (int i = 0; i < serieAll.Length; i++)
            {
                serieChartAll.Points.AddXY(minPixelValue + i, serieAll[i]);
            }

            // se agrega la serie0 y la serieChartAll
            chart1.Series.Add(serie0);
            chart1.Series.Add(serieChartAll);
        }

        /// <summary>
        /// Lee los umbrales en el DataGrid y dibuja los umbrales en el histograma
        /// </summary>
        public void UmbralizarHistograma()
        {
            ResetChart();

            // para cada umbral
            for (int i = 0; i < umbral.Count; i++)
            {
                // failsafe
                if (umbral[i].maximo < umbral[i].minimo) break;

                // se agrega la serie
                chart1.Series.Add(i.ToString());

                // se le da el color a la serie
                chart1.Series[i + 2].Color = umbral[i].color;

                // se llenan los valores
                // se preparan los indices para recorrer la serie All
                int ini = umbral[i].minimo - minPixelValue;
                int fin = umbral[i].maximo - minPixelValue;
                for (int j = ini; j < fin; j++)
                {
                    chart1.Series[i + 2].Points.Add(serieChartAll.Points[j]);
                }
            }
        }

        private void barPopup_Scroll(object sender, EventArgs e)
        {
            controlValidar = false;

            dataGrid.Rows[filaActual].Cells[columnaActual].Value = barPopup.Value;

            if (columnaActual == 0) umbral[filaActual].minimo = barPopup.Value;
            else umbral[filaActual].maximo = barPopup.Value;


            // se revisa que el umbral.maximo anterior coincida con el nuevo valor de minimo, si existe mas de un elemento
            if ((umbral.Count > 1) & (filaActual > 0))
            {
                dataGrid.Rows[filaActual - 1].Cells[1].Value = umbral[filaActual - 1].maximo = umbral[filaActual].minimo;
            }

            // se revisa que el umbral.minimo siguiente coincida con el nuevo valor de maximo, si existe mas de un elemento
            if ((umbral.Count > 1) & (filaActual < (umbral.Count - 1)))
            {
                dataGrid.Rows[filaActual + 1].Cells[0].Value = umbral[filaActual + 1].minimo = umbral[filaActual].maximo;
            }

            UmbralizarHistograma();
            if (chkHabilitar.Checked) picTrans.Image = Umbralizar(trackBar.Value);

            controlValidar = true;
        }

        /// <summary>
        /// Para un dicom en especifico (indice) pinta de un color especifico los valores CT que se encuentran dentro de minimoValor y maximoValor
        /// </summary>
        /// <param name="indice"></param>
        /// <param name="minimoValor"></param>
        /// <param name="maximoValor"></param>
        /// <returns></returns>
        private Bitmap Umbralizar(int indice)
        {
            Bitmap imagen = new Bitmap(padre.actualV.datacubo.dataCube[indice].selector.Columns.Data, padre.actualV.datacubo.dataCube[indice].selector.Rows.Data, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            BitmapData bmd = imagen.LockBits(new Rectangle(0, 0, imagen.Width, imagen.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, imagen.PixelFormat);

            int valorCT;
            int colorOriginal;

            unsafe
            {
                int pixelSize = 3;
                int i, j, j1, i1;
                byte b;

                // se prepara una lista con todos los posibles colores de los intervalos
                List<byte> bR = new List<byte>();
                List<byte> bG = new List<byte>();
                List<byte> bB = new List<byte>();

                for (int k = 0; k < umbral.Count; k++)
                {
                    bR.Add(Convert.ToByte(umbral[k].color.R));
                    bG.Add(Convert.ToByte(umbral[k].color.G));
                    bB.Add(Convert.ToByte(umbral[k].color.B));
                }

                byte nuevoR;
                byte nuevoG;
                byte nuevoB;

                double range = rangeBar.RangeMaximum - rangeBar.RangeMinimum;

                for (i = 0; i < bmd.Height; ++i)
                {
                    byte* row = (byte*)bmd.Scan0 + (i * bmd.Stride);
                    i1 = i * bmd.Width;

                    for (j = 0; j < bmd.Width; ++j)
                    {
                        valorCT = padre.actualV.datacubo.dataCube[indice].pixelData[i * bmd.Width + j];
                        colorOriginal = Convert.ToInt32(Convert.ToDouble(padre.actualV.datacubo.dataCube[indice].pixelData[i * bmd.Width + j] - rangeBar.RangeMinimum) * ((double)255) / range);
                        if (colorOriginal < 0) colorOriginal = 0;
                        if (colorOriginal > 255) colorOriginal = 255;
                        b = Convert.ToByte(colorOriginal);
                        j1 = j * pixelSize;

                        nuevoR = b;
                        nuevoG = b;
                        nuevoB = b;

                        // se recorre la lista de umbrales
                        for (int k = 0; k < umbral.Count; k++)
                        {
                            if ((valorCT >= umbral[k].minimo) & (valorCT <= umbral[k].maximo))
                            {
                                // esta dentro del rango, se repinta ese pixel
                                nuevoR = bR[k];
                                nuevoG = bG[k];
                                nuevoB = bB[k];
                            }
                        }

                        row[j1] = nuevoB;            // Red
                        row[j1 + 1] = nuevoG;        // Green
                        row[j1 + 2] = nuevoR;        // Blue                                                   
                    }
                }
            }
            imagen.UnlockBits(bmd);

            return imagen;
        }

        /// <summary>
        /// Para un dicom en especifico (indice) normaliza a escala de grises los valores CT que se encuentran dentro de minimoValor y maximoValor
        /// </summary>
        /// <param name="indice"></param>
        /// <param name="minimoValor"></param>
        /// <param name="maximoValor"></param>
        /// <returns></returns>
        private Bitmap Normalizar(int indice, int minimoValor, int maximoValor)
        {
            Bitmap imagen = new Bitmap(padre.actualV.datacubo.dataCube[indice].selector.Columns.Data, padre.actualV.datacubo.dataCube[indice].selector.Rows.Data, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            BitmapData bmd = imagen.LockBits(new Rectangle(0, 0, imagen.Width, imagen.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, imagen.PixelFormat);

            // se normaliza de 0 a 255
            double range = maximoValor - minimoValor;
            double color;

            unsafe
            {
                int pixelSize = 3;
                int i, j, j1, i1;
                byte b;

                for (i = 0; i < bmd.Height; ++i)
                {
                    byte* row = (byte*)bmd.Scan0 + (i * bmd.Stride);
                    i1 = i * bmd.Width;

                    for (j = 0; j < bmd.Width; ++j)
                    {
                        color = Convert.ToInt32(Convert.ToDouble(padre.actualV.datacubo.dataCube[indice].pixelData[i * bmd.Width + j] - minimoValor) * ((double)255) / range);
                        if (color < 0) color = 0;
                        if (color > 255) color = 255;
                        b = Convert.ToByte(color);
                        j1 = j * pixelSize;
                        row[j1] = b;            // Red
                        row[j1 + 1] = b;        // Green
                        row[j1 + 2] = b;        // Blue
                    }
                }
            }
            imagen.UnlockBits(bmd);

            return imagen;
        }

        private Bitmap CrearGradiente(int minimoValorRango, int maximoValorRango)
        {

            Bitmap imggrad = new Bitmap(pictGradiente.Width, pictGradiente.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            BitmapData bmdgrad = imggrad.LockBits(new Rectangle(0, 0, pictGradiente.Width, pictGradiente.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, imggrad.PixelFormat);

            unsafe
            {

                int i, j;
                //double rango = maximoValor - minimoValor;
                int p1 = Convert.ToInt32(Math.Round(((double)(minimoValorRango - minPixelValue)) * imggrad.Width / ((double)(maxPixelValue - minPixelValue))));
                int p2 = imggrad.Width - Convert.ToInt32(Math.Round(((double)(maxPixelValue - maximoValorRango)) * imggrad.Width / ((double)(maxPixelValue - minPixelValue))));
                //System.Console.Out.WriteLine("min="+minPixelValue.ToString());
                //System.Console.Out.WriteLine("max=" + maxPixelValue.ToString());

                // version rgb
                int j1;
                int pixelSize = 3;
                byte b;
                double valint;
                double rangogray = 255;
                valint = rangogray / (p2 - p1);
                for (i = 0; i < bmdgrad.Height; ++i)
                {
                    byte* row = (byte*)bmdgrad.Scan0 + (i * bmdgrad.Stride);

                    for (j = 0; j < bmdgrad.Width; ++j)
                    {
                        if (j < p1)
                            b = Convert.ToByte(0);
                        else if (j >= p1 && j <= p2)
                            b = Convert.ToByte(Math.Round(valint * (j - p1)));
                        else
                            b = Convert.ToByte(255);
                        j1 = j * pixelSize;
                        row[j1] = b;
                        row[j1 + 1] = b;
                        row[j1 + 2] = b;
                    }
                }

                // version grayscale
                //short b;
                //short valint;
                //double rangogray = 65535;
                ////ushort* row = (ushort*)bmdgrad.Scan0;
                //valint = Convert.ToInt16(Math.Round(rangogray / (p2 - p1)));
                //for (i = 0; i < bmdgrad.Width; ++i)
                //{
                //    if (i < p1)
                //        b = Convert.ToInt16(0);
                //    else if (i >= p1 && i <= p2)
                //        b = Convert.ToInt16(30000);//valint * i
                //    else
                //        b = Convert.ToInt16(65535);
                //    for (j = 0; j < bmdgrad.Height; ++j)
                //    {
                //        //row += j * bmdgrad.Stride;
                //        short* row = (short*)bmdgrad.Scan0 + (j * bmdgrad.Stride);
                //        row[i] = b;
                //        //row[i + j * bmd.Stride]=b;
                //    }
                //    //row -= (bmdgrad.Height - 1) * (bmdgrad.Stride);
                //}
            }
            imggrad.UnlockBits(bmdgrad);
            return imggrad;
        }

        private void VisualForm_Load(object sender, EventArgs e)
        {
            // color semi-transparente para la segunda serie del chart
            chart1.Series[0].Color = Color.FromArgb(50, Color.Green);

            barPopup = new System.Windows.Forms.TrackBar();
            barPopup.Minimum = 0;
            barPopup.Maximum = 100;
            barPopup.Value = 50;
            barPopup.AutoSize = false;
            barPopup.TickStyle = TickStyle.None;
            barPopup.Size = new Size(200, 23);
            barPopup.SmallChange = 1;
            barPopup.Scroll += barPopup_Scroll;

            // se hallan el minimo y el maximo de todos los pixeles de dicom leidos para normalizar
            minPixelValue = padre.actualV.datacubo.dataCube[0].pixelData.Min();
            maxPixelValue = padre.actualV.datacubo.dataCube[0].pixelData.Max();
            for (int i = 1; i < padre.actualV.datacubo.dataCube.Count; i++)
            {
                if (padre.actualV.datacubo.dataCube[i].pixelData.Min() < minPixelValue) minPixelValue = padre.actualV.datacubo.dataCube[i].pixelData.Min();
                if (padre.actualV.datacubo.dataCube[i].pixelData.Max() > maxPixelValue) maxPixelValue = padre.actualV.datacubo.dataCube[i].pixelData.Max();
            }

            // minimo y maximo de la barra de rango para normalizacion
            rangeBar.TotalMaximum = maxPixelValue;
            rangeBar.TotalMinimum = minPixelValue;
            rangeBar.RangeMaximum = maxPixelValue;
            rangeBar.RangeMinimum = minPixelValue;

            padre.actualV.normalizacion2D[0] = Convert.ToUInt32(minPixelValue);
            padre.actualV.normalizacion2D[1] = Convert.ToUInt32(maxPixelValue);

            pictGradiente.Image = CrearGradiente(rangeBar.RangeMinimum, rangeBar.RangeMaximum);

            labelMin.Text = "Min: " + rangeBar.RangeMinimum.ToString();
            labelMax.Text = "Max: " + rangeBar.RangeMaximum.ToString();

            rango = maxPixelValue - minPixelValue;

            // se prepara la data que alimentara el histograma
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            serieAll = new int[Convert.ToInt32(rango) + 1];
            chart1.ChartAreas[0].AxisX.Minimum = minPixelValue;
            chart1.ChartAreas[0].AxisX.Maximum = maxPixelValue;
            chart1.ChartAreas[0].AxisX.Interval = Convert.ToInt32(rango / 10);

            // se ponen las series en cero
            for (int i = 0; i < rango; i++) serieAll[i] = 0;

            // se llena la serie All
            for (int i = 0; i < padre.actualV.datacubo.dataCube.Count; i++)
            {
                for (int j = 0; j < padre.actualV.datacubo.dataCube[i].pixelData.Count; j++)
                {
                    // no se cuenta el valor minimo (vacio de la imagen CT)
                    if (padre.actualV.datacubo.dataCube[i].pixelData[j] > minPixelValue) serieAll[Convert.ToInt32(padre.actualV.datacubo.dataCube[i].pixelData[j] - minPixelValue)]++;
                }
            }
            serieAll[0] = 0;
            maxValAll = serieAll.Max();

            // se prepara la serieChartAll
            serieChartAll = new System.Windows.Forms.DataVisualization.Charting.Series();
            serieChartAll.ChartType = chart1.Series[1].ChartType;
            serieChartAll.ChartArea = chart1.Series[1].ChartArea;
            serieChartAll.Color = Color.CornflowerBlue;
            for (int i = 0; i < serieAll.Length; i++)
            {
                serieChartAll.Points.AddXY(minPixelValue + i, serieAll[i]);
            }

            // se agrega la serie al chart
            chart1.Series.RemoveAt(1);
            chart1.Series.Add(serieChartAll);

            // se agrega la serie Selected para NORMALIZACION
            chart1.Series[0].Points.Clear();
            chart1.Series[0].Points.AddXY(minPixelValue, 0);
            chart1.Series[0].Points.AddXY(rangeBar.RangeMinimum, 0);
            chart1.Series[0].Points.AddXY(rangeBar.RangeMinimum, maxValAll);
            chart1.Series[0].Points.AddXY(rangeBar.RangeMaximum, maxValAll);
            chart1.Series[0].Points.AddXY(rangeBar.RangeMaximum, 0);
            chart1.Series[0].Points.AddXY(maxPixelValue, 0);

            trackBar.Minimum = 0;
            trackBar.Maximum = padre.actualV.datacubo.dataCube.Count - 1;
            trackBar.Value = 0;
            trackBar.TickFrequency = Convert.ToInt32(padre.actualV.datacubo.dataCube.Count / 100);

            picTrans.Image = Normalizar(0, minPixelValue, maxPixelValue);    
        
            labelSlide.Text = "Slide " + (trackBar.Value + 1).ToString() + " de " + padre.actualV.datacubo.dataCube.Count;

            pictGradiente.Image = CrearGradiente(rangeBar.RangeMinimum, rangeBar.RangeMaximum);

            barPopup.Minimum = minPixelValue + 20;
            barPopup.Maximum = maxPixelValue;
            barPopup.Value = Convert.ToInt32((barPopup.Maximum - barPopup.Minimum) / 2) + barPopup.Minimum;
        }

        private void rangeBar_RangeChanging(object sender, EventArgs e)
        {
            labelMin.Text = "Min: " + rangeBar.RangeMinimum.ToString();
            labelMax.Text = "Max: " + rangeBar.RangeMaximum.ToString();

            // se prepara la serie Selected
            chart1.Series[0].Points.Clear();
            chart1.Series[0].Points.AddXY(minPixelValue, 0);
            chart1.Series[0].Points.AddXY(rangeBar.RangeMinimum, 0);
            chart1.Series[0].Points.AddXY(rangeBar.RangeMinimum, maxValAll);
            chart1.Series[0].Points.AddXY(rangeBar.RangeMaximum, maxValAll);
            chart1.Series[0].Points.AddXY(rangeBar.RangeMaximum, 0);
            chart1.Series[0].Points.AddXY(maxPixelValue, 0);

            // se debe ajustar la imagen normalizada, y umbralizada, de cada dicom segun se vaya cambiando el rangebar
            // se pregunta si se debe tambien umbralizar, o si solo es necesario normalizar y presentar la imagen normalizada
            picTrans.Image = Normalizar(this.trackBar.Value, rangeBar.RangeMinimum, rangeBar.RangeMaximum);
            
            pictGradiente.Image = CrearGradiente(rangeBar.RangeMinimum, rangeBar.RangeMaximum);

            padre.actualV.normalizacion2D[0] = Convert.ToUInt32(rangeBar.RangeMinimum);
            padre.actualV.normalizacion2D[1] = Convert.ToUInt32(rangeBar.RangeMaximum);
        }

        private void trackBar_Scroll(object sender, EventArgs e)
        {
            // se debe ajustar la imagen normalizada, y umbralizada, de cada dicom segun se vaya cambiando el trackbar

            // primero se normaliza la imagen 
            picTrans.Image = Normalizar(this.trackBar.Value, rangeBar.RangeMinimum, rangeBar.RangeMaximum);

            // se pregunta si se debe tambien umbralizar, o si solo es necesario normalizar y presentar la imagen normalizada
            if (chkHabilitar.Checked)
            {
                picTrans.Image = Umbralizar(trackBar.Value);
            }
            //else pictureBox1.Image = dd[trackBar.Value].bmp;

            labelSlide.Text = "Slide " + trackBar.Value + " de " + padre.actualV.datacubo.dataCube.Count; 
        }        
    }
}
