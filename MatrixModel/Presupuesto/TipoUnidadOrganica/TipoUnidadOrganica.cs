
using System;
using Utilitarios;
using System.Collections.Generic;
namespace Presupuesto
{
    public class TipoUnidadOrganica
    {
        public int IdTipoUnidadOrganica { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public bool EstadoAuditoria { get; set; }
        public int IdUsuarioAuditoria { get; set; }
        public Mensaje mensaje { get; set; }
        public TipoUnidadOrganica()
        {
            mensaje = new Mensaje();
        }
    }
    public class ListaTipoUnidadOrganica
    {
        public List<TipoUnidadOrganica> lista { get; set; }
        public Mensaje mensaje { get; set; }
        public Paginacion paginacion { get; set; }
        public ListaTipoUnidadOrganica()
        {
            lista = new List<TipoUnidadOrganica>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }
}