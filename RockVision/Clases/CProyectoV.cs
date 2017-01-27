using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockVision
{
    /// <summary>
    /// Esta clase permite el manejo de todos los dicom que componen el datacubo a visualizar, además de las opciones de visualizacion 2D y 3D
    /// </summary>
    public class CProyectoV
    {
        #region variables de clase

        /// <summary>
        /// nombre del proyectoV
        /// </summary>
        public string name = "";

        /// <summary>
        /// ruta en disco del archivo .RVV
        /// </summary>
        public string ruta="";

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
        public List<uint> segmentacion2D = null;

        /// <summary>
        /// lista de colores para la segmentacion 2D
        /// </summary>
        public List<System.Drawing.Color> colorSeg2D = null;

        /// <summary>
        /// valores limites de la normalizacion. Vector de 2 posiciones: [0] valor minimo, [1] valor maximo
        /// </summary>
        public uint[] normalizacion2D = null;

        /// <summary>
        /// lista que contiene los limites de la segmentacion 3D
        /// </summary>
        public List<uint> segmentacion3D = null;

        /// <summary>
        /// lista de colores para la segmentacion 3D
        /// </summary>
        public List<System.Drawing.Color> colorSeg3D = null;

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
        /// Constructor con asignacion
        /// </summary>
        /// <param name="path">ruta del proyecto</param>
        /// <param name="elementos">lista de elementos dicom a incluir en el proyecto</param>
        public CProyectoV(string path, List<string> elementos)
        {
            // nombre del proyecto
            name = System.IO.Path.GetFileNameWithoutExtension(path);

            // ruta de la carpeta que contiene el proyecto
            folder = System.IO.Path.GetDirectoryName(path);

            // ruta del proyecto = ruta del folder + nombre
            ruta = path;

            // se crea la carpeta del proyecto
            System.IO.Directory.CreateDirectory(path);

            // se crea la carpeta de los dicom
            System.IO.Directory.CreateDirectory(path + "\\files");

            // se mueven todos los dicom a la carpeta FILEs
            foreach (string dicom in elementos)
            {
                string nombre=System.IO.Path.GetFileName(dicom);
                System.IO.File.Copy(dicom, path + "\\files\\" + nombre);
            }

            // se crea el archivo del proyecto, .RVV
            System.IO.StreamWriter sw = new System.IO.StreamWriter(ruta,false);
            sw.Close();

            // se guarda la informacion del proyecto (info por derecto)
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
