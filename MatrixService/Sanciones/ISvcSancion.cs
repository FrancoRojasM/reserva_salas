using General;
using Sanciones;
using System;
using System.Collections.Generic;
using System.Data;
using System.ServiceModel;
using System.Threading.Tasks;
using Utilitarios;
namespace MatrixService
{

    public interface ISvcSancion
    {
        Task<ListaSancion> ListarSancionAsync(int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral);
        Task<Sancion> ObtenerSancionAsync(int Id);
        Task<Sancion> ObtenerSancionPorCodeAsync(string Id);
        Task<Sancion> GuardaSancionAsync(int Id, string NombreCompleto, string TipoDocumento, string NroDocumento, string TipoSancion, string ConsejoRegional, string CMP, string NumeroResolucion, DateTime? FechaResolucion, DateTime? FechaNotificacionMedico, DateTime? FechaVencimientoSancion, DateTime? FechaCumplimientoMulta, DateTime? FechaRegistroInscripcion, string EstadoSancion, string SancionImpuesta, string IndicacionNormaVulnerada, int IdUsuarioAuditoria);
        Task<Mensaje> EliminarSancionAsync(int Id, int IdUsuarioAuditoria);
        Task<DataSet> DescargarReporteExcel(int IdUsuarioAuditoria);
        Task<ListaSancion> BuscarSancionesAsync(int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral, string TipoDocumento = null, string NroDocumento = null, string NombreCompleto = null, string ConsejoRegional = null, string TipoSancion = null, string Departamento = null, string Provincia = null, string Distrito = null);
        Task<DataSet> DescargarReporteExcelExterno(int IdUsuarioAuditoria, string TipoDocumento, string NroDocumento, string NombreCompleto, string ConsejoRegional, string TipoSancion, string Departamento, string Provincia, string Distrito);
        Task<List<Sucursal>> ObtenerConsejosRegionalesAsync();
    }
}

