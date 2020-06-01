using LojaVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LojaVirtual.Libraries.Login
{
    public class LoginCliente
    {
        private string Key = "Login.Cliente";
        private Sessao.Sessao session;

        public LoginCliente(Sessao.Sessao sessao)
        {
            session = sessao;
        }

        public void Login(Cliente cliente)
        {
            //Serializar
            string clientestringJSON = JsonConvert.SerializeObject(cliente);

            session.Cadastrar(Key,clientestringJSON);
        }

        public Cliente GetCliente()
        {
            if (session.Existe(Key))
            {
                //Deserializar
                string clientestringJSON = session.Consultar(Key);

                return JsonConvert.DeserializeObject<Cliente>(clientestringJSON);
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
