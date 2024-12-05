		using Utilitarios;
		using System;
		using System.Threading.Tasks;
		using General;
namespace MatrixService 
{						public interface  ISvcRegion
    {			 
		 Task<ListaRegion> ListarRegionAsync(  int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral);
		 Task<Region> GuardaRegionAsync( int IdRegion,string NombreRegion,int IdUsuarioAuditoria);		
		 Task<Mensaje> EliminarRegionAsync( int IdRegion, int IdUsuarioAuditoria);   
		 Task<Region> ObtenerRegionAsync( int IdRegion,int IdUsuarioAuditoria); 
		 Task<ListaRegion> ListarComboRegion(int IdUsuarioAuditoria);
    }
}
		