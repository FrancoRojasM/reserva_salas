
using System;
using Utilitarios;
using System.Collections.Generic;
namespace Presupuesto
{
    public class TipoLaudo
    {
        public int IdTipoLaudo { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public bool EstadoAuditoria { get; set; }
        public int IdUsuarioAuditoria { get; set; }
        public Mensaje mensaje { get; set; }
        public TipoLaudo()
        {
            mensaje = new Mensaje();
        }
    }
    public class ListaTipoLaudo
    {
        public List<TipoLaudo> lista { get; set; }
        public Mensaje mensaje { get; set; }
        public Paginacion paginacion { get; set; }
        public ListaTipoLaudo()
        {
            lista = new List<TipoLaudo>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }
}