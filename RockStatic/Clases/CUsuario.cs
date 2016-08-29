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
        public string Nombre;

        public string Apellidos;

        public string Afiliacion;

        public CPermisos[] permisos;

        public CUsuario()
        {

        }
    }
}
