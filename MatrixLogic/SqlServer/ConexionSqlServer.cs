using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ConexionSqlServer
{
    public class GDatos
    {
        public static async Task<Object[]> EjecutarAsync(Utilitarios.DatosGlobales.ListaConexiones ConexionDeEsquema, string ProcedimientoAlmacenado, params Object[] args)
        {
            string cadenadeconexion = Utilitarios.DatosGlobales.ObtenerCadenaDeConexion(ConexionDeEsquema);           
            Object[] Resp = new Object[101];
            SqlCommand com = new SqlCommand();
            try
            {               
                using (SqlConnection conn = new SqlConnection(cadenadeconexion))
                {
                   await conn.OpenAsync();
                    com = new SqlCommand(ProcedimientoAlmacenado, conn) { CommandType = CommandType.StoredProcedure };
                    SqlCommandBuilder.DeriveParameters(com);
                    CargarParametros(com, args);
                    await com.ExecuteNonQueryAsync();
                }

                int dim = 0;
                for (int i = 0; i < com.Parameters.Count; i++)
                {
                    var Par = (IDbDataParameter)com.Parameters[i];
                    if (Par.Direction == ParameterDirection.InputOutput || Par.Direction == ParameterDirection.Output)
                    {
                        Resp[dim] = Par.Value.ToString();
                        dim++;
                    }
                }
                Resp[100] = 0;
                return Resp;
            }
            catch (Exception ex)
            {
                Resp[99] = ex.Message;
                Resp[100] = 1;
                return Resp;
            }
            finally
            {
                com.Dispose();
            }
        }
        public static Object[] Ejecutar(Utilitarios.DatosGlobales.ListaConexiones ConexionDeEsquema, string ProcedimientoAlmacenado, params Object[] args)
        {
            string cadenadeconexion = Utilitarios.DatosGlobales.ObtenerCadenaDeConexion(ConexionDeEsquema);
            Object[] Resp = new Object[101];
            SqlCommand com = new SqlCommand();
            try
            {
                using (SqlConnection conn = new SqlConnection(cadenadeconexion))
                {
                    conn.Open();
                    com = new SqlCommand(ProcedimientoAlmacenado, conn) { CommandType = CommandType.StoredProcedure };
                    SqlCommandBuilder.DeriveParameters(com);
                    CargarParametros(com, args);
                    com.ExecuteNonQuery();
                }

                int dim = 0;
                for (int i = 0; i < com.Parameters.Count; i++)
                {
                    var Par = (IDbDataParameter)com.Parameters[i];
                    if (Par.Direction == ParameterDirection.InputOutput || Par.Direction == ParameterDirection.Output)
                    {
                        Resp[dim] = Par.Value.ToString();
                        dim++;
                    }
                }
                Resp[100] = 0;
                return Resp;
            }
            catch (Exception ex)
            {
                Resp[99] = ex.Message;
                Resp[100] = 1;
                return Resp;
            }
            finally
            {
                com.Dispose();
            }
        }
        public static async Task<DataSet> TraerDataSetAsync(Utilitarios.DatosGlobales.ListaConexiones ConexionDeEsquema, string procedimientoAlmacenado, params Object[] args)
        {
            string cadenadeconexion = Utilitarios.DatosGlobales.ObtenerCadenaDeConexion(ConexionDeEsquema);
            SqlCommand com = new SqlCommand();
            DataSet mDataSet = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                using (SqlConnection conn = new SqlConnection(cadenadeconexion))
                {
                    await conn.OpenAsync();
                    com = new SqlCommand(procedimientoAlmacenado, conn) { CommandType = CommandType.StoredProcedure };
                    SqlCommandBuilder.DeriveParameters(com);
                    CargarParametros(com, args);                   
                    adapter.SelectCommand = com;
                    await com.ExecuteNonQueryAsync();
                    adapter.Fill(mDataSet);
                }
                mDataSet.ExtendedProperties.Add("NumeroError", 0);
                mDataSet.ExtendedProperties.Add("DescripcionError", "");
                return mDataSet;
            }
            catch (Exception ex)
            {
                mDataSet.ExtendedProperties.Add("NumeroError", 1);
                mDataSet.ExtendedProperties.Add("DescripcionError", ex.Message);
                return mDataSet;
            }
            finally
            {
                com.Dispose();
                mDataSet.Dispose();
                adapter.Dispose();
            }
        }
        public static DataSet TraerDataSet(Utilitarios.DatosGlobales.ListaConexiones ConexionDeEsquema, string procedimientoAlmacenado, params Object[] args)
        {
            string cadenadeconexion = Utilitarios.DatosGlobales.ObtenerCadenaDeConexion(ConexionDeEsquema);
            SqlCommand com = new SqlCommand();
            DataSet mDataSet = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                using (SqlConnection conn = new SqlConnection(cadenadeconexion))
                {
                    conn.Open();
                    com = new SqlCommand(procedimientoAlmacenado, conn) { CommandType = CommandType.StoredProcedure };
                    SqlCommandBuilder.DeriveParameters(com);
                    CargarParametros(com, args);
                    adapter.SelectCommand = com;
                    com.ExecuteNonQuery();
                    adapter.Fill(mDataSet);
                }
                mDataSet.ExtendedProperties.Add("NumeroError", 0);
                mDataSet.ExtendedProperties.Add("DescripcionError", "");
                return mDataSet;
            }
            catch (Exception ex)
            {
                mDataSet.ExtendedProperties.Add("NumeroError", 1);
                mDataSet.ExtendedProperties.Add("DescripcionError", ex.Message);
                return mDataSet;
            }
            finally
            {
                com.Dispose();
                mDataSet.Dispose();
                adapter.Dispose();
            }
        }
        private static void CargarParametros(IDbCommand com, Object[] args)
        {
            for (int i = 1; i < com.Parameters.Count; i++)
            {
                var p = (SqlParameter)com.Parameters[i];
                p.Value = i <= args.Length ? args[i - 1] ?? DBNull.Value : null;
            }
        }
        //Fos 20230626
        public static async Task<string> EjecutarComandoAsync(Utilitarios.DatosGlobales.ListaConexiones ConexionDeEsquema, string procedimientoAlmacenado, params Object[] args)
        {
            string rpta = "";
            string cadenadeconexion = Utilitarios.DatosGlobales.ObtenerCadenaDeConexion(ConexionDeEsquema);
            SqlCommand com = new SqlCommand();
            try
            {
                using (SqlConnection conn = new SqlConnection(cadenadeconexion))
                {
                    await conn.OpenAsync();
                    com = new SqlCommand(procedimientoAlmacenado, conn) { CommandType = CommandType.StoredProcedure };
                    SqlCommandBuilder.DeriveParameters(com);
                    CargarParametros(com, args);
                    object data = com.ExecuteScalar();
                    if (data != null) rpta = data.ToString();
                }
                return rpta;
            }
            catch (Exception ex)
            {
                return "Error¬" + ex.Message;
            }
            finally
            {
                com.Dispose();
            }
        }

        public static async Task<DataSet> TraerDataSetAsync2(Utilitarios.DatosGlobales.ListaConexiones ConexionDeEsquema, string procedimientoAlmacenado, params Object[] args)
        {
            string cadenadeconexion = Utilitarios.DatosGlobales.ObtenerCadenaDeConexion(ConexionDeEsquema, 2);
            SqlCommand com = new SqlCommand();
            DataSet mDataSet = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                using (SqlConnection conn = new SqlConnection(cadenadeconexion))
                {
                    await conn.OpenAsync();
                    com = new SqlCommand(procedimientoAlmacenado, conn) { CommandType = CommandType.StoredProcedure };
                    SqlCommandBuilder.DeriveParameters(com);
                    CargarParametros(com, args);
                    adapter.SelectCommand = com;
                    await com.ExecuteNonQueryAsync();
                    adapter.Fill(mDataSet);
                }
                mDataSet.ExtendedProperties.Add("NumeroError", 0);
                mDataSet.ExtendedProperties.Add("DescripcionError", "");
                return mDataSet;
            }
            catch (Exception ex)
            {
                mDataSet.ExtendedProperties.Add("NumeroError", 1);
                mDataSet.ExtendedProperties.Add("DescripcionError", ex.Message);
                return mDataSet;
            }
            finally
            {
                com.Dispose();
                mDataSet.Dispose();
                adapter.Dispose();
            }
        }

        public static async Task<DataSet> TraerDataSetPorQueryAsync(Utilitarios.DatosGlobales.ListaConexiones ConexionDeEsquema, string query)
        {
            string cadenadeconexion = Utilitarios.DatosGlobales.ObtenerCadenaDeConexion(ConexionDeEsquema);
            SqlCommand com = new SqlCommand();
            DataSet mDataSet = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                using (SqlConnection conn = new SqlConnection(cadenadeconexion))
                {
                    await conn.OpenAsync();
                    com = new SqlCommand(query, conn) { CommandType = CommandType.Text };
                    adapter.SelectCommand = com;
                    await com.ExecuteNonQueryAsync();
                    adapter.Fill(mDataSet);
                }
                mDataSet.ExtendedProperties.Add("NumeroError", 0);
                mDataSet.ExtendedProperties.Add("DescripcionError", "");
                return mDataSet;
            }
            catch (Exception ex)
            {
                mDataSet.ExtendedProperties.Add("NumeroError", 1);
                mDataSet.ExtendedProperties.Add("DescripcionError", ex.Message);
                return mDataSet;
            }
            finally
            {
                com.Dispose();
                mDataSet.Dispose();
                adapter.Dispose();
            }
        }

    }
}