using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockStatic
{
    /// <summary>
    /// Clase que contiene la información de los usuarios que pueden ejecutar ROCKSTATIC
    /// </summary>
    public class CUsuario
    {
        /// <summary>
        /// Nombre del usuario
        /// </summary>
        public string Nombre;

        /// <summary>
        /// Apellidos del usuario
        /// </summary>
        public string Apellidos;

        /// <summary>
        /// Tipo de afiliacion del usuario
        /// </summary>
        public string Afiliacion;

        /// <summary>
        /// Lista de perfiles de uso que puede tener el usuario
        /// </summary>
        public CPermisos[] permisos;

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public CUsuario()
        {

        }
    }
}
