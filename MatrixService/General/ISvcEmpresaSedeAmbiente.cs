		using System.ServiceModel;		
		using Utilitarios;
namespace MatrixService
{
    
    public interface ISvcEmpresaSedeAmbiente
    {

        
        General.ListaEmpresaSedeAmbiente ListarEmpresaSedeAmbiente( int IdEmpresaSede, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral);
        
        General.EmpresaSedeAmbiente GuardaEmpresaSedeAmbiente( int IdEmpresaSedeAmbiente, int IdEmpresaSede, string CodigoAmbiente, string NombreAmbiente, string DescripcionAmbiente, bool Activo, int IdUsuarioAuditoria);
        
        Mensaje EliminarEmpresaSedeAmbiente( int IdEmpresaSedeAmbiente, int IdUsuarioAuditoria);
        
        General.EmpresaSedeAmbiente ObtenerEmpresaSedeAmbiente( int IdEmpresaSedeAmbiente, int IdUsuarioAuditoria);

        
        General.ListaEmpresaSedeAmbiente ListarComboEmpresaSedeAmbiente( int IdUsuarioAuditoria);



    }

}

		