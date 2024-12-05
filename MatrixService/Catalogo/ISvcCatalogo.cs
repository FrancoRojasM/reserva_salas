using System.Threading.Tasks;
using Utilitarios;
namespace MatrixService
{
    
   public  interface ISvcCatalogo
    {
        Task<ListaCatalogo> ListarCatalogoPadreAsync(int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral, string Esquema);
        Task<ListaCatalogo> ListarCatalogoAsync(int IdUsuarioAuditoria, int IdCatalogoPadre, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral, string Esquema);
        Task<Catalogo> GuardaCatalogoAsync(int IdCatalogo, int IdGrupo, int IdCatalogoPadre, int OrdenItem, string Descripcion, string Detalle, bool Activo, int IdUsuarioAuditoria, string Esquema);
        Task<Mensaje> EliminarCatalogoAsync(int IdCatalogo, int IdUsuarioAuditoria, string Esquema);
        Task<Catalogo> ObtenerCatalogoAsync(int IdCatalogo, int IdUsuarioAuditoria, string Esquema);

        ListaCatalogo ListarCatalogoCombo(int IdCatalogoPadre, string Esquema);
        Task<ListaCatalogo> ListarCatalogoComboAsync(int IdCatalogoPadre, string Esquema);

        Task<ListaCatalogo> ListarCatalogoEspecialidadAsync(int IdCatalogoPadre, string Esquema);

        Task<ListaCatalogo> ListarCatalogoTipoDocumentoAsync(int IdCatalogoPadre, string Esquema);

        Task<ListaCatalogo> ListarCatalogoTipoDocumentoBusquedaExpediente();
        Task<ListaCatalogo> ListarAreaOrigenBusquedaExpediente();
        Task<ListaCatalogo> ListarAreaDestinoBusquedaExpediente();
        Task<ListaCatalogo> ListarTipoSituacionMovimientoTramite();
        
        Task<ListaCatalogo> ListarCargoOrigenBusquedaExpediente();
        Task<ListaCatalogo> ListarCargoDestinoBusquedaExpediente();
        Task<ListaCatalogo> ListarPeriodoBusquedaExpediente();
        Task<ListaCatalogo> ListarPeriodoBusquedaDocumentoEspecialista(int IdPersona);
        Task<ListaCatalogo> ListarPeriodoBusquedaDocumentoJefatura(int IdArea);
        Task<ListaCatalogo> ListarCatalogoCategoriaBusquedaExpediente();
        Task<ListaCatalogo> ListarCatalogoCategoriaTramiteSeguimiento();
        Task<ListaCatalogo> ListarCatalogoTipoDocumentoEspecialista(int IdEmpresa, int IdArea, int IdCargo, int IdPersona);
        Task<ListaCatalogo> ListarCatalogoTipoDocumentoJefatura(int IdArea);
        Task<ListaCatalogo> ListarCatalogoTipoDevolucion(int IdExpedienteDocumentoOrigenDestino);
        Task<ListaCatalogo> ListarCatalogoTipoDocumentoBusquedaDocumentoJefatura(int IdArea);
        Task<ListaCatalogo> ListarCatalogoTipoDocumentoPorAreaPersona(int IdArea, int IdPersona);
        Task<ListaCatalogo> ListarCatalogoTipoDocumentoBusquedaDocumentoEspecialista(int IdPersona);
        Task<ListaCatalogo> ListarComboPersonaPorAreaPadrePendientes(int IdUsuario, int IdArea);
        Task<ListaCatalogo> ListarComboPersonaBusquedaDocumentoJefatura(int IdUsuario, int IdArea);
        Task<ListaCatalogo> ListarComboDocumentosVinculados(int IdUsuario, int IdExpediente);
        Task<ListaCatalogo> ListarComboAreaPorAreaPadrePendientes(int IdUsuario, int IdArea);
        ListaCatalogo ListaEntidadPideAsync(int IdCatalogoPadre, string Codigo);
        Task<ListaCatalogo> ListarCatalogoTipoEntidadAsync(int IdCatalogoPadre, string Esquema);
        Task<ListaCatalogo> ListarCatalogoTipoEntidadPideAsync(int IdCatalogoPadre, string Esquema);

    }

}
		
