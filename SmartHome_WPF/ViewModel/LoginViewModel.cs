using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml;
using MahApps.Metro.Controls.Dialogs;
using SmartHome_WPF.Auxiliares;
using SmartHome_WPF.Collections;
using SmartHome_WPF.Factory;
using SmartHome_WPF.Model;
using SmartHome_WPF.SQLCommands;
using SmartHome_WPF.View;

namespace SmartHome_WPF.ViewModel
{
    public class LoginViewModel : ModelBase
    {
 
        private UsuarioCollection _UsuariCollection;
        public LoginViewModel()
        {
            UsuarioModel = new UsuarioModel();
        }
        public UsuarioModel UsuarioModel { get; set; }
        public static UsuarioModel UsuarioLogado { get; set; }
        public UsuarioCollection UsuarioCollection
        {
            get { return _UsuariCollection; }
            set { Set(ref _UsuariCollection, value); }
        }
        public ICommand ButtonsLogin => new RelayCommand((object commandParameter) =>
        {
            string parametro = Convert.ToString(commandParameter);
            if (parametro == "Acessar")
            {
                Acessar();
            }
            else
            {
                Registrar();
            }
        });
        public async void Acessar()
        {
            string message;
            MessageBoxResult result;
            UsuarioSQL usuarioSQL = new UsuarioSQL();
            //try
            //{
                UsuarioLogado = await usuarioSQL.RealizarLoginAsync(UsuarioModel);
                if(UsuarioLogado != null)
                {
                foreach (Window w in Application.Current.Windows)
                        w.Hide();

                SettingsFactory.Instance.Username = UsuarioLogado.Nome_Usuario;

                Dashboard dash = new Dashboard();
                //Login.FecharOuAbrirDash(false);
                
                dash.ShowDialog();
                UsuarioModel = new UsuarioModel();

            }   
                else
                {
                //await Coordinator.ShowMessageAsync(this, "Aviso", "Usuário e senha inválidos, tente novamente");
                message = "Usuário ou senha inválidos";
                result = MessageBox.Show(message);
            }
               
            //}
            //catch (SqlException ex)
            //{
            //    await Coordinator.ShowMessageAsync(this, "Aviso", "Banco de dados SQL com problema, deseja abrir as configurações?\n\nDetalhes: " + ex.Message);
            //}
        }
        public async void Registrar()
        {

        }
        
    }
}
