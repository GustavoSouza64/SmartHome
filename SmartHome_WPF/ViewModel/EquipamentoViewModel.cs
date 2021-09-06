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
using System.Windows.Navigation;

namespace SmartHome_WPF.ViewModel
{
    public class EquipamentoViewModel : ModelBase
    {
        #region Contrutores
        public EquipamentoViewModel()
        {
            equipamentoModel = new EquipamentoModel();
            Task.Run(async () =>
            {
                Comodos = await comodoSQL.GetComodos();
            });

            Task.Run(async () =>
            {
                TipoEquipamento = await tipoEquipamentoSQL.GetTipoEquipamento();
            });

            Task.Run(async () =>
            {
                Equipamentos = await equipamentoSQL.GetEquipamentos();
            });
        }
        #endregion

        private ComodoSQL comodoSQL = new ComodoSQL();
        private TipoEquipamentoSQL tipoEquipamentoSQL = new TipoEquipamentoSQL();
        private EquipamentoSQL equipamentoSQL = new EquipamentoSQL();

        #region Getters and Setters
        public ObservableCollection<ComodoModel> Comodos { get => comodos; set => Set(ref comodos, value); }
        public EquipamentoModel EquipamentoSelecionado 
        { 
            get => equipamentoSelecionado; 
            set {
                if (value != null)
                {
                    equipamentoModel = value;
                    IsEdit = true;
                }
                Set(ref equipamentoSelecionado, value);
            }
        }
        public EquipamentoModel equipamentoModel
        {
            get => _equipamentoModel;
            set => Set(ref _equipamentoModel, value);
        }
        public ObservableCollection<EquipamentoModel> Equipamentos { get => equipamentos; set => Set(ref equipamentos, value); }
        public ObservableCollection<TipoEquipamentoModel> TipoEquipamento { get => tipoEquipamento; set => Set(ref tipoEquipamento, value); }

        //public EquipamentoModel EquipamentoModel { get => equipamentoModel ?? (equipamentoModel = new EquipamentoModel()); set => Set(ref equipamentoModel, value); }
        #endregion


        private EquipamentoModel _equipamentoModel;
        private ObservableCollection<ComodoModel> comodos;
        private ObservableCollection<TipoEquipamentoModel> tipoEquipamento;
        private ObservableCollection<EquipamentoModel> equipamentos;
        private EquipamentoModel equipamentoSelecionado;
        private bool isEdit = false;


        public ICommand ButtonsCadastrarEquipamentos => new RelayCommand((object commandParameter) =>
        {
            string parametro = Convert.ToString(commandParameter);
            if(parametro == "Salvar")
            {
                Cadastrar();
            }
            else
            {
                Voltar();
            }

        });

        public ICommand ButtonsConsultarEquipamentos => new RelayCommand((object commandParameter) =>
        {
            string parametro = Convert.ToString(commandParameter);
            if (parametro == "Alterar")
            {
                Alterar();
            }
            else if(parametro == "Deletar")
            {
                Deletar();
            }
            else
            {
                Voltar();
            }

        });

        public bool IsEdit { get => isEdit; set => Set(ref isEdit, value); }

        public async void Cadastrar()
        {
           
            string message;
            MessageBoxResult result;

            if (validaEntrada(equipamentoModel))
            {
              
                try
                {
                    Boolean EquipamentoCadastrado = await equipamentoSQL.InserirEquipamento(equipamentoModel);
                    if (EquipamentoCadastrado == true)
                    {
                        message = "Equipamento cadastrado com sucesso!";
                        equipamentoModel = new EquipamentoModel();
                        result = MessageBox.Show(message);
                    }
                    else
                    {
                        message = "Equipamento já existente no banco de dados";
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
                message = "Campos inválidos, verifique se todos os campos foram preenchidos corretamente.";
                result = MessageBox.Show(message);
            }
        }
        public async void Alterar()
        {
            string message;
            MessageBoxResult resultadoAlteracao;

            if (validaEntrada(equipamentoModel))
            {
                try
                {
                    Boolean EquipamentoAlterado = await equipamentoSQL.AlterarEquipamento(equipamentoModel);
                    if (EquipamentoAlterado == true)
                    {
                        message = "Equipamento " + equipamentoModel.Nome_Equipamento + " alterado com sucesso!";
                        equipamentoModel = new EquipamentoModel();
                        IsEdit = false;
                        resultadoAlteracao = MessageBox.Show(message);
                    }
                    else
                    {
                        message = "Equipamento já existente no banco de dados";
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
            MessageBoxResult resultadoAlteracao;

            if (validaEntrada(equipamentoModel))
            {
                try
                {
                    Boolean EquipamentoAlterado = await equipamentoSQL.DeletarEquipamento(equipamentoModel);
                    if (EquipamentoAlterado == true)
                    {
                        message = "Equipamento " + equipamentoModel.Nome_Equipamento + " excluído com sucesso!";

                        equipamentoModel = new EquipamentoModel();

                        resultadoAlteracao = MessageBox.Show(message);
                    }
                    else
                    {
                        message = "Equipamento não existente no banco de dados";
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
        public Boolean validaEntrada(EquipamentoModel equipamento)
        {
            if(equipamento.Comodo != null &&
               equipamento.Nome_Equipamento != ""
               && equipamento.Id_Tipo_Equipamento != null)
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
