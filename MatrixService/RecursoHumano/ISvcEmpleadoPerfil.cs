using RecursoHumano;
using System.ServiceModel;
using System.Threading.Tasks;
using Utilitarios;
namespace MatrixService
{

	public interface ISvcEmpleadoPerfil
    {
        Task<ListaEmpleadoPerfil> ListarEmpleadoPerfilAsync(int IdEmpleado, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral);
        Task<EmpleadoPerfil> GuardaEmpleadoPerfilAsync(int IdEmpleadoPerfil, int IdEmpleado, int IdEmpresaSede, int IdArea, int IdCargo, bool Activo, bool DestinoTodos, int IdUsuarioAuditoria);
        Task<Mensaje> EliminarEmpleadoPerfilAsync(int IdEmpleadoPerfil, int IdUsuarioAuditoria);
        Task<EmpleadoPerfil> ObtenerEmpleadoPerfilAsync(int IdEmpleadoPerfil, int IdUsuarioAuditoria);
        ListaEmpleadoPerfil ListarComboEmpleadoPerfil(int IdPersona);
		ListaEmpleadoPerfil ListarEmpleadoPerfilPorAutoComplete(string NombreEmpleadoPerfil);
		ListaEmpleadoPerfil ListarEmpleadoPerfilPorAutoCompletePorSede(string NombreEmpleadoPerfil, int IdEmpresaSede);

        //METODOS ASYNCRONOS
        
        Task<ListaEmpleadoPerfil> ListarRolesPorEmpleado(int IdUsuarioAuditoria, int IdEmpleado, int IdEmpleadoPerfilActual);
      	Task<ListaEmpleadoPerfil> ListarComboEmpleadoPerfilAsync(int IdPersona);
		Task<ListaEmpleadoPerfil> ListarEmpleadoPerfilPorAutoCompleteAsync(string NombreEmpleadoPerfil);	
		Task<ListaEmpleadoPerfil> ListarEmpleadoPerfilContactosAsync(int IdEmpleado, int IdUsuarioAuditoria,string BusquedaGeneral);
        Task<Mensaje> ActualizarEstadoEmpleadoPerfil(int IdEmpleadoPerfil, bool Activo, int IdUsuarioAuditoria);
		Task<ListaEmpleadoPerfil> ListarEmpleadoPerfilPorAutoCompletePorSedeAsync(string NombreEmpleadoPerfil, int IdEmpresaSede);
        }
}

		