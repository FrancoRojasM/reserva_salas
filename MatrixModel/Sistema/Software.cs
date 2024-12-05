
using Utilitarios;
using System.Collections.Generic;
namespace Sistema
{
	public class Software
	{


		public int IdSoftware { get; set; }


		public string NombreLargoSoftware { get; set; }


		public string NombreCortoSoftware { get; set; }


		public string NumeroVersionSoftware { get; set; }


		public string RutaImagenSoftware { get; set; }


		public string RutaImagenLogoSoftware { get; set; }


		public string NombreLargoEmpresa { get; set; }


		public string NombreCortoEmpresa { get; set; }


		public bool EstadoAuditoria { get; set; }

		public int IdUsuarioAuditoria { get; set; }

		public Mensaje mensaje { get; set; }
		//~Software()
		//{
		//}
		public Software()
		{
			mensaje = new Mensaje();
		}
	}
	public class ListaSoftware
	{
		public List<Software> lista { get; set; }
		public Mensaje mensaje { get; set; }
		public Paginacion paginacion { get; set; }
		public ListaSoftware()
		{
			lista = new List<Software>();
			mensaje = new Mensaje();
			paginacion = new Paginacion();
		}
	}
}

