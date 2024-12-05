
using Seguridad;
using System.Threading.Tasks;
using Utilitarios;
namespace MatrixService

{
    public class SvcUsuarioPerfilSql : ISvcUsuarioPerfil
    {
        private SeguridadLogic.UsuarioPerfilLogic usuarioperfil;
        public SvcUsuarioPerfilSql()
        {
            usuarioperfil = new SeguridadLogic.UsuarioPerfilLogic();
        }
        public ListaUsuarioPerfil ListarComboUsuarioPerfil( int IdUsuario)
        {
            
            return usuarioperfil.ListarComboUsuarioPerfil(DatosGlobales.GestorSqlServer, IdUsuario);
        }
        public ListaUsuarioPerfil ListarUsuarioPerfil( int IdUsuario, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {
            
            return usuarioperfil.ListarUsuarioPerfil(DatosGlobales.GestorSqlServer, IdUsuario, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
        }

        public UsuarioPerfil GuardaUsuarioPerfil( int IdUsuarioPerfil, int IdUsuario, int IdEmpleadoPerfil, int IdPerfil, bool Activo, int IdUsuarioAuditoria)
        {
            
            return usuarioperfil.GuardaUsuarioPerfil(DatosGlobales.GestorSqlServer, IdUsuarioPerfil, IdUsuario, IdEmpleadoPerfil, IdPerfil, Activo, IdUsuarioAuditoria);
        }
        public Mensaje EliminarUsuarioPerfil( int IdUsuarioPerfil, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();            
            mensaje = usuarioperfil.EliminarUsuarioPerfil(DatosGlobales.GestorSqlServer, IdUsuarioPerfil, IdUsuarioAuditoria);
            return mensaje;
        }
        public UsuarioPerfil ObtenerUsuarioPerfil( int IdUsuarioPerfil)
        {
            
            return usuarioperfil.ObtenerUsuarioPerfil(DatosGlobales.GestorSqlServer, IdUsuarioPerfil);
        }
        public UsuarioPerfil ObtenerUsuarioPorEmpleadoPerfil(int IdEmpleadoPerfil)
        {
            return usuarioperfil.ObtenerUsuarioPorEmpleadoPerfil(DatosGlobales.GestorSqlServer, IdEmpleadoPerfil);
        }
        public UsuarioPerfil ObtenerAreaCargoPorEmpleadoPerfil( int IdEmpleadoPerfil)
        {
            return usuarioperfil.ObtenerAreaCargoPorEmpleadoPerfil(DatosGlobales.GestorSqlServer, IdEmpleadoPerfil);
        }
        public async Task<UsuarioPerfil> ObtenerAreaCargoPorEmpleadoPerfilAsync(int IdEmpleadoPerfil)
        {
            return await usuarioperfil.ObtenerAreaCargoPorEmpleadoPerfilAsync(DatosGlobales.GestorSqlServer, IdEmpleadoPerfil);
        }

        public async Task<int> ValidarAccesoUsuarioPerfilAsync(int IdUsuario, int IdPerfil)
        {
            return await usuarioperfil.ValidarAccesoUsuarioPerfilAsync(DatosGlobales.GestorSqlServer, IdUsuario, IdPerfil);
        }
    }
}
		