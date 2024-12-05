using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilitarios;

namespace MatrixModel.Presupuesto.PapAnual
{
    public class PapAnualDetalle
    {
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
        public int IdPap { get; set;}
        public decimal MontoEscala { get; set;}
        public decimal AsignacionFamiliar { get; set;}
        public decimal BonoEF { get; set;}
        public decimal IngresoImponible { get; set;}
        public decimal Essalud { get; set;}
        public decimal Eps { get; set;}
        public decimal SctrSalud { get; set;}
        public decimal VidaLey { get;set;}
        public decimal SctrPension { get; set;}
        public decimal EscolaridadCap { get;set;}
        public decimal GratificacionJulio { get; set;}
        public decimal GratificacionDiciembre { get; set;}
        public decimal CtsMayo { get; set;}
        public decimal CtsNoviembre { get; set;}
        public decimal BonoExtraJulio { get; set;}
        public decimal EpsJulio { get; set;}
        public decimal BonoExtraDiciembre { get; set;}
        public decimal EpsDiciembre { get; set;}
        public decimal SctrSaludGratiJulio { get; set;}
        public decimal SctrPensionGratiJulio { get; set;}
        public decimal SctrSaludGratiDiciembre { get; set;}
        public decimal SctrPensionGratiDiciembre { get; set;}
        public decimal MensIngresoImponible { get; set;}
        public decimal MensCargaSocial { get; set;}
        public decimal MensIngresoNoImponible { get; set;}
        public decimal TotalMensual { get; set;}
        public decimal TotalAnual { get;set;}
        public decimal OcasEscolaridad { get;set;}
        public decimal OcaAguinaldosGratif { get; set;}
        public decimal OcaCTS { get; set;}
        public decimal OcaOtrosIngresos { get; set;}
        public decimal OcaAportes { get; set;}
        public decimal TotalOcasional { get; set;}
        public decimal CostoTotalAnual { get; set;}
    }
    public class ListaPapAnualDetalle
    {
        public List<PapAnualDetalle> lista { get; set; }
        public Mensaje mensaje { get; set; }
        public Paginacion paginacion { get; set; }

        public ListaPapAnualDetalle()
        {
            lista = new List<PapAnualDetalle>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }
}
