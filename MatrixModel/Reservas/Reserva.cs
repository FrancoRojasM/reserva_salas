
using System;
using Utilitarios;
using System.Collections.Generic;
namespace Reservas
{
    public class Reserva
    {
        public int IdReserva { get; set; }
        public Solicitud solicitud { get; set; }
        public Sala sala { get; set; }
        public string FechaDesdeReserva { get; set; }
        public string FechaHastaReserva { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }
        public string Observaciones { get; set; }
        public bool EstadoAuditoria { get; set; }
        public int IdUsuarioAuditoria { get; set; }
        public Mensaje mensaje { get; set; }
        public Reserva()
        {
            solicitud = new Solicitud();
            sala = new Sala();
            mensaje = new Mensaje();
        }
    }
    public class ListaReserva
    {
        public List<Reserva> lista { get; set; }
        public Mensaje mensaje { get; set; }
        public Paginacion paginacion { get; set; }
        public ListaReserva()
        {
            lista = new List<Reserva>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }
}