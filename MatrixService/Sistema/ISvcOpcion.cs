using System.ServiceModel;
using Utilitarios;
namespace MatrixService
{
    
    public interface ISvcOpcion
    {        
        Sistema.ListaOpcion ListarOpcion( int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral);
        
        Sistema.Opcion GuardaOpcion( int IdOpcion, int IdModulo, int IdOpcionPadre, string NombreOpcion, string DetalleOpcion, int IdCatalogoTipoOpcion, int OrdenOpcion, string RutaImagenOpcion, bool Activo, int IdUsuarioAuditoria);
        
        Mensaje EliminarOpcion( int IdOpcion, int IdUsuarioAuditoria);
        
        Sistema.Opcion ObtenerOpcion( int IdOpcion);

        
        Sistema.ListaOpcion ListarComboOpcion();
        
        Sistema.ListaOpcion ListarComboOpcionPadre( int IdModulo);


    }

}

