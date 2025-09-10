using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Ejercicio_1._5__Comercio_.Data
{   //Segundo paso
    public class DataHelper
    {
        private static DataHelper _instance;
        private SqlConnection _connection;

        private DataHelper()
        {
            _connection = new SqlConnection(Properties.Resources.CadenaConexionLocal);
        }
        public static DataHelper GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DataHelper();
            }
            return _instance;
        }
        public DataTable ExcecuteSPQuery(string sp,List<ParameterSP>? parameters =null)
        {
            DataTable dt = new DataTable();
            try
            {
                _connection.Open();
                var cmd = new SqlCommand(sp, _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = sp;
                if(parameters != null)
                {
                    foreach (ParameterSP param in parameters)
                    {
                        cmd.Parameters.AddWithValue(param.Name, param.Value);
                    }
                }
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception ex)
            {
                dt = null;
            }
            finally
            {
                _connection.Close();
            }
            return dt;
        }
        public int ExcecuteSPDMLQuery(string sp, List<ParameterSP>? parameters = null)
        {
            int rowsAffected = 0;
            try
            {
                _connection.Open();
                var cmd = new SqlCommand(sp, _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = sp;
                if (parameters != null)
                {
                    foreach (ParameterSP param in parameters)
                    {
                        cmd.Parameters.AddWithValue(param.Name, param.Value ?? DBNull.Value);
                    }
                }
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                rowsAffected = -1;
            }
            finally
            {
                _connection.Close();
            }
            return rowsAffected;
        }


        public SqlConnection GetConnection()
        {
            //devolvemos la conexion abierta
            return _connection;
        }


        public int ExcecuteSPDMLQueryTransact(string sp, List<ParameterSP>? parameters,SqlTransaction transaction) 
        {
            //To do
            return 0;
        }



    }


        
    
}
