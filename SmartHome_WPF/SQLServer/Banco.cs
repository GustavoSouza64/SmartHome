using SmartHome_WPF.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome_WPF.SQLServer
{
    class Banco
    {
        public SqlConnection GetConnection()
        {
            return new SqlConnection(Properties.Settings.Default.ConnectionString);
        }
        List<SqlParameter> listaDados = new List<SqlParameter>();
        public void AdicionarDado(object parameterValue, string parameterName)
        {
            listaDados.Add(new SqlParameter(parameterName, parameterValue));
        }
        public void LimpaDados()
        {
            listaDados.Clear();
        }
        public async Task<DataTable> GetData(string Texto, CommandType Tipo)
        {
            SqlConnection conexao = this.GetConnection();
            try
            {
                await conexao.OpenAsync();
                SqlCommand comando = conexao.CreateCommand();
                comando.CommandType = Tipo;
                comando.CommandText = Texto;
                comando.CommandTimeout = 120;
                listaDados.ForEach((parametro) => comando.Parameters.Add(parametro));
                SqlDataReader reader = await comando.ExecuteReaderAsync();
                DataTable dt = new DataTable();
                dt.Load(reader);
                return dt;
            }
            catch (SqlException ex)
            {
                    throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conexao.State != ConnectionState.Closed)
                    conexao.Close();
            }
        }
        public bool ExecutarManipulacao(string comando, CommandType commandType)
        {
            SqlConnection sqlConnection = GetConnection();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandTimeout = 720;
            sqlCommand.CommandText = comando;
            sqlCommand.CommandType = commandType;
            listaDados.ForEach((parametro) => sqlCommand.Parameters.Add(parametro));
            try
            {
                sqlConnection.Open();
                return sqlCommand.ExecuteNonQuery() > 0;
            }
            finally
            {
                if (sqlConnection.State != ConnectionState.Closed)
                {
                    sqlCommand.Dispose();
                    sqlConnection.Dispose();
                }
            }
        }

        SqlParameterCollection Colecao = new SqlCommand().Parameters;
        public void LimparParametros()
        {
            Colecao.Clear();
        }
        public void AdicionarParametro( Object Valor, String Parametro)
        {
            Colecao.Add(new SqlParameter(Parametro, Valor));
        }
        public async Task<int> ManipulacaoAsync(CommandType tipo, String Texto)
        {
            SqlConnection conexao = GetConnection();
            try
            {
                SqlCommand comando = conexao.CreateCommand();
                await conexao.OpenAsync();
                comando.CommandType = tipo;
                comando.CommandText = Texto;
                comando.CommandTimeout = 120;
                foreach (SqlParameter Parametro in Colecao)
                {
                    comando.Parameters.Add(new SqlParameter(Parametro.ParameterName, Parametro.Value));
                }
                return await comando.ExecuteNonQueryAsync();
            }
           
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conexao.State != ConnectionState.Closed)
                {
                    conexao.Close();
                }
            }
        }
        public static string pegaTexto(string texto)
        {
            texto += ")";
            texto = texto.Substring(texto.IndexOf("(") + 1);
            return texto.Substring(0, texto.IndexOf(")"));
        }
    }
}
