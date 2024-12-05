using Utilitarios;
using General;
namespace MatrixService
{
	public interface ISvcPeriodo
	{
		ListaPeriodo ListarPeriodo( int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral);
		Periodo GuardaPeriodo( int IdPeriodo, string NombrePeriodo, string DecenioNombrePeriodo, string Decenio, bool Actual, int IdUsuarioAuditoria);
		Mensaje EliminarPeriodo( int IdPeriodo, int IdUsuarioAuditoria);
		General.Periodo ObtenerPeriodo( int IdPeriodo, int IdUsuarioAuditoria);
		General.ListaPeriodo ListarComboPeriodo( int IdUsuarioAuditoria);
		ListaPeriodo ListarComboPeriodoProcesoPorResponsable(int IdEmpleadoPerfil, int IdUsuarioAuditoria);
		ListaPeriodo ListarComboPeriodoProcesoEtapaPorResponsable(int IdEmpleadoPerfil, int IdUsuarioAuditoria); 
		
	}
}
