using SmartHome_WPF.Auxiliares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome_WPF.Model
{
    public class ComodoModel : ModelBase
    {
        private int _idComodo;
        private string _descricao = string.Empty;
        private ResidenciaModel residencia;
        private int _idResidencia;
        private string _operacao = string.Empty;

        public int IdComodo { get => _idComodo; set => Set(ref _idComodo, value); }
        public string Descricao { get => _descricao; set => Set(ref _descricao, value); }
        public ResidenciaModel Residencia { get => residencia ?? (residencia = new ResidenciaModel()); set => Set(ref residencia, value); }
        public int IdResidencia { get => residencia.Id_Residencia; set => Set(ref _idResidencia, value); }

        public string Operacao
        {
            get { return _operacao; }
            set { Set(ref _operacao, value); }
        }
    }
}
