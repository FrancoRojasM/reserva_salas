using System;
using Utilitarios;
using System.Collections.Generic;
namespace Presupuesto
{
    public class Laudo
    {
        public int IdLaudo { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public bool EstadoEjecucion { get; set; }
        public decimal ValorMonto { get; set; }
        public decimal ValorPer { get; set; }
        public TipoLaudo tipolaudo { get; set; }
        public bool EstadoAuditoria { get; set; }
        public int IdUsuarioAuditoria { get; set; }
        public Mensaje mensaje { get; set; }
        public Laudo()
        {
            tipolaudo = new TipoLaudo();
            mensaje = new Mensaje();
        }
    }
    public class ListaLaudo
    {
        public List<Laudo> lista { get; set; }
        public Mensaje mensaje { get; set; }
        public Paginacion paginacion { get; set; }
        public ListaLaudo()
        {
            lista = new List<Laudo>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }
}