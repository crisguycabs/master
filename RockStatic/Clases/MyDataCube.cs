using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using EvilDICOM.Core;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Selection;
using System.Drawing;
using System.Drawing.Imaging;


namespace RockStatic
{
    /// <summary>
    /// Wrapper para la libreria EvilDICOM (Rex Cardan, http://www.rexcardan.com/evildicom/) con modificaciones propias para ser manejadas por la suite RockUIS.
    /// Un objeto MyDataCube contiene un List de MyDicom, es decir un datacubo. Contiene las rutinas necesarias para manejar el datacubo, crear el histograma y los cortes laterales
    /// </summary>
    public class MyDataCube
    {
        /// <summary>
        /// List de MyDicom que conforman el datacubo
        /// </summary>
        public List<MyDicom> dataCube = null;

        /// <summary>
        /// Guarda la informacion del histograma en 100 bins
        /// </summary>
        public uint[] histograma = null;

        /// <summary>
        /// Marcas de clase del histograma de 100 bins
        /// </summary>
        public ushort[] bins = null;

        /// <summary>
        /// List que contiene los List de ushort que contienen los pixeles CT para cada corte horizontal del core
        /// </summary>
        public List<ushort>[] coresHorizontal = null;

        /// <summary>
        /// List que contiene los List de ushort que contienen los pixeles CT para cada corte vertical del core
        /// </summary>
        public List<ushort>[] coresVertical = null;

        /// <summary>
        /// Indica el ancho de la imagen segmentada resultante
        /// </summary>
        public int widthSeg = 0;

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public MyDataCube()
        {
            dataCube = null;
            histograma = null;
            bins = null;
            coresHorizontal = null;
            coresVertical = null;
            
        }

        /// <summary>
        /// Constructor con asignación
        /// </summary>
        /// <param name="rutas">Array de rutas de los DICOM a cargar</param>
        public MyDataCube(string[] rutas)
        {
            dataCube = new List<MyDicom>();

            for (int i = 0; i < rutas.Length; i++)
            {
                dataCube.Add(new MyDicom(rutas[i]));
            }

            histograma = new uint[100];
            bins = new ushort[100];
            coresHorizontal = new List<ushort>[Convert.ToInt16(dataCube[0].selector.Rows.Data)];
            coresVertical = new List<ushort>[Convert.ToInt16(dataCube[0].selector.Columns.Data)];
            
        }

        /// <summary>
        /// Constructor con asignación
        /// </summary>
        /// <param name="rutas">Array de rutas de los DICOM a cargar</param>
        public MyDataCube(List<string> rutas)
        {
            dataCube = new List<MyDicom>();

            for (int i = 0; i < rutas.Count; i++)
            {
                dataCube.Add(new MyDicom(rutas[i]));
            }

            histograma = new uint[100];
            bins = new ushort[100];
            coresHorizontal = new List<ushort>[Convert.ToInt16(dataCube[0].selector.Rows.Data)];
            coresVertical = new List<ushort>[Convert.ToInt16(dataCube[0].selector.Columns.Data)];
            
        }

        /// <summary>
        /// Segmenta la totalidad del datacubo a una seccion rectangular
        /// </summary>
        /// <param name="centerX">Centro X de la seccion rectangular, con cero en la esquina superior izquierda</param>
        /// <param name="centerY">Centro Y de la seccion rectangular, con cero en la esquina superior izquierda</param>
        /// <param name="altoRect">Distancia del centro al borde superior del rectangulo</param>
        /// <param name="anchoRect">Distancia del centro al borde lateral del rectangulo</param>
        public void CropRect(int centerX, int centerY, int altoRect, int anchoRect)
        {
            for (int i = 0; i < dataCube.Count; i++)
            {
                dataCube[i].CropCTRectangle(dataCube[i].pixelData, centerX, centerY, altoRect, anchoRect, Convert.ToInt32(dataCube[i].selector.Columns.Data), Convert.ToInt32(dataCube[i].selector.Rows.Data));
            }
        }

        /// <summary>
        /// Segmenta la totalidad del datacubo a una seccion circular
        /// </summary>
        /// <param name="centerX">Centro X de la seccion rectangular, con cero en la esquina superior izquierda</param>
        /// <param name="centerY">Centro Y de la seccion rectangular, con cero en la esquina superior izquierda</param>
        /// <param name="radio">Radio del circulo a extraer</param>
        public void CropCirc(int centerX, int centerY, int radio)
        {
            for (int i = 0; i < dataCube.Count; i++)
            {
                dataCube[i].CropCTCircle(dataCube[i].pixelData, centerX, centerY, radio, Convert.ToInt32(dataCube[i].selector.Columns.Data), Convert.ToInt32(dataCube[i].selector.Rows.Data));
            }
        }

        /// <summary>
        /// Genera la totalidad de los bitmap del datacubo
        /// </summary>
        public void CrearBitmap()
        {
            for (int i = 0; i < dataCube.Count; i++)
            {
                dataCube[i].CreateBitmap(dataCube[i].pixelData, Convert.ToInt32(dataCube[i].selector.Columns.Data), Convert.ToInt32(dataCube[i].selector.Rows.Data));
            }
        }

        /// <summary>
        /// Genera la totalidad de los bitmap del datacubo usando threads
        /// </summary>
        public void CrearBitmapThread()
        {
            int nbmp = dataCube.Count;
            ManualResetEvent[] doneEvents = new ManualResetEvent[nbmp];
            AuxThread[] threads = new AuxThread[nbmp];

            int width = Convert.ToInt32(dataCube[0].selector.Columns.Data);
            int height = Convert.ToInt32(dataCube[0].selector.Rows.Data);

            for (int i = 0; i < nbmp; i++)
            {
                doneEvents[i] = new ManualResetEvent(false);
                AuxThread thread = new AuxThread(this.dataCube[i].pixelData, width, height, doneEvents[i]);
                threads[i] = thread;
                ThreadPool.QueueUserWorkItem(thread.ThreadCrearBitmap, i);
            }

            foreach (var e in doneEvents)
                e.WaitOne();

            for (int i = 0; i < nbmp; i++)
            {
                this.dataCube[i].bmp = threads[i].Corte;
            }
        }

        /// <summary>
        /// Genera la totalidad de las segmentaciones circulares de cores usando threads
        /// </summary>
        /// <param name="area">Objeto CCuadrado con la información del area a segmentar</param>
        public void SegCircThread(CCuadrado area)
        {
            int nseg = dataCube.Count;
            ManualResetEvent[] doneEvents = new ManualResetEvent[nseg];
            AuxThread[] threads = new AuxThread[nseg];

            int width = Convert.ToInt32(dataCube[0].selector.Columns.Data);
            int height = Convert.ToInt32(dataCube[0].selector.Rows.Data);

            for (int i = 0; i < nseg; i++)
            {
                doneEvents[i] = new ManualResetEvent(false);
                AuxThread thread = new AuxThread(this.dataCube[i].pixelData, area.x, area.y, area.width, width, height, doneEvents[i]);
                threads[i] = thread;
                ThreadPool.QueueUserWorkItem(thread.ThreadSegmentar, i);
            }

            foreach (var e in doneEvents)
                e.WaitOne();

            for (int i = 0; i < nseg; i++)
            {
                this.dataCube[i].segCore = threads[i].Segmentacion;
            }
        }

        /// <summary>
        /// Genera la totalidad de los bitmap del datacubo normalizando segun los límites que se le pasan
        /// </summary>
        /// <param name="minNormalizacion">Valor minimo CT de la normalizacion</param>
        /// <param name="maxNormalizacion">Valor maximo CT de la normalizacion</param>
        public void CrearBitmap(int minNormalizacion, int maxNormalizacion)
        {
            for (int i = 0; i < dataCube.Count; i++)
            {
                dataCube[i].CreateBitmap(dataCube[i].pixelData, Convert.ToInt32(dataCube[i].selector.Columns.Data), Convert.ToInt32(dataCube[i].selector.Rows.Data), minNormalizacion, maxNormalizacion);
            }
        }

        /// <summary>
        /// Genera el histograma y las marcas de clase para el datacubo, con 100 bins
        /// </summary>
        public void GenerarHistograma()
        {
            // se busca el menor y el mayor valor del datacubo
            ushort minimo = dataCube[0].pixelData.Min();
            ushort maximo = dataCube[0].pixelData.Max();

            for (int i = 0; i < dataCube.Count; i++)
            {
                if (dataCube[i].pixelData.Min() < minimo)
                {
                    minimo = dataCube[i].pixelData.Min();
                }
                else if (dataCube[i].pixelData.Max() > maximo)
                {
                    maximo = dataCube[i].pixelData.Max();
                }

            }

            // se calculan los limites de cada marca de clase
            double step = (double)(((double)(maximo - minimo)) / 100);
            ushort[] limites = new ushort[101];
            double[] tempLimites = new double[101];
            limites[0] = minimo;
            for (int i = 1; i < 101; i++)
            {
                tempLimites[i] = tempLimites[i - 1] + step;
                limites[i] = (ushort)(tempLimites[i]);
            }
            limites[100] = maximo;

            uint bincount = 0;
            int ibin = 0;

            List<ushort> pixelsOrdenados = new List<ushort>();
            for (int i = 0; i < dataCube.Count; i++)
            {
                // se genera una copia de los pixeles para poder ordenarlos
                pixelsOrdenados.Clear();
                for (int j = 0; j < dataCube[i].pixelData.Count; j++)
                    pixelsOrdenados.Add(dataCube[i].pixelData[j]);

                // se ordenan los pixeles
                pixelsOrdenados.Sort();

                // se empiezan a contar cuantos elementos caben en cada bin
                bincount = 0;
                ibin = 1;
                for (int j = 0; j < pixelsOrdenados.Count; j++)
                {
                    if (pixelsOrdenados[j] <= limites[ibin])
                        bincount++;
                    else
                    {
                        histograma[ibin - 1] = histograma[ibin - 1] + bincount;
                        bincount = 1;
                        ibin++;
                        if (ibin > 100)
                            ibin = 100;
                    }
                }
            }

            // se calculan las marcas de clase
            bins = new ushort[100];
            for (int i = 0; i < bins.Length; i++)
            {
                bins[i] = (ushort)((limites[i] + limites[i + 1]) / 2);
            }
        }

        /// <summary>
        /// Se genera un plano de core horizontal que corresponde al indice que se pasa como argumento
        /// </summary>
        /// <param name="indice">Numero de la fila que se debe extraer de cada DICOM</param>
        /// <returns>Core horizontal generado en la posicion indicada</returns>
        public List<ushort> GenerarCoreHorizontal(int indice)
        {
            // se genera una MATRIZ para guardar, temporalmente, los numeros CT extraidos de todo el data cubo para cada imagen de corte horizontal
            // una vez extraida la matriz se mapea a una imagen Bitmap

            // se crea la matriz temporal y se inicializa
            int alto = Convert.ToInt16(this.widthSeg);
            int ancho = dataCube.Count;
            ushort[][] temp = new ushort[alto][];
            for (int i = 0; i < alto; i++)
                temp[i] = new ushort[ancho];

            int a = 0;
            int ini = indice * Convert.ToInt16(this.widthSeg);

            // se empieza a llenar cada columna de la imagen nueva con la fila de cada DICOM
            for (int j = 0; j < ancho; j++)
            {
                for (int i = 0; i < alto; i++)
                {
                    temp[i][j] = dataCube[j].segCore[ini + i];
                }
                a++;
            }

            // se calcula el factor de escalado debido al espaciado entre Slides
            double resZ = Convert.ToDouble(dataCube[0].selector.SliceThickness.Data);
            double resXY = Convert.ToDouble(dataCube[0].selector.PixelSpacing.Data_[0]);
            int factor = Convert.ToInt32(resZ / resXY); { }

            // se convierte a un List<ushort> para poder usarlo en el mapeo a Bitmap
            List<ushort> pixels16 = new List<ushort>();
            for (int i = 0; i < alto; i++)
                for (int j = 0; j < ancho; j++)
                    for (int k = 0; k < factor; k++)
                        pixels16.Add(temp[i][j]);

            return pixels16;
        }

        /// <summary>
        /// Se genera un plano de core Vertical que corresponde al indice que se pasa como argumento
        /// </summary>
        /// <param name="indice">Numero de la fila que se debe extraer de cada DICOM</param>
        /// <returns>Core vertical generado en la posicion indicada</returns>
        public List<ushort> GenerarCoreVertical(int indice)
        {
            // se genera una MATRIZ para guardar, temporalmente, los numeros CT extraidos de todo el data cubo para cada imagen de corte horizontal
            // una vez extraida la matriz se mapea a una imagen Bitmap

            // se crea la matriz temporal y se inicializa
            int alto = Convert.ToInt16(this.widthSeg);
            int ancho = dataCube.Count;
            ushort[][] temp = new ushort[alto][];
            for (int i = 0; i < alto; i++)
                temp[i] = new ushort[ancho];

            int columnas = Convert.ToInt16(this.widthSeg);
            int offset = 0;

            // se empieza a llenar cada columna de la imagen nueva con la fila de cada DICOM
            for (int j = 0; j < ancho; j++)
            {
                for (int i = 0; i < alto; i++)
                {
                    offset = i * columnas;
                    temp[i][j] = dataCube[j].pixelData[indice + offset];
                }
            }

            // se calcula el factor de escalado debido al espaciado entre Slides
            double resZ = Convert.ToDouble(dataCube[0].selector.SliceThickness.Data);
            double resXY = Convert.ToDouble(dataCube[0].selector.PixelSpacing.Data_[0]);
            int factor = Convert.ToInt32(resZ / resXY);

            // se convierte a un List<ushort> para poder usarlo en el mapeo a Bitmap
            List<ushort> pixels16 = new List<ushort>();
            for (int i = 0; i < alto; i++)
                for (int j = 0; j < ancho; j++)
                    for (int k = 0; k < factor; k++)
                        pixels16.Add(temp[i][j]);

            return pixels16;
        }

        /// <summary>
        /// Genera todos y cada uno de los planos de corte horizontales del core. No genera imágenes sino los List de ushort para cada plano de corte
        /// </summary>
        public void GenerarCoresHorizontales()
        {
            // se genera un List<ushort> por cada pixel de altura de un DICOM
            coresHorizontal = new List<ushort>[widthSeg];

            // se calcula el factor de escalado debido al espaciado entre Slides
            double resZ = Convert.ToDouble(dataCube[0].selector.SliceThickness.Data);
            double resXY = Convert.ToDouble(dataCube[0].selector.PixelSpacing.Data_[0]);
            int factor = Convert.ToInt32(resZ / resXY);

            for (int i = 0; i < coresHorizontal.Length; i++)
            {
                coresHorizontal[i] = new List<ushort>();
                coresHorizontal[i] = GenerarCoreHorizontal(i);
            }
        }

        /// <summary>
        /// Genera todos y cada uno de los planos de corte verticales del datacubo. No genera imágenes sino los List de ushort para cada plano de corte
        /// </summary>
        public void GenerarCoresVerticales()
        {
            // se genera un List<ushort> por cada pixel de ancho de un DICOM
            coresVertical = new List<ushort>[widthSeg];

            // se calcula el factor de escalado debido al espaciado entre Slides
            double resZ = Convert.ToDouble(dataCube[0].selector.SliceThickness.Data);
            double resXY = Convert.ToDouble(dataCube[0].selector.PixelSpacing.Data_[0]);
            int factor = Convert.ToInt32(resZ / resXY);

            for (int i = 0; i < coresVertical.Length; i++)
            {
                coresVertical[i] = new List<ushort>();
                coresVertical[i] = GenerarCoreVertical(i);
            }
        }

        /// <summary>
        /// Se mapea un List de ushort, obtenido de un corte horizontal/vertical a una imagen Bitmap
        /// </summary>
        /// <param name="pixels16">List de ushort que contiene la informacion CT de cada pixel</param>
        /// <param name="width">Ancho deseado de la imagen resultante</param>
        /// <param name="height">Alto deseado de la imagen resultante</param>
        /// <returns>Imagen reconstruida a partir del List de ushort</returns>
        public Bitmap CreateBitmapCorte(List<ushort> pixels16, int width, int height)
        {
            Bitmap bmp = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            BitmapData bmd = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, bmp.PixelFormat);

            double maximoValor = pixels16.Max();
            double minimoValor = pixels16.Min();
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
                        // se normaliza de 0 a 255
                        color = Convert.ToInt32(Convert.ToDouble(pixels16[i * bmd.Width + j] - minimoValor) * ((double)255) / range);
                        if (color < 0) color = 0;
                        if (color > 255) color = 255;

                        // se convierte el color gris a byte
                        b = Convert.ToByte(color);
                        j1 = j * pixelSize;

                        row[j1] = b;            // Red
                        row[j1 + 1] = b;        // Green
                        row[j1 + 2] = b;        // Blue
                    }
                }
            }

            bmp.UnlockBits(bmd);
            return bmp;
        }

        /// <summary>
        /// Obtiene el valor mínimo de TODO el datacubo
        /// </summary>
        /// <returns>Valor minimo de todo el datacubo</returns>
        public ushort GetMinimo()
        {
            ushort minimo = dataCube[0].pixelData.Min();

            for (int i = 0; i < dataCube.Count; i++)
            {
                if (minimo < dataCube[i].pixelData.Min())
                    minimo = dataCube[i].pixelData.Min();
            }

            return minimo;
        }

        /// <summary>
        /// Obtiene el valor maximo de TODO el datacubo
        /// </summary>
        /// <returns>Valor maximo de todo el datacubo</returns>
        public ushort GetMaximo()
        {
            ushort maximo = dataCube[0].pixelData.Max();

            for (int i = 0; i < dataCube.Count; i++)
            {
                if (maximo < dataCube[i].pixelData.Max())
                    maximo = dataCube[i].pixelData.Max();
            }

            return maximo;
        }

        /// <summary>
        /// Se mapea un List de ushort, obtenido de un corte horizontal/vertical a una imagen Bitmap normalizada según los parámetros de entrada
        /// </summary>
        /// <param name="pixels16">List de ushort que contiene la informacion CT de cada pixel</param>
        /// <param name="width">Ancho deseado de la imagen resultante</param>
        /// <param name="height">Alto deseado de la imagen resultante</param>
        /// <param name="minNormalizacion">Valor minimo CT de la normalizacion</param>
        /// <param name="maxNormalizacion">Valor maximo CT de la normalizacion</param>
        /// <returns>Imagen reconstruida a partir del List de ushort</returns>
        public Bitmap CreateBitmapCorte(List<ushort> pixels16, int width, int height, int minNormalizacion, int maxNormalizacion)
        {
            Bitmap bmp = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            BitmapData bmd = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, bmp.PixelFormat);

            double maximoValor = maxNormalizacion;
            double minimoValor = minNormalizacion;
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
                        // se normaliza de 0 a 255
                        color = Convert.ToInt32(Convert.ToDouble(pixels16[i * bmd.Width + j] - minimoValor) * ((double)255) / range);
                        if (color < 0) color = 0;
                        if (color > 255) color = 255;

                        // se convierte el color gris a byte
                        b = Convert.ToByte(color);
                        j1 = j * pixelSize;

                        row[j1] = b;            // Red
                        row[j1 + 1] = b;        // Green
                        row[j1 + 2] = b;        // Blue
                    }
                }
            }

            bmp.UnlockBits(bmd);
            return bmp;
        }


    }
}
