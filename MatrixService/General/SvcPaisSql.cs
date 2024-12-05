
using Utilitarios;
using General;
using System.Threading.Tasks;

namespace MatrixService

{
    public class SvcPaisSql : ISvcPais
    {
        private GeneralLogic.PaisLogic pais;
        public SvcPaisSql()
        {
            pais = new GeneralLogic.PaisLogic();
        }
        public ListaPais ListarPais( int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {
            return pais.ListarPais(DatosGlobales.GestorSqlServer, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
        }
        public Pais GuardaPais( int IdPais, string NombrePais, string Gentilicio, string Alfa3, string Alfa2, string Bandera, int IdUsuarioAuditoria)
        {
            return pais.GuardaPais(DatosGlobales.GestorSqlServer, IdPais, NombrePais, Gentilicio, Alfa3, Alfa2, Bandera, IdUsuarioAuditoria);
        }
        public Mensaje EliminarPais( int IdPais, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();
            mensaje = pais.EliminarPais(DatosGlobales.GestorSqlServer, IdPais, IdUsuarioAuditoria);
            return mensaje;
        }
        public Pais ObtenerPais( int IdPais, int IdUsuarioAuditoria)
        {
            return pais.ObtenerPais(DatosGlobales.GestorSqlServer, IdPais, IdUsuarioAuditoria);
        }

        public ListaPais ListarComboPais( int IdUsuarioAuditoria)
        {
            return pais.ListarComboPais(DatosGlobales.GestorSqlServer, IdUsuarioAuditoria);
        }
        public async Task<ListaPais> ListarComboPaisAsync(int IdUsuarioAuditoria)
        {
            return await pais.ListarComboPaisAsync(DatosGlobales.GestorSqlServer, IdUsuarioAuditoria);
        }

    }
}
