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

        // lista de colores preferidos
        List<System.Drawing.Color> colores = new List<Color>();

        // habilitar cambios
        bool habilitarCambios = true;

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

        // Resetea el chart y pinta la serie de normalizacion y el histograma general
        public void ResetChart()
        {
            // se elimina cualquier serie que no sea la de normalizacion y el histograma general
            while (chart1.Series.Count > 2)
                chart1.Series.RemoveAt(2);

            // se limpian las series de normalizacion [0] y del histograma general
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();

            chart1.ChartAreas[0].AxisX.Minimum = minPixelValue;
            chart1.ChartAreas[0].AxisX.Maximum = maxPixelValue;

            numAmplitud.Value = maxValAll;

            // se llena la serie de normalizacion
            chart1.Series[0].Points.AddXY(minPixelValue, 0);
            chart1.Series[0].Points.AddXY(rangeBar.RangeMinimum, 0);
            chart1.Series[0].Points.AddXY(rangeBar.RangeMinimum, padre.actualV.datacubo.histograma.Max());
            chart1.Series[0].Points.AddXY(rangeBar.RangeMaximum, padre.actualV.datacubo.histograma.Max());
            chart1.Series[0].Points.AddXY(rangeBar.RangeMaximum, 0);
            chart1.Series[0].Points.AddXY(maxPixelValue, 0);

            // se llena la serie del histograma
            for (int i = 1; i < padre.actualV.datacubo.histograma.Length; i++)
                chart1.Series[1].Points.AddXY(padre.actualV.datacubo.bins[i], padre.actualV.datacubo.histograma[i]);
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

                // se agregan a la serie los valores del histograma que esten dentro de los límites del umbral
                int ini = umbral[i].minimo - minPixelValue;
                int fin = umbral[i].maximo - minPixelValue;

                for (int j = 0; j < padre.actualV.datacubo.histograma.Length; j++)
                {
                    if ((padre.actualV.datacubo.bins[j] >= ini) & (padre.actualV.datacubo.bins[j] <= fin))
                    {
                        chart1.Series[i + 2].Points.AddXY(padre.actualV.datacubo.bins[j], padre.actualV.datacubo.histograma[j]);
                    }
                }                
            }
        }

        private void barPopup_Scroll(object sender, EventArgs e)
        {
            controlValidar = false;

            dataGrid.Rows[filaActual].Cells[columnaActual].Value = barPopup.Value;

            if (columnaActual == 1) umbral[filaActual].minimo = barPopup.Value;
            else umbral[filaActual].maximo = barPopup.Value;


            // se revisa que el umbral.maximo anterior coincida con el nuevo valor de minimo, si existe mas de un elemento
            if ((umbral.Count > 1) & (filaActual > 0))
            {
                dataGrid.Rows[filaActual - 1].Cells[2].Value = umbral[filaActual - 1].maximo = umbral[filaActual].minimo;
            }

            // se revisa que el umbral.minimo siguiente coincida con el nuevo valor de maximo, si existe mas de un elemento
            if ((umbral.Count > 1) & (filaActual < (umbral.Count - 1)))
            {
                dataGrid.Rows[filaActual + 1].Cells[1].Value = umbral[filaActual + 1].minimo = umbral[filaActual].maximo;
            }

            UmbralizarHistograma();
            if (chkNorm.Checked)
            {
                pictTrans.Image = Normalizar(trackBar.Value, rangeBar.RangeMinimum, rangeBar.RangeMaximum);
                pictHor.Image = NormalizarH(trackCorte.Value, rangeBar.RangeMinimum, rangeBar.RangeMaximum);
            }
            else
            {
                pictTrans.Image = Umbralizar(trackBar.Value);
                pictHor.Image = UmbralizarH(trackCorte.Value);
            }

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
            // failsafe: no hay valores de umbralizacion
            if (umbral.Count <= 0)
            {
                return Normalizar(trackBar.Value, rangeBar.RangeMinimum, rangeBar.RangeMaximum);
            }

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
        /// Para un corte horizontal especifico (indice) pinta de un color especifico los valores CT que se encuentran dentro de minimoValor y maximoValor
        /// </summary>
        /// <param name="indice"></param>
        /// <param name="minimoValor"></param>
        /// <param name="maximoValor"></param>
        /// <returns></returns>
        private Bitmap UmbralizarH(int indice)
        {
            // failsafe: no hay valores de umbralizacion
            if (umbral.Count <= 0)
            {
                return NormalizarH(trackCorte.Value, rangeBar.RangeMinimum, rangeBar.RangeMaximum);
            }

            int alto = padre.actualV.datacubo.dataCube[0].selector.Columns.Data;
            int total = padre.actualV.datacubo.coresHorizontal[0].Count;
            int ancho = Convert.ToInt32(Convert.ToDouble(total) / Convert.ToDouble(alto));

            Bitmap imagen = new Bitmap(ancho, alto, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

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
                        valorCT = padre.actualV.datacubo.coresHorizontal[indice][i * bmd.Width + j];
                        colorOriginal = Convert.ToInt32(Convert.ToDouble(padre.actualV.datacubo.coresHorizontal[indice][i * bmd.Width + j] - rangeBar.RangeMinimum) * ((double)255) / range);
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

        /// <summary>
        /// Normaliza una imagen de un corte horizontal
        /// </summary>
        /// <param name="indice"></param>
        /// <param name="minimoValor"></param>
        /// <param name="maximoValor"></param>
        /// <returns></returns>
        private Bitmap NormalizarH(int indice, int minimoValor, int maximoValor)
        {
            int alto = padre.actualV.datacubo.dataCube[0].selector.Columns.Data;
            int total = padre.actualV.datacubo.coresHorizontal[0].Count;

            int ancho = Convert.ToInt32(Convert.ToDouble(total) / Convert.ToDouble(alto));
            return RockStatic.MyDicom.CrearBitmap(padre.actualV.datacubo.coresHorizontal[indice], ancho, alto, minimoValor, maximoValor);

            /*
            // width height
            Bitmap imagen = new Bitmap(padre.actualV.datacubo.dataCube.Count, padre.actualV.datacubo.dataCube[indice].selector.Columns.Data, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

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
                        color = Convert.ToInt32(Convert.ToDouble(padre.actualV.datacubo.coresHorizontal[indice][i * bmd.Width + j] - minimoValor) * ((double)255) / range);
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
             */

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
            // colores favoritos para la segmentacion del histograma
            colores.Add(Color.DodgerBlue);
            colores.Add(Color.SpringGreen);
            colores.Add(Color.Gold);
            colores.Add(Color.Orange);
            colores.Add(Color.Red);

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
            barPopup.Leave +=barPopup_Leave;
            barPopup.LostFocus +=barPopup_Leave;
            barPopup.MouseLeave += barPopup_Leave;

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
            try
            {
                rangeBar.RangeMaximum = padre.actualV.normalizacion2D[1];
                rangeBar.RangeMinimum = padre.actualV.normalizacion2D[0];
            }
            catch
            {
                rangeBar.RangeMaximum = maxPixelValue;
                rangeBar.RangeMinimum = minPixelValue;
            }

            padre.actualV.normalizacion2D[0] = minPixelValue;
            padre.actualV.normalizacion2D[1] = maxPixelValue;

            pictGradiente.Image = CrearGradiente(rangeBar.RangeMinimum, rangeBar.RangeMaximum);

            numNmin.Minimum = minPixelValue;
            numNmin.Maximum = maxPixelValue;
            numNmax.Minimum = minPixelValue;
            numNmax.Maximum = maxPixelValue;
            numNmin.Value = minPixelValue;
            numNmax.Value = maxPixelValue;

            rango = maxPixelValue - minPixelValue;

            // se halla el maximo valor del histograma
            maxValAll = Convert.ToInt32(padre.actualV.datacubo.histograma[1]);
            for (int i = 2; i < padre.actualV.datacubo.histograma.Length; i++)
            {
                if (padre.actualV.datacubo.histograma[i] > maxValAll) maxValAll = Convert.ToInt32(padre.actualV.datacubo.histograma[i]);
            }

            ResetChart();
                
            trackBar.Minimum = 0;
            trackBar.Maximum = padre.actualV.datacubo.dataCube.Count - 1;
            trackBar.Value = 0;
            trackBar.TickFrequency = Convert.ToInt32(padre.actualV.datacubo.dataCube.Count / 100);
            labelSlide.Text = "Slide " + (trackBar.Value + 1).ToString() + " de " + padre.actualV.datacubo.dataCube.Count;

            pictTrans.Image = Normalizar(0, minPixelValue, maxPixelValue);

            trackCorte.Minimum = 0;
            trackCorte.Maximum = padre.actualV.datacubo.coresHorizontal.Length - 1;
            trackCorte.Value = Convert.ToInt32(padre.actualV.datacubo.coresHorizontal.Length / 2);
            trackBar.TickFrequency = Convert.ToInt32(padre.actualV.datacubo.coresHorizontal.Length / 100);
            labelCorte.Text = "Corte " + (trackCorte.Value + 1).ToString() + " de " + padre.actualV.datacubo.coresHorizontal.Length;

            pictHor.Image = NormalizarH(trackCorte.Value, minPixelValue, maxPixelValue);

            pictGradiente.Image = CrearGradiente(rangeBar.RangeMinimum, rangeBar.RangeMaximum);

            barPopup.Minimum = minPixelValue + 20;
            barPopup.Maximum = maxPixelValue;
            barPopup.Value = Convert.ToInt32((barPopup.Maximum - barPopup.Minimum) / 2) + barPopup.Minimum;

            // minimo y maximo de los numericUpDown del histograma
            numHmin.Minimum = numHmax.Minimum = minPixelValue;
            numHmin.Maximum = numHmax.Maximum = maxPixelValue;
            numHmin.Value = minPixelValue;
            numHmax.Value = maxPixelValue;

            // minimo y maximo del RangeBar del histograma
            rangeHist.TotalMinimum = minPixelValue;
            rangeHist.TotalMaximum = maxPixelValue;
            rangeHist.RangeMinimum = minPixelValue;
            rangeHist.RangeMaximum = maxPixelValue;

            pictHor.Invalidate();
            pictTrans.Invalidate();

            // si existe informacion de la segmentacion se carga en 
            try
            {
                umbral = new List<CUmbralCT>();
                for (int i = 0; i < (padre.actualV.segmentacion2D.Count - 1); i++)
                {
                    umbral.Add(new CUmbralCT(padre.actualV.segmentacion2D[i], padre.actualV.segmentacion2D[i + 1], padre.actualV.colorSeg2D[i]));

                    dataGrid.Rows.Add();
                    dataGrid.Rows[dataGrid.Rows.Count - 1].Cells[0].Value = true;
                    dataGrid.Rows[dataGrid.Rows.Count - 1].Cells[1].Value = umbral.Last().minimo;
                    dataGrid.Rows[dataGrid.Rows.Count - 1].Cells[2].Value = umbral.Last().maximo;
                    dataGrid.Rows[dataGrid.Rows.Count - 1].Cells[3].Style.BackColor = dataGrid.Rows[dataGrid.Rows.Count - 1].Cells[3].Style.ForeColor = dataGrid.Rows[dataGrid.Rows.Count - 1].Cells[3].Style.SelectionBackColor = dataGrid.Rows[dataGrid.Rows.Count - 1].Cells[3].Style.SelectionForeColor = umbral.Last().color;
                }

                UmbralizarHistograma();
            }
            catch
            {
            }

            // se cierra la ventana HomeForm
            if (padre.abiertoHomeForm) padre.homeForm.Close();
        }

        /// <summary>
        /// pinta el histograma del slide en el chart
        /// </summary>
        public void SetHistogramaSlide(int slide)
        {
            // numero de series que DEBERIAN haber
            int nseries = 2 + umbral.Count;

            if (chart1.Series.Count > nseries)
            {
                // hay una serie adicional, de histograma de slide, y se debe eliminar
                chart1.Series.RemoveAt(chart1.Series.Count - 1);
            }

            // se crea la nueva serie
            System.Windows.Forms.DataVisualization.Charting.Series histslide = new System.Windows.Forms.DataVisualization.Charting.Series();
            histslide.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            histslide.Color = Color.Black;

            int i;
            try
            {
                for (i = 1; i < padre.actualV.datacubo.dataCube[slide].histograma.Length; i++)
                    histslide.Points.AddXY(padre.actualV.datacubo.bins[i], padre.actualV.datacubo.dataCube[slide].histograma[i]);
            }
            catch
            {
                MessageBox.Show("error");
            }

            chart1.Series.Add(histslide);
        }

        private void rangeBar_RangeChanging(object sender, EventArgs e)
        {
            if (habilitarCambios)
            {
                habilitarCambios = false;

                numNmin.Value = rangeBar.RangeMinimum;
                numNmax.Value = rangeBar.RangeMaximum;

                habilitarCambios = true;
            }

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
            pictTrans.Image = Normalizar(this.trackBar.Value, rangeBar.RangeMinimum, rangeBar.RangeMaximum);
            pictHor.Image = NormalizarH(this.trackCorte.Value, rangeBar.RangeMinimum, rangeBar.RangeMaximum);

            pictGradiente.Image = CrearGradiente(rangeBar.RangeMinimum, rangeBar.RangeMaximum);

            padre.actualV.normalizacion2D[0] = rangeBar.RangeMinimum;
            padre.actualV.normalizacion2D[1] = rangeBar.RangeMaximum;
        }

        private void dataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                colorDialog1 = new ColorDialog();
                if (colorDialog1.ShowDialog() == DialogResult.OK)
                {
                    umbral[dataGrid.CurrentCell.RowIndex].color = dataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = dataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = dataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.SelectionBackColor = dataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.SelectionForeColor = colorDialog1.Color;
                    UmbralizarHistograma();
                    if (chkNorm.Checked)
                    {
                        pictTrans.Image = Normalizar(trackBar.Value, rangeBar.RangeMinimum, rangeBar.RangeMaximum);
                        pictHor.Image = NormalizarH(trackCorte.Value, rangeBar.RangeMinimum, rangeBar.RangeMaximum);
                    }
                    else
                    {
                        pictTrans.Image = Umbralizar(trackBar.Value);
                        pictHor.Image = UmbralizarH(trackCorte.Value);
                    }
                }
            }

            SetSegmentacion2D();
        }

        private void dataGrid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if ((e.ColumnIndex >= 1) & (e.ColumnIndex <= 2) & (e.Button == MouseButtons.Right))
            {
                filaActual = e.RowIndex;
                columnaActual = e.ColumnIndex;

                barPopup.Location = this.PointToClient(Cursor.Position);
                barPopup.Value = Convert.ToInt32(dataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);

                Controls.Add(barPopup);
                popup = true;

                dataGrid.SendToBack();
                barPopup.BringToFront();
            }
            if ((e.Button == MouseButtons.Left) & (popup))
            {
                Controls.Remove(barPopup);
                popup = false;
            }
        }

        private void barPopup_Leave(object sender, System.EventArgs e)
        {
            if (popup)
            {
                Controls.Remove(barPopup);
                popup = false;
            }
        }

        private void dataGrid_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            // pueden existir 4 casos para validar
            // (1) El nuevo valor es menor que minpixelvalue
            // (2) El nuevo valor es mayor que maxpixelvalue
            // (3) El nuevo valor de maxRange es menor que el valor de minRange
            // (4) El nuevo valor de minRange es mayor que el valor de maxRange

            // el nuevo valor de minRange debe cumplir las condiciones (1) (2) y (4)
            // el nuevo valor de maxRange debe cumplir las condiciones (1) (2) y (3)

            // si incumple alguna de las condiciones, se resete al valor guardaddo en memoria en la lista de umbrales

            if (!controlValidar) return;

            bool errorMin = false;
            bool errorMax = false;

            // condicion (1)
            if (Convert.ToInt32(dataGrid.Rows[e.RowIndex].Cells[1].Value) < minPixelValue)
            {
                dataGrid.Rows[e.RowIndex].Cells[0].Value = umbral[e.RowIndex].minimo;
                errorMin = true;
            }
            if (Convert.ToInt32(dataGrid.Rows[e.RowIndex].Cells[2].Value) < minPixelValue)
            {
                dataGrid.Rows[e.RowIndex].Cells[1].Value = umbral[e.RowIndex].maximo;
                errorMax = true;
            }

            // condicion (2)
            if (Convert.ToInt32(dataGrid.Rows[e.RowIndex].Cells[1].Value) > maxPixelValue)
            {
                dataGrid.Rows[e.RowIndex].Cells[0].Value = umbral[e.RowIndex].minimo;
                errorMin = true;
            }
            if (Convert.ToInt32(dataGrid.Rows[e.RowIndex].Cells[2].Value) > maxPixelValue)
            {
                dataGrid.Rows[e.RowIndex].Cells[1].Value = umbral[e.RowIndex].maximo;
                errorMax = true;
            }

            // condicion (3)
            if (Convert.ToInt32(dataGrid.Rows[e.RowIndex].Cells[2].Value) < umbral[e.RowIndex].minimo)
            {
                dataGrid.Rows[e.RowIndex].Cells[1].Value = umbral[e.RowIndex].maximo;
                errorMax = true;
            }

            // condicion (4)
            if (Convert.ToInt32(dataGrid.Rows[e.RowIndex].Cells[1].Value) > umbral[e.RowIndex].maximo)
            {
                dataGrid.Rows[e.RowIndex].Cells[0].Value = umbral[e.RowIndex].minimo;
                errorMin = true;
            }

            // si  no se encuentran errores al valiar entonces si se guardan los nuevos valores
            if (!errorMin)
            {
                umbral[e.RowIndex].minimo = Convert.ToInt32(dataGrid.Rows[e.RowIndex].Cells[1].Value);

                // se revisa que el umbral.maximo anterior coincida con el nuevo valor de minimo, si existe mas de un elemento
                if ((umbral.Count > 1) & (e.RowIndex > 0))
                {
                    dataGrid.Rows[e.RowIndex - 1].Cells[2].Value = umbral[e.RowIndex - 1].maximo = umbral[e.RowIndex].minimo;
                }
            }
            if (!errorMax)
            {
                umbral[e.RowIndex].maximo = Convert.ToInt32(dataGrid.Rows[e.RowIndex].Cells[2].Value);

                // se revisa que el umbral.minimo siguiente coincida con el nuevo valor de maximo, si existe mas de un elemento
                if ((umbral.Count > 1) & (e.RowIndex < (umbral.Count - 1)))
                {
                    dataGrid.Rows[e.RowIndex + 1].Cells[1].Value = umbral[e.RowIndex + 1].minimo = umbral[e.RowIndex].maximo;
                }
            }
            umbral[e.RowIndex].color = dataGrid.Rows[e.RowIndex].Cells[3].Style.BackColor;

            UmbralizarHistograma();
            if (chkNorm.Checked)
            {
                pictTrans.Image = Normalizar(trackBar.Value, rangeBar.RangeMinimum, rangeBar.RangeMaximum);
                pictHor.Image = NormalizarH(trackCorte.Value, rangeBar.RangeMinimum, rangeBar.RangeMaximum);
            }
            else
            {
                pictTrans.Image = Umbralizar(trackBar.Value);
                pictHor.Image = UmbralizarH(trackCorte.Value);
            }

            SetSegmentacion2D();
        }

        private void btnSubir_MouseEnter(object sender, EventArgs e)
        {
            ((System.Windows.Forms.Button)(sender)).ForeColor = Color.White;
        }

        private void btnSubir_MouseLeave(object sender, EventArgs e)
        {
            ((System.Windows.Forms.Button)(sender)).ForeColor = Color.Black;
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            int indice = dataGrid.CurrentCell.RowIndex;
            dataGrid.Rows.RemoveAt(indice);
            umbral.RemoveAt(indice);

            ResetChart();

            pictTrans.Image = Umbralizar(trackBar.Value);
            pictHor.Image = UmbralizarH(trackCorte.Value);

            SetSegmentacion2D();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            // se limpia el datagridview
            dataGrid.Rows.Clear();

            ResetChart();

            pictTrans.Image = Normalizar(trackBar.Value, rangeBar.RangeMinimum, rangeBar.RangeMaximum);
            pictHor.Image = NormalizarH(trackCorte.Value, rangeBar.RangeMinimum, rangeBar.RangeMaximum);

            SetSegmentacion2D();
        }

        private void numHmin_ValueChanged(object sender, EventArgs e)
        {
            if (habilitarCambios)
            {
                habilitarCambios = false;

                // se cambia el limite izq del eje X del chat
                // se debe verificar que el limite izq no sea >= que el limite derecho

                if (numHmin.Value >= numHmax.Value) numHmin.Value = numHmax.Value - 1;
                if (rangeHist.RangeMinimum >= rangeHist.RangeMaximum) rangeHist.RangeMinimum = rangeHist.RangeMaximum - 1;

                rangeHist.RangeMinimum = Convert.ToInt32(numHmin.Value);
                chart1.ChartAreas[0].AxisX.Minimum = Convert.ToDouble(numHmin.Value);

                habilitarCambios = true;
            }
        }

        private void numHmax_ValueChanged(object sender, EventArgs e)
        {
            if (habilitarCambios)
            {
                habilitarCambios = false;

                // se cambia el limite der del eje X del chat
                // se debe verificar que el limite der no sea <= que el limite izq

                if (numHmax.Value <= numHmin.Value) numHmax.Value = numHmin.Value + 1;
                if (rangeHist.RangeMaximum <= rangeHist.RangeMinimum) rangeHist.RangeMaximum = rangeHist.RangeMinimum + 1;

                rangeHist.RangeMaximum = Convert.ToInt32(numHmax.Value);
                chart1.ChartAreas[0].AxisX.Maximum = Convert.ToDouble(numHmax.Value);

                habilitarCambios = true;
            }
        }

        private void tab2D_Click(object sender, EventArgs e)
        {

        }

        private void pictHor_Paint(object sender, PaintEventArgs e)
        {
            // se pintan la linea de posicion
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            double imgWidth = pictHor.Image.Width;
            double imgHeight = pictHor.Image.Height;
            double boxWidth = pictHor.Size.Width;
            double boxHeight = pictHor.Size.Height;

            double scale;
            double ycero = 0;
            double xcero = 0;

            Pen brocha2 = new Pen(Color.FromArgb(128, 0, 255, 0));
            brocha2.Width = 3;

            if (imgWidth / imgHeight > boxWidth / boxHeight)
            {
                //If true, that means that the image is stretched through the width of the control.
                //'In other words: the image is limited by the width.

                //The scale of the image in the Picture Box.
                scale = boxWidth / imgWidth;

                //Since the image is in the middle, this code is used to determinate the empty space in the height
                //'by getting the difference between the box height and the image actual displayed height and dividing it by 2.
                ycero = (boxHeight - scale * imgHeight) / 2;
            }
            else
            {
                //If false, that means that the image is stretched through the height of the control.
                //'In other words: the image is limited by the height.

                //The scale of the image in the Picture Box.
                scale = boxHeight / imgHeight;

                //Since the image is in the middle, this code is used to determinate the empty space in the width
                //'by getting the difference between the box width and the image actual displayed width and dividing it by 2.
                xcero = (boxWidth - scale * imgWidth) / 2;
            }

            // se averigua el factor de escalado del corte horizontal
            double alto = padre.actualV.datacubo.dataCube[0].selector.Columns.Data;
            double total = padre.actualV.datacubo.coresHorizontal[0].Count;
            double ancho = Convert.ToInt32(total / alto);
            int factor = Convert.ToInt32(ancho / Convert.ToInt32(padre.actualV.datacubo.dataCube.Count));

            int pos = Convert.ToInt32((trackBar.Value * scale * factor) + xcero);
            e.Graphics.DrawLine(brocha2, pos, 0, pos, pictHor.Height);
        }

        private void pictTrans_Paint(object sender, PaintEventArgs e)
        {
            // se pintan la linea de posicion
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            double imgWidth = pictTrans.Image.Width;
            double imgHeight = pictTrans.Image.Height;
            double boxWidth = pictTrans.Size.Width;
            double boxHeight = pictTrans.Size.Height;

            double scale;
            double ycero = 0;
            double xcero = 0;

            Pen brocha2 = new Pen(Color.FromArgb(128, 0, 255, 0));
            brocha2.Width = 3;

            if (imgWidth / imgHeight > boxWidth / boxHeight)
            {
                //If true, that means that the image is stretched through the width of the control.
                //'In other words: the image is limited by the width.

                //The scale of the image in the Picture Box.
                scale = boxWidth / imgWidth;

                //Since the image is in the middle, this code is used to determinate the empty space in the height
                //'by getting the difference between the box height and the image actual displayed height and dividing it by 2.
                ycero = (boxHeight - scale * imgHeight) / 2;
            }
            else
            {
                //If false, that means that the image is stretched through the height of the control.
                //'In other words: the image is limited by the height.

                //The scale of the image in the Picture Box.
                scale = boxHeight / imgHeight;

                //Since the image is in the middle, this code is used to determinate the empty space in the width
                //'by getting the difference between the box width and the image actual displayed width and dividing it by 2.
                xcero = (boxWidth - scale * imgWidth) / 2;
            }

            int pos = Convert.ToInt32((trackCorte.Value * scale) + ycero);
            e.Graphics.DrawLine(brocha2, 0, pos, pictTrans.Width, pos);
        }

        private void trackBar_Scroll(object sender, EventArgs e)
        {
            // se debe ajustar la imagen normalizada, y umbralizada, de cada dicom segun se vaya cambiando el trackbar

            if (chkNorm.Checked)
            {
                pictTrans.Image = Normalizar(trackBar.Value, rangeBar.RangeMinimum, rangeBar.RangeMaximum);
                pictHor.Image = NormalizarH(trackCorte.Value, rangeBar.RangeMinimum, rangeBar.RangeMaximum);
            }
            else
            {
                pictTrans.Image = Umbralizar(trackBar.Value);
                pictHor.Image = UmbralizarH(trackCorte.Value);
            }

            labelSlide.Text = "Slide " + (trackBar.Value + 1) + " de " + padre.actualV.datacubo.dataCube.Count;
            pictHor.Invalidate();

            //SetHistogramaSlide(trackBar.Value);
        }

        private void trackCorte_Scroll(object sender, EventArgs e)
        {
            // se debe ajustar la imagen normalizada, y umbralizada, de cada dicom segun se vaya cambiando el trackbar

            if (chkNorm.Checked)
            {
                pictTrans.Image = Normalizar(trackBar.Value, rangeBar.RangeMinimum, rangeBar.RangeMaximum);
                pictHor.Image = NormalizarH(trackCorte.Value, rangeBar.RangeMinimum, rangeBar.RangeMaximum);
            }
            else
            {
                pictTrans.Image = Umbralizar(trackBar.Value);
                pictHor.Image = UmbralizarH(trackCorte.Value);
            }

            labelCorte.Text = "Corte " + (trackCorte.Value + 1) + " de " + padre.actualV.datacubo.dataCube[0].selector.Rows.Data;
            pictTrans.Invalidate();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // se agrega un nuevo umbral
            // si no hay umbrales creados, se crea uno que cubra todo el rango
            if (umbral.Count == 0)
            {
                CUmbralCT temp = new CUmbralCT(minPixelValue, maxPixelValue, colores[0]);
                umbral.Add(temp);

                dataGrid.Rows.Add();
                dataGrid.Rows[0].Cells[0].Value = true;
                dataGrid.Rows[0].Cells[1].Value = minPixelValue + 20;
                dataGrid.Rows[0].Cells[2].Value = maxPixelValue;
                dataGrid.Rows[0].Cells[3].Style.BackColor = dataGrid.Rows[0].Cells[3].Style.ForeColor = dataGrid.Rows[0].Cells[3].Style.SelectionBackColor = dataGrid.Rows[0].Cells[3].Style.SelectionForeColor = colores[0];
            }
            else
            {
                Color colorAagregar;
                if (umbral.Count < colores.Count) colorAagregar = colores[umbral.Count];
                else colorAagregar = colores[umbral.Count - 1];

                // se agrega un nuevo umbral a continuacion del ultimo, hasta el valor maximo
                CUmbralCT temp = new CUmbralCT(umbral.Last().maximo, maxPixelValue, colorAagregar);
                umbral.Add(temp);

                dataGrid.Rows.Add();
                dataGrid.Rows[dataGrid.Rows.Count - 1].Cells[0].Value = true;
                dataGrid.Rows[dataGrid.Rows.Count - 1].Cells[1].Value = umbral.Last().minimo;
                dataGrid.Rows[dataGrid.Rows.Count - 1].Cells[2].Value = umbral.Last().maximo;
                dataGrid.Rows[dataGrid.Rows.Count - 1].Cells[3].Style.BackColor = dataGrid.Rows[dataGrid.Rows.Count - 1].Cells[3].Style.ForeColor = dataGrid.Rows[dataGrid.Rows.Count - 1].Cells[3].Style.SelectionBackColor = dataGrid.Rows[dataGrid.Rows.Count - 1].Cells[3].Style.SelectionForeColor = colorAagregar;
            }

            UmbralizarHistograma();

            if (chkNorm.Checked)
            {
                pictTrans.Image = Normalizar(trackBar.Value, rangeBar.RangeMinimum, rangeBar.RangeMaximum);
                pictHor.Image = NormalizarH(trackCorte.Value, rangeBar.RangeMinimum, rangeBar.RangeMaximum);
            }
            else
            {
                pictTrans.Image = Umbralizar(trackBar.Value);
                pictHor.Image = UmbralizarH(trackCorte.Value);
            }

            SetSegmentacion2D();
        }

        /// <summary>
        /// Envía al proyecto la información de la segmentacion 2D
        /// </summary>
        public void SetSegmentacion2D()
        {
            // se guardan los colores de la segmentacion
            padre.actualV.colorSeg2D = new List<Color>();
            padre.actualV.segmentacion2D = new List<int>();
            padre.actualV.segmentacion2D.Add(umbral[0].minimo);

            if (umbral.Count > 0)
            {
                for (int i = 0; i < umbral.Count; i++)
                {
                    padre.actualV.segmentacion2D.Add(umbral[i].maximo);
                    padre.actualV.colorSeg2D.Add(umbral[i].color);
                }
            }
            else return;
        }

        private void chkNorm_CheckedChanged(object sender, EventArgs e)
        {
            chkUmbral.Checked = !chkNorm.Checked;
            groupNorm.Enabled = chkNorm.Checked;

            if (chkNorm.Checked)
            {
                pictTrans.Image = Normalizar(trackBar.Value, rangeBar.RangeMinimum, rangeBar.RangeMaximum);
                pictHor.Image = NormalizarH(trackCorte.Value, rangeBar.RangeMinimum, rangeBar.RangeMaximum);
            }
            else
            {
                pictTrans.Image = Umbralizar(trackBar.Value);
                pictHor.Image = UmbralizarH(trackCorte.Value);
            }
        }

        private void chkUmbral_CheckedChanged(object sender, EventArgs e)
        {
            chkNorm.Checked = !chkUmbral.Checked;
            groupUmbral.Enabled = chkUmbral.Checked;

            if (chkNorm.Checked)
            {
                pictTrans.Image = Normalizar(trackBar.Value, rangeBar.RangeMinimum, rangeBar.RangeMaximum);
                pictHor.Image = NormalizarH(trackCorte.Value, rangeBar.RangeMinimum, rangeBar.RangeMaximum);
            }
            else
            {
                pictTrans.Image = Umbralizar(trackBar.Value);
                pictHor.Image = UmbralizarH(trackCorte.Value);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            padre.actualV.Guardar();
            this.Close();
            padre.AbrirHomeForm();
        }

        private void tab3D_Click(object sender, EventArgs e)
        {

        }

        private void rangeHist_RangeChanging(object sender, EventArgs e)
        {
            if (habilitarCambios)
            {
                habilitarCambios = false;

                numHmin.Value = rangeHist.RangeMinimum;
                numHmax.Value = rangeHist.RangeMaximum;

                chart1.ChartAreas[0].AxisX.Minimum = Convert.ToDouble(numHmin.Value);
                chart1.ChartAreas[0].AxisX.Maximum = Convert.ToDouble(numHmax.Value);

                habilitarCambios = true;
            }
        }

        private void numNmin_ValueChanged(object sender, EventArgs e)
        {
            if (habilitarCambios)
            {
                habilitarCambios = false;

                rangeBar.RangeMinimum = Convert.ToInt32(numNmin.Value);

                habilitarCambios = true;
            }
        }

        private void numNmax_ValueChanged(object sender, EventArgs e)
        {
            if (habilitarCambios)
            {
                habilitarCambios = false;

                rangeBar.RangeMaximum = Convert.ToInt32(numNmax.Value);

                habilitarCambios = true;
            }
        }

        private void numAmplitud_ValueChanged(object sender, EventArgs e)
        {
            this.chart1.ChartAreas[0].AxisY.Maximum = Convert.ToDouble(this.numAmplitud.Value);
        }
    }
}
