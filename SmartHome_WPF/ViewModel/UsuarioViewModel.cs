using Caliburn.Micro;
using SmartHome_WPF.Auxiliares;
using SmartHome_WPF.Collections;
using SmartHome_WPF.Model;
using SmartHome_WPF.SQLCommands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SmartHome_WPF.ViewModel
{
    public class UsuarioViewModel : ModelBase
    {
        #region Construtor
        public UsuarioViewModel()
        {
            UsuarioModel = new UsuarioModel();

        }
        #endregion

        #region Getters and Setters

        public UsuarioModel UsuarioModel { get; set; }
        
 
        #endregion

        #region Botões
        public ICommand ButtonsCadastrarUsuario => new RelayCommand((object commandParameter) =>
        {
            string parametro = Convert.ToString(commandParameter);
            if (parametro == "Cadastrar")
            {
                Cadastrar();
            }
            else
            {
                Voltar();
            }
        });

        public ICommand ButtonsConsultarUsuario => new RelayCommand((object commandParameter) =>
        {
            string parametro = Convert.ToString(commandParameter);
           

        });
        #endregion Botões

        public Boolean validaEntrada(UsuarioModel usuario)
        {
            if (usuario.Nome_Usuario != "" &&
                usuario.Login != "" &&
                usuario.Senha != "" &&
                usuario.ConfirmaSenha != ""
                )
            {
                return true;
            }
            else
                return false;
        }


        #region Métodos
        public async void Cadastrar()
        {
            UsuarioSQL usuarioSQL = new UsuarioSQL();
            string message;
            MessageBoxResult result;

            if (validaEntrada(UsuarioModel))
            {
                if (UsuarioModel.Senha == UsuarioModel.ConfirmaSenha)
                {
                    try
                    {
                        Boolean UsuarioCadastrado = await usuarioSQL.InserirUsuario(UsuarioModel);
                        if (UsuarioCadastrado == true)
                        { 
                            message = "Usuário incluído com sucesso!";
                            UsuarioModel = new UsuarioModel();
                            result = MessageBox.Show(message);
                            
                        }
                        else
                        {
                            message = "Usuário já existente no banco de dados";
                            result = MessageBox.Show(message);
                        }
                    }
                    catch (SqlException Erro)
                    {
                        message = "Banco de dados indisponível" + Erro.Message;
                        result = MessageBox.Show(message);
                    }
                    catch (Exception Erro)
                    {
                        message = "Erro operacional" + Erro.Message;
                        result = MessageBox.Show(message);
                    }
                }
                else
                {
                    message = "Senhas não coincidem, digite novamente.";
                    result = MessageBox.Show(message);
                }
            }
            else
            {
                message = "Campos inválidos, verifique se todos os campos foram preenchidos corretamente.";
                result = MessageBox.Show(message);
            } 
        }

        public void Voltar()
        {
            Application.Current.Shutdown();
        }
        #endregion
    }

}
