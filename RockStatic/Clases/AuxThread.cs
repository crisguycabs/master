using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;
using System.Drawing.Imaging;

namespace RockStatic.Clases
{
    /// <summary>
    /// Clase auxiliar para poder generar todos los Bitmap de los cortes horizontales/verticales
    /// </summary>
    public class AuxThread
    {
        /// <summary>
        /// Guarda la copia de los pixeles que se van a procesar como imagen 
        /// </summary>
        private List<ushort> pixels16;

        /// <summary>
        /// Imagen resultante
        /// </summary>
        private Bitmap corte;

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
        /// Manejador de finalizacion del thread
        /// </summary>
        private ManualResetEvent _doneEvent;

        /// <summary>
        /// Devuelve la imagen generada
        /// </summary>
        public Bitmap Corte { get { return corte; } }

        /// <summary>
        /// Constructor con asignación para crear una imagen de un corte longitudinal
        /// </summary>
        /// <param name="_pixels16">copia de los pixeles que se van a procesar como imagen </param>
        /// <param name="_width">Ancho deseado de la imagen resultante</param>
        /// <param name="_height">Alto deseado de la imagen resultante</param>
        /// <param name="_minNormalizacion">Valor minimo CT de la normalizacion</param>
        /// <param name="_maxNormalizacion">Valor maximo CT de la normalizacion</param>
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
        /// Metodo que genera la imagen del corte longitudinal
        /// </summary>
        /// <returns></returns>
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

        // Wrapper method for use with thread pool.
        public void ThreadPoolCallback(Object threadContext)
        {
            int threadIndex = (int)threadContext;
            corte = CreateBitmapCorte();
            _doneEvent.Set();
        }
    }
}
