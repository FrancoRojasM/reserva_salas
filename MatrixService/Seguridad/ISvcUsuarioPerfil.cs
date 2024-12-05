using Seguridad;
using System.ServiceModel;
using System.Threading.Tasks;
using Utilitarios;
namespace MatrixService
{
    public interface ISvcUsuarioPerfil
    {
        ListaUsuarioPerfil ListarComboUsuarioPerfil( int IdUsuario);
        ListaUsuarioPerfil ListarUsuarioPerfil( int IdUsuario, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral);
        UsuarioPerfil GuardaUsuarioPerfil( int IdUsuarioPerfil, int IdUsuario, int IdEmpleadoPerfil, int IdPerfil, bool Activo, int IdUsuarioAuditoria);
        Mensaje EliminarUsuarioPerfil( int IdUsuarioPerfil, int IdUsuarioAuditoria);
        UsuarioPerfil ObtenerUsuarioPerfil( int IdUsuarioPerfil);
        UsuarioPerfil ObtenerAreaCargoPorEmpleadoPerfil( int IdEmpleadoPerfil);
        Task<UsuarioPerfil> ObtenerAreaCargoPorEmpleadoPerfilAsync(int IdEmpleadoPerfil);
        UsuarioPerfil ObtenerUsuarioPorEmpleadoPerfil(int IdEmpleadoPerfil);
        Task<int> ValidarAccesoUsuarioPerfilAsync(int IdUsuario, int IdPerfil);
    }
}

		