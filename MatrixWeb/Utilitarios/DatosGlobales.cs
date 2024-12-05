

namespace MatrixUtilitarios
{
    public static class DatosGlobales
    {
        
        public static int SoporteAsignacionPendiente = 0;
        public const string NombreSoftware = "CMP";
        public const string NombreSoftwareLargo = "CMP Versión 1.0";
        public const string NombreSoftwareEspacio = "C M P";
        public static string NombreInstitucion = "CMP";
        public static string NombreInstitucionEspacio = "CMP";
        public static string NombreInstitucionLargo = "CMP";
        public static string VersionSoftware = "1.0";

        public static string ClaveEncripcion = "RasputinColoradoBlanco";

        //public static int GestorMatrixSql = 1;//sql server
        //public static int GestorMatrixOracle = 2;//Oracle
        //public static string ClaveMatrix = "MatrixService";//sql server
        public static int IdCatalogoTipoModulo = 2;//DESKTOP
        public static string GestorConectado = "Sql Server";

        public static string NombreUsuarioCliente = "";
        public static string NombrePcCliente = "";
        public static string IpCliente = "";

        public static int TipoConeccionMatrix = 2;//1= servicios/2= dao
        public static int IdUsuario { get; set; }
        public static string NombreUsuario { get; set; }
        public static int IdPersona { get; set; }
        public static int IdPersonal { get; set; }
        public static int IdPerfil{ get; set; }
        public static int IdTipoUsuario { get; set; }
        public static string Servidor { get; set; }
        public static string Datasource { get; set; }
           
        public static string RutaLogTxt { get; set; }

    }
}

