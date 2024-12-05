
using RecursoHumano;
using System.Threading.Tasks;
using Utilitarios;
		namespace MatrixService

			{
    public class SvcEmpleadoSql : ISvcEmpleado
    {
        private PersonalLogic.EmpleadoLogic empleado;
        public SvcEmpleadoSql()
        {
            empleado = new PersonalLogic.EmpleadoLogic();
        }

        public async Task<ListaEmpleado> ListarEmpleadoAsync(int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {
            return await empleado.ListarEmpleadoAsync(DatosGlobales.GestorSqlServer, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
        }
        public async Task<Empleado> GuardaEmpleadoAsync(int IdEmpleado, int IdEmpresaPadre, int IdPersona, bool Activo, int IdCatalogoEstadoEmpleado, int IdCatalogoTipoEmpleado, int IdUsuarioAuditoria)
        {
            return await empleado.GuardaEmpleadoAsync(DatosGlobales.GestorSqlServer, IdEmpleado, IdEmpresaPadre, IdPersona, Activo, IdCatalogoEstadoEmpleado, IdCatalogoTipoEmpleado, IdUsuarioAuditoria);
        }
        public async Task<Mensaje> EliminarEmpleadoAsync(int IdEmpleado, int IdUsuarioAuditoria)
        {
            return await empleado.EliminarEmpleadoAsync(DatosGlobales.GestorSqlServer, IdEmpleado, IdUsuarioAuditoria);
        }
        public async Task<Empleado> ObtenerEmpleadoAsync(int IdEmpleado, int IdUsuarioAuditoria)
        {
            return await empleado.ObtenerEmpleadoAsync(DatosGlobales.GestorSqlServer, IdEmpleado, IdUsuarioAuditoria);
        }
        public async Task<ListaEmpleado> ListarDirectorio(int IdUsuarioAuditoria, int IdArea, int IdCargo, string NombreCompleto)        
        {

            return await empleado.ListarDirectorio(DatosGlobales.GestorSqlServer, IdUsuarioAuditoria,IdArea,IdCargo,NombreCompleto);
        }
        public async Task<ListaEmpleado> ListarEmpleadoCumpleanios(int IdUsuarioAuditoria)
        {

            return await empleado.ListarEmpleadoCumpleanios(DatosGlobales.GestorSqlServer, IdUsuarioAuditoria);
        }
        public async Task<ListaEmpleado> ListarComboEmpleado(int IdUsuarioAuditoria)
        {
            return await empleado.ListarComboEmpleado(DatosGlobales.GestorSqlServer, IdUsuarioAuditoria);
        }
        public async Task<ListaEmpleado> ListarComboEmpleado2(int IdUsuarioAuditoria)
        {
            return await empleado.ListarComboEmpleado2(DatosGlobales.GestorSqlServer, IdUsuarioAuditoria);
        }
        public async Task<ListaEmpleado> ObtenerDocumentosAsync(string search)
        {
            return await empleado.ObtenerDocumentosAsync(DatosGlobales.GestorSqlServer, search);
        }

    }
}
		