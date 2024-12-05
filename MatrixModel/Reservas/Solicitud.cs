
using System;
using Utilitarios;
using System.Collections.Generic;
namespace Reservas
{
    public class Solicitud
    {
        public int IdSolicitud { get; set; }
        public Catalogo catalogoconsejoregional { get; set; }
        public Catalogo catalogosecretaria { get; set; }
        public Catalogo catalogocomite { get; set; }
        public string NombreEvento { get; set; }
        public int NumeroParticipantes { get; set; }
        public string FechaDesde { get; set; }
        public string FechaHasta { get; set; }
        public int NumeroDias { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }
        public int NumeroAuditorios { get; set; }
        public string ResponsableEvento { get; set; }
        public string TelefonoContacto { get; set; }
        public string CorreoContacto { get; set; }
        public string Observaciones { get; set; }
        public Sala salaasignada { get; set; }
        public Catalogo catalogoestadosolicitud { get; set; }
        public bool EstadoAuditoria { get; set; }
        public int IdUsuarioAuditoria { get; set; }
        public Mensaje mensaje { get; set; }
        public Solicitud()
        {
            catalogoconsejoregional = new Catalogo();
            catalogosecretaria = new Catalogo();
            catalogocomite = new Catalogo();
            salaasignada = new Sala();
            catalogoestadosolicitud = new Catalogo();
            mensaje = new Mensaje();
        }
    }
    public class ListaSolicitud
    {
        public List<Solicitud> lista { get; set; }
        public Mensaje mensaje { get; set; }
        public Paginacion paginacion { get; set; }
        public ListaSolicitud()
        {
            lista = new List<Solicitud>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }
}