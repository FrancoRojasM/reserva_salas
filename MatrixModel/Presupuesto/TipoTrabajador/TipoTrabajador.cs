
using System;
using Utilitarios;
using System.Collections.Generic;
namespace Presupuesto
{
    public class TipoTrabajador
    {
        public int IdTipoTrabajador { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public string Regimen { get; set; }
        public bool EstadoAuditoria { get; set; }
        public int IdUsuarioAuditoria { get; set; }
        public Mensaje mensaje { get; set; }
        public TipoTrabajador()
        {
            mensaje = new Mensaje();
        }
    }
    public class ListaTipoTrabajador
    {
        public List<TipoTrabajador> lista { get; set; }
        public Mensaje mensaje { get; set; }
        public Paginacion paginacion { get; set; }
        public ListaTipoTrabajador()
        {
            lista = new List<TipoTrabajador>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }
}