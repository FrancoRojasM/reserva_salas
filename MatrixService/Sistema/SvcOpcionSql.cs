
using Utilitarios;
namespace MatrixService

{
    public class SvcOpcionSql : ISvcOpcion
    {
        private SistemaLogic.OpcionLogic opcion;
        public SvcOpcionSql()
        {
            opcion = new SistemaLogic.OpcionLogic();
        }
        public Sistema.ListaOpcion ListarOpcion( int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {
            
            return opcion.ListarOpcion(DatosGlobales.GestorSqlServer, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
        }

        public Sistema.Opcion GuardaOpcion( int IdOpcion, int IdModulo, int IdOpcionPadre, string NombreOpcion, string DetalleOpcion, int IdCatalogoTipoOpcion, int OrdenOpcion, string RutaImagenOpcion, bool Activo, int IdUsuarioAuditoria)
        {          
            return opcion.GuardaOpcion(DatosGlobales.GestorSqlServer, IdOpcion, IdModulo, IdOpcionPadre, NombreOpcion, DetalleOpcion, IdCatalogoTipoOpcion, OrdenOpcion, RutaImagenOpcion, Activo, IdUsuarioAuditoria);
        }
        public Mensaje EliminarOpcion( int IdOpcion, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();
            mensaje = opcion.EliminarOpcion(DatosGlobales.GestorSqlServer, IdOpcion, IdUsuarioAuditoria);
            return mensaje;
        }
        public Sistema.Opcion ObtenerOpcion( int IdOpcion)
        {           
            return opcion.ObtenerOpcion(DatosGlobales.GestorSqlServer, IdOpcion);
        }

        public Sistema.ListaOpcion ListarComboOpcion()
        {
            
            return opcion.ListarComboOpcion(DatosGlobales.GestorSqlServer);
        }
        public Sistema.ListaOpcion ListarComboOpcionPadre( int IdModulo)
        {
            
            return opcion.ListarComboOpcionPadre(DatosGlobales.GestorSqlServer,  IdModulo);
        }
        

    }
}
