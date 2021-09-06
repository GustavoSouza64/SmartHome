using Caliburn.Micro;
using SmartHome_WPF.Model;
using SmartHome_WPF.SQLServer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace SmartHome_WPF.SQLCommands
{
    public class ComodoSQL
    {
       

        private Banco controller = new Banco();
        public async Task<ObservableCollection<ComodoModel>> GetComodos()
        {
            ComodoModel comodos = new ComodoModel();
            DataTable retornoComodos = await controller.GetData("SP_retornaComodos", CommandType.StoredProcedure);


            return await Task.Run(() =>
            {
                var colecaoComodos = new ObservableCollection<ComodoModel>();
                foreach(DataRow row in retornoComodos.Rows)
                {

                    colecaoComodos.Add(new ComodoModel()
                    {
                        IdComodo = Convert.ToInt32(row["id_Comodo"]),
                        Residencia = new ResidenciaModel()
                        {
                           Id_Residencia = Convert.ToInt32(row["fk_Residencia"]),
                           Nome_Residencia = Convert.ToString(row["nome_residencia"])
                        },
                        Descricao = Convert.ToString(row["descricao"])
                    });;
                }

                return colecaoComodos;
            });
        }

        public async Task<Boolean> InserirComodo(ComodoModel comodoModel)
        {
            ComodoModel comodo = new ComodoModel();
            controller.LimpaDados();
            controller.AdicionarParametro(comodoModel.Descricao, "@descricao");
            controller.AdicionarParametro(comodoModel.Residencia.Id_Residencia, "@fk_Residencia");
            int retorno = await controller.ManipulacaoAsync(System.Data.CommandType.StoredProcedure, "SP_InsereComodo");
            if(retorno > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<Boolean> AlterarComodo(ComodoModel comodoModel)
        {
            ComodoModel comodo = new ComodoModel();
            controller.LimpaDados();
            controller.AdicionarParametro(comodoModel.Descricao, "@descricao");
            controller.AdicionarParametro(comodoModel.Residencia.Id_Residencia, "@fk_Residencia");
            controller.AdicionarParametro(comodoModel.IdComodo, "@id_Comodo");
            int retorno = await controller.ManipulacaoAsync(System.Data.CommandType.StoredProcedure, "SP_AlteraComodo");
            if (retorno > 0) 
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<Boolean> DeletaComodo(ComodoModel comodoModel)
        {
            string connetionString;
            SqlConnection cnn;
            connetionString = @"Data Source=LAPTOP-9HVCT4FE\SQLEXPRESS;Initial Catalog=SMARTHOME;User ID=admin;Password=admin";
            cnn = new SqlConnection(connetionString);

            SqlCommand command = new SqlCommand("EXEC SP_DeletaComodo " + comodoModel.IdComodo + ";", cnn);
            command.CommandType = CommandType.Text;
            cnn.Close();
            cnn.Open();
            SqlDataReader reader = command.ExecuteReader();

            string idEquipamento = "";
            while (reader.Read())
            {
                idEquipamento = reader["id_Equipamento"].ToString();

                if (idEquipamento != "")
                    await DeletarEquipamento(idEquipamento);
            }
            if (idEquipamento != "")
                reader = command.ExecuteReader();

            reader.Close();
            return true;
        }

        public async Task<Boolean> DeletarEquipamento(string id_equipamento)
        {
            EquipamentoModel equipamento = new EquipamentoModel();
            controller.LimparParametros();
            controller.AdicionarParametro(id_equipamento, "@id_Equipamento");
            int retorno = await controller.ManipulacaoAsync(System.Data.CommandType.StoredProcedure, "SP_DeletaEquipamento");
            if (retorno > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #region Não utilizado
        public async Task<ObservableCollection<ComodoModel>> GetAll()
        {
            return await Task.Run(() =>
            {
                var colecao = new ObservableCollection<ComodoModel>();
                colecao.Add(new ComodoModel()
                {
                    Descricao = "Sala"
                });
                colecao.Add(new ComodoModel()
                {
                    Descricao = "Quarto"
                });
                colecao.Add(new ComodoModel()
                {
                    Descricao = "Cozinha"
                });
                return colecao;
            });
        }
        #endregion
    }
}
