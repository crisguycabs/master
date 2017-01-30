using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockVision
{
    class CUmbralCT
    {
        /// <summary>
        /// valor minimo del rango
        /// </summary>
        public int minimo;

        /// <summary>
        /// valor maximo del rango
        /// </summary>
        public int maximo;

        /// <summary>
        /// color del rango
        /// </summary>
        public System.Drawing.Color color;

        /// <summary>
        /// constructor por defecto
        /// </summary>
        public CUmbralCT()
        {
            minimo = 0;
            maximo = 0;
            color = System.Drawing.Color.Black;
        }

        /// <summary>
        /// constructor con asignacion
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="colour"></param>
        public CUmbralCT(int min, int max, System.Drawing.Color colour)
        {
            minimo = min;
            maximo = max;
            color = colour;
        }
    }
}
