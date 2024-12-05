		
		using Utilitarios;
		using System.Collections.Generic;
using System;

namespace Seguridad
		{
	public class UsuarioToken
	{


		public int IdUsuarioToken { get; set; }


		public Usuario usuario { get; set; }

		
		public string Issuer { get; set; }
		public string Audience { get; set; }

		public string HostDeUso { get; set; }


		public string Token { get; set; }


		public string FechaCreacion { get; set; }


		public string FechaVencimiento { get; set; }


		public bool Activo { get; set; }


		public int EstadoAuditoria { get; set; }

		public int IdUsuarioAuditoria { get; set; }

		public Mensaje mensaje { get; set; }
		//~UsuarioToken()
		//{
		//}
		public UsuarioToken()
		{
			FechaCreacion = DateTime.Now.ToString("dd/MM/yyyy");
			usuario = new Usuario();
			Activo = true;
			mensaje = new Mensaje();
		}
	}
	public class ListaUsuarioToken
    {
        public List<UsuarioToken> lista{ get; set; }    
        public Mensaje mensaje { get; set; }
        public Paginacion paginacion { get; set; }
        public ListaUsuarioToken()
        {
            lista = new List<UsuarioToken>();
            mensaje = new Mensaje();
			paginacion=new Paginacion();
        }
    }
}	

		