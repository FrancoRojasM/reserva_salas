
using RecursoHumano;
using System.Threading.Tasks;
using Utilitarios;
namespace MatrixService

{
    public class SvcEmpleadoPerfilSql : ISvcEmpleadoPerfil
    {
        private PersonalLogic.EmpleadoPerfilLogic empleadoperfil;
        public SvcEmpleadoPerfilSql()
        {
            empleadoperfil = new PersonalLogic.EmpleadoPerfilLogic();
        }
        public async Task<ListaEmpleadoPerfil> ListarEmpleadoPerfilAsync(int IdEmpleado, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {
            return await empleadoperfil.ListarEmpleadoPerfilAsync(DatosGlobales.GestorSqlServer, IdEmpleado, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
        }
        public async Task<EmpleadoPerfil> GuardaEmpleadoPerfilAsync(int IdEmpleadoPerfil, int IdEmpleado, int IdEmpresaSede, int IdArea, int IdCargo, bool Activo, bool DestinoTodos, int IdUsuarioAuditoria)
        {
            return await empleadoperfil.GuardaEmpleadoPerfilAsync(DatosGlobales.GestorSqlServer, IdEmpleadoPerfil, IdEmpleado, IdEmpresaSede, IdArea, IdCargo, Activo, DestinoTodos, IdUsuarioAuditoria);
        }
        public async Task<Mensaje> EliminarEmpleadoPerfilAsync(int IdEmpleadoPerfil, int IdUsuarioAuditoria)
        {
            return await empleadoperfil.EliminarEmpleadoPerfilAsync(DatosGlobales.GestorSqlServer, IdEmpleadoPerfil, IdUsuarioAuditoria);
        }
        public async Task<EmpleadoPerfil> ObtenerEmpleadoPerfilAsync(int IdEmpleadoPerfil, int IdUsuarioAuditoria)
        {
            return await empleadoperfil.ObtenerEmpleadoPerfilAsync(DatosGlobales.GestorSqlServer, IdEmpleadoPerfil, IdUsuarioAuditoria);
        }
        public ListaEmpleadoPerfil ListarComboEmpleadoPerfil(int IdPersona)
        {
            return  empleadoperfil.ListarComboEmpleadoPerfil(DatosGlobales.GestorSqlServer, IdPersona);
        }
        public ListaEmpleadoPerfil ListarEmpleadoPerfilPorAutoComplete(string NombreEmpleadoPerfil)
        {
            return  empleadoperfil.ListarEmpleadoPerfilPorAutoComplete(DatosGlobales.GestorSqlServer, NombreEmpleadoPerfil); 
        }
        public ListaEmpleadoPerfil ListarEmpleadoPerfilPorAutoCompletePorSede(string NombreEmpleadoPerfil, int IdEmpresaSede)
        {

            return  empleadoperfil.ListarEmpleadoPerfilPorAutoCompletePorSede(DatosGlobales.GestorSqlServer, NombreEmpleadoPerfil, IdEmpresaSede);
        }

        //METODOS ASYNCRONOS
        public async Task<ListaEmpleadoPerfil> ListarComboEmpleadoPerfilAsync(int IdPersona)
        {
            return await empleadoperfil.ListarComboEmpleadoPerfilAsync(DatosGlobales.GestorSqlServer, IdPersona);
        }
        public async Task<ListaEmpleadoPerfil> ListarEmpleadoPerfilPorAutoCompleteAsync(string NombreEmpleadoPerfil)
        {
            return await empleadoperfil.ListarEmpleadoPerfilPorAutoCompleteAsync(DatosGlobales.GestorSqlServer, NombreEmpleadoPerfil);
        }        
            public async Task<ListaEmpleadoPerfil> ListarRolesPorEmpleado(int IdUsuarioAuditoria, int IdEmpleado, int IdEmpleadoPerfilActual)
        {
            return await empleadoperfil.ListarRolesPorEmpleado(DatosGlobales.GestorSqlServer, IdUsuarioAuditoria, IdEmpleado, IdEmpleadoPerfilActual);
        }
        public async Task<ListaEmpleadoPerfil> ListarEmpleadoPerfilContactosAsync(int IdEmpleado, int IdUsuarioAuditoria,string BusquedaGeneral)
        {
            return await empleadoperfil.ListarEmpleadoPerfilContactosAsync(DatosGlobales.GestorSqlServer, IdEmpleado, IdUsuarioAuditoria,BusquedaGeneral);
        }       
        public async Task<Mensaje> ActualizarEstadoEmpleadoPerfil(int IdEmpleadoPerfil, bool Activo, int IdUsuarioAuditoria)
        {
            return await empleadoperfil.ActualizarEstadoEmpleadoPerfil(DatosGlobales.GestorSqlServer, IdEmpleadoPerfil, Activo, IdUsuarioAuditoria);
        }       
        public async Task<ListaEmpleadoPerfil> ListarEmpleadoPerfilPorAutoCompletePorSedeAsync(string NombreEmpleadoPerfil, int IdEmpresaSede)
        {
            return await empleadoperfil.ListarEmpleadoPerfilPorAutoCompletePorSedeAsync(DatosGlobales.GestorSqlServer, NombreEmpleadoPerfil, IdEmpresaSede);
        }
    }
}
		