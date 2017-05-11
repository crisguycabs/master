using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockVision
{
    /// <summary>
    /// Esta clase permite el manejo de todos los dicom que componen el datacubo a procesar para la estimación de propiedades dinámicas
    /// </summary>
    class CProyectoD
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
        /// ruta en disco del archivo .RVD
        /// </summary>
        public string ruta = "";

        /// <summary>
        /// ruta de la carpeta del proyecto
        /// </summary>
        public string folder = "";

        /// <summary>
        /// datacubo que contiene los dicoms a visualizar
        /// </summary>
        public List<RockStatic.MyDataCube> datacubos = null;

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

        #endregion


    }
}
