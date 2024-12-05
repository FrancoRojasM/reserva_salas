
using System;
using Utilitarios;
using General;
using System.Collections.Generic;
namespace Presupuesto
{
    public class FuenteFinanciamiento
    {
        public int IdFuenteFinanciamiento { get; set; }
        public Periodo periodo { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public bool EstadoAuditoria { get; set; }
        public int IdUsuarioAuditoria { get; set; }
        public Mensaje mensaje { get; set; }
        public FuenteFinanciamiento()
        {
            periodo = new Periodo();
            mensaje = new Mensaje();
        }
    }
    public class ListaFuenteFinanciamiento
    {
        public List<FuenteFinanciamiento> lista { get; set; }
        public Mensaje mensaje { get; set; }
        public Paginacion paginacion { get; set; }
        public ListaFuenteFinanciamiento()
        {
            lista = new List<FuenteFinanciamiento>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }
}