using System;
using System.Runtime.Serialization;
using Utilitarios;
using System.Data;
using System.Collections.Generic;
using General;
using System.Threading.Tasks;

namespace GeneralLogic
{
    
    public class PersonaLogic
    {
        private Persona persona;
        private ListaPersona lista;

        public PersonaLogic()
        {
            persona = new Persona();
            lista = new ListaPersona();
        }
        public Mensaje EliminarPersona(int Gestor, int IdPersona, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();
            if (Gestor == DatosGlobales.GestorSqlServer)
            {
                System.Object[] ParamtrosOutPut = null; 
                ParamtrosOutPut = ConexionSqlServer.GDatos.Ejecutar(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paEliminarPersona", IdPersona, IdUsuarioAuditoria, mensaje.DescripcionMensaje, mensaje.CodigoMensaje);
                if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
                {
                    mensaje.CodigoMensaje = 1;
                    mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA CAPA DE DATOS [EliminarPersona] VERIFICAR CONSOLA";
                    mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
                    return mensaje;
                }
                mensaje.DescripcionMensaje = ParamtrosOutPut[0].ToString();
                mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[1].ToString());
            }
            return mensaje;
        }
        public async Task<Mensaje> EliminarPersonaAsync(int Gestor, int IdPersona, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();
            if (Gestor == DatosGlobales.GestorSqlServer)
            {
                System.Object[] ParamtrosOutPut = null; 
                ParamtrosOutPut = await ConexionSqlServer.GDatos.EjecutarAsync(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paEliminarPersona", IdPersona, IdUsuarioAuditoria, mensaje.DescripcionMensaje, mensaje.CodigoMensaje);
                if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
                {
                    mensaje.CodigoMensaje = 1;
                    mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA CAPA DE DATOS [EliminarPersona] VERIFICAR CONSOLA";
                    mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
                    return mensaje;
                }
                mensaje.DescripcionMensaje = ParamtrosOutPut[0].ToString();
                mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[1].ToString());
            }
            return mensaje;
        }
        public Persona ObtenerPersona(int Gestor, int IdPersona)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (IdPersona == 0)
                {
                    return persona;
                }

                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paObtenerPersona", IdPersona);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        persona.mensaje.CodigoMensaje = 1;
                        persona.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ObtenerPersona], VERIFICAR CONSOLA";
                        persona.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();

                        return persona;
                    }
                    dt = ds.Tables[0].Copy();
                    persona.IdPersona = (int)dt.Rows[0]["IdPersona"];
                    persona.catalogotipopersona.IdCatalogo = (int)dt.Rows[0]["IdCatalogoTipoPersona"];
                    persona.NombreCompleto = (string)dt.Rows[0]["NombreCompleto"];
                    persona.Nombres = (string)dt.Rows[0]["Nombres"];
                    persona.ApellidoPaterno = (string)dt.Rows[0]["ApellidoPaterno"];
                    persona.ApellidoMaterno = (string)dt.Rows[0]["ApellidoMaterno"];
                    persona.catalogotipodocumentopersonal.IdCatalogo = (int)dt.Rows[0]["IdCatalogoTipoDocumentoPersonal"];
                    persona.NumeroDocumento = (string)dt.Rows[0]["NumeroDocumento"];
                    persona.ubigeo.IdUbigeo = (int)dt.Rows[0]["IdUbigeo"];
                    persona.ubigeo.CodigoDepartamento = (int)dt.Rows[0]["CodigoDepartamento"];
                    persona.ubigeo.CodigoProvincia = (int)dt.Rows[0]["CodigoProvincia"];
                    persona.Direccion = (string)dt.Rows[0]["Direccion"];
                    persona.Celular = (string)dt.Rows[0]["Celular"];
                    persona.FechaNacimiento = (string)dt.Rows[0]["FechaNacimiento"];
                    persona.Sexo = (bool)dt.Rows[0]["Sexo"];

                }
                return persona;
            }
            catch (Exception ex)
            {
                persona.mensaje.CodigoMensaje = 1;
                persona.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ObtenerPersona], VERIFICAR CONSOLA";
                persona.mensaje.DescripcionMensajeSistema = ex.Message;
                return persona;
            }
            finally
            {
                ds.Dispose();
                ds.Clear();
                dt.Dispose();
                dt.Clear();
            }
        }
        
        public async Task<Persona> ObtenerPersonaPorDocumento(int Gestor,string NumeroDocumento, int IdCatalogoTipoDocumentoPersonal)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {               

                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paObtenerPersonaPorDocumento", NumeroDocumento, IdCatalogoTipoDocumentoPersonal);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        persona.mensaje.CodigoMensaje = 1;
                        persona.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ObtenerPersona], VERIFICAR CONSOLA";
                        persona.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();

                        return persona;
                    }
                    dt = ds.Tables[0].Copy();
                    if (dt.Rows.Count > 0)
                    {
                        persona.IdPersona = (int)dt.Rows[0]["IdPersona"];
                        persona.catalogotipopersona.IdCatalogo = (int)dt.Rows[0]["IdCatalogoTipoPersona"];
                        persona.NombreCompleto = (string)dt.Rows[0]["NombreCompleto"];
                        persona.Nombres = (string)dt.Rows[0]["Nombres"];
                        persona.ApellidoPaterno = (string)dt.Rows[0]["ApellidoPaterno"];
                        persona.ApellidoMaterno = (string)dt.Rows[0]["ApellidoMaterno"];
                        persona.catalogotipodocumentopersonal.IdCatalogo = (int)dt.Rows[0]["IdCatalogoTipoDocumentoPersonal"];
                        persona.NumeroDocumento = (string)dt.Rows[0]["NumeroDocumento"];
                        persona.FechaNacimiento = (string)dt.Rows[0]["FechaNacimiento"];
                        persona.paisnacimiento.IdPais = (int)dt.Rows[0]["IdPaisNacimiento"];
                        persona.LugarNacimiento = (string)dt.Rows[0]["LugarNacimiento"];
                        persona.Sexo = Convert.ToBoolean(dt.Rows[0]["Sexo"]);
                        persona.catalogotipopersona.Descripcion = (string)dt.Rows[0]["CatalogoTipoPersona"];
                    }
                }
                return persona;
            }
            catch (Exception ex)
            {
                persona.mensaje.CodigoMensaje = 1;
                persona.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ObtenerPersonaPorDocumento], VERIFICAR CONSOLA";
                persona.mensaje.DescripcionMensajeSistema = ex.Message;
                return persona;
            }
            finally
            {
                ds.Dispose();
                ds.Clear();
                dt.Dispose();
                dt.Clear();
            }
        }
        public async Task<Persona> ObtenerPersonaAsync(int Gestor, int IdPersona)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (IdPersona == 0)
                {
                    return persona;
                }

                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paObtenerPersona", IdPersona);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        persona.mensaje.CodigoMensaje = 1;
                        persona.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ObtenerPersona], VERIFICAR CONSOLA";
                        persona.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();

                        return persona;
                    }
                    dt = ds.Tables[0].Copy();
                    if (dt.Rows.Count > 0)
                    {
                        persona.IdPersona = (int)dt.Rows[0]["IdPersona"];
                        persona.catalogotipopersona.IdCatalogo = (int)dt.Rows[0]["IdCatalogoTipoPersona"];
                        persona.NombreCompleto = (string)dt.Rows[0]["NombreCompleto"];
                        persona.Nombres = (string)dt.Rows[0]["Nombres"];
                        persona.ApellidoPaterno = (string)dt.Rows[0]["ApellidoPaterno"];
                        persona.ApellidoMaterno = (string)dt.Rows[0]["ApellidoMaterno"];
                        persona.catalogotipodocumentopersonal.IdCatalogo = (int)dt.Rows[0]["IdCatalogoTipoDocumentoPersonal"];
                        persona.NumeroDocumento = (string)dt.Rows[0]["NumeroDocumento"];
                        persona.ubigeo.IdUbigeo = (int)dt.Rows[0]["IdUbigeo"];
                        persona.ubigeo.CodigoDepartamento = (int)dt.Rows[0]["CodigoDepartamento"];
                        persona.ubigeo.CodigoProvincia = (int)dt.Rows[0]["CodigoProvincia"];
                        persona.Direccion = (string)dt.Rows[0]["Direccion"];
                        persona.Celular = (string)dt.Rows[0]["Celular"];
                        persona.FechaNacimiento = (string)dt.Rows[0]["FechaNacimiento"];
                        persona.Sexo = (bool)dt.Rows[0]["Sexo"];
                        persona.TelefonoFijo = (string)dt.Rows[0]["TelefonoFijo"];
                    }
                }
                return persona;
            }
            catch (Exception ex)
            {
                persona.mensaje.CodigoMensaje = 1;
                persona.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ObtenerPersona], VERIFICAR CONSOLA";
                persona.mensaje.DescripcionMensajeSistema = ex.Message;
                return persona;
            }
            finally
            {
                ds.Dispose();
                ds.Clear();
                dt.Dispose();
                dt.Clear();
            }
        }
        public Persona GuardaPersona(int Gestor, int IdPersona, int IdCatalogoTipoPersona, string NombreCompleto, string Nombres, string ApellidoPaterno, string ApellidoMaterno, int IdCatalogoTipoDocumentoPersonal, string NumeroDocumento, int IdUbigeo, string Direccion,string Celular, string FechaNacimiento, bool Sexo, int IdUsuarioAuditoria)
        {
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    System.Object[] ParamtrosOutPut = null; 
                    ParamtrosOutPut = ConexionSqlServer.GDatos.Ejecutar(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paGuardarPersona", IdPersona, IdCatalogoTipoPersona, NombreCompleto, Nombres, ApellidoPaterno, ApellidoMaterno, IdCatalogoTipoDocumentoPersonal, NumeroDocumento, IdUbigeo, Direccion, Celular, FechaNacimiento, Sexo, IdUsuarioAuditoria, "", 0);
                    if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
                    {
                        persona.mensaje.CodigoMensaje = 1;
                        persona.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [GuardaPersona], VERIFICAR CONSOLA";
                        persona.mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
                        return persona;
                    }
                    persona.IdPersona = Convert.ToInt32(ParamtrosOutPut[0].ToString());
                    persona.mensaje.DescripcionMensaje = ParamtrosOutPut[1].ToString();
                    persona.mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[2].ToString());
                }
                return persona;
            }
            catch (Exception ex)
            {
                persona.mensaje.CodigoMensaje = 1;
                persona.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [GuardaPersona], VERIFICAR CONSOLA";
                persona.mensaje.DescripcionMensajeSistema = ex.Message;
                return persona;
            }
        }
        public async Task<Persona> GuardaPersonaAsync(int Gestor, int IdPersona, int IdCatalogoTipoPersona, string NombreCompleto, string Nombres, string ApellidoPaterno, string ApellidoMaterno, int IdCatalogoTipoDocumentoPersonal, string NumeroDocumento, int IdUbigeo, string Direccion, string Celular, string FechaNacimiento, bool Sexo,string Anexo, int IdUsuarioAuditoria)
        {
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    System.Object[] ParamtrosOutPut = null; 
                    ParamtrosOutPut = await ConexionSqlServer.GDatos.EjecutarAsync(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paGuardarPersona", IdPersona, IdCatalogoTipoPersona, NombreCompleto, Nombres, ApellidoPaterno, ApellidoMaterno, IdCatalogoTipoDocumentoPersonal, NumeroDocumento, IdUbigeo, Direccion, Celular, FechaNacimiento, Sexo, Anexo, IdUsuarioAuditoria, "", 0);
                    if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
                    {
                        persona.mensaje.CodigoMensaje = 1;
                        persona.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [GuardaPersona], VERIFICAR CONSOLA";
                        persona.mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
                        return persona;
                    }
                    persona = await ObtenerPersonaAsync(Gestor, Convert.ToInt32(ParamtrosOutPut[0].ToString()));
                  //  persona.IdPersona = Convert.ToInt32(ParamtrosOutPut[0].ToString());
                    persona.mensaje.DescripcionMensaje = ParamtrosOutPut[1].ToString();
                    persona.mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[2].ToString());
                }
                return persona;
            }
            catch (Exception ex)
            {
                persona.mensaje.CodigoMensaje = 1;
                persona.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [GuardaPersona], VERIFICAR CONSOLA";
                persona.mensaje.DescripcionMensajeSistema = ex.Message;
                return persona;
            }
        }
        public ListaPersona ListarPersona(int Gestor, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paListarPersona", IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarPersona], VERIFICAR CONSOLA";
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
                        return lista;
                    }
                    dt = ds.Tables[0].Copy();
                    DataTable dtParametros = null;
                    dtParametros = ds.Tables[1].Copy();
                    lista.paginacion.TotalRegistros = Convert.ToInt32(dtParametros.Rows[0][0]);
                }
                Persona persona = null;
                foreach (DataRow row in dt.Rows)
                {
                    persona = new Persona();
                    persona.IdPersona = (int)row["IdPersona"];
                    persona.catalogotipopersona.Descripcion = (string)row["CatalogoTipoPersona"];
                    persona.NombreCompleto = (string)row["NombreCompleto"];                   
                    persona.catalogotipodocumentopersonal.Descripcion = (string)row["CatalogoTipoDocumentoPersonal"];
                    persona.NumeroDocumento = (string)row["NumeroDocumento"];
                    persona.ubigeo.Departamento = (string)row["Departamento"];
                    persona.ubigeo.Provincia = (string)row["Provincia"];
                    persona.ubigeo.Distrito = (string)row["Distrito"];
                    persona.Direccion = (string)row["Direccion"];
                    persona.Celular =  (string)row["Celular"];
                    lista.lista.Add(persona);
                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarPersona], VERIFICAR CONSOLA";
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
        public async Task<ListaPersona> ListarPersonaAsync(int Gestor, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paListarPersona", IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarPersona], VERIFICAR CONSOLA";
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
                        return lista;
                    }
                    dt = ds.Tables[0].Copy();
                    DataTable dtParametros = null;
                    dtParametros = ds.Tables[1].Copy();
                    lista.paginacion.TotalRegistros = Convert.ToInt32(dtParametros.Rows[0][0]);
                }
                Persona persona = null;
                foreach (DataRow row in dt.Rows)
                {
                    persona = new Persona();
                    persona.IdPersona = (int)row["IdPersona"];
                    persona.catalogotipopersona.Descripcion = (string)row["CatalogoTipoPersona"];
                    persona.NombreCompleto = (string)row["NombreCompleto"];
                    persona.catalogotipodocumentopersonal.Descripcion = (string)row["CatalogoTipoDocumentoPersonal"];
                    persona.NumeroDocumento = (string)row["NumeroDocumento"];
                    persona.ubigeo.Departamento = (string)row["Departamento"];
                    persona.ubigeo.Provincia = (string)row["Provincia"];
                    persona.ubigeo.Distrito = (string)row["Distrito"];
                    persona.Direccion = (string)row["Direccion"];
                    persona.Celular = (string)row["Celular"];
                    lista.lista.Add(persona);
                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarPersona], VERIFICAR CONSOLA";
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

        public ListaPersona ListarPersonaPorAutoComplete(int Gestor, string NombreCompleto)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paListarPersonaPorAutoComplete", NombreCompleto);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarPersona], VERIFICAR CONSOLA";
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
                        return lista;
                    }
                    dt = ds.Tables[0].Copy();
                }
                Persona persona = null;
                foreach (DataRow row in dt.Rows)
                {
                    persona = new Persona();
                    persona.IdPersona = (int)row["IdPersona"];
                    persona.NombreCompleto = (string)row["NombreCompleto"];
                    lista.lista.Add(persona);
                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarPersona], VERIFICAR CONSOLA";
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
        
        public async Task<ListaPersona> ListarComboAutocompleteMesaParte(int Gestor, string NombreCompleto)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paListarComboAutocompleteMesaParte", NombreCompleto);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarPersona], VERIFICAR CONSOLA";
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
                        return lista;
                    }
                    dt = ds.Tables[0].Copy();
                }
                foreach (DataRow row in dt.Rows)
                {
                    persona = new Persona();
                    persona.IdPersona = (int)row["IdPersona"];
                    persona.NombreCompleto = (string)row["NombreCompleto"];
                    lista.lista.Add(persona);
                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarPersona], VERIFICAR CONSOLA";
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
        public async Task<ListaPersona> ListarPersonaPorAutoCompleteAsync(int Gestor, string NombreCompleto)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paListarPersonaPorAutoComplete", NombreCompleto);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarPersona], VERIFICAR CONSOLA";
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
                        return lista;
                    }
                    dt = ds.Tables[0].Copy();
                }
                foreach (DataRow row in dt.Rows)
                {
                    persona = new Persona();
                    persona.IdPersona = (int)row["IdPersona"];
                    persona.NombreCompleto = (string)row["NombreCompleto"];
                    lista.lista.Add(persona);
                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarPersona], VERIFICAR CONSOLA";
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

        public async Task<ListaPersona> ListarPersonaBuscarSISAsync(int Gestor, int IdPersona)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paListarPersonaBuscarSIS", IdPersona);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarPersona], VERIFICAR CONSOLA";
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
                        return lista;
                    }
                    dt = ds.Tables[0].Copy();
                }
                foreach (DataRow row in dt.Rows)
                {
                    persona = new Persona();
                    persona.NumeroDocumento = (string)row["NumeroDocumento"];
                    persona.TipoDocumento = (int)row["TipoDocumento"];
                    lista.lista.Add(persona);
                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarPersona], VERIFICAR CONSOLA";
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



        public ListaPersona ListarPersonaJuridicaPorAutoComplete(int Gestor, string NombreCompleto)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paListarPersonaJuridicaPorAutoComplete", NombreCompleto);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarPersona], VERIFICAR CONSOLA";
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
                        return lista;
                    }
                    dt = ds.Tables[0].Copy();
                }
                Persona persona = null;
                foreach (DataRow row in dt.Rows)
                {
                    persona = new Persona();
                    persona.IdPersona = (int)row["IdPersona"];
                    persona.NombreCompleto = (string)row["NombreCompleto"];
                    lista.lista.Add(persona);
                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarPersona], VERIFICAR CONSOLA";
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
        public async Task<ListaPersona> ListarPersonaJuridicaPorAutoCompleteAsync(int Gestor, string NombreCompleto)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paListarPersonaJuridicaPorAutoComplete", NombreCompleto);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarPersona], VERIFICAR CONSOLA";
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
                        return lista;
                    }
                    dt = ds.Tables[0].Copy();
                }
                Persona persona = null;
                foreach (DataRow row in dt.Rows)
                {
                    persona = new Persona();
                    persona.IdPersona = (int)row["IdPersona"];
                    persona.NombreCompleto = (string)row["NombreCompleto"];
                    lista.lista.Add(persona);
                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarPersona], VERIFICAR CONSOLA";
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
        public ListaPersona ListarPersonaNaturalPorAutoComplete(int Gestor, string NombreCompleto)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paListarPersonaNaturalPorAutoComplete", NombreCompleto);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarPersona], VERIFICAR CONSOLA";
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
                        return lista;
                    }
                    dt = ds.Tables[0].Copy();
                }
                Persona persona = null;
                foreach (DataRow row in dt.Rows)
                {
                    persona = new Persona();
                    persona.IdPersona = (int)row["IdPersona"];
                    persona.NombreCompleto = (string)row["NombreCompleto"];
                    lista.lista.Add(persona);
                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarPersona], VERIFICAR CONSOLA";
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
        public async Task<ListaPersona> ListarPersonaNaturalPorAutoCompleteAsync(int Gestor, string NombreCompleto)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paListarPersonaNaturalPorAutoComplete", NombreCompleto);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarPersona], VERIFICAR CONSOLA";
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
                        return lista;
                    }
                    dt = ds.Tables[0].Copy();
                }
                Persona persona = null;
                foreach (DataRow row in dt.Rows)
                {
                    persona = new Persona();
                    persona.IdPersona = (int)row["IdPersona"];
                    persona.NombreCompleto = (string)row["NombreCompleto"];
                    lista.lista.Add(persona);
                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarPersona], VERIFICAR CONSOLA";
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



