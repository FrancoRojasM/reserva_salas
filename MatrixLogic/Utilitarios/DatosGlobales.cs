using MatrixLogic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Sistema;
using System.Configuration;
using System.IO;
namespace Utilitarios
{
    public static class DatosGlobales
    {

        public static int GestorSqlServer = 1;//sql server
        public static int GestorOracle = 2;
        public static int GestorApi = 3;
        public static int GestorHana = 4;

        //public static string HostApi = "http://192.168.10.121:8080/api/";
        //public static string ClaveApi = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiaGx1Y2FzZmVybmFuZGV6IiwiSWRVc3VhcmlvIjoiODIiLCJIb3N0RW50aWRhZFNvbGljaXRhbnRlIjoiKiIsIm5iZiI6MTYyMjg2NjY0OSwiZXhwIjoxNjI1OTc5NjAwLCJpc3MiOiJodHRwOi8vMTkyLjE2OC4xMC4xMjE6ODA4MC8iLCJhdWQiOiJodHRwOi8vMTkyLjE2OC4xMC4xMjE6ODA4MC8ifQ.zMQfZFGhVsTfAhbWJgOSC5dJpNDvan5C_jJlmeTIk6Q";


        public enum ListaConexiones
        {
            cnGeneralSql = 1,
            cnSeguridadSql = 2,
            cnRecursoHumanoSql = 3,
            cnCasillaSql = 3,
            cnAsistenciasSql = 3,
            cnSistemaSql = 4,
            cnCatastroSql = 5,
            cnInstitucionalSql = 6,
            cnCourrierSql = 7,
            cnTramiteSql = 8,
            cnIDOSGDSql = 9,
            cnInteroperabilidadSql = 10,
            cnObrasSql = 11,
            cnEstudiosSql = 12,
            cnProyectosSGP = 13,
            cnInventarioSql = 14,
            cnSancionesSql = 15,

            cnGeneralOracle = 101,
            cnSeguridadOracle = 102,
            cnPersonalOracle = 103,
            cnSistemaOracle = 104,
            cnCatastroOracle = 105,
            cnGeneralHana = 201,
            cnReservasSql = 202,
        }
        public static IConfiguration Configuration { get; set; }
        public static string ObtenerCadenaDeConexion(ListaConexiones ConexionDeEsquema, int opcion = 1)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // <== compile failing here
                .AddJsonFile("appsettings.json");
            
            Configuration = builder.Build();
            string conexionString = "";

            if (ConexionDeEsquema == ListaConexiones.cnGeneralHana)
            {
                conexionString = Configuration.GetConnectionString("ConexionHanaGeneral");
            }
            else if (ConexionDeEsquema == ListaConexiones.cnProyectosSGP)
            {
                conexionString = Configuration.GetConnectionString("ConexionSqlSGP");
            }
            else 
            {
                if (opcion == 1)
                {
                    conexionString = Configuration.GetConnectionString("ConexionSqlGeneral");
                }
                else
                {
                    conexionString = Configuration.GetConnectionString("ConexionSqlSiaf");
                }
            }
            //string conexionString = Startup.StaticConfig ["ConnectionStrings:ConexionSqlGeneral"];

            /*  PRODUCCION DOMINIO HL.NET.PE*/
            //if (ConexionDeEsquema == ListaConexiones.cnGeneralSql) { conex
            //if (ConexionDeEsquema == ListaConexiones.cnSeguridadSql) { con
            //if (ConexionDeEsquema == ListaConexiones.cnSistemaSql) { conex
            //if (ConexionDeEsquema == ListaConexiones.cnRecursoHumanoSql) {
            //if (ConexionDeEsquema == ListaConexiones.cnCatastroSql) { cone
            //if (ConexionDeEsquema == ListaConexiones.cnInstitucionalSql) {
            //if (ConexionDeEsquema == ListaConexiones.cnCourrierSql) { cone
            //if (ConexionDeEsquema == ListaConexiones.cnTramiteSql) { conex


            /*  PRODUCCION DOMINIO HL.NET.PE*/
            //if (ConexionDeEsquema == ListaConexiones.cnGeneralSql) { conexionString = "Data Source=154.12.232.124\\INSTEST;Initial Catalog=BD_SGD;Persist Security Info=True;User ID=usrMgd;Password=wsx123WSX;Connect Timeout=200; pooling='false'; Max Pool Size=999999"; return conexionString; };
            //if (ConexionDeEsquema == ListaConexiones.cnSeguridadSql) { conexionString = "Data Source=154.12.232.124\\INSTEST;Initial Catalog=BD_SGD;Persist Security Info=True;User ID=usrMgd;Password=wsx123WSX;Connect Timeout=200; pooling='false'; Max Pool Size=999999"; return conexionString; };
            //if (ConexionDeEsquema == ListaConexiones.cnSistemaSql) { conexionString = "Data Source=154.12.232.124\\INSTEST;Initial Catalog=BD_SGD;Persist Security Info=True;User ID=usrMgd;Password=wsx123WSX;Connect Timeout=200; pooling='false'; Max Pool Size=999999"; return conexionString; };
            //if (ConexionDeEsquema == ListaConexiones.cnRecursoHumanoSql) { conexionString = "Data Source=154.12.232.124\\INSTEST;Initial Catalog=BD_SGD;Persist Security Info=True;User ID=usrMgd;Password=wsx123WSX;Connect Timeout=200; pooling='false'; Max Pool Size=999999"; return conexionString; };
            //if (ConexionDeEsquema == ListaConexiones.cnCatastroSql) { conexionString = "Data Source=154.12.232.124\\INSTEST;Initial Catalog=BD_SGD;Persist Security Info=True;User ID=usrMgd;Password=wsx123WSX;Connect Timeout=200; pooling='false'; Max Pool Size=999999"; return conexionString; };
            //if (ConexionDeEsquema == ListaConexiones.cnInstitucionalSql) { conexionString = "Data Source=154.12.232.124\\INSTEST;Initial Catalog=BD_SGD;Persist Security Info=True;User ID=usrMgd;Password=wsx123WSX;Connect Timeout=200; pooling='false'; Max Pool Size=999999"; return conexionString; };
            //if (ConexionDeEsquema == ListaConexiones.cnCourrierSql) { conexionString = "Data Source=154.12.232.124\\INSTEST;Initial Catalog=BD_SGD;Persist Security Info=True;User ID=usrMgd;Password=wsx123WSX;Connect Timeout=200; pooling='false'; Max Pool Size=999999"; return conexionString; };
            //if (ConexionDeEsquema == ListaConexiones.cnTramiteSql) { conexionString = "Data Source=154.12.232.124\\INSTEST;Initial Catalog=BD_SGD;Persist Security Info=True;User ID=usrMgd;Password=wsx123WSX;Connect Timeout=200; pooling='false'; Max Pool Size=999999"; return conexionString; };

            //DESARROLLO PROVIAS
            //if (ConexionDeEsquema == ListaConexiones.cnGeneralSql) { conexionString = "Data Source=DBD13;Initial Catalog=BD_SGD;Persist Security Info=True;User ID=userSGD;Password=xyz456*;Connect Timeout=200; pooling='false'; Max Pool Size=999999"; return conexionString; };
            //if (ConexionDeEsquema == ListaConexiones.cnSeguridadSql) { conexionString = "Data Source=DBD13;Initial Catalog=BD_SGD;Persist Security Info=True;User ID=userSGD;Password=xyz456*;Connect Timeout=200; pooling='false'; Max Pool Size=999999"; return conexionString; };
            //if (ConexionDeEsquema == ListaConexiones.cnSistemaSql) { conexionString = "Data Source=DBD13;Initial Catalog=BD_SGD;Persist Security Info=True;User ID=userSGD;Password=xyz456*;Connect Timeout=200; pooling='false'; Max Pool Size=999999"; return conexionString; };
            //if (ConexionDeEsquema == ListaConexiones.cnRecursoHumanoSql) { conexionString = "Data Source=DBD13;Initial Catalog=BD_SGD;Persist Security Info=True;User ID=userSGD;Password=xyz456*;Connect Timeout=200; pooling='false'; Max Pool Size=999999"; return conexionString; };
            //if (ConexionDeEsquema == ListaConexiones.cnCatastroSql) { conexionString = "Data Source=DBD13;Initial Catalog=BD_SGD;Persist Security Info=True;User ID=userSGD;Password=xyz456*;Connect Timeout=200; pooling='false'; Max Pool Size=999999"; return conexionString; };
            //if (ConexionDeEsquema == ListaConexiones.cnInstitucionalSql) { conexionString = "Data Source=DBD13;Initial Catalog=BD_SGD;Persist Security Info=True;User ID=userSGD;Password=xyz456*;Connect Timeout=200; pooling='false'; Max Pool Size=999999"; return conexionString; };
            //if (ConexionDeEsquema == ListaConexiones.cnCourrierSql) { conexionString = "Data Source=DBD13;Initial Catalog=BD_SGD;Persist Security Info=True;User ID=userSGD;Password=xyz456*;Connect Timeout=200; pooling='false'; Max Pool Size=999999"; return conexionString; };
            //if (ConexionDeEsquema == ListaConexiones.cnTramiteSql) { conexionString = "Data Source=DBD13;Initial Catalog=BD_SGD;Persist Security Info=True;User ID=userSGD;Password=xyz456*;Connect Timeout=200; pooling='false'; Max Pool Size=999999"; return conexionString; };

            ////* TEST PROVIAS
            //if (ConexionDeEsquema == ListaConexiones.cnGeneralSql) { conexionString = "Data Source=DBC19;Initial Catalog=BD_SGD;Persist Security Info=True;User ID=userSGD;Password=abc123*;Connect Timeout=200; pooling='false'; Max Pool Size=999999"; return conexionString; };
            //if (ConexionDeEsquema == ListaConexiones.cnSeguridadSql) { conexionString = "Data Source=DBC19;Initial Catalog=BD_SGD;Persist Security Info=True;User ID=userSGD;Password=abc123*;Connect Timeout=200; pooling='false'; Max Pool Size=999999"; return conexionString; };
            //if (ConexionDeEsquema == ListaConexiones.cnSistemaSql) { conexionString = "Data Source=DBC19;Initial Catalog=BD_SGD;Persist Security Info=True;User ID=userSGD;Password=abc123*;Connect Timeout=200; pooling='false'; Max Pool Size=999999"; return conexionString; };
            //if (ConexionDeEsquema == ListaConexiones.cnRecursoHumanoSql) { conexionString = "Data Source=DBC19;Initial Catalog=BD_SGD;Persist Security Info=True;User ID=userSGD;Password=abc123*;Connect Timeout=200; pooling='false'; Max Pool Size=999999"; return conexionString; };
            //if (ConexionDeEsquema == ListaConexiones.cnCatastroSql) { conexionString = "Data Source=DBC19;Initial Catalog=BD_SGD;Persist Security Info=True;User ID=userSGD;Password=abc123*;Connect Timeout=200; pooling='false'; Max Pool Size=999999"; return conexionString; };
            //if (ConexionDeEsquema == ListaConexiones.cnInstitucionalSql) { conexionString = "Data Source=DBC19;Initial Catalog=BD_SGD;Persist Security Info=True;User ID=userSGD;Password=abc123*;Connect Timeout=200; pooling='false'; Max Pool Size=999999"; return conexionString; };
            //if (ConexionDeEsquema == ListaConexiones.cnCourrierSql) { conexionString = "Data Source=DBC19;Initial Catalog=BD_SGD;Persist Security Info=True;User ID=userSGD;Password=abc123*;Connect Timeout=200; pooling='false'; Max Pool Size=999999"; return conexionString; };
            //if (ConexionDeEsquema == ListaConexiones.cnTramiteSql) { conexionString = "Data Source=DBC19;Initial Catalog=BD_SGD;Persist Security Info=True;User ID=userSGD;Password=abc123*;Connect Timeout=200; pooling='false'; Max Pool Size=999999"; return conexionString; };

            return conexionString;
        }
    }
}
