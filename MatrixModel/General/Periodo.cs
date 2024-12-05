
using Utilitarios;
using System.Collections.Generic;
namespace General
{
	public class Periodo
	{


		public int IdPeriodo { get; set; }


		public string NombrePeriodo { get; set; }


		public string DecenioNombrePeriodo { get; set; }


		public string Decenio { get; set; }


		public bool Actual { get; set; }


		public bool EstadoAuditoria { get; set; }

		public int IdUsuarioAuditoria { get; set; }

		public Mensaje mensaje { get; set; }
		//~Periodo()
		//{
		//}
		public Periodo()
		{
			mensaje = new Mensaje();
		}
	}
	public class ListaPeriodo
	{
		public List<Periodo> lista { get; set; }
		public Mensaje mensaje { get; set; }
		public Paginacion paginacion { get; set; }
		public ListaPeriodo()
		{
			lista = new List<Periodo>();
			mensaje = new Mensaje();
			paginacion = new Paginacion();
		}
	}
}

