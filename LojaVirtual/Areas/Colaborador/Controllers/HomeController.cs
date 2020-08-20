using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Libraries.Filtro;
using LojaVirtual.Libraries.Login;
using LojaVirtual.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Areas.Colaborador.Controllers
{
    [Area("Colaborador")]
    public class HomeController : Controller
    {
        private IColaboradorRepository RepositoryColaborador;
        private LoginColaborador loginColaborador;

        public HomeController(IColaboradorRepository repository, LoginColaborador login)
        {
            RepositoryColaborador = repository;
            loginColaborador = login;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login([FromForm]Models.Colaborador colaborador)
        {
            Models.Colaborador forLogin = RepositoryColaborador.Login(colaborador.Email, colaborador.Senha);

            if (forLogin != null)
            {
                loginColaborador.Login(forLogin);

                return new RedirectResult(Url.Action(nameof(Painel)));
            }
            else
            {
                ViewData["MSG_E"] = "Colaborador não encotrado, verifique o E-mail e senha digitados!";

                return View();
            }
        }

        [ColaboradorAutorizacao]
        [ValidateHttpReferer]
        public IActionResult Logout()
        {
            loginColaborador.Logout();
            return RedirectToAction(nameof(Login));
        }

        public IActionResult RecuperarSenha()
        {
            return View();
        }
        public IActionResult CadastrarNovaSenha()
        {
            return View();
        }

        [ColaboradorAutorizacao]
        public IActionResult Painel()
        {
            return View();
        }
    }
}