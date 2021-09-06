using SmartHome_WPF.Model;
using SmartHome_WPF.SQLServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome_WPF.SQLCommands
{
    public class UsuarioSQL
    {
        private Banco controller = new Banco();

        public async Task<UsuarioModel> RealizarLoginAsync(UsuarioModel usuarioModel)
        {
            UsuarioModel usuario = new UsuarioModel();
            controller.LimpaDados();
            controller.AdicionarDado(usuarioModel.Login, "@login");
            controller.AdicionarDado(usuarioModel.Senha, "@senha");
            DataTable retornoConsulta = await controller.GetData("SP_AUTENTICA", CommandType.StoredProcedure);
            if (retornoConsulta.Rows.Count > 0)
            {
                usuario.Id_Usuario = Convert.ToInt32(retornoConsulta.Rows[0]["id_Usuario"]);
                usuario.Login = Convert.ToString(retornoConsulta.Rows[0]["login"]);
                usuario.Senha = Convert.ToString(retornoConsulta.Rows[0]["senha"]);
                usuario.Nome_Usuario = Convert.ToString(retornoConsulta.Rows[0]["nome_Usuario"]);
                usuario.Tipo_Usuario = Convert.ToInt32(retornoConsulta.Rows[0]["tipo_Usuario"]);
                return usuario;
            }
            else
            {
                return null;
            }
        }

        public async Task<Boolean> InserirUsuario (UsuarioModel usuarioModel)
        {
            UsuarioModel usuario = new UsuarioModel();
            string operacao = "i";
            int tipo = 1;
            usuarioModel.Tipo_Usuario = tipo;
            usuarioModel.Operacao = operacao;
            controller.LimpaDados();
            controller.AdicionarParametro(usuarioModel.Login, "@login");
            controller.AdicionarParametro(usuarioModel.Senha, "@senha");
            controller.AdicionarParametro(usuarioModel.Nome_Usuario, "@nome_Usuario");
            controller.AdicionarParametro(usuarioModel.Tipo_Usuario, "@tipo_Usuario");
            controller.AdicionarParametro(usuarioModel.Operacao, "@operacao"); 
            int Retorno = await controller.ManipulacaoAsync(System.Data.CommandType.StoredProcedure, "sp_inserirUsuario");
            if(Retorno > 0)
                return true;
            else
                return false;
        }
    }
}
