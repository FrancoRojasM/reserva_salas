		using Utilitarios;
		using System.Threading.Tasks;
		using Seguridad;
namespace MatrixService
{
	public interface ISvcUsuarioToken
	{
		ListaUsuarioToken ListarUsuarioToken(int IdUsuario, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral);
		Task<ListaUsuarioToken> ListarUsuarioTokenAsync(int IdUsuario, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral);
		UsuarioToken GuardaUsuarioToken(int IdUsuarioToken, int IdUsuario, string Issuer, string Audience, string HostDeUso, string Token, string FechaCreacion, string FechaVencimiento, bool Activo, int IdUsuarioAuditoria);
		Task<UsuarioToken> GuardaUsuarioTokenAsync(int IdUsuarioToken, int IdUsuario, string Issuer, string Audience, string HostDeUso, string Token, string FechaCreacion, string FechaVencimiento, bool Activo, int IdUsuarioAuditoria);
		Mensaje EliminarUsuarioToken(int IdUsuarioToken, int IdUsuarioAuditoria);
		Task<UsuarioToken> ActualizarFechaUltimoAccesoToken(string Token, int IdUsuarioAuditoria);
		Task<Mensaje> EliminarUsuarioTokenAsync(int IdUsuarioToken, int IdUsuarioAuditoria);
		UsuarioToken ObtenerUsuarioToken(int IdUsuarioToken, int IdUsuarioAuditoria);
		Task<UsuarioToken> ObtenerUsuarioTokenAsync(int IdUsuarioToken, int IdUsuarioAuditoria);
	}
}
		