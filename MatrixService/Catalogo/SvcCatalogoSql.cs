using System.Threading.Tasks;
using Utilitarios;
namespace MatrixService
{
    public class SvcCatalogoSql : ISvcCatalogo
    {
        private UtilitariosLogic.CatalogoLogic catalogo;
        public SvcCatalogoSql()
        {
            catalogo = new UtilitariosLogic.CatalogoLogic();
        }
        public async Task<ListaCatalogo> ListarCatalogoPadreAsync(int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral, string Esquema)
        {
            return await catalogo.ListarCatalogoPadreAsync(DatosGlobales.GestorSqlServer, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral, Esquema);
        }
        public async Task<ListaCatalogo> ListarCatalogoAsync(int IdUsuarioAuditoria, int IdCatalogoPadre, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral, string Esquema)
        {
            return await catalogo.ListarCatalogoAsync(DatosGlobales.GestorSqlServer, IdUsuarioAuditoria, IdCatalogoPadre, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral, Esquema);
        }
        public async Task<Catalogo> GuardaCatalogoAsync(int IdCatalogo, int IdGrupo, int IdCatalogoPadre, int OrdenItem, string Descripcion, string Detalle, bool Activo, int IdUsuarioAuditoria, string Esquema)
        {
            return await catalogo.GuardaCatalogoAsync(DatosGlobales.GestorSqlServer, IdCatalogo, IdGrupo, IdCatalogoPadre, OrdenItem, Descripcion, Detalle, Activo, IdUsuarioAuditoria, Esquema);
        }
        public async Task<Mensaje> EliminarCatalogoAsync(int IdCatalogo, int IdUsuarioAuditoria, string Esquema)
        {
            Mensaje mensaje = new Mensaje();
            mensaje = await catalogo.EliminarCatalogoAsync(DatosGlobales.GestorSqlServer, IdCatalogo, IdUsuarioAuditoria, Esquema);
            return mensaje;
        }
        public async Task<Catalogo> ObtenerCatalogoAsync(int IdCatalogo, int IdUsuarioAuditoria, string Esquema)
        {
            return await catalogo.ObtenerCatalogoAsync(DatosGlobales.GestorSqlServer, IdCatalogo, IdUsuarioAuditoria, Esquema);
        }

        public ListaCatalogo ListarCatalogoCombo(int IdCatalogoPadre, string Esquema) { return catalogo.ListarCatalogoCombo(DatosGlobales.GestorSqlServer, IdCatalogoPadre, Esquema); }
        public async Task<ListaCatalogo> ListarCatalogoComboAsync(int IdCatalogoPadre, string Esquema) { return await catalogo.ListarCatalogoComboAsync(DatosGlobales.GestorSqlServer, IdCatalogoPadre, Esquema); }

        public async Task<ListaCatalogo> ListarCatalogoEspecialidadAsync(int IdCatalogoPadre, string Esquema)
        { return await catalogo.ListarCatalogoEspecialidadAsync(DatosGlobales.GestorSqlServer, IdCatalogoPadre, Esquema); }

        public async Task<ListaCatalogo> ListarCatalogoTipoDocumentoAsync(int IdCatalogoPadre, string Esquema)
        { return await catalogo.ListarCatalogoTipoDocumentoAsync(DatosGlobales.GestorSqlServer, IdCatalogoPadre, Esquema); }

        public async Task<ListaCatalogo> ListarCatalogoTipoDocumentoBusquedaExpediente()
        {
            return await catalogo.ListarCatalogoTipoDocumentoBusquedaExpediente(DatosGlobales.GestorSqlServer);
        }
        public async Task<ListaCatalogo> ListarAreaOrigenBusquedaExpediente()
        {
            return await catalogo.ListarAreaOrigenBusquedaExpediente(DatosGlobales.GestorSqlServer);
        }

        public async Task<ListaCatalogo> ListarTipoSituacionMovimientoTramite()
        {
            return await catalogo.ListarTipoSituacionMovimientoTramite(DatosGlobales.GestorSqlServer);
        }
        public async Task<ListaCatalogo> ListarAreaDestinoBusquedaExpediente()
        {
            return await catalogo.ListarAreaDestinoBusquedaExpediente(DatosGlobales.GestorSqlServer);
        }
        public async Task<ListaCatalogo> ListarCargoOrigenBusquedaExpediente()
        {
            return await catalogo.ListarCargoOrigenBusquedaExpediente(DatosGlobales.GestorSqlServer);
        }
        public async Task<ListaCatalogo> ListarCargoDestinoBusquedaExpediente()
        {
            return await catalogo.ListarCargoDestinoBusquedaExpediente(DatosGlobales.GestorSqlServer);
        }
        public async Task<ListaCatalogo> ListarPeriodoBusquedaExpediente()
        {
            return await catalogo.ListarPeriodoBusquedaExpediente(DatosGlobales.GestorSqlServer);
        }
        public async Task<ListaCatalogo> ListarPeriodoBusquedaDocumentoEspecialista(int IdPersona)
        {
            return await catalogo.ListarPeriodoBusquedaDocumentoEspecialista(DatosGlobales.GestorSqlServer, IdPersona);
        }
        public async Task<ListaCatalogo> ListarPeriodoBusquedaDocumentoJefatura(int IdArea)
        {
            return await catalogo.ListarPeriodoBusquedaDocumentoJefatura(DatosGlobales.GestorSqlServer, IdArea);
        }
        public async Task<ListaCatalogo> ListarCatalogoCategoriaBusquedaExpediente()
        {
            return await catalogo.ListarCatalogoCategoriaBusquedaExpediente(DatosGlobales.GestorSqlServer);
        }
        public async Task<ListaCatalogo> ListarCatalogoCategoriaTramiteSeguimiento()
        {
            return await catalogo.ListarCatalogoCategoriaTramiteSeguimiento(DatosGlobales.GestorSqlServer);
        }
        public async Task<ListaCatalogo> ListarCatalogoTipoDocumentoEspecialista(int IdEmpresa, int IdArea, int IdCargo, int IdPersona)
        {
            return await catalogo.ListarCatalogoTipoDocumentoEspecialista(DatosGlobales.GestorSqlServer, IdEmpresa, IdArea, IdCargo, IdPersona);
        }
        public async Task<ListaCatalogo> ListarCatalogoTipoDocumentoJefatura(int IdArea)
        {
            return await catalogo.ListarCatalogoTipoDocumentoJefatura(DatosGlobales.GestorSqlServer, IdArea);
        }
        public async Task<ListaCatalogo> ListarCatalogoTipoDevolucion(int IdExpedienteDocumentoOrigenDestino)
        {
            return await catalogo.ListarCatalogoTipoDevolucion(DatosGlobales.GestorSqlServer, IdExpedienteDocumentoOrigenDestino);
        }
        public async Task<ListaCatalogo> ListarCatalogoTipoDocumentoBusquedaDocumentoJefatura(int IdArea)
        {
            return await catalogo.ListarCatalogoTipoDocumentoBusquedaDocumentoJefatura(DatosGlobales.GestorSqlServer, IdArea);
        }
        public async Task<ListaCatalogo> ListarCatalogoTipoDocumentoPorAreaPersona(int IdArea, int IdPersona)
        {
            return await catalogo.ListarCatalogoTipoDocumentoPorAreaPersona(DatosGlobales.GestorSqlServer, IdArea, IdPersona);
        }
        public async Task<ListaCatalogo> ListarCatalogoTipoDocumentoBusquedaDocumentoEspecialista(int IdPersona)
        {
            return await catalogo.ListarCatalogoTipoDocumentoBusquedaDocumentoEspecialista(DatosGlobales.GestorSqlServer, IdPersona);
        }
        public async Task<ListaCatalogo> ListarComboPersonaPorAreaPadrePendientes(int IdUsuario, int IdArea)
        {
            return await catalogo.ListarComboPersonaPorAreaPadrePendientes(DatosGlobales.GestorSqlServer, IdUsuario, IdArea);
        }
        public async Task<ListaCatalogo> ListarComboPersonaBusquedaDocumentoJefatura(int IdUsuario, int IdArea)
        {
            return await catalogo.ListarComboPersonaBusquedaDocumentoJefatura(DatosGlobales.GestorSqlServer, IdUsuario, IdArea);
        }
        public async Task<ListaCatalogo> ListarComboDocumentosVinculados(int IdUsuario, int IdExpediente)
        {
            return await catalogo.ListarComboDocumentosVinculados(DatosGlobales.GestorSqlServer, IdUsuario, IdExpediente);
        }

        public async Task<ListaCatalogo> ListarComboAreaPorAreaPadrePendientes(int IdUsuario, int IdArea)
        {
            return await catalogo.ListarComboAreaPorAreaPadrePendientes(DatosGlobales.GestorSqlServer, IdUsuario, IdArea);
        }
        public ListaCatalogo ListaEntidadPideAsync(int IdCatalogoPadre, string Codigo)
        { return catalogo.ListaEntidadPideAsync(DatosGlobales.GestorSqlServer, IdCatalogoPadre, Codigo); }
        public async Task<ListaCatalogo> ListarCatalogoTipoEntidadAsync(int IdCatalogoPadre, string Esquema)
        { return await catalogo.ListarCatalogoTipoEntidadAsync(DatosGlobales.GestorSqlServer, IdCatalogoPadre, Esquema); }
        public async Task<ListaCatalogo> ListarCatalogoTipoEntidadPideAsync(int IdCatalogoPadre, string Esquema)
        { return await catalogo.ListarCatalogoTipoEntidadPideAsync(DatosGlobales.GestorSqlServer, IdCatalogoPadre, Esquema); }
    }

}
