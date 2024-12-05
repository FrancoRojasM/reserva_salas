
using Utilitarios;
using General;
namespace MatrixService

{
    public class SvcMesSql : ISvcMes
    {
        private GeneralLogic.MesLogic mes;
        public SvcMesSql()
        {
            mes = new GeneralLogic.MesLogic();
        }
        public ListaMes ListarMes( int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {
            return mes.ListarMes(DatosGlobales.GestorSqlServer, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
        }
        public Mes GuardaMes( int IdMes, string NombreMes, string NominacionAbreviada, int IdUsuarioAuditoria)
        {
            return mes.GuardaMes(DatosGlobales.GestorSqlServer, IdMes, NombreMes, NominacionAbreviada, IdUsuarioAuditoria);
        }
        public Mensaje EliminarMes( int IdMes, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();
            mensaje = mes.EliminarMes(DatosGlobales.GestorSqlServer, IdMes, IdUsuarioAuditoria);
            return mensaje;
        }
        public Mes ObtenerMes( int IdMes, int IdUsuarioAuditoria)
        {
            return mes.ObtenerMes(DatosGlobales.GestorSqlServer, IdMes, IdUsuarioAuditoria);
        }

        public ListaMes ListarComboMes( int IdUsuarioAuditoria)
        {
            return mes.ListarComboMes(DatosGlobales.GestorSqlServer, IdUsuarioAuditoria);
        }

    }
}
