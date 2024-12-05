
using System;
using Utilitarios;
using System.Collections.Generic;
namespace Presupuesto
{
    public class Generica
    {
        public int IdGenerica { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public bool EstadoAuditoria { get; set; }
        public int IdUsuarioAuditoria { get; set; }
        public Mensaje mensaje { get; set; }
        public Generica()
        {
            mensaje = new Mensaje();
        }
    }
    public class ListaGenerica
    {
        public List<Generica> lista { get; set; }
        public Mensaje mensaje { get; set; }
        public Paginacion paginacion { get; set; }
        public ListaGenerica()
        {
            lista = new List<Generica>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }
}