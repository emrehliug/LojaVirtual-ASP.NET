using LojaVirtual.Database;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Repositories
{
    public class ColaboradorRepository : IColaboradorRepository
    {
        private LojaVirtualContext banco;

        public ColaboradorRepository(LojaVirtualContext _banco)
        {
            banco = _banco;
        }
        public void Atualizar(Colaborador colaborador)
        {
            banco.Update(colaborador);
            banco.SaveChanges();
        }

        public void Cadastrar(Colaborador colaborador)
        {
            banco.Add(colaborador);
            banco.SaveChanges();
        }

        public void Excluir(int Id)
        {
            Colaborador colaborador = ObterColaborador(Id);
            banco.Remove(colaborador);
            banco.SaveChanges();
        }

        public Colaborador Login(string email, string senha)
        {
            Colaborador colaborador = banco.Colaboradores.Where(x => x.Email == email && x.Senha == senha).FirstOrDefault();
            return colaborador;
        }

        public Colaborador ObterColaborador(int Id)
        {
            return banco.Colaboradores.Find(Id);
        }

        public IEnumerable<Colaborador> TodosColaboradores()
        {
            return banco.Colaboradores.ToList();
        }
    }
}
