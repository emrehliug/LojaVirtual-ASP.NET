using LojaVirtual.Libraries.Login;
using LojaVirtual.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.Filtro
{
    public class ColaboradorAutorizacaoAttribute : Attribute, IAuthorizationFilter
    {
        LoginColaborador login;
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            login = (LoginColaborador)context.HttpContext.RequestServices.GetService(typeof(LoginColaborador));

            Colaborador colaborador = login.GetColaborador();

            if (colaborador == null)
            {
                context.Result = new RedirectToActionResult("Login","Home",null);
            }
        }
    }
}
