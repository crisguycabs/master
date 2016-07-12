using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockStatic
{
    /// <summary>
    /// Permite guardar la información de los phantom
    /// </summary>
    public class CPhantom
    {
        /// <summary>
        /// Valor medio de la distribución de probabilidad del phantom
        /// </summary>
        public double media;

        /// <summary>
        /// Valor de la desviación estandar de la distribución de probabilidad del phantom
        /// </summary>
        public double desv;

        /// <summary>
        /// Valor de la densidad del phantom
        /// </summary>
        public double densidad;

        /// <summary>
        /// Valor del número atómico del phantom
        /// </summary>
        public double zeff;

        /// <summary>
        /// Constructor con solo la informacion de la densidad y Zeff
        /// </summary>
        /// <param name="_densidad">Valor de la densidad</param>
        /// <param name="_zeff">Valor del numero atomico efectivo</param>
        public CPhantom(double _densidad, double _zeff)
        {
            densidad = _densidad;
            zeff = _zeff;
        }

        /// <summary>
        /// Constructor completo
        /// </summary>
        /// <param name="_media">Valor medio de la distribución de probabilidad del phantom</param>
        /// <param name="_desv">Valor de la desviación estandar de la distribución de probabilidad del phantom</param>
        /// <param name="_densidad">Valor de la densidad</param>
        /// <param name="_zeff">Valor del numero atomico efectivo</param>
        public CPhantom(double _media, double _desv, double _densidad, double _zeff)
        {
            media = _media;
            desv = _desv;
            densidad = _densidad;
            zeff = _zeff;
        }
    }
}
