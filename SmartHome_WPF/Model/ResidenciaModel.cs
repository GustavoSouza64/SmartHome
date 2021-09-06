using SmartHome_WPF.Auxiliares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome_WPF.Model
{
    public class ResidenciaModel : ModelBase
    {
        private int _idResidencia;
        private string _nomeResidencia = string.Empty;
        private string _cep = string.Empty;
        private int _numeroResidencia;

        public int Id_Residencia
        {
            get { return _idResidencia; }
            set { Set(ref _idResidencia, value); }
        }

        public string Nome_Residencia
        {
            get { return _nomeResidencia; }
            set { Set(ref _nomeResidencia, value); }
        }

        public string CEP
        {
            get { return _cep; }
            set { Set(ref _cep, value); }
        }

        public int NumeroResidencia
        {
            get { return _numeroResidencia; }
            set { Set(ref _numeroResidencia, value); }
        }
    }
}
