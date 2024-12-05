using Utilitarios;
using System.Threading.Tasks;
using General;
namespace MatrixService
{
	public interface ISvcUnidadEjecutora
	{
		ListaUnidadEjecutora ListarUnidadEjecutora(int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral);
		Task<ListaUnidadEjecutora> ListarUnidadEjecutoraAsync(int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral);
		UnidadEjecutora GuardaUnidadEjecutora(int IdUnidadEjecutora, string Descripcion, int IdUsuarioAuditoria);
		Task<UnidadEjecutora> GuardaUnidadEjecutoraAsync(int IdUnidadEjecutora, string Descripcion, int IdUsuarioAuditoria);
		Mensaje EliminarUnidadEjecutora(int IdUnidadEjecutora, int IdUsuarioAuditoria);
		Task<Mensaje> EliminarUnidadEjecutoraAsync(int IdUnidadEjecutora, int IdUsuarioAuditoria);
		UnidadEjecutora ObtenerUnidadEjecutora(int IdUnidadEjecutora, int IdUsuarioAuditoria);
		Task<UnidadEjecutora> ObtenerUnidadEjecutoraAsync(int IdUnidadEjecutora, int IdUsuarioAuditoria);
	}
}
