		using Utilitarios;
		using System.Collections.Generic;
namespace General
{    
    public class Mes
    {        
        public int IdMes { get; set; }        
        public string NombreMes { get; set; }        
        public string NominacionAbreviada { get; set; }
        
        public int IdUsuarioAuditoria { get; set; }
        
        public Mensaje mensaje { get; set; }
        //~Mes()
        //{
        //}
        public Mes()
        {
            mensaje = new Mensaje();
        }   
    }    
    public class ListaMes
    {        
        public List<Mes> lista { get; set; }        
        public Mensaje mensaje { get; set; }
        
        public Paginacion paginacion { get; set; }
        public ListaMes()
        {
            lista = new List<Mes>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }

}
	

		