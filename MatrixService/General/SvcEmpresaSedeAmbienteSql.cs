		
		using Utilitarios;
namespace MatrixService

{
    public class SvcEmpresaSedeAmbienteSql : ISvcEmpresaSedeAmbiente
    {
        private GeneralLogic.EmpresaSedeAmbienteLogic empresasedeambiente;
        public SvcEmpresaSedeAmbienteSql()
        {
            empresasedeambiente = new GeneralLogic.EmpresaSedeAmbienteLogic();
        }
        public General.ListaEmpresaSedeAmbiente ListarEmpresaSedeAmbiente( int IdEmpresaSede, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {

            return empresasedeambiente.ListarEmpresaSedeAmbiente(DatosGlobales.GestorSqlServer, IdEmpresaSede, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
        }

        public General.EmpresaSedeAmbiente GuardaEmpresaSedeAmbiente( int IdEmpresaSedeAmbiente, int IdEmpresaSede, string CodigoAmbiente, string NombreAmbiente, string DescripcionAmbiente, bool Activo, int IdUsuarioAuditoria)
        {

            return empresasedeambiente.GuardaEmpresaSedeAmbiente(DatosGlobales.GestorSqlServer, IdEmpresaSedeAmbiente, IdEmpresaSede, CodigoAmbiente, NombreAmbiente, DescripcionAmbiente, Activo, IdUsuarioAuditoria);
        }
        public Mensaje EliminarEmpresaSedeAmbiente( int IdEmpresaSedeAmbiente, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();
            mensaje = empresasedeambiente.EliminarEmpresaSedeAmbiente(DatosGlobales.GestorSqlServer, IdEmpresaSedeAmbiente, IdUsuarioAuditoria);
            return mensaje;
        }
        public General.EmpresaSedeAmbiente ObtenerEmpresaSedeAmbiente( int IdEmpresaSedeAmbiente, int IdUsuarioAuditoria)
        {

            return empresasedeambiente.ObtenerEmpresaSedeAmbiente(DatosGlobales.GestorSqlServer, IdEmpresaSedeAmbiente, IdUsuarioAuditoria);
        }

        public General.ListaEmpresaSedeAmbiente ListarComboEmpresaSedeAmbiente( int IdUsuarioAuditoria)
        {

            return empresasedeambiente.ListarComboEmpresaSedeAmbiente(DatosGlobales.GestorSqlServer, IdUsuarioAuditoria);
        }

    }
}
		