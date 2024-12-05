		using System;
		using System.Runtime.Serialization;
		using Utilitarios;
		using System.Data;
		using System.Collections.Generic;
		using General;
namespace GeneralLogic
{
    
    public class MesLogic
    {
        private Mes mes;
        private ListaMes lista;

        public MesLogic()
        {
            mes = new Mes();
            lista = new ListaMes();
        }
        public Mensaje EliminarMes(int Gestor, int IdMes, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();
            if (Gestor == DatosGlobales.GestorSqlServer)
            {
                System.Object[] ParamtrosOutPut = null; 
                ParamtrosOutPut = ConexionSqlServer.GDatos.Ejecutar(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paEliminarMes", IdMes, IdUsuarioAuditoria, mensaje.DescripcionMensaje, mensaje.CodigoMensaje);
                mensaje.DescripcionMensaje = ParamtrosOutPut[0].ToString();
                mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[1].ToString());
            }
            return mensaje;
        }

        public Mes ObtenerMes( int Gestor, int IdMes, int IdUsuarioAuditoria)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (IdMes == 0)
                {
                    return mes;
                }

                if (Gestor == DatosGlobales.GestorSqlServer)
                {

                     ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paObtenerMes", IdMes);
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0].Copy();
                        if (dt.Rows.Count > 0)
                        {
                            mes.IdMes = (int)dt.Rows[0]["IdMes"];
                            mes.NombreMes = (string)dt.Rows[0]["NombreMes"];
                            mes.NominacionAbreviada = (string)dt.Rows[0]["NominacionAbreviada"];

                        }
                    }
                }
                return mes;
            }
            catch (Exception ex)
            {
                mes.mensaje.CodigoMensaje = 1;
                mes.mensaje.DescripcionMensaje = ex.Message;
                return mes;
            }
            finally
            {
                ds.Dispose();
                ds.Clear();
                dt.Dispose();
                dt.Clear();
            }
        }

        public Mes GuardaMes(int Gestor, int IdMes, string NombreMes, string NominacionAbreviada, int IdUsuarioAuditoria)
        {
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    System.Object[] ParamtrosOutPut = null; 
                    ParamtrosOutPut = ConexionSqlServer.GDatos.Ejecutar(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paGuardarMes", IdMes, NombreMes, NominacionAbreviada, IdUsuarioAuditoria, "", 0);
                    mes.IdMes = Convert.ToInt32(ParamtrosOutPut[0].ToString());
                    mes.mensaje.DescripcionMensaje = ParamtrosOutPut[1].ToString();
                    mes.mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[2].ToString());
                }
                return mes;
            }
            catch (Exception ex)
            {
                mes.mensaje.CodigoMensaje = 1;
                mes.mensaje.DescripcionMensaje = ex.Message;
                return mes;
            }
        }
        public ListaMes ListarMes(int Gestor, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {

                     ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paListarMes", IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0].Copy();
                        DataTable dtParametros = null;
                        dtParametros = ds.Tables[1].Copy();
                        lista.paginacion.TotalRegistros = Convert.ToInt32(dtParametros.Rows[0][0]);
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    Mes mes = null;
                    foreach (DataRow row in dt.Rows)
                    {
                        mes = new Mes();
                        mes.IdMes = (int)row["IdMes"];
                        mes.NombreMes = (string)row["NombreMes"];
                        mes.NominacionAbreviada = (string)row["NominacionAbreviada"];

                        lista.lista.Add(mes);
                    }
                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = ex.Message;
                return lista;
            }
            finally
            {
                ds.Dispose();
                ds.Clear();
                dt.Dispose();
                dt.Clear();
            }
        }
        public ListaMes ListarComboMes(int Gestor, int IdUsuarioAuditoria)
        {
            DataTable dt = new DataTable();
            if (Gestor == DatosGlobales.GestorSqlServer)
            {
                DataSet ds = null;
                 ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paListarComboMes");
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0].Copy();
                }
            }
            if (dt != null)
            {
                Mes mes = null;
                foreach (DataRow row in dt.Rows)
                {
                    mes = new Mes();
                    mes.IdMes = Convert.ToInt32(row["IdMes"].ToString());
                    mes.NombreMes = row["NombreMes"].ToString();
                    lista.lista.Add(mes);
                }
            }
            return lista;
        }

    }    

}
	

		