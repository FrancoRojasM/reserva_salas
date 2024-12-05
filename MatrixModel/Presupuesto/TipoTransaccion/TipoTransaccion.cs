
using System;
using Utilitarios;
using System.Collections.Generic;
namespace Presupuesto
{
    public class TipoTransaccion
    {
        public int IdTipoTransaccion { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public bool EstadoAuditoria { get; set; }
        public int IdUsuarioAuditoria { get; set; }
        public Mensaje mensaje { get; set; }
        public TipoTransaccion()
        {
            mensaje = new Mensaje();
        }
    }
    public class ListaTipoTransaccion
    {
        public List<TipoTransaccion> lista { get; set; }
        public Mensaje mensaje { get; set; }
        public Paginacion paginacion { get; set; }
        public ListaTipoTransaccion()
        {
            lista = new List<TipoTransaccion>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }
}