using Utilitarios;
using System.Collections.Generic;
namespace General
{
    
    public class Proveedor
    {
                
        public int IdProveedor { get; set; }
        public string NombreProveedor { get; set; }

        public int EstadoAuditoria { get; set; }

        public int IdUsuarioAuditoria { get; set; }

        public Mensaje mensaje { get; set; }

        public Proveedor()
        { 
            mensaje = new Mensaje();
        }
    }
    
    public class ListaProveedor
    {
        
        public List<Proveedor> lista { get; set; }
        
        public Mensaje mensaje { get; set; }
        
        public Paginacion paginacion { get; set; }
        public ListaProveedor()
        {
            lista = new List<Proveedor>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }

}



