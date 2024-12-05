
using General;
using System.Threading.Tasks;
using Utilitarios;
namespace MatrixService

{
    public class SvcEmpresaSql : ISvcEmpresa
    {

        private GeneralLogic.EmpresaLogic empresa;
        public SvcEmpresaSql()
        {
            empresa = new GeneralLogic.EmpresaLogic();
        }
        
        public ListaEmpresa ListarEmpresaPrincipal( int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {

            return empresa.ListarEmpresaPrincipal(DatosGlobales.GestorSqlServer, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
        }
        public ListaEmpresa ListarEmpresa( int IdUsuarioAuditoria,int IdEmpresaPadre, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {
            
            return empresa.ListarEmpresa(DatosGlobales.GestorSqlServer, IdUsuarioAuditoria, IdEmpresaPadre, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
        }
        
        public Empresa GuardaEmpresaPrincipal( int IdEmpresa, int IdEmpresaPadre, int IdPersona, string NombreEmpresa, bool Activo, int IdUsuarioAuditoria)
        {

            return empresa.GuardaEmpresaPrincipal(DatosGlobales.GestorSqlServer, IdEmpresa, IdEmpresaPadre, IdPersona, NombreEmpresa, Activo, IdUsuarioAuditoria);
        }
        public Empresa GuardaEmpresa( int IdEmpresa, int IdEmpresaPadre,int IdPersona, string NombreEmpresa, bool Activo, int IdUsuarioAuditoria)
        {
            
            return empresa.GuardaEmpresa(DatosGlobales.GestorSqlServer, IdEmpresa, IdEmpresaPadre, IdPersona, NombreEmpresa, Activo, IdUsuarioAuditoria);
        }
        public Mensaje EliminarEmpresa( int IdEmpresa, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();
            
             mensaje = empresa.EliminarEmpresa(DatosGlobales.GestorSqlServer, IdEmpresa, IdUsuarioAuditoria);
            return mensaje;
        }
        public Empresa ObtenerEmpresa( int IdEmpresa)
        {            
            return empresa.ObtenerEmpresa(DatosGlobales.GestorSqlServer, IdEmpresa);
        }

        public ListaEmpresa ListarComboEmpresa()
        {            
            return empresa.ListarComboEmpresa(DatosGlobales.GestorSqlServer);
        }
        public ListaEmpresa ListarComboEmpresaPadre()
        {
            
            return empresa.ListarComboEmpresaPadre(DatosGlobales.GestorSqlServer);
        }
        //METODOS ASINCRONOS
        public async Task<ListaEmpresa> ListarEmpresaPrincipalAsync(int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {

            return await  empresa.ListarEmpresaPrincipalAsync(DatosGlobales.GestorSqlServer, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
        }
        public async Task<ListaEmpresa> ListarEmpresaAsync(int IdUsuarioAuditoria, int IdEmpresaPadre, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {

            return await  empresa.ListarEmpresaAsync(DatosGlobales.GestorSqlServer, IdUsuarioAuditoria, IdEmpresaPadre, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
        }

        public async Task<Empresa> GuardaEmpresaPrincipalAsync(int IdEmpresa, int IdEmpresaPadre, int IdPersona, string NombreEmpresa, bool Activo, int IdUsuarioAuditoria)
        {

            return await  empresa.GuardaEmpresaPrincipalAsync(DatosGlobales.GestorSqlServer, IdEmpresa, IdEmpresaPadre, IdPersona, NombreEmpresa, Activo, IdUsuarioAuditoria);
        }
        public async Task<Empresa> GuardaEmpresaAsync(int IdEmpresa, int IdEmpresaPadre, int IdPersona, string NombreEmpresa, bool Activo, int IdUsuarioAuditoria)
        {

            return await  empresa.GuardaEmpresaAsync(DatosGlobales.GestorSqlServer, IdEmpresa, IdEmpresaPadre, IdPersona, NombreEmpresa, Activo, IdUsuarioAuditoria);
        }
        public async Task<Mensaje> EliminarEmpresaAsync(int IdEmpresa, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();

            mensaje =await empresa.EliminarEmpresaAsync(DatosGlobales.GestorSqlServer, IdEmpresa, IdUsuarioAuditoria);
            return  mensaje;
        }
        public async Task<Empresa> ObtenerEmpresaAsync(int IdEmpresa)
        {
            return await  empresa.ObtenerEmpresaAsync(DatosGlobales.GestorSqlServer, IdEmpresa);
        }

        public async Task<ListaEmpresa> ListarComboEmpresaAsync()
        {
            return await  empresa.ListarComboEmpresaAsync(DatosGlobales.GestorSqlServer);
        }
        public async Task<ListaEmpresa> ListarComboEmpresaPadreAsync()
        {

            return await  empresa.ListarComboEmpresaPadreAsync(DatosGlobales.GestorSqlServer);
        }
    }
}
