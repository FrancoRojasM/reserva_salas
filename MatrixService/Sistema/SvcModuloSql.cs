
using Utilitarios;
namespace MatrixService

{
    public class SvcModuloSql : ISvcModulo
    {
        private SistemaLogic.ModuloLogic modulo;
        public SvcModuloSql()
        {
            modulo = new SistemaLogic.ModuloLogic();
        }
        public Sistema.ListaModulo ListarModulo( int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {
            
            return modulo.ListarModulo(DatosGlobales.GestorSqlServer, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
        }

        public Sistema.Modulo GuardaModulo( int IdModulo, string NombreModulo, string DetalleModulo, int OrdenModulo, int IdCatalogoTipoModulo, bool Activo, string RutaImagenModulo, int IdUsuarioAuditoria)
        {            
            return modulo.GuardaModulo(DatosGlobales.GestorSqlServer, IdModulo, NombreModulo, DetalleModulo, OrdenModulo, IdCatalogoTipoModulo, Activo, RutaImagenModulo, IdUsuarioAuditoria);
        }
        public Mensaje EliminarModulo( int IdModulo, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();
            mensaje = modulo.EliminarModulo(DatosGlobales.GestorSqlServer, IdModulo, IdUsuarioAuditoria);
            return mensaje;
        }
        public Sistema.Modulo ObtenerModulo( int IdModulo)
        {
           
            return modulo.ObtenerModulo(DatosGlobales.GestorSqlServer, IdModulo);
        }

        public Sistema.ListaModulo ListarComboModulo()
        {
            
            return modulo.ListarComboModulo(DatosGlobales.GestorSqlServer);
        }

    }
}
