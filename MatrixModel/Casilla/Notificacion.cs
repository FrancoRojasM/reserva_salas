
using System;
using Utilitarios;
using System.Collections.Generic;
using General;

namespace Casilla
{
	public class Notificacion
	{
		public int IdNotificacion { get; set; }
        public int NumeroNotificacion { get; set; }
		public int CantidadArchivos { get; set; }
		public string NombreNumeroNotificacion { get; set; }
        public Administrado administradonotificado { get; set; }
		public Catalogo catalogocategoria { get; set; }
        
        public string AsuntoNotificacion { get; set; }
		public string MensajeNotificacion { get; set; }
        public string MensajeNotificacionHtml { get; set; }
        public DateTime? FechaHoraNotificacionEnviada { get; set; }
        public DateTime? FechaHoraRegistroNotificacion { get; set; }
        
        public bool NotificacionEnviada { get; set; }
        public Area areanotificador { get; set; }
        public Periodo periodo { get; set; }
        public DateTime? FechaHoraRecepcionNotificacion { get; set; }
		public bool NotificacionRecibida { get; set; }
		public bool EstadoAuditoria { get; set; }
		public int IdUsuarioAuditoria { get; set; }
		public Mensaje mensaje { get; set; }
		public Notificacion()
		{
			administradonotificado = new Administrado();
			catalogocategoria = new Catalogo();
			mensaje = new Mensaje();
            periodo = new Periodo();
			areanotificador = new Area();
        }
	}
	public class ListaNotificacion
	{
		public List<Notificacion> lista { get; set; }
		public Mensaje mensaje { get; set; }
		public Paginacion paginacion { get; set; }
		public ListaNotificacion()
		{
			lista = new List<Notificacion>();
			mensaje = new Mensaje();
			paginacion = new Paginacion();
           

        }
	}
}