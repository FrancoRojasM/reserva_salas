
using General;
using System.Threading.Tasks;
using Utilitarios;
namespace MatrixService

{
    public class SvcCargoOracle : ISvcCargo
    {
        private GeneralLogic.CargoLogic cargo;
        public SvcCargoOracle()
        {
            cargo = new GeneralLogic.CargoLogic();
        }
        public ListaCargo ListarCargo( int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {
            
            return cargo.ListarCargo(DatosGlobales.GestorOracle, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
        }

        public Cargo GuardaCargo( int IdCargo, string NombreCargo, int IdCatalogoTipoCargo,bool Activo, int IdUsuarioAuditoria)
        {
            
            return cargo.GuardaCargo(DatosGlobales.GestorOracle, IdCargo, NombreCargo,IdCatalogoTipoCargo, Activo, IdUsuarioAuditoria);
        }
        public Mensaje EliminarCargo( int IdCargo, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();            
            mensaje = cargo.EliminarCargo(DatosGlobales.GestorOracle, IdCargo, IdUsuarioAuditoria);
            return mensaje;
        }
        public Cargo ObtenerCargo( int IdCargo)
        {
            
            return cargo.ObtenerCargo(DatosGlobales.GestorOracle, IdCargo);
        }

        public ListaCargo ListarComboCargo()
        {            
            return cargo.ListarComboCargo(DatosGlobales.GestorOracle);
        }
        public async Task<ListaCargo> ListarComboCargoAsync()
        {
            return await cargo.ListarComboCargoAsync(DatosGlobales.GestorOracle);
        }        
    }
}
