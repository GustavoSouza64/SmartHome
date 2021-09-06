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
    public class ComodosViewModel : ModelBase
    {
        private ResidenciaSQL residenciaSQL = new ResidenciaSQL();
        private ComodoSQL comodoSQL = new ComodoSQL();
        private ObservableCollection<ResidenciaModel> residencias;
        public ObservableCollection<ResidenciaModel> Residencias { get => residencias; set => Set(ref residencias, value);}
        private ObservableCollection<ComodoModel> comodos;
        public ObservableCollection<ComodoModel> Comodos { get => comodos; set => Set(ref comodos, value); }
        private ComodoModel _comodoModel;
        private ComodoModel comodoSelecionado;
        private bool isEdit = false;
        public ComodoModel comodoModel
        {
            get => _comodoModel;
            set => Set(ref _comodoModel, value);
        }
        public ComodoModel ComodoSelecionado
        {
            get => comodoSelecionado;
            set
            {
                if(value != null)
                {
                    comodoModel = value;
                    IsEdit = true;
                }
                Set(ref comodoSelecionado, value);
            }
        }
        public bool IsEdit { get => isEdit; set => Set(ref isEdit, value); }
        public ComodosViewModel()
        {
            comodoModel = new ComodoModel();
            Task.Run(async () =>
            {
                Residencias = await residenciaSQL.GetResidencias();
            });

            Task.Run(async () =>
            {
                Comodos = await comodoSQL.GetComodos();
            });
        }

        public ICommand ButtonsCadastrarComodo => new RelayCommand((object commandParameter) =>
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

        public ICommand ButtonsConsultarComodo => new RelayCommand((object commandParameter) =>
        {
            string parametro = Convert.ToString(commandParameter);
            if (parametro == "Alterar")
            {
                Alterar();
            }
            else if(parametro == "Deletar"){
                Deletar();
            }else
            {
                Voltar();
            }
        });

        public async void Cadastrar()
        {
            string message;
            MessageBoxResult result;

            if (validaEntrada(comodoModel))
            {

                try
                {
                    Boolean ComodoCadastrado = await comodoSQL.InserirComodo(comodoModel);
                    if (ComodoCadastrado == true)
                    {
                        message = "Cômodo: " + comodoModel.Descricao + " cadastrado com sucesso!";
                        comodoModel = new ComodoModel();
                        result = MessageBox.Show(message);
                    }
                    else
                    {
                        message = "Comodo já existente no banco de dados";
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
        public async void Alterar()
        {
            string message;
            MessageBoxResult resultadoAlteracao;

            if (validaEntrada(comodoModel))
            {
                try
                {
                    Boolean ComodoAlterado = await comodoSQL.AlterarComodo(comodoModel);
                    if (ComodoAlterado == true)
                    {
                        message = "Comodo " + comodoModel.Descricao + " alterado com sucesso!";
                        comodoModel = new ComodoModel();
                        MessageBox.Show(message);
                    }
                    else
                    {
                        message = "Comodo já existente no banco de dados";
                        resultadoAlteracao = MessageBox.Show(message);
                    }
                }
                catch (SqlException Erro)
                {
                    message = "Banco de dados indisponível: " + Erro.Message;
                    resultadoAlteracao = MessageBox.Show(message);
                }
                catch (Exception Erro)
                {
                    message = "Erro operacional: " + Erro.Message;
                    resultadoAlteracao = MessageBox.Show(message);
                }
            }
            else
            {
                message = "Campos inválidos, verifique se todos os campos foram preenchidos corretamente.";
                resultadoAlteracao = MessageBox.Show(message);
            }
        }

        public async void Deletar()
        {
            string message;
            MessageBoxResult result;

            if (validaEntrada(comodoModel))
            {
                try
                {
                    Boolean ComodoDeletado = await comodoSQL.DeletaComodo(comodoModel);
                    if (ComodoDeletado)
                    {
                        message = "Comodo " + comodoModel.Descricao + " deletado com sucesso!";
                        comodoModel = new ComodoModel();
                        result = MessageBox.Show(message);
                    }
                    else
                    {
                        message = "Comodo já existente no banco de dados";
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
        public Boolean validaEntrada(ComodoModel comodo)
        {
            if (comodo.Descricao != "" &&
                comodo.Residencia != null)
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
            
        }
    }
}
