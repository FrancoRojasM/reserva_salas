using System;
using System.Runtime.Serialization;
using Utilitarios;
using System.Data;
using System.Collections.Generic;
using General;
using System.Threading.Tasks;

namespace GeneralLogic
{    
    public class CargoLogic
    {
        private Cargo cargo;
        private ListaCargo Lista;
        public CargoLogic()
        {
            cargo = new Cargo();
            Lista = new ListaCargo();
        }
        public Mensaje EliminarCargo(int Gestor, int IdCargo, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();
            System.Object[] ParamtrosOutPut = null;
            if (Gestor == DatosGlobales.GestorSqlServer)
            {                
                ParamtrosOutPut = ConexionSqlServer.GDatos.Ejecutar(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paEliminarCargo", IdCargo, IdUsuarioAuditoria, mensaje.DescripcionMensaje, mensaje.CodigoMensaje);               
            }
          
            if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
            {
                mensaje.CodigoMensaje = 1;
                mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA CAPA DE DATOS [EliminarCargo] VERIFICAR CONSOLA";
                mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
                return mensaje;
            }
            mensaje.DescripcionMensaje = ParamtrosOutPut[0].ToString();
            mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[1].ToString());
            return mensaje;
        }
        public Cargo ObtenerCargo(int Gestor, int IdCargo)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (IdCargo == 0)
                {
                    return cargo;
                }

                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paObtenerCargo", IdCargo);
                }
                
                if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                {
                    cargo.mensaje.CodigoMensaje = 1;
                    cargo.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ObtenerCargo], VERIFICAR CONSOLA";
                    cargo.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();

                    return cargo;
                }
                dt = ds.Tables[0].Copy();
                cargo.IdCargo = (int)dt.Rows[0]["IdCargo"];
                cargo.catalogotipocargo.IdCatalogo= (int)dt.Rows[0]["IdCatalogoTipoCargo"];
                cargo.NombreCargo = (string)dt.Rows[0]["NombreCargo"];
                cargo.Activo = (bool)dt.Rows[0]["Activo"];
                return cargo;
            }
            catch (Exception ex)
            {
                cargo.mensaje.CodigoMensaje = 1;
                cargo.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ObtenerCargo], VERIFICAR CONSOLA";
                cargo.mensaje.DescripcionMensajeSistema = ex.Message;
                return cargo;
            }
            finally
            {
                ds.Dispose();
                ds.Clear();
                dt.Dispose();
                dt.Clear();
            }
        }
        public Cargo GuardaCargo(int Gestor, int IdCargo, string NombreCargo, int IdCatalogoTipoCargo, bool Activo, int IdUsuarioAuditoria)
        {
            try
            {
                System.Object[] ParamtrosOutPut = null;
                if (Gestor == DatosGlobales.GestorSqlServer)
                {                    
                    ParamtrosOutPut = ConexionSqlServer.GDatos.Ejecutar(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paGuardarCargo", IdCargo, NombreCargo, IdCatalogoTipoCargo, Activo, IdUsuarioAuditoria, "", 0);
                }
                //if (Gestor == DatosGlobales.GestorOracle)
                //{
                //    ParamtrosOutPut = ConexionOracleServer.GDatos.Ejecutar(DatosGlobales.ListaConexiones.cnGeneralOracle, "General.paGuardarCargo", IdCargo, NombreCargo, Activo, IdUsuarioAuditoria, "", 0);
                //}
                if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
                {
                    cargo.mensaje.CodigoMensaje = 1;
                    cargo.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [GuardaCargo], VERIFICAR CONSOLA";
                    cargo.mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
                    return cargo;
                }
                cargo.IdCargo = Convert.ToInt32(ParamtrosOutPut[0].ToString());
                cargo.mensaje.DescripcionMensaje = ParamtrosOutPut[1].ToString();
                cargo.mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[2].ToString());
                return cargo;
            }
            catch (Exception ex)
            {
                cargo.mensaje.CodigoMensaje = 1;
                cargo.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [GuardaCargo], VERIFICAR CONSOLA";
                cargo.mensaje.DescripcionMensajeSistema = ex.Message;
                return cargo;
            }
        }
        public ListaCargo ListarCargo(int Gestor, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paListarCargo", IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
                }
                //if (Gestor == DatosGlobales.GestorOracle)
                //{
                //    ds = ConexionOracleServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnGeneralOracle, "General.paListarCargo", IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
                //}
                if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                {
                    Lista.mensaje.CodigoMensaje = 1;
                    Lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarCargo], VERIFICAR CONSOLA";
                    Lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
                    return Lista;
                }
                dt = ds.Tables[0].Copy();
                DataTable dtParametros = null;
                dtParametros = ds.Tables[1].Copy();
                Lista.paginacion.TotalRegistros = Convert.ToInt32(dtParametros.Rows[0][0]);
                foreach (DataRow row in dt.Rows)
                {
                    cargo = new Cargo();
                    cargo.IdCargo = (int)row["IdCargo"];
                    cargo.NombreCargo = (string)row["NombreCargo"];
                    cargo.catalogotipocargo.Descripcion = (string)row["CatalogoTipoCargo"];                    
                    cargo.Activo = (bool)row["Activo"];
                    Lista.lista.Add(cargo);
                }
                return Lista;
            }
            catch (Exception ex)
            {
                Lista.mensaje.CodigoMensaje = 1;
                Lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarCargo], VERIFICAR CONSOLA";
                Lista.mensaje.DescripcionMensajeSistema = ex.Message;
                return Lista;
            }
            finally
            {
                ds.Dispose();
                ds.Clear();
                dt.Dispose();
                dt.Clear();
            }
        }
        public ListaCargo ListarComboCargo(int Gestor)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paListarComboCargo");
                }
                
                if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                {
                    Lista.mensaje.CodigoMensaje = 1;
                    Lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarComboCargo], VERIFICAR CONSOLA";
                    Lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
                    return Lista;
                }
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0].Copy();
                }
                if (dt != null)
                {                  
                    foreach (DataRow row in dt.Rows)
                    {
                        cargo = new Cargo();
                        cargo.IdCargo = Convert.ToInt32(row["IdCargo"].ToString());
                        cargo.NombreCargo = row["NombreCargo"].ToString();
                        Lista.lista.Add(cargo);
                    }

                }
                return Lista;
            }
            catch (Exception ex)
            {
                Lista.mensaje.CodigoMensaje = 1;
                Lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarComboCargo], VERIFICAR CONSOLA";
                Lista.mensaje.DescripcionMensajeSistema = ex.Message;
                return Lista;
            }
            finally
            {
                ds.Dispose();
                ds.Clear();
                dt.Dispose();
                dt.Clear();
            }
        }

        //METODOS ASINCRONOS
        public async Task<ListaCargo> ListarComboCargoAsync(int Gestor)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paListarComboCargo");
                }
                
                if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                {
                    Lista.mensaje.CodigoMensaje = 1;
                    Lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarComboCargo], VERIFICAR CONSOLA";
                    Lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
                    return Lista;
                }
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0].Copy();
                }
                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        cargo = new Cargo();
                        cargo.IdCargo = Convert.ToInt32(row["IdCargo"].ToString());
                        cargo.NombreCargo = row["NombreCargo"].ToString();
                        Lista.lista.Add(cargo);
                    }

                }
                return await Task.FromResult(Lista);
            }
            catch (Exception ex)
            {
                Lista.mensaje.CodigoMensaje = 1;
                Lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarComboCargo], VERIFICAR CONSOLA";
                Lista.mensaje.DescripcionMensajeSistema = ex.Message;
                return await Task.FromResult(Lista);
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



