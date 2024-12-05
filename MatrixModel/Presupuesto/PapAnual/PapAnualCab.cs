using MatrixModel.Presupuesto.Proyeccion;
using System.Collections.Generic;
using Utilitarios;

namespace MatrixModel.Presupuesto.PapAnual
{
    public class PapAnualCab
    {
        public int IdPapAnualCab { get; set; }
        public int IdUsuarioAuditoria { get; set; }
        public string NombreUsuario { get; set; }
        public string FechaCreacion { get; set; }
    }
    public class ListaPapAnualCab
    {
        public List<PapAnualCab> lista { get; set; }
        public Mensaje mensaje { get; set; }
        public Paginacion paginacion { get; set; }
        public ListaPapAnualCab()
        {
            lista = new List<PapAnualCab>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }

}
