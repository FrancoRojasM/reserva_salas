
using General;
using MatrixLogic.General;
using System.Threading.Tasks;
using Utilitarios;
namespace MatrixService

{
    public class SvcPersonaSql : ISvcPersona
    {
        private GeneralLogic.PersonaLogic persona;
        private PersonaWebServiceReniecLogic personawebservice;
        public SvcPersonaSql()
        {
            persona = new GeneralLogic.PersonaLogic();
            personawebservice = new PersonaWebServiceReniecLogic();
        }
        
       

        public ListaPersona ListarPersonaPorAutoComplete( string NombreCompleto)
        {

            return persona.ListarPersonaPorAutoComplete(DatosGlobales.GestorSqlServer, NombreCompleto);
        }
        
        public async Task<ListaPersona> ListarComboAutocompleteMesaParte(string NombreCompleto)
        {

            return await persona.ListarComboAutocompleteMesaParte(DatosGlobales.GestorSqlServer, NombreCompleto);
        }
        public async Task<ListaPersona> ListarPersonaPorAutoCompleteAsync(string NombreCompleto)
        {

            return await persona.ListarPersonaPorAutoCompleteAsync(DatosGlobales.GestorSqlServer, NombreCompleto);
        }
        public ListaPersona ListarPersonaJuridicaPorAutoComplete( string NombreCompleto)
        {

            return persona.ListarPersonaJuridicaPorAutoComplete(DatosGlobales.GestorSqlServer, NombreCompleto);
        }
        public async Task<ListaPersona> ListarPersonaJuridicaPorAutoCompleteAsync(string NombreCompleto)
        {

            return await persona.ListarPersonaJuridicaPorAutoCompleteAsync(DatosGlobales.GestorSqlServer, NombreCompleto);
        }
        public ListaPersona ListarPersonaNaturalPorAutoComplete( string NombreCompleto)
        {

            return persona.ListarPersonaNaturalPorAutoComplete(DatosGlobales.GestorSqlServer, NombreCompleto);
        }

        public async Task<ListaPersona> ListarPersonaNaturalPorAutoCompleteAsync(string NombreCompleto)
        {
            return await persona.ListarPersonaNaturalPorAutoCompleteAsync(DatosGlobales.GestorSqlServer, NombreCompleto);
        }
        public ListaPersona ListarPersona( int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {

            return persona.ListarPersona(DatosGlobales.GestorSqlServer, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
        }
        public async Task<ListaPersona> ListarPersonaAsync(int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {

            return await persona.ListarPersonaAsync(DatosGlobales.GestorSqlServer, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
        }
        public Persona GuardaPersona( int IdPersona, int IdCatalogoTipoPersona, string NombreCompleto, string Nombres, string ApellidoPaterno, string ApellidoMaterno, int IdCatalogoTipoDocumentoPersonal, string NumeroDocumento, int IdUbigeo, string Direccion, string Celular, string FechaNacimiento, bool Sexo, int IdUsuarioAuditoria)
        {

            return persona.GuardaPersona(DatosGlobales.GestorSqlServer, IdPersona, IdCatalogoTipoPersona, NombreCompleto, Nombres, ApellidoPaterno, ApellidoMaterno, IdCatalogoTipoDocumentoPersonal, NumeroDocumento, IdUbigeo, Direccion, Celular, FechaNacimiento, Sexo, IdUsuarioAuditoria);
        }
        public async Task<Persona> GuardaPersonaAsync(int IdPersona, int IdCatalogoTipoPersona, string NombreCompleto, string Nombres, string ApellidoPaterno, string ApellidoMaterno, int IdCatalogoTipoDocumentoPersonal, string NumeroDocumento, int IdUbigeo, string Direccion, string Celular, string FechaNacimiento, bool Sexo,string Anexo,  int IdUsuarioAuditoria)
        {

            return await persona.GuardaPersonaAsync(DatosGlobales.GestorSqlServer, IdPersona, IdCatalogoTipoPersona, NombreCompleto, Nombres, ApellidoPaterno, ApellidoMaterno, IdCatalogoTipoDocumentoPersonal, NumeroDocumento, IdUbigeo, Direccion, Celular, FechaNacimiento, Sexo, Anexo, IdUsuarioAuditoria);
        }
        public Mensaje EliminarPersona( int IdPersona, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();
            mensaje = persona.EliminarPersona(DatosGlobales.GestorSqlServer, IdPersona, IdUsuarioAuditoria);
            return mensaje;
        }
        public async Task<Mensaje> EliminarPersonaAsync(int IdPersona, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();
            mensaje = await persona.EliminarPersonaAsync(DatosGlobales.GestorSqlServer, IdPersona, IdUsuarioAuditoria);
            return mensaje;
        }
        public Persona ObtenerPersona( int IdPersona)
        {
            return persona.ObtenerPersona(DatosGlobales.GestorSqlServer, IdPersona);
        }
        public async Task<Persona> ObtenerPersonaAsync(int IdPersona)
        {
            return await persona.ObtenerPersonaAsync(DatosGlobales.GestorSqlServer, IdPersona);
        }
        public async Task<Persona> ObtenerPersonaPorDocumento(string NumeroDocumento, int IdCatalogoTipoDocumentoPersonal)
        {
            return await persona.ObtenerPersonaPorDocumento(DatosGlobales.GestorSqlServer, NumeroDocumento, IdCatalogoTipoDocumentoPersonal);
        }

        
        public async Task<ListaPersona> ListarPersonaBuscarSISAsync(int IdPersona)
        {

            return await persona.ListarPersonaBuscarSISAsync(DatosGlobales.GestorSqlServer, IdPersona);
        }

        //public async Task<Persona> ObtenerDatosPersonaReniec(string NumeroDocumento)
        //{
        //    return await personawebservice.ObtenerDatosPersonaReniec(NumeroDocumento);
        //}

        public Persona ObtenerDatosPersonaReniecV2(string NumeroDocumento)
        {
            return personawebservice.ObtenerDatosPersonaReniecV2(NumeroDocumento);
        }


    }
}
		