using SmartHome_WPF.Auxiliares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome_WPF.Model
{
    public class EquipamentoModel : ModelBase
    {
        private int _id_equipamento;
        private ComodoModel comodo;
        private TipoEquipamentoModel tipoEquipamento;
        private string _nome_equipamento = string.Empty;
        private string _operacao = string.Empty;

        public int Id_Equipamento
        {
            get { return _id_equipamento; }
            set { Set(ref _id_equipamento, value); }
        }

        public ComodoModel Comodo
        {
            get { return comodo ?? (comodo = new ComodoModel());}
            set { Set(ref comodo, value); }
        }

        public TipoEquipamentoModel Id_Tipo_Equipamento
        {
            get { return tipoEquipamento ?? (tipoEquipamento = new TipoEquipamentoModel()); }
            set { Set(ref tipoEquipamento, value); }
        }
        

        public string Nome_Equipamento
        {
            get { return _nome_equipamento; }
            set { Set(ref _nome_equipamento, value); }
        }

        public string Operacao
        {
            get { return _operacao; }
            set { Set(ref _operacao, value); }
        }
    }
}
