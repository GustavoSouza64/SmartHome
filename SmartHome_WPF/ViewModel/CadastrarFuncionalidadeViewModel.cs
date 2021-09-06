using SmartHome_WPF.Auxiliares;
using SmartHome_WPF.Model;
using SmartHome_WPF.SQLCommands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SmartHome_WPF.ViewModel
{
    public class CadastrarFuncionalidadeViewModel : ModelBase
    {
        public CadastrarFuncionalidadeViewModel()
        {
            funcionalidadeModel = new FuncionalidadesModel();
            Task.Run(async () =>
            {
                Equipamentos = await equipamentoSQL.GetEquipamentos();
            });

            Task.Run(async () =>
            {
                Funcionalidades = await funcionalidadeSQL.GetFuncionalidades();
            });
        }

        private bool isEdit = false;
        private FuncionalidadesModel _funcionalidadeModel;
        private FuncionalidadesModel _funcionalidadeSelecionada;
        private ObservableCollection<EquipamentoModel> equipamentos;
        private ObservableCollection<FuncionalidadesModel> funcionalidades;
        private EquipamentoSQL equipamentoSQL = new EquipamentoSQL();
        private FuncionalidadeSQL funcionalidadeSQL = new FuncionalidadeSQL();


        public bool IsEdit { get => isEdit; set => Set(ref isEdit, value); }
        public FuncionalidadesModel funcionalidadeModel
        {
            get => _funcionalidadeModel;
            set => Set(ref _funcionalidadeModel, value);
        }

        public FuncionalidadesModel FuncionalidadeSelecionada
        {
            get => _funcionalidadeSelecionada;
            set
            {
                
                IsEdit = true;
                Set(ref _funcionalidadeSelecionada, value);
            }
        }

        public ObservableCollection<EquipamentoModel> Equipamentos { get => equipamentos; set => Set(ref equipamentos, value); }
        public ObservableCollection<FuncionalidadesModel> Funcionalidades { get => funcionalidades; set => Set(ref funcionalidades, value); }

        public ICommand ButtonsCadastrarFuncionalidades => new RelayCommand((object commandParameter) =>
        {
            string parametro = Convert.ToString(commandParameter);
            if(parametro == "Cadastrar")
            {
                Cadastrar();
            }
            else
            {
                Voltar();
            }
        });

        public ICommand ButtonsConsultarFuncionalidades => new RelayCommand((object commandParameter) =>
        {
            string parametro = Convert.ToString(commandParameter);
            if(parametro == "Alterar")
            {
                Alterar();
            }else if (parametro == "Deletar")
            {
                Deletar();
            }
            else
            {
                Voltar();
            }
        });

        public async void Cadastrar()
        {
            string message;
            MessageBoxResult result;

            if (validaEntrada(funcionalidadeModel))
            {
                try
                {
                    Boolean FuncionalidadeCadastrada = await funcionalidadeSQL.InserirFuncionalidade(funcionalidadeModel);
                    if (FuncionalidadeCadastrada == true)
                    {
                        message = "Funcionalidade incluído com sucesso!";
                        result = MessageBox.Show(message);
                        funcionalidadeModel = new FuncionalidadesModel();
                    }
                    else
                    {
                        message = "Funcionalidade já existente no banco de dados";
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
        }

        public async void Alterar()
        {
            string message;
            MessageBoxResult result;

            if (validaEntrada(funcionalidadeModel))
            {
                try
                {
                    Boolean FuncionalidadeAlterada = await funcionalidadeSQL.AlterarFuncionalidade(funcionalidadeModel);
                    if (FuncionalidadeAlterada == true)
                    {
                        message = "Funcionalidade alterada com sucesso!";
                        funcionalidadeModel = new FuncionalidadesModel();
                        IsEdit = false;
                        result = MessageBox.Show(message);
                    }
                    else
                    {
                        message = "Funcionalidade já existente no banco de dados";
                        result = MessageBox.Show(message);
                    }
                }
                catch (SqlException Erro)
                {
                    message = "Banco de dados indisponível: " + Erro.Message;
                    result = MessageBox.Show(message);
                }
                catch (Exception Erro)
                {
                    message = "Erro operacional: " + Erro.Message;
                    result = MessageBox.Show(message);
                }
            }
            else
            {
                message = "Campos inválidos, verifique se todos os campos foram preenchidos corretamente.";
                result = MessageBox.Show(message);
            }
        }
        
        public void Deletar()
        {

        }

        public Boolean validaEntrada(FuncionalidadesModel funcionalidadesModel)
        {
            if(funcionalidadesModel.NomeFuncionalidade != null && funcionalidadesModel.Equipamento != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Voltar()
        {
            Application.Current.Shutdown();
        }
    }
}
