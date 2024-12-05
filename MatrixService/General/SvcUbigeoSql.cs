		using General;
		using Utilitarios;
		namespace MatrixService

			{
    public class SvcUbigeoSql : ISvcUbigeo
    {
        private GeneralLogic.UbigeoLogic ubigeo;
        public SvcUbigeoSql()
        {
            ubigeo = new GeneralLogic.UbigeoLogic();
        }
        public ListaUbigeo ListarComboDepartamento()
        {
            
            return ubigeo.ListarComboDepartamento(DatosGlobales.GestorSqlServer);
        }
        public ListaUbigeo ListarComboProvincia( int CodigoDepartamento)
        {
            
            return ubigeo.ListarComboProvincia(DatosGlobales.GestorSqlServer, CodigoDepartamento);
        }
        public ListaUbigeo ListarComboDistrito( int CodigoDepartamento,int  CodigoProvincia)
        {
            
            return ubigeo.ListarComboDistrito(DatosGlobales.GestorSqlServer, CodigoDepartamento,CodigoProvincia);
        }
    }
}
		