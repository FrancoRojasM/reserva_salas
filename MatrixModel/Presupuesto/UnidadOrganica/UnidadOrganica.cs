
using System;
using Utilitarios;
using System.Collections.Generic;
namespace Presupuesto
{
    public class UnidadOrganica
    {
        public int IdUnidadOrganica { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public Naturaleza naturaleza { get; set; }
        public TipoUnidadOrganica tipounidadorganica { get; set; }
        public bool EstadoAuditoria { get; set; }
        public int IdUsuarioAuditoria { get; set; }
        public Mensaje mensaje { get; set; }
        public UnidadOrganica()
        {
            naturaleza = new Naturaleza();
            tipounidadorganica = new TipoUnidadOrganica();
            mensaje = new Mensaje();
        }
    }
    public class ListaUnidadOrganica
    {
        public List<UnidadOrganica> lista { get; set; }
        public Mensaje mensaje { get; set; }
        public Paginacion paginacion { get; set; }
        public ListaUnidadOrganica()
        {
            lista = new List<UnidadOrganica>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }
}