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
            // ruta de la carpeta que contiene el proyecto
            folder = System.IO.Path.GetDirectoryName(path);

            // ruta del proyecto = ruta del folder + nombre
            ruta = folder + "\\" + System.IO.Path.GetFileName(path);

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
        }

        /// <summary>
        /// Se guarda el archivo RVV con toda la información del proyecto de visualizacion
        /// </summary>
        public void Guardar()
        {

        }
    }
}
