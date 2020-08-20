using LojaVirtual.Libraries.Email;
using LojaVirtual.Libraries.Filtro;
using LojaVirtual.Libraries.GeradorSenha;
using LojaVirtual.Libraries.Lang;
using LojaVirtual.Models.Constants;
using LojaVirtual.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace LojaVirtual.Areas.Colaborador.Controllers
{  
    [Area("Colaborador")]
    [ColaboradorAutorizacao(ColaboradorTipoConstant.Gerente)]
    public class ColaboradorController : Controller
    {
        private IColaboradorRepository colaboradorRepository;
        private GerenciarEmail GerenciarEmail;

        public ColaboradorController(IColaboradorRepository repository, GerenciarEmail gerenciarEmail)
        {
            colaboradorRepository = repository;
            GerenciarEmail = gerenciarEmail;
        }

        public IActionResult Index(int? pagina)
        {
            IPagedList<Models.Colaborador> colaboradores = colaboradorRepository.ObterTodosColaboradores(pagina);
            return View(colaboradores);
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar([FromForm]Models.Colaborador colaborador)
        {
            ModelState.Remove("Senha");
            if (ModelState.IsValid)
            {
                colaborador.Tipo = ColaboradorTipoConstant.Comum;
                colaborador.Senha = GeradorDeSenha.ObterSenhaUnica(8);
                colaboradorRepository.Cadastrar(colaborador);

                GerenciarEmail.EnviarSenhaNovoCadastro(colaborador);

                TempData["MSG_S"] = Mensagem.MSG_C001;

                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        [ValidateHttpReferer]
        public IActionResult GerarSenha(int id)
        {
            var colaborador = colaboradorRepository.ObterColaborador(id);
            colaborador.Senha = GeradorDeSenha.ObterSenhaUnica(8);
            colaboradorRepository.AtualizarSenha(colaborador);

            GerenciarEmail.EnviarSenhaNovaPorEmail(colaborador);

            TempData["MSG_S"] = Mensagem.MSG_EMAILSENHA;

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Atualizar(int id)
        {
            Models.Colaborador colaborador = colaboradorRepository.ObterColaborador(id);
            return View(colaborador);
        }

        [HttpPost]
        public IActionResult Atualizar([FromForm] Models.Colaborador colaborador, int id)
        {
            ModelState.Remove("Senha");
            if (ModelState.IsValid)
            {
                colaboradorRepository.Atualizar(colaborador);

                TempData["MSG_S"] = Mensagem.MSG_A001;

                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        [ValidateHttpReferer]
        public IActionResult Excluir(int id)
        {
            colaboradorRepository.Excluir(id);

            TempData["MSG_S"] = Mensagem.MSG_R001;

            return RedirectToAction(nameof(Index));
        }
    }
}
