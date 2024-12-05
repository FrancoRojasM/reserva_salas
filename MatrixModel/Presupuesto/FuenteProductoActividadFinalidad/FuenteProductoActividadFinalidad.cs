
using System;
using Utilitarios;
using System.Collections.Generic;
namespace Presupuesto
{
    public class FuenteProductoActividadFinalidad
    {
        public int IdFuenteProductoActividadFinalidad { get; set; }
        public FuenteFinanciamiento fuentefinanciamiento { get; set; }
        public Producto producto { get; set; }
        public Actividad actividad { get; set; }
        public Finalidad finalidad { get; set; }
        public bool EstadoAuditoria { get; set; }
        public int IdUsuarioAuditoria { get; set; }
        public Mensaje mensaje { get; set; }
        public FuenteProductoActividadFinalidad()
        {
            fuentefinanciamiento = new FuenteFinanciamiento();
            producto = new Producto();
            actividad = new Actividad();
            finalidad = new Finalidad();
            mensaje = new Mensaje();
        }
    }
    public class ListaFuenteProductoActividadFinalidad
    {
        public List<FuenteProductoActividadFinalidad> lista { get; set; }
        public Mensaje mensaje { get; set; }
        public Paginacion paginacion { get; set; }
        public ListaFuenteProductoActividadFinalidad()
        {
            lista = new List<FuenteProductoActividadFinalidad>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }
}