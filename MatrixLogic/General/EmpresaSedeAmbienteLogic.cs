		using System;
		using System.Runtime.Serialization;
		using Utilitarios;
		using System.Data;
		using System.Collections.Generic;
		using General;
namespace GeneralLogic
{
    
    public class EmpresaSedeAmbienteLogic
    {

        private EmpresaSedeAmbiente empresasedeambiente;
        private ListaEmpresaSedeAmbiente lista;

        public EmpresaSedeAmbienteLogic()
        {
            empresasedeambiente = new EmpresaSedeAmbiente();
            lista = new ListaEmpresaSedeAmbiente();
        }
        public Mensaje EliminarEmpresaSedeAmbiente(int Gestor, int IdEmpresaSedeAmbiente, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();
            if (Gestor == DatosGlobales.GestorSqlServer)
            {
                System.Object[] ParamtrosOutPut = null; 
                ParamtrosOutPut = ConexionSqlServer.GDatos.Ejecutar(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paEliminarEmpresaSedeAmbiente", IdEmpresaSedeAmbiente, IdUsuarioAuditoria, mensaje.DescripcionMensaje, mensaje.CodigoMensaje);
                if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
                {
                    mensaje.CodigoMensaje = 1;
                    mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA CAPA DE DATOS [EliminarEmpresaSedeAmbiente] VERIFICAR CONSOLA";
                    mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
                    return mensaje;
                }
                mensaje.DescripcionMensaje = ParamtrosOutPut[0].ToString();
                mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[1].ToString());
            }
            return mensaje;
        }

        public EmpresaSedeAmbiente ObtenerEmpresaSedeAmbiente(int Gestor, int IdEmpresaSedeAmbiente, int IdUsuarioAuditoria)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paObtenerEmpresaSedeAmbiente", IdEmpresaSedeAmbiente, IdUsuarioAuditoria);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        empresasedeambiente.mensaje.CodigoMensaje = 1;
                        empresasedeambiente.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ObtenerEmpresaSedeAmbiente], VERIFICAR CONSOLA";
                        empresasedeambiente.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();

                        return empresasedeambiente;
                    }
                    dt = ds.Tables[0].Copy();
                    empresasedeambiente.IdEmpresaSedeAmbiente = (int)dt.Rows[0]["IdEmpresaSedeAmbiente"];
                    empresasedeambiente.empresasede.IdEmpresaSede = (int)dt.Rows[0]["IdEmpresaSede"];
                    empresasedeambiente.CodigoAmbiente = (string)dt.Rows[0]["CodigoAmbiente"];
                    empresasedeambiente.NombreAmbiente = (string)dt.Rows[0]["NombreAmbiente"];
                    empresasedeambiente.DescripcionAmbiente = (string)dt.Rows[0]["DescripcionAmbiente"];
                    empresasedeambiente.Activo = (bool)dt.Rows[0]["Activo"];

                }
                return empresasedeambiente;
            }
            catch (Exception ex)
            {
                empresasedeambiente.mensaje.CodigoMensaje = 1;
                empresasedeambiente.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ObtenerEmpresaSedeAmbiente], VERIFICAR CONSOLA";
                empresasedeambiente.mensaje.DescripcionMensajeSistema = ex.Message;
                return empresasedeambiente;
            }
            finally
            {
                ds.Dispose();
                ds.Clear();
                dt.Dispose();
                dt.Clear();
            }
        }


        public EmpresaSedeAmbiente GuardaEmpresaSedeAmbiente(int Gestor, int IdEmpresaSedeAmbiente, int IdEmpresaSede, string CodigoAmbiente, string NombreAmbiente, string DescripcionAmbiente, bool Activo, int IdUsuarioAuditoria)
        {
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    System.Object[] ParamtrosOutPut = null; 
                    ParamtrosOutPut = ConexionSqlServer.GDatos.Ejecutar(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paGuardarEmpresaSedeAmbiente", IdEmpresaSedeAmbiente, IdEmpresaSede, CodigoAmbiente, NombreAmbiente, DescripcionAmbiente, Activo, IdUsuarioAuditoria, "", 0);
                    if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
                    {
                        empresasedeambiente.mensaje.CodigoMensaje = 1;
                        empresasedeambiente.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [GuardaEmpresaSedeAmbiente], VERIFICAR CONSOLA";
                        empresasedeambiente.mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
                        return empresasedeambiente;
                    }
                    empresasedeambiente.IdEmpresaSedeAmbiente = Convert.ToInt32(ParamtrosOutPut[0].ToString());
                    empresasedeambiente.mensaje.DescripcionMensaje = ParamtrosOutPut[1].ToString();
                    empresasedeambiente.mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[2].ToString());
                }
                return empresasedeambiente;
            }
            catch (Exception ex)
            {
                empresasedeambiente.mensaje.CodigoMensaje = 1;
                empresasedeambiente.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [GuardaEmpresaSedeAmbiente], VERIFICAR CONSOLA";
                empresasedeambiente.mensaje.DescripcionMensajeSistema = ex.Message;
                return empresasedeambiente;
            }
        }



        public ListaEmpresaSedeAmbiente ListarEmpresaSedeAmbiente(int Gestor, int IdEmpresaSede, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paListarEmpresaSedeAmbiente", IdEmpresaSede, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarEmpresaSedeAmbiente], VERIFICAR CONSOLA";
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
                        return lista;
                    }
                    dt = ds.Tables[0].Copy();
                    DataTable dtParametros = null;
                    dtParametros = ds.Tables[1].Copy();
                    lista.paginacion.TotalRegistros = Convert.ToInt32(dtParametros.Rows[0][0]);
                }
                EmpresaSedeAmbiente empresasedeambiente = null;
                foreach (DataRow row in dt.Rows)
                {
                    empresasedeambiente = new EmpresaSedeAmbiente();
                    empresasedeambiente.IdEmpresaSedeAmbiente = (int)row["IdEmpresaSedeAmbiente"];
                    empresasedeambiente.empresasede.IdEmpresaSede = (int)row["IdEmpresaSede"];
                    empresasedeambiente.empresasede.NombreSede = (string)row["NombreSede"];
                    empresasedeambiente.CodigoAmbiente = (string)row["CodigoAmbiente"];
                    empresasedeambiente.NombreAmbiente = (string)row["NombreAmbiente"];
                    empresasedeambiente.DescripcionAmbiente = (string)row["DescripcionAmbiente"];
                    empresasedeambiente.Activo = (bool)row["Activo"];

                    lista.lista.Add(empresasedeambiente);
                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarEmpresaSedeAmbiente], VERIFICAR CONSOLA";
                lista.mensaje.DescripcionMensajeSistema = ex.Message;
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
        public ListaEmpresaSedeAmbiente ListarComboEmpresaSedeAmbiente(int Gestor, int IdUsuarioAuditoria)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paListarComboEmpresaSedeAmbiente", IdUsuarioAuditoria);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarComboEmpresaSedeAmbiente], VERIFICAR CONSOLA";
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
                        return lista;
                    }
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0].Copy();
                    }
                }
                if (dt != null)
                {
                    EmpresaSedeAmbiente empresasedeambiente = null;
                    foreach (DataRow row in dt.Rows)
                    {
                        empresasedeambiente = new EmpresaSedeAmbiente();
                        empresasedeambiente.IdEmpresaSedeAmbiente = Convert.ToInt32(row["IdEmpresaSedeAmbiente"].ToString());
                        empresasedeambiente.NombreAmbiente = row["NombreAmbiente"].ToString();
                        lista.lista.Add(empresasedeambiente);
                    }

                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarComboEmpresaSedeAmbiente], VERIFICAR CONSOLA";
                lista.mensaje.DescripcionMensajeSistema = ex.Message;
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

    }   

}

	

		