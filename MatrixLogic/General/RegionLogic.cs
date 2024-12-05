		using System;
		using Utilitarios;
		using System.Data;
		using System.Threading.Tasks;
		using General;
		namespace GeneralLogic
		{	
				
			public class RegionLogic
			{
			private Region region;
			private ListaRegion lista;

			public RegionLogic()
			{
				region = new Region();
				lista = new ListaRegion();
			}
			
			public async Task<Mensaje> EliminarRegionAsync (int Gestor, int IdRegion,int IdUsuarioAuditoria )
			{
				Mensaje mensaje = new Mensaje();			
				if (Gestor == DatosGlobales.GestorSqlServer)
				{
					System.Object[] ParamtrosOutPut = null;				
						ParamtrosOutPut =await ConexionSqlServer.GDatos.EjecutarAsync(DatosGlobales.ListaConexiones.cnGeneralSql,"General.paEliminarRegion",IdRegion,IdUsuarioAuditoria,mensaje.DescripcionMensaje,mensaje.CodigoMensaje);
						if (Convert.ToInt32(ParamtrosOutPut[100].ToString())>0)
						{
							mensaje.CodigoMensaje = 1;
							mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA CAPA DE DATOS [EliminarRegion] VERIFICAR CONSOLA";
							mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
							return mensaje;
						}
						mensaje.DescripcionMensaje = ParamtrosOutPut[0].ToString();
						mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[1].ToString());					
				}
				return mensaje;
			}	
			public async Task<Region>  ObtenerRegionAsync (int Gestor, int IdRegion, int IdUsuarioAuditoria)
			{		
				DataSet ds = new DataSet(); 
				DataTable dt = new DataTable(); 		   
				try
				{					          
					if (Gestor == DatosGlobales.GestorSqlServer)
					{
						ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnGeneralSql,"General.paObtenerRegion", IdRegion,IdUsuarioAuditoria );
						 if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
						 {
							region.mensaje.CodigoMensaje =1;
							region.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ObtenerRegion], VERIFICAR CONSOLA";
							region.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
                     
							return region;      
						}
						dt = ds.Tables[0].Copy(); 
														region.IdRegion = (int)dt.Rows[0]["IdRegion"];		
																	region.NombreRegion = (string)dt.Rows[0]["NombreRegion"];		
																				
					} 		
					return region;            
				}
				catch (Exception ex)
				{
					region.mensaje.CodigoMensaje = 1;
					region.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ObtenerRegion], VERIFICAR CONSOLA";
					region.mensaje.DescripcionMensajeSistema = ex.Message;
					return region;
				}
				 finally
				{
					ds.Dispose();
					ds.Clear();
					dt.Dispose();
					dt.Clear();
				}
			}
		public async Task<Region> GuardaRegionAsync(int Gestor, int IdRegion,string NombreRegion,int IdUsuarioAuditoria)
        { 
			try
			{            
				if (Gestor == DatosGlobales.GestorSqlServer)
				{
					System.Object[] ParamtrosOutPut = null;				
					ParamtrosOutPut =await ConexionSqlServer.GDatos.EjecutarAsync(DatosGlobales.ListaConexiones.cnGeneralSql,"General.paGuardarRegion",  IdRegion, NombreRegion,IdUsuarioAuditoria,"",0);					
					if (Convert.ToInt32(ParamtrosOutPut[100].ToString())>0)
					{
						region.mensaje.CodigoMensaje = 1;
						region.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [GuardaRegion], VERIFICAR CONSOLA";
						region.mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
						return region;
					}
					region.IdRegion = Convert.ToInt32(ParamtrosOutPut[0].ToString());
					region.mensaje.DescripcionMensaje = ParamtrosOutPut[1].ToString();
					region.mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[2].ToString());
				}
				return region;
			}
			catch (Exception ex)
			{
				region.mensaje.CodigoMensaje = 1;
				region.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [GuardaRegion], VERIFICAR CONSOLA";
				region.mensaje.DescripcionMensajeSistema = ex.Message;
				return region;
			}
        }
				
		public async Task<ListaRegion> ListarRegionAsync(int Gestor, int IdUsuarioAuditoria,  string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
            {                
			 DataSet ds = new DataSet(); 
             DataTable dt = new DataTable();  
			try
				{                
				if (Gestor == DatosGlobales.GestorSqlServer)
				{      
					ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnGeneralSql,"General.paListarRegion",IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
					 if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
					 {
							lista.mensaje.CodigoMensaje =1;
							lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarRegion], VERIFICAR CONSOLA";
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
				region = new Region();
											region.IdRegion = (int)row["IdRegion"];			
															region.NombreRegion = (string)row["NombreRegion"];			
							              
			    lista.lista.Add(region);              
                }
				 return lista;
			}
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarRegion], VERIFICAR CONSOLA";
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

	public async Task<ListaRegion> ListarComboRegion(int Gestor, int IdUsuarioAuditoria)
	{
		DataSet ds = new DataSet();
		DataTable dt = new DataTable();
		try
		{
			if (Gestor == DatosGlobales.GestorSqlServer)
			{
				ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnGeneralSql, "mt.paListarComboRegion", IdUsuarioAuditoria);
				if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
				{
					lista.mensaje.CodigoMensaje = 1;
					lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarRegion], VERIFICAR CONSOLA";
					lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
					return lista;
				}
				dt = ds.Tables[0].Copy();				
			}
			foreach (DataRow row in dt.Rows)
			{
				region = new Region();
				region.IdRegion = (int)row["IdRegion"];
				region.NombreRegion = (string)row["NombreRegion"];
				lista.lista.Add(region);
			}
			return lista;
		}
		catch (Exception ex)
		{
			lista.mensaje.CodigoMensaje = 1;
			lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarRegion], VERIFICAR CONSOLA";
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
		