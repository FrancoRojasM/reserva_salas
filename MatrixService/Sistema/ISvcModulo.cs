using System.ServiceModel;
using Utilitarios;
namespace MatrixService
{

    public interface ISvcModulo
    {
        Sistema.ListaModulo ListarModulo( int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral);
        Sistema.Modulo GuardaModulo( int IdModulo, string NombreModulo, string DetalleModulo, int OrdenModulo, int IdCatalogoTipoModulo, bool Activo, string RutaImagenModulo, int IdUsuarioAuditoria);
        Mensaje EliminarModulo( int IdModulo, int IdUsuarioAuditoria);
        Sistema.Modulo ObtenerModulo( int IdModulo);
        Sistema.ListaModulo ListarComboModulo();
    }
}
