
using Utilitarios;
namespace MatrixService

{
    public class SvcOpcionFormularioSql : ISvcOpcionFormulario
    {
        private SistemaLogic.OpcionFormularioLogic opcionformulario;
        public SvcOpcionFormularioSql()
        {
            opcionformulario = new SistemaLogic.OpcionFormularioLogic();
        }
        public Sistema.ListaOpcionFormulario ListarOpcionFormulario( int IdOpcion, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {
            return opcionformulario.ListarOpcionFormulario(DatosGlobales.GestorSqlServer, IdOpcion, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
        }
        public Sistema.OpcionFormulario GuardaOpcionFormulario( int IdOpcionFormulario, int IdOpcion, string NombreFormulario, string RutaFormulario, string Controlador, string Accion, string Parametros, bool Ver, bool Nuevo, bool Editar, bool Guardar, bool Eliminar, bool Imprimir, bool Exportar, bool VistaPrevia, bool Consultar, bool Activo, int IdUsuarioAuditoria)
        {
            return opcionformulario.GuardaOpcionFormulario(DatosGlobales.GestorSqlServer, IdOpcionFormulario, IdOpcion, NombreFormulario, RutaFormulario, Controlador, Accion, Parametros, Ver, Nuevo, Editar, Guardar, Eliminar, Imprimir, Exportar, VistaPrevia, Consultar, Activo, IdUsuarioAuditoria);
        }
        public Mensaje EliminarOpcionFormulario( int IdOpcionFormulario, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();

            mensaje = opcionformulario.EliminarOpcionFormulario(DatosGlobales.GestorSqlServer, IdOpcionFormulario, IdUsuarioAuditoria);
            return mensaje;
        }
        public Sistema.OpcionFormulario ObtenerOpcionFormulario( int IdOpcion)
        {
            return opcionformulario.ObtenerOpcionFormulario(DatosGlobales.GestorSqlServer, IdOpcion);
        }

    }
}
