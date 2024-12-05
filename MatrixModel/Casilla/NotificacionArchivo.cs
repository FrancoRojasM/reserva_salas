
using System;
using Utilitarios;
using System.Collections.Generic;
namespace Casilla
{
	public class NotificacionArchivo
	{
		public int IdNotificacionArchivo { get; set; }
		public Notificacion notificacion { get; set; }
		public Catalogo catalogotipodocumento { get; set; }
		public string NumeroDocumento { get; set; }
		public string RutaArchivo { get; set; }
		public string ExtensionArchivo { get; set; }
		public decimal PesoArchivo { get; set; }
		public DateTime? FechaHoraLecturaArchivo { get; set; }
		public bool ArchivoLeido { get; set; }
		public bool EstadoAuditoria { get; set; }
		public int IdUsuarioAuditoria { get; set; }
		public Mensaje mensaje { get; set; }
		public NotificacionArchivo()
		{
			notificacion = new Notificacion();
			catalogotipodocumento = new Catalogo();
			mensaje = new Mensaje();
		}
	}
	public class ListaNotificacionArchivo
	{
		public List<NotificacionArchivo> lista { get; set; }
		public Mensaje mensaje { get; set; }
		public Paginacion paginacion { get; set; }
		public ListaNotificacionArchivo()
		{
			lista = new List<NotificacionArchivo>();
			mensaje = new Mensaje();
			paginacion = new Paginacion();
		}
	}
}