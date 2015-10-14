using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
        /// Lista de byte[] para almacenar las imagenesByte de los segmentos Core de los elementos HIGH
        /// </summary>
        private List<byte[]> segCoreHigh;

        /// <summary>
        /// Lista de byte[] para almacenar las imagenesByte de los segmentos Core de los elementos LOW
        /// </summary>
        private List<byte[]> segCoreLow;

        /// <summary>
        /// Lista de byte[] para almacenar las imagenesByte de los segmentos Phantom1 de los elementos HIGH
        /// </summary>
        private List<byte[]> segPhantom1High;

        /// <summary>
        /// Lista de byte[] para almacenar las imagenesByte de los segmentos Phantom2 de los elementos HIGH
        /// </summary>
        private List<byte[]> segPhantom2High;

        /// <summary>
        /// Lista de byte[] para almacenar las imagenesByte de los segmentos Phantom3 de los elementos HIGH
        /// </summary>
        private List<byte[]> segPhantom3High;

        /// <summary>
        /// Lista de byte[] para almacenar las imagenesByte de los segmentos Phantom1 de los elementos LOW
        /// </summary>
        private List<byte[]> segPhantom1Low;

        /// <summary>
        /// Lista de byte[] para almacenar las imagenesByte de los segmentos Phantom2 de los elementos LOW
        /// </summary>
        private List<byte[]> segPhantom2Low;

        /// <summary>
        /// Lista de byte[] para almacenar las imagenesByte de los segmentos Phantom3 de los elementos LOW
        /// </summary>
        private List<byte[]> segPhantom3Low;

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
    }
}
