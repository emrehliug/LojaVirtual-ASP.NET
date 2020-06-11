using LojaVirtual.Database;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace LojaVirtual.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        const int numeroRegistroPagina = 20;
        private LojaVirtualContext banco;
        public CategoriaRepository(LojaVirtualContext context)
        {
            banco = context;
        }

        public void Atualizar(Categoria categoria)
        {
            banco.Update(categoria);
            banco.SaveChanges();
        }

        public void Cadastrar(Categoria categoria)
        {
            banco.Add(categoria);
            banco.SaveChanges();
        }

        public void Excluir(int Id)
        {
            Categoria categoria = ObterCategoria(Id);
            banco.Remove(categoria);
            banco.SaveChanges();
        }

        public Categoria ObterCategoria(int Id)
        {
            return banco.Categorias.Find(Id);
        }

        IPagedList<Categoria> ICategoriaRepository.TodasCategorias(int? pagina)
        {
            int NumeroPagina = pagina ?? 1;
            return banco.Categorias.ToPagedList<Categoria>(NumeroPagina, numeroRegistroPagina);
        }
    }
}
