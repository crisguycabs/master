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
        /// Lista de byte para almacenar las imagenenesByte de las imagenes/dycom de HIGH energy
        /// </summary>
        private List<byte[]> filesHigh;

        /// <summary>
        /// Lista de string para almacenar las imagenenesByte de las imagenes/dycom de LOW energy
        /// </summary>
        private List<byte[]> filesLow;
                
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
        /// Indica si ya se ha realizado la segmentacion de los elementos HIGH
        /// </summary>
        private bool segmentacionHigh;

        /// <summary>
        /// Indica si ya se ha realizado la segmentacion de los elementos LOW
        /// </summary>
        private bool segmentacionLow;

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
        /// Indica si ya se ha realizado la seleccion de areas de interes de los elementos HIGH
        /// </summary>
        private bool areasHigh;

        /// <summary>
        /// Indica si ya se ha realizado la seleccion de areas de interes de los elementos LOW
        /// </summary>
        private bool areasLow;

        /// <summary>
        /// Guarda las coordenadas del recuadro que enmarca el Core de los elementos HIGH
        /// </summary>
        private CCuadrado areaCoreHigh;

        /// <summary>
        /// Guarda las coordenadas del recuadro que enmarca el Phantom de la IZQUIERDA de los elementos HIGH
        /// </summary>
        private CCuadrado areaPhantom1High;

        /// <summary>
        /// Guarda las coordenadas del recuadro que enmarca el Phantom del CENTRO de los elementos HIGH
        /// </summary>
        private CCuadrado areaPhantom2High;

        /// <summary>
        /// Guarda las coordenadas del recuadro que enmarca el Phantom de la DERECHA de los elementos HIGH
        /// </summary>
        private CCuadrado areaPhantom3High;

        /// <summary>
        /// Guarda las coordenadas del recuadro que enmarca el Core de los elementos HIGH
        /// </summary>
        private CCuadrado areaCoreLow;

        /// <summary>
        /// Guarda las coordenadas del recuadro que enmarca el Phantom de la IZQUIERDA de los elementos HIGH
        /// </summary>
        private CCuadrado areaPhantom1Low;

        /// <summary>
        /// Guarda las coordenadas del recuadro que enmarca el Phantom del CENTRO de los elementos HIGH
        /// </summary>
        private CCuadrado areaPhantom2Low;

        /// <summary>
        /// Guarda las coordenadas del recuadro que enmarca el Phantom de la DERECHA de los elementos HIGH
        /// </summary>
        private CCuadrado areaPhantom3Low;

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
        /// Constructor con asignacion. Para cuando se necesite hacer una copia del proyecto
        /// </summary>
        /// <param name="project"></param>
        public CProyecto(CProyecto project)
        {
            name = project.name;
            folderHigh = project.GetFolderHigh();
            folderLow = project.GetFolderLow();
            folderPath = project.GetFolderPath();
            segmentacionHigh = project.GetSegmentacionHigh();
            segmentacionLow = project.GetSegmentacionLow();
            areasHigh = project.GetAreasHigh();
            areasLow = project.GetAreasLow();

            for (int i = 0; i < filesHigh.Count; i++)
            {
                SetHigh(project.GetHigh());
                SetLow(project.GetLow());
            }
        }

        /// <summary>
        /// Llena la lista de elementos HIGH
        /// </summary>
        /// <param name="lista"></param>
        public void SetHigh(List<byte[]> lista)
        {
            filesHigh = new List<byte[]>();
            for (int i = 0; i < lista.Count; i++) filesHigh.Add(lista[i]);
        }

        /// <summary>
        /// Llena la lista de elementos HIGH
        /// </summary>
        /// <param name="lista"></param>
        public void SetHigh(List<string> lista)
        {
            filesHigh=new List<byte[]>();
            for (int i = 0; i < lista.Count; i++) filesHigh.Add(MainForm.Img2byte(lista[i]));
        }

        /// <summary>
        /// Llena la lista de elementos LOW
        /// </summary>
        /// <param name="lista"></param>
        public void SetLow(List<byte[]> lista)
        {
            filesLow = new List<byte[]>();
            for (int i = 0; i < lista.Count; i++) filesLow.Add(lista[i]);
        }

        /// <summary>
        /// Llena la lista de elementos LOW
        /// </summary>
        /// <param name="lista"></param>
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
        public bool GetSegmentacionHigh()
        {
            return segmentacionHigh;
        }

        /// <summary>
        /// Devuelve el estado de la segmentacion de los elementos LOW
        /// </summary>
        /// <returns></returns>
        public bool GetSegmentacionLow()
        {
            return segmentacionLow;
        }

        /// <summary>
        /// Establece la ruta de la carpeta donde se guarda el proyecto, y las imagenes
        /// </summary>
        public void SetFolderPath(string path)
        {
            this.folderPath = path + "\\";

            // ruta imagenes HIGH
            this.folderHigh = folderPath + "high\\";

            // ruta imagenes LOW
            this.folderLow = folderPath + "low\\";
        }

        public string GetFolderPath()
        {
            return folderPath;
        }

        public string GetFolderHigh()
        {
            return folderHigh;
        }

        public string GetFolderLow()
        {
            return folderLow;
        }

        /// <summary>
        /// Establece si ya se realizo o no la segmentacion de los elementos HIGH
        /// </summary>
        /// <param name="set"></param>
        public void SetSegmentacionHigh(bool set)
        {
            segmentacionHigh = set;
        }

        /// <summary>
        /// Establece si ya se realizo o no la segmentacion de los elementos LOW
        /// </summary>
        /// <param name="set"></param>
        public void SetSegmentacionLow(bool set)
        {
            segmentacionLow = set;
        }

        /// <summary>
        /// Se establece si ya se ha realizado, o no, la seleccion de areas HIGH
        /// </summary>
        /// <param name="set"></param>
        public void SetAreasHigh(bool set)
        {
            areasHigh = set;
        }

        /// <summary>
        /// Se establece si ya se ha realizado, o no, la seleccion de areas LOW
        /// </summary>
        /// <param name="set"></param>
        public void SetAreasLow(bool set)
        {
            areasLow = set;
        }

        /// <summary>
        /// Obtiene si ya se ha realizado, o no, la segmentacion de areas HIGH
        /// </summary>
        /// <returns></returns>
        public bool GetAreasHigh()
        {
            return areasHigh;
        }

        /// <summary>
        /// Obtiene si ya se ha realizado, o no, la segmentacion de areas LOW
        /// </summary>
        /// <returns></returns>
        public bool GetAreasLow()
        {
            return areasLow;
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
            sw.WriteLine("SEGMENTACION HIGH");
            sw.WriteLine(segmentacionHigh.ToString());
            sw.WriteLine("SEGMENTACION LOW");
            sw.WriteLine(segmentacionLow.ToString());
            sw.WriteLine("AREAS HIGH");
            sw.WriteLine(areasHigh.ToString());
            sw.WriteLine("AREAS LOW");
            sw.WriteLine(areasLow.ToString());
            sw.WriteLine("COUNT");
            sw.WriteLine(this.filesHigh.Count.ToString());
            
            // se cierra el streamwriter del archivo RSP
            sw.Close();

            // se empiezan a escribir los elementos HIGH tal y como se tienen ordenados en memoria
            sw = new StreamWriter(folderPath + name + ".rsph");
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
            if (this.segmentacionHigh)
            {
                sw.WriteLine("CORE");
                sw.WriteLine("X");
                sw.WriteLine(this.areaCoreHigh.x);
                sw.WriteLine("Y");
                sw.WriteLine(this.areaCoreHigh.y);
                sw.WriteLine("WIDTH");
                sw.WriteLine(this.areaCoreHigh.width);
                sw.WriteLine("PHANTOM1");
                sw.WriteLine("X");
                sw.WriteLine(this.areaPhantom1High.x);
                sw.WriteLine("Y");
                sw.WriteLine(this.areaPhantom1High.y);
                sw.WriteLine("WIDTH");
                sw.WriteLine(this.areaPhantom1High.width);
                sw.WriteLine("PHANTOM2");
                sw.WriteLine("X");
                sw.WriteLine(this.areaPhantom2High.x);
                sw.WriteLine("Y");
                sw.WriteLine(this.areaPhantom2High.y);
                sw.WriteLine("WIDTH");
                sw.WriteLine(this.areaPhantom2High.width);
                sw.WriteLine("PHANTOM3");
                sw.WriteLine("X");
                sw.WriteLine(this.areaPhantom3High.x);
                sw.WriteLine("Y");
                sw.WriteLine(this.areaPhantom3High.y);
                sw.WriteLine("WIDTH");
                sw.WriteLine(this.areaPhantom3High.width);
            }            
            
            // se cierra el streamwriter del archivo RSPH
            sw.Close();

            // se empiezan a escribir los elementos LOW tal y como se tienen ordenados en memoria
            sw = new StreamWriter(folderPath + name + ".rspl");
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
            if (this.segmentacionLow)
            {
                sw.WriteLine("CORE");
                sw.WriteLine("X");
                sw.WriteLine(this.areaCoreLow.x);
                sw.WriteLine("Y");
                sw.WriteLine(this.areaCoreLow.y);
                sw.WriteLine("WIDTH");
                sw.WriteLine(this.areaCoreLow.width);
                sw.WriteLine("PHANTOM1");
                sw.WriteLine("X");
                sw.WriteLine(this.areaPhantom1Low.x);
                sw.WriteLine("Y");
                sw.WriteLine(this.areaPhantom1Low.y);
                sw.WriteLine("WIDTH");
                sw.WriteLine(this.areaPhantom1Low.width);
                sw.WriteLine("PHANTOM2");
                sw.WriteLine("X");
                sw.WriteLine(this.areaPhantom2Low.x);
                sw.WriteLine("Y");
                sw.WriteLine(this.areaPhantom2Low.y);
                sw.WriteLine("WIDTH");
                sw.WriteLine(this.areaPhantom2Low.width);
                sw.WriteLine("PHANTOM3");
                sw.WriteLine("X");
                sw.WriteLine(this.areaPhantom3Low.x);
                sw.WriteLine("Y");
                sw.WriteLine(this.areaPhantom3Low.y);
                sw.WriteLine("WIDTH");
                sw.WriteLine(this.areaPhantom3Low.width);
            }   

            // se cierra el streamwriter del archivo RSPL
            sw.Close();
        }

        /// <summary>
        /// Establece las coordenadas para la segmentacion del Core con el tipo de dato CCuadrado
        /// </summary>
        /// <param name="elemento">CCuadrado que contiene la informacion del cuadrado</param>
        public void SetCoreHigh(CCuadrado elemento)
        {
            this.areaCoreHigh = new CCuadrado(elemento);
            this.areaCoreHigh.nombre = "Core";
            // se corrigen las coordenadas y ancho del cuadrado

        }

        /// <summary>
        /// Establece las coordenadas para la segmentacion del Core con el tipo de dato CCuadrado
        /// </summary>
        /// <param name="elemento"></param>
        public void SetCoreLow(CCuadrado elemento)
        {
            this.areaCoreLow = new CCuadrado(elemento);
            this.areaCoreLow.nombre = "Core";
        }

        /// <summary>
        /// Devuelve el tipo de dato CCuadrado que contiene las coordenadas de la segmentacion del Core
        /// </summary>
        /// <returns></returns>
        public CCuadrado GetCoreHigh()
        {
            return areaCoreHigh;
        }

        /// <summary>
        /// Devuelve el tipo de dato CCuadrado que contiene las coordenadas de la segmentacion del Core
        /// </summary>
        /// <returns></returns>
        public CCuadrado GetCoreLow()
        {
            return areaCoreLow;
        }

        /// <summary>
        /// Establece las coordenadas para la segmentacion del Phantom1 con el tipo de dato CCuadrado
        /// </summary>
        /// <param name="elemento"></param>
        public void SetPhantom1High(CCuadrado elemento)
        {
            this.areaPhantom1High = new CCuadrado(elemento);
            this.areaPhantom1High.nombre = "Phantom1";
        }

        /// <summary>
        /// Establece las coordenadas para la segmentacion del Phantom1 con el tipo de dato CCuadrado
        /// </summary>
        /// <param name="elemento"></param>
        public void SetPhantom1Low(CCuadrado elemento)
        {
            this.areaPhantom1Low = new CCuadrado(elemento);
            this.areaPhantom1Low.nombre = "Phantom1";
        }

        /// <summary>
        /// Establece las coordenadas para la segmentacion del Phantom2 con el tipo de dato CCuadrado
        /// </summary>
        /// <param name="elemento"></param>
        public void SetPhantom2High(CCuadrado elemento)
        {
            this.areaPhantom2High = new CCuadrado(elemento);
            this.areaPhantom2High.nombre = "Phantom2";
        }

        // <summary>
        /// Establece las coordenadas para la segmentacion del Phantom2 con el tipo de dato CCuadrado
        /// </summary>
        /// <param name="elemento"></param>
        public void SetPhantom2Low(CCuadrado elemento)
        {
            this.areaPhantom2Low = new CCuadrado(elemento);
            this.areaPhantom2Low.nombre = "Phantom2";
        }

        // <summary>
        /// Establece las coordenadas para la segmentacion del Phantom3 con el tipo de dato CCuadrado
        /// </summary>
        /// <param name="elemento"></param>
        public void SetPhantom3High(CCuadrado elemento)
        {
            this.areaPhantom3High = new CCuadrado(elemento);
            this.areaPhantom3High.nombre = "Phantom3";
        }

        // <summary>
        /// Establece las coordenadas para la segmentacion del Phantom3 con el tipo de dato CCuadrado
        /// </summary>
        /// <param name="elemento"></param>
        public void SetPhantom3Low(CCuadrado elemento)
        {
            this.areaPhantom3Low = new CCuadrado(elemento);
            this.areaPhantom3Low.nombre = "Phantom3";
        }

        /// <summary>
        /// Devuelve el tipo de dato CCuadrado que contiene las coordenadas de la segmentacion del Phantom1
        /// </summary>
        /// <returns></returns>
        public CCuadrado GetPhantom1High()
        {
            return areaPhantom1High;
        }

        /// <summary>
        /// Devuelve el tipo de dato CCuadrado que contiene las coordenadas de la segmentacion del Phantom2
        /// </summary>
        /// <returns></returns>
        public CCuadrado GetPhantom2High()
        {
            return areaPhantom2High;
        }

        /// <summary>
        /// Devuelve el tipo de dato CCuadrado que contiene las coordenadas de la segmentacion del Phantom3
        /// </summary>
        /// <returns></returns>
        public CCuadrado GetPhantom3High()
        {
            return areaPhantom3High;
        }

        /// <summary>
        /// Devuelve el tipo de dato CCuadrado que contiene las coordenadas de la segmentacion del Phantom1
        /// </summary>
        /// <returns></returns>
        public CCuadrado GetPhantom1Low()
        {
            return areaPhantom1Low;
        }

        /// <summary>
        /// Devuelve el tipo de dato CCuadrado que contiene las coordenadas de la segmentacion del Phantom2
        /// </summary>
        /// <returns></returns>
        public CCuadrado GetPhantom2Low()
        {
            return areaPhantom2Low;
        }

        /// <summary>
        /// Devuelve el tipo de dato CCuadrado que contiene las coordenadas de la segmentacion del Phantom3
        /// </summary>
        /// <returns></returns>
        public CCuadrado GetPhantom3Low()
        {
            return areaPhantom3Low;
        }
    }
}
