using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Libraries.Filtro;
using LojaVirtual.Libraries.Lang;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LojaVirtual.Areas.Colaborador.Controllers
{
    [Area("Colaborador")]
    [ColaboradorAutorizacao]
    public class CategoriaController : Controller
    {
        private ICategoriaRepository categoriaRepository;

        public CategoriaController(ICategoriaRepository _categoriaRepository)
        {
            categoriaRepository = _categoriaRepository;
        }

        public IActionResult Index(int? pagina)
        {
            var categoriasPorPagina = categoriaRepository.TodasCategorias(pagina);
            return View(categoriasPorPagina);
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            ViewBag.Categorias = categoriaRepository.ObterTodasCategorias().Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
            return View();
        }
        [HttpPost]
        public IActionResult Cadastrar([FromForm]Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                categoriaRepository.Cadastrar(categoria);

                TempData["MSG_S"] = Mensagem.MSG_C001;

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categorias = categoriaRepository.ObterTodasCategorias().Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
            return View();
        }
        [HttpGet]
        public IActionResult Atualizar(int id)
        {
            var categoria = categoriaRepository.ObterCategoria(id);
            ViewBag.Categorias = categoriaRepository.ObterTodasCategorias().Where(a=> a.Id != id).Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
            return View(categoria);
        }
        [HttpPost]
        public IActionResult Atualizar([FromForm] Categoria categoria, int id)
        {
            if (ModelState.IsValid)
            {
                categoriaRepository.Atualizar(categoria);
                
                TempData["MSG_S"] = Mensagem.MSG_A001;

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categorias = categoriaRepository.ObterTodasCategorias().Where(a => a.Id != id).Select(a => new SelectListItem(a.Nome, a.Id.ToString()));

            return View();
        }
        [HttpGet]
        [ValidateHttpReferer]
        public IActionResult Excluir(int id)
        {
            categoriaRepository.Excluir(id);
            TempData["MSG_S"] = Mensagem.MSG_R001;
            
            return RedirectToAction(nameof(Index));
        }

    }
}