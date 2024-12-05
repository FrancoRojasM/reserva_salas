
using System;
using Utilitarios;
using System.Collections.Generic;
namespace Presupuesto
{
    public class Naturaleza
    {
        public int IdNaturaleza { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public bool EstadoAuditoria { get; set; }
        public int IdUsuarioAuditoria { get; set; }
        public Mensaje mensaje { get; set; }
        public Naturaleza()
        {
            mensaje = new Mensaje();
        }
    }
    public class ListaNaturaleza
    {
        public List<Naturaleza> lista { get; set; }
        public Mensaje mensaje { get; set; }
        public Paginacion paginacion { get; set; }
        public ListaNaturaleza()
        {
            lista = new List<Naturaleza>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }
}