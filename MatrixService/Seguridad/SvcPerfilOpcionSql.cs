
using Seguridad;
using System.Threading.Tasks;
using Utilitarios;
		namespace MatrixService

			{
    public class SvcPerfilOpcionSql : ISvcPerfilOpcion
    {
        private SeguridadLogic.PerfilOpcionLogic perfilopcion;
        public SvcPerfilOpcionSql()
        {
            perfilopcion = new SeguridadLogic.PerfilOpcionLogic();
        }
        public ListaPerfilOpcion ListarMenuPorUsuario( int IdUsuario, int IdEmpleadoPerfil)
        {
                        
            return perfilopcion.ListarMenuPorUsuario(DatosGlobales.GestorSqlServer, IdUsuario, IdEmpleadoPerfil);
        }
        public async Task<ListaPerfilOpcion> ListarMenuPorUsuarioAsync(int IdUsuario, int IdEmpleadoPerfil)
        {

            return await perfilopcion.ListarMenuPorUsuarioAsync(DatosGlobales.GestorSqlServer, IdUsuario, IdEmpleadoPerfil);
        }
        public ListaPerfilOpcion ListarPerfilOpcion( int IdPerfil, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {
            
            return perfilopcion.ListarPerfilOpcion(DatosGlobales.GestorSqlServer, IdPerfil, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
        }
        public PerfilOpcion GuardaPerfilOpcion(int IdPerfilOpcion,int IdPerfil,int IdOpcionPadre,int IdOpcion,bool Ver,bool Nuevo,bool Editar,bool Guardar,bool Eliminar,bool Imprimir,bool Exportar,bool VistaPrevia,bool Consultar,int IdUsuarioAuditoria)
        {
            
            return perfilopcion.GuardaPerfilOpcion(DatosGlobales.GestorSqlServer,  IdPerfilOpcion, IdPerfil, IdOpcionPadre, IdOpcion, Ver, Nuevo, Editar, Guardar, Eliminar, Imprimir, Exportar, VistaPrevia, Consultar,IdUsuarioAuditoria);
        }
        public Mensaje EliminarPerfilOpcion( int IdPerfilOpcion, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();
            mensaje = perfilopcion.EliminarPerfilOpcion(DatosGlobales.GestorSqlServer,IdPerfilOpcion, IdUsuarioAuditoria);
            return mensaje;
        }
        public PerfilOpcion ObtenerPerfilOpcion( int IdPerfilOpcion)
        {                              
            return perfilopcion.ObtenerPerfilOpcion(DatosGlobales.GestorSqlServer, IdPerfilOpcion);
        }    
		   
    }
}
		