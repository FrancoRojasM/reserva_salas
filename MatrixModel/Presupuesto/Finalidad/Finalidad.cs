
using System;
using Utilitarios;
using System.Collections.Generic;
namespace Presupuesto
{
    public class Finalidad
    {
        public int IdFinalidad { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public bool EstadoAuditoria { get; set; }
        public int IdUsuarioAuditoria { get; set; }
        public Mensaje mensaje { get; set; }
        public Finalidad()
        {
            mensaje = new Mensaje();
        }
    }
    public class ListaFinalidad
    {
        public List<Finalidad> lista { get; set; }
        public Mensaje mensaje { get; set; }
        public Paginacion paginacion { get; set; }
        public ListaFinalidad()
        {
            lista = new List<Finalidad>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }
}