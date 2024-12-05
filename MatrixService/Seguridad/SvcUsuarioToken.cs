		
		using Utilitarios;
		using Seguridad;
		using System.Threading.Tasks;
namespace MatrixService

{
    public class SvcUsuarioTokenSql : ISvcUsuarioToken
    {
        private SeguridadLogic.UsuarioTokenLogic usuariotoken;
        public SvcUsuarioTokenSql()
        {
            usuariotoken = new SeguridadLogic.UsuarioTokenLogic();
        }
        public ListaUsuarioToken ListarUsuarioToken(int IdUsuario, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {
            return usuariotoken.ListarUsuarioToken(DatosGlobales.GestorSqlServer, IdUsuario, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
        }
        public async Task<ListaUsuarioToken> ListarUsuarioTokenAsync(int IdUsuario, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {
            return await usuariotoken.ListarUsuarioTokenAsync(DatosGlobales.GestorSqlServer, IdUsuario, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
        }
        public UsuarioToken GuardaUsuarioToken(int IdUsuarioToken, int IdUsuario, string Issuer, string Audience, string HostDeUso, string Token, string FechaCreacion, string FechaVencimiento, bool Activo, int IdUsuarioAuditoria)
        {
            return usuariotoken.GuardaUsuarioToken(DatosGlobales.GestorSqlServer, IdUsuarioToken, IdUsuario, Issuer, Audience, HostDeUso, Token, FechaCreacion, FechaVencimiento, Activo, IdUsuarioAuditoria);
        }
        public async Task<UsuarioToken> ActualizarFechaUltimoAccesoToken(string Token, int IdUsuarioAuditoria)
        {
            return await usuariotoken.ActualizarFechaUltimoAccesoToken(DatosGlobales.GestorSqlServer, Token, IdUsuarioAuditoria);
        }
        public async Task<UsuarioToken> GuardaUsuarioTokenAsync(int IdUsuarioToken, int IdUsuario, string Issuer, string Audience, string HostDeUso, string Token, string FechaCreacion, string FechaVencimiento, bool Activo, int IdUsuarioAuditoria)
        {
            return await usuariotoken.GuardaUsuarioTokenAsync(DatosGlobales.GestorSqlServer, IdUsuarioToken, IdUsuario, Issuer, Audience, HostDeUso, Token, FechaCreacion, FechaVencimiento, Activo, IdUsuarioAuditoria);
        }
        public Mensaje EliminarUsuarioToken(int IdUsuarioToken, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();
            mensaje = usuariotoken.EliminarUsuarioToken(DatosGlobales.GestorSqlServer, IdUsuarioToken, IdUsuarioAuditoria);
            return mensaje;
        }
        public async Task<Mensaje> EliminarUsuarioTokenAsync(int IdUsuarioToken, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();
            mensaje = await usuariotoken.EliminarUsuarioTokenAsync(DatosGlobales.GestorSqlServer, IdUsuarioToken, IdUsuarioAuditoria);
            return mensaje;
        }
        public UsuarioToken ObtenerUsuarioToken(int IdUsuarioToken, int IdUsuarioAuditoria)
        {
            return usuariotoken.ObtenerUsuarioToken(DatosGlobales.GestorSqlServer, IdUsuarioToken, IdUsuarioAuditoria);
        }
        public async Task<UsuarioToken> ObtenerUsuarioTokenAsync(int IdUsuarioToken, int IdUsuarioAuditoria)
        {
            return await usuariotoken.ObtenerUsuarioTokenAsync(DatosGlobales.GestorSqlServer, IdUsuarioToken, IdUsuarioAuditoria);
        }
    }
}
		