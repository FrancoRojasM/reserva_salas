using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Utilitarios
{

    public class Mensaje
    {

        public int CodigoRegistroTabla { get; set; }
        public int CodigoMensaje { get; set; } = 0;
        public string DescripcionMensaje { get; set; }
        public string mensaje1 { get; set; }
        public string mensaje2 { get; set; }
        public string DescripcionMensajeSistema { get; set; }
        public string ObtenerError()
        {
            string mensaje = "";
            switch (CodigoMensaje)
            {
                case 1:
                    mensaje = "Error, Usted No tiene acceso a los servicios de Matrix";
                    break;
            }
            return mensaje;
        }
    }
}
