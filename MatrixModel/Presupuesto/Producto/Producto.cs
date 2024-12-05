
using System;
using Utilitarios;
using System.Collections.Generic;
namespace Presupuesto
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public bool EstadoAuditoria { get; set; }
        public int IdUsuarioAuditoria { get; set; }
        public Mensaje mensaje { get; set; }
        public Producto()
        {
            mensaje = new Mensaje();
        }
    }
    public class ListaProducto
    {
        public List<Producto> lista { get; set; }
        public Mensaje mensaje { get; set; }
        public Paginacion paginacion { get; set; }
        public ListaProducto()
        {
            lista = new List<Producto>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }
}