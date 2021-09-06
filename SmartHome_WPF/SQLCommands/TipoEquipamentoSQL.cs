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
    public class TipoEquipamentoSQL
    {

        private Banco controller = new Banco();
        public async Task<ObservableCollection<TipoEquipamentoModel>> GetTipoEquipamento()
        {
            TipoEquipamentoModel tipoEquipamento = new TipoEquipamentoModel();
            DataTable retornoComodos = await controller.GetData("SP_retornaTipoEquipamento", CommandType.StoredProcedure);

            return await Task.Run(() =>
            {
                var colecaoTipoEquipamento = new ObservableCollection<TipoEquipamentoModel>();
                foreach (DataRow row in retornoComodos.Rows)
                {
                    int index = retornoComodos.Rows.IndexOf(row);
                    colecaoTipoEquipamento.Add(new TipoEquipamentoModel()
                    {
                        Id_TipoEquipamento = Convert.ToInt32(retornoComodos.Rows[index]["id_TipoEquipamento"]),
                        DescricaoTipo = Convert.ToString(retornoComodos.Rows[index]["descricao"])  
                    }); ;
                }
                return colecaoTipoEquipamento;
            });
        }
    }
}
