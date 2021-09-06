using SmartHome_WPF.Auxiliares;
using SmartHome_WPF.Collections;
using SmartHome_WPF.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.IO.Ports;
using System;
using System.Data.SqlClient;
using System.Windows.Input;
using SmartHome_WPF.View;
using SmartHome_WPF.Factory;

namespace SmartHome_WPF.ViewModel
{
    public class RelatoriosViewModel : ModelBase
    {
        #region fields

        public ObservableCollection<RelatorioModel> _relatorio = new ObservableCollection<RelatorioModel>();

        #endregion

        #region constructor
        public RelatoriosViewModel(ResourceDictionary resourceDictionary): base() 
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

        public ObservableCollection<RelatorioModel> Relatorio
        {
            get { return _relatorio; }
            set
            {
                _relatorio = value;
            }
        }

        public ICommand ButtonsRel => new RelayCommand((object commandParameter) =>
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
                Comodo usuarios = new Comodo();
                usuarios.ShowDialog();
            }
        });
        #endregion

        #region methods

        public void LoadData()
        {
        }

        public void Query(string data01, string data02)
        {
            Relatorio.Clear();

            DateTime dataIni = Convert.ToDateTime(data01);
            DateTime dataFim = Convert.ToDateTime(data02);

            if (dataIni <= dataFim)
            {

                string connetionString;
                SqlConnection cnn;
                connetionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=SMARTHOME;User ID=admin;Password=admin";
                cnn = new SqlConnection(connetionString);

                SqlCommand command = new SqlCommand("select mes, sum(valor) kwh from (select concat(Year(dataConsumo), '/', month(dataConsumo)) mes, ABS(valor) valor from tbl_Consumo where dataConsumo > \'" + data01 + "\' AND dataConsumo <= \'" + data02 + "\' and valor != 0) tbl group by mes", cnn);
                cnn.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    double valorMonetario = Convert.ToDouble(reader["kwh"]) * 0.21;
                    string[] stringSplit = Convert.ToString(valorMonetario).Split(',');
                    string valorMonetarioFinal = "R$ " + stringSplit[0] + "," + stringSplit[stringSplit.Length - 1].Substring(0, 2);

                    Relatorio.Add(new RelatorioModel(Convert.ToString(reader["mes"]), Convert.ToString(reader["kwh"]),Convert.ToString(valorMonetarioFinal)));
                }

                reader.Close();
            }
            else
            {
                MessageBox.Show("Data Ínicio não pode ser maior que a data fim");
            }
        }

      
        #endregion
    }
}
