using SmartHome_WPF.Auxiliares;
using SmartHome_WPF.Factory;
using SmartHome_WPF.Model;
using SmartHome_WPF.SQLCommands;
using SmartHome_WPF.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SmartHome_WPF.ViewModel
{
    public class DashViewModel : ModelBase
    {
        #region fields

        public string _consumoAtual;
        public string _previaMensal;
        public List<string> _historicoConsumoData = new List<string>();
        public List<string> _historicoConsumoValor = new List<string>();
        public List<string> _historicoConsumoPotencia = new List<string>(); 

        #endregion

        #region constructor
        public DashViewModel(ResourceDictionary resourceDictionary) : base() 
        {
            LoadData();
        }
        #endregion

        #region properties

        public string Username
        {
            get { return SettingsFactory.Instance.Username; }
            set
            {
                SettingsFactory.Instance.Username = value;
            }
        }

        public string ConsumoAtual
        {
            get { return _consumoAtual; }
            set
            {
                _consumoAtual = value;
            }
        }

        public string PreviaMensal
        {
            get { return _previaMensal; }
            set
            {
                _previaMensal = value;
            }
        }

        public List<string> HistoricoConsumoData
        {
            get { return _historicoConsumoData; }
            set
            {
                _historicoConsumoData = value;
            }
        }

        public List<string> HistoricoConsumoValor
        {
            get { return _historicoConsumoValor; }
            set
            {
                _historicoConsumoValor = value;
            }
        }

        public List<string> HistoricoConsumoPotencia
        {
            get { return _historicoConsumoPotencia; }
            set
            {
                _historicoConsumoPotencia = value;
            }
        }

        #endregion

        public ICommand ButtonsDash => new RelayCommand((object commandParameter) =>
        {
            string parametro = Convert.ToString(commandParameter);
            if (parametro == "AcessarEquipamentos")
            {
                Equipamentos equipamentos = new Equipamentos();
                equipamentos.ShowDialog();
            }
            else if (parametro == "AcessarUsuarios")
            {
                Usuarios usuarios = new Usuarios();
                usuarios.ShowDialog();
            }
            else if (parametro == "AcessarComodos")
            {
                Comodo comodos = new Comodo();
                comodos.ShowDialog();
            }
        });

        public void LoadData()
        {
            string[] stringSplit = Consulta("[dbo].[CONSUMO_ATUAL]").Split(',');
            ConsumoAtual = "R$ " + stringSplit[0] + "," + stringSplit[stringSplit.Length-1].Substring(0,2);
            stringSplit = Consulta("[dbo].[PREVIA_MES]").Split(',');
            PreviaMensal = "R$ " + stringSplit[0] + "," + stringSplit[stringSplit.Length-1].Substring(0, 2);

            HistoricoConsumo();
        }

        public List<string> HistoricoConsumo()
        {
            string connetionString;
            SqlConnection cnn;
            connetionString = @"Data Source=LAPTOP-9HVCT4FE\SQLEXPRESS;Initial Catalog=SMARTHOME;User ID=admin;Password=admin";
            cnn = new SqlConnection(connetionString);

            SqlCommand command = new SqlCommand("[dbo].[HISTORICO_CONSUMO]", cnn);
            command.CommandType = CommandType.StoredProcedure;
            cnn.Close();
            cnn.Open();
            SqlDataReader reader = command.ExecuteReader();

            HistoricoConsumoData.Clear();
            HistoricoConsumoValor.Clear();

            while (reader.Read())
            {
                HistoricoConsumoData.Add(Convert.ToString(reader["mes"]));
                HistoricoConsumoValor.Add(Convert.ToString(reader["valor"]));
            }

            reader.Close();
            return HistoricoConsumoValor;
        }

        public List<string> HistoricoPotencia()
        {
            string connetionString;
            SqlConnection cnn;
            connetionString = @"Data Source=LAPTOP-9HVCT4FE\SQLEXPRESS;Initial Catalog=SMARTHOME;User ID=admin;Password=admin";
            cnn = new SqlConnection(connetionString);

            SqlCommand command = new SqlCommand("[dbo].[HISTORICO_CONSUMO]", cnn);
            command.CommandType = CommandType.StoredProcedure;
            cnn.Close();
            cnn.Open();
            SqlDataReader reader = command.ExecuteReader();

            HistoricoConsumoData.Clear();
            HistoricoConsumoPotencia.Clear();

            while (reader.Read())
            {
                HistoricoConsumoData.Add(Convert.ToString(reader["mes"]));
                HistoricoConsumoPotencia.Add(Convert.ToString(reader["kwh"]));
            }

            reader.Close();
            return HistoricoConsumoPotencia;
        }

        public string Consulta(string query)
        {
            string connetionString;
            string result = "";
            SqlConnection cnn;
            connetionString = @"Data Source=LAPTOP-9HVCT4FE\SQLEXPRESS;Initial Catalog=SMARTHOME;User ID=admin;Password=admin";
            cnn = new SqlConnection(connetionString);

            SqlCommand command = new SqlCommand(query, cnn);
            command.CommandType = CommandType.StoredProcedure;
            cnn.Open();
            SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    result = Convert.ToString(reader["valor"]);
                }

                reader.Close();
                return result; 
        }

    }
}
