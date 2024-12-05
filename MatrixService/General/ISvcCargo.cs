using General;
using System.ServiceModel;
using System.Threading.Tasks;
using Utilitarios;
namespace MatrixService
{
    
    public interface ISvcCargo
    {        
        ListaCargo ListarCargo( int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral);
        
        Cargo GuardaCargo( int IdCargo, string NombreCargo,int IdCatalogoTipoCargo, bool Activo, int IdUsuarioAuditoria);
        
        Mensaje EliminarCargo( int IdCargo, int IdUsuarioAuditoria);
        
        Cargo ObtenerCargo( int IdCargo);                
        ListaCargo ListarComboCargo();
        Task<ListaCargo> ListarComboCargoAsync();



    }

}

