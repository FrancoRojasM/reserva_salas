using Inventario;
using General;
using System.Collections.Generic;
using System.Data;
using System.ServiceModel;
using System.Threading.Tasks;
using Utilitarios;
namespace MatrixService
{

    public interface ISvcArticulo
    {
        ListaArticulo ListarArticulo(int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral);
        Task<ListaArticulo> ListarArticuloAsync(int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral);
        Articulo GuardaArticulo(int Id);
        Task<Articulo> GuardaArticuloAsync(int Id, string Codigo_Barra, string ItemCode, string Ubicacion_Region, string Ubicacion_Sede, string Ubicacion_Area, string Ubicacion_Sub_Area, string Piso, string ItemName, string Detalle, string Marca, string Modelo, string Serie, string Material, string Medida, string Color, string Estado, string Condicion_Uso, string Usuario, string Documento, string Cargo, string Gerencia, string Periodo, string Tipo_Inventario, string Tipo_Asignacion, int Sucursal, string image1, string image2, string image3, string image4, int IdUsuarioAuditoria);
        Mensaje EliminarArticulo(int Id, int IdUsuarioAuditoria);
        Task<Mensaje> EliminarArticuloAsync(int Id, int IdUsuarioAuditoria);
        Articulo ObtenerArticulo(int Id);
        Task<Articulo> ObtenerArticuloAsync(int Id);
        Task<Articulo> ObtenerArticuloPorCodeAsync(string Id);
        Task<string> ReservarCorrelativoAsync();
        Task<Mensaje> LiberarCorrelativoAsync(int Id);
        Task<List<string>> BuscarCampoAsync(string query, string field);
        Task<DataSet> DescargarReporteExcel(int IdUsuarioAuditoria);
        Task<List<Sucursal>> ObtenerSucursalesAsync();
        Task<DataSet> DescargarReporteCodigosExcel(int IdUsuarioAuditoria);
        Task<DataSet> DescargarReporteBienesExcel(int IdUsuarioAuditoria);
        Task<DataSet> DescargarReporteCodigosQRExcel(int IdUsuarioAuditoria);
    }
}

