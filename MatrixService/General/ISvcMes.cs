using Utilitarios;
using General;
namespace MatrixService
{
	public interface ISvcMes
	{
		ListaMes ListarMes( int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral);
		Mes GuardaMes( int IdMes, string NombreMes, string NominacionAbreviada, int IdUsuarioAuditoria);
		Mensaje EliminarMes( int IdMes, int IdUsuarioAuditoria);
		General.Mes ObtenerMes( int IdMes, int IdUsuarioAuditoria);
		General.ListaMes ListarComboMes( int IdUsuarioAuditoria);

	}
}
