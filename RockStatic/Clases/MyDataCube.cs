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
        /// Guarda la informacion del histograma
        /// </summary>
        public uint[] histograma = null;

        /// <summary>
        /// Marcas de clase del histograma 
        /// </summary>
        public ushort[] bins = null;

        /// <summary>
        /// Limites de cada bin
        /// </summary>
        ushort[] limites = null;

        /// <summary>
        /// List que contiene los List de ushort que contienen los pixeles CT para cada corte horizontal del core
        /// </summary>
        public List<ushort>[] coresHorizontal = null;

        //-----------------------esto es modificado --------------------------------
        /// <summary>
        /// List que contiene los List de ushort que contienen los pixeles CT para cada corte horizontal del phanton1
        /// </summary>
        public List<ushort>[] phantoms1Horizontal = null;

        /// <summary>
        /// List que contiene los List de ushort que contienen los pixeles CT para cada corte horizontal del phanton1
        /// </summary>
        public List<ushort>[] phantoms2Horizontal = null;

        /// <summary>
        /// List que contiene los List de ushort que contienen los pixeles CT para cada corte horizontal del phanton1
        /// </summary>
        public List<ushort>[] phantoms3Horizontal = null;

        //-----------------------esto es modificado --------------------------------
        /// <summary>
        /// List que contiene los List de ushort que contienen los pixeles CT para cada corte vertical del core
        /// </summary>
        public List<ushort>[] coresVertical = null;

        /// <summary>
        /// Indica el ancho de la imagen segmentada del core resultante
        /// </summary>
        public int widthSegCore = 0;

        /// <summary>
        /// Indica el ancho de la imagen segmentada del phantom 1 resultante
        /// </summary>
        public int widthSegP1 = 0;

        /// <summary>
        /// Indica el ancho de la imagen segmentada del phantom 2 resultante
        /// </summary>
        public int widthSegP2 = 0;

        /// <summary>
        /// Indica el ancho de la imagen segmentada del phantom 3 resultante
        /// </summary>
        public int widthSegP3 = 0;

        /// <summary>
        /// factor de escalado para el largo de los cortes horizontales y verticales
        /// </summary>
        public double factorZ = 0;

        /// <summary>
        /// Diametro de la segmentacion en RV
        /// </summary>
        public double diametroSegRV = 0;

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
        /// <param name="elemento">"'core','p1','p2','p3'</param>
        public void SegCircThread(CCuadrado area,string elemento)
        {
            int nseg = dataCube.Count;
            ManualResetEvent[] doneEvents = new ManualResetEvent[nseg];
            AuxThread[] threads = new AuxThread[nseg];

            int width = Convert.ToInt32(dataCube[0].selector.Columns.Data);
            int height = Convert.ToInt32(dataCube[0].selector.Rows.Data);

            for (int i = 0; i < nseg; i++)
            {
                doneEvents[i] = new ManualResetEvent(false);
                 AuxThread thread = new AuxThread(this.dataCube[i].pixelData, area.x, area.y , area.width, width, height, doneEvents[i]);
                threads[i] = thread;
                ThreadPool.QueueUserWorkItem(thread.ThreadSegmentar, i);
            }

            foreach (var e in doneEvents)
                e.WaitOne();

            switch(elemento)
            {
                case "p1":
                    for (int i = 0; i < nseg; i++)
                    {
                        this.dataCube[i].segPhantom1 = threads[i].Segmentacion;
                    }
                    break;
                case "p2":
                    for (int i = 0; i < nseg; i++)
                    {
                        this.dataCube[i].segPhantom2 = threads[i].Segmentacion;
                    }
                    break;
                case "p3":
                    for (int i = 0; i < nseg; i++)
                    {
                        this.dataCube[i].segPhantom3 = threads[i].Segmentacion;
                    }
                    break;
                default:
                    for (int i = 0; i < nseg; i++)
                    {
                        this.dataCube[i].segCore = threads[i].Segmentacion;
                    }
                    break;
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
        /// Genera el histograma general y por slide, las marcas de clase para el datacubo, limites de cada bin, con bins = raiz de numero de datos
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

            // se toman tantos bins como el maximo del rango de valores
            int nbins = Convert.ToInt32(maximo);

            // se instancia el histograma general 
            this.histograma = new uint[nbins];

            //temporal hisograma


            // se calculan los limites de cada marca de clase
            double step = (double)(((double)(maximo - minimo)) / Convert.ToDouble(nbins));
            limites = new ushort[nbins + 1];
            double[] tempLimites = new double[nbins + 1];
            limites[0] = minimo;
            for (int i = 1; i < (nbins + 1); i++)
            {
                tempLimites[i] = tempLimites[i - 1] + step;
                limites[i] = (ushort)(tempLimites[i]);
            }
            limites[nbins] = maximo;

            uint bincount = 0;
            int ibin = 0;

            List<ushort> pixelsOrdenados = new List<ushort>();
            this.histograma = new uint[nbins];
            for (int i = 0; i < dataCube.Count; i++)
            {

                // se instancia el histograma general 
              

                //temporal hisograma

                // se instancia el histograma de cada slide
                dataCube[i].histograma = new uint[nbins];

                // se genera una copia de los pixeles para poder ordenarlos
                pixelsOrdenados.Clear();
                for (int j = 0; j < dataCube[i].pixelData.Count; j++)
                    pixelsOrdenados.Add(dataCube[i].pixelData[j]);

                // se ordenan los pixeles
                pixelsOrdenados.Sort();

                // se empiezan a contar cuantos elementos caben en cada bin
                bincount = 0;
                ibin = 1;

                // se instancia el histograma general 
                for (int j = 0; j < pixelsOrdenados.Count - 1; j++)
                {


                    //temporal hisograma

                    while (pixelsOrdenados[j] > limites[ibin])
                    {
                        ibin++;
                        bincount = 0;
                    }
                    if (ibin == 1)
                    {
                        if (pixelsOrdenados[j] <= limites[ibin])
                        {
                            bincount++;
                            histograma[ibin - 1] = histograma[ibin - 1] + bincount;
                        }
                        else
                        {
                            bincount = 1;
                            histograma[ibin - 1] = histograma[ibin - 1] + bincount;
                            ibin++;
                        }

                    }
                    else
                    {
                        if (pixelsOrdenados[j] == pixelsOrdenados[j + 1])
                        {
                            if (limites[ibin - 1] <= pixelsOrdenados[j] && pixelsOrdenados[j] <= limites[ibin])
                            {
                                bincount++;
                                histograma[ibin - 1] = histograma[ibin - 1] + bincount;
                            }
                        }
                        else
                        {
                            bincount++;
                            histograma[ibin - 1] = histograma[ibin - 1] + bincount;
                            ibin++;
                            bincount = 1;
                        }
                    }
                }

            }

            // se calculan las marcas de clase
            bins = new ushort[nbins];
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
            int alto = Convert.ToInt16(this.widthSegCore);
            int ancho = dataCube.Count;
            ushort[][] temp = new ushort[alto][];
            for (int i = 0; i < alto; i++)
                temp[i] = new ushort[ancho];

            int a = 0;
            int ini = indice * Convert.ToInt16(this.widthSegCore);

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
        /// Se genera un plano de phanton1 horizontal que corresponde al indice que se pasa como argumento
        /// </summary>
        /// <param name="indice">Numero de la fila que se debe extraer de cada DICOM</param>
        /// <returns>Core horizontal generado en la posicion indicada</returns>
        public List<ushort> Generarphanton1Horizontal(int indice)
        {
            // se genera una MATRIZ para guardar, temporalmente, los numeros CT extraidos de todo el data cubo para cada imagen de corte horizontal
            // una vez extraida la matriz se mapea a una imagen Bitmap

            // se crea la matriz temporal y se inicializa
            int alto = Convert.ToInt16(this.widthSegP1);
            int ancho = dataCube.Count;
            ushort[][] temp = new ushort[alto][];
            for (int i = 0; i < alto; i++)
                temp[i] = new ushort[ancho];

            int a = 0;
            int ini = indice * Convert.ToInt16(this.widthSegP1);

            // se empieza a llenar cada columna de la imagen nueva con la fila de cada DICOM
            for (int j = 0; j < ancho; j++)
            {
                for (int i = 0; i < alto; i++)
                {
                    temp[i][j] = dataCube[j].segPhantom1[ini + i];
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
        /// Se genera un plano de phanton2 horizontal que corresponde al indice que se pasa como argumento
        /// </summary>
        /// <param name="indice">Numero de la fila que se debe extraer de cada DICOM</param>
        /// <returns>Core horizontal generado en la posicion indicada</returns>
        public List<ushort> Generarphanton2Horizontal(int indice)
        {
            // se genera una MATRIZ para guardar, temporalmente, los numeros CT extraidos de todo el data cubo para cada imagen de corte horizontal
            // una vez extraida la matriz se mapea a una imagen Bitmap

            // se crea la matriz temporal y se inicializa
            int alto = Convert.ToInt16(this.widthSegP2);
            int ancho = dataCube.Count;
            ushort[][] temp = new ushort[alto][];
            for (int i = 0; i < alto; i++)
                temp[i] = new ushort[ancho];

            int a = 0;
            int ini = indice * Convert.ToInt16(this.widthSegP2);

            // se empieza a llenar cada columna de la imagen nueva con la fila de cada DICOM
            for (int j = 0; j < ancho; j++)
            {
                for (int i = 0; i < alto; i++)
                {
                    temp[i][j] = dataCube[j].segPhantom2[ini + i];
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
        /// Se genera un plano de phanton3 horizontal que corresponde al indice que se pasa como argumento
        /// </summary>
        /// <param name="indice">Numero de la fila que se debe extraer de cada DICOM</param>
        /// <returns>Core horizontal generado en la posicion indicada</returns>
        public List<ushort> Generarphanton3Horizontal(int indice)
        {
            // se genera una MATRIZ para guardar, temporalmente, los numeros CT extraidos de todo el data cubo para cada imagen de corte horizontal
            // una vez extraida la matriz se mapea a una imagen Bitmap

            // se crea la matriz temporal y se inicializa
            int alto = Convert.ToInt16(this.widthSegP3);
            int ancho = dataCube.Count;
            ushort[][] temp = new ushort[alto][];
            for (int i = 0; i < alto; i++)
                temp[i] = new ushort[ancho];

            int a = 0;
            int ini = indice * Convert.ToInt16(this.widthSegP3);

            // se empieza a llenar cada columna de la imagen nueva con la fila de cada DICOM
            for (int j = 0; j < ancho; j++)
            {
                for (int i = 0; i < alto; i++)
                {
                    temp[i][j] = dataCube[j].segPhantom3[ini + i];
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
        /// Se genera un plano de core horizontal que corresponde al indice que se pasa como argumento. Version RV
        /// </summary>
        /// <param name="indice">Numero de la fila que se debe extraer de cada DICOM</param>
        /// <returns>Core horizontal generado en la posicion indicada</returns>
        public List<ushort> GenerarCoreHorizontalRV(int indice)
        {
            // se genera una MATRIZ para guardar, temporalmente, los numeros CT extraidos de todo el data cubo para cada imagen de corte horizontal
            // una vez extraida la matriz se mapea a una imagen Bitmap

            // se crea la matriz temporal y se inicializa
            int alto = Convert.ToInt16(this.diametroSegRV);
            int ancho = dataCube.Count;
            ushort[][] temp = new ushort[alto][];
            for (int i = 0; i < alto; i++)
                temp[i] = new ushort[ancho];

            int ini = indice * Convert.ToInt16(this.diametroSegRV);
            for (int i = 0; i < alto; i++)
            {
                for (int j = 0; j < ancho; j++)
                {
                    temp[i][j] = dataCube[j].pixelData[ini + i];
                }
            }

            // se calcula el factor de escalado debido al espaciado entre Slides
            double resZ = Convert.ToDouble(dataCube[0].selector.SliceThickness.Data);
            double resXY = Convert.ToDouble(dataCube[0].selector.PixelSpacing.Data_[0]);
            int factor = Convert.ToInt32(resZ / resXY); { }

            this.factorZ = factor;

            // se convierte a un List<ushort> para poder usarlo en el mapeo a Bitmap
            List<ushort> pixels16 = new List<ushort>();
            for (int i = 0; i < alto; i++)
                for (int j = 0; j < ancho; j++)
                    for (int k = 0; k < factor; k++)
                        pixels16.Add(temp[i][j]);

            return pixels16;
        }

        /// <summary>
        /// Se genera un plano de core vertical que corresponde al indice que se pasa como argumento. Version RV
        /// </summary>
        /// <param name="indice">Numero de la columna que se debe extraer de cada DICOM</param>
        /// <returns>Core vertical generado en la posicion indicada</returns>
        public List<ushort> GenerarCoreVerticalRV(int indice)
        {
            // se genera una MATRIZ para guardar, temporalmente, los numeros CT extraidos de todo el data cubo para cada imagen de corte vertical
            // una vez extraida la matriz se mapea a una imagen Bitmap

            // se crea la matriz temporal y se inicializa
            int alto = Convert.ToInt16(this.diametroSegRV);
            int ancho = dataCube.Count;
            ushort[][] temp = new ushort[alto][];
            for (int i = 0; i < alto; i++)
                temp[i] = new ushort[ancho];

            int ini = indice;
            int salto = Convert.ToInt16(this.diametroSegRV);
            for (int i = 0; i < alto; i++)
            {
                for (int j = 0; j < ancho; j++)
                {
                    temp[i][j] = dataCube[j].pixelData[ini + i*salto];
                }
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
            int alto = Convert.ToInt16(this.widthSegCore);
            int ancho = dataCube.Count;
            ushort[][] temp = new ushort[alto][];
            for (int i = 0; i < alto; i++)
                temp[i] = new ushort[ancho];

            int columnas = Convert.ToInt16(this.widthSegCore);
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
            coresHorizontal = new List<ushort>[widthSegCore];

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

        //-----------------------esto es modificado --------------------------------
        ///<summary>
        ///Genera todos y cda uno de los planos corte horizontal del phanton 1
        ///</summary>
        public void GeneraPhanton1Horizonales()
        {
            // se genera un List<ushort> por cada pixel de altura de un DICOM
            phantoms1Horizontal = new List<ushort>[widthSegP1];

            // se calcula el factor de escalado debido al espaciado entre Slides
            double resZ = Convert.ToDouble(dataCube[0].selector.SliceThickness.Data);
            double resXY = Convert.ToDouble(dataCube[0].selector.PixelSpacing.Data_[0]);
            int factor = Convert.ToInt32(resZ / resXY);

            for (int i = 0; i < phantoms1Horizontal.Length; i++)
            {
                phantoms1Horizontal[i] = new List<ushort>();
                phantoms1Horizontal[i] = Generarphanton1Horizontal(i);
            }

        }


        ///<summary>
        ///Genera todos y cda uno de los planos corte horizontal del phanton 2
        ///</summary>
        public void GeneraPhanton2Horizonales()
        {
            // se genera un List<ushort> por cada pixel de altura de un DICOM
            phantoms2Horizontal = new List<ushort>[widthSegP2];

            // se calcula el factor de escalado debido al espaciado entre Slides
            double resZ = Convert.ToDouble(dataCube[0].selector.SliceThickness.Data);
            double resXY = Convert.ToDouble(dataCube[0].selector.PixelSpacing.Data_[0]);
            int factor = Convert.ToInt32(resZ / resXY);

            for (int i = 0; i < phantoms2Horizontal.Length; i++)
            {
                phantoms2Horizontal[i] = new List<ushort>();
                phantoms2Horizontal[i] = Generarphanton2Horizontal(i);
            }

        }



        ///<summary>
        ///Genera todos y cda uno de los planos corte horizontal del phanton 3
        ///</summary>
        public void GeneraPhanton3Horizonales()
        {
            // se genera un List<ushort> por cada pixel de altura de un DICOM
            phantoms3Horizontal = new List<ushort>[widthSegP3];

            // se calcula el factor de escalado debido al espaciado entre Slides
            double resZ = Convert.ToDouble(dataCube[0].selector.SliceThickness.Data);
            double resXY = Convert.ToDouble(dataCube[0].selector.PixelSpacing.Data_[0]);
            int factor = Convert.ToInt32(resZ / resXY);

            for (int i = 0; i < phantoms3Horizontal.Length; i++)
            {
                phantoms3Horizontal[i] = new List<ushort>();
                phantoms3Horizontal[i] = Generarphanton3Horizontal(i);
            }

        }

        //-----------------------esto es modificado --------------------------------

        /// <summary>
        /// Genera todos y cada uno de los planos de corte horizontales del core. No genera imágenes sino los List de ushort para cada plano de corte. Version para RV
        /// </summary>
        public void GenerarCortesHorizontalesRV()
        {
            // se genera un List<ushort> por cada pixel de altura de un DICOM
            coresHorizontal = new List<ushort>[Convert.ToInt16(diametroSegRV)];

            for (int i = 0; i < coresHorizontal.Length; i++)
            {
                coresHorizontal[i] = new List<ushort>();
                coresHorizontal[i] = GenerarCoreHorizontalRV(i);
            }
        }

        /// <summary>
        /// Genera todos y cada uno de los planos de corte verticales del core. No genera imágenes sino los List de ushort para cada plano de corte. Version para RV
        /// </summary>
        public void GenerarCortesVerticalesRV()
        {
            // se genera un List<ushort> por cada pixel de altura de un DICOM
            coresVertical = new List<ushort>[Convert.ToInt16(diametroSegRV)];

            for (int i = 0; i < coresVertical.Length; i++)
            {
                coresVertical[i] = new List<ushort>();
                coresVertical[i] = GenerarCoreVerticalRV(i);
            }
        }

        /// <summary>
        /// Genera todos y cada uno de los planos de corte verticales del datacubo. No genera imágenes sino los List de ushort para cada plano de corte
        /// </summary>
        public void GenerarCoresVerticales()
        {
            // se genera un List<ushort> por cada pixel de ancho de un DICOM
            coresVertical = new List<ushort>[widthSegCore];

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
