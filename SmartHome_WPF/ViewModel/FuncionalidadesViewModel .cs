using SmartHome_WPF.Auxiliares;
using SmartHome_WPF.Collections;
using SmartHome_WPF.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.IO.Ports;
using System;
using System.Windows.Input;
using SmartHome_WPF.View;
using System.Data.SqlClient;
using System.Data;
using SmartHome_WPF.Factory;

namespace SmartHome_WPF.ViewModel
{
    public class FuncionalidadesViewModel : ModelBase
    {
        #region fields

        public SerialPort s;
        private UsuarioCollection _UsuariCollection;
        public FuncionalidadesModel _selectedFunc;
        public bool _executeArduino;
        public ObservableCollection<FuncionalidadesModel> _funcionalidades = new ObservableCollection<FuncionalidadesModel>();
        public ObservableCollection<FuncionalidadesModel> _comodos = new ObservableCollection<FuncionalidadesModel>();

        #endregion

        #region constructor
        public FuncionalidadesViewModel(ResourceDictionary resourceDictionary): base() { }
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

        public ObservableCollection<FuncionalidadesModel> Funcionalidades
        {
            get { return _funcionalidades; }
            set
            {
                _funcionalidades = value;
                //RaisePropertyChanged("Funcionalidades");
            }
        }

        public ObservableCollection<FuncionalidadesModel> Comodos
        {
            get { return _comodos; }
            set
            {
                _comodos = value;
                //RaisePropertyChanged("Funcionalidades");
            }
        }


        public FuncionalidadesModel SelectedFunc
        {
            get { return _selectedFunc; }
            set
            {
                _selectedFunc = value;
                ExecuteArduinoCommand();
            }
        }

        public bool ExecuteArduino
        {
            get { return _executeArduino; }
            set
            {
                _executeArduino = value;
                ExecuteArduinoCommand();
            }
        }

        public ICommand ButtonsFunc => new RelayCommand((object commandParameter) =>
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
            else if (parametro == "AcessarFunc")
            {
                CadastrarFuncionalidade funcs = new CadastrarFuncionalidade();
                funcs.ShowDialog();
            }
            
        });
        public UsuarioModel UsuarioModel { get; set; }
        public static UsuarioModel UsuarioLogado { get; set; }
        public UsuarioCollection UsuarioCollection
        {
            get { return _UsuariCollection; }
            set { Set(ref _UsuariCollection, value); }
        }
        #endregion

        #region methods

        public void LoadData()
        {
            s = new SerialPort();
            s.PortName = "COM7"; // Mude conforme a porta serial do seu Arduino
            s.BaudRate = 9600;


            string connetionString;
            SqlConnection cnn;
            connetionString = @"Data Source=LAPTOP-9HVCT4FE\SQLEXPRESS;Initial Catalog=SMARTHOME;User ID=admin;Password=admin";
            cnn = new SqlConnection(connetionString);

            SqlCommand command = new SqlCommand("[dbo].[SP_retornaComodos]", cnn);
            command.CommandType = CommandType.StoredProcedure;
            cnn.Close();
            cnn.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Comodos.Add(new FuncionalidadesModel(1, "Ligar Lâmpada", Convert.ToString(reader["descricao"])));
            }

            Funcionalidades.Add(new FuncionalidadesModel(1, "Ligar Lâmpada", "test"));
            Funcionalidades.Add(new FuncionalidadesModel(2, "Ligar Ventilador", "test"));

            reader.Close();

        }

        public void ExecuteArduinoCommand()
        {
            if (!s.IsOpen) // Se a porta serial não estiver disponível
            {
                try
                {
                    
                    s.Open(); // Abre

                    if(SelectedFunc.IdFuncionalidade == 1)
                    {
                        s.Write("1"); // Escreve na porta
                    }
                    else
                    {
                        s.Write("2"); // Escreve na porta
                    }

                    s.Close(); // Fecha
                }
                catch(Exception e)
                {
                }
            }
        }
        #endregion
    }
}
