
using General;
using Microsoft.Extensions.Logging;
using Seguridad;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utilitarios;
namespace MatrixService
{
    public class SvcUsuarioSql : ISvcUsuario
    {
        private SeguridadLogic.UsuarioLogic usuario;
        public SvcUsuarioSql()
        {
            usuario = new SeguridadLogic.UsuarioLogic();
        }
        public ListaUsuario ListarUsuario(int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {

            return usuario.ListarUsuario(DatosGlobales.GestorSqlServer, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
        }
		
		  public async Task<ListaUsuario> ListarUsuarioPorAutoComplete(int IdUsuarioAuditoria,string NombreUsuario )
		{
			return await usuario.ListarUsuarioPorAutoComplete(DatosGlobales.GestorSqlServer, IdUsuarioAuditoria,NombreUsuario);
		}
		public async Task<ListaUsuario> ListarUsuarioAsync(int Bloqueado, int EsInstitucion, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral, bool forceRefresh = false)
        {
            return await usuario.ListarUsuarioAsync(DatosGlobales.GestorSqlServer, Bloqueado, EsInstitucion, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
        }
        public Usuario GuardarFotoUsuario(int IdUsuario, string RutaArchivoFoto, int IdUsuarioAuditoria)
        {

            return usuario.GuardarFotoUsuario(DatosGlobales.GestorOracle, IdUsuario, RutaArchivoFoto, IdUsuarioAuditoria);
        }
        public async Task<Usuario> GuardarFotoUsuarioAsync(int IdUsuario, string RutaArchivoFoto, int IdUsuarioAuditoria)
        {
            return await usuario.GuardarFotoUsuarioAsync(DatosGlobales.GestorSqlServer, IdUsuario, RutaArchivoFoto, IdUsuarioAuditoria);
        }
        public Usuario GuardaUsuario(int IdUsuario, int IdPersona, int IdCatalogoTipoUsuario, string Logueo, string Clave, bool Bloqueado, string Email, string RutaArchivoFoto, int IdUsuarioAuditoria)
        {

            return usuario.GuardaUsuario(DatosGlobales.GestorSqlServer, IdUsuario, IdPersona, IdCatalogoTipoUsuario, Logueo, Clave, Bloqueado, Email, RutaArchivoFoto, IdUsuarioAuditoria);
        }
        public async Task<Usuario> GuardaUsuarioAsync(int IdUsuario, int IdPersona, int IdCatalogoTipoUsuario, string Logueo, string Clave, bool Bloqueado, bool EsInstitucion, string Email, string RutaArchivoFoto, int IdUsuarioAuditoria)
        {
            return await usuario.GuardaUsuarioAsync(DatosGlobales.GestorSqlServer, IdUsuario, IdPersona, IdCatalogoTipoUsuario, Logueo, Clave, Bloqueado, EsInstitucion, Email, RutaArchivoFoto, IdUsuarioAuditoria);
        }
        public Mensaje EliminarUsuario(int IdUsuario, int IdUsuarioAuditoria)
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
        public Usuario ObtenerUsuario(int IdUsuario)
        {
            return usuario.ObtenerUsuario(DatosGlobales.GestorSqlServer, IdUsuario);
        }
        public async Task<Usuario> ObtenerUsuarioAsync(int IdUsuario)
        {
            return await usuario.ObtenerUsuarioAsync(DatosGlobales.GestorSqlServer, IdUsuario);
        }
        public Usuario AutenticarUsuario(string Logue, string Clave)
        {
            return usuario.AutenticarUsuario(DatosGlobales.GestorSqlServer, Logue, Clave);
        }
        public async Task<Usuario> AutenticarUsuarioAsync(string Logueo, string Clave, string Ip)
        {
            return await usuario.AutenticarUsuarioAsync(DatosGlobales.GestorSqlServer, Logueo, Clave, Ip);
        }
        public async Task<Usuario> ObtenerDatosUsuarioAutenticadoLdap(string Logueo, string Ip)
        {
            return await usuario.ObtenerDatosUsuarioAutenticadoLdap(DatosGlobales.GestorSqlServer, Logueo, Ip);
        }
		public Usuario ObtenerDatosUsuarioAutenticadoLdapNoAsincrono(string Logueo)
		{
			return usuario.ObtenerDatosUsuarioAutenticadoLdapNoAsincrono(DatosGlobales.GestorSqlServer, Logueo);
		}
		public Usuario GuardaCambioClave(int IdUsuario, string Clave, string ClaveAnterior, int IdUsuarioAuditoria)
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

        public async Task<Usuario> GuardarNuevaCuentaCasilla(int IdCatalogoTipoDocumentoPersonal, string NumeroDocumento, string Nombres, string ApellidoPaterno, string ApellidoMaterno, int Pais, string Email, string NombreUsuario, string FechaNacimiento, int Sexo, string LugarNacimiento, string NumeroCelular, string Password,int IdVerificacion, int IdUsuarioAuditoria)
        {
            return await usuario.GuardarNuevaCuentaCasilla(DatosGlobales.GestorSqlServer, IdCatalogoTipoDocumentoPersonal,
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
                                NumeroCelular,
                                Password,
                                IdVerificacion,
                                IdUsuarioAuditoria);
        }

        public List<Sucursal> ObtenerSucursales()
        {
            return usuario.ObtenerSucursales(DatosGlobales.GestorSqlServer);
        }
    }
}
