
using General;
using System.Threading.Tasks;
using Utilitarios;
namespace MatrixService

{
    public class SvcEmpresaSedeSql : ISvcEmpresaSede
    {
        private GeneralLogic.EmpresaSedeLogic empresasede;
        public SvcEmpresaSedeSql()
        {
            empresasede = new GeneralLogic.EmpresaSedeLogic();
        }
        public ListaEmpresaSede ListarEmpresaSede(int IdEmpresa, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {

            return empresasede.ListarEmpresaSede(DatosGlobales.GestorSqlServer, IdEmpresa, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
        }
        public EmpresaSede GuardaEmpresaSede(int IdEmpresaSede, int IdEmpresa, string DireccionSede, string NombreSede, bool Activo, int IdUsuarioAuditoria)
        {

            return empresasede.GuardaEmpresaSede(DatosGlobales.GestorSqlServer, IdEmpresaSede, IdEmpresa, DireccionSede, NombreSede, Activo, IdUsuarioAuditoria);
        }
        public Mensaje EliminarEmpresaSede(int IdEmpresaSede, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();

            mensaje = empresasede.EliminarEmpresaSede(DatosGlobales.GestorSqlServer, IdEmpresaSede, IdUsuarioAuditoria);
            return mensaje;
        }
        public EmpresaSede ObtenerEmpresaSede(int IdEmpresaSede, int IdUsuarioAuditoria)
        {

            return empresasede.ObtenerEmpresaSede(DatosGlobales.GestorSqlServer, IdEmpresaSede, IdUsuarioAuditoria);
        }
        public ListaEmpresaSede ListarComboEmpresaSede(int IdUsuarioAuditoria, int IdEmpresaPadre)
        {

            return empresasede.ListarComboEmpresaSede(DatosGlobales.GestorSqlServer, IdUsuarioAuditoria, IdEmpresaPadre);
        }

        //METODOS ASINCRONOS
        public async Task<ListaEmpresaSede> ListarEmpresaSedeAsync(int IdEmpresa, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {
            return await empresasede.ListarEmpresaSedeAsync(DatosGlobales.GestorSqlServer, IdEmpresa, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
        }
        public async Task<EmpresaSede> GuardaEmpresaSedeAsync(int IdEmpresaSede, int IdEmpresa, string DireccionSede, string NombreSede, bool Activo, int IdUsuarioAuditoria)
        {
            return await empresasede.GuardaEmpresaSedeAsync(DatosGlobales.GestorSqlServer, IdEmpresaSede, IdEmpresa, DireccionSede, NombreSede, Activo, IdUsuarioAuditoria);
        }
        public async Task<Mensaje> EliminarEmpresaSedeAsync(int IdEmpresaSede, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();
            mensaje = await empresasede.EliminarEmpresaSedeAsync(DatosGlobales.GestorSqlServer, IdEmpresaSede, IdUsuarioAuditoria);
            return mensaje;
        }
        public async Task<EmpresaSede> ObtenerEmpresaSedeAsync(int IdEmpresaSede, int IdUsuarioAuditoria)
        {
            return await empresasede.ObtenerEmpresaSedeAsync(DatosGlobales.GestorSqlServer, IdEmpresaSede, IdUsuarioAuditoria);
        }
        public async Task<ListaEmpresaSede> ListarComboEmpresaSedeAsync(int IdUsuarioAuditoria, int IdEmpresaPadre)
        {
            return await empresasede.ListarComboEmpresaSedeAsync(DatosGlobales.GestorSqlServer, IdUsuarioAuditoria, IdEmpresaPadre);
        }
    }
}