		using System.ServiceModel;		
		using Utilitarios;
namespace MatrixService
{

	public interface ISvcPerfil
	{
		Seguridad.ListaPerfil ListarPerfil( int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral);

		Seguridad.Perfil GuardaPerfil(  int IdPerfil, string NombrePerfil, string DetallePerfil, bool Activo, int IdUsuarioAuditoria);

		Mensaje EliminarPerfil( int IdPerfil, int IdUsuarioAuditoria);

		Seguridad.Perfil ObtenerPerfil( int IdPerfil);


		Seguridad.ListaPerfil ListarComboPerfil();
	}
}

		