using General;
using Seguridad;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utilitarios;
namespace MatrixService
{
    public interface ISvcUsuario
    {
        ListaUsuario ListarUsuario(int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral);
		
		Task<ListaUsuario> ListarUsuarioPorAutoComplete(int  IdUsuario,string NombreUsuario);
		Task<ListaUsuario> ListarUsuarioAsync(int Bloqueado, int EsInstitucion, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral, bool forceRefresh = false);
        Usuario GuardarFotoUsuario(int IdUsuario, string RutaArchivoFoto, int IdUsuarioAuditoria);
        Task<Usuario> GuardarFotoUsuarioAsync(int IdUsuario, string RutaArchivoFoto, int IdUsuarioAuditoria);
        Usuario GuardaUsuario(int IdUsuario, int IdPersona, int IdCatalogoTipoUsuario, string Logueo, string Clave, bool Bloqueado, string Email, string RutaArchivoFoto, int IdUsuarioAuditoria);
        Task<Usuario> GuardaUsuarioAsync(int IdUsuario, int IdPersona, int IdCatalogoTipoUsuario, string Logueo, string Clave, bool Bloqueado, bool EsInstitucion, string Email, string RutaArchivoFoto, int IdUsuarioAuditoria);
        Mensaje EliminarUsuario(int IdUsuario, int IdUsuarioAuditoria);
        Task<Mensaje> EliminarUsuarioAsync(int IdUsuario, int IdUsuarioAuditoria);
        Usuario ObtenerUsuario(int IdUsuario);
        Task<Usuario> ObtenerUsuarioAsync(int IdUsuario);
        Usuario AutenticarUsuario(string Logueo, string Clave);
        Task<Usuario> AutenticarUsuarioAsync(string Logue, string Clave, string Ip);
        Task<Usuario> ObtenerDatosUsuarioAutenticadoLdap(string Logue, string Ip);
		Usuario ObtenerDatosUsuarioAutenticadoLdapNoAsincrono(string Logue);

		Usuario GuardaCambioClave(int IdUsuario, string Clave, string ClaveAnterior, int IdUsuarioAuditoria);
        Task<Usuario> GuardaCambioClaveAsync(int IdUsuario, string Clave, string ClaveAnterior, int IdUsuarioAuditoria);

        Task<Usuario> BuscarUsuario(string NombreUsuario);
        Task<Usuario> BuscarEmail(string Email);
        Task<Usuario> GuardarNuevaCuentaCasilla(int IdCatalogoTipoDocumentoPersonal, string NumeroDocumento, string Nombres, string ApellidoPaterno, string ApellidoMaterno, int Pais, string Email, string NombreUsuario, string FechaNacimiento, int Sexo, string LugarNacimiento, string NumeroCelular, string Password,int IdVerificacion, int IdUsuarioAuditoria);
        List<Sucursal> ObtenerSucursales();
    }
}

