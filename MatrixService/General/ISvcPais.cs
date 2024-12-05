using Utilitarios;
using General;
using System.Threading.Tasks;

namespace MatrixService
{
	public interface ISvcPais
	{
		ListaPais ListarPais( int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral);
		Pais GuardaPais( int IdPais, string NombrePais, string Gentilicio, string Alfa3, string Alfa2, string Bandera, int IdUsuarioAuditoria);
		Mensaje EliminarPais( int IdPais, int IdUsuarioAuditoria);
		General.Pais ObtenerPais( int IdPais, int IdUsuarioAuditoria);
		General.ListaPais ListarComboPais( int IdUsuarioAuditoria);
		Task<General.ListaPais> ListarComboPaisAsync(int IdUsuarioAuditoria);
		
	}
}
