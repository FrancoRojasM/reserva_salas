
using Utilitarios;
using General;
namespace MatrixService

{
    public class SvcPeriodoSql : ISvcPeriodo
    {
        private GeneralLogic.PeriodoLogic periodo;
        public SvcPeriodoSql()
        {
            periodo = new GeneralLogic.PeriodoLogic();
        }
        public ListaPeriodo ListarPeriodo( int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {
            return periodo.ListarPeriodo(DatosGlobales.GestorSqlServer, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
        }
        public Periodo GuardaPeriodo( int IdPeriodo, string NombrePeriodo, string DecenioNombrePeriodo, string Decenio, bool Actual, int IdUsuarioAuditoria)
        {
            return periodo.GuardaPeriodo(DatosGlobales.GestorSqlServer, IdPeriodo, NombrePeriodo, DecenioNombrePeriodo, Decenio, Actual, IdUsuarioAuditoria);
        }
        public Mensaje EliminarPeriodo( int IdPeriodo, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();
            mensaje = periodo.EliminarPeriodo(DatosGlobales.GestorSqlServer, IdPeriodo, IdUsuarioAuditoria);
            return mensaje;
        }
        public Periodo ObtenerPeriodo( int IdPeriodo, int IdUsuarioAuditoria)
        {
            return periodo.ObtenerPeriodo(DatosGlobales.GestorSqlServer, IdPeriodo, IdUsuarioAuditoria);
        }

        public ListaPeriodo ListarComboPeriodo( int IdUsuarioAuditoria)
        {
            return periodo.ListarComboPeriodo(DatosGlobales.GestorSqlServer, IdUsuarioAuditoria);
        }
        
      
        public ListaPeriodo ListarComboPeriodoProcesoPorResponsable(int IdEmpleadoPerfil,int IdUsuarioAuditoria)
        {
            return periodo.ListarComboPeriodoProcesoPorResponsable(DatosGlobales.GestorSqlServer, IdEmpleadoPerfil,IdUsuarioAuditoria);
        }
        public ListaPeriodo ListarComboPeriodoProcesoEtapaPorResponsable(int IdEmpleadoPerfil, int IdUsuarioAuditoria)
        {
            return periodo.ListarComboPeriodoProcesoEtapaPorResponsable(DatosGlobales.GestorSqlServer, IdEmpleadoPerfil, IdUsuarioAuditoria);
        }
        
    }
}
