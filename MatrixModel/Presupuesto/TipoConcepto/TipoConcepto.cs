
using System;
using Utilitarios;
using System.Collections.Generic;
namespace Presupuesto
{
    public class TipoConcepto
    {
        public int IdTipoConcepto { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public bool EstadoAuditoria { get; set; }
        public int IdUsuarioAuditoria { get; set; }
        public Mensaje mensaje { get; set; }
        public TipoConcepto()
        {
            mensaje = new Mensaje();
        }
    }
    public class ListaTipoConcepto
    {
        public List<TipoConcepto> lista { get; set; }
        public Mensaje mensaje { get; set; }
        public Paginacion paginacion { get; set; }
        public ListaTipoConcepto()
        {
            lista = new List<TipoConcepto>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }
}