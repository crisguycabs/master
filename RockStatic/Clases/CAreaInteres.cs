using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockStatic
{
    /// <summary>
    /// Esta tipo de dato maneja la informacion de las areas de interes seleccionadas
    /// </summary>
    class CAreaInteres
    {
        #region variables de clase

        /// <summary>
        /// Coordenada x de la esquina superior izquierda
        /// </summary>
        public int x;

        /// <summary>
        /// Coordenada y de la esquina superior izquierda
        /// </summary>
        public int y;

        /// <summary>
        /// Ancho del cuadrado
        /// </summary>
        public int width;

        /// <summary>
        /// Nombre que se le da al cuadrado
        /// </summary>
        public string nombre;

        /// <summary>
        /// Primer slide del area de interes
        /// </summary>
        public int ini;

        /// <summary>
        /// Ultimo slide del area de interes
        /// </summary>
        public int fin;

        #endregion

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public CAreaInteres()
        {
            x = 0;
            y = 0;
            width = 1;
            nombre = "";
            ini = 0;
            fin = 0;
        }

        /// <summary>
        /// Constructor con asignacion
        /// </summary>
        /// <param name="_x"></param>
        /// <param name="_y"></param>
        /// <param name="_width"></param>
        /// <param name="_nombre"></param>
        /// <param name="_ini"></param>
        /// <param name="_fin"></param>
        public CAreaInteres(int _x, int _y, int _width, string _nombre, int _ini, int _fin)
        {
            x = _x;
            y = _y;
            width = _width;
            nombre = _nombre;
            ini = _ini;
            fin = _fin;
        }

        /// <summary>
        /// Constructor para duplicar el tipo de dato
        /// </summary>
        /// <param name="area"></param>
        public CAreaInteres(CAreaInteres area)
        {
            x = area.x;
            y = area.y;
            width = area.width;
            nombre = area.nombre;
            ini = area.ini;
            fin = area.fin;
        }
    }
}
