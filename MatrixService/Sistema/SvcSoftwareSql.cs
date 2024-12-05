
using Utilitarios;
using Sistema;
using System.Threading.Tasks;
namespace MatrixService

{
	public class SvcSoftwareSql : ISvcSoftware
	{
		private SistemaLogic.SoftwareLogic software;
		public SvcSoftwareSql()
		{
			software = new SistemaLogic.SoftwareLogic();
		}
		public ListaSoftware ListarSoftware(int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
		{
			return software.ListarSoftware(DatosGlobales.GestorSqlServer, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
		}
		public async Task<ListaSoftware> ListarSoftwareAsync(int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
		{
			return await software.ListarSoftwareAsync(DatosGlobales.GestorSqlServer, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
		}
		public Software GuardaSoftware(int IdSoftware, string NombreLargoSoftware, string NombreCortoSoftware, string NumeroVersionSoftware, string RutaImagenSoftware, string RutaImagenLogoSoftware, string NombreLargoEmpresa, string NombreCortoEmpresa, int IdUsuarioAuditoria)
		{
			return software.GuardaSoftware(DatosGlobales.GestorSqlServer, IdSoftware, NombreLargoSoftware, NombreCortoSoftware, NumeroVersionSoftware, RutaImagenSoftware, RutaImagenLogoSoftware, NombreLargoEmpresa, NombreCortoEmpresa, IdUsuarioAuditoria);
		}
		public async Task<Software> GuardaSoftwareAsync(int IdSoftware, string NombreLargoSoftware, string NombreCortoSoftware, string NumeroVersionSoftware, string RutaImagenSoftware, string RutaImagenLogoSoftware, string NombreLargoEmpresa, string NombreCortoEmpresa, int IdUsuarioAuditoria)
		{
			return await software.GuardaSoftwareAsync(DatosGlobales.GestorSqlServer, IdSoftware, NombreLargoSoftware, NombreCortoSoftware, NumeroVersionSoftware, RutaImagenSoftware, RutaImagenLogoSoftware, NombreLargoEmpresa, NombreCortoEmpresa, IdUsuarioAuditoria);
		}
		public Mensaje EliminarSoftware(int IdSoftware, int IdUsuarioAuditoria)
		{
			Mensaje mensaje = new Mensaje();
			mensaje = software.EliminarSoftware(DatosGlobales.GestorSqlServer, IdSoftware, IdUsuarioAuditoria);
			return mensaje;
		}
		public async Task<Mensaje> EliminarSoftwareAsync(int IdSoftware, int IdUsuarioAuditoria)
		{
			Mensaje mensaje = new Mensaje();
			mensaje = await software.EliminarSoftwareAsync(DatosGlobales.GestorSqlServer, IdSoftware, IdUsuarioAuditoria);
			return mensaje;
		}
		public Software ObtenerSoftware(int IdSoftware, int IdUsuarioAuditoria)
		{
			return software.ObtenerSoftware(DatosGlobales.GestorSqlServer, IdSoftware, IdUsuarioAuditoria);
		}
		public async Task<Software> ObtenerSoftwareAsync(int IdSoftware, int IdUsuarioAuditoria)
		{
			return await software.ObtenerSoftwareAsync(DatosGlobales.GestorSqlServer, IdSoftware, IdUsuarioAuditoria);
		}

	}
}
