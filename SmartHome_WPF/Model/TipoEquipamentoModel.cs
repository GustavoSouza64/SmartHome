using SmartHome_WPF.Auxiliares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome_WPF.Model
{
    public class TipoEquipamentoModel : ModelBase
    {

        private int _id_tipoEquipamento;
        private string _descricao = string.Empty;

        public int Id_TipoEquipamento { get => _id_tipoEquipamento; set => Set(ref _id_tipoEquipamento, value); }
        public string DescricaoTipo
        {
            get { return _descricao; }
            set { Set(ref _descricao, value); }
        }
    }
}
