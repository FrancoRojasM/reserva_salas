using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilitarios;

namespace MatrixModel.Presupuesto.PapLaudo
{
    public class PapLaudoDetalle
    {
        public int IdPap { get; set;}
        public string UnidadOrganica { get; set; }
        public string Meta { get; set;}
        public string NroPap { get; set;}
        public string NroCap { get; set;}   
        public string TipoRegistro { get; set;}
        public string NroAirhsp { get;set;}
        public string EstadoPap { get; set;}
        public string EstadoCap { get; set; }
        public string EstadoAirhsp { get;set;}
        public string TipoRegimen { get; set;}
        public string GrupoOcupacional { get; set;}
        public string CargoCap { get; set;}
        public string Clasificador { get;set;}
        public string CodCargoAirhsp { get; set;}
        public string CodTipoJerarquia { get; set;}
        public decimal MontoEscala { get; set;}
        public decimal Bonificacion { get; set;}
        public decimal AsignacionFam { get; set;}
        public decimal BaseImponible { get; set;}
        public string  perlaudo1 { get; set;}
        public decimal mntlaudo1 { get; set;}
        public string  perlaudo2 { get; set;}
        public decimal mntlaudo2 { get; set;}
        public string  perlaudo3 { get; set;}
        public decimal mntlaudo3 { get; set;}
        public string  perlaudo4 { get; set;}
        public decimal mntlaudo4 { get; set;}
        public string  perlaudo5 { get; set;}
        public decimal mntlaudo5 { get; set;}
    }
    public class ListaPapLaudoDetalle
    {
        public List<PapLaudoDetalle> lista { get; set; }
        public Mensaje mensaje { get; set; }
        public Paginacion paginacion { get; set; }

        public ListaPapLaudoDetalle()
        {
            lista = new List<PapLaudoDetalle>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }
}
