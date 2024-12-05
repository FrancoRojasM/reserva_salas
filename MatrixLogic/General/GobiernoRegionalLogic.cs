		using System;
		using Utilitarios;
		using System.Data;
		using System.Threading.Tasks;
		using General;
		namespace GeneralLogic
		{	
				
			public class GobiernoRegionalLogic
			{
			private GobiernoRegional gobiernoregional;
			private ListaGobiernoRegional lista;

			public GobiernoRegionalLogic()
			{
				gobiernoregional = new GobiernoRegional();
				lista = new ListaGobiernoRegional();
			}
			
			public async Task<Mensaje> EliminarGobiernoRegionalAsync (int Gestor, int IdGobiernoRegional,int IdUsuarioAuditoria )
			{
				Mensaje mensaje = new Mensaje();			
				if (Gestor == DatosGlobales.GestorSqlServer)
				{
					System.Object[] ParamtrosOutPut = null;				
						ParamtrosOutPut =await ConexionSqlServer.GDatos.EjecutarAsync(DatosGlobales.ListaConexiones.cnGeneralSql,"General.paEliminarGobiernoRegional",IdGobiernoRegional,IdUsuarioAuditoria,mensaje.DescripcionMensaje,mensaje.CodigoMensaje);
						if (Convert.ToInt32(ParamtrosOutPut[100].ToString())>0)
						{
							mensaje.CodigoMensaje = 1;
							mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA CAPA DE DATOS [EliminarGobiernoRegional] VERIFICAR CONSOLA";
							mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
							return mensaje;
						}
						mensaje.DescripcionMensaje = ParamtrosOutPut[0].ToString();
						mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[1].ToString());					
				}
				return mensaje;
			}	
			public async Task<GobiernoRegional>  ObtenerGobiernoRegionalAsync (int Gestor, int IdGobiernoRegional, int IdUsuarioAuditoria)
			{		
				DataSet ds = new DataSet(); 
				DataTable dt = new DataTable(); 		   
				try
				{					          
					if (Gestor == DatosGlobales.GestorSqlServer)
					{
						ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnGeneralSql,"General.paObtenerGobiernoRegional", IdGobiernoRegional,IdUsuarioAuditoria );
						 if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
						 {
							gobiernoregional.mensaje.CodigoMensaje =1;
							gobiernoregional.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ObtenerGobiernoRegional], VERIFICAR CONSOLA";
							gobiernoregional.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
                     
							return gobiernoregional;      
						}
						dt = ds.Tables[0].Copy(); 
														gobiernoregional.IdGobiernoRegional = (int)dt.Rows[0]["IdGobiernoRegional"];		
																	gobiernoregional.NombreGobiernoRegional = (string)dt.Rows[0]["NombreGobiernoRegional"];		
																								gobiernoregional.region.IdRegion = (int)dt.Rows[0]["IdRegion"];		
																												
					} 		
					return gobiernoregional;            
				}
				catch (Exception ex)
				{
					gobiernoregional.mensaje.CodigoMensaje = 1;
					gobiernoregional.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ObtenerGobiernoRegional], VERIFICAR CONSOLA";
					gobiernoregional.mensaje.DescripcionMensajeSistema = ex.Message;
					return gobiernoregional;
				}
				 finally
				{
					ds.Dispose();
					ds.Clear();
					dt.Dispose();
					dt.Clear();
				}
			}
		public async Task<GobiernoRegional> GuardaGobiernoRegionalAsync(int Gestor, int IdGobiernoRegional,string NombreGobiernoRegional,int IdRegion,int IdUsuarioAuditoria)
        { 
			try
			{            
				if (Gestor == DatosGlobales.GestorSqlServer)
				{
					System.Object[] ParamtrosOutPut = null;				
					ParamtrosOutPut =await ConexionSqlServer.GDatos.EjecutarAsync(DatosGlobales.ListaConexiones.cnGeneralSql,"General.paGuardarGobiernoRegional",  IdGobiernoRegional, NombreGobiernoRegional, IdRegion,IdUsuarioAuditoria,"",0);					
					if (Convert.ToInt32(ParamtrosOutPut[100].ToString())>0)
					{
						gobiernoregional.mensaje.CodigoMensaje = 1;
						gobiernoregional.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [GuardaGobiernoRegional], VERIFICAR CONSOLA";
						gobiernoregional.mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
						return gobiernoregional;
					}
					gobiernoregional.IdGobiernoRegional = Convert.ToInt32(ParamtrosOutPut[0].ToString());
					gobiernoregional.mensaje.DescripcionMensaje = ParamtrosOutPut[1].ToString();
					gobiernoregional.mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[2].ToString());
				}
				return gobiernoregional;
			}
			catch (Exception ex)
			{
				gobiernoregional.mensaje.CodigoMensaje = 1;
				gobiernoregional.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [GuardaGobiernoRegional], VERIFICAR CONSOLA";
				gobiernoregional.mensaje.DescripcionMensajeSistema = ex.Message;
				return gobiernoregional;
			}
        }
				
		public async Task<ListaGobiernoRegional> ListarGobiernoRegionalAsync(int Gestor, int IdUsuarioAuditoria,  string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
            {                
			 DataSet ds = new DataSet(); 
             DataTable dt = new DataTable();  
			try
				{                
				if (Gestor == DatosGlobales.GestorSqlServer)
				{      
					ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnGeneralSql,"General.paListarGobiernoRegional",IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
					 if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
					 {
							lista.mensaje.CodigoMensaje =1;
							lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarGobiernoRegional], VERIFICAR CONSOLA";
							lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
							return lista;      
					}					
					dt = ds.Tables[0].Copy();
					DataTable dtParametros = null;
					dtParametros = ds.Tables[1].Copy();                        
					lista.paginacion.TotalRegistros = Convert.ToInt32(dtParametros.Rows[0][0]);
				}
                foreach (DataRow row in dt.Rows)
                {
				gobiernoregional = new GobiernoRegional();
											gobiernoregional.IdGobiernoRegional = (int)row["IdGobiernoRegional"];			
															gobiernoregional.NombreGobiernoRegional = (string)row["NombreGobiernoRegional"];			
																								gobiernoregional.region.IdRegion = (int)row["IdRegion"];		
																	              
			    lista.lista.Add(gobiernoregional);              
                }
				 return lista;
			}
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarGobiernoRegional], VERIFICAR CONSOLA";
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

	public async Task<ListaGobiernoRegional> ListarComboGobiernoRegional(int Gestor, int IdUsuarioAuditoria)
	{
		DataSet ds = new DataSet();
		DataTable dt = new DataTable();
		try
		{
			if (Gestor == DatosGlobales.GestorSqlServer)
			{
				ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paListarComboGobiernoRegional", IdUsuarioAuditoria);
				if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
				{
					lista.mensaje.CodigoMensaje = 1;
					lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarGobiernoRegional], VERIFICAR CONSOLA";
					lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
					return lista;
				}
				dt = ds.Tables[0].Copy();				
			}
			foreach (DataRow row in dt.Rows)
			{
				gobiernoregional = new GobiernoRegional();
				gobiernoregional.IdGobiernoRegional = (int)row["IdGobiernoRegional"];
				gobiernoregional.NombreGobiernoRegional = (string)row["NombreGobiernoRegional"];
				lista.lista.Add(gobiernoregional);
			}
			return lista;
		}
		catch (Exception ex)
		{
			lista.mensaje.CodigoMensaje = 1;
			lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarGobiernoRegional], VERIFICAR CONSOLA";
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
		