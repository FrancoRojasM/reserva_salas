using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Utilitarios
{    
    public class Auditoria
    {        
        public Int32 IdUsuarioAuditoria { get; set; }        
        public DateTime FechaAuditoria { get; set; }        
        public Int32 EstadoAuditoria { get; set; }
    }
}
