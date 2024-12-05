
using System;
using Utilitarios;
using Personal;
using System.Collections.Generic;
namespace Presupuesto
{
    public class PapModificado
    {
        public int IdPapModificado { get; set; }
        public string Nombre { get; set; }
        public string CodAirHSP { get; set; }
        public string EstadoAirHSP { get; set; }
        public string UnidadOrganica { get; set; }
        public string CodCargoFuncional { get; set; }
        public string NomCargoFuncional { get; set; }
        public string Estado { get; set; }
        public string Situacion { get; set; }
        public string CodigoPap { get; set; }
        public string TipoRegistro { get; set; }
        public Airhsp airhsp{ get; set; }
        public CapDetalle capdetalle { get; set; }
        public MetaPresupuestal metapresupuestal { get; set; }
        public Contrato contrato { get; set; }
        public bool EstadoAuditoria { get; set; }
        public int IdUsuarioAuditoria { get; set; }
        public Mensaje mensaje { get; set; }
        public PapModificado()
        {
            capdetalle = new CapDetalle();
            airhsp = new Airhsp();
            metapresupuestal = new MetaPresupuestal();
            contrato = new Contrato();
            mensaje = new Mensaje();
        }
    }
    public class ListaPapModificado
    {
        public List<PapModificado> lista { get; set; }
        public Mensaje mensaje { get; set; }
        public Paginacion paginacion { get; set; }
        public ListaPapModificado()
        {
            lista = new List<PapModificado>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }
}