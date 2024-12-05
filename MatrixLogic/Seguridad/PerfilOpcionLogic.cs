using System;
using Utilitarios;
using System.Data;
using System.Collections.Generic;
using Seguridad;
using System.Threading.Tasks;

namespace SeguridadLogic
{    
    public class PerfilOpcionLogic
    {
        private Seguridad.PerfilOpcion perfilopcion;
        public PerfilOpcionLogic()
        {
            perfilopcion = new Seguridad.PerfilOpcion();
        }
        public Mensaje EliminarPerfilOpcion(int Gestor, int IdPerfilOpcion, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();
            if (Gestor == DatosGlobales.GestorSqlServer)
            {
                System.Object[] ParamtrosOutPut = null; 
                ParamtrosOutPut = ConexionSqlServer.GDatos.Ejecutar(DatosGlobales.ListaConexiones.cnSeguridadSql,"Seguridad.paEliminarPerfilOpcion", IdPerfilOpcion, IdUsuarioAuditoria, mensaje.DescripcionMensaje, mensaje.CodigoMensaje);
                mensaje.DescripcionMensaje = ParamtrosOutPut[0].ToString();
                mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[1].ToString());
            }
            return mensaje;
        }

        public PerfilOpcion ObtenerPerfilOpcion(int Gestor, int IdPerfilOpcion)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (IdPerfilOpcion == 0)
                {
                    return perfilopcion;
                }

                if (Gestor == DatosGlobales.GestorSqlServer)
                {

                     ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnSeguridadSql,"Seguridad.paObtenerPerfilOpcion", IdPerfilOpcion);
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0].Copy();
                        if (dt.Rows.Count > 0)
                        {
                            perfilopcion.IdPerfilOpcion = (int)dt.Rows[0]["IdPerfilOpcion"];
                            perfilopcion.perfil.IdPerfil = (int)dt.Rows[0]["IdPerfil"];
                            perfilopcion.opcion.IdOpcionPadre = (int)dt.Rows[0]["IdOpcionPadre"];
                            perfilopcion.opcion.IdOpcion = (int)dt.Rows[0]["IdOpcion"];
                            perfilopcion.Ver = (bool)dt.Rows[0]["Ver"];
                            perfilopcion.Nuevo = (bool)dt.Rows[0]["Nuevo"];
                            perfilopcion.Editar = (bool)dt.Rows[0]["Editar"];
                            perfilopcion.Guardar = (bool)dt.Rows[0]["Guardar"];
                            perfilopcion.Eliminar = (bool)dt.Rows[0]["Eliminar"];
                            perfilopcion.Imprimir = (bool)dt.Rows[0]["Imprimir"];
                            perfilopcion.Exportar = (bool)dt.Rows[0]["Exportar"];
                            perfilopcion.VistaPrevia = (bool)dt.Rows[0]["VistaPrevia"];
                            perfilopcion.Consultar = (bool)dt.Rows[0]["Consultar"];

                            perfilopcion.Ver0 = (bool)dt.Rows[0]["Ver0"];
                            perfilopcion.Nuevo0 = (bool)dt.Rows[0]["Nuevo0"];
                            perfilopcion.Editar0 = (bool)dt.Rows[0]["Editar0"];
                            perfilopcion.Guardar0 = (bool)dt.Rows[0]["Guardar0"];
                            perfilopcion.Eliminar0 = (bool)dt.Rows[0]["Eliminar0"];
                            perfilopcion.Imprimir0 = (bool)dt.Rows[0]["Imprimir0"];
                            perfilopcion.Exportar0 = (bool)dt.Rows[0]["Exportar0"];
                            perfilopcion.VistaPrevia0 = (bool)dt.Rows[0]["VistaPrevia0"];
                            perfilopcion.Consultar0 = (bool)dt.Rows[0]["Consultar0"];

                        }
                    }
                }
                return perfilopcion;
            }
            catch (Exception ex)
            {
                perfilopcion.mensaje.CodigoMensaje = 1;
                perfilopcion.mensaje.DescripcionMensaje = ex.Message;
                return perfilopcion;
            }
            finally
            {
                ds.Dispose();
                ds.Clear();
                dt.Dispose();
                dt.Clear();
            }
        }


        public PerfilOpcion GuardaPerfilOpcion(int Gestor, int IdPerfilOpcion, int IdPerfil, int IdOpcionPadre, int IdOpcion, bool Ver, bool Nuevo, bool Editar, bool Guardar, bool Eliminar, bool Imprimir, bool Exportar, bool VistaPrevia, bool Consultar, int IdUsuarioAuditoria)
        {
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    System.Object[] ParamtrosOutPut = null; 
                    ParamtrosOutPut = ConexionSqlServer.GDatos.Ejecutar(DatosGlobales.ListaConexiones.cnSeguridadSql, "Seguridad.paGuardarPerfilOpcion", IdPerfilOpcion, IdPerfil,  IdOpcion, IdOpcionPadre, Ver, Nuevo, Editar, Guardar, Eliminar, Imprimir, Exportar, VistaPrevia, Consultar, IdUsuarioAuditoria, "", 0);
                    if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
                    {
                        perfilopcion.mensaje.CodigoMensaje = 1;
                        perfilopcion.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [GuardaPerfilOpcion], VERIFICAR CONSOLA";
                        perfilopcion.mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
                        return perfilopcion;
                    }
                    perfilopcion.IdPerfilOpcion = Convert.ToInt32(ParamtrosOutPut[0].ToString());
                    perfilopcion.mensaje.DescripcionMensaje = ParamtrosOutPut[1].ToString();
                    perfilopcion.mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[2].ToString());
                }
                return perfilopcion;
            }
            catch (Exception ex)
            {
                perfilopcion.mensaje.CodigoMensaje = 1;
                perfilopcion.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [GuardaPerfilOpcion], VERIFICAR CONSOLA";
                perfilopcion.mensaje.DescripcionMensajeSistema = ex.Message;
                return perfilopcion;
            }
        }



        public ListaPerfilOpcion ListarMenuPorUsuario(int Gestor, int IdUsuario, int IdEmpleadoPerfil)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ListaPerfilOpcion lista = new ListaPerfilOpcion();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {

                     ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnSeguridadSql, "Seguridad.paListarMenuPorUsuario", IdUsuario, IdEmpleadoPerfil);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
                        lista.mensaje.DescripcionMensaje = "SUCEDIO UN ERROR EN LA CAPA DE DATOS [ListarMenuPorUsuario], VERIFIQUE CONSOLA";
                        return lista;
                    }
                    dt = ds.Tables[0].Copy();

                }
                foreach (DataRow row in dt.Rows)
                {
                    perfilopcion = new PerfilOpcion();
                    perfilopcion.opcion.modulo.NombreModulo = (string)row["NombreModulo"];
                    perfilopcion.opcion.modulo.IdModulo = (int)row["IdModulo"];
                    perfilopcion.opcion.modulo.RutaImagenModulo = (string)row["RutaImagenModulo"];
                    perfilopcion.opcion.NombreOpcion = (string)row["NombreOpcion"];
                    perfilopcion.opcion.catalogotipoopcion.IdCatalogo = (int)row["IdCatalogoTipoOpcion"];
                    perfilopcion.opcion.RutaImagenOpcion = (string)row["RutaImagenOpcion"];
                    perfilopcion.opcion.NombreFormulario = (string)row["NombreFormulario"];
                    perfilopcion.opcion.RutaFormulario = (string)row["RutaFormulario"];
                    perfilopcion.opcion.IdOpcion = (int)row["IdOpcion"];

                    perfilopcion.opcion.IdOpcionPadre = (int)row["IdOpcionPadre"];
                    perfilopcion.Ver = (bool)row["Ver"];
                    perfilopcion.Nuevo = (bool)row["Nuevo"];
                    perfilopcion.Editar = (bool)row["Editar"];
                    perfilopcion.Guardar = (bool)row["Guardar"];
                    perfilopcion.Eliminar = (bool)row["Eliminar"];
                    perfilopcion.Imprimir = (bool)row["Imprimir"];
                    perfilopcion.Exportar = (bool)row["Exportar"];
                    perfilopcion.VistaPrevia = (bool)row["VistaPrevia"];
                    perfilopcion.Consultar = (bool)row["Consultar"];

                    perfilopcion.opcion.modulo.OrdenModulo = (int)row["OrdenModulo"];
                    perfilopcion.opcion.OrdenOpcion = (int)row["OrdenOpcion"];
                    perfilopcion.opcion.Controlador = (string)row["Controlador"];
                    perfilopcion.opcion.Accion = (string)row["Accion"];
                    perfilopcion.opcion.Parametros = (string)row["Parametros"];
                    lista.lista.Add(perfilopcion);
                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensajeSistema = ex.Message;
                lista.mensaje.DescripcionMensaje = "SUCEDIO UN ERROR EN LA CAPA LOGICA [ListarMenuPorUsuario], VERIFIQUE CONSOLA";
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
        public async Task<ListaPerfilOpcion> ListarMenuPorUsuarioAsync(int Gestor, int IdUsuario, int IdEmpleadoPerfil)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ListaPerfilOpcion lista = new ListaPerfilOpcion();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {

                     ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnSeguridadSql, "Seguridad.paListarMenuPorUsuario", IdUsuario, IdEmpleadoPerfil);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
                        lista.mensaje.DescripcionMensaje = "SUCEDIO UN ERROR EN LA CAPA DE DATOS [ListarMenuPorUsuario], VERIFIQUE CONSOLA";
                        return lista;
                    }
                    dt = ds.Tables[0].Copy();

                }
                foreach (DataRow row in dt.Rows)
                {
                    perfilopcion = new PerfilOpcion();
                    perfilopcion.opcion.modulo.NombreModulo = (string)row["NombreModulo"];
                    perfilopcion.opcion.modulo.IdModulo = (int)row["IdModulo"];
                    perfilopcion.opcion.modulo.RutaImagenModulo = (string)row["RutaImagenModulo"];
                    perfilopcion.opcion.NombreOpcion = (string)row["NombreOpcion"];
                    perfilopcion.opcion.catalogotipoopcion.IdCatalogo = (int)row["IdCatalogoTipoOpcion"];
                    perfilopcion.opcion.RutaImagenOpcion = row["RutaImagenOpcion"] is System.DBNull ? "" : (string)row["RutaImagenOpcion"];
                    
                    perfilopcion.opcion.NombreFormulario = (string)row["NombreFormulario"];
                    perfilopcion.opcion.RutaFormulario = (string)row["RutaFormulario"];
                    perfilopcion.opcion.IdOpcion = (int)row["IdOpcion"];
                    perfilopcion.opcion.modulo.NuevoModulo = (bool)row["NuevoModulo"];
                    perfilopcion.opcion.IdOpcionPadre = (int)row["IdOpcionPadre"];
                    perfilopcion.Ver = (bool)row["Ver"];
                    perfilopcion.Nuevo = (bool)row["Nuevo"];
                    perfilopcion.Editar = (bool)row["Editar"];
                    perfilopcion.Guardar = (bool)row["Guardar"];
                    perfilopcion.Eliminar = (bool)row["Eliminar"];
                    perfilopcion.Imprimir = (bool)row["Imprimir"];
                    perfilopcion.Exportar = (bool)row["Exportar"];
                    perfilopcion.VistaPrevia = (bool)row["VistaPrevia"];
                    perfilopcion.Consultar = (bool)row["Consultar"];

                    perfilopcion.opcion.modulo.OrdenModulo = (int)row["OrdenModulo"];
                    perfilopcion.opcion.OrdenOpcion = (int)row["OrdenOpcion"];
                    perfilopcion.opcion.Controlador = (string)row["Controlador"];
                    perfilopcion.opcion.Accion = (string)row["Accion"];
                    perfilopcion.opcion.Parametros = (string)row["Parametros"];
                    lista.lista.Add(perfilopcion);
                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensajeSistema = ex.Message;
                lista.mensaje.DescripcionMensaje = "SUCEDIO UN ERROR EN LA CAPA LOGICA [ListarMenuPorUsuario], VERIFIQUE CONSOLA";
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
        public ListaPerfilOpcion ListarPerfilOpcion(int Gestor, int IdPerfil, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ListaPerfilOpcion lista = new ListaPerfilOpcion();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {

                     ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnSeguridadSql,"Seguridad.paListarPerfilOpcion", IdPerfil, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
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
                    foreach (DataRow row in dt.Rows)
                    {
                        perfilopcion = new PerfilOpcion();
                        perfilopcion.IdPerfilOpcion = (int)row["IdPerfilOpcion"];
                        perfilopcion.perfil.IdPerfil = (int)row["IdPerfil"];

                        perfilopcion.opcion.IdOpcion = (int)row["IdOpcion"];
                        perfilopcion.opcion.NombreOpcion = (string)row["NombreOpcion"];
                        perfilopcion.opcion.NombreOpcionPadre = (string)row["NombreOpcionPadre"];
                        perfilopcion.opcion.modulo.NombreModulo = (string)row["NombreModulo"];
                        perfilopcion.opcion.catalogotipoopcion.Descripcion = (string)row["CatalogoTipoOpcion"];
                        perfilopcion.opcion.catalogotipoopcion.IdCatalogo = (int)row["IdCatalogoTipoOpcion"];

                        perfilopcion.Ver = (bool)row["Ver"];
                        perfilopcion.Nuevo = (bool)row["Nuevo"];
                        perfilopcion.Editar = (bool)row["Editar"];
                        perfilopcion.Guardar = (bool)row["Guardar"];
                        perfilopcion.Eliminar = (bool)row["Eliminar"];
                        perfilopcion.Imprimir = (bool)row["Imprimir"];
                        perfilopcion.Exportar = (bool)row["Exportar"];
                        perfilopcion.VistaPrevia = (bool)row["VistaPrevia"];
                        perfilopcion.Consultar = (bool)row["Consultar"];

                        perfilopcion.Ver0 = (bool)dt.Rows[0]["Ver0"];
                        perfilopcion.Nuevo0 = (bool)dt.Rows[0]["Nuevo0"];
                        perfilopcion.Editar0 = (bool)dt.Rows[0]["Editar0"];
                        perfilopcion.Guardar0 = (bool)dt.Rows[0]["Guardar0"];
                        perfilopcion.Eliminar0 = (bool)dt.Rows[0]["Eliminar0"];
                        perfilopcion.Imprimir0 = (bool)dt.Rows[0]["Imprimir0"];
                        perfilopcion.Exportar0 = (bool)dt.Rows[0]["Exportar0"];
                        perfilopcion.VistaPrevia0 = (bool)dt.Rows[0]["VistaPrevia0"];
                        perfilopcion.Consultar0 = (bool)dt.Rows[0]["Consultar0"];

                        lista.lista.Add(perfilopcion);
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

    }  
}

	

		