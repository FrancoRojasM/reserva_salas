using General;
using System.ServiceModel;
using System.Threading.Tasks;
using Utilitarios;
namespace MatrixService
{

    public interface ISvcPersona
    {
        ListaPersona ListarPersonaPorAutoComplete(string NombreCompleto);
        Task<ListaPersona> ListarComboAutocompleteMesaParte(string NombreCompleto);
        Task<ListaPersona> ListarPersonaPorAutoCompleteAsync(string NombreCompleto);
        ListaPersona ListarPersonaJuridicaPorAutoComplete(string NombreCompleto);
        Task<ListaPersona> ListarPersonaJuridicaPorAutoCompleteAsync(string NombreCompleto);
        ListaPersona ListarPersonaNaturalPorAutoComplete(string NombreCompleto);
        Task<ListaPersona> ListarPersonaNaturalPorAutoCompleteAsync(string NombreCompleto);
        ListaPersona ListarPersona(int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral);
        Task<ListaPersona> ListarPersonaAsync(int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral);
        Persona GuardaPersona(int IdPersona, int IdCatalogoTipoPersona, string NombreCompleto, string Nombres, string ApellidoPaterno, string ApellidoMaterno, int IdCatalogoTipoDocumentoPersonal, string NumeroDocumento, int IdUbigeo, string Direccion, string Celular, string FechaNacimiento, bool Sexo, int IdUsuarioAuditoria);
        Task<Persona> GuardaPersonaAsync(int IdPersona, int IdCatalogoTipoPersona, string NombreCompleto, string Nombres, string ApellidoPaterno, string ApellidoMaterno, int IdCatalogoTipoDocumentoPersonal, string NumeroDocumento, int IdUbigeo, string Direccion, string Celular, string FechaNacimiento, bool Sexo ,string Anexo, int IdUsuarioAuditoria);
        Mensaje EliminarPersona(int IdPersona, int IdUsuarioAuditoria);
        Task<Mensaje> EliminarPersonaAsync(int IdPersona, int IdUsuarioAuditoria);
        Persona ObtenerPersona(int IdPersona);   
        Task<Persona> ObtenerPersonaAsync(int IdPersona);
        Task<ListaPersona> ListarPersonaBuscarSISAsync(int IdPersona);
        //Task<Persona> ObtenerDatosPersonaReniec(string NumeroDocumento);
        Persona ObtenerDatosPersonaReniecV2(string NumeroDocumento);

        Task<Persona> ObtenerPersonaPorDocumento(string NumeroDocumento, int TipoDocumentoPersona);

        
    }
}

