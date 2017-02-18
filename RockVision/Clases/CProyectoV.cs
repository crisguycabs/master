using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvilDICOM.Core;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Selection;

namespace RockVision
{
    /// <summary>
    /// Esta clase permite el manejo de todos los dicom que componen el datacubo a visualizar, además de las opciones de visualizacion 2D y 3D
    /// </summary>
    public class CProyectoV
    {
        #region variables de clase

        /// <summary>
        /// centro X de la segmentacion
        /// </summary>
        public int segX = 0;

        /// <summary>
        /// centro Y de la segmentacion
        /// </summary>
        public int segY = 0;

        /// <summary>
        /// radio de la segmentacion
        /// </summary>
        public int segR = 0;

        /// <summary>
        /// nombre del proyectoV
        /// </summary>
        public string name = "";

        /// <summary>
        /// ruta en disco del archivo .RVV
        /// </summary>
        public string ruta = "";

        /// <summary>
        /// ruta de la carpeta del proyecto
        /// </summary>
        public string folder = "";

        /// <summary>
        /// datacubo que contiene los dicoms a visualizar
        /// </summary>
        public RockStatic.MyDataCube datacubo = null;

        /// <summary>
        /// lista que contiene los limites de la segmentacion 2D
        /// </summary>
        public List<int> segmentacion2D = new List<int>();

        /// <summary>
        /// lista de colores para la segmentacion 2D
        /// </summary>
        public List<System.Drawing.Color> colorSeg2D = new List<System.Drawing.Color>();

        /// <summary>
        /// valores limites de la normalizacion. Vector de 2 posiciones: [0] valor minimo, [1] valor maximo
        /// </summary>
        public int[] normalizacion2D = new int[2];

        /// <summary>
        /// lista que contiene los limites de la segmentacion 3D
        /// </summary>
        public List<int> segmentacion3D = new List<int>();

        /// <summary>
        /// lista de colores para la segmentacion 3D
        /// </summary>
        public List<System.Drawing.Color> colorSeg3D = new List<System.Drawing.Color>();

        /// <summary>
        /// numero del plano de corte XY. -1 indica que no hay plano de corte
        /// </summary>
        public int planoXY = -1;

        /// <summary>
        /// numero del plano de corte YZ. -1 indica que no hay plano de corte
        /// </summary>
        public int planoYZ = -1;

        /// <summary>
        /// numero del plano de corte XZ. -1 indica que no hay plano de corte
        /// </summary>
        public int planoXZ = -1;

        #endregion

        /// <summary>
        /// Constructor con asignacion para cargar un proyecto existente en disco
        /// </summary>
        /// <param name="path"></param>
        public CProyectoV(string path)
        {
            // se lee el archivo
            System.IO.StreamReader sr = new System.IO.StreamReader(path);

            string line="";
            
            this.ruta = path;

            while ((line = sr.ReadLine()) != null)
            {
                switch (line)
                {
                    case "NAME":
                        this.name = sr.ReadLine();
                        break;

                    case "SEGMENTACIONX":
                        this.segX = Convert.ToInt32(sr.ReadLine());
                        break;

                    case "SEGMENTACIONY":
                        this.segY = Convert.ToInt32(sr.ReadLine());
                        break;

                    case "SEGMENTACIONR":
                        this.segR = Convert.ToInt32(sr.ReadLine());
                        break;

                    case "SEGMENTACION2D":
                        this.segmentacion2D = new List<int>();
                        while ((line = sr.ReadLine()) != "") this.segmentacion2D.Add(Convert.ToInt32(line));
                        break;

                    case "COLORSEG2D":
                        this.colorSeg2D = new List<System.Drawing.Color>();
                        while ((line = sr.ReadLine()) != "")
                        {
                            line=line.Replace("Color [", "");
                            line=line.Replace("]", "");
                            this.colorSeg2D.Add(System.Drawing.Color.FromName(line));
                        }
                        break;

                    case "NORMALIZACION2D":
                        this.normalizacion2D = new int[2];
                        normalizacion2D[0] = Convert.ToInt32(sr.ReadLine());
                        normalizacion2D[1] = Convert.ToInt32(sr.ReadLine());
                        break;

                    case "SEGMENTACION3D":
                        this.segmentacion3D = new List<int>();
                        while ((line = sr.ReadLine()) != "") this.segmentacion3D.Add(Convert.ToInt32(line));
                        break;

                    case "COLORSEG3D":
                        this.colorSeg3D = new List<System.Drawing.Color>();
                        while ((line = sr.ReadLine()) != "") this.colorSeg3D.Add(System.Drawing.Color.FromName(line));
                        break;

                    case "PLANOXY":
                        this.planoXY = Convert.ToInt32(sr.ReadLine());
                        break;

                    case "PLANOXZ":
                        this.planoXZ = Convert.ToInt32(sr.ReadLine());
                        break;

                    case "PLANOYZ":
                        this.planoYZ = Convert.ToInt32(sr.ReadLine());
                        break;
                }
            }

            sr.Close();

            // se leen todos y cada uno de los archivos dicom que estan en la carpeta files
            string folder = System.IO.Path.GetDirectoryName(path) + "\\files";
            string[] nfiles = System.IO.Directory.GetFiles(folder);
            this.datacubo = new RockStatic.MyDataCube(nfiles);

            // la segmentacion transversal es TODO el DICOM
            for (int i = 0; i < this.datacubo.dataCube.Count; i++) this.datacubo.dataCube[i].segCore = this.datacubo.dataCube[i].pixelData;

            // hay tantos cortes horizontales como
            this.datacubo.widthSeg = Convert.ToInt32(this.datacubo.dataCube[0].selector.Rows.Data);

            this.datacubo.GenerarCortesHorizontalesRV();
            this.datacubo.GenerarCortesVerticalesRV();
            
            // se genera el histograma
            this.datacubo.GenerarHistograma();            
        }

        /// <summary>
        /// Constructor con asignacion para nuevos proyectos
        /// </summary>
        /// <param name="path">ruta del proyecto</param>
        /// <param name="elementos">lista de elementos dicom a incluir en el proyecto</param>
        public CProyectoV(string path, int segmentacionX, int segmentacionY, int radio, List<string> elementos)
        {
            // nombre del proyecto
            name = System.IO.Path.GetFileNameWithoutExtension(path);

            // ruta de la carpeta que contiene el proyecto
            folder = System.IO.Path.GetDirectoryName(path) + "\\" + name;

            // ruta del proyecto = ruta del folder + nombre
            ruta = folder + "\\" + name + ".rvv";

            // se crea la carpeta del proyecto
            System.IO.Directory.CreateDirectory(folder);

            // se crea la carpeta de los dicom
            System.IO.Directory.CreateDirectory(folder + "\\files");

            // se mueven todos los dicom a la carpeta FILES con un nombre generico
            List<string> elementos2 = new List<string>();
            for (int i = 0; i < elementos.Count; i++)
            {
                string nombre = System.IO.Path.GetFileName(elementos[i]);
                string ext = System.IO.Path.GetExtension(elementos[i]);
                elementos2.Add(folder + "\\files\\" + i.ToString("000000") + ext);
                System.IO.File.Copy(elementos[i], elementos2[i]);
            }

            // se cargan los elementos copiados en el datacubo
            this.datacubo = new RockStatic.MyDataCube(elementos2);

            // se realiza la segmentacion transversal
            for (int i = 0; i < this.datacubo.dataCube.Count;i++)
                this.datacubo.dataCube[i].pixelData = this.datacubo.dataCube[i].CropCTCircle(segmentacionX, segmentacionY, radio, this.datacubo.dataCube[i].selector.Columns.Data, this.datacubo.dataCube[i].selector.Rows.Data);
            
            // la segmentacion transversal es TODO el DICOM
            for (int i = 0; i < this.datacubo.dataCube.Count; i++) this.datacubo.dataCube[i].segCore = this.datacubo.dataCube[i].pixelData;

            // hay tantos cortes horizontales como
            this.datacubo.widthSeg = Convert.ToInt32(this.datacubo.dataCube[0].selector.Rows.Data);

            // se crean las segmentaciones horizontales
            //System.DateTime ini = System.DateTime.Now;
            this.datacubo.GenerarCortesHorizontalesRV();
            this.datacubo.GenerarCortesVerticalesRV();
            //System.DateTime fin = System.DateTime.Now;
            //System.Windows.Forms.MessageBox.Show(((fin - ini).Milliseconds + 1000 * (fin - ini).Seconds).ToString());

            // informacion de la segmentacino
            segX = segmentacionX;
            segY = segmentacionY;
            segR = radio;

            // se genera el histograma
            this.datacubo.GenerarHistograma();
                        
            normalizacion2D = new int[2];
            normalizacion2D[0] = datacubo.GetMinimo();
            normalizacion2D[1] = datacubo.GetMaximo();

            // se crea el archivo del proyecto, .RVV
            System.IO.StreamWriter sw = new System.IO.StreamWriter(ruta, false);
            sw.Close();

            // se guarda la informacion del proyecto (info por derecto)
            Guardar();
        }

        /// <summary>
        /// Se guarda el archivo RVV con toda la información del proyecto de visualizacion
        /// </summary>
        public void Guardar()
        {
            // se abre el archivo
            System.IO.StreamWriter sw = new System.IO.StreamWriter(ruta, false);

            // se escribe el cabezote
            sw.WriteLine("PROYECTO DE ROCKVISION: VISUALIZACION DE MUESTRAS DE ROCAS");
            sw.WriteLine("COPYRIGHT CRISOSTOMO ALBERTO BARAJAS SOLANO");
            sw.WriteLine("HDSP, UIS, 2017");
            sw.WriteLine("");
            sw.WriteLine("SE PROHIBE LA MODIFICACION DE CUALQUIERA DE LOS ARCHIVOS RELACIONADOS");
            sw.WriteLine("CON EL SOFTWARE ROCKSTATIC SIN LA DEBIDA AUTORIZACION DEL AUTOR O DEL");
            sw.WriteLine("DIRECTOR DEL GRUPO HDSP");
            sw.WriteLine("=====================================================================");
            sw.WriteLine("");
            sw.WriteLine("NAME");
            sw.WriteLine(name);
            sw.WriteLine("RUTA");
            sw.WriteLine(ruta);
            sw.WriteLine("");
            sw.WriteLine("SEGMENTACIONX");
            sw.WriteLine(segX.ToString());
            sw.WriteLine("");
            sw.WriteLine("SEGMENTACIONY");
            sw.WriteLine(segY.ToString());
            sw.WriteLine("");
            sw.WriteLine("SEGMENTACIONR");
            sw.WriteLine(segR.ToString());
            sw.WriteLine("");
            sw.WriteLine("SEGMENTACION2D");
            for (int i = 0; i < segmentacion2D.Count; i++)
                sw.WriteLine(segmentacion2D[i].ToString());
            sw.WriteLine("");
            sw.WriteLine("COLORSEG2D");
            for (int i = 0; i < colorSeg2D.Count; i++)
                sw.WriteLine(colorSeg2D[i].ToString());
            sw.WriteLine("");
            sw.WriteLine("NORMALIZACION2D");
            sw.WriteLine(normalizacion2D[0].ToString());
            sw.WriteLine(normalizacion2D[1].ToString());
            sw.WriteLine("");
            sw.WriteLine("SEGMENTACION3D");
            for (int i = 0; i < segmentacion3D.Count; i++)
                sw.WriteLine(segmentacion3D[i].ToString());
            sw.WriteLine("");
            sw.WriteLine("COLORSEG3D");
            for (int i = 0; i < colorSeg3D.Count; i++)
                sw.WriteLine(colorSeg3D[i].ToString());
            sw.WriteLine("");
            sw.WriteLine("PLANOXY");
            sw.WriteLine(planoXY.ToString());
            sw.WriteLine("");
            sw.WriteLine("PLANOXZ");
            sw.WriteLine(planoXZ.ToString());
            sw.WriteLine("");
            sw.WriteLine("PLANOYZ");
            sw.WriteLine(planoYZ.ToString());
            sw.WriteLine("");
            sw.Close();
        }
    }
}
