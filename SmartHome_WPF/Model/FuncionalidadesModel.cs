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
    public class FuncionalidadesModel : ModelBase
    {
        #region constructor
        public FuncionalidadesModel(int _idFuncionalidade, string _nomeFuncionalidade, string _nomeComodo) {

            this.NomeFuncionalidade = _nomeFuncionalidade;
            this.NomeComodo = _nomeComodo;
            this.IdFuncionalidade = _idFuncionalidade;
        }

        public FuncionalidadesModel() { }
        

        #endregion
        public string NomeFuncionalidade { get; set; }
        public string NomeComodo { get; set; }
        public int IdFuncionalidade { get; set; }
        public EquipamentoModel Equipamento { get; set; }
        public UsuarioModel Usuario { get; set; }
        public string IdUsuario { get; set; }
    }
}
