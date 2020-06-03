using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Libraries.Login;
using LojaVirtual.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LojaVirtual.Libraries.Filtro
{
    /*
     * Tipos de Filtros
     * 
     * - Autorização
     * - Recurso
     * - Ação
     * - Exceção
     * - Resultado
     * 
     */
    public class ClienteAutorizacaoAttribute : Attribute, IAuthorizationFilter
    {
        LoginCliente login;
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            login = (LoginCliente)context.HttpContext.RequestServices.GetService(typeof(LoginCliente));

            Cliente cliente = login.GetCliente();

            if (cliente == null)
            {
                context.Result = new ContentResult() { Content = "Acesso negado!" };
            }
        }
    }
}
