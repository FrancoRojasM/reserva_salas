using System.ServiceModel;
using System.Threading.Tasks;
using Utilitarios;
namespace MatrixService
{

    public interface ISvcArea
    {

        General.ListaArea ListarArea(int IdEmpresa, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral);
        General.Area GuardaArea(int IdArea, int IdEmpresa, int IdAreaPadre, string NombreArea, string Abreviatura, string Sigla, bool Activo, int IdCatalogoTipoArea, int IdUsuarioAuditoria);
        Mensaje EliminarArea(int IdArea, int IdUsuarioAuditoria); 
        General.Area ObtenerArea(int IdArea);
        General.ListaArea ListarComboArea(int IdEmpresa);
        General.ListaArea ListarComboAreaPadre(int IdEmpresa);

        //METODOS ASINCRONOS
        Task<General.ListaArea> ListarAreaAsync(int IdEmpresa, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral);
        Task<General.Area> GuardaAreaAsync(int IdArea, int IdEmpresa, int IdAreaPadre, string NombreArea, string Abreviatura, string Sigla, bool Activo, int IdCatalogoTipoArea,bool VerRecepcion, int IdUsuarioAuditoria);
        Task<Mensaje> EliminarAreaAsync(int IdArea, int IdUsuarioAuditoria);
        Task<General.Area> ObtenerAreaAsync(int IdArea);
        Task<General.ListaArea> ListarComboAreaAsync(int IdEmpresa);
        Task<General.ListaArea> ListarComboArea3Async(int IdUsuarioAuditoria);
        Task<General.ListaArea> ListarComboAreaPadreAsync(int IdEmpresa);
    }

}

