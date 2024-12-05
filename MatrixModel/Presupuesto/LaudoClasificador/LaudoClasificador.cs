
using System;
using Utilitarios;
using System.Collections.Generic;
namespace Presupuesto
{
    public class LaudoClasificador
    {
        public int IdLaudoClasificador { get; set; }
        public Laudo laudo { get; set; }
        public Clasificador clasificador { get; set; }
        public bool EstadoAuditoria { get; set; }
        public int IdUsuarioAuditoria { get; set; }
        public Mensaje mensaje { get; set; }
        public LaudoClasificador()
        {
            laudo = new Laudo();
            clasificador = new Clasificador();
            mensaje = new Mensaje();
        }
    }
    public class ListaLaudoClasificador
    {
        public List<LaudoClasificador> lista { get; set; }
        public Mensaje mensaje { get; set; }
        public Paginacion paginacion { get; set; }
        public ListaLaudoClasificador()
        {
            lista = new List<LaudoClasificador>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }
}