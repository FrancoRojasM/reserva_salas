using General;
using System.Threading.Tasks;
using Utilitarios;
namespace MatrixService
{    
    public interface ISvcEmpresaSede
    {        
        ListaEmpresaSede ListarEmpresaSede( int IdEmpresa, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral);
        EmpresaSede GuardaEmpresaSede( int IdEmpresaSede, int IdEmpresa, string DireccionSede, string NombreSede, bool Activo, int IdUsuarioAuditoria);        
        Mensaje EliminarEmpresaSede( int IdEmpresaSede, int IdUsuarioAuditoria);        
        EmpresaSede ObtenerEmpresaSede( int IdEmpresaSede, int IdUsuarioAuditoria);                
        ListaEmpresaSede ListarComboEmpresaSede( int IdUsuarioAuditoria , int IdEmpresaPadre);

        //METODOS ASINCRONOS
        Task<ListaEmpresaSede> ListarEmpresaSedeAsync(int IdEmpresa, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral);
        Task<EmpresaSede> GuardaEmpresaSedeAsync(int IdEmpresaSede, int IdEmpresa, string DireccionSede, string NombreSede, bool Activo, int IdUsuarioAuditoria);
        Task<Mensaje> EliminarEmpresaSedeAsync(int IdEmpresaSede, int IdUsuarioAuditoria); 
        Task<EmpresaSede> ObtenerEmpresaSedeAsync(int IdEmpresaSede, int IdUsuarioAuditoria);
        Task<ListaEmpresaSede> ListarComboEmpresaSedeAsync(int IdUsuarioAuditoria, int IdEmpresaPadre);
    }
}

