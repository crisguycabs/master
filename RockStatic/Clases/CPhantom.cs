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
        /// Valor medio de la distribución de probabilidad del phantom para HIGH
        /// </summary>
        public double mediaHigh;

        /// <summary>
        /// Valor de la desviación estandar de la distribución de probabilidad del phantom para HIGH
        /// </summary>
        public double desvHigh;

        /// <summary>
        /// Valor medio de la distribución de probabilidad del phantom para LOW
        /// </summary>
        public double mediaLow;

        /// <summary>
        /// Valor de la desviación estandar de la distribución de probabilidad del phantom para LOW
        /// </summary>
        public double desvLow;

        /// <summary>
        /// Valor de la densidad del phantom
        /// </summary>
        public double densidad;

        /// <summary>
        /// Valor del número atómico del phantom
        /// </summary>
        public double zeff;

        /// <summary>
        /// Constructor con asignacion
        /// </summary>
        /// <param name="_mediaHigh">Valor medio de la distribución de probabilidad del phantom en HIGH</param>
        /// <param name="_desvHigh">Valor de la desviación estandar de la distribución de probabilidad del phantom en HIGH</param>
        /// <param name="_mediaLow">Valor medio de la distribución de probabilidad del phantom en LOW</param>
        /// <param name="_desvLow">Valor de la desviación estandar de la distribución de probabilidad del phantom en LOW</param>
        /// <param name="_densidad">Valor de la densidad</param>
        /// <param name="_zeff">Valor del numero atomico efectivo</param>
        public CPhantom(double _mediaHigh, double _desvHigh, double _mediaLow, double _desvLow, double _densidad, double _zeff)
        {
            mediaHigh = _mediaHigh;
            desvHigh = _desvHigh;
            mediaLow = _mediaLow;
            desvLow = _desvLow;
            densidad = _densidad;
            zeff = _zeff;
        }

        /// <summary>
        /// Constructor con duplicacion
        /// </summary>
        /// <param name="phantom"></param>
        public CPhantom(CPhantom phantom)
        {
            this.mediaHigh = phantom.mediaHigh;
            this.desvHigh = phantom.desvHigh;
            this.mediaLow = phantom.mediaLow;
            this.desvLow = phantom.desvLow;
            this.densidad = phantom.densidad;
            this.zeff = phantom.zeff;
        }
    }
}
