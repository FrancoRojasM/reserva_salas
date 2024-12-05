
using System;
using Utilitarios;
using System.Collections.Generic;
namespace Presupuesto
{
    public class CapDetalle
    {
        public int IdCapDetalle { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public string Estado { get; set; }
        public CAP cap { get; set; }
        public UnidadOrganica unidadorganica { get; set; }
        public TipoPersonal tipopersonal { get; set; }
        public TipoJerarquia tipojerarquia { get; set; }
        public bool EstadoAuditoria { get; set; }
        public int IdUsuarioAuditoria { get; set; }
        public Mensaje mensaje { get; set; }
        public CapDetalle()
        {
            cap = new CAP();
            unidadorganica = new UnidadOrganica();
            tipopersonal = new TipoPersonal();
            tipojerarquia = new TipoJerarquia();
            mensaje = new Mensaje();
        }
    }
    public class ListaCapDetalle
    {
        public List<CapDetalle> lista { get; set; }
        public Mensaje mensaje { get; set; }
        public Paginacion paginacion { get; set; }
        public ListaCapDetalle()
        {
            lista = new List<CapDetalle>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }
}