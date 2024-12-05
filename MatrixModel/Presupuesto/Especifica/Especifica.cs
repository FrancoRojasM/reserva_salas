
using System;
using Utilitarios;
using System.Collections.Generic;
namespace Presupuesto
{
    public class Especifica
    {
        public int IdEspecifica { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public bool EstadoAuditoria { get; set; }
        public int IdUsuarioAuditoria { get; set; }
        public Mensaje mensaje { get; set; }
        public Especifica()
        {
            mensaje = new Mensaje();
        }
    }
    public class ListaEspecifica
    {
        public List<Especifica> lista { get; set; }
        public Mensaje mensaje { get; set; }
        public Paginacion paginacion { get; set; }
        public ListaEspecifica()
        {
            lista = new List<Especifica>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }
}