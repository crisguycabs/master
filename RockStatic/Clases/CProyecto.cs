using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace RockStatic
{
    /// <summary>
    /// La clase Proyecto guarda la ruta de los archivos de imagenes/dycom ademas de las 
    /// secciones de Core y phantom, y las areas de interes
    /// </summary>
    public class CProyecto
    {
        #region variables de clase

        /// <summary>
        /// Indica si ya se realizo o no el recorte transversal del core y phantoms
        /// </summary>
        public bool segTransDone=false;

        /// <summary>
        /// Indica si ya se realizo o no el recorte del plano horizontal XZ del core y phantoms
        /// </summary>
        public bool segHorDone=false;

        /// <summary>
        /// Indica si ya se realizo o no el recorte del plano horizontal YZ del core y phantoms
        /// </summary>
        public bool segVerDone=false;

        /// <summary>
        /// Nombre del proyecto
        /// </summary>
        public string name = null;
        
        /// <summary>
        /// Indica si ya se ha realizado la segmentacion de los elementos HIGH y LOW (la misma informacion geometrica sirve para ambos)
        /// </summary>
        public bool segmentacionDone=false;

        /// <summary>
        /// Guarda la ruta de la carpeta donde se encuentra el proyecto
        /// </summary>
        public string folderPath = null;

        /// <summary>
        /// Guarda la ruta de la carpeta donde se encuentran las imagenes HIGH
        /// </summary>
        public string folderHigh = null;

        /// <summary>
        /// Guarda la ruta de la carpeta donde se encuentran las imagenes LOW
        /// </summary>
        public string folderLow=null;

        /// <summary>
        /// Indica si ya se ha realizado la seleccion de areas de interes de los elementos 
        /// </summary>
        public bool areasDone=false;

        /// <summary>
        /// Guarda las coordenadas del recuadro que enmarca el Core de los elementos HIGH
        /// </summary>
        public CCuadrado areaCore = null;

        /// <summary>
        /// Guarda las coordenadas del recuadro que enmarca el Phantom de la IZQUIERDA de los elementos HIGH
        /// </summary>
        public CCuadrado areaPhantom1 = null;

        /// <summary>
        /// Guarda las coordenadas del recuadro que enmarca el Phantom del CENTRO de los elementos HIGH
        /// </summary>
        public CCuadrado areaPhantom2 = null;

        /// <summary>
        /// Guarda las coordenadas del recuadro que enmarca el Phantom de la DERECHA de los elementos HIGH
        /// </summary>
        public CCuadrado areaPhantom3=null;

        /// <summary>
        /// Instancia del datacubo que contiene todos los DICOMs High cargados
        /// </summary>
        public MyDataCube datacuboHigh = null;

        /// <summary>
        /// Instancia del datacubo que contiene todos los DICOMs Low cargados
        /// </summary>
        public MyDataCube datacuboLow = null;

        /// <summary>
        /// Indica si los phantoms vienen incluidos en los DICOM
        /// </summary>
        public bool phantomEnDicom=false;

        /// <summary>
        /// Instancia del phantom 1
        /// </summary>
        public CPhantom phantom1 = null;

        /// <summary>
        /// Instancia del phantom 2
        /// </summary>
        public CPhantom phantom2 = null;

        /// <summary>
        /// Instancia del phantom 3
        /// </summary>
        public CPhantom phantom3 = null;

        /// <summary>
        /// Lista para guardar las areas seleccionadas del core
        /// </summary>
        public List<CAreaInteres> areasCore;

        /// <summary>
        /// Profundidad de la cabeza de la muestra
        /// </summary>
        public long head=0;

        /// <summary>
        /// Profundida de la cola de la muestra
        /// </summary>
        public long tail=0;

        /// <summary>
        /// Unidades de profundidad
        /// </summary>
        public string unidadProfundidad="km";

        #endregion

        /// <summary>
        /// Constructor con asignacion. Crea un proyecto, con su datacubo, a partir
        /// </summary>
        /// <param name="name_">Nombre a darle al proyecto</param>
        /// <param name="rutasHigh">Lista de rutas de DICOM High a cargar en el proyecto</param>
        /// <param name="rutasLow">Lista de rutas de DICOM Low a cargar en el proyecto</param>
        /// <param name="phantom">Indica si los DICOM contienen phantoms o no</param>
        public CProyecto(string name_, List<string> rutasHigh, List<string> rutasLow, bool phantom)
        {
            name = name_;
            datacuboHigh = new MyDataCube(rutasHigh);
            datacuboLow = new MyDataCube(rutasLow);
            phantomEnDicom = phantom;

            // se prepara una lista vacia de areas para los core, cada una con elementos null
            areasCore = new List<CAreaInteres>();
        }      
        

        //--

        /// <summary>
        /// Devuelve el estado de la segmentacion de los elementos HIGH. True, todos los elementos segmentados; False, faltan elementos por segmentar
        /// </summary>
        /// <returns></returns>
        public bool GetSegmentacionDone()
        {
            return segmentacionDone;
        }

        /// <summary>
        /// Establece la ruta de la carpeta donde se guarda el proyecto, y las imagenes
        /// </summary>
        /// <param name="path">Ruta de la carpeta donde se guarda el proyecto</param>
        public void SetFolderPath(string path)
        {
            this.folderPath = path + "\\";

            // ruta imagenes HIGH
            this.folderHigh = folderPath + "high\\";

            // ruta imagenes LOW
            this.folderLow = folderPath + "low\\";
        }

        /// <summary>
        /// Devuelve la ruta completa del folder que contiene el proyecto, el archivo RSP
        /// </summary>
        /// <returns></returns>
        public string GetFolderPath()
        {
            return folderPath;
        }

        /// <summary>
        /// Devuelve la ruta completa del folder que contiene los elementos HIGH
        /// </summary>
        /// <returns></returns>
        public string GetFolderHigh()
        {
            return folderHigh;
        }

        /// <summary>
        /// Devuelve la ruta completa del folder que contiene los elementos LOW
        /// </summary>
        /// <returns></returns>
        public string GetFolderLow()
        {
            return folderLow;
        }

        /// <summary>
        /// Guarda el proyecto en disco en la ruta indicada en las propiedades del metodo
        /// </summary>
        public void Salvar()
        {
            // se empieza a escribir el archivo RSP
            StreamWriter sw = new StreamWriter(folderPath + name + ".rsp");

            // se escriben algunas lineas de informacion
            sw.WriteLine("PROYECTO DE ROCKSTATIC: CARACTERIZACION ESTATICA DE ROCAS");
            sw.WriteLine("COPYRIGHT CRISOSTOMO ALBERTO BARAJAS SOLANO");
            sw.WriteLine("HDSP, UIS, 2015");
            sw.WriteLine("");
            sw.WriteLine("SE PROHIBE LA MODIFICACION DE CUALQUIERA DE LOS ARCHIVOS RELACIONADOS");
            sw.WriteLine("CON EL SOFTWARE ROCKSTATIC SIN LA DEBIDA AUTORIZACION DEL AUTOR O DEL");
            sw.WriteLine("DIRECTOR DEL GRUPO HDSP");
            sw.WriteLine("=====================================================================");
            sw.WriteLine("NAME");
            sw.WriteLine(name);
            sw.WriteLine("---------------------------------------------------------------------");
            sw.WriteLine("PATH");
            sw.WriteLine(folderPath);
            sw.WriteLine("---------------------------------------------------------------------");
            sw.WriteLine("COUNT");
            sw.WriteLine(datacuboHigh.dataCube.Count);
            sw.WriteLine("---------------------------------------------------------------------");
            sw.WriteLine("PHANTOMS");
            sw.WriteLine(phantomEnDicom.ToString());
            sw.WriteLine("---------------------------------------------------------------------");
            sw.WriteLine("PHANTOM1");
            sw.WriteLine("DENSIDAD");
            sw.WriteLine(phantom1.densidad.ToString());
            sw.WriteLine("ZEFF");
            sw.WriteLine(phantom1.zeff.ToString());
            if (!this.phantomEnDicom)
            {
                sw.WriteLine("MEDIAHIGH");
                sw.WriteLine(phantom1.mediaHigh.ToString());
                sw.WriteLine("DESVHIGH");
                sw.WriteLine(phantom1.desvHigh.ToString());
                sw.WriteLine("MEDIALOW");
                sw.WriteLine(phantom1.mediaLow.ToString());
                sw.WriteLine("DESVLOW");
                sw.WriteLine(phantom1.desvLow.ToString());
            }
            sw.WriteLine("---------------------------------------------------------------------");
            sw.WriteLine("PHANTOM2");
            sw.WriteLine("DENSIDAD");
            sw.WriteLine(phantom2.densidad.ToString());
            sw.WriteLine("ZEFF");
            sw.WriteLine(phantom2.zeff.ToString());
            if (!this.phantomEnDicom)
            {
                sw.WriteLine("MEDIAHIGH");
                sw.WriteLine(phantom2.mediaHigh.ToString());
                sw.WriteLine("DESVHIGH");
                sw.WriteLine(phantom2.desvHigh.ToString());
                sw.WriteLine("MEDIALOW");
                sw.WriteLine(phantom2.mediaLow.ToString());
                sw.WriteLine("DESVLOW");
                sw.WriteLine(phantom2.desvLow.ToString());
            }
            sw.WriteLine("---------------------------------------------------------------------");
            sw.WriteLine("PHANTOM3");
            sw.WriteLine("DENSIDAD");
            sw.WriteLine(phantom3.densidad.ToString());
            sw.WriteLine("ZEFF");
            sw.WriteLine(phantom3.zeff.ToString());
            if (!this.phantomEnDicom)
            {
                sw.WriteLine("MEDIAHIGH");
                sw.WriteLine(phantom3.mediaHigh.ToString());
                sw.WriteLine("DESVHIGH");
                sw.WriteLine(phantom3.desvHigh.ToString());
                sw.WriteLine("MEDIALOW");
                sw.WriteLine(phantom3.mediaLow.ToString());
                sw.WriteLine("DESVLOW");
                sw.WriteLine(phantom3.desvLow.ToString());
            }
            sw.WriteLine("---------------------------------------------------------------------");
            sw.WriteLine("SEGMENTACION");
            sw.WriteLine(segmentacionDone.ToString());
            sw.WriteLine("---------------------------------------------------------------------");
            if (this.segmentacionDone)
            {
                sw.WriteLine("CORE");
                sw.WriteLine("X");
                sw.WriteLine(this.areaCore.x);
                sw.WriteLine("Y");
                sw.WriteLine(this.areaCore.y);
                sw.WriteLine("WIDTH");
                sw.WriteLine(this.areaCore.width);
                sw.WriteLine("---------------------------------------------------------------------");
                // si hay informacion de segmentacon Y los DICOM incluyen los phantom
                if (phantomEnDicom)
                {
                    sw.WriteLine("PHANTOM1");
                    sw.WriteLine("X");
                    sw.WriteLine(this.areaPhantom1.x);
                    sw.WriteLine("Y");
                    sw.WriteLine(this.areaPhantom1.y);
                    sw.WriteLine("WIDTH");
                    sw.WriteLine(this.areaPhantom1.width);
                    sw.WriteLine("---------------------------------------------------------------------");
                    sw.WriteLine("PHANTOM2");
                    sw.WriteLine("X");
                    sw.WriteLine(this.areaPhantom2.x);
                    sw.WriteLine("Y");
                    sw.WriteLine(this.areaPhantom2.y);
                    sw.WriteLine("WIDTH");
                    sw.WriteLine(this.areaPhantom2.width);
                    sw.WriteLine("---------------------------------------------------------------------");
                    sw.WriteLine("PHANTOM3");
                    sw.WriteLine("X");
                    sw.WriteLine(this.areaPhantom3.x);
                    sw.WriteLine("Y");
                    sw.WriteLine(this.areaPhantom3.y);
                    sw.WriteLine("WIDTH");
                    sw.WriteLine(this.areaPhantom3.width);
                    sw.WriteLine("---------------------------------------------------------------------");
                }
            }
            sw.WriteLine("AREAS DE INTERES");
            sw.WriteLine(areasDone.ToString());
            sw.WriteLine("---------------------------------------------------------------------");
            if (this.areasDone)
            {
                sw.WriteLine("TOTAL AREAS");
                sw.WriteLine(this.areasCore.Count.ToString());
                for (int i = 0; i < areasCore.Count; i++)
                {
                    sw.WriteLine("X");
                    sw.WriteLine(areasCore[i].x.ToString());
                    sw.WriteLine("Y");
                    sw.WriteLine(areasCore[i].y.ToString());
                    sw.WriteLine("WIDTH");
                    sw.WriteLine(areasCore[i].width.ToString());
                    sw.WriteLine("INI");
                    sw.WriteLine(areasCore[i].ini.ToString());
                    sw.WriteLine("FIN");
                    sw.WriteLine(areasCore[i].fin.ToString());
                }
            }

            // se cierra el streamwriter del archivo RSP
            sw.Close();
        }

        /// <summary>
        /// Establece las coordenadas para la segmentacion del Core con el tipo de dato CCuadrado
        /// </summary>
        /// <param name="elemento">CCuadrado que contiene la informacion del cuadrado</param>
        public void SetCore(CCuadrado elemento)
        {
            this.areaCore = new CCuadrado(elemento);
            this.areaCore.nombre = "Core";
            // se corrigen las coordenadas y ancho del cuadrado

        }

        /// <summary>
        /// Devuelve el tipo de dato CCuadrado que contiene las coordenadas de la segmentacion del Core
        /// </summary>
        /// <returns></returns>
        public CCuadrado GetCore()
        {
            return areaCore;
        }

        /// <summary>
        /// Establece las coordenadas para la segmentacion del Phantom1 con el tipo de dato CCuadrado
        /// </summary>
        /// <param name="elemento">CCuadrado que contiene la informacion del cuadrado</param>
        public void SetPhantom1(CCuadrado elemento)
        {
            this.areaPhantom1 = new CCuadrado(elemento);
            this.areaPhantom1.nombre = "Phantom1";
        }

        /// <summary>
        /// Establece las coordenadas para la segmentacion del Phantom2 con el tipo de dato CCuadrado
        /// </summary>
        /// <param name="elemento">CCuadrado que contiene la informacion del cuadrado</param>
        public void SetPhantom2(CCuadrado elemento)
        {
            this.areaPhantom2 = new CCuadrado(elemento);
            this.areaPhantom2.nombre = "Phantom2";
        }        

        /// <summary>
        /// Establece las coordenadas para la segmentacion del Phantom3 con el tipo de dato CCuadrado
        /// </summary>
        /// <param name="elemento">CCuadrado que contiene la informacion del cuadrado</param>
        public void SetPhantom3(CCuadrado elemento)
        {
            this.areaPhantom3 = new CCuadrado(elemento);
            this.areaPhantom3.nombre = "Phantom3";
        }

        /// <summary>
        /// Devuelve el tipo de dato CCuadrado que contiene las coordenadas de la segmentacion del Phantom1
        /// </summary>
        /// <returns></returns>
        public CCuadrado GetPhantom1()
        {
            return areaPhantom1;
        }

        /// <summary>
        /// Devuelve el tipo de dato CCuadrado que contiene las coordenadas de la segmentacion del Phantom2
        /// </summary>
        /// <returns></returns>
        public CCuadrado GetPhantom2()
        {
            return areaPhantom2;
        }

        /// <summary>
        /// Devuelve el tipo de dato CCuadrado que contiene las coordenadas de la segmentacion del Phantom3
        /// </summary>
        /// <returns></returns>
        public CCuadrado GetPhantom3()
        {
            return areaPhantom3;
        }

        /// <summary>
        /// Toma una imagenByte, la transforma a imagen, recorta el area señalada, la guarda en disco como imagenByte y devuelve una imagenByte
        /// </summary>
        /// <param name="srcByte">Byte[] con la imagen a recortar</param>
        /// <param name="area">CCuadrado que contiene el area a recortar</param>
        /// <param name="path">Folder donde se debe guardar la imagenByte recortada</param>
        /// <param name="nombre">Nombre que se le debe dar a la imagenByte recortada</param>
        /// <param name="indice">Indice necesario para nombrar la imagenByte en disco</param>
        /// <returns></returns>
        public static byte[] Cropper(byte[] srcByte,CCuadrado area,string path, string nombre, int indice)
        {
            DateTime ini = DateTime.Now;

            // se transforma a imagen el byteImage guardado
            Bitmap srcImage = (Bitmap)MainForm.Byte2image(srcByte);

            // se crea un rectangulo para realizar el corte
            Rectangle rectArea = new Rectangle(area.x - area.width, area.y - area.width, area.width * 2, area.width * 2);

            // se crea la imagen destino
            Bitmap rectImage = new Bitmap(srcImage.Width, srcImage.Height, srcImage.PixelFormat);

            // se realiza el corte, a una imagen CUADRADA
            rectImage = srcImage.Clone(rectArea, srcImage.PixelFormat);

            BitmapData bmd = rectImage.LockBits(new Rectangle(0, 0, rectImage.Width, rectImage.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, rectImage.PixelFormat);

            unsafe
            {
                int pixelSize = 3;
                int i, j, j1, i1;
                byte b = Convert.ToByte(Color.Black.R);

                double centroX = Convert.ToDouble(rectImage.Width) / 2;
                double centroY = centroX;
                double distancia, dx, dy;

                for (i = 0; i < bmd.Height; ++i)
                {
                    byte* row = (byte*)bmd.Scan0 + (i * bmd.Stride);
                    i1 = i * bmd.Width;

                    for (j = 0; j < bmd.Width; ++j)
                    {
                        j1 = j * pixelSize;

                        dx = i - centroX;
                        dy = j - centroY;
                        distancia = Math.Sqrt((dx*dx)+(dy*dy));

                        if (distancia > centroX)
                        {
                            row[j1] = b;            // Red
                            row[j1 + 1] = b;        // Green
                            row[j1 + 2] = b;        // Blue       
                        }
                    }
                }

            }

            rectImage.UnlockBits(bmd);

            DateTime fin = DateTime.Now;
            TimeSpan span = fin - ini;

            // se convierte a byte
            byte[] tempByte = MainForm.Img2byte(rectImage);
            
            rectImage.Dispose();
            srcImage.Dispose();

            // se guarda en disco
            File.WriteAllBytes(path + nombre + indice, tempByte);

            return tempByte;
        }             
    }
}
