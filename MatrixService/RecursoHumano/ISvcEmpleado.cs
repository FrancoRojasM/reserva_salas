using RecursoHumano;
using System.ServiceModel;
using System.Threading.Tasks;
using Utilitarios;
namespace MatrixService
{
    public interface ISvcEmpleado
    {

        Task<ListaEmpleado> ListarEmpleadoAsync(int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral);
        Task<Empleado> GuardaEmpleadoAsync(int IdEmpleado, int IdEmpresaPadre, int IdPersona, bool Activo, int IdCatalogoEstadoEmpleado, int IdCatalogoTipoEmpleado, int IdUsuarioAuditoria);
        Task<Mensaje> EliminarEmpleadoAsync(int IdEmpleado, int IdUsuarioAuditoria);
        Task<Empleado> ObtenerEmpleadoAsync(int IdEmpleado, int IdUsuarioAuditoria);
        Task<ListaEmpleado> ListarDirectorio(int IdUsuarioAuditoria, int IdArea,int IdCargo, string NombreCompleto);        
        Task<ListaEmpleado> ListarEmpleadoCumpleanios(int IdUsuarioAuditoria);
        Task<ListaEmpleado> ListarComboEmpleado(int IdUsuarioAuditoria);
        Task<ListaEmpleado> ListarComboEmpleado2(int IdUsuarioAuditoria);
        Task<ListaEmpleado> ObtenerDocumentosAsync(string search);

    }
}

		