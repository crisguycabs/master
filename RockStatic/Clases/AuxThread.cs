using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;
using System.Drawing.Imaging;

namespace RockStatic
{
    /// <summary>
    /// Clase auxiliar para poder generar todos los Bitmap de los cortes horizontales/verticales
    /// </summary>
    public class AuxThread
    {
        #region variables de clase

        /// <summary>
        /// Guarda la copia de los pixeles que se van a procesar como imagen 
        /// </summary>
        private List<ushort> pixels16;

        /// <summary>
        /// Imagen resultante
        /// </summary>
        private Bitmap corte;

        /// <summary>
        /// Segmentacion resultante
        /// </summary>
        private List<ushort> segmentacion;

        /// <summary>
        /// Devuelve la segmentacion resultante
        /// </summary>
        public List<ushort> Segmentacion { get { return segmentacion; } }

        /// <summary>
        /// Ancho deseado de la imagen resultante
        /// </summary>
        int width;

        /// <summary>
        /// Alto deseado de la imagen resultante
        /// </summary>
        int height;

        /// <summary>
        /// Valor minimo CT de la normalizacion
        /// </summary>
        int minNormalizacion;

        /// <summary>
        /// Valor maximo CT de la normalizacion
        /// </summary>
        int maxNormalizacion;

        /// <summary>
        /// Centro del circulo de la segmentacion circular
        /// </summary>
        int xcenter;

        /// <summary>
        /// Centro del circulo de la segmentacion circular
        /// </summary>
        int ycenter;

        /// <summary>
        /// Radio del circulo de la segmentacion circular
        /// </summary>
        int rad;

        /// <summary>
        /// Manejador de finalizacion del thread
        /// </summary>
        private ManualResetEvent _doneEvent;

        /// <summary>
        /// Devuelve la imagen generada
        /// </summary>
        public Bitmap Corte { get { return corte; } }

        #endregion

        /// <summary>
        /// Constructor con asignación para crear una imagen de un corte longitudinal
        /// </summary>
        /// <param name="_pixels16">copia de los pixeles que se van a procesar como imagen </param>
        /// <param name="_width">Ancho deseado de la imagen resultante</param>
        /// <param name="_height">Alto deseado de la imagen resultante</param>
        /// <param name="_minNormalizacion">Valor minimo CT de la normalizacion</param>
        /// <param name="_maxNormalizacion">Valor maximo CT de la normalizacion</param>
        /// <param name="_done">Parametro para el control del ThreadPool</param>
        public AuxThread(List<ushort> _pixels16, int _width, int _height, int _minNormalizacion, int _maxNormalizacion, ManualResetEvent _done)
        {
            pixels16 = new List<ushort>();
            for (int i = 0; i < _pixels16.Count; i++)
                pixels16.Add(_pixels16[i]);

            width = _width;
            height = _height;
            minNormalizacion = _minNormalizacion;
            maxNormalizacion = _maxNormalizacion;
            _doneEvent = _done;
        }

        /// <summary>
        /// Constructor con asignación para crear la imagen de un DICOM
        /// </summary>
        /// <param name="_pixels16">copia de los pixeles que se van a procesar como imagen</param>
        /// <param name="_width">Ancho deseado de la imagen resultante</param>
        /// <param name="_height">Alto deseado de la imagen resultante</param>
        /// <param name="_done">Parametro para el control del ThreadPool</param>
        public AuxThread(List<ushort> _pixels16, int _width, int _height, ManualResetEvent _done)
        {
            pixels16 = new List<ushort>();
            for (int i = 0; i < _pixels16.Count; i++)
                pixels16.Add(_pixels16[i]);

            width = _width;
            height = _height;
            _doneEvent = _done;
        }

        /// <summary>
        /// Constructor con asignación para realizar la segmentacion circular, con threads, del core de un datacubo
        /// </summary>
        /// <param name="_pixels16">copia de los pixeles que se van a procesar</param>
        /// <param name="_xcenter">Coordenadas cartesianas del centro X, con cero en la esquina superior izquierda</param>
        /// <param name="_ycenter">Coordenadas cartesianas del centro Y, con cero en la esquina superior izquierda</param>
        /// <param name="_rad">Radio del circulo a extraer</param>
        /// <param name="_width">Ancho de la imagen original</param>
        /// <param name="_height">Alto de la imagen original</param>
        /// <param name="_done">Parametro para el control del ThreadPool</param>
        public AuxThread(List<ushort> _pixels16, int _xcenter, int _ycenter, int _rad, int _width, int _height, ManualResetEvent _done)
        {
            pixels16 = new List<ushort>();
            for (int i = 0; i < _pixels16.Count; i++)
                pixels16.Add(_pixels16[i]);

            width = _width;
            height = _height;

            xcenter = _xcenter;
            ycenter = _ycenter;
            rad = _rad;

            _doneEvent = _done;
        }

        /// <summary>
        /// Rutina para el Threadpool para crear una imagen del pixeldata cargado
        /// </summary>
        /// <returns>Imagen generada a partir del pixeldata cargado</returns>
        public Bitmap CreateBitmap()
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
        /// Metodo que genera la imagen del corte longitudinal
        /// </summary>
        /// <returns>Imagen del corte longitudinal</returns>
        public Bitmap CreateBitmapCorte()
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

        /// <summary>
        /// Wrapper method for use with thread pool.
        /// </summary>
        /// <param name="threadContext"></param> 
        public void ThreadCrearBitmapCorte(Object threadContext)
        {
            int threadIndex = (int)threadContext;
            corte = CreateBitmapCorte();
            _doneEvent.Set();
        }

        /// <summary>
        /// Wrapper method for use with thread pool.
        /// </summary>
        /// <param name="threadContext">Manejador del hilo</param> 
        public void ThreadCrearBitmap(Object threadContext)
        {
            int threadIndex = (int)threadContext;
            corte = CreateBitmap();
            _doneEvent.Set();
        }

        /// <summary>
        /// Se segmenta la imagen (pixelDatata) que se pasa en el constructor de acuerdo con la informacion de centro y radio que se pasa en el constructor
        /// </summary>
        /// <returns>Imagen que contiene la segmentacion circular</returns>
        public List<ushort> SegmentarCircular()
        {
            List<ushort> pixelsCrop = new List<ushort>();
            int k = 0;
            double dist;

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if ((i >= xcenter ) && (i < (xcenter + rad)) && (j >= ycenter) && (j < (ycenter + rad)))
                    {
                        //// si esta dentro del area cuadrada
                        //int dx = i - xcenter;
                        //int dy = j - ycenter;
                        //dist = Math.Sqrt(dx * dx + dy * dy);

                        //if (dist <= rad)
                        //{
                        //    // si esta dentro del circulo
                        //    pixelsCrop.Add(pixels16[k]);
                        //}
                        //else
                        //{
                        //    // si esta dentro del area cuadrado pero fuera del circulo
                        //    pixelsCrop.Add(0);
                        //}
                        pixelsCrop.Add(pixels16[k]);
                    }
                    k++;
                }
            }

            return pixelsCrop;
        }

        /// <summary>
        /// Wrapper method for use with thread pool.
        /// </summary>
        /// <param name="threadContext">Manejador del hilo</param> 
        public void ThreadSegmentar(Object threadContext)
        {
            int threadIndex = (int)threadContext;
            segmentacion = SegmentarCircular();
            _doneEvent.Set();
        }
    }
}
