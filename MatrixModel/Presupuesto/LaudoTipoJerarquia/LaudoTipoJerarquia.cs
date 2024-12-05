
using System;
using Utilitarios;
using System.Collections.Generic;
namespace Presupuesto
{
    public class LaudoTipoJerarquia
    {
        public int IdLaudoJerarquia { get; set; }
        public decimal ValorMonto { get; set; }
        public decimal ValorPer { get; set; }
        public Laudo laudo { get; set; }
        public TipoJerarquia tipojerarquia { get; set; }
        public bool EstadoAuditoria { get; set; }
        public int IdUsuarioAuditoria { get; set; }
        public Mensaje mensaje { get; set; }
        public LaudoTipoJerarquia()
        {
            laudo = new Laudo();
            tipojerarquia = new TipoJerarquia();
            mensaje = new Mensaje();
        }
    }
    public class ListaLaudoTipoJerarquia
    {
        public List<LaudoTipoJerarquia> lista { get; set; }
        public Mensaje mensaje { get; set; }
        public Paginacion paginacion { get; set; }
        public ListaLaudoTipoJerarquia()
        {
            lista = new List<LaudoTipoJerarquia>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }
}