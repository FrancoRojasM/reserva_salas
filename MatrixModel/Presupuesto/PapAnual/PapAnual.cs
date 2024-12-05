using System;
using Utilitarios;
using System.Collections.Generic;
namespace Presupuesto
{
    public class PapAnual
    {
        public int Id { get; set; }
        public int IdPapAnual { get; set; }
        public int IdPap { get; set; }
        public string CodConceptos { get; set; }
        public string MntConceptos { get; set; }
        public decimal RemuneracionCap { get; set; }
        public decimal AsignacionFamiliar { get; set; }
        public decimal BonoEF { get; set; }
        public decimal Essalud { get; set; }
        public decimal Eps { get; set; }
        public decimal SctrSalud { get; set; }
        public decimal VidaLey { get; set; }
        public decimal SctrPension { get; set; }
        public decimal EscolaridadCap { get; set; }
        public decimal GratificacionFiestasP { get; set; }
        public decimal GratificacionNavidad { get; set; }
        public decimal CtsMayo { get; set; }
        public decimal CtsNoviembre { get; set; }
        public decimal BonoExtraJulio { get; set; }
        public decimal EpsJulio { get; set; }
        public decimal BonoExtraDiciembre { get; set; }
        public decimal EpsDiciembre { get; set; }
        public decimal SctrSaludGratiJulio { get; set; }
        public decimal SctrPensionGratiJulio { get; set; }
        public decimal SctrSaludGratiDiciembre { get; set; }
        public decimal SctrPensionGratiDiciembre { get; set; }
        public string Situacion { get; set; }
        public bool EstadoAuditoria { get; set; }
        public int IdUsuarioAuditoria { get; set; }

        public Mensaje mensaje { get; set; }

        public PapAnual()
        {
            Id = 0;
            IdPapAnual = 0;
            IdPap = 0;
            CodConceptos = string.Empty;
            MntConceptos = string.Empty;
            RemuneracionCap = 0;
            AsignacionFamiliar= 0;
            BonoEF = 0;
            Essalud = 0;
            Eps = 0;
            SctrSalud = 0;
            VidaLey = 0;
            SctrPension = 0;
            EscolaridadCap = 0;
            GratificacionFiestasP = 0;
            GratificacionNavidad = 0;
            CtsMayo = 0;
            CtsNoviembre = 0;
            BonoExtraJulio = 0;
            EpsJulio = 0;
            BonoExtraDiciembre = 0;
            EpsDiciembre = 0;
            SctrSaludGratiJulio = 0;
            SctrPensionGratiJulio = 0;
            SctrSaludGratiDiciembre = 0;
            SctrPensionGratiDiciembre = 0;
            Situacion = string.Empty;
            EstadoAuditoria = true;
            IdUsuarioAuditoria = 0;
            mensaje = new Mensaje();
        }
        

    }
    public class ListaPapAnual
    {
        public List<PapAnual> lista { get; set; }
        public Mensaje mensaje { get; set; }
        public Paginacion paginacion { get; set; }
        public ListaPapAnual()
        {
            lista = new List<PapAnual>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }
}
