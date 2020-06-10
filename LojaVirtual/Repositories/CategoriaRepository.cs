using LojaVirtual.Database;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
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

        public IEnumerable<Categoria> TodasCategorias()
        {
            return banco.Categorias.ToList();
        }
    }
}
