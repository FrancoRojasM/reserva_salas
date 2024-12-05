
using Utilitarios;
using System.Collections.Generic;
namespace General
{
	public class Pais
	{


		public int IdPais { get; set; }


		public string NombrePais { get; set; }


		public string Gentilicio { get; set; }


		public string Alfa3 { get; set; }


		public string Alfa2 { get; set; }


		public string Bandera { get; set; }


		public bool EstadoAuditoria { get; set; }

		public int IdUsuarioAuditoria { get; set; }

		public Mensaje mensaje { get; set; }
		//~Pais()
		//{
		//}
		public Pais()
		{
			mensaje = new Mensaje();
		}
	}
	public class ListaPais
	{
		public List<Pais> lista { get; set; }
		public Mensaje mensaje { get; set; }
		public Paginacion paginacion { get; set; }
		public ListaPais()
		{
			lista = new List<Pais>();
			mensaje = new Mensaje();
			paginacion = new Paginacion();
		}
	}
}

