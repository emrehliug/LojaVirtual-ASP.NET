using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.Sessao
{
    public class Sessao 
    {
        IHttpContextAccessor Context;

        public Sessao(IHttpContextAccessor context)
        {
            Context = context;
        }


        //CRUD para usar Session
        public void Cadastrar(string Key, string Value)
        {
            Context.HttpContext.Session.SetString(Key, Value);
        }

        public void Atualizar(string Key, string Value)
        {
            if (Existe(Key))
            {
                Remover(Key);
            }
            Context.HttpContext.Session.SetString(Key, Value);
        }

        public void Remover(string Key)
        {
            Context.HttpContext.Session.Remove(Key);
        }

        public string Consultar(string Key)
        {
            return Context.HttpContext.Session.GetString(Key);
        }

        public bool Existe(string Key)
        {
            if(Context.HttpContext.Session.GetString(Key) == null)
            {
                return false;
            }

            return true;
        }

        public void RemoverTodos()
        {

        }
    }
}
