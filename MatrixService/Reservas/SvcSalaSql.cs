
using Utilitarios;
using System;
using Reservas;
using System.Threading.Tasks;
using System.Data;
namespace MatrixService

{
	public class SvcSalaSql : ISvcSala
	{
		private ReservasLogic.SalaLogic sala;
		public SvcSalaSql()
		{
			sala = new ReservasLogic.SalaLogic();
		}
		public async Task<ListaSala> ListarSalaAsync(int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
		{
			return await sala.ListarSalaAsync(DatosGlobales.GestorSqlServer, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
		}
		public async Task<Sala> GuardaSalaAsync(int IdSala, string Nombre, int Aforo, int IdCatalogoPiso, string Observaciones, int IdUsuarioAuditoria)
		{
			return await sala.GuardaSalaAsync(DatosGlobales.GestorSqlServer, IdSala, Nombre, Aforo, IdCatalogoPiso, Observaciones, IdUsuarioAuditoria);
		}
		public async Task<Mensaje> EliminarSalaAsync(int IdSala, int IdUsuarioAuditoria)
		{
			return await sala.EliminarSalaAsync(DatosGlobales.GestorSqlServer, IdSala, IdUsuarioAuditoria);
		}
		public async Task<Sala> ObtenerSalaAsync(int IdSala, int IdUsuarioAuditoria)
		{
			return await sala.ObtenerSalaAsync(DatosGlobales.GestorSqlServer, IdSala, IdUsuarioAuditoria);
		}
		public async Task<DataSet> DescargarSala(int IdUsuarioAuditoria)
		{
			return await sala.DescargarSala(DatosGlobales.GestorSqlServer, IdUsuarioAuditoria);
		}
	}
}
