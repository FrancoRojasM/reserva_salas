using Utilitarios;
using System.Threading.Tasks;
using Sistema;
namespace MatrixService
{
	public interface ISvcSoftware
	{
		ListaSoftware ListarSoftware(int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral);
		Task<ListaSoftware> ListarSoftwareAsync(int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral);
		Software GuardaSoftware(int IdSoftware, string NombreLargoSoftware, string NombreCortoSoftware, string NumeroVersionSoftware, string RutaImagenSoftware, string RutaImagenLogoSoftware, string NombreLargoEmpresa, string NombreCortoEmpresa, int IdUsuarioAuditoria);
		Task<Software> GuardaSoftwareAsync(int IdSoftware, string NombreLargoSoftware, string NombreCortoSoftware, string NumeroVersionSoftware, string RutaImagenSoftware, string RutaImagenLogoSoftware, string NombreLargoEmpresa, string NombreCortoEmpresa, int IdUsuarioAuditoria);
		Mensaje EliminarSoftware(int IdSoftware, int IdUsuarioAuditoria);
		Task<Mensaje> EliminarSoftwareAsync(int IdSoftware, int IdUsuarioAuditoria);
		Software ObtenerSoftware(int IdSoftware, int IdUsuarioAuditoria);
		Task<Software> ObtenerSoftwareAsync(int IdSoftware, int IdUsuarioAuditoria);
	}
}
