using LojaVirtual.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.Login
{
    public class LoginColaborador
    {
        private string Key = "Login.Colaborador";
        private Sessao.Sessao session;

        public LoginColaborador(Sessao.Sessao sessao)
        {
            session = sessao;
        }

        public void Login(Colaborador colaborador)
        {
            //Serializar
            string colabstringJSON = JsonConvert.SerializeObject(colaborador);

            session.Cadastrar(Key, colabstringJSON);
        }

        public Colaborador GetColaborador()
        {
            if (session.Existe(Key))
            {
                //Deserializar
                string colabstringJSON = session.Consultar(Key);

                return JsonConvert.DeserializeObject<Colaborador>(colabstringJSON);
            }
            else
            {
                return null;
            }

        }

        public void Logout()
        {
            session.RemoverTodos();
        }
    }
}
