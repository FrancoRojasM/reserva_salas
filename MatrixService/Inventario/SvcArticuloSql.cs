
using Casilla;
using General;
using Inventario;
using InventarioLogic;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Utilitarios;
namespace MatrixService

{
    public class SvcArticuloSql : ISvcArticulo
    {
        private InventarioLogic.ArticuloLogic articulo;
        public SvcArticuloSql()
        {
            articulo = new InventarioLogic.ArticuloLogic();
        }

        public ListaArticulo ListarArticulo(int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {

            return articulo.ListarArticulo(DatosGlobales.GestorSqlServer, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
        }
        public async Task<ListaArticulo> ListarArticuloAsync(int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {

            return await articulo.ListarArticuloAsync(DatosGlobales.GestorSqlServer, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
        }
        public Articulo GuardaArticulo(int Id)
        {

            return articulo.GuardaArticulo(DatosGlobales.GestorSqlServer, Id);
        }
        public async Task<Articulo> GuardaArticuloAsync(int Id, string Codigo_Barra, string ItemCode, string Ubicacion_Region, string Ubicacion_Sede, string Ubicacion_Area, string Ubicacion_Sub_Area, string Piso, string ItemName, string Detalle, string Marca, string Modelo, string Serie, string Material, string Medida, string Color, string Estado, string Condicion_Uso, string Usuario, string Documento, string Cargo, string Gerencia, string Periodo, string Tipo_Inventario, string Tipo_Asignacion, int Sucursal, string image1, string image2, string image3, string image4, int IdUsuarioAuditoria)
        {

            return await articulo.GuardaArticuloAsync(DatosGlobales.GestorSqlServer, Id, Codigo_Barra, ItemCode, Ubicacion_Region, Ubicacion_Sede, Ubicacion_Area, Ubicacion_Sub_Area, Piso, ItemName, Detalle, Marca, Modelo, Serie, Material, Medida, Color, Estado, Condicion_Uso, Usuario, Documento, Cargo, Gerencia, Periodo, Tipo_Inventario, Tipo_Asignacion, Sucursal, image1, image2, image3, image4, IdUsuarioAuditoria);
        }
        public Mensaje EliminarArticulo(int Id, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();
            mensaje = articulo.EliminarArticulo(DatosGlobales.GestorSqlServer, Id, IdUsuarioAuditoria);
            return mensaje;
        }
        public async Task<Mensaje> EliminarArticuloAsync(int Id, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();
            mensaje = await articulo.EliminarArticuloAsync(DatosGlobales.GestorSqlServer, Id, IdUsuarioAuditoria);
            return mensaje;
        }
        public Articulo ObtenerArticulo(int Id)
        {
            return articulo.ObtenerArticulo(DatosGlobales.GestorSqlServer, Id);
        }
        public async Task<Articulo> ObtenerArticuloAsync(int Id)
        {
            return await articulo.ObtenerArticuloAsync(DatosGlobales.GestorSqlServer, Id);
        }

        public async Task<Articulo> ObtenerArticuloPorCodeAsync(string Id)
        {
            return await articulo.ObtenerArticuloPorCodeAsync(DatosGlobales.GestorSqlServer, Id);
        }

        public async Task<string> ReservarCorrelativoAsync()
        {
            return await articulo.ReservarCorrelativoAsync();
        }

        public async Task<Mensaje> LiberarCorrelativoAsync(int Id)
        {
            Mensaje mensaje = new Mensaje();
            mensaje = await articulo.LiberarCorrelativoAsync(Id);
            return mensaje;
        }

        public async Task<List<string>> BuscarCampoAsync(string query, string field)
        {
            return await articulo.BuscarCampoAsync(query, field);
        }

        public async Task<DataSet> DescargarReporteExcel(int IdUsuarioAuditoria)
        {
            return await articulo.DescargarReporteExcel(DatosGlobales.GestorSqlServer, IdUsuarioAuditoria);
        }

        public async Task<List<Sucursal>> ObtenerSucursalesAsync()
        {
            return await articulo.ObtenerSucursalesAsync(DatosGlobales.GestorSqlServer);
        }

        public async Task<DataSet> DescargarReporteCodigosExcel(int IdUsuarioAuditoria)
        {
            return await articulo.DescargarReporteCodigosExcel(DatosGlobales.GestorSqlServer, IdUsuarioAuditoria);
        }

        public async Task<DataSet> DescargarReporteBienesExcel(int IdUsuarioAuditoria)
        {
            return await articulo.DescargarReporteBienesExcel(DatosGlobales.GestorSqlServer, IdUsuarioAuditoria);
        }

        public async Task<DataSet> DescargarReporteCodigosQRExcel(int IdUsuarioAuditoria)
        {
            return await articulo.DescargarReporteCodigosQRExcel(DatosGlobales.GestorSqlServer, IdUsuarioAuditoria);
        }

    }
}
