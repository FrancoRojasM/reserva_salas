
using Utilitarios;
using System;
using Casilla;
using System.Threading.Tasks;
using General;
using System.Data;

namespace MatrixService

{
    public class SvcAdministradoSql : ISvcAdministrado
    {
        private CasillaLogic.AdministradoLogic administrado;
        public SvcAdministradoSql()
        {
            administrado = new CasillaLogic.AdministradoLogic();
        }
        public async Task<ListaAdministrado> ListarAdministradoAsync(int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {
            return await administrado.ListarAdministradoAsync(DatosGlobales.GestorSqlServer, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
        }
        public async Task<Administrado> GuardaAdministradoAsync(int IdAdministrado, int IdPersona, int IdUsuario, string EmailNotificacion, string NumeroCelularNotificacion, string AsientoElectronico, string PartidaElectronica, bool Activo, string CodigoTelefonoConfirmacion, string CodigoCorreoConfirmacion, int CodigoCorreoValidar, int CodigoTelefonoValidar, int IdUsuarioAuditoria)
        {
            return await administrado.GuardaAdministradoAsync(DatosGlobales.GestorSqlServer, IdAdministrado, IdPersona, IdUsuario, EmailNotificacion, NumeroCelularNotificacion, AsientoElectronico, PartidaElectronica, Activo, CodigoTelefonoConfirmacion, CodigoCorreoConfirmacion,  CodigoCorreoValidar,  CodigoTelefonoValidar, IdUsuarioAuditoria);
        }
        public async Task<Mensaje> EliminarAdministradoAsync(int IdAdministrado, int IdUsuarioAuditoria)
        {
            return await administrado.EliminarAdministradoAsync(DatosGlobales.GestorSqlServer, IdAdministrado, IdUsuarioAuditoria);
        }
        public async Task<Mensaje> GenerarCodigoCorreoConfirmacion(int IdAdministrado,string CodigoCorreoConfirmacion,string EmailNotificacion, int IdUsuarioAuditoria)
        {
            return await administrado.GenerarCodigoCorreoConfirmacion(DatosGlobales.GestorSqlServer, IdAdministrado, CodigoCorreoConfirmacion, EmailNotificacion, IdUsuarioAuditoria);
        }
        public async Task<Mensaje> GenerarCodigoTelefonoConfirmacion(int IdAdministrado,string CodigoCorreoConfirmacion,string NumeroCelularNotificacion, int IdUsuarioAuditoria)
        {
            return await administrado.GenerarCodigoTelefonoConfirmacion(DatosGlobales.GestorSqlServer, IdAdministrado, CodigoCorreoConfirmacion, NumeroCelularNotificacion, IdUsuarioAuditoria);
        }
        public async Task<Administrado> ObtenerAdministradoAsync(int IdAdministrado, int IdUsuarioAuditoria)
        {
            return await administrado.ObtenerAdministradoAsync(DatosGlobales.GestorSqlServer, IdAdministrado, IdUsuarioAuditoria);
        }
        public async Task<DataSet> DescargarAdministrado(int IdUsuarioAuditoria)
        {
            return await administrado.DescargarAdministrado(DatosGlobales.GestorSqlServer, IdUsuarioAuditoria);
        }
        public async Task<ListaAdministrado> ListarAdministradoPorAutoComplete(int IdUsuarioAuditoria, string NombreCompleto)
        {

            return await administrado.ListarAdministradoPorAutoComplete(DatosGlobales.GestorSqlServer,  IdUsuarioAuditoria, NombreCompleto);
        }
        public async Task<Mensaje> GenerarCodigoCorreoConfirmacionLogin(int IdVerificacion, string CodigoCorreoConfirmacion, string EmailNotificacion, string CodigoTelefonoConfirmacion, string NumeroCelularNotificacion, int IdUsuarioAuditoria)
        {
            return await administrado.GenerarCodigoCorreoConfirmacionLogin(DatosGlobales.GestorSqlServer, IdVerificacion, CodigoCorreoConfirmacion, EmailNotificacion, CodigoTelefonoConfirmacion, NumeroCelularNotificacion, IdUsuarioAuditoria);
        } 
        public async Task<Mensaje> GenerarCodigoCorreoValidacionLogin(int IdVerificacion, string CodigoCorreoConfirmacion, string EmailNotificacion, string CodigoTelefonoConfirmacion, string NumeroCelularNotificacion, int IdUsuarioAuditoria)
        {
            return await administrado.GenerarCodigoCorreoValidacionLogin(DatosGlobales.GestorSqlServer, IdVerificacion, CodigoCorreoConfirmacion, EmailNotificacion, CodigoTelefonoConfirmacion, NumeroCelularNotificacion, IdUsuarioAuditoria);
        }
    }
}
