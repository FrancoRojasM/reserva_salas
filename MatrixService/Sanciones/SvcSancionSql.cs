
using General;
using Inventario;
using Sanciones;
using SancionesLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Utilitarios;
namespace MatrixService

{
    public class SvcSancionSql : ISvcSancion
    {
        private SancionLogic sancion;
        public SvcSancionSql()
        {
            sancion = new SancionLogic();
        }

        public async Task<ListaSancion> ListarSancionAsync(int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {

            return await sancion.ListarSancionAsync(DatosGlobales.GestorSqlServer, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
        }

        public async Task<Sancion> ObtenerSancionAsync(int Id)
        {
            return await sancion.ObtenerSancionAsync(DatosGlobales.GestorSqlServer, Id);
        }

        public async Task<Sancion> ObtenerSancionPorCodeAsync(string Id)
        {
            return await sancion.ObtenerSancionPorCodeAsync(DatosGlobales.GestorSqlServer, Id);
        }

        public async Task<Sancion> GuardaSancionAsync(int Id, string NombreCompleto, string TipoDocumento, string NroDocumento, string TipoSancion, string ConsejoRegional, string CMP, string NumeroResolucion, DateTime? FechaResolucion, DateTime? FechaNotificacionMedico, DateTime? FechaVencimientoSancion, DateTime? FechaCumplimientoMulta, DateTime? FechaRegistroInscripcion, string EstadoSancion, string SancionImpuesta, string IndicacionNormaVulnerada, int IdUsuarioAuditoria)
        {
            return await sancion.GuardaSancionAsync(DatosGlobales.GestorSqlServer, Id, NombreCompleto, TipoDocumento, NroDocumento, TipoSancion, ConsejoRegional, CMP, NumeroResolucion, FechaResolucion, FechaNotificacionMedico, FechaVencimientoSancion, FechaCumplimientoMulta, FechaRegistroInscripcion, EstadoSancion, SancionImpuesta, IndicacionNormaVulnerada, IdUsuarioAuditoria);
        }

        public async Task<Mensaje> EliminarSancionAsync(int Id, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();
            mensaje = await sancion.EliminarSancionAsync(DatosGlobales.GestorSqlServer, Id, IdUsuarioAuditoria);
            return mensaje;
        }

        public async Task<DataSet> DescargarReporteExcel(int IdUsuarioAuditoria)
        {
            return await sancion.DescargarReporteExcel(DatosGlobales.GestorSqlServer, IdUsuarioAuditoria);
        }

        public async Task<ListaSancion> BuscarSancionesAsync(int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral, string TipoDocumento = null, string NroDocumento = null, string NombreCompleto = null, string ConsejoRegional = null, string TipoSancion = null, string Departamento = null, string Provincia = null, string Distrito = null)
        {
            return await sancion.BuscarSancionesAsync(DatosGlobales.GestorSqlServer, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral, TipoDocumento, NroDocumento, NombreCompleto, ConsejoRegional, TipoSancion, Departamento, Provincia, Distrito);
        }

        public async Task<DataSet> DescargarReporteExcelExterno(int IdUsuarioAuditoria, string TipoDocumento, string NroDocumento, string NombreCompleto, string ConsejoRegional, string TipoSancion, string Departamento, string Provincia, string Distrito)
        {
            return await sancion.DescargarReporteExcelExterno(DatosGlobales.GestorSqlServer, IdUsuarioAuditoria, TipoDocumento, NroDocumento, NombreCompleto, ConsejoRegional, TipoSancion, Departamento, Provincia, Distrito);
        }

        public async Task<List<Sucursal>> ObtenerConsejosRegionalesAsync()
        {
            return await sancion.ObtenerConsejosRegionalesAsync(DatosGlobales.GestorSqlServer);
        }

    }
}
