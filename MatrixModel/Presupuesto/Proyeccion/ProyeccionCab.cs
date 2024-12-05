using System;
using System.Collections.Generic;
using Utilitarios;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presupuesto;

namespace MatrixModel.Presupuesto.Proyeccion
{
    public class ProyeccionCab
    {
        public int IdProyeccionCab { get; set; }
        public string ConceptoImponible { get; set; }
        public string ConceptoCargaSocial { get; set; }
        public string ConceptoCargaContrator { get; set; }
        public string ConceptoOcasional { get; set; }
        public string Meta { get; set; }
        public string Clasificador{ get; set; }
        public string Mes { get; set; }
        public int IdUsuarioAuditoria { get; set; }
        public string NombreUsuario { get; set; }
        public string FechaCreacion { get; set; }
    }

    public class ListaProyeccionCab
    {
        public List<ProyeccionCab> lista { get; set; }
        public Mensaje mensaje { get; set; }
        public Paginacion paginacion { get; set; }
        public ListaProyeccionCab()
        {
            lista = new List<ProyeccionCab>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }
}
