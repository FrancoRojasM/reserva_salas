
using Seguridad;
using System.Threading.Tasks;
using Utilitarios;
namespace MatrixService
{
    public class SvcUsuarioOracle : ISvcUsuario
    {
        private SeguridadLogic.UsuarioLogic usuario;
        public SvcUsuarioOracle()
        {
            usuario = new SeguridadLogic.UsuarioLogic();
       }
        public ListaUsuario ListarUsuario( int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {
            
            return usuario.ListarUsuario(DatosGlobales.GestorOracle, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
        }
        public async Task<ListaUsuario> ListarUsuarioAsync(int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral, bool forceRefresh = false)
        {
            return await usuario.ListarUsuarioAsync(DatosGlobales.GestorOracle, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);           
        }
        public Usuario GuardarFotoUsuario( int IdUsuario, string RutaArchivoFoto, int IdUsuarioAuditoria)
        {
            
            return usuario.GuardarFotoUsuario(DatosGlobales.GestorOracle, IdUsuario, RutaArchivoFoto, IdUsuarioAuditoria);
        }
        public async Task<Usuario> GuardarFotoUsuarioAsync(int IdUsuario, string RutaArchivoFoto, int IdUsuarioAuditoria)
        {
            return await usuario.GuardarFotoUsuarioAsync(DatosGlobales.GestorOracle, IdUsuario, RutaArchivoFoto, IdUsuarioAuditoria);
        }
        public Usuario GuardaUsuario( int IdUsuario, int IdPersona, int IdCatalogoTipoUsuario, string Logueo, string Clave, bool Bloqueado, string Email, string RutaArchivoFoto, int IdUsuarioAuditoria)
        {
            
            return usuario.GuardaUsuario(DatosGlobales.GestorOracle, IdUsuario, IdPersona, IdCatalogoTipoUsuario, Logueo, Clave, Bloqueado, Email, RutaArchivoFoto, IdUsuarioAuditoria);
        }
        public async Task<Usuario> GuardaUsuarioAsync(int IdUsuario, int IdPersona, int IdCatalogoTipoUsuario, string Logueo, string Clave, bool Bloqueado, string Email, string RutaArchivoFoto, int IdUsuarioAuditoria)
        {
            return await usuario.GuardaUsuarioAsync(DatosGlobales.GestorOracle, IdUsuario, IdPersona, IdCatalogoTipoUsuario, Logueo, Clave, Bloqueado, Email, RutaArchivoFoto, IdUsuarioAuditoria);
        }
        public Mensaje EliminarUsuario( int IdUsuario, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();
            mensaje = usuario.EliminarUsuario(DatosGlobales.GestorOracle, IdUsuario, IdUsuarioAuditoria);
            return mensaje;
        }
        public async Task<Mensaje> EliminarUsuarioAsync(int IdUsuario, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();
            mensaje = await usuario.EliminarUsuarioAsync(DatosGlobales.GestorOracle, IdUsuario, IdUsuarioAuditoria);
            return mensaje;
        }
        public Usuario ObtenerUsuario( int IdUsuario)
        {           
            return usuario.ObtenerUsuario(DatosGlobales.GestorOracle, IdUsuario);
        }
        public async Task<Usuario> ObtenerUsuarioAsync(int IdUsuario)
        {
            return await usuario.ObtenerUsuarioAsync(DatosGlobales.GestorOracle, IdUsuario);
        }
        public Usuario AutenticarUsuario( string Logue, string Clave)
        {           
            return usuario.AutenticarUsuario(DatosGlobales.GestorOracle, Logue, Clave);
        }
        public async Task<Usuario> AutenticarUsuarioAsync(string Logueo, string Clave)
        {
            return await usuario.AutenticarUsuarioAsync(DatosGlobales.GestorOracle, Logueo, Clave);
        }
        public Usuario GuardaCambioClave( int IdUsuario, string Clave, string ClaveAnterior, int IdUsuarioAuditoria)
        {
            return usuario.GuardaCambioClave(DatosGlobales.GestorOracle, IdUsuario, Clave, ClaveAnterior, IdUsuarioAuditoria);
        }
        public async Task<Usuario> GuardaCambioClaveAsync(int IdUsuario, string Clave, string ClaveAnterior, int IdUsuarioAuditoria)
        {
            return await usuario.GuardaCambioClaveAsync(DatosGlobales.GestorOracle, IdUsuario, Clave, ClaveAnterior, IdUsuarioAuditoria);
        }
    }
}
		