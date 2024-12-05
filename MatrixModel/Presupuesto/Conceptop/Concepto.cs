
using System;
using Utilitarios;
using System.Collections.Generic;
namespace Presupuesto
{
    public class Conceptop
    {
        public int IdConcepto { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public TipoConcepto tipoconcepto { get; set; }
        public int Mes { get; set; }
        public decimal valor_per { get; set; }
        public decimal valor_mnt { get; set; }
        public bool EstadoAuditoria { get; set; }
        public int IdUsuarioAuditoria { get; set; }
        public Mensaje mensaje { get; set; }
        public Conceptop()
        {
            tipoconcepto = new TipoConcepto();
            mensaje = new Mensaje();
        }
    }
    public class ListaConceptop
    {
        public List<Conceptop> lista { get; set; }
        public Mensaje mensaje { get; set; }
        public Paginacion paginacion { get; set; }
        public ListaConceptop()
        {
            lista = new List<Conceptop>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }
}