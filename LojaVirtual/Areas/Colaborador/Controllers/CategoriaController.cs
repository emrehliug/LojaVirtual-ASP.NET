using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Libraries.Filtro;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

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

        public IActionResult Index()
        {
            IEnumerable<Categoria> ListaCategorias = categoriaRepository.TodasCategorias();
            return View(ListaCategorias);
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Cadastrar([FromForm]Categoria categoria)
        {
            //TODO Implementar Logica
            return View();
        }
        [HttpGet]
        public IActionResult Atualizar()
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