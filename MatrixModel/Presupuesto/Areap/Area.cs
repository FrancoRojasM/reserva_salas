
using System;
using Utilitarios;
using System.Collections.Generic;
namespace Presupuesto
{
    public class Areap
    {
        public int IdArea { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public UnidadOrganica unidadorganica { get; set; }
        public bool EstadoAuditoria { get; set; }
        public int IdUsuarioAuditoria { get; set; }
        public Mensaje mensaje { get; set; }
        public Areap()
        {
            unidadorganica = new UnidadOrganica();
            mensaje = new Mensaje();
        }
    }
    public class ListaAreap
    {
        public List<Areap> lista { get; set; }
        public Mensaje mensaje { get; set; }
        public Paginacion paginacion { get; set; }
        public ListaAreap()
        {
            lista = new List<Areap>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }
}