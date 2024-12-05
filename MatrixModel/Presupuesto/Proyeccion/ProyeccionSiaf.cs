using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilitarios;

namespace MatrixModel.Presupuesto.Proyeccion
{
    public class ProyeccionSiaf
    {
        public int IdProyeccion { get; set; }
        public int IdSiaf { get; set; }
        public string Periodo { get; set; }
        public string Fuente { get; set; }
        public string Producto { get; set; }
        public string TipoTransaccion { get; set; }
        public string Generica { get; set; }
        public string SubGenerica { get; set; }
        public string SubGenericaDetalle { get; set; }
        public string Especifica { get; set; }
        public string Clasificador { get; set; }
        public string Meta { get; set; }
        public decimal Mt_pia { get; set; }
        public decimal Mt_pim { get; set; }
        public decimal Mt_cert { get; set; }
        public decimal Mt_eje { get; set; }
        public int CantOcupado { get; set; }
        public decimal MntOcupado  { get; set; }
        public int CantVacante { get; set; }
        public decimal MntVacante { get; set; }
        public int CantTotal { get; set; }
        public decimal MntTotal { get; set; }
        public string Mnemonico { get; set; }
        public string Partida { get; set; }
        public decimal SaldoPim { get; set; }
        public decimal SaldoCert { get; set; }

    }

    public class ListaProyeccionSiaf
    {
        public List<ProyeccionSiaf> lista { get; set; }
        public Mensaje mensaje { get; set; }
        public Paginacion paginacion { get; set; }
        public ListaProyeccionSiaf()
        {
            lista = new List<ProyeccionSiaf>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }
}
