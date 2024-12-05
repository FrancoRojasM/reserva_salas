		
		using Utilitarios;
namespace MatrixService

{
    public class SvcPerfilSql : ISvcPerfil
    {
        private SeguridadLogic.PerfilLogic perfil;
        public SvcPerfilSql()
        {
            perfil = new SeguridadLogic.PerfilLogic();
        }
        public Seguridad.ListaPerfil ListarPerfil( int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {

            return perfil.ListarPerfil(DatosGlobales.GestorSqlServer, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
        }

        public Seguridad.Perfil GuardaPerfil( int IdPerfil, string NombrePerfil, string DetallePerfil, bool Activo, int IdUsuarioAuditoria)
        {

            return perfil.GuardaPerfil(DatosGlobales.GestorSqlServer, IdPerfil, NombrePerfil, DetallePerfil, Activo, IdUsuarioAuditoria);
        }
        public Mensaje EliminarPerfil( int IdPerfil, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();

            mensaje = perfil.EliminarPerfil(DatosGlobales.GestorSqlServer, IdPerfil, IdUsuarioAuditoria);
            return mensaje;
        }
        public Seguridad.Perfil ObtenerPerfil( int IdPerfil)
        {
            return perfil.ObtenerPerfil(DatosGlobales.GestorSqlServer, IdPerfil);
        }

        public Seguridad.ListaPerfil ListarComboPerfil()
        {
            return perfil.ListarComboPerfil(DatosGlobales.GestorSqlServer);
        }

    }
}	