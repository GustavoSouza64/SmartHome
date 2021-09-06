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
    public class UsuarioModel : ModelBase
    {
        private int _id_usuario;
        private string _login = string.Empty;
        private string _senha = string.Empty;
        private string _confirmaSenha = string.Empty;
        private string _nome_usuario = string.Empty;
        private int _tipo_usuario;
        private string _operacao = string.Empty;

        public int Id_Usuario {
            get { return _id_usuario; } 
            set { Set(ref _id_usuario, value); } 
        }

        public string Login
        {
            get { return _login; }
            set { Set(ref _login, value); }
        }
        public string Senha
        {
            get { return _senha; }
            set { Set(ref _senha, value); }
        }

        public string ConfirmaSenha
        {
            get { return _confirmaSenha; }
            set { Set(ref _confirmaSenha, value); }
        }
        public string Nome_Usuario
        {
            get { return _nome_usuario; }
            set { Set(ref _nome_usuario, value); }
        }
        public int Tipo_Usuario
        {
            get { return _tipo_usuario; }
            set { Set(ref _tipo_usuario, value); }
        }

        public string Operacao
        {
            get { return _operacao;  }
            set { Set(ref _operacao, value);  }
        }

    }
}
