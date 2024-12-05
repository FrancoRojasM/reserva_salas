		
		using Utilitarios;
		using System;
		using General;
		using System.Threading.Tasks;
		namespace MatrixService

			{
    public class SvcRegionSql : ISvcRegion
    {
		private GeneralLogic.RegionLogic region;
        public SvcRegionSql()
        {
            region = new GeneralLogic.RegionLogic();
        }
		 public async Task<ListaRegion> ListarRegionAsync( int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {
            return await region.ListarRegionAsync(DatosGlobales.GestorSqlServer, IdUsuarioAuditoria,CampoOrdenado, TipoOrdenacion,NumeroPagina,DimensionPagina,BusquedaGeneral);
        }
		public async Task<Region> GuardaRegionAsync( int IdRegion,string NombreRegion,int IdUsuarioAuditoria)
        {
            return await region.GuardaRegionAsync(DatosGlobales.GestorSqlServer,  IdRegion, NombreRegion,IdUsuarioAuditoria);
        }
		 public async Task<Mensaje> EliminarRegionAsync( int IdRegion, int IdUsuarioAuditoria)
        {           
           return await region.EliminarRegionAsync(DatosGlobales.GestorSqlServer,IdRegion, IdUsuarioAuditoria);         
        }
		public async Task<Region> ObtenerRegionAsync( int IdRegion,int IdUsuarioAuditoria)
        {      
            return await region.ObtenerRegionAsync(DatosGlobales.GestorSqlServer, IdRegion,IdUsuarioAuditoria);
        }   
		 public async Task<ListaRegion> ListarComboRegion(int IdUsuarioAuditoria)
        {
            return await region.ListarComboRegion(DatosGlobales.GestorSqlServer, IdUsuarioAuditoria);
        }
    }
}
		