		using System.ServiceModel;
		using General;
		using Utilitarios;
		namespace MatrixService
							{
	   
    public interface  ISvcUbigeo
    {

        
        ListaUbigeo ListarComboDepartamento();

        
        ListaUbigeo ListarComboProvincia( int CodigoDepartamento);

        
        ListaUbigeo ListarComboDistrito( int CodigoDepartamento, int CodigoProvincia);
    }

}

		