
using System;
using Utilitarios;
using System.Collections.Generic;
namespace Reservas
{
	public class Sala
	{
		public int IdSala { get; set; }
		public string Nombre { get; set; }
		public int Aforo { get; set; }
		public Catalogo catalogopiso { get; set; }
		public string Observaciones { get; set; }
		public bool EstadoAuditoria { get; set; }
		public int IdUsuarioAuditoria { get; set; }
		public Mensaje mensaje { get; set; }
		public Sala()
		{
			catalogopiso = new Catalogo();
			mensaje = new Mensaje();
		}
	}
	public class ListaSala
	{
		public List<Sala> lista { get; set; }
		public Mensaje mensaje { get; set; }
		public Paginacion paginacion { get; set; }
		public ListaSala()
		{
			lista = new List<Sala>();
			mensaje = new Mensaje();
			paginacion = new Paginacion();
		}
	}
}