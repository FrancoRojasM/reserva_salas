using Utilitarios;
using System.Threading.Tasks;
using General;
namespace MatrixService
{
	public interface ISvcAreaDocumento
	{
		ListaAreaDocumento ListarAreaDocumento(int IdArea, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral);
		Task<ListaAreaDocumento> ListarAreaDocumentoAsync(int IdArea, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral);
		AreaDocumento GuardaAreaDocumento(int IdAreaDocumento, int IdArea, int IdCatalogoTipoDocumento, bool Activo, int IdUsuarioAuditoria);
		Task<AreaDocumento> GuardaAreaDocumentoAsync(int IdAreaDocumento, int IdArea, int IdCatalogoTipoDocumento, bool Activo, int IdUsuarioAuditoria);
		Mensaje EliminarAreaDocumento(int IdAreaDocumento, int IdUsuarioAuditoria);
		Task<Mensaje> EliminarAreaDocumentoAsync(int IdAreaDocumento, int IdUsuarioAuditoria);
		AreaDocumento ObtenerAreaDocumento(int IdAreaDocumento, int IdUsuarioAuditoria);
		Task<AreaDocumento> ObtenerAreaDocumentoAsync(int IdAreaDocumento, int IdUsuarioAuditoria);
	}
}
