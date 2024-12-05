
using Utilitarios;
using General;
using System.Threading.Tasks;
namespace MatrixService

{
    public class SvcAreaDocumentoSql : ISvcAreaDocumento
    {
        private GeneralLogic.AreaDocumentoLogic areadocumento;
        public SvcAreaDocumentoSql()
        {
            areadocumento = new GeneralLogic.AreaDocumentoLogic();
        }
        public ListaAreaDocumento ListarAreaDocumento(int IdArea, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {
            return areadocumento.ListarAreaDocumento(DatosGlobales.GestorSqlServer, IdArea, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
        }
        public async Task<ListaAreaDocumento> ListarAreaDocumentoAsync(int IdArea, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {
            return await areadocumento.ListarAreaDocumentoAsync(DatosGlobales.GestorSqlServer, IdArea, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
        }
        public AreaDocumento GuardaAreaDocumento(int IdAreaDocumento, int IdArea, int IdCatalogoTipoDocumento, bool Activo, int IdUsuarioAuditoria)
        {
            return areadocumento.GuardaAreaDocumento(DatosGlobales.GestorSqlServer, IdAreaDocumento, IdArea, IdCatalogoTipoDocumento, Activo, IdUsuarioAuditoria);
        }
        public async Task<AreaDocumento> GuardaAreaDocumentoAsync(int IdAreaDocumento, int IdArea, int IdCatalogoTipoDocumento, bool Activo, int IdUsuarioAuditoria)
        {
            return await areadocumento.GuardaAreaDocumentoAsync(DatosGlobales.GestorSqlServer, IdAreaDocumento, IdArea, IdCatalogoTipoDocumento, Activo, IdUsuarioAuditoria);
        }
        public Mensaje EliminarAreaDocumento(int IdAreaDocumento, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();
            mensaje = areadocumento.EliminarAreaDocumento(DatosGlobales.GestorSqlServer, IdAreaDocumento, IdUsuarioAuditoria);
            return mensaje;
        }
        public async Task<Mensaje> EliminarAreaDocumentoAsync(int IdAreaDocumento, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();
            mensaje = await areadocumento.EliminarAreaDocumentoAsync(DatosGlobales.GestorSqlServer, IdAreaDocumento, IdUsuarioAuditoria);
            return mensaje;
        }
        public AreaDocumento ObtenerAreaDocumento(int IdAreaDocumento, int IdUsuarioAuditoria)
        {
            return areadocumento.ObtenerAreaDocumento(DatosGlobales.GestorSqlServer, IdAreaDocumento, IdUsuarioAuditoria);
        }
        public async Task<AreaDocumento> ObtenerAreaDocumentoAsync(int IdAreaDocumento, int IdUsuarioAuditoria)
        {
            return await areadocumento.ObtenerAreaDocumentoAsync(DatosGlobales.GestorSqlServer, IdAreaDocumento, IdUsuarioAuditoria);
        }
    }
}
