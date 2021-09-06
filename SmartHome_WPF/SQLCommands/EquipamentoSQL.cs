using SmartHome_WPF.Model;
using SmartHome_WPF.SQLServer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome_WPF.SQLCommands
{
    public class EquipamentoSQL
    {
        private Banco controller = new Banco();


        public async Task<ObservableCollection<EquipamentoModel>> GetEquipamentos()
        {
            EquipamentoModel equipamento = new EquipamentoModel();
            DataTable retornoEquipamentos = await controller.GetData("SP_retornaEquipamentos", CommandType.StoredProcedure);

            return await Task.Run(() =>
            {
                var colecaoEquipamentos = new ObservableCollection<EquipamentoModel>();
                foreach (DataRow row in retornoEquipamentos.Rows)
                {
   
                    colecaoEquipamentos.Add(new EquipamentoModel()
                    {
                        Id_Equipamento = Convert.ToInt32(row["id_Equipamento"]),
                        Nome_Equipamento = Convert.ToString(row["nome_Equipamento"]),
                        Comodo = new ComodoModel()
                        {
                            IdComodo = Convert.ToInt32(row["fk_Comodo"]),
                            Descricao = Convert.ToString(row["comodo_desc"])  
                        },
                        Id_Tipo_Equipamento = new TipoEquipamentoModel()
                        {
                            Id_TipoEquipamento = Convert.ToInt32(row["fk_TipoEquipamento"]),
                            DescricaoTipo = Convert.ToString(row["equip_desc"])
                        }
                  
                    });
                }
                return colecaoEquipamentos;
            }); ;
        }
        public async Task<Boolean> InserirEquipamento (EquipamentoModel equipamentoModel)
        {
            
            controller.LimparParametros();
            controller.AdicionarParametro(equipamentoModel.Nome_Equipamento, "@nome_Equipamento");
            controller.AdicionarParametro(equipamentoModel.Comodo.IdComodo, "@fk_Comodo");
            controller.AdicionarParametro(equipamentoModel.Id_Tipo_Equipamento.Id_TipoEquipamento, "@fk_TipoEquipamento");
            int retorno = await controller.ManipulacaoAsync(System.Data.CommandType.StoredProcedure, "SP_InsereEquipamento");
            if(retorno > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<Boolean> AlterarEquipamento(EquipamentoModel equipamentoModel)
        {
            
            controller.LimparParametros();
            controller.AdicionarParametro(equipamentoModel.Id_Equipamento, "@id_Equipamento");
            controller.AdicionarParametro(equipamentoModel.Nome_Equipamento, "@nome_Equipamento");
            controller.AdicionarParametro(equipamentoModel.Comodo.IdComodo, "@fk_Comodo");
            controller.AdicionarParametro(equipamentoModel.Id_Tipo_Equipamento.Id_TipoEquipamento, "@fk_TipoEquipamento");
            int retorno = await controller.ManipulacaoAsync(System.Data.CommandType.StoredProcedure, "SP_AlteraEquipamento");
            if (retorno > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<Boolean> DeletarEquipamento(EquipamentoModel equipamentoModel)
        {
            EquipamentoModel equipamento = new EquipamentoModel();
            controller.LimparParametros();
            controller.AdicionarParametro(equipamentoModel.Id_Equipamento, "@id_Equipamento");
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
    }
}
