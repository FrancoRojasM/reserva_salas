
		using System.Data;
		using System.Collections.Generic;
		
namespace Utilitarios
{
    
    public class Catalogo
    {


        public int? IdCatalogo { get; set; } = 0;


        
        public int Grupo { get; set; }

        
        public int IdCatalogoPadre { get; set; }
        public int CantidadCatalogo { get; set; }
        public int CantidadCatalogoRecibidas { get; set; }


        public int OrdenItem { get; set; }

        
        public string Descripcion { get; set; }
        public string CODCAT { get; set; }

        
        public string Detalle { get; set; }


        public int Activo { get; set; } = 1;

        
        public int EstadoAuditoria { get; set; }
        
        public int IdUsuarioAuditoria { get; set; }
        
        public Mensaje mensaje { get; set; }
        public Catalogo()
        {
           
          
            mensaje = new Mensaje();
        }      
    }
    
    public class ListaCatalogo
    {
        
        public List<Catalogo> lista { get; set; }
        
        public Mensaje mensaje { get; set; }
        
        public Paginacion paginacion { get; set; }
        public ListaCatalogo()
        {
            lista = new List<Catalogo>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }

}

	

		