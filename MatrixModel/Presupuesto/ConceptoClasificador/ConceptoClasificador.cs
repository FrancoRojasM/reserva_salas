
using System;
using Utilitarios;
using System.Collections.Generic;
namespace Presupuesto
{
    public class ConceptoClasificador
    {
        public int IdConceptoClasificador { get; set; }
        public Conceptop concepto { get; set; }
        public Clasificador clasificador { get; set; }
        public bool EstadoAuditoria { get; set; }
        public int IdUsuarioAuditoria { get; set; }
        public Mensaje mensaje { get; set; }
        public ConceptoClasificador()
        {
            concepto = new Conceptop();
            clasificador = new Clasificador();
            mensaje = new Mensaje();
        }
    }
    public class ListaConceptoClasificador
    {
        public List<ConceptoClasificador> lista { get; set; }
        public Mensaje mensaje { get; set; }
        public Paginacion paginacion { get; set; }
        public ListaConceptoClasificador()
        {
            lista = new List<ConceptoClasificador>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }
}