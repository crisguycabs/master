using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RockStatic
{
    /// <summary>
    /// Tipo de dato para almacenar en memoria la seleccion de areas circulares que se dibujan en pantalla
    /// 
    /// Crisostomo Barajas
    /// Junio 2015
    /// </summary>
    public class CCirculo
    {
        #region variables de clase

        /// <summary>
        /// Coordenada x del centro del circulo
        /// </summary>
        public int x;

        /// <summary>
        /// Coordenada y del centro del circulo
        /// </summary>
        public int y;

        /// <summary>
        /// Radio del circulo
        /// </summary>
        public int r;

        /// <summary>
        /// Nombre que se le da al circulo
        /// </summary>
        public string nombre;

        #endregion

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public CCirculo()
        {
            x = 0;
            y = 0;
            r = 1;
        }

        /// <summary>
        /// Constructor con asignacion
        /// </summary>
        /// <param name="inX">coordenada x del centro</param>
        /// <param name="inY">coordenada y del centro</param>
        /// <param name="inR">radio</param>
        public CCirculo(int inX, int inY, int inR)
        {
            x = inX;
            y = inY;
            r = inR;
        }

        /// <summary>
        /// Constructor con asignacion. Se crea un duplicado del elemento CCirculo que se pasa como argumento
        /// </summary>
        /// <param name="punto">elemento CCirculo que se va a duplicar</param>
        public CCirculo(CCirculo punto)
        {
            x = punto.x;
            y = punto.y;
            r = punto.r;
        }
    }
}
