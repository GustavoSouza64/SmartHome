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
    public class RelatorioModel : ModelBase
    {
        #region constructor
        public RelatorioModel(string mes, string kwh, string valor) {

            this.Mes = mes;
            this.Kwh = kwh;
            this.Valor = valor;
        }

        #endregion
        public string Mes { get; set; }
        public string Kwh { get; set; }
        public string Valor { get; set; }
    }
}
