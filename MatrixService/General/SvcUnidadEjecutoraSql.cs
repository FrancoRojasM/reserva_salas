
using Utilitarios;
using General;
using System.Threading.Tasks;
namespace MatrixService

{
    public class SvcUnidadEjecutoraSql : ISvcUnidadEjecutora
    {
        private GeneralLogic.UnidadEjecutoraLogic unidadejecutora;
        public SvcUnidadEjecutoraSql()
        {
            unidadejecutora = new GeneralLogic.UnidadEjecutoraLogic();
        }
        public ListaUnidadEjecutora ListarUnidadEjecutora(int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {
            return unidadejecutora.ListarUnidadEjecutora(DatosGlobales.GestorSqlServer, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
        }
        public async Task<ListaUnidadEjecutora> ListarUnidadEjecutoraAsync(int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {
            return await unidadejecutora.ListarUnidadEjecutoraAsync(DatosGlobales.GestorSqlServer, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
        }
        public UnidadEjecutora GuardaUnidadEjecutora(int IdUnidadEjecutora, string Descripcion, int IdUsuarioAuditoria)
        {
            return unidadejecutora.GuardaUnidadEjecutora(DatosGlobales.GestorSqlServer, IdUnidadEjecutora, Descripcion, IdUsuarioAuditoria);
        }
        public async Task<UnidadEjecutora> GuardaUnidadEjecutoraAsync(int IdUnidadEjecutora, string Descripcion, int IdUsuarioAuditoria)
        {
            return await unidadejecutora.GuardaUnidadEjecutoraAsync(DatosGlobales.GestorSqlServer, IdUnidadEjecutora, Descripcion, IdUsuarioAuditoria);
        }
        public Mensaje EliminarUnidadEjecutora(int IdUnidadEjecutora, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();
            mensaje = unidadejecutora.EliminarUnidadEjecutora(DatosGlobales.GestorSqlServer, IdUnidadEjecutora, IdUsuarioAuditoria);
            return mensaje;
        }
        public async Task<Mensaje> EliminarUnidadEjecutoraAsync(int IdUnidadEjecutora, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();
            mensaje = await unidadejecutora.EliminarUnidadEjecutoraAsync(DatosGlobales.GestorSqlServer, IdUnidadEjecutora, IdUsuarioAuditoria);
            return mensaje;
        }
        public UnidadEjecutora ObtenerUnidadEjecutora(int IdUnidadEjecutora, int IdUsuarioAuditoria)
        {
            return unidadejecutora.ObtenerUnidadEjecutora(DatosGlobales.GestorSqlServer, IdUnidadEjecutora, IdUsuarioAuditoria);
        }
        public async Task<UnidadEjecutora> ObtenerUnidadEjecutoraAsync(int IdUnidadEjecutora, int IdUsuarioAuditoria)
        {
            return await unidadejecutora.ObtenerUnidadEjecutoraAsync(DatosGlobales.GestorSqlServer, IdUnidadEjecutora, IdUsuarioAuditoria);
        }
    }
}
