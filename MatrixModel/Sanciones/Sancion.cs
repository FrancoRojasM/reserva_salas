using Utilitarios;
using System.Collections.Generic;
using System;
namespace Sanciones
{
    public class Sancion
    {
        public int Id { get; set; }

        public int IdCatalogoTipoDocumentoPersonal { get; set; }

        public int IdCatalogoTipoSancion { get; set; }

        public int IdSucursal { get; set; }

        public string TipoDocumento { get; set; }

        public string NroDocumento { get; set; }

        public string NombreCompleto { get; set; }

        public string TipoSancion { get; set; }

        public string ConsejoRegional { get; set; }

        public string CMP { get; set; }

        public string NumeroResolucion { get; set; }

        public DateTime? FechaResolucion { get; set; }

        public DateTime? FechaNotificacionMedico { get; set; }

        public DateTime? FechaVencimientoSancion { get; set; }

        public DateTime? FechaCumplimientoMulta { get; set; }

        public DateTime? FechaRegistroInscripcion { get; set; }

        public string EstadoSancion { get; set; }

        public string SancionImpuesta { get; set; }

        public string IndicacionNormaVulnerada { get; set; }

        public string Departamento { get; set; }

        public string Provincia { get; set; }

        public string Distrito { get; set; }

        public DateTime? FE_CREA { get; set; }

        public DateTime? FE_MODI { get; set; }

        public int? US_CREA { get; set; }

        public int? US_MODI { get; set; }

        public int? STATUS { get; set; }

        public int IdUsuarioAuditoria { get; set; }

        public Mensaje mensaje { get; set; }

        public Sancion()
        {
            mensaje = new Mensaje();
        }
    }

    public class SancionSearch
    {
        public string TipoDocumento { get; set; }

        public string NroDocumento { get; set; }

        public string NombreCompleto { get; set; }

        public string ConsejoRegional { get; set; }

        public string TipoSancion { get; set; }

        public string Departamento { get; set; }

        public string Provincia { get; set; }

        public string Distrito { get; set; }
    }


    public class ListaSancion
    {

        public List<Sancion> lista { get; set; }

        public Mensaje mensaje { get; set; }

        public Paginacion paginacion { get; set; }
        public ListaSancion()
        {
            lista = new List<Sancion>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }

}



