
using Seguridad;
using System.Threading.Tasks;
using Utilitarios;
namespace MatrixService
{
    
    public interface ISvcPerfilOpcion
    {
        
        ListaPerfilOpcion ListarMenuPorUsuario( int IdUsuario, int IdEmpleadoPerfil);
        Task<ListaPerfilOpcion> ListarMenuPorUsuarioAsync(int IdUsuario, int IdEmpleadoPerfil);

        ListaPerfilOpcion ListarPerfilOpcion( int IdPerfil, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral);
        
        PerfilOpcion GuardaPerfilOpcion( int IdPerfilOpcion, int IdPerfil, int IdOpcionPadre, int IdOpcion, bool Ver, bool Nuevo, bool Editar, bool Guardar, bool Eliminar, bool Imprimir, bool Exportar, bool VistaPrevia, bool Consultar, int IdUsuarioAuditoria);
        
        Mensaje EliminarPerfilOpcion( int IdPerfilOpcion, int IdUsuarioAuditoria);
        
        PerfilOpcion ObtenerPerfilOpcion( int IdPerfilOpcion);
    }
}

		