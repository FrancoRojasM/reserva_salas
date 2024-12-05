using Utilitarios;
using System;
using System.Threading.Tasks;
using Casilla;
using General;
using System.Data;

namespace MatrixService
{
    public interface ISvcAdministrado
    {
        Task<ListaAdministrado> ListarAdministradoAsync(int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral);
        Task<Administrado> GuardaAdministradoAsync(int IdAdministrado, int IdPersona, int IdUsuario, string EmailNotificacion, string NumeroCelularNotificacion, string AsientoElectronico, string PartidaElectronica, bool Activo,string CodigoTelefonoConfirmacion,string CodigoCorreoConfirmacion,int CodigoCorreoValidar, int CodigoTelefonoValidar, int IdUsuarioAuditoria);
        Task<Mensaje> EliminarAdministradoAsync(int IdAdministrado, int IdUsuarioAuditoria);
        Task<Mensaje> GenerarCodigoCorreoConfirmacion(int IdAdministrado,string CodigoCorreoConfirmacion,string EmailNotificacion, int IdUsuarioAuditoria);
        Task<Mensaje> GenerarCodigoTelefonoConfirmacion(int IdAdministrado,string CodigoCorreoConfirmacion,string NumeroCelularNotificacion, int IdUsuarioAuditoria);
        Task<Administrado> ObtenerAdministradoAsync(int IdAdministrado, int IdUsuarioAuditoria);
        Task<DataSet> DescargarAdministrado(int IdUsuarioAuditoria);
        Task<ListaAdministrado> ListarAdministradoPorAutoComplete(int IdUsuarioAuditoria,string NombreCompleto);
        Task<Mensaje> GenerarCodigoCorreoConfirmacionLogin(int IdVerificacion, string CodigoCorreoConfirmacion, string EmailNotificacion,string CodigoTelefonoConfirmacion, string NumeroCelularNotificacion, int IdUsuarioAuditoria);
        Task<Mensaje> GenerarCodigoCorreoValidacionLogin(int IdVerificacion, string CodigoCorreoConfirmacion, string EmailNotificacion,string CodigoTelefonoConfirmacion, string NumeroCelularNotificacion, int IdUsuarioAuditoria);
    }
}
