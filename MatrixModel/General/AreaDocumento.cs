
using Utilitarios;
using System.Collections.Generic;
namespace General
{
	public class AreaDocumento
	{


		public int IdAreaDocumento { get; set; }


		public Area area { get; set; }


		public Catalogo catalogotipodocumento { get; set; }


		public bool Activo { get; set; }


		public int EstadoAuditoria { get; set; }

		public int IdUsuarioAuditoria { get; set; }

		public Mensaje mensaje { get; set; }
		//~AreaDocumento()
		//{
		//}
		public AreaDocumento()
		{
			area = new Area();
			catalogotipodocumento = new Catalogo();
			Activo = true;
			mensaje = new Mensaje();
		}
	}
	public class ListaAreaDocumento
	{
		public List<AreaDocumento> lista { get; set; }
		public Mensaje mensaje { get; set; }
		public Paginacion paginacion { get; set; }
		public ListaAreaDocumento()
		{
			lista = new List<AreaDocumento>();
			mensaje = new Mensaje();
			paginacion = new Paginacion();
		}
	}
}

