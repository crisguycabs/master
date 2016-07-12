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
        private List<byte[]> segCoreVer;

        /// <summary>
        /// Lista de byte[] para almacenar las imagenesByte de la segmentacion vertical de los segmentos Phantom1 de los elementos HIGH
        /// </summary>
        private List<byte[]> segPhantom1Ver;

        /// <summary>
        /// Lista de byte[] para almacenar las imagenesByte de la segmentacion vertical de los segmentos Phantom2 de los elementos HIGH
        /// </summary>
        private List<byte[]> segPhantom2Ver;

        /// <summary>
        /// Lista de byte[] para almacenar las imagenesByte de la segmentacion vertical de los segmentos Phantom3 de los elementos HIGH
        /// </summary>
        private List<byte[]> segPhantom3Ver;

        /// <summary>
        /// Lista de byte[] para almacenar las imagenesByte de la segmentacion horizontal de los segmentos Core de los elementos HIGH
        /// </summary>
        private List<byte[]> segCoreHor;

        /// <summary>
        /// Lista de byte[] para almacenar las imagenesByte de la segmentacion horizontal de los segmentos Phantom1 de los elementos HIGH
        /// </summary>
        private List<byte[]> segPhantom1Hor;

        /// <summary>
        /// Lista de byte[] para almacenar las imagenesByte de la segmentacion horizontal de los segmentos Phantom2 de los elementos HIGH
        /// </summary>
        private List<byte[]> segPhantom2Hor;

        /// <summary>
        /// Lista de byte[] para almacenar las imagenesByte de la segmentacion horizontal de los segmentos Phantom3 de los elementos HIGH
        /// </summary>
        private List<byte[]> segPhantom3Hor;

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
        /// Se inicializa la lista que contiene los elementos HIGH como una lista vacia
        /// </summary>
        public void SetHigh()
        {
            filesHigh = new List<byte[]>();
        }

        /// <summary>
        /// Se agrega solo un elemento a la lista de elementos HIGH
        /// </summary>
        /// <param name="elemento">elemento byte[] a agregar</param>
        public void SetHigh(byte[] elemento)
        {
            filesHigh.Add(elemento);
        }

        /// <summary>
        /// Establece la lista de elementos core trans high
        /// </summary>
        /// <param name="lista"></param>
        public void SetCoreTransHigh(List<byte[]> lista)
        {
            segCoreTransHigh = new List<byte[]>();
            for (int i = 0; i < lista.Count; i++) segCoreTransHigh.Add(lista[i]);
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
        /// Se inicializa la lista que contiene los elementos LOW como una lista vacia
        /// </summary>
        public void SetLow()
        {
            filesLow = new List<byte[]>();
        }

        /// <summary>
        /// Se agrega solo un elemento a la lista de elementos LOW
        /// </summary>
        /// <param name="elemento">elemento byte[] a agregar</param>
        public void SetLow(byte[] elemento)
        {
            filesLow.Add(elemento);
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
        /// Instancia como lista vacia la lista que contiene las segmentaciones core trans high
        /// </summary>
        public void SetSegCoreTransHigh()
        {
            this.segCoreTransHigh = new List<byte[]>();
        }

        /// <summary>
        /// Agrega un elemento a la lista de segmentaciones core trans high
        /// </summary>
        /// <param name="elemento"></param>
        public void SetSegCoreTransHigh(byte[] elemento)
        {
            this.segCoreTransHigh.Add(elemento);
        }

        /// <summary>
        /// Instancia como lista vacia la lista que contiene las segmentaciones core hor
        /// </summary>
        public void SetSegCoreHor()
        {
            this.segCoreHor = new List<byte[]>();
        }

        /// <summary>
        /// Agrega un elemento a la lista de segmentaciones core hor
        /// </summary>
        /// <param name="elemento"></param>
        public void SetSegCoreHor(byte[] elemento)
        {
            this.segCoreHor.Add(elemento);
        }

        /// <summary>
        /// Instancia como lista vacia la lista que contiene las segmentaciones phantom1 hor
        /// </summary>
        public void SetSegPhantom1Hor()
        {
            this.segPhantom1Hor = new List<byte[]>();
        }

        /// <summary>
        /// Agrega un elemento a la lista de segmentaciones phantom1 hor
        /// </summary>
        /// <param name="elemento"></param>
        public void SetSegPhantom1Hor(byte[] elemento)
        {
            this.segPhantom1Hor.Add(elemento);
        }

        /// <summary>
        /// Instancia como lista vacia la lista que contiene las segmentaciones phantom2 hor
        /// </summary>
        public void SetSegPhantom2Hor()
        {
            this.segPhantom2Hor = new List<byte[]>();
        }

        /// <summary>
        /// Agrega un elemento a la lista de segmentaciones phantom3 hor
        /// </summary>
        /// <param name="elemento"></param>
        public void SetSegPhantom3Hor(byte[] elemento)
        {
            this.segPhantom3Hor.Add(elemento);
        }

        /// <summary>
        /// Instancia como lista vacia la lista que contiene las segmentaciones phantom3 hor
        /// </summary>
        public void SetSegPhantom3Hor()
        {
            this.segPhantom3Hor = new List<byte[]>();
        }

        // ---

        /// <summary>
        /// Instancia como lista vacia la lista que contiene las segmentaciones core Ver
        /// </summary>
        public void SetSegCoreVer()
        {
            this.segCoreVer = new List<byte[]>();
        }

        /// <summary>
        /// Agrega un elemento a la lista de segmentaciones core Ver
        /// </summary>
        /// <param name="elemento"></param>
        public void SetSegCoreVer(byte[] elemento)
        {
            this.segCoreVer.Add(elemento);
        }

        /// <summary>
        /// Instancia como lista vacia la lista que contiene las segmentaciones phantom1 Ver
        /// </summary>
        public void SetSegPhantom1Ver()
        {
            this.segPhantom1Ver = new List<byte[]>();
        }

        /// <summary>
        /// Agrega un elemento a la lista de segmentaciones phantom1 Ver
        /// </summary>
        /// <param name="elemento"></param>
        public void SetSegPhantom1Ver(byte[] elemento)
        {
            this.segPhantom1Ver.Add(elemento);
        }      

        /// <summary>
        /// Instancia como lista vacia la lista que contiene las segmentaciones phantom2 Ver
        /// </summary>
        public void SetSegPhantom2Ver()
        {
            this.segPhantom2Ver = new List<byte[]>();
        }

        /// <summary>
        /// Agrega un elemento a la lista de segmentaciones phantom2 Ver
        /// </summary>
        /// <param name="elemento"></param>
        public void SetSegPhantom2Ver(byte[] elemento)
        {
            this.segPhantom2Ver.Add(elemento);
        }        

        /// <summary>
        /// Instancia como lista vacia la lista que contiene las segmentaciones phantom3 Ver
        /// </summary>
        public void SetSegPhantom3Ver()
        {
            this.segPhantom3Ver = new List<byte[]>();
        }

        /// <summary>
        /// Agrega un elemento a la lista de segmentaciones phantom3 Ver
        /// </summary>
        /// <param name="elemento"></param>
        public void SetSegPhantom3Ver(byte[] elemento)
        {
            this.segPhantom3Ver.Add(elemento);
        }

        // ---

        /// <summary>
        /// Agrega un elemento a la lista de segmentaciones phantom2 hor
        /// </summary>
        /// <param name="elemento"></param>
        public void SetSegPhantom2Hor(byte[] elemento)
        {
            this.segPhantom2Hor.Add(elemento);
        }

        /// <summary>
        /// Instancia como lista vacia la lista que contiene las segmentaciones phantom1 trans high
        /// </summary>
        public void SetSegPhantom1TransHigh()
        {
            this.segPhantom1TransHigh = new List<byte[]>();
        }

        /// <summary>
        /// Agrega un elemento a la lista de segmentaciones phantom1 trans high
        /// </summary>
        /// <param name="elemento"></param>
        public void SetSegPhantom1TransHigh(byte[] elemento)
        {
            this.segPhantom1TransHigh.Add(elemento);
        }

        /// <summary>
        /// Instancia como lista vacia la lista que contiene las segmentaciones phantom2 trans high
        /// </summary>
        public void SetSegPhantom2TransHigh()
        {
            this.segPhantom2TransHigh = new List<byte[]>();
        }

        /// <summary>
        /// Agrega un elemento a la lista de segmentaciones phantom2 trans high
        /// </summary>
        /// <param name="elemento"></param>
        public void SetSegPhantom2TransHigh(byte[] elemento)
        {
            this.segPhantom2TransHigh.Add(elemento);
        }

        /// <summary>
        /// Instancia como lista vacia la lista que contiene las segmentaciones phantom3 trans high
        /// </summary>
        public void SetSegPhantom3TransHigh()
        {
            this.segPhantom3TransHigh = new List<byte[]>();
        }

        /// <summary>
        /// Agrega un elemento a la lista de segmentaciones phantom3 trans high
        /// </summary>
        /// <param name="elemento"></param>
        public void SetSegPhantom3TransHigh(byte[] elemento)
        {
            this.segPhantom3TransHigh.Add(elemento);
        }

        //--

        /// <summary>
        /// Instancia como lista vacia la lista que contiene las segmentaciones core trans low
        /// </summary>
        public void SetSegCoreTransLow()
        {
            this.segCoreTransLow = new List<byte[]>();
        }

        /// <summary>
        /// Agrega un elemento a la lista de segmentaciones core trans Low
        /// </summary>
        /// <param name="elemento"></param>
        public void SetSegCoreTransLow(byte[] elemento)
        {
            this.segCoreTransLow.Add(elemento);
        }

        /// <summary>
        /// Instancia como lista vacia la lista que contiene las segmentaciones phantom1 trans Low
        /// </summary>
        public void SetSegPhantom1TransLow()
        {
            this.segPhantom1TransLow = new List<byte[]>();
        }

        /// <summary>
        /// Agrega un elemento a la lista de segmentaciones phantom1 trans Low
        /// </summary>
        /// <param name="elemento"></param>
        public void SetSegPhantom1TransLow(byte[] elemento)
        {
            this.segPhantom1TransLow.Add(elemento);
        }

        /// <summary>
        /// Instancia como lista vacia la lista que contiene las segmentaciones phantom2 trans Low
        /// </summary>
        public void SetSegPhantom2TransLow()
        {
            this.segPhantom2TransLow = new List<byte[]>();
        }

        /// <summary>
        /// Agrega un elemento a la lista de segmentaciones phantom2 trans Low
        /// </summary>
        /// <param name="elemento"></param>
        public void SetSegPhantom2TransLow(byte[] elemento)
        {
            this.segPhantom2TransLow.Add(elemento);
        }

        /// <summary>
        /// Instancia como lista vacia la lista que contiene las segmentaciones phantom3 trans Low
        /// </summary>
        public void SetSegPhantom3TransLow()
        {
            this.segPhantom3TransLow = new List<byte[]>();
        }

        /// <summary>
        /// Agrega un elemento a la lista de segmentaciones phantom3 trans Low
        /// </summary>
        /// <param name="elemento"></param>
        public void SetSegPhantom3TransLow(byte[] elemento)
        {
            this.segPhantom3TransLow.Add(elemento);
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
            #region version antes de 24bpp

            /*

            // se transforma a imagen
            Bitmap tempImage = (Bitmap)MainForm.Byte2image(srcByte);

            // se crea un rectangulo para realizar el corte
            Rectangle rectArea = new Rectangle(area.x - area.width, area.y - area.width, area.width * 2, area.width * 2);
            
            // se realiza el corte, a una imagen CUADRADA
            tempImage = tempImage.Clone(rectArea, tempImage.PixelFormat);

            // se convierte a una imagen REDONDA
            // tempImage = CropCircle8bit(tempImage);
                        
            // se convierte a byte
            byte[] tempByte = MainForm.Img2byte(tempImage);
            tempImage.Dispose();
            
            // se guarda en disco
            File.WriteAllBytes(path + nombre + indice, tempByte);

            return tempByte;

            */

            #endregion

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

            DateTime ini = DateTime.Now;

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

            DateTime fin = DateTime.Now;
            TimeSpan span = fin - ini;

            SetSegTransDone(true);

            return true;
        }

        /// <summary>
        /// Toma una imagen de 8bpp (escala de grises) y la vuelve una imagen de borde circular
        /// </summary>
        /// <param name="source"></param>
        public static Bitmap CropCircle8bit(Bitmap source)
        {
            // radio del circulo
            int alto = source.Width;
            double r = alto/2;

            // el centro del circulo debe estar en las coordenandas (r,r)
            // cualquier punto que se encuentre a una distancia mayor de r se vuelve cero

            // se hace un lock sobre la imagen para poder tratarla
            // se hace lock sobre la imagen para empezar a pintar
            using (CLockBitmap lockSource = new CLockBitmap(source))
            {
                for(int i=0;i<alto;i++)
                {
                    for (int j = 0; j < alto; j++)
                    {
                        double distancia = Math.Sqrt(Math.Pow(i - r, 2) + Math.Pow(j - r, 2));
                        if (distancia > r) lockSource.SetPixel(i, j, Color.White);
                    }
                }
            }

            return source;
        }

        /// <summary>
        /// Toma una lista de byte[], con un indice, y genera los planos horizontales/verticales
        /// </summary>
        /// <param name="elementos">List de byte[] de los elementos a extraer el plano</param>
        /// <param name="indice">indice de la imagen de la cual extraer el plano de corte</param>
        /// <param name="alto">alto del plano a generar</param>
        /// <param name="factorEscalado">factor de escalado</param>
        /// <param name="horizontal">True: planos horizontales; False: planos verticals</param>
        /// <returns>plano generado</returns>
        private byte[] GenerarPlano(List<byte[]> elementos, int indice, int alto, int factorEscalado, bool horizontal)
        {
            Bitmap plano = new Bitmap(elementos.Count * factorEscalado, alto,PixelFormat.Format24bppRgb);

            #region antes de 24bpp

            /*
            // se hace lock sobre la imagen para empezar a pintar
            using (CLockBitmap lockPlano = new CLockBitmap(plano))
            {
                // se recorren todos los elementos a extraer el plano
                int iSlide = 0;
                for (int i = 0; i < elementos.Count * factorEscalado; i = i + factorEscalado)
                {
                    Bitmap src = (Bitmap)MainForm.Byte2image(elementos[iSlide]);
                    // se hace lock sobre cada imagen origen para obtener mas rapido los pizeles que se quieren
                    using (CLockBitmap lockSrc = new CLockBitmap(src))
                    {
                        for (int j = 0; j < alto; j++)
                        {
                            for (int k = 0; k < factorEscalado; k++)
                            {
                                // se escoge la fila (plano horizontal) o la columna(plano vertical) segun sea el caso
                                if (horizontal)
                                {
                                    // plano horizontal
                                    lockPlano.SetPixel(i + k, j, lockSrc.GetPixel(j, indice));
                                }
                                else
                                {
                                    // plano vertical
                                    lockPlano.SetPixel(i + k, j, lockSrc.GetPixel(indice, j));
                                }
                            }
                        }
                    }
                    src.Dispose();
                    iSlide++;
                }
            }
            
            */
            
            #endregion

            Bitmap srcImage;
            BitmapData bmdSrc;

            BitmapData bmdPlano = plano.LockBits(new Rectangle(0, 0, plano.Width, plano.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, plano.PixelFormat);

            unsafe
            {
                int pixelSize = 3;
                int i, j, j1, i1, kindice;

                byte[][] b = new byte[plano.Height][];
                for (i = 0; i < plano.Height;i++)
                {
                    b[i] = new byte[plano.Width];
                }

                // se recorre cada uno de los elementos y se extrae la fila o la columna segun sea el caso
                for (int k = 0; k < elementos.Count; k++)
                {
                    // se convierte a imagen el elemento del que se van a extraer la fila o columna
                    srcImage = (Bitmap)MainForm.Byte2image(elementos[k]);

                    // lo primero es hacer lock sobre la imagen fuente
                    bmdSrc = srcImage.LockBits(new Rectangle(0, 0, srcImage.Width, srcImage.Height), ImageLockMode.ReadOnly, srcImage.PixelFormat);

                    if (horizontal)
                    {
                        // se genera el plano horizontal, el indice I es fijo
                        i = indice;

                        byte* row = (byte*)bmdSrc.Scan0 + (i * bmdSrc.Stride);
                        i1 = i * bmdSrc.Width;

                        for (j = 0; j < bmdSrc.Width; ++j)
                        {
                            j1 = j * pixelSize;

                            // se guarda el color de ese pixel
                            kindice = k * factorEscalado;
                            b[j][kindice] = row[j1];
                            b[j][kindice + 1] = row[j1]; 
                            //b.Add(row[j1]);
                        }
                    }
                    else
                    {
                        // se genera el plano vertical, el indice J es fijo
                        j = indice;
                        for (i = 0; i < bmdSrc.Height; ++i)
                        {
                            byte* row = (byte*)bmdSrc.Scan0 + (i * bmdSrc.Stride);
                            i1 = i * bmdSrc.Width;

                            j1 = j * pixelSize;

                            // se guarda el color de ese pixel
                            kindice = k * factorEscalado;
                            b[i][kindice] = row[j1];
                            b[i][kindice + 1] = row[j1];
                            //b.Add(row[j1]);
                        }
                    }

                    // se libera la imagen fuente y se dispone de ella
                    srcImage.UnlockBits(bmdSrc);
                    srcImage.Dispose();
                }

                // se llena el plano
                for (i = 0; i < bmdPlano.Height; ++i)
                {
                    byte* row = (byte*)bmdPlano.Scan0 + (i * bmdPlano.Stride);
                    i1 = i * bmdPlano.Width;

                    for (j = 0; j < bmdPlano.Width; ++j)
                    {
                        j1 = j * pixelSize;

                        row[j1] = b[i][j];            // Red
                        row[j1 + 1] = b[i][j];        // Green
                        row[j1 + 2] = b[i][j];        // Blue   
                    }
                }
            }

            plano.UnlockBits(bmdPlano);

            return MainForm.Img2byte(plano);
        }

        /// <summary>
        /// Toma las segmentaciones transversales y genera los planes de corte verticales
        /// </summary>
        /// <returns></returns>
        public bool GenerarSegVertical()
        {
            // primero se verifica que se halla realizado la segmentacion transversal
            if (!segTransDone) return false;

            // solo se creara un plano de corte vertical y horizontal, por ahora
            // aun asi se preparan listas que solo tendran un unico elemento
            segCoreVer = new List<byte[]>();
            segPhantom1Ver = new List<byte[]>();
            segPhantom2Ver = new List<byte[]>();
            segPhantom3Ver = new List<byte[]>();
            
            // el largo del plano de corte es la cantidad de slides que existen
            // el alto del plano es el alto de la segmentacion transversal

            // se tiene un factor de escalado por que cada pixel de profundidad no corresponde a la misma distancia de un 
            // pixel de los cortes transversales
            int factorEscalado = (int)Math.Ceiling(voxelDepth / voxelHeight);

            #region para los CORE

            Bitmap referencia = (Bitmap)MainForm.Byte2image(segCoreTransHigh[0]);
            int alto = referencia.Height;
            referencia.Dispose(); // se libera memoria, no es necesario usar mas ese bitmap
            // se toma el plano de la mitad, unicamente, por ahora
            double dindice=alto/2;
            int indice = Convert.ToInt32(Math.Round(dindice)); // posicion de la que se extraen los planos horizontales y verticales
            
            // solo se genera un plano, por ahora
            segCoreVer.Add(GenerarPlano(segCoreTransHigh, indice, alto, factorEscalado, false));

            File.WriteAllBytes(this.folderHigh + "coreVer", segCoreVer[0]);
            
            #endregion

            #region para los PHANTOM1

            referencia = (Bitmap)MainForm.Byte2image(segPhantom1TransHigh[0]);
            alto = referencia.Height;
            referencia.Dispose(); // se libera memoria, no es necesario usar mas ese bitmap
            // se toma el plano de la mitad, unicamente, por ahora
            dindice = alto / 2;
            indice = Convert.ToInt32(Math.Round(dindice)); // posicion de la que se extraen los planos horizontales y verticales

            // solo se genera un plano, por ahora
            segPhantom1Ver.Add(GenerarPlano(segPhantom1TransHigh, indice, alto, factorEscalado, false));

            File.WriteAllBytes(this.folderHigh + "phantom1Ver", segPhantom1Ver[0]);
            
            #endregion

            #region para los PHANTOM2

            referencia = (Bitmap)MainForm.Byte2image(segPhantom2TransHigh[0]);
            alto = referencia.Height;
            referencia.Dispose(); // se libera memoria, no es necesario usar mas ese bitmap
            // se toma el plano de la mitad, unicamente, por ahora
            dindice = alto / 2;
            indice = Convert.ToInt32(Math.Round(dindice)); // posicion de la que se extraen los planos horizontales y verticales

            // solo se genera un plano, por ahora
            segPhantom2Ver.Add(GenerarPlano(segPhantom2TransHigh, indice, alto, factorEscalado, false));

            File.WriteAllBytes(this.folderHigh + "phantom2Ver", segPhantom2Ver[0]);
            
            #endregion

            #region para los PHANTOM3
            
            referencia = (Bitmap)MainForm.Byte2image(segPhantom3TransHigh[0]);
            alto = referencia.Height;
            referencia.Dispose(); // se libera memoria, no es necesario usar mas ese bitmap
            // se toma el plano de la mitad, unicamente, por ahora
            dindice = alto / 2;
            indice = Convert.ToInt32(Math.Round(dindice)); // posicion de la que se extraen los planos horizontales y verticales

            // solo se genera un plano, por ahora
            segPhantom3Ver.Add(GenerarPlano(segPhantom3TransHigh, indice, alto, factorEscalado, false));

            File.WriteAllBytes(this.folderHigh + "phantom3Ver", segPhantom3Ver[0]);
            
            #endregion

            return true;
        }

        /// <summary>
        /// Toma las segmentaciones transversales y genera los planes de corte verticales
        /// </summary>
        /// <returns></returns>
        public bool GenerarSegHorizontal()
        {
            // primero se verifica que se halla realizado la segmentacion transversal
            if (!segTransDone) return false;

            // solo se creara un plano de corte vertical y horizontal, por ahora
            // aun asi se preparan listas que solo tendran un unico elemento
            segCoreHor = new List<byte[]>();            
            segPhantom1Hor = new List<byte[]>();
            segPhantom2Hor = new List<byte[]>();
            segPhantom3Hor = new List<byte[]>();
            
            // el largo del plano de corte es la cantidad de slides que existen
            // el alto del plano es el alto de la segmentacion transversal

            // se tiene un factor de escalado por que cada pixel de profundidad no corresponde a la misma distancia de un 
            // pixel de los cortes transversales
            int factorEscalado = (int)Math.Ceiling(voxelDepth / voxelHeight);

            #region para los CORE
            
            Bitmap referencia = (Bitmap)MainForm.Byte2image(segCoreTransHigh[0]);
            int alto = referencia.Height;
            referencia.Dispose(); // se libera memoria, no es necesario usar mas ese bitmap
            // se toma el plano de la mitad, unicamente, por ahora
            double dindice = alto / 2;
            int indice = Convert.ToInt32(Math.Round(dindice)); // posicion de la que se extraen los planos horizontales y verticales

            // solo se genera un plano, por ahora
            segCoreHor.Add(GenerarPlano(segCoreTransHigh, indice, alto, factorEscalado, true));

            // se guarda el plano en disco para futuras referencias
            File.WriteAllBytes(this.folderHigh + "coreHor", segCoreHor[0]);
                        
            #endregion

            #region para los PHANTOM1

            referencia = (Bitmap)MainForm.Byte2image(segPhantom1TransHigh[0]);
            alto = referencia.Height;
            referencia.Dispose(); // se libera memoria, no es necesario usar mas ese bitmap
            // se toma el plano de la mitad, unicamente, por ahora
            dindice = alto / 2;
            indice = Convert.ToInt32(Math.Round(dindice)); // posicion de la que se extraen los planos horizontales y verticales

            // solo se genera un plano, por ahora
            segPhantom1Hor.Add(GenerarPlano(segPhantom1TransHigh, indice, alto, factorEscalado, true));

            // se guarda el plano en disco para futuras referencias
            File.WriteAllBytes(this.folderHigh + "phantom1Hor", segPhantom1Hor[0]);
            
            #endregion 

            #region para los PHANTOM2

            referencia = (Bitmap)MainForm.Byte2image(segPhantom2TransHigh[0]);
            alto = referencia.Height;
            referencia.Dispose(); // se libera memoria, no es necesario usar mas ese bitmap
            // se toma el plano de la mitad, unicamente, por ahora
            dindice = alto / 2;
            indice = Convert.ToInt32(Math.Round(dindice)); // posicion de la que se extraen los planos horizontales y verticales

            // solo se genera un plano, por ahora
            segPhantom2Hor.Add(GenerarPlano(segPhantom2TransHigh, indice, alto, factorEscalado, true));

            File.WriteAllBytes(this.folderHigh + "phantom2Hor", segPhantom2Hor[0]);
            
            #endregion

            #region para los PHANTOM3
            referencia = (Bitmap)MainForm.Byte2image(segPhantom3TransHigh[0]);
            alto = referencia.Height;
            referencia.Dispose(); // se libera memoria, no es necesario usar mas ese bitmap
            // se toma el plano de la mitad, unicamente, por ahora
            dindice = alto / 2;
            indice = Convert.ToInt32(Math.Round(dindice)); // posicion de la que se extraen los planos horizontales y verticales

            // solo se genera un plano, por ahora
            segPhantom3Hor.Add(GenerarPlano(segPhantom3TransHigh, indice, alto, factorEscalado, true));

            File.WriteAllBytes(this.folderHigh + "phantom3Hor", segPhantom3Hor[0]);
            
            #endregion

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

        public List<byte[]> GetSegPhantom1TransHigh()
        {
            return segPhantom1TransHigh;
        }

        public List<byte[]> GetSegPhantom2TransHigh()
        {
            return segPhantom2TransHigh;
        }

        public List<byte[]> GetSegPhantom3TransHigh()
        {
            return segPhantom3TransHigh;
        }

        public List<byte[]> GetSegCoreHor()
        {
            return segCoreHor;
        }

        public List<byte[]> GetSegPhantom1Hor()
        {
            return segPhantom1Hor;
        }

        public List<byte[]> GetSegPhantom2Hor()
        {
            return segPhantom2Hor;
        }

        public List<byte[]> GetSegPhantom3Hor()
        {
            return segPhantom3Hor;
        }

        public List<byte[]> GetSegCoreVer()
        {
            return segCoreHor;
        }

        public List<byte[]> GetSegPhantom1Ver()
        {
            return segPhantom1Hor;
        }

        public List<byte[]> GetSegPhantom2Ver()
        {
            return segPhantom2Hor;
        }

        public List<byte[]> GetSegPhantom3Ver()
        {
            return segPhantom3Hor;
        }
    }
}
