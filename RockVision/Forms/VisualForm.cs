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
using Kitware.VTK;

namespace RockVision
{
    public partial class VisualForm : Form
    {
        #region variales de diseñador

        double lastRotX = 0;
        double lastRotY = 0;
        double lastRotZ = 0;

        bool mouseDownTransparencia = false;

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

        /// <summary>
        /// Factor de escalado
        /// </summary>
        double factor;

        /// <summary>
        /// Indica si ya ha sido creado, o no, el renderwindow de VTK
        /// </summary>
        bool renderCargado = false;

        DateTime dateRenderCargado;

        vtkRenderWindow renderWindow;
        vtkRenderer renderer;

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
                chart1.Series[i + 2].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Area;

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

            chart1.ChartAreas[0].AxisX.Minimum = Convert.ToDouble(numHmin.Value);
            chart1.ChartAreas[0].AxisX.Maximum = Convert.ToDouble(numHmax.Value);
            chart1.ChartAreas[0].AxisY.Maximum = Convert.ToDouble(numAmplitud.Value);
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
                pictHor.Image = NormalizarH(trackHor.Value, rangeBar.RangeMinimum, rangeBar.RangeMaximum);
            }
            else
            {
                pictTrans.Image = Umbralizar(trackBar.Value);
                pictHor.Image = UmbralizarH(trackHor.Value);
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
                return Normalizar(trackBar.Value, padre.actualV.segR * 2, padre.actualV.segR * 2, rangeBar.RangeMinimum, rangeBar.RangeMaximum);
            }

            Bitmap imagen = new Bitmap(Convert.ToInt16(padre.actualV.datacubo.diametroSegRV), Convert.ToInt16(padre.actualV.datacubo.diametroSegRV), System.Drawing.Imaging.PixelFormat.Format24bppRgb);

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
                return NormalizarH(trackHor.Value, rangeBar.RangeMinimum, rangeBar.RangeMaximum);
            }

            int alto = Convert.ToInt16(padre.actualV.datacubo.diametroSegRV);
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
        /// Para un corte vertical especifico (indice) pinta de un color especifico los valores CT que se encuentran dentro de minimoValor y maximoValor
        /// </summary>
        /// <param name="indice"></param>
        /// <param name="minimoValor"></param>
        /// <param name="maximoValor"></param>
        /// <returns></returns>
        private Bitmap UmbralizarV(int indice)
        {
            // failsafe: no hay valores de umbralizacion
            if (umbral.Count <= 0)
            {
                return NormalizarV(trackHor.Value, rangeBar.RangeMinimum, rangeBar.RangeMaximum);
            }

            int alto = Convert.ToInt16(padre.actualV.datacubo.diametroSegRV);
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
                        colorOriginal = Convert.ToInt32(Convert.ToDouble(padre.actualV.datacubo.coresVertical[indice][i * bmd.Width + j] - rangeBar.RangeMinimum) * ((double)255) / range);
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
        /// Para un dicom en especifico (indice) normaliza a escala de grises los valores CT que se encuentran dentro de minimoValor y maximoValor
        /// </summary>
        /// <param name="indice"></param>
        /// <param name="minimoValor"></param>
        /// <param name="maximoValor"></param>
        /// <returns></returns>
        private Bitmap Normalizar(int indice, int widht, int height, int minimoValor, int maximoValor)
        {
            Bitmap imagen = new Bitmap(widht, height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

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
            int alto = Convert.ToInt16(this.padre.actualV.datacubo.diametroSegRV);
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

        /// <summary>
        /// Normaliza una imagen de un corte vertical
        /// </summary>
        /// <param name="indice"></param>
        /// <param name="minimoValor"></param>
        /// <param name="maximoValor"></param>
        /// <returns></returns>
        private Bitmap NormalizarV(int indice, int minimoValor, int maximoValor)
        {
            int alto = Convert.ToInt16(this.padre.actualV.datacubo.diametroSegRV);
            int total = padre.actualV.datacubo.coresVertical[0].Count;

            int ancho = Convert.ToInt32(Convert.ToDouble(total) / Convert.ToDouble(alto));
            return RockStatic.MyDicom.CrearBitmap(padre.actualV.datacubo.coresVertical[indice], ancho, alto, minimoValor, maximoValor);

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
                ////short* row = (short*)bmdgrad.Scan0;
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
            padre.ShowWaiting("Espere mientras RockVision prepara la visualizacion 2D y 3D");

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
            barPopup.Leave += barPopup_Leave;
            barPopup.LostFocus += barPopup_Leave;
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
            rangeBar.RangeMaximum = maxPixelValue;
            rangeBar.RangeMinimum = minPixelValue;
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

            pictGradiente.Image = CrearGradiente(rangeBar.RangeMinimum, rangeBar.RangeMaximum);

            habilitarCambios = false;
            numNmin.Minimum = minPixelValue;
            numNmin.Maximum = maxPixelValue;
            numNmax.Minimum = minPixelValue;
            numNmax.Maximum = maxPixelValue;
            habilitarCambios = true;

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
            //trackBar.TickFrequency = Convert.ToInt32(padre.actualV.datacubo.dataCube.Count / 100);
            labelSlide.Text = "Slide " + (trackBar.Value + 1).ToString() + " de " + padre.actualV.datacubo.dataCube.Count;

            pictTrans.Image = Normalizar(0, padre.actualV.segR*2,padre.actualV.segR*2,rangeBar.RangeMinimum, rangeBar.RangeMaximum);

            trackHor.Minimum = 0;
            trackHor.Maximum = padre.actualV.datacubo.coresHorizontal.Length - 1;
            trackHor.Value = Convert.ToInt32(padre.actualV.datacubo.coresHorizontal.Length / 2);
            //trackHor.TickFrequency = Convert.ToInt32(padre.actualV.datacubo.coresHorizontal.Length / 100);
            lblHor.Text = "Corte " + (trackHor.Value + 1).ToString() + " de " + padre.actualV.datacubo.coresHorizontal.Length;

            pictHor.Image = NormalizarH(trackHor.Value, minPixelValue, maxPixelValue);

            trackVer.Minimum = 0;
            trackVer.Maximum = padre.actualV.datacubo.coresVertical.Length - 1;
            trackVer.Value = Convert.ToInt32(padre.actualV.datacubo.coresVertical.Length / 2);
            //trackVer.TickFrequency = Convert.ToInt32(padre.actualV.datacubo.coresVertical.Length / 100);
            lblVer.Text = "Corte " + (trackVer.Value + 1).ToString() + " de " + padre.actualV.datacubo.coresVertical.Length;

            pictVer.Image = NormalizarV(trackVer.Value, rangeBar.RangeMinimum, rangeBar.RangeMaximum);

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

            numNmin.Value = padre.actualV.normalizacion2D[0];
            numNmax.Value = padre.actualV.normalizacion2D[1];

            pictHor.Invalidate();
            pictTrans.Invalidate();
            pictVer.Invalidate();

            // si existe informacion de la segmentacion se carga en el datagrid
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
        /// se toman los limites de los planos de corte en cada eje (2 planos para cada eje) y se agregan solo los puntos que conforman el cascaron al control VTK
        /// </summary>
        public void Visual3Dcortes()
        {

            //// se recorre de manera tridimensional, primero el plano transversal y luego la profundidad, los puntos
            //// se agregan aquellos puntos que esten cerca/sobre el convex hull

            //// primero se revisan los limites en Z y luego en cada plano transversal



            //// se crean los puntos que pertenecen en la circunferencia
            //List<System.Drawing.Point> listaCirc = new List<Point>();

            ///*
            //double dist = 0;
            //double tol = 0.5;
            //for (int i = 0; i < padre.actualV.datacubo.dataCube[0].selector.Columns.Data; i=i+2)
            //{
            //    for (int j = 0; j < padre.actualV.datacubo.dataCube[0].selector.Rows.Data; j=j+2)
            //    {
            //        dist = Math.Sqrt(Convert.ToDouble((i - padre.actualV.segX) * (i - padre.actualV.segX)) + Convert.ToDouble((j - padre.actualV.segY) * (j - padre.actualV.segY)));
            //        if (Math.Abs(dist - Convert.ToDouble(padre.actualV.segR)) <= tol) // el punto esta dentro de la tolerancia de la tolerancia
            //            listaCirc.Add(new Point(i, j));
            //    }
            //}
            //*/

            //// se calculan los puntos de la circunferencia cada dos grados
            //for (int i = 0; i < 200; i++)
            //{
            //    double angulo = Convert.ToDouble(i) * Math.PI / Convert.ToDouble(100);
            //    double x = Math.Cos(angulo) * Convert.ToDouble(padre.actualV.segR) + padre.actualV.segX;
            //    double y = Math.Sin(angulo) * Convert.ToDouble(padre.actualV.segR) + padre.actualV.segY;
            //    listaCirc.Add(new Point(Convert.ToInt32(x),Convert.ToInt32(y)));                
            //}


            //int contador = 0;
            //contador++;

            //// se evalua cada punto de la circunferencia, por cuadrantes. Se reduce la circunferencia de acuerdo con los planos de corte
            //for (int i = 0; i < listaCirc.Count; i++)
            //{
            //    if ((listaCirc[i].X <= padre.actualV.segX) & (listaCirc[i].Y <= padre.actualV.segY)) // primer cuadrante, arriba a la izq
            //    {
            //        if (listaCirc[i].X <= rangeCorteX.RangeMinimum) listaCirc[i] = new Point(rangeCorteX.RangeMinimum,listaCirc[i].Y);
            //        if (listaCirc[i].Y <= rangeCorteY.RangeMinimum) listaCirc[i] = new Point(listaCirc[i].X, rangeCorteY.RangeMinimum);
            //    }
            //    else if ((listaCirc[i].X > padre.actualV.segX) & (listaCirc[i].Y <= padre.actualV.segY)) // segundo cuadrante, arriba a la derecha
            //    {
            //        if (listaCirc[i].X >= rangeCorteX.RangeMaximum) listaCirc[i] = new Point(rangeCorteX.RangeMaximum, listaCirc[i].Y);
            //        if (listaCirc[i].Y <= rangeCorteY.RangeMinimum) listaCirc[i] = new Point(listaCirc[i].X, rangeCorteY.RangeMinimum);
            //    }
            //    else if ((listaCirc[i].X <= padre.actualV.segX) & (listaCirc[i].Y > padre.actualV.segY)) // tercer cuadrante, abajo a la izq
            //    {
            //        if (listaCirc[i].X <= rangeCorteX.RangeMinimum) listaCirc[i] = new Point(rangeCorteX.RangeMinimum, listaCirc[i].Y);
            //        if (listaCirc[i].Y >= rangeCorteY.RangeMaximum) listaCirc[i] = new Point(listaCirc[i].X, rangeCorteY.RangeMaximum);
            //    }
            //    else if ((listaCirc[i].X > padre.actualV.segX) & (listaCirc[i].Y > padre.actualV.segY)) // segundo cuadrante, abajo a la derecha
            //    {
            //        if (listaCirc[i].X >= rangeCorteX.RangeMaximum) listaCirc[i] = new Point(rangeCorteX.RangeMaximum, listaCirc[i].Y);
            //        if (listaCirc[i].Y >= rangeCorteY.RangeMaximum) listaCirc[i] = new Point(listaCirc[i].X, rangeCorteY.RangeMaximum);
            //    }
            //}

            //// se empiezan a agregar los puntos, en las coordenadas especificadas, al control VTK, con el correspondiente color
            //Visualize3DCortes(listaCirc);


        }

        /// <summary>
        /// Toma los puntos escogidos en Visual3Dcortes y los pinta en el corte 
        /// </summary>
        /// <param name="hull"></param>
        private void Visualize3DCortes(List<System.Drawing.Point> hull)
        {
            vtkImageData.GlobalWarningDisplayOff();

            int ancho = Convert.ToInt32(padre.actualV.datacubo.dataCube[0].selector.Columns.Data);
            int alto = Convert.ToInt32(padre.actualV.datacubo.dataCube[0].selector.Rows.Data);
            int pixSlide = ancho * alto;
            int x = 0; // columnas
            int y = 0; // filas

            int largoCilindro = padre.actualV.datacubo.dataCube.Count;
            int altoCilindro = Convert.ToInt32(padre.actualV.datacubo.dataCube[0].selector.Rows.Data / 2);
            int anchoCilindro = Convert.ToInt32(padre.actualV.datacubo.dataCube[0].selector.Columns.Data / 2);

            double centerX = 0;
            double centerY = 0;
            double centerZ = 0;

            // se calcula el factor de escalado debido al espaciado entre Slides
            double resZ = Convert.ToDouble(padre.actualV.datacubo.dataCube[0].selector.SliceThickness.Data);
            double resXY = Convert.ToDouble(padre.actualV.datacubo.dataCube[0].selector.PixelSpacing.Data_[0]);
            int factorZ = Convert.ToInt32(resZ / resXY); { }

            // factor de escalado
            double factor = 100;

            // se preparan los colores
            vtkUnsignedCharArray colors = new vtkUnsignedCharArray();
            colors.SetName("Colors");
            colors.SetNumberOfComponents(3);

            vtkPoints puntos = new vtkPoints();

            for (int z = 0; z < 2; z++)
            {
                for (int i = 0; i < hull.Count / 2; i++)
                {
                    puntos.InsertNextPoint(hull[i].X, hull[i].Y, z);
                }
            }

            /*
            for (int i = 0; i < hull.Count/2; i++)
            {
                
                // se agrega la coordenada Z desde el primer valor hasta el ultimo del rango de seleccion
                for (int z = rangeCorteZ.RangeMinimum; z < rangeCorteZ.RangeMaximum; z++)
                {
                    puntos.InsertNextPoint(hull[i].X, hull[i].Y, z);
                }
                 

                // se agrega la coordenada Z desde el primer valor hasta el ultimo del rango de seleccion
                for (int z = 0; z < 2; z++)
                {
                    puntos.InsertNextPoint(hull[i].X, hull[i].Y, z);
                }
            }*/

            vtkPolyData polydata = new vtkPolyData();
            polydata.SetPoints(puntos);

            vtkSurfaceReconstructionFilter surf = new vtkSurfaceReconstructionFilter();
            surf.SetInput(polydata);

            vtkContourFilter cf = new vtkContourFilter();
            cf.SetInputConnection(surf.GetOutputPort());
            cf.SetValue(0, 0.0);

            /*
            vtkReverseSense reverse = new vtkReverseSense();
            reverse.SetInputConnection(cf.GetOutputPort());
            reverse.ReverseCellsOn();
            reverse.ReverseNormalsOn();

            vtkPolyDataMapper map = vtkPolyDataMapper.New();
            map.SetInputConnection(reverse.GetOutputPort());
            map.ScalarVisibilityOff();*/

            vtkPolyDataMapper map = vtkPolyDataMapper.New();
            map.SetInputConnection(cf.GetOutputPort());
            map.ScalarVisibilityOff();

            vtkActor surfaceActor = new vtkActor();
            surfaceActor.SetMapper(map);

            renderer.AddActor(surfaceActor);
            //renderer.SetBackground(0.2, 0.3, 0.4);

            //renderWindow.Render();

            /*
            vtkDataSetMapper originalMapper = new vtkDataSetMapper();
            originalMapper.SetInput(polydata);

            vtkActor originalActor = new vtkActor();
            originalActor.SetMapper(originalMapper);
            originalActor.GetProperty().SetColor(1, 0, 0);

            renderWindow = renderWindowControl1.RenderWindow;
            renderer = renderWindow.GetRenderers().GetFirstRenderer();
            renderer.SetBackground(0, 0, 0);
            renderer.SetInteractive(0);
             

            renderer.AddActor(originalActor);
             */

            /*
            vtkSurfaceReconstructionFilter surf = new vtkSurfaceReconstructionFilter();
            surf.SetInput(polydata);

            vtkContourFilter cf = new vtkContourFilter();
            cf.SetInputConnection(surf.GetOutputPort());
            cf.SetValue(0, 0.0);

            vtkReverseSense reverse = new vtkReverseSense();
            reverse.SetInputConnection(cf.GetOutputPort());
            reverse.ReverseCellsOn();
            reverse.ReverseNormalsOn();

            vtkPolyDataMapper map = vtkPolyDataMapper.New();
            map.SetInputConnection(reverse.GetOutputPort());
            map.ScalarVisibilityOff();

            vtkActor actor = vtkActor.New();
            actor.SetMapper(map);

            renderer.AddActor(actor);    
             * */

            /*
            vtkMarchingCubes mc = vtkMarchingCubes.New();
            mc.SetInput(polydata);
            mc.ComputeNormalsOn();
            mc.ComputeGradientsOn();
            mc.SetValue(0, 50);

            vtkPolyDataConnectivityFilter confilter = vtkPolyDataConnectivityFilter.New();
            confilter.SetInputConnection(mc.GetOutputPort());
            confilter.SetExtractionModeToLargestRegion();

            // Create a mapper
            vtkPolyDataMapper mapper = vtkPolyDataMapper.New();
            mapper.SetInputConnection(confilter.GetOutputPort());
            
            mapper.ScalarVisibilityOff();    // utilize actor's property I set

            // Visualize
            vtkActor actor = vtkActor.New();
            actor.GetProperty().SetColor(1, 1, 1);
            actor.SetMapper(mapper);

            //this.renderWindowControl1_Load(new object(), new EventArgs());

            try
            {
                renderer.AddActor(actor);
            }
            catch
            {
                MessageBox.Show("error");
            }
            */
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
            pictTrans.Image = Normalizar(trackBar.Value, padre.actualV.segR * 2, padre.actualV.segR * 2, rangeBar.RangeMinimum, rangeBar.RangeMaximum);
            pictHor.Image = NormalizarH(this.trackHor.Value, rangeBar.RangeMinimum, rangeBar.RangeMaximum);
            pictVer.Image = NormalizarH(this.trackVer.Value, rangeBar.RangeMinimum, rangeBar.RangeMaximum);

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
                        pictHor.Image = NormalizarH(trackHor.Value, rangeBar.RangeMinimum, rangeBar.RangeMaximum);
                        pictVer.Image=NormalizarH(trackVer.Value, rangeBar.RangeMinimum, rangeBar.RangeMaximum);
                    }
                    else
                    {
                        pictTrans.Image = Umbralizar(trackBar.Value);
                        pictHor.Image = UmbralizarH(trackHor.Value);
                        pictVer.Image = UmbralizarH(trackVer.Value);
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
            // pueden existir 4 casos de error para validar
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
                pictHor.Image = NormalizarH(trackHor.Value, rangeBar.RangeMinimum, rangeBar.RangeMaximum);
                pictVer.Image = NormalizarH(trackVer.Value, rangeBar.RangeMinimum, rangeBar.RangeMaximum);
            }
            else
            {
                pictTrans.Image = Umbralizar(trackBar.Value);
                pictHor.Image = UmbralizarH(trackHor.Value);
                pictVer.Image = UmbralizarH(trackVer.Value);
            }

            SetSegmentacion2D();

            if ((tabControl1.SelectedIndex == 1) && (renderCargado))
            {
                renderer.RemoveAllViewProps();
                Visual3DDispersion();
                btnResetRot_Click(sender, e);
                renderWindowControl1.Refresh();
            }
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
            pictHor.Image = UmbralizarH(trackHor.Value);
            pictVer.Image = UmbralizarV(trackVer.Value);

            if (dataGrid.Rows.Count < 1)
            {
                btnLimpiar.Enabled = btnBorrar.Enabled = false;
                btnLimpiar_Click(sender, e);
            }
            else
            {
                SetSegmentacion2D();
                UmbralizarHistograma();
            }

            
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            // se limpia el datagridview
            dataGrid.Rows.Clear();

            umbral = new List<CUmbralCT>();
            padre.actualV.colorSeg2D = new List<Color>();
            padre.actualV.segmentacion2D = new List<int>();

            ResetChart();

            pictTrans.Image = Normalizar(trackBar.Value, Convert.ToInt16(padre.actualV.datacubo.diametroSegRV), Convert.ToInt16(padre.actualV.datacubo.diametroSegRV), rangeBar.RangeMinimum, rangeBar.RangeMaximum);
            pictHor.Image = NormalizarH(trackHor.Value, rangeBar.RangeMinimum, rangeBar.RangeMaximum);
            pictVer.Image = NormalizarV(trackVer.Value, rangeBar.RangeMinimum, rangeBar.RangeMaximum);

            this.btnLimpiar.Enabled = false;
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
            double alto = padre.actualV.datacubo.diametroSegRV;
            double total = padre.actualV.datacubo.coresHorizontal[0].Count;
            double ancho = Convert.ToInt32(total / alto);
            int factor = Convert.ToInt32(ancho / Convert.ToInt32(padre.actualV.datacubo.dataCube.Count));

            int pos = Convert.ToInt32((trackBar.Value * scale * factor) + xcero);
            e.Graphics.DrawLine(brocha2, pos, 0, pos, pictHor.Height);

            pos = Convert.ToInt32((trackVer.Value * scale) + ycero);
            e.Graphics.DrawLine(brocha2, 0, pos, pictHor.Width, pos);
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

            int pos = Convert.ToInt32((trackHor.Value * scale) + ycero);
            e.Graphics.DrawLine(brocha2, 0, pos, pictTrans.Width, pos);

            pos = Convert.ToInt32((trackVer.Value * scale) + xcero);
            e.Graphics.DrawLine(brocha2, pos, 0, pos, pictTrans.Height);
        }

        private void trackBar_Scroll(object sender, EventArgs e)
        {
            // se debe ajustar la imagen normalizada, y umbralizada, de cada dicom segun se vaya cambiando el trackbar

            if (chkNorm.Checked)
            {
                pictTrans.Image = Normalizar(trackBar.Value, padre.actualV.segR * 2, padre.actualV.segR * 2, rangeBar.RangeMinimum, rangeBar.RangeMaximum);
                //pictHor.Image = NormalizarH(trackHor.Value, rangeBar.RangeMinimum, rangeBar.RangeMaximum);
                //pictVer.Image = NormalizarV(trackHor.Value, rangeBar.RangeMinimum, rangeBar.RangeMaximum);
            }
            else
            {
                pictTrans.Image = Umbralizar(trackBar.Value);
                //pictHor.Image = UmbralizarH(trackHor.Value);
            }

            labelSlide.Text = "Slide " + (trackBar.Value + 1) + " de " + padre.actualV.datacubo.dataCube.Count;

            pictTrans.Invalidate();
            pictHor.Invalidate();
            pictVer.Invalidate();

            //SetHistogramaSlide(trackBar.Value);
        }

        private void trackCorte_Scroll(object sender, EventArgs e)
        {
            // se debe ajustar la imagen normalizada, y umbralizada, de cada dicom segun se vaya cambiando el trackbar

            if (chkNorm.Checked)
            {
                //pictTrans.Image = Normalizar(trackBar.Value, rangeBar.RangeMinimum, rangeBar.RangeMaximum);
                pictHor.Image = NormalizarH(trackHor.Value, rangeBar.RangeMinimum, rangeBar.RangeMaximum);
            }
            else
            {
                //pictTrans.Image = Umbralizar(trackBar.Value);
                pictHor.Image = UmbralizarH(trackHor.Value);
            }

            lblHor.Text = "Corte " + (trackHor.Value + 1) + " de " + padre.actualV.datacubo.dataCube[0].selector.Rows.Data;

            pictTrans.Invalidate();
            pictHor.Invalidate();
            pictVer.Invalidate();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // se agrega un nuevo umbral
            // si no hay umbrales creados, se crea uno que cubra todo el rango
            if (umbral.Count == 0)
            {
                CUmbralCT temp = new CUmbralCT(minPixelValue + 20, maxPixelValue, colores[0]);
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
                pictHor.Image = NormalizarH(trackHor.Value, rangeBar.RangeMinimum, rangeBar.RangeMaximum);
            }
            else
            {
                pictTrans.Image = Umbralizar(trackBar.Value);
                pictHor.Image = UmbralizarH(trackHor.Value);
            }

            btnBorrar.Enabled = btnLimpiar.Enabled = true;

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
                pictTrans.Image = Normalizar(trackBar.Value, padre.actualV.segR * 2, padre.actualV.segR * 2, rangeBar.RangeMinimum, rangeBar.RangeMaximum);
                pictHor.Image = NormalizarH(trackHor.Value, rangeBar.RangeMinimum, rangeBar.RangeMaximum);
                pictVer.Image = NormalizarH(trackVer.Value, rangeBar.RangeMinimum, rangeBar.RangeMaximum);
            }
            else
            {
                pictTrans.Image = Umbralizar(trackBar.Value);
                pictHor.Image = UmbralizarH(trackHor.Value);
                pictVer.Image = UmbralizarH(trackVer.Value);

                if (dataGrid.Rows.Count < 1)
                {
                    btnLimpiar.Enabled = false;
                    btnBorrar.Enabled = false;
                }
            }

            if ((tabControl1.SelectedIndex == 1) && (renderCargado))
            {
                try
                {
                    renderer.RemoveAllViewProps();
                    renderWindowControl1.Refresh();
                    renderWindowControl1_Load(sender, e);
                }
                catch
                {

                }
            }
        }

        private void chkUmbral_CheckedChanged(object sender, EventArgs e)
        {

            chkNorm.Checked = !chkUmbral.Checked;
            groupUmbral.Enabled = chkUmbral.Checked;

            if (chkNorm.Checked)
            {
                pictTrans.Image = Normalizar(trackBar.Value, padre.actualV.segR * 2, padre.actualV.segR * 2, rangeBar.RangeMinimum, rangeBar.RangeMaximum);
                pictHor.Image = NormalizarH(trackHor.Value, rangeBar.RangeMinimum, rangeBar.RangeMaximum);
                pictVer.Image = NormalizarH(trackVer.Value, rangeBar.RangeMinimum, rangeBar.RangeMaximum);
            }
            else
            {
                pictTrans.Image = Umbralizar(trackBar.Value);
                pictHor.Image = UmbralizarH(trackHor.Value);
                pictVer.Image = UmbralizarH(trackVer.Value);

                if (dataGrid.Rows.Count < 1)
                {
                    btnLimpiar.Enabled = false;
                    btnBorrar.Enabled = false;
                }
            }

            if ((tabControl1.SelectedIndex == 1) && (renderCargado))
            {
                try
                {
                    renderer.RemoveAllViewProps();
                    renderWindowControl1.Refresh();
                    renderWindowControl1_Load(sender, e);
                }
                catch
                {

                }
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

                // se debe ajustar la imagen normalizada, y umbralizada, de cada dicom segun se vaya cambiando el rangebar
                // se pregunta si se debe tambien umbralizar, o si solo es necesario normalizar y presentar la imagen normalizada
                pictTrans.Image = Normalizar(trackBar.Value, padre.actualV.segR * 2, padre.actualV.segR * 2, rangeBar.RangeMinimum, rangeBar.RangeMaximum);
                pictHor.Image = NormalizarH(this.trackHor.Value, rangeBar.RangeMinimum, rangeBar.RangeMaximum);
                pictVer.Image = NormalizarV(this.trackVer.Value, rangeBar.RangeMinimum, rangeBar.RangeMaximum);

                pictGradiente.Image = CrearGradiente(rangeBar.RangeMinimum, rangeBar.RangeMaximum);

                padre.actualV.normalizacion2D[0] = rangeBar.RangeMinimum;

                // se prepara la serie Selected
                chart1.Series[0].Points.Clear();
                chart1.Series[0].Points.AddXY(minPixelValue, 0);
                chart1.Series[0].Points.AddXY(rangeBar.RangeMinimum, 0);
                chart1.Series[0].Points.AddXY(rangeBar.RangeMinimum, maxValAll);
                chart1.Series[0].Points.AddXY(rangeBar.RangeMaximum, maxValAll);
                chart1.Series[0].Points.AddXY(rangeBar.RangeMaximum, 0);
                chart1.Series[0].Points.AddXY(maxPixelValue, 0);

                habilitarCambios = true;
            }
        }

        private void numNmax_ValueChanged(object sender, EventArgs e)
        {
            if (habilitarCambios)
            {
                habilitarCambios = false;

                rangeBar.RangeMaximum = Convert.ToInt32(numNmax.Value);

                // se debe ajustar la imagen normalizada, y umbralizada, de cada dicom segun se vaya cambiando el rangebar
                // se pregunta si se debe tambien umbralizar, o si solo es necesario normalizar y presentar la imagen normalizada
                pictTrans.Image = Normalizar(trackBar.Value, padre.actualV.segR * 2, padre.actualV.segR * 2, rangeBar.RangeMinimum, rangeBar.RangeMaximum);
                pictHor.Image = NormalizarH(this.trackHor.Value, rangeBar.RangeMinimum, rangeBar.RangeMaximum);
                pictVer.Image = NormalizarV(this.trackVer.Value, rangeBar.RangeMinimum, rangeBar.RangeMaximum);

                pictGradiente.Image = CrearGradiente(rangeBar.RangeMinimum, rangeBar.RangeMaximum);

                padre.actualV.normalizacion2D[1] = rangeBar.RangeMaximum;

                // se prepara la serie Selected
                chart1.Series[0].Points.Clear();
                chart1.Series[0].Points.AddXY(minPixelValue, 0);
                chart1.Series[0].Points.AddXY(rangeBar.RangeMinimum, 0);
                chart1.Series[0].Points.AddXY(rangeBar.RangeMinimum, maxValAll);
                chart1.Series[0].Points.AddXY(rangeBar.RangeMaximum, maxValAll);
                chart1.Series[0].Points.AddXY(rangeBar.RangeMaximum, 0);
                chart1.Series[0].Points.AddXY(maxPixelValue, 0);

                habilitarCambios = true;
            }
        }

        private void numAmplitud_ValueChanged(object sender, EventArgs e)
        {
            this.chart1.ChartAreas[0].AxisY.Maximum = Convert.ToDouble(this.numAmplitud.Value);
        }

        private void renderWindowControl1_Load(object sender, EventArgs e)
        {
            //padre.ShowWaiting("Espere mientras RockVision prepara la visualizacion 3D");
            try
            {
                renderWindow = renderWindowControl1.RenderWindow;
                renderer = renderWindow.GetRenderers().GetFirstRenderer();
                renderer.SetBackground(0, 0, 0);
                renderer.SetInteractive(0);

                // Visual3Dcortes();

                //Visual3Dplanos();
                Visual3DDispersion();
                btnResetRot_Click(sender, e);

                renderCargado = true;
                dateRenderCargado = DateTime.Now;
            }
            catch
            {
                MessageBox.Show("Error al cargar el control VTK");
            }
            //padre.CloseWaiting();
        }

        /// <summary>
        /// Se encarga de visualizar los 3 planos secantes en el control VTK
        /// </summary>
        private void Visual3Dplanos()
        {
            /*

            // se preparan y se guardan las texturas a cargar en los planos de corte
            imagenXY = null;
            imagenXZ = null;
            imagenYZ = null;

            if (chkNorm.Checked)
            {
                imagenXY = Normalizar(trckPlanoXY.Value, rangeBar.RangeMinimum, rangeBar.RangeMaximum);
                imagenXZ = NormalizarH(trckPlanoXZ.Value, rangeBar.RangeMinimum, rangeBar.RangeMaximum);
                imagenYZ = NormalizarV(trckPlanoYZ.Value, rangeBar.RangeMinimum, rangeBar.RangeMaximum);
            }
            else
            {
                imagenXY = Umbralizar(trckPlanoXY.Value);
                imagenXY = UmbralizarH(trckPlanoXZ.Value);
                imagenXY = UmbralizarV(trckPlanoYZ.Value);                
            }

            imagenXY.MakeTransparent(Color.Black);
            imagenXZ.MakeTransparent(Color.Black);
            imagenYZ.MakeTransparent(Color.Black);

            imagenXY.Save("imagenXY.png", ImageFormat.Png);
            imagenXZ.Save("imagenXZ.png", ImageFormat.Png);
            imagenYZ.Save("imagenYZ.png", ImageFormat.Png);

            // se leen las imagenes desde disco para cargar a los planos
            vtkPNGReader jpegreaderXY = new vtkPNGReader();
            jpegreaderXY.SetFileName("imagenXY.png");

            vtkPNGReader jpegreaderXZ = new vtkPNGReader();
            jpegreaderXZ.SetFileName("imagenXZ.png");

            vtkPNGReader jpegreaderYZ = new vtkPNGReader();
            jpegreaderYZ.SetFileName("imagenYZ.png");

            vtkPlaneSource planoXY = new vtkPlaneSource();
            vtkPlaneSource planoXZ = new vtkPlaneSource();
            vtkPlaneSource planoYZ = new vtkPlaneSource();

            factor = 100;
            double lx = Convert.ToDouble(imagenXY.Width) / factor;
            double ly = Convert.ToDouble(imagenXY.Height) / factor; 
            double lz = Convert.ToDouble(imagenXZ.Width) / factor;

            double posicionZ = ((imagenXZ.Width / Convert.ToDouble(2)) - Convert.ToDouble(trckPlanoXY.Value*padre.actualV.datacubo.factorZ)) / factor;
            double posicionY = ((imagenXY.Height / Convert.ToDouble(2)) - Convert.ToDouble(trckPlanoXZ.Value)) / factor;
            double posicionX = ((imagenXY.Width / Convert.ToDouble(2)) - Convert.ToDouble(trckPlanoYZ.Value)) / factor;

            planoXY = CrearPlano2(0, 0, posicionZ, lx, ly, 0, "z");
            planoXZ = CrearPlano2(0, posicionY, 0, lx, 0, lz, "y");
            planoYZ = CrearPlano2(posicionX, 0, 0, 0, ly, lz, "x");

            vtkTexture textureXY = new vtkTexture();
            textureXY.SetInputConnection(jpegreaderXY.GetOutputPort());

            vtkTexture textureXZ = new vtkTexture();
            textureXZ.SetInputConnection(jpegreaderXZ.GetOutputPort());

            vtkTexture textureYZ = new vtkTexture();
            textureYZ.SetInputConnection(jpegreaderYZ.GetOutputPort());

            // plano XY
            vtkPolyDataMapper planeMapperXY = vtkPolyDataMapper.New();
            planeMapperXY.SetInputConnection(planoXY.GetOutputPort());

            texturedPlaneXY = new vtkActor();
            texturedPlaneXY.SetMapper(planeMapperXY);
            texturedPlaneXY.SetTexture(textureXY);

            // plano XZ
            vtkPolyDataMapper planeMapperXZ = vtkPolyDataMapper.New();
            planeMapperXZ.SetInputConnection(planoXZ.GetOutputPort());

            texturedPlaneXZ = new vtkActor();
            texturedPlaneXZ.SetMapper(planeMapperXZ);
            texturedPlaneXZ.SetTexture(textureXZ);

            // plano YZ
            vtkPolyDataMapper planeMapperYZ = vtkPolyDataMapper.New();
            planeMapperYZ.SetInputConnection(planoYZ.GetOutputPort());

            texturedPlaneYZ = new vtkActor();
            texturedPlaneYZ.SetMapper(planeMapperYZ);
            texturedPlaneYZ.SetTexture(textureYZ);

            renderer.AddActor(texturedPlaneXY);
            renderer.AddActor(texturedPlaneXZ);
            renderer.AddActor(texturedPlaneYZ);

            vtkAxesActor axes = new vtkAxesActor();
            vtkTextProperty prop = new vtkTextProperty();
            prop.SetColor(1,1,1);
            prop.SetBold(1);
            prop.SetFontSize(20);
            //prop.SetOpacity(0.8);
            axes.GetXAxisCaptionActor2D().SetCaptionTextProperty(prop);
            axes.GetYAxisCaptionActor2D().SetCaptionTextProperty(prop);
            axes.GetZAxisCaptionActor2D().SetCaptionTextProperty(prop);

            axes.GetXAxisCaptionActor2D().GetTextActor().SetTextScaleModeToNone();
            axes.GetYAxisCaptionActor2D().GetTextActor().SetTextScaleModeToNone();
            axes.GetZAxisCaptionActor2D().GetTextActor().SetTextScaleModeToNone();

            axes.SetShaftTypeToCylinder();
            axes.SetTotalLength(1, 1, 1);
            axes.SetTipTypeToSphere();
            
            vtkRenderWindowInteractor renderWindowInteractor = new vtkRenderWindowInteractor();
            renderWindowInteractor.SetRenderWindow(renderWindow);
            renderWindowInteractor.SetInteractorStyle(new vtkInteractorStyleImage());
           
            vtkOrientationMarkerWidget widget = new vtkOrientationMarkerWidget();
            widget.SetOutlineColor(0.9300, 0.5700, 0.1300);
            widget.SetOrientationMarker(axes);
            widget.SetInteractor(renderWindowInteractor);
            //widget.SetViewport(0.0, 0.0, 0.4, 0.4);
            widget.SetEnabled(1);
            widget.InteractiveOff();

            
                        
            //axes.GetXAxisTipProperty().SetRepresentationToWireframe();
            //axes.GetXAxisTipProperty().SetOpacity(0.4);
            //axes.GetXAxisShaftProperty().SetRepresentationToWireframe();
            //axes.GetXAxisShaftProperty().SetOpacity(0.4);

            //axes.GetYAxisTipProperty().SetRepresentationToWireframe();
            //axes.GetYAxisTipProperty().SetOpacity(0.4);
            //axes.GetYAxisShaftProperty().SetRepresentationToWireframe();
            //axes.GetYAxisShaftProperty().SetOpacity(0.4);

            //axes.GetZAxisTipProperty().SetRepresentationToWireframe();
            //axes.GetZAxisTipProperty().SetOpacity(0.4);
            //axes.GetZAxisShaftProperty().SetRepresentationToWireframe();
            //axes.GetZAxisShaftProperty().SetOpacity(0.4);
            

            //renderer.AddActor(axes);

            //vtkTransform transladar = new vtkTransform();
            //transladar.Translate((3 * lx / 2), 0, lz/2);
            //axes.SetUserTransform(transladar);

            //renderer.AddActor(axes);

            renderWindowInteractor.Start();
            
            */
        }

        public System.IntPtr GetPixels(Bitmap imagen)
        {
            unsafe
            {

                BitmapData bmd = imagen.LockBits(new Rectangle(0, 0, imagen.Width, imagen.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, imagen.PixelFormat);

                int indice = 0;
                short[] colores = new short[imagen.Height * imagen.Width];



                int pixelSize = 3;
                int i, j, j1, i1;
                byte b;

                for (i = 0; i < bmd.Height; ++i)
                {
                    byte* row = (byte*)bmd.Scan0 + (i * bmd.Stride);
                    i1 = i * bmd.Width;

                    for (j = 0; j < bmd.Width; ++j)
                    {
                        // dado que es una imagen en escala de grises => los canales R G y B son el mismo valor
                        j1 = j * pixelSize;
                        b = row[j1];            // Red  

                        indice = i1 + j;
                        colores[indice] = (short)Convert.ToInt32(row[j1]);
                    }
                }

                imagen.UnlockBits(bmd);

                fixed (short* pArray = colores)
                {
                    IntPtr intPtr = new IntPtr((void*)pArray);
                    return intPtr;
                }
            }
        }

        /// <summary>
        /// Crea un plano con posicion custom y normal a uno de los tres ejes cartesianos
        /// </summary>
        /// <param name="ox">centro del plano</param>
        /// <param name="oy">centro del plano</param>
        /// <param name="oz">centro del plano</param>
        /// <param name="lx"></param>
        /// <param name="ly"></param>
        /// <param name="lz"></param>
        /// <param name="eje"></param>
        /// <returns></returns>
        public vtkPlaneSource CrearPlano2(double ox, double oy, double oz, double lx, double ly, double lz, string eje)
        {
            vtkPlaneSource planeSource = new vtkPlaneSource();

            planeSource.SetXResolution(1);
            planeSource.SetYResolution(1);

            double cx, cy, cz;

            switch (eje)
            {
                case "x": // plano perpendicular al eje X, se dibuja en el plano YZ

                    cx = ox;
                    cy = oy - (ly / 2);
                    cz = oz - (lz / 2);

                    planeSource.SetOrigin(cx, cy, cz);
                    planeSource.SetPoint1(cx, cy + ly, cz);
                    planeSource.SetPoint2(cx, cy, cz + lz);
                    planeSource.SetXResolution(Convert.ToInt32(ly));
                    planeSource.SetYResolution(Convert.ToInt32(lz));

                    planeSource.SetNormal(1, 0, 0);

                    break;

                case "y": // plano perpendicular al eje Y, se dibuja en el plano XZ

                    cx = ox - (lx / 2);
                    cy = oy;
                    cz = oz - (lz / 2);

                    planeSource.SetOrigin(cx, cy, cz);
                    planeSource.SetPoint1(cx + lx, cy, cz);
                    planeSource.SetPoint2(cx, cy, cz + lz);
                    planeSource.SetXResolution(Convert.ToInt32(lx));
                    planeSource.SetYResolution(Convert.ToInt32(lz));

                    planeSource.SetNormal(0, 1, 0);

                    break;

                case "z": // plano perpendicular al eje Z, se dibuja en el plano XY

                    cx = ox - (lx / 2);
                    cy = oy - (ly / 2);
                    cz = oz;

                    planeSource.SetOrigin(cx, cy, cz);
                    planeSource.SetPoint1(cx + lx, cy, cz);
                    planeSource.SetPoint2(cx, cy + ly, cz);
                    planeSource.SetXResolution(Convert.ToInt32(lx));
                    planeSource.SetYResolution(Convert.ToInt32(ly));

                    planeSource.SetNormal(0, 0, 1);

                    break;
            }

            return planeSource;
        }

        private void trckRotX_Scroll(object sender, EventArgs e)
        {
            double valorRotarX = Convert.ToInt32(Convert.ToDouble(trckRotX.Value) / Convert.ToDouble(4)) - lastRotX;
            lastRotX = Convert.ToDouble(trckRotX.Value) / Convert.ToDouble(4);

            vtkTransform t = new vtkTransform();
            t.RotateX((valorRotarX));

            renderer.GetActiveCamera().ApplyTransform(t);
            renderWindowControl1.Invalidate();
        }

        private void trckRotY_Scroll(object sender, EventArgs e)
        {
            double valorRotarY = Convert.ToInt32(Convert.ToDouble(trckRotY.Value) / Convert.ToDouble(4)) - lastRotY;
            lastRotY = Convert.ToDouble(trckRotY.Value) / Convert.ToDouble(4);

            vtkTransform t = new vtkTransform();
            t.RotateY((valorRotarY));

            renderer.GetActiveCamera().ApplyTransform(t);
            renderWindowControl1.Invalidate();
        }

        private void trckRotZ_Scroll(object sender, EventArgs e)
        {
            double valorRotarZ = Convert.ToInt32(Convert.ToDouble(trckRotZ.Value) / Convert.ToDouble(4)) - lastRotZ;
            lastRotZ = Convert.ToDouble(trckRotZ.Value) / Convert.ToDouble(4);

            vtkTransform t = new vtkTransform();
            t.RotateZ((valorRotarZ));

            renderer.GetActiveCamera().ApplyTransform(t);
            renderWindowControl1.Invalidate();
        }

        private void btnResetRot_Click(object sender, EventArgs e)
        {
            lastRotX = 0;
            lastRotY = 0;
            lastRotZ = 0;
            trckRotX.Value = 0;
            trckRotY.Value = 0;
            trckRotZ.Value = 0;

            //renderer.GetActiveCamera().SetPosition(position0[0], position0[1], position0[2]);
            renderer.GetActiveCamera().SetPosition(6.551632, 3.149799, 7.519487);
            renderer.GetActiveCamera().SetViewUp(-0.259344, 0.931174, -0.256232);
            renderer.GetActiveCamera().SetFocalPoint(-0.430526, -0.736878, 0.461197);

            renderer.ResetCameraClippingRange();
            //renderer.ResetCamera();

            renderWindowControl1.Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("error");
        }

        /// <summary>
        /// Visualizacion en modo dispersion
        /// </summary>
        public void Visual3DDispersion()
        {
            // se prepara algo de informacion necesaria

            factor = 100;
            double lx = Convert.ToDouble(padre.actualV.datacubo.diametroSegRV) / factor;
            double ly = Convert.ToDouble(padre.actualV.datacubo.diametroSegRV) / factor;

            int alto = Convert.ToInt16(padre.actualV.datacubo.diametroSegRV);
            int total = padre.actualV.datacubo.coresVertical[0].Count;
            int ancho = Convert.ToInt32(Convert.ToDouble(total) / Convert.ToDouble(alto));
            double lz = Convert.ToDouble(ancho) / factor;

            // se crea el cilindro de referencia
            vtkCylinderSource cylinderSource = vtkCylinderSource.New();
            cylinderSource.SetCenter(0.0, 0.0, 0.0);
            cylinderSource.SetRadius(lx / 2);
            cylinderSource.SetHeight(lz);
            cylinderSource.SetResolution(20);

            // Create a mapper and actor
            vtkPolyDataMapper mapper = vtkPolyDataMapper.New();
            mapper.SetInputConnection(cylinderSource.GetOutputPort());
            vtkActor cilindro = vtkActor.New();
            cilindro.SetMapper(mapper);
            cilindro.GetProperty().SetOpacity(0.15);
            cilindro.GetProperty().SetRepresentationToWireframe();

            vtkTransform t = new vtkTransform();
            t.RotateX((90));
            cilindro.SetUserTransform(t);

            renderer.AddActor(cilindro);


            Random rnd = new Random(Environment.TickCount);

            double centerx, centery, centerz;
            int color;

            double range = Convert.ToDouble(rangeBar.RangeMaximum - rangeBar.RangeMinimum);

            vtkPoints points = new vtkPoints();

            // se preparan los colores
            vtkUnsignedCharArray colors = new vtkUnsignedCharArray();
            colors.SetName("Colors");
            colors.SetNumberOfComponents(3);
            int probabilidad = 0;

            // se crean los puntos de dispersion

            bool grises = chkNorm.Checked;

            // se verifica si hay informacion de segmentacion. Si no hay entonces igual se grafica en grises
            if ((dataGrid.Rows.Count > 0) & (!chkUmbral.Checked)) grises = true;

            if (grises)
            {
                // se colocan los puntos en escala de grises

                // se recorren todos los puntos, o punto de por medio
                for (int k = 0; k < padre.actualV.datacubo.dataCube.Count; k = k + 4)
                {
                    for (int i = 0; i < Convert.ToInt16(padre.actualV.datacubo.diametroSegRV); i = i + 3)
                    {
                        for (int j = 0; j < Convert.ToInt16(padre.actualV.datacubo.diametroSegRV); j = j + 3)
                        {
                            // cada pixel se verifica si se grafica o no según la cantidad de puntos seleccionado en el trackbar trckTransparencia
                            probabilidad = rnd.Next(0, 100);
                            if (probabilidad < trckTransparencia.Value)
                            {
                                color = Convert.ToInt32(Convert.ToDouble(padre.actualV.datacubo.dataCube[k].pixelData[i * Convert.ToInt16(padre.actualV.datacubo.diametroSegRV) + j] - rangeBar.RangeMinimum) * ((double)255) / range);

                                // solo entran los pixeles con un color mayor o igual a 5
                                if (color >= 5)
                                {
                                    // el pixel entra a la visualizacion

                                    if (color < 5) color = 5;       // normalizacion de los limites
                                    if (color > 255) color = 255;

                                    // coordenadas del pixel
                                    centerz = (lz / 2) - (Convert.ToDouble(k) * padre.actualV.datacubo.factorZ / factor);
                                    centerx = (lx / 2) - (Convert.ToDouble(i) / factor);
                                    centery = (ly / 2) - (Convert.ToDouble(j) / factor);

                                    points.InsertNextPoint(centerx, centery, centerz);
                                    colors.InsertNextTuple3(color, color, color);
                                }
                            }

                        }
                    }
                }
            }
            else
            {
                // se colocan los puntos en modo segmentacion. un color para cada segmentacion

                int rojo = 0;
                int verde = 0;
                int azul = 0;

                // se recorren todos los puntos, o punto de por medio
                for (int k = 0; k < padre.actualV.datacubo.dataCube.Count; k = k + 4)
                {
                    for (int i = 0; i < Convert.ToInt16(padre.actualV.datacubo.diametroSegRV); i = i + 3)
                    {
                        for (int j = 0; j < Convert.ToInt16(padre.actualV.datacubo.diametroSegRV); j = j + 3)
                        {
                            // cada pixel se verifica si se grafica o no según la cantidad de puntos seleccionado en el trackbar trckTransparencia
                            probabilidad = rnd.Next(0, 100);
                            if (probabilidad < trckTransparencia.Value)
                            {
                                color = Convert.ToInt32(Convert.ToDouble(padre.actualV.datacubo.dataCube[k].pixelData[i * Convert.ToInt16(padre.actualV.datacubo.diametroSegRV) + j] - rangeBar.RangeMinimum) * ((double)255) / range);

                                // solo entran los pixeles con un color mayor o igual a 5
                                if (color >= 5)
                                {
                                    // el pixel entra a la visualizacion

                                    // se busca a que intervalo pertenece el pixel
                                    bool perteneceSegmentacion = false;

                                    int iseg = 0;
                                    int pixel = 0;
                                    int minimo = 0;
                                    int maximo = 0;
                                    for (iseg = 0; iseg < dataGrid.Rows.Count; iseg++)
                                    {
                                        pixel = padre.actualV.datacubo.dataCube[k].pixelData[i * Convert.ToInt16(padre.actualV.datacubo.diametroSegRV) + j];
                                        minimo = Convert.ToInt32(dataGrid.Rows[iseg].Cells[1].Value);
                                        maximo = Convert.ToInt32(dataGrid.Rows[iseg].Cells[2].Value);
                                        if ((pixel >= minimo) & (pixel <= maximo))
                                        {
                                            perteneceSegmentacion = true;
                                            break;
                                        }
                                    }

                                    if (perteneceSegmentacion)
                                    {
                                        if (((bool)(dataGrid.Rows[iseg].Cells[0].Value)) == true)
                                        {
                                            rojo = Convert.ToInt32(dataGrid.Rows[iseg].Cells[3].Style.BackColor.R);
                                            verde = Convert.ToInt32(dataGrid.Rows[iseg].Cells[3].Style.BackColor.G);
                                            azul = Convert.ToInt32(dataGrid.Rows[iseg].Cells[3].Style.BackColor.B);

                                            // coordenadas del pixel
                                            centerz = (lz / 2) - (Convert.ToDouble(k) * padre.actualV.datacubo.factorZ / factor);
                                            centerx = (lx / 2) - (Convert.ToDouble(i) / factor);
                                            centery = (ly / 2) - (Convert.ToDouble(j) / factor);

                                            points.InsertNextPoint(centerx, centery, centerz);
                                            colors.InsertNextTuple3(rojo, verde, azul);
                                        }
                                    }
                                }
                            }

                        }
                    }
                }
            }

            vtkPolyData pointsPolyData = new vtkPolyData();
            pointsPolyData.SetPoints(points);

            vtkVertexGlyphFilter vertexFilter = new vtkVertexGlyphFilter();
            vertexFilter.SetInputConnection(pointsPolyData.GetProducerPort());
            vertexFilter.Update();

            vtkPolyData polydata = new vtkPolyData();
            polydata.ShallowCopy(vertexFilter.GetOutput());

            polydata.GetPointData().SetScalars(colors);

            vtkPolyDataMapper mapperPoints = vtkPolyDataMapper.New();
            mapperPoints.SetInputConnection(polydata.GetProducerPort());

            vtkActor actorPoints = new vtkActor();
            actorPoints.SetMapper(mapperPoints);
            actorPoints.GetProperty().SetPointSize(2);
            actorPoints.GetProperty().SetOpacity(0.5);

            renderer.AddActor(actorPoints);

            /*
            vtkAxesActor axes = new vtkAxesActor();
            vtkTextProperty prop = new vtkTextProperty();
            prop.SetColor(1, 1, 1);
            prop.SetBold(1);
            prop.SetFontSize(20);
            //prop.SetOpacity(0.8);
            axes.GetXAxisCaptionActor2D().SetCaptionTextProperty(prop);
            axes.GetYAxisCaptionActor2D().SetCaptionTextProperty(prop);
            axes.GetZAxisCaptionActor2D().SetCaptionTextProperty(prop);

            axes.GetXAxisCaptionActor2D().GetTextActor().SetTextScaleModeToNone();
            axes.GetYAxisCaptionActor2D().GetTextActor().SetTextScaleModeToNone();
            axes.GetZAxisCaptionActor2D().GetTextActor().SetTextScaleModeToNone();

            axes.SetShaftTypeToCylinder();
            axes.SetTotalLength(1, 1, 1);
            axes.SetTipTypeToSphere();

            vtkRenderWindowInteractor renderWindowInteractor = new vtkRenderWindowInteractor();
            renderWindowInteractor.SetRenderWindow(renderWindow);
            renderWindowInteractor.SetInteractorStyle(new vtkInteractorStyleImage());

            vtkOrientationMarkerWidget widget = new vtkOrientationMarkerWidget();
            widget.SetOutlineColor(0.9300, 0.5700, 0.1300);
            widget.SetOrientationMarker(axes);
            widget.SetInteractor(renderWindowInteractor);
            //widget.SetViewport(0.0, 0.0, 0.4, 0.4);
            widget.SetEnabled(1);
            widget.InteractiveOn();*/

            renderWindowControl1.Refresh();
        }

        private void trckTransparencia_Scroll(object sender, EventArgs e)
        {
            if (mouseDownTransparencia) return;

            renderer.RemoveAllViewProps();
            renderWindowControl1.Refresh();

            Visual3DDispersion();
        }

        private void trckTransparencia_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDownTransparencia = true;
        }

        private void trckTransparencia_MouseUp(object sender, MouseEventArgs e)
        {
            if (!mouseDownTransparencia) return;

            mouseDownTransparencia = false;

            renderer.RemoveAllViewProps();
            renderWindowControl1.Refresh();

            Visual3DDispersion();
        }

        private void rangeBar_RangeChanged(object sender, EventArgs e)
        {
            // se cambio el rango de la normalizacion y se debe re-dibujar el VTK

            // se verifica que se esté en el tab de VTK y se halla cargado el renderer

            if ((tabControl1.SelectedIndex == 1) && (renderCargado))
            {
                renderer.RemoveAllViewProps();
                Visual3DDispersion();
                btnResetRot_Click(sender, e);
                renderWindowControl1.Refresh();
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // se cambio la pestaña del tabcontrol y se verifica que recien no se halla cargado el renderwindow
            // si el renderwindow ya ha sido cargado hace tiempo se pide re-dibujar el VTK para tomar los cambios en normalizacion y umbralizacion

            DateTime now = DateTime.Now;

            if ((now - dateRenderCargado).TotalMilliseconds >= 500)
            { 
                // han pasado mas de 500ms desde que se cargo el renderwindow, asi que se debe re-dibujar el vtk
                if ((tabControl1.SelectedIndex == 1) && (renderCargado))
                {
                    renderer.RemoveAllViewProps();
                    Visual3DDispersion();
                    btnResetRot_Click(sender, e);
                    renderWindowControl1.Refresh();
                }
            }
        }

        private void trackVer_Scroll(object sender, EventArgs e)
        {
            // se debe ajustar la imagen normalizada, y umbralizada, de cada dicom segun se vaya cambiando el trackbar

            if (chkNorm.Checked)
            {
                //pictTrans.Image = Normalizar(trackBar.Value, rangeBar.RangeMinimum, rangeBar.RangeMaximum);
                //pictHor.Image = NormalizarH(trackHor.Value, rangeBar.RangeMinimum, rangeBar.RangeMaximum);
                pictVer.Image = NormalizarV(trackVer.Value, rangeBar.RangeMinimum, rangeBar.RangeMaximum);
            }
            else
            {
                //pictTrans.Image = Umbralizar(trackBar.Value);
                //pictHor.Image = UmbralizarH(trackHor.Value);
                pictVer.Image = UmbralizarV(trackVer.Value);
            }

            lblVer.Text = "Corte " + (trackVer.Value + 1) + " de " + padre.actualV.datacubo.dataCube[0].selector.Columns.Data;

            pictTrans.Invalidate();
            pictHor.Invalidate();
            pictVer.Invalidate();
        }

        private void pictVer_Paint(object sender, PaintEventArgs e)
        {
            // se pintan la linea de posicion
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            double imgWidth = pictVer.Image.Width;
            double imgHeight = pictVer.Image.Height;
            double boxWidth = pictVer.Size.Width;
            double boxHeight = pictVer.Size.Height;

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

            // se averigua el factor de escalado del corte vertical
            double alto = padre.actualV.datacubo.diametroSegRV;
            double total = padre.actualV.datacubo.coresHorizontal[0].Count;
            double ancho = Convert.ToInt32(total / alto);
            int factor = Convert.ToInt32(ancho / Convert.ToInt32(padre.actualV.datacubo.dataCube.Count));

            int pos = Convert.ToInt32((trackBar.Value * scale * factor) + xcero);
            e.Graphics.DrawLine(brocha2, pos, 0, pos, pictVer.Height);

            pos = Convert.ToInt32((trackHor.Value * scale) + ycero);
            e.Graphics.DrawLine(brocha2, 0, pos, pictVer.Width, pos);
        }

        private void dataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // se hacen efectivos cambios como el check/uncheck de la columna 0
            dataGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dataGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                // esta en modo 3D y se hizo algún cambio en el dataGridView
                // se vuelve a dibujar

                renderer.RemoveAllViewProps();
                Visual3DDispersion();
                //btnResetRot_Click(sender, e);
                renderWindowControl1.Refresh();
            }
        }
    }
}
