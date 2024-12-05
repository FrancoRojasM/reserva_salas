
using Utilitarios;
using System.Collections.Generic;
namespace General
{
	public class UnidadEjecutora
	{


		public int IdUnidadEjecutora { get; set; }


		public string Descripcion { get; set; }


		public bool EstadoAuditoria { get; set; }

		public int IdUsuarioAuditoria { get; set; }

		public Mensaje mensaje { get; set; }
		//~UnidadEjecutora()
		//{
		//}
		public UnidadEjecutora()
		{
			mensaje = new Mensaje();
		}
	}
	public class ListaUnidadEjecutora
	{
		public List<UnidadEjecutora> lista { get; set; }
		public Mensaje mensaje { get; set; }
		public Paginacion paginacion { get; set; }
		public ListaUnidadEjecutora()
		{
			lista = new List<UnidadEjecutora>();
			mensaje = new Mensaje();
			paginacion = new Paginacion();
		}
	}
}

