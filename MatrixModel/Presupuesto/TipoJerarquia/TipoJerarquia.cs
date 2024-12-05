
using System;
using Utilitarios;
using System.Collections.Generic;
namespace Presupuesto
{
    public class TipoJerarquia
    {
        public int IdTipoJerarquia { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public decimal Monto { get; set; }
        public bool EstadoAuditoria { get; set; }
        public int IdUsuarioAuditoria { get; set; }
        public Mensaje mensaje { get; set; }
        public TipoJerarquia()
        {
            mensaje = new Mensaje();
        }
    }
    public class ListaTipoJerarquia
    {
        public List<TipoJerarquia> lista { get; set; }
        public Mensaje mensaje { get; set; }
        public Paginacion paginacion { get; set; }
        public ListaTipoJerarquia()
        {
            lista = new List<TipoJerarquia>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }
}