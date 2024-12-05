using System;
using System.Runtime.Serialization;
using Utilitarios;
using System.Data;
using System.Collections.Generic;
using Sistema;
namespace SistemaLogic
{    
    public class OpcionFormularioLogic
    {
        private OpcionFormulario opcionformulario;
        private ListaOpcionFormulario lista;

        public OpcionFormularioLogic()
        {
            opcionformulario = new OpcionFormulario();
            lista = new ListaOpcionFormulario();
        }
        public Mensaje EliminarOpcionFormulario(int Gestor, int IdOpcionFormulario, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();
            if (Gestor == DatosGlobales.GestorSqlServer)
            {
                System.Object[] ParamtrosOutPut = null; 
                ParamtrosOutPut = ConexionSqlServer.GDatos.Ejecutar(DatosGlobales.ListaConexiones.cnSistemaSql, "Sistema.paEliminarOpcionFormulario", IdOpcionFormulario, IdUsuarioAuditoria, mensaje.DescripcionMensaje, mensaje.CodigoMensaje);
                if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
                {
                    mensaje.CodigoMensaje = 1;
                    mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA CAPA DE DATOS [EliminarOpcionFormulario] VERIFICAR CONSOLA";
                    mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
                    return mensaje;
                }
                mensaje.DescripcionMensaje = ParamtrosOutPut[0].ToString();
                mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[1].ToString());
            }
            return mensaje;
        }

        public OpcionFormulario ObtenerOpcionFormulario(int Gestor, int IdOpcion)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {

                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnSistemaSql, "Sistema.paObtenerOpcionFormulario", IdOpcion);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        opcionformulario.mensaje.CodigoMensaje = 1;
                        opcionformulario.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ObtenerOpcionFormulario], VERIFICAR CONSOLA";
                        opcionformulario.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();

                        return opcionformulario;
                    }
                    dt = ds.Tables[0].Copy();
                    if (dt.Rows.Count == 0)
                    {
                        return opcionformulario;
                    }
                    opcionformulario.IdOpcionFormulario = (int)dt.Rows[0]["IdOpcionFormulario"];
                    opcionformulario.opcion.IdOpcion = (int)dt.Rows[0]["IdOpcion"];
                    opcionformulario.NombreFormulario = (string)dt.Rows[0]["NombreFormulario"];
                    opcionformulario.RutaFormulario = (string)dt.Rows[0]["RutaFormulario"];
                    opcionformulario.Controlador = (string)dt.Rows[0]["Controlador"];
                    opcionformulario.Accion = (string)dt.Rows[0]["Accion"];
                    opcionformulario.Parametros = (string)dt.Rows[0]["Parametros"];
                    opcionformulario.Ver = (bool)dt.Rows[0]["Ver"];
                    opcionformulario.Nuevo = (bool)dt.Rows[0]["Nuevo"];
                    opcionformulario.Editar = (bool)dt.Rows[0]["Editar"];
                    opcionformulario.Guardar = (bool)dt.Rows[0]["Guardar"];
                    opcionformulario.Eliminar = (bool)dt.Rows[0]["Eliminar"];
                    opcionformulario.Imprimir = (bool)dt.Rows[0]["Imprimir"];
                    opcionformulario.Exportar = (bool)dt.Rows[0]["Exportar"];
                    opcionformulario.VistaPrevia = (bool)dt.Rows[0]["VistaPrevia"];
                    opcionformulario.Consultar = (bool)dt.Rows[0]["Consultar"];
                    opcionformulario.Activo = (bool)dt.Rows[0]["Activo"];

                }
                return opcionformulario;
            }
            catch (Exception ex)
            {
                opcionformulario.mensaje.CodigoMensaje = 1;
                opcionformulario.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ObtenerOpcionFormulario], VERIFICAR CONSOLA";
                opcionformulario.mensaje.DescripcionMensajeSistema = ex.Message;
                return opcionformulario;
            }
            finally
            {
                ds.Dispose();
                ds.Clear();
                dt.Dispose();
                dt.Clear();
            }
        }


        public OpcionFormulario GuardaOpcionFormulario(int Gestor, int IdOpcionFormulario, int IdOpcion, string NombreFormulario, string RutaFormulario, string Controlador, string Accion, string Parametros, bool Ver, bool Nuevo, bool Editar, bool Guardar, bool Eliminar, bool Imprimir, bool Exportar, bool VistaPrevia, bool Consultar, bool Activo, int IdUsuarioAuditoria)
        {
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    System.Object[] ParamtrosOutPut = null; 
                    ParamtrosOutPut = ConexionSqlServer.GDatos.Ejecutar(DatosGlobales.ListaConexiones.cnSistemaSql, "Sistema.paGuardarOpcionFormulario", IdOpcionFormulario, IdOpcion, NombreFormulario, RutaFormulario, Controlador, Accion, Parametros, Ver, Nuevo, Editar, Guardar, Eliminar, Imprimir, Exportar, VistaPrevia, Consultar, Activo, IdUsuarioAuditoria, "", 0);
                    if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
                    {
                        opcionformulario.mensaje.CodigoMensaje = 1;
                        opcionformulario.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [GuardaOpcionFormulario], VERIFICAR CONSOLA";
                        opcionformulario.mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
                        return opcionformulario;
                    }
                    opcionformulario.IdOpcionFormulario = Convert.ToInt32(ParamtrosOutPut[0].ToString());
                    opcionformulario.mensaje.DescripcionMensaje = ParamtrosOutPut[1].ToString();
                    opcionformulario.mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[2].ToString());
                }
                return opcionformulario;
            }
            catch (Exception ex)
            {
                opcionformulario.mensaje.CodigoMensaje = 1;
                opcionformulario.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [GuardaOpcionFormulario], VERIFICAR CONSOLA";
                opcionformulario.mensaje.DescripcionMensajeSistema = ex.Message;
                return opcionformulario;
            }
        }



        public ListaOpcionFormulario ListarOpcionFormulario(int Gestor, int IdOpcion, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnSistemaSql, "Sistema.paListarOpcionFormulario", IdOpcion, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarOpcionFormulario], VERIFICAR CONSOLA";
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
                        return lista;
                    }
                    dt = ds.Tables[0].Copy();
                    DataTable dtParametros = null;
                    dtParametros = ds.Tables[1].Copy();
                    lista.paginacion.TotalRegistros = Convert.ToInt32(dtParametros.Rows[0][0]);
                }
                OpcionFormulario opcionformulario = null;
                foreach (DataRow row in dt.Rows)
                {
                    opcionformulario = new OpcionFormulario();
                    opcionformulario.IdOpcionFormulario = (int)row["IdOpcionFormulario"];
                    opcionformulario.opcion.IdOpcion = (int)row["IdOpcion"];
                    opcionformulario.NombreFormulario = (string)row["NombreFormulario"];
                    opcionformulario.RutaFormulario = (string)row["RutaFormulario"];
                    opcionformulario.Controlador = (string)row["Controlador"];
                    opcionformulario.Accion = (string)row["Accion"];
                    opcionformulario.Parametros = (string)row["Parametros"];
                    opcionformulario.Ver = (bool)row["Ver"];
                    opcionformulario.Nuevo = (bool)row["Nuevo"];
                    opcionformulario.Editar = (bool)row["Editar"];
                    opcionformulario.Guardar = (bool)row["Guardar"];
                    opcionformulario.Eliminar = (bool)row["Eliminar"];
                    opcionformulario.Imprimir = (bool)row["Imprimir"];
                    opcionformulario.Exportar = (bool)row["Exportar"];
                    opcionformulario.VistaPrevia = (bool)row["VistaPrevia"];
                    opcionformulario.Consultar = (bool)row["Consultar"];
                    opcionformulario.Activo = (bool)row["Activo"];

                    lista.lista.Add(opcionformulario);
                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarOpcionFormulario], VERIFICAR CONSOLA";
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



