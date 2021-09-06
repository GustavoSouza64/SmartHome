using SmartHome_WPF.Auxiliares;
using SmartHome_WPF.Classes_auxiliares;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Packaging;
using System.Linq;
using System.Net.Mail;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome_WPF.Model
{
    public class DashboardModel : ModelBase
    {
        private string _mes = string.Empty;
        private double _kwh = 0;
        private double _valor = 0;

        public string Mes 
        {
            get { return _mes; } 
            set { Set(ref _mes, value); } 
        }

        public double Kwh
        {
            get { return _kwh; }
            set { Set(ref _kwh, value); }
        }
        public double Valor
        {
            get { return _valor; }
            set { Set(ref _valor, value); }
        }
    }
}
