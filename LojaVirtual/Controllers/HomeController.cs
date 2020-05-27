using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Libraries.Email;
using LojaVirtual.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text;
using LojaVirtual.Database;
using LojaVirtual.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;

namespace LojaVirtual.Controllers
{
    public class HomeController : Controller
    {
        private IClienteRepository RepositoryCliente;
        private INewsletterRepository NewsletterRepository;

        public HomeController(IClienteRepository clienteRepository,INewsletterRepository newsletterRepository)
        {
            RepositoryCliente = clienteRepository;
            NewsletterRepository = newsletterRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index([FromForm]NewsletterEmail newsletter)
        {
            if (ModelState.IsValid)
            {
                NewsletterRepository.Cadastrar(newsletter);
               
                TempData["MSG_S"] = "E-mail cadastrado! Agora você esta por dentro das promoções! ";
                
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }

        public IActionResult Contato()
        {
            return View();
        }
        public IActionResult ContatoAcao()
        {
            try
            {
                Contato contato = new Contato();
                contato.Nome = HttpContext.Request.Form["nome"];
                contato.Email = HttpContext.Request.Form["email"];
                contato.Texto = HttpContext.Request.Form["texto"];

                var listaMensagens = new List<ValidationResult>();
                var contexto = new ValidationContext(contato);
                bool isValid = Validator.TryValidateObject(contato, contexto, listaMensagens, true);

                if (isValid)
                {
                    ContatoEmail.EnviarContatoPorEmail(contato);

                    ViewData["MSG_S"] = "Mensagem de contato enviado com sucesso!";
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var texto in listaMensagens)
                    {
                        sb.Append(texto.ErrorMessage + "<br />");
                    }

                    ViewData["MSG_E"] = sb.ToString();
                    ViewData["CONTATO"] = contato;
                }

                
            }
            catch (Exception)
            {
                ViewData["MSG_E"] = "Opps! Tivemos um erro, tente novamente mais tarde!";

                //TODO - Implementar Log
            }
            

            return View("Contato");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login([FromForm]Cliente cliente)
        {
            if(cliente.Email == "guigui410@gmail.com" && cliente.Senha == "123456")
            {
                HttpContext.Session.Set("ID", new byte[] { 33 });
                HttpContext.Session.SetString("Email", cliente.Email);
                HttpContext.Session.SetInt32("Idade", 23);

                return new ContentResult() { Content = "Logado!" };
            }
            else
            {
                return new ContentResult() { Content = "Não Logado" };
            }
        }

        [HttpGet]
        public IActionResult Painel()
        {
            byte[] UsuarioID;

            if (HttpContext.Session.TryGetValue("ID", out UsuarioID))
            {
                return new ContentResult() { Content = "Usuario do ID:" + UsuarioID[0] + 
                    " Email:" + HttpContext.Session.GetString("Email") + 
                    " Idade:" + HttpContext.Session.GetInt32("Idade") +
                    " esta logado."};
            }
            else
            {
                return new ContentResult() { Content = "Acesso negado!" };
            }
        }

        [HttpGet]
        public IActionResult CadastroCliente()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CadastroCliente([FromForm]Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                RepositoryCliente.Cadastrar(cliente);

                //TODO verificar porque não esta funcionado a mensagem de sucesso
                TempData["MSG_S"] = "Cadastro realizado com sucesso!";

                return RedirectToAction(nameof(CadastroCliente));
            }
            return View();
        }

        public IActionResult CarrinhoCompras()
        {
            return View();
        }
    }
}