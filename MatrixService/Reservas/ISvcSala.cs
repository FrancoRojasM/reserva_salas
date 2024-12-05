using Utilitarios;
using System;
using System.Threading.Tasks;
using Reservas;
using System.Data;
namespace MatrixService
{
	public interface ISvcSala
	{
		Task<ListaSala> ListarSalaAsync(int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral);
		Task<Sala> GuardaSalaAsync(int IdSala, string Nombre, int Aforo, int IdCatalogoPiso, string Observaciones, int IdUsuarioAuditoria);
		Task<Mensaje> EliminarSalaAsync(int IdSala, int IdUsuarioAuditoria);
		Task<Sala> ObtenerSalaAsync(int IdSala, int IdUsuarioAuditoria);
		Task<DataSet> DescargarSala(int IdUsuarioAuditoria);
	}
}
