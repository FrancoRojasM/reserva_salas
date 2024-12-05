using General;
using System.ServiceModel;
using System.Threading.Tasks;
using Utilitarios;
namespace MatrixService
{

    public interface ISvcEmpresa
    {
        ListaEmpresa ListarEmpresaPrincipal(int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral);
        ListaEmpresa ListarEmpresa(int IdUsuarioAuditoria, int IdEmpresaPadre, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral);
        Empresa GuardaEmpresaPrincipal(int IdEmpresa, int IdEmpresaPadre, int IdPersona, string NombreEmpresa, bool Activo, int IdUsuarioAuditoria);
        Empresa GuardaEmpresa(int IdEmpresa, int IdEmpresaPadre, int IdPersona, string NombreEmpresa, bool Activo, int IdUsuarioAuditoria);
        Mensaje EliminarEmpresa(int IdEmpresa, int IdUsuarioAuditoria);
        Empresa ObtenerEmpresa(int IdEmpresa);
        ListaEmpresa ListarComboEmpresa();
        ListaEmpresa ListarComboEmpresaPadre(); 
        //METODOS ASINCRONOS
        Task<ListaEmpresa> ListarEmpresaPrincipalAsync(int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral);
        Task<ListaEmpresa> ListarEmpresaAsync(int IdUsuarioAuditoria, int IdEmpresaPadre, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral);
        Task<Empresa> GuardaEmpresaPrincipalAsync(int IdEmpresa, int IdEmpresaPadre, int IdPersona, string NombreEmpresa, bool Activo, int IdUsuarioAuditoria);
        Task<Empresa> GuardaEmpresaAsync(int IdEmpresa, int IdEmpresaPadre, int IdPersona, string NombreEmpresa, bool Activo, int IdUsuarioAuditoria);
        Task<Mensaje> EliminarEmpresaAsync(int IdEmpresa, int IdUsuarioAuditoria);
        Task<Empresa> ObtenerEmpresaAsync(int IdEmpresa);
        Task<ListaEmpresa> ListarComboEmpresaAsync();
        Task<ListaEmpresa> ListarComboEmpresaPadreAsync();
    }
}

