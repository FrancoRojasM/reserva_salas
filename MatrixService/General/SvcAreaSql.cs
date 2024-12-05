using General;
using System.Threading.Tasks;
using Utilitarios;
namespace MatrixService
{
    public class SvcAreaSql : ISvcArea
    {
        private GeneralLogic.AreaLogic area;
        public SvcAreaSql()
        {
            area = new GeneralLogic.AreaLogic();
        }
        public ListaArea ListarArea(int IdEmpresa, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {
            return area.ListarArea(DatosGlobales.GestorSqlServer, IdEmpresa, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
        }
        public Area GuardaArea(int IdArea, int IdEmpresa, int IdAreaPadre, string NombreArea, string Abreviatura, string Sigla, bool Activo, int IdCatalogoTipoArea, int IdUsuarioAuditoria)
        {
            return area.GuardaArea(DatosGlobales.GestorSqlServer, IdArea, IdEmpresa, IdAreaPadre, NombreArea, Abreviatura, Sigla, Activo, IdCatalogoTipoArea, IdUsuarioAuditoria);
        }
        public Mensaje EliminarArea(int IdArea, int IdUsuarioAuditoria) 
        {
            Mensaje mensaje = new Mensaje();
            mensaje = area.EliminarArea(DatosGlobales.GestorSqlServer, IdArea, IdUsuarioAuditoria);
            return mensaje;
        }
        public Area ObtenerArea(int IdArea)
        {
            return area.ObtenerArea(DatosGlobales.GestorSqlServer, IdArea);
        }
        public ListaArea ListarComboArea(int IdEmpresa)
        {
            return area.ListarComboArea(DatosGlobales.GestorSqlServer, IdEmpresa);
        }
        public ListaArea ListarComboAreaPadre(int IdEmpresa)
        {
            return area.ListarComboArea(DatosGlobales.GestorSqlServer, IdEmpresa);
        }

        //METODOS ASINCRONOS
        public async Task<ListaArea> ListarAreaAsync(int IdEmpresa, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {
            return await area.ListarAreaAsync(DatosGlobales.GestorSqlServer, IdEmpresa, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
        }
        public async Task<Area> GuardaAreaAsync(int IdArea, int IdEmpresa, int IdAreaPadre, string NombreArea, string Abreviatura, string Sigla, bool Activo, int IdCatalogoTipoArea,bool VerRecepcion, int IdUsuarioAuditoria)
        {
            return await area.GuardaAreaAsync(DatosGlobales.GestorSqlServer, IdArea, IdEmpresa, IdAreaPadre, NombreArea, Abreviatura, Sigla, Activo, IdCatalogoTipoArea, VerRecepcion, IdUsuarioAuditoria);
        }
        public async Task<Mensaje> EliminarAreaAsync(int IdArea, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();
            mensaje = await area.EliminarAreaAsync(DatosGlobales.GestorSqlServer, IdArea, IdUsuarioAuditoria);
            return mensaje;
        }
        public async Task<Area> ObtenerAreaAsync(int IdArea)
        {
            return await area.ObtenerAreaAsync(DatosGlobales.GestorSqlServer, IdArea);
        }
        public async Task<ListaArea> ListarComboAreaAsync(int IdEmpresa)
        {
            return await area.ListarComboAreaAsync(DatosGlobales.GestorSqlServer, IdEmpresa);
        }
        public async Task<ListaArea> ListarComboArea3Async(int IdUsuarioAuditoria)
        {
            return await area.ListarComboArea3Async(DatosGlobales.GestorSqlServer, IdUsuarioAuditoria);
        }
        public async Task<ListaArea> ListarComboAreaPadreAsync(int IdEmpresa)
        {
            return await area.ListarComboAreaPadreAsync(DatosGlobales.GestorSqlServer, IdEmpresa);
        }
    }
}
