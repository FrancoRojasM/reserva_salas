		using Utilitarios;
		using System;
		using System.Threading.Tasks;
		using General;
namespace MatrixService 
{						public interface  ISvcGobiernoRegional
    {			 
		 Task<ListaGobiernoRegional> ListarGobiernoRegionalAsync(  int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral);
		 Task<GobiernoRegional> GuardaGobiernoRegionalAsync( int IdGobiernoRegional,string NombreGobiernoRegional,int IdRegion,int IdUsuarioAuditoria);		
		 Task<Mensaje> EliminarGobiernoRegionalAsync( int IdGobiernoRegional, int IdUsuarioAuditoria);   
		 Task<GobiernoRegional> ObtenerGobiernoRegionalAsync( int IdGobiernoRegional,int IdUsuarioAuditoria); 
		 Task<ListaGobiernoRegional> ListarComboGobiernoRegional(int IdUsuarioAuditoria);
    }
}
		