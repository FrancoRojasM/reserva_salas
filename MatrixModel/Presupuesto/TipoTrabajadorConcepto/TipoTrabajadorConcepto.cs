
using System;
using Utilitarios;
using System.Collections.Generic;
namespace Presupuesto
{
    public class TipoTrabajadorConcepto
    {
        public int IdTipoTrabajadorConcepto { get; set; }
        public TipoTrabajador tipotrabajador { get; set; }
        public Conceptop concepto { get; set; }
        public bool EstadoAuditoria { get; set; }
        public int IdUsuarioAuditoria { get; set; }
        public Mensaje mensaje { get; set; }
        public TipoTrabajadorConcepto()
        {
            tipotrabajador = new TipoTrabajador();
            concepto = new Conceptop();
            mensaje = new Mensaje();
        }
    }
    public class ListaTipoTrabajadorConcepto
    {
        public List<TipoTrabajadorConcepto> lista { get; set; }
        public Mensaje mensaje { get; set; }
        public Paginacion paginacion { get; set; }
        public ListaTipoTrabajadorConcepto()
        {
            lista = new List<TipoTrabajadorConcepto>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }
}