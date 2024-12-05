		
		using System;
		using Utilitarios;
		using System.Collections.Generic;
		namespace General
		{		
			public class Region		
			{				
	public int IdRegion{get;set;}				
	public string NombreRegion{get;set;}				
	public bool EstadoAuditoria{get;set;}	public int IdUsuarioAuditoria {get;set;}
		public Mensaje mensaje { get; set; }
  			public Region()
			{
			mensaje = new Mensaje();
		}  
	}
	public class ListaRegion
    {
        public List<Region> lista{ get; set; }    
        public Mensaje mensaje { get; set; }
        public Paginacion paginacion { get; set; }
        public ListaRegion()
        {
            lista = new List<Region>();
            mensaje = new Mensaje();
			paginacion=new Paginacion();
        }
    }
}