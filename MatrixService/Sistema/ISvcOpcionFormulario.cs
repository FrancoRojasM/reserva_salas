using System.ServiceModel;
using Utilitarios;
namespace MatrixService
{
    
    public interface ISvcOpcionFormulario
    {

        
        Sistema.ListaOpcionFormulario ListarOpcionFormulario( int IdOpcion, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral);
        
        Sistema.OpcionFormulario GuardaOpcionFormulario( int IdOpcionFormulario, int IdOpcion, string NombreFormulario, string RutaFormulario, string Controlador, string Accion, string Parametros, bool Ver, bool Nuevo, bool Editar, bool Guardar, bool Eliminar, bool Imprimir, bool Exportar, bool VistaPrevia, bool Consultar, bool Activo, int IdUsuarioAuditoria);
        
        Mensaje EliminarOpcionFormulario( int IdOpcionFormulario, int IdUsuarioAuditoria);
        
        Sistema.OpcionFormulario ObtenerOpcionFormulario( int IdOpcion);


    }

}

