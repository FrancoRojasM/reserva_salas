
using System;
using Utilitarios;
using System.Collections.Generic;
namespace Presupuesto
{
    public class CAP
    {
        public int IdCap { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public TipoTrabajador tipotrabajador { get; set; }
        public DateTime? FechaResolucion { get; set; }
        public DateTime? FechaVigencia { get; set; }
        public bool EstadoAuditoria { get; set; }
        public int IdUsuarioAuditoria { get; set; }
        public Mensaje mensaje { get; set; }
        public CAP()
        {
            tipotrabajador = new TipoTrabajador();
            mensaje = new Mensaje();
        }
    }
    public class ListaCAP
    {
        public List<CAP> lista { get; set; }
        public Mensaje mensaje { get; set; }
        public Paginacion paginacion { get; set; }
        public ListaCAP()
        {
            lista = new List<CAP>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }
}