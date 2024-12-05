
using Seguridad;
using System.Threading.Tasks;
using Utilitarios;
namespace MatrixService
{
    public class SvcUsuarioApi : ISvcUsuario
    {
        private SeguridadLogic.UsuarioLogic usuario;
        public SvcUsuarioApi()
        {
            usuario = new SeguridadLogic.UsuarioLogic();
       }
        public ListaUsuario ListarUsuario( int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {
            
            return usuario.ListarUsuario(DatosGlobales.GestorSqlServer, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
        }
        public async Task<ListaUsuario> ListarUsuarioAsync(int Bloqueado, int EsInstitucion,int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral, bool forceRefresh = false)
        {
            return await usuario.ListarUsuarioAsync(DatosGlobales.GestorApi,  Bloqueado,  EsInstitucion, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);           
        }
        public Usuario GuardarFotoUsuario( int IdUsuario, string RutaArchivoFoto, int IdUsuarioAuditoria)
        {
            
            return usuario.GuardarFotoUsuario(DatosGlobales.GestorOracle, IdUsuario, RutaArchivoFoto, IdUsuarioAuditoria);
        }
        public async Task<Usuario> GuardarFotoUsuarioAsync(int IdUsuario, string RutaArchivoFoto, int IdUsuarioAuditoria)
        {
            return await usuario.GuardarFotoUsuarioAsync(DatosGlobales.GestorSqlServer, IdUsuario, RutaArchivoFoto, IdUsuarioAuditoria);
        }
        public Usuario GuardaUsuario( int IdUsuario, int IdPersona, int IdCatalogoTipoUsuario, string Logueo, string Clave, bool Bloqueado, string Email, string RutaArchivoFoto, int IdUsuarioAuditoria)
        {
            
            return usuario.GuardaUsuario(DatosGlobales.GestorSqlServer, IdUsuario, IdPersona, IdCatalogoTipoUsuario, Logueo, Clave, Bloqueado, Email, RutaArchivoFoto, IdUsuarioAuditoria);
        }
        public async Task<Usuario> GuardaUsuarioAsync(int IdUsuario, int IdPersona, int IdCatalogoTipoUsuario, string Logueo, string Clave, bool Bloqueado,bool EsInstitucion, string Email, string RutaArchivoFoto, int IdUsuarioAuditoria)
        {
            return await usuario.GuardaUsuarioAsync(DatosGlobales.GestorApi, IdUsuario, IdPersona, IdCatalogoTipoUsuario, Logueo, Clave, Bloqueado, EsInstitucion, Email, RutaArchivoFoto, IdUsuarioAuditoria);
        }
        public Mensaje EliminarUsuario( int IdUsuario, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();
            mensaje = usuario.EliminarUsuario(DatosGlobales.GestorSqlServer, IdUsuario, IdUsuarioAuditoria);
            return mensaje;
        }
        public async Task<Mensaje> EliminarUsuarioAsync(int IdUsuario, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();
            mensaje = await usuario.EliminarUsuarioAsync(DatosGlobales.GestorSqlServer, IdUsuario, IdUsuarioAuditoria);
            return mensaje;
        }
        public Usuario ObtenerUsuario( int IdUsuario)
        {           
            return usuario.ObtenerUsuario(DatosGlobales.GestorSqlServer, IdUsuario);
        }
        public async Task<Usuario> ObtenerUsuarioAsync(int IdUsuario)
        {
            return await usuario.ObtenerUsuarioAsync(DatosGlobales.GestorApi, IdUsuario);            
        }
        public Usuario AutenticarUsuario( string Logue, string Clave)
        {           
            return usuario.AutenticarUsuario(DatosGlobales.GestorSqlServer, Logue, Clave);
        }
        public async Task<Usuario> AutenticarUsuarioAsync(string Logueo, string Clave)
        {
            return await usuario.AutenticarUsuarioAsync(DatosGlobales.GestorApi, Logueo, Clave);
        }
        public Usuario GuardaCambioClave( int IdUsuario, string Clave, string ClaveAnterior, int IdUsuarioAuditoria)
        {
            return usuario.GuardaCambioClave(DatosGlobales.GestorSqlServer, IdUsuario, Clave, ClaveAnterior, IdUsuarioAuditoria);
        }
        public async Task<Usuario> GuardaCambioClaveAsync(int IdUsuario, string Clave, string ClaveAnterior, int IdUsuarioAuditoria)
        {
            return await usuario.GuardaCambioClaveAsync(DatosGlobales.GestorSqlServer, IdUsuario, Clave, ClaveAnterior, IdUsuarioAuditoria);
        }
        public async Task<Usuario> BuscarUsuario(string NombreUsuario)
        {
            return await usuario.BuscarUsuario(DatosGlobales.GestorSqlServer, NombreUsuario);
        }
        public async Task<Usuario> BuscarEmail(string Email)
        {
               return await usuario.BuscarEmail(DatosGlobales.GestorSqlServer, Email);
        }
        public async Task<Usuario> GuardarNuevaCuentaMesaPartesVirtual(int IdCatalogoTipoDocumentoPersonal, string NumeroDocumento, string Nombres, string ApellidoPaterno, string ApellidoMaterno, int Pais, string Email, string NombreUsuario, string FechaNacimiento, int Sexo, string LugarNacimiento, string Clave, string Password, int IdUsuarioAuditoria)
        {
            return await usuario.GuardarNuevaCuentaMesaPartesVirtual(DatosGlobales.GestorSqlServer, IdCatalogoTipoDocumentoPersonal,
                                NumeroDocumento,
                                Nombres,
                                ApellidoPaterno,
                                ApellidoMaterno,
                                Pais,
                                Email,
                                NombreUsuario,
                                FechaNacimiento,
                                Sexo,
                                LugarNacimiento,
                                Clave,
                                Password,
                                IdUsuarioAuditoria);
        }
    }
}
		