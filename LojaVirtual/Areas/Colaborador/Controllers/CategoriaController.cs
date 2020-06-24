using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Libraries.Filtro;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LojaVirtual.Areas.Colaborador.Controllers
{
    [Area("Colaborador")]
    //[ColaboradorAutorizacao]
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

                TempData["MSG_S"] = "Categoria salva com sucesso!";

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categorias = categoriaRepository.ObterTodasCategorias().Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
            return View();
        }
        [HttpGet]
        public IActionResult Atualizar(int id)
        {
            return View();
        }
        [HttpPost]
        public IActionResult Atualizar([FromForm]Categoria categoria)
        {
            //TODO Implementar Logica
            return View();
        }
        [HttpGet]
        public IActionResult Excluir(int id)
        {
            return View();
        }

    }
}