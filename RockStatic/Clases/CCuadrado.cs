using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockStatic
{
    /// <summary>
    /// Tipo de dato para almacenar en memoria la seleccion de areas cuadradas que se dibujan en pantalla
    /// 
    /// Crisostomo Barajas
    /// Junio 2015
    /// </summary>
    public class CCuadrado
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

        #endregion

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public CCuadrado()
        {
            x = 0;
            y = 0;
            width = 1;
        }

        /// <summary>
        /// Constructor con asignacion
        /// </summary>
        /// <param name="inX">coordenada x del centro</param>
        /// <param name="inY">coordenada y del centro</param>
        /// <param name="inR">radio</param>
        public CCuadrado(int inX, int inY, int inW)
        {
            x = inX;
            y = inY;
            width = inW;
        }

        /// <summary>
        /// Constructor con asignacion. Se crea una copia del elemento CCuadrado que se pasa como argumento
        /// </summary>
        /// <param name="copia">Elemento CCuadrado que se va a duplicar</param>
        public CCuadrado(CCuadrado copia)
        {
            nombre = copia.nombre;
            x = copia.x;
            y = copia.y;
            width = copia.width;
        }
    }
}
