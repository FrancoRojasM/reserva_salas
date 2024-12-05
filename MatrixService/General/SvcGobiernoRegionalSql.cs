		
		using Utilitarios;
		using System;
		using General;
		using System.Threading.Tasks;
		namespace MatrixService

			{
    public class SvcGobiernoRegionalSql : ISvcGobiernoRegional
    {
		private GeneralLogic.GobiernoRegionalLogic gobiernoregional;
        public SvcGobiernoRegionalSql()
        {
            gobiernoregional = new GeneralLogic.GobiernoRegionalLogic();
        }
		 public async Task<ListaGobiernoRegional> ListarGobiernoRegionalAsync( int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {
            return await gobiernoregional.ListarGobiernoRegionalAsync(DatosGlobales.GestorSqlServer, IdUsuarioAuditoria,CampoOrdenado, TipoOrdenacion,NumeroPagina,DimensionPagina,BusquedaGeneral);
        }
		public async Task<GobiernoRegional> GuardaGobiernoRegionalAsync( int IdGobiernoRegional,string NombreGobiernoRegional,int IdRegion,int IdUsuarioAuditoria)
        {
            return await gobiernoregional.GuardaGobiernoRegionalAsync(DatosGlobales.GestorSqlServer,  IdGobiernoRegional, NombreGobiernoRegional, IdRegion,IdUsuarioAuditoria);
        }
		 public async Task<Mensaje> EliminarGobiernoRegionalAsync( int IdGobiernoRegional, int IdUsuarioAuditoria)
        {           
           return await gobiernoregional.EliminarGobiernoRegionalAsync(DatosGlobales.GestorSqlServer,IdGobiernoRegional, IdUsuarioAuditoria);         
        }
		public async Task<GobiernoRegional> ObtenerGobiernoRegionalAsync( int IdGobiernoRegional,int IdUsuarioAuditoria)
        {      
            return await gobiernoregional.ObtenerGobiernoRegionalAsync(DatosGlobales.GestorSqlServer, IdGobiernoRegional,IdUsuarioAuditoria);
        }   
		 public async Task<ListaGobiernoRegional> ListarComboGobiernoRegional(int IdUsuarioAuditoria)
        {
            return await gobiernoregional.ListarComboGobiernoRegional(DatosGlobales.GestorSqlServer, IdUsuarioAuditoria);
        }
    }
}
		