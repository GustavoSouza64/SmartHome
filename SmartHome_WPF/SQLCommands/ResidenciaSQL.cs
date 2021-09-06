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
    public class ResidenciaSQL
    {
        private Banco controller = new Banco();
        public async Task<ObservableCollection<ResidenciaModel>> GetResidencias()
        {
            ResidenciaModel residencia = new ResidenciaModel();
            DataTable retornoResidencias = await controller.GetData("SP_retornaResidencias", CommandType.StoredProcedure);


            return await Task.Run(() =>
            {
                var colecaoResidencias = new ObservableCollection<ResidenciaModel>();
                foreach (DataRow row in retornoResidencias.Rows)
                {
                    int index = retornoResidencias.Rows.IndexOf(row);
                    colecaoResidencias.Add(new ResidenciaModel()
                    {
                        Id_Residencia = Convert.ToInt32(retornoResidencias.Rows[index]["id_Residencia"]),
                        Nome_Residencia = Convert.ToString(retornoResidencias.Rows[index]["nome_Residencia"]),
                        CEP = Convert.ToString(retornoResidencias.Rows[index]["cep"]),
                        NumeroResidencia = Convert.ToInt32(retornoResidencias.Rows[index]["numero_Residencia"])
                    }); ;
                }

                return colecaoResidencias;
            });
        }
    }
}
