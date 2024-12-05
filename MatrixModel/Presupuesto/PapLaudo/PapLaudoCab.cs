using MatrixModel.Presupuesto.Proyeccion;
using System.Collections.Generic;
using Utilitarios;

namespace MatrixModel.Presupuesto.PapLaudo
{
    public class PapLaudoCab
    {
        public int IdPapLaudoCab { get; set; }
        public int IdUsuarioAuditoria { get; set; }
        public string NombreUsuario { get; set; }
        public string FechaCreacion { get; set; }
    }
    public class ListaPapLaudoCab
    {
        public List<PapLaudoCab> lista { get; set; }
        public Mensaje mensaje { get; set; }
        public Paginacion paginacion { get; set; }
        public ListaPapLaudoCab()
        {
            lista = new List<PapLaudoCab>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }

}
