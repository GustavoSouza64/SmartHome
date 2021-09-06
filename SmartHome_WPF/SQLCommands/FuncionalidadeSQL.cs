using SmartHome_WPF.Model;
using SmartHome_WPF.SQLServer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome_WPF.SQLCommands
{
    public class FuncionalidadeSQL
    {
        private Banco controller = new Banco();

        public async Task<ObservableCollection<FuncionalidadesModel>> GetFuncionalidades()
        {
            FuncionalidadesModel funcionalidade = new FuncionalidadesModel();
            DataTable retornaFuncionalidades = await controller.GetData("SP_RetornaFuncionalidades", CommandType.StoredProcedure);

            return await Task.Run(() =>
            {
                var colecaoFuncionalidades = new ObservableCollection<FuncionalidadesModel>();
                foreach (DataRow row in retornaFuncionalidades.Rows)
                {
                    colecaoFuncionalidades.Add(new FuncionalidadesModel()
                    {
                        IdFuncionalidade = Convert.ToInt32(row["id_Funcionalidade"]),
                        NomeFuncionalidade = Convert.ToString(row["nome_Funcionalidade"]),
                        Equipamento = new EquipamentoModel()
                        {
                            Id_Equipamento = Convert.ToInt32(row["fk_Equipamento"]),
                            Nome_Equipamento = Convert.ToString(row["nome_Equipamento"])
                        },
                        Usuario = new UsuarioModel()
                        {
                            Id_Usuario = Convert.ToInt32(row["fk_Usuario"])
                        }
                    });
                }
                return colecaoFuncionalidades;
            }); ;
        }

        public async Task<Boolean> InserirFuncionalidade(FuncionalidadesModel funcionalidadesModel)
        {
            controller.LimparParametros();
            controller.AdicionarParametro(funcionalidadesModel.NomeFuncionalidade, "@NomeFuncionalidade");
            controller.AdicionarParametro(funcionalidadesModel.Equipamento.Id_Equipamento, "@fk_Equipamento");
            controller.AdicionarParametro(funcionalidadesModel.IdUsuario, "@fk_Usuario");
            int retorno = await controller.ManipulacaoAsync(System.Data.CommandType.StoredProcedure, "SP_InsereFuncionalidade");
            if (retorno > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<Boolean> AlterarFuncionalidade(FuncionalidadesModel funcionalidadesModel)
        {
            controller.LimparParametros();
            controller.AdicionarParametro(funcionalidadesModel.IdFuncionalidade, "@IdFuncionalidade");
            controller.AdicionarParametro(funcionalidadesModel.NomeFuncionalidade, "@NomeFuncionalidade");
            controller.AdicionarParametro(funcionalidadesModel.Equipamento.Id_Equipamento, "@fk_Equipamento");
            int retorno = await controller.ManipulacaoAsync(System.Data.CommandType.StoredProcedure, "SP_AlteraFuncionalidade");
            if (retorno > 0)
            {
                return true;
            }
            else
            {
                return true;
            }
        }
    }
}
