		
		using System;
		using Utilitarios;
		using System.Collections.Generic;
		namespace General
		{		
			public class GobiernoRegional		
			{				
	public int IdGobiernoRegional{get;set;}				
	public string NombreGobiernoRegional{get;set;}				
	public Region region{get;set;}					
	public bool EstadoAuditoria{get;set;}	public int IdUsuarioAuditoria {get;set;}
		public Mensaje mensaje { get; set; }
  			public GobiernoRegional()
			{
														region = new Region();
															mensaje = new Mensaje();
		}  
	}
	public class ListaGobiernoRegional
    {
        public List<GobiernoRegional> lista{ get; set; }    
        public Mensaje mensaje { get; set; }
        public Paginacion paginacion { get; set; }
        public ListaGobiernoRegional()
        {
            lista = new List<GobiernoRegional>();
            mensaje = new Mensaje();
			paginacion=new Paginacion();
        }
    }
}