using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace RockStatic
{
    /// <summary>
    /// La clase Proyecto guarda la ruta de los archivos de imagenes/dycom ademas de las 
    /// secciones de Core y phantom, y las areas de interes
    /// </summary>
    public class CProyecto
    {
        /* Se mantendrá el principio de encapsulamiento, por tanto todas variables se mantendran
         * privadas para el exterior. Se deben establecer metodos para obtenerlas y modificarlas
         */

        #region variables de clase

        /// <summary>
        /// Lista de byte[] para almacenar las imagenenesByte de las imagenes/dycom de HIGH energy
        /// </summary>
        private List<byte[]> filesHigh;

        /// <summary>
        /// Lista de byte[] para almacenar las imagenenesByte de las imagenes/dycom de LOW energy
        /// </summary>
        private List<byte[]> filesLow;

        /// <summary>
        /// Lista de byte[] para almacenar las imagenesByte de la segmentacion transversal de los segmentos Core de los elementos HIGH
        /// </summary>
        private List<byte[]> segCoreTransHigh;

        /// <summary>
        /// Lista de byte[] para almacenar las imagenesByte de la segmentacion transversal de los segmentos Core de los elementos LOW
        /// </summary>
        private List<byte[]> segCoreTransLow;

        /// <summary>
        /// Lista de byte[] para almacenar las imagenesByte de la segmentacion transversal de los segmentos Phantom1 de los elementos HIGH
        /// </summary>
        private List<byte[]> segPhantom1TransHigh;

        /// <summary>
        /// Lista de byte[] para almacenar las imagenesByte de la segmentacion transversal de los segmentos Phantom2 de los elementos HIGH
        /// </summary>
        private List<byte[]> segPhantom2TransHigh;

        /// <summary>
        /// Lista de byte[] para almacenar las imagenesByte de la segmentacion transversal de los segmentos Phantom3 de los elementos HIGH
        /// </summary>
        private List<byte[]> segPhantom3TransHigh;

        /// <summary>
        /// Lista de byte[] para almacenar las imagenesByte de la segmentacion transversal de los segmentos Phantom1 de los elementos LOW
        /// </summary>
        private List<byte[]> segPhantom1TransLow;

        /// <summary>
        /// Lista de byte[] para almacenar las imagenesByte de la segmentacion transversal de los segmentos Phantom2 de los elementos LOW
        /// </summary>
        private List<byte[]> segPhantom2TransLow;

        /// <summary>
        /// Lista de byte[] para almacenar las imagenesByte de la segmentacion transversal de los segmentos Phantom3 de los elementos LOW
        /// </summary>
        private List<byte[]> segPhantom3TransLow;

        /// <summary>
        /// Lista de byte[] para almacenar las imagenesByte de la segmentacion vertical de los segmentos Core de los elementos HIGH
        /// </summary>
        private List<byte[]> segCoreVerHigh;

        /// <summary>
        /// Lista de byte[] para almacenar las imagenesByte de la segmentacion vertical de los segmentos Core de los elementos LOW
        /// </summary>
        private List<byte[]> segCoreVerLow;

        /// <summary>
        /// Lista de byte[] para almacenar las imagenesByte de la segmentacion vertical de los segmentos Phantom1 de los elementos HIGH
        /// </summary>
        private List<byte[]> segPhantom1VerHigh;

        /// <summary>
        /// Lista de byte[] para almacenar las imagenesByte de la segmentacion vertical de los segmentos Phantom2 de los elementos HIGH
        /// </summary>
        private List<byte[]> segPhantom2VerHigh;

        /// <summary>
        /// Lista de byte[] para almacenar las imagenesByte de la segmentacion vertical de los segmentos Phantom3 de los elementos HIGH
        /// </summary>
        private List<byte[]> segPhantom3VerHigh;

        /// <summary>
        /// Lista de byte[] para almacenar las imagenesByte de la segmentacion vertical de los segmentos Phantom1 de los elementos LOW
        /// </summary>
        private List<byte[]> segPhantom1VerLow;

        /// <summary>
        /// Lista de byte[] para almacenar las imagenesByte de la segmentacion vertical de los segmentos Phantom2 de los elementos LOW
        /// </summary>
        private List<byte[]> segPhantom2VerLow;

        /// <summary>
        /// Lista de byte[] para almacenar las imagenesByte de la segmentacion vertical de los segmentos Phantom3 de los elementos LOW
        /// </summary>
        private List<byte[]> segPhantom3VerLow;

        /// <summary>
        /// Lista de byte[] para almacenar las imagenesByte de la segmentacion horizontal de los segmentos Core de los elementos HIGH
        /// </summary>
        private List<byte[]> segCoreHorHigh;

        /// <summary>
        /// Lista de byte[] para almacenar las imagenesByte de la segmentacion horizontal de los segmentos Core de los elementos LOW
        /// </summary>
        private List<byte[]> segCoreHorLow;

        /// <summary>
        /// Lista de byte[] para almacenar las imagenesByte de la segmentacion horizontal de los segmentos Phantom1 de los elementos HIGH
        /// </summary>
        private List<byte[]> segPhantom1HorHigh;

        /// <summary>
        /// Lista de byte[] para almacenar las imagenesByte de la segmentacion horizontal de los segmentos Phantom2 de los elementos HIGH
        /// </summary>
        private List<byte[]> segPhantom2HorHigh;

        /// <summary>
        /// Lista de byte[] para almacenar las imagenesByte de la segmentacion horizontal de los segmentos Phantom3 de los elementos HIGH
        /// </summary>
        private List<byte[]> segPhantom3HorHigh;

        /// <summary>
        /// Lista de byte[] para almacenar las imagenesByte de la segmentacion horizontal de los segmentos Phantom1 de los elementos LOW
        /// </summary>
        private List<byte[]> segPhantom1HorLow;

        /// <summary>
        /// Lista de byte[] para almacenar las imagenesByte de la segmentacion horizontal de los segmentos Phantom2 de los elementos LOW
        /// </summary>
        private List<byte[]> segPhantom2HorLow;

        /// <summary>
        /// Lista de byte[] para almacenar las imagenesByte de la segmentacion horizontal de los segmentos Phantom3 de los elementos LOW
        /// </summary>
        private List<byte[]> segPhantom3HorLow;



        /// <summary>
        /// Indica si ya se realizo o no el recorte transversal del core y phantoms
        /// </summary>
        private bool segTransDone;

        /// <summary>
        /// Indica si ya se realizo o no el recorte del plano horizontal XZ del core y phantoms
        /// </summary>
        private bool segHorDone;

        /// <summary>
        /// Indica si ya se realizo o no el recorte del plano horizontal YZ del core y phantoms
        /// </summary>
        private bool segVerDone;

        private int _count;
        /// <summary>
        /// Guarda el número de elementos HIGH (o LOW) con los que se está trabajando, y que se
        /// deben leer de disco
        /// </summary>
        public int count
        {
            get { return _count; }
            set { _count = value; }
        }

        private string _name;
        /// <summary>
        /// Nombre del proyecto
        /// </summary>
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Indica si ya se ha realizado la segmentacion de los elementos HIGH y LOW (la misma informacion geometrica sirve para ambos)
        /// </summary>
        private bool segmentacionDone;

        /// <summary>
        /// Guarda la ruta de la carpeta donde se encuentra el proyecto
        /// </summary>
        private string folderPath;

        /// <summary>
        /// Guarda la ruta de la carpeta donde se encuentran las imagenes HIGH
        /// </summary>
        private string folderHigh;

        /// <summary>
        /// Guarda la ruta de la carpeta donde se encuentran las imagenes LOW
        /// </summary>
        private string folderLow;

        /// <summary>
        /// Indica si ya se ha realizado la seleccion de areas de interes de los elementos 
        /// </summary>
        private bool areasDone;

        /// <summary>
        /// Guarda las coordenadas del recuadro que enmarca el Core de los elementos HIGH
        /// </summary>
        private CCuadrado areaCore;

        /// <summary>
        /// Guarda las coordenadas del recuadro que enmarca el Phantom de la IZQUIERDA de los elementos HIGH
        /// </summary>
        private CCuadrado areaPhantom1;

        /// <summary>
        /// Guarda las coordenadas del recuadro que enmarca el Phantom del CENTRO de los elementos HIGH
        /// </summary>
        private CCuadrado areaPhantom2;

        /// <summary>
        /// Guarda las coordenadas del recuadro que enmarca el Phantom de la DERECHA de los elementos HIGH
        /// </summary>
        private CCuadrado areaPhantom3;

        /// <summary>
        /// Alto del voxel, en mm. Debe ser igual al ancho del voxel
        /// </summary>
        public double voxelHeight=0.175781;

        /// <summary>
        /// Ancho del voxel, en mm. Debe ser igual al alto del voxel
        /// </summary>
        public double voxelWidth=0.175781;

        /// <summary>
        /// Profundo del voxel, en mm
        /// </summary>
        public double voxelDepth=0.33;

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public CProyecto()
        {
            this.name = "NuevoProyecto";
            filesHigh = new List<byte[]>();
            filesLow = new List<byte[]>();

            
        }

        /// <summary>
        /// Constructor con asignacion. Crea un CProyecto nuevo como copia del CProyecto que se pasa como argumento
        /// </summary>
        /// <param name="project">CProyecto que se va a duplicar</param>
        public CProyecto(CProyecto project)
        {
            name = project.name;
            folderHigh = project.GetFolderHigh();
            folderLow = project.GetFolderLow();
            folderPath = project.GetFolderPath();
            segmentacionDone = project.GetSegmentacionDone();
            areasDone = project.GetAreasDone();
            
            for (int i = 0; i < filesHigh.Count; i++)
            {
                SetHigh(project.GetHigh());
                SetLow(project.GetLow());
            }
        }

        /// <summary>
        /// Se establece si ya se realizo, o no, el recorte horizontal de los core y phantom
        /// </summary>
        /// <param name="estado"></param>
        public void SetSegHorDone(bool estado)
        {
            segHorDone = estado;
        }

        /// <summary>
        /// Se establece si ya se realizo, o no, el recorte vertical de los core y phantom
        /// </summary>
        /// <param name="estado"></param>
        public void SetSegVerDone(bool estado)
        {
            segVerDone = estado;
        }

        /// <summary>
        /// Se establece si ya se realizo, o no, el recorte transversal de los core y phantom
        /// </summary>
        /// <param name="estado"></param>
        public void SetSegTransDone(bool estado)
        {
            segTransDone = estado;
        }
        
        /// <summary>
        /// Devuelve si ya se realizo, o no, el recorte transversal de los core y phantom
        /// </summary>
        /// <returns></returns>
        public bool GetSegTransDone()
        {
            return segTransDone;
        }

        /// <summary>
        /// Devuelve si ya se realizo, o no, el recorte horizontal de los core y phantom
        /// </summary>
        /// <returns></returns>
        public bool GetSegHorDone()
        {
            return segHorDone;
        }

        /// <summary>
        /// Devuelve si ya se realizo, o no, el recorte vertical de los core y phantom
        /// </summary>
        /// <returns></returns>
        public bool GetSegVerDone()
        {
            return segVerDone;
        }

        /// <summary>
        /// Establece la lista de elementos HIGH
        /// </summary>
        /// <param name="lista">Lista temporal de elementos que se se convertiran en los elementos HIGH</param>
        public void SetHigh(List<byte[]> lista)
        {
            filesHigh = new List<byte[]>();
            for (int i = 0; i < lista.Count; i++) filesHigh.Add(lista[i]);
        }

        /// <summary>
        /// Establece la lista de elementos HIGH
        /// </summary>
        /// <param name="lista">Lista temporal de elementos que se se convertiran en los elementos HIGH</param>
        public void SetHigh(List<string> lista)
        {
            filesHigh=new List<byte[]>();
            for (int i = 0; i < lista.Count; i++) filesHigh.Add(MainForm.Img2byte(lista[i]));
        }

        /// <summary>
        /// Establece la lista de elementos LOW
        /// </summary>
        /// <param name="lista">Lista temporal de elementos que se se convertiran en los elementos LOW</param>
        public void SetLow(List<byte[]> lista)
        {
            filesLow = new List<byte[]>();
            for (int i = 0; i < lista.Count; i++) filesLow.Add(lista[i]);
        }

        /// <summary>
        /// Establece la lista de elementos LOW
        /// </summary>
        /// <param name="lista">Lista temporal de elementos que se se convertiran en los elementos LOW</param>
        public void SetLow(List<string> lista)
        {
            filesLow = new List<byte[]>();
            for (int i = 0; i < lista.Count; i++) filesLow.Add(MainForm.Img2byte(lista[i]));
        }

        /// <summary>
        /// Se devuelve la cantidad de elementos HIGH que hay
        /// </summary>
        /// <returns></returns>
        public int GetLengthHigh()
        {
            return filesHigh.Count;
        }

        /// <summary>
        /// Se devuelve la cantidad de elementos LOW que hay
        /// </summary>
        /// <returns></returns>
        public int GetLengthLow()
        {
            return filesLow.Count;
        }

        /// <summary>
        /// Devuelve el List de elementos HIGH
        /// </summary>
        /// <returns></returns>
        public List<byte[]> GetHigh()
        {
            return this.filesHigh;
        }

        /// <summary>
        /// Devuelve el List de elementos LOW
        /// </summary>
        /// <returns></returns>
        public List<byte[]> GetLow()
        {
            return this.filesLow;
        }

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
        /// Establece si ya se realizo o no la segmentacion de los elementos HIGH
        /// </summary>
        /// <param name="set"></param>
        public void SetSegmentacionDone(bool set)
        {
            segmentacionDone = set;
        }

        /// <summary>
        /// Se establece si ya se ha realizado, o no, la seleccion de areas HIGH
        /// </summary>
        /// <param name="set">True, ya se realizo la segmentacion; False, no se ha realizado la segmentacion</param>
        public void SetAreasDone(bool set)
        {
            areasDone = set;
        }

        /// <summary>
        /// Obtiene si ya se ha realizado, o no, la segmentacion de areas HIGH
        /// </summary>
        /// <returns></returns>
        public bool GetAreasDone()
        {
            return areasDone;
        }

        /// <summary>
        /// Crea el proyecto en disco en la ruta indicada en las propiedades del metodo
        /// </summary>
        public void Crear()
        {
            // se crean los archivos RSP RSPH y RSPL y se llenan con informacion
            this.Salvar();

            // se mueven las imagenes HIGH al nuevo folder como byte[] escritos en un archivo
            for (int i = 0; i < filesHigh.Count; i++) File.WriteAllBytes(folderHigh + i, filesHigh[i]);

            // se mueven las imagenes LOW al nuevo folder como byte[] escritos en un archivo
            for (int i = 0; i < filesLow.Count; i++) File.WriteAllBytes(folderLow + i, filesLow[i]);
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
            sw.WriteLine("---------------------------------------------------------------------");
            sw.WriteLine("");
            sw.WriteLine("NAME");
            sw.WriteLine(name);
            sw.WriteLine("SEGMENTACION");
            sw.WriteLine(segmentacionDone.ToString());
            sw.WriteLine("AREAS");
            sw.WriteLine(areasDone.ToString());
            sw.WriteLine("COUNT");
            sw.WriteLine(this.filesHigh.Count.ToString());
            
            // se cierra el streamwriter del archivo RSP
            sw.Close();

            // se empiezan a escribir los elementos de configuracion RSPC tal y como se tienen ordenados en memoria
            sw = new StreamWriter(folderPath + name + ".rspc");
            sw.WriteLine("PROYECTO DE ROCKSTATIC: CARACTERIZACION ESTATICA DE ROCAS");
            sw.WriteLine("COPYRIGHT CRISOSTOMO ALBERTO BARAJAS SOLANO");
            sw.WriteLine("HDSP, UIS, 2015");
            sw.WriteLine("");
            sw.WriteLine("SE PROHIBE LA MODIFICACION DE CUALQUIERA DE LOS ARCHIVOS RELACIONADOS");
            sw.WriteLine("CON EL SOFTWARE ROCKSTATIC SIN LA DEBIDA AUTORIZACION DEL AUTOR O DEL");
            sw.WriteLine("DIRECTOR DEL GRUPO HDSP");
            sw.WriteLine("---------------------------------------------------------------------");
            sw.WriteLine("");
            // si existe informacion de segmentacion entonces se escibe en disco
            if (this.segmentacionDone)
            {
                sw.WriteLine("CORE");
                sw.WriteLine("X");
                sw.WriteLine(this.areaCore.x);
                sw.WriteLine("Y");
                sw.WriteLine(this.areaCore.y);
                sw.WriteLine("WIDTH");
                sw.WriteLine(this.areaCore.width);
                sw.WriteLine("PHANTOM1");
                sw.WriteLine("X");
                sw.WriteLine(this.areaPhantom1.x);
                sw.WriteLine("Y");
                sw.WriteLine(this.areaPhantom1.y);
                sw.WriteLine("WIDTH");
                sw.WriteLine(this.areaPhantom1.width);
                sw.WriteLine("PHANTOM2");
                sw.WriteLine("X");
                sw.WriteLine(this.areaPhantom2.x);
                sw.WriteLine("Y");
                sw.WriteLine(this.areaPhantom2.y);
                sw.WriteLine("WIDTH");
                sw.WriteLine(this.areaPhantom2.width);
                sw.WriteLine("PHANTOM3");
                sw.WriteLine("X");
                sw.WriteLine(this.areaPhantom3.x);
                sw.WriteLine("Y");
                sw.WriteLine(this.areaPhantom3.y);
                sw.WriteLine("WIDTH");
                sw.WriteLine(this.areaPhantom3.width);
            }            
            
            // se cierra el streamwriter del archivo RSPC
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
            // se transforma a imagen
            Bitmap tempImage = (Bitmap)MainForm.Byte2image(srcByte);

            // se crea un rectangulo para realizar el corte
            Rectangle rectArea = new Rectangle(area.x - area.width, area.y - area.width, area.width * 2, area.width * 2);
            
            // se realiza el corte
            tempImage = tempImage.Clone(rectArea, tempImage.PixelFormat);

            // se convierte a byte
            byte[] tempByte = MainForm.Img2byte(tempImage);
            tempImage.Dispose();

            // se guarda en disco
            File.WriteAllBytes(path + nombre + indice, tempByte);

            return tempByte;
        }

        /// <summary>
        /// Se recortan los elementos HIGH según la informacion de segmentacion que hay guardaday se generan los cortes transversales
        /// de los segmentos CORE y PHANTOM HIGH y LOW
        /// </summary>
        /// <returns>True, el corte se realizo de exitosamente; False, no se puede realizar el corte</returns>
        public bool GenerarSegTransveral()
        {
            // primero se verifica que exista la informacion de segmentacion
            if(!segmentacionDone) return false;
            
            // se toma cada elemento HIGH y LOW y se recorta el CORE, PHANTOM1, PHANTOM2 y PHANTOM3
            segCoreTransHigh = new List<byte[]>();
            segCoreTransLow = new List<byte[]>();
            segPhantom1TransHigh = new List<byte[]>();
            segPhantom1TransLow = new List<byte[]>();
            segPhantom2TransHigh = new List<byte[]>();
            segPhantom2TransLow = new List<byte[]>();
            segPhantom3TransHigh = new List<byte[]>();
            segPhantom3TransLow = new List<byte[]>();

            for (int i = 0; i < _count; i++)
            {
                // se toma cada byte[] y se convierte en image, luego en bitmap
                // esa imagen luego se recorta segun la informacion de segmentacion
                // cada recorte se guarda en disco y en memoria

                segCoreTransHigh.Add(Cropper(filesHigh[i], areaCore, GetFolderHigh(), "coreTrans-",i));
                segCoreTransLow.Add(Cropper(filesLow[i], areaCore, GetFolderLow(), "coreTrans-", i));

                segPhantom1TransHigh.Add(Cropper(filesHigh[i], areaPhantom1, GetFolderHigh(), "phantom1Trans-", i));
                segPhantom1TransLow.Add(Cropper(filesLow[i], areaPhantom1, GetFolderLow(), "phantom1Trans-", i));

                segPhantom2TransHigh.Add(Cropper(filesHigh[i], areaPhantom2, GetFolderHigh(), "phantom2Trans-", i));
                segPhantom2TransLow.Add(Cropper(filesLow[i], areaPhantom2, GetFolderLow(), "phantom2Trans-", i));

                segPhantom3TransHigh.Add(Cropper(filesHigh[i], areaPhantom3, GetFolderHigh(), "phantom3Trans-", i));
                segPhantom3TransLow.Add(Cropper(filesLow[i], areaPhantom3, GetFolderLow(), "phantom3Trans-", i));
            }

            SetSegTransDone(true);

            return true;
        }

        /// <summary>
        /// Toma las segmentaciones transversales y genera los planes de corte verticales. Se varia la coordenada X de la segmentacion
        /// transversal para formar cada plano
        /// </summary>
        /// <returns></returns>
        public bool GenerarSegVertical()
        {
            // primero se verifica que se halla realizado la segmentacion transversal
            if (!segTransDone) return false;

            // se crea la imagen que contendra el plano de corte del core
            // el largo del plano de corte es la cantidad de slides que existen
            // el alto del plano es el alto de la segmentacion transversal del core
            Bitmap referencia = (Bitmap)MainForm.Byte2image(segCoreTransHigh[0]);
            int alto = referencia.Height;

            // se tiene un factor de escalado por que cada pixel de profundidad no corresponde a la misma distancia de un pixel de
            // los cortes transversales
            int factorEscalado = (int)Math.Ceiling(voxelDepth / voxelHeight);
            
            referencia.Dispose(); // se libera memoria, no es necesario usar mas ese bitmap

            segCoreVerHigh = new List<byte[]>();
            segCoreVerLow = new List<byte[]>();
            segPhantom1VerHigh = new List<byte[]>();
            segPhantom2VerHigh = new List<byte[]>();
            segPhantom3VerHigh = new List<byte[]>();
            segPhantom1HorHigh = new List<byte[]>();
            segPhantom2HorHigh = new List<byte[]>();
            segPhantom3HorHigh = new List<byte[]>();

            // el corte transversal es un circulo, el alto y el ancho es el mismo
            // se recorre la misma coordenada X para todos los cortes transversales
            for (int iplano = 0; iplano < alto; iplano++)
            {
                Bitmap plano = new Bitmap(_count*factorEscalado, alto);

                // se hace lock sobre la imagen para empezar a pintar
                using (CLockBitmap lockPlano = new CLockBitmap(plano))
                {
                    // se recorren todos los segmentos transversales del core
                    int iSlide = 0;
                    for (int i = 0; i < _count*factorEscalado; i=i+2)
                    {
                        Bitmap src = (Bitmap)MainForm.Byte2image(segCoreTransHigh[iSlide]);
                        // se hace lock sobre cada imagen destino para obtener mas rapido los pizeles que se quieren
                        using (CLockBitmap lockSrc = new CLockBitmap(src))
                        {
                            // se toma la coordenada X segun el indice iplano
                            for (int j = 0; j < alto; j++)
                            {
                                lockPlano.SetPixel(i, j, lockSrc.GetPixel(iplano, j));
                                lockPlano.SetPixel(i+1, j, lockSrc.GetPixel(iplano, j));
                            }
                        }
                        src.Dispose();
                        iSlide++;
                    }
                }

                // con el plano creado se guarda como imagenByte en disco y en memoria
                segCoreVerHigh.Add(MainForm.Img2byte(plano));
                File.WriteAllBytes(folderHigh + "coreVer-" + iplano, segCoreVerHigh[iplano]);

                // despues de haber creado y guardado el plano, se elimina
                plano.Dispose();
            }

            return true;
        }

        /// <summary>
        /// Devuelve los segmentos transversales del core de los elementos HIGH
        /// </summary>
        /// <returns></returns>
        public List<byte[]> GetSegCoreTransHigh()
        {
            return segCoreTransHigh;
        }

        /// <summary>
        /// Devuelve los segmentos transversales del core de los elementos LOW
        /// </summary>
        /// <returns></returns>
        public List<byte[]> GetSegCoreTransLow()
        {
            return segCoreTransLow;
        }
    }
}
