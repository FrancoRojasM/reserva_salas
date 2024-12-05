
using System;
using Utilitarios;
using System.Collections.Generic;
using General;
using Seguridad;

namespace Casilla
{
    public class Administrado
    {
        public int IdAdministrado { get; set; }
        public Persona persona { get; set; }
        public Usuario usuario { get; set; }
        public string EmailNotificacion { get; set; }
        public string NumeroCelularNotificacion { get; set; }
        public string AsientoElectronico { get; set; }
        public string PartidaElectronica { get; set; }
        public string CodigoCorreoValidacion { get; set; }
        public string CodigoCorreoConfirmacion { get; set; }
        public string CodigoTelefonoValidacion { get; set; }
        public string CodigoTelefonoConfirmacion { get; set; }
        public bool Activo { get; set; }
        public bool EstadoAuditoria { get; set; }
        public int IdUsuarioAuditoria { get; set; }
        public Mensaje mensaje { get; set; }
        public Administrado()
        {
            persona = new Persona();
            usuario = new Usuario();
            Activo = true;
            mensaje = new Mensaje();
        }
    }
    public class ListaAdministrado
    {
        public List<Administrado> lista { get; set; }
        public Mensaje mensaje { get; set; }
        public Paginacion paginacion { get; set; }
        public ListaAdministrado()
        {
            lista = new List<Administrado>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }
}