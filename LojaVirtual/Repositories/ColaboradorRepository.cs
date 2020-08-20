using LojaVirtual.Database;
using LojaVirtual.Models;
using LojaVirtual.Models.Constants;
using LojaVirtual.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace LojaVirtual.Repositories
{
    public class ColaboradorRepository : IColaboradorRepository
    {
        private IConfiguration conf;
        private LojaVirtualContext banco;

        public ColaboradorRepository(LojaVirtualContext _banco, IConfiguration configuration)
        {
            banco = _banco;
            conf = configuration;
        }
        public void Atualizar(Colaborador colaborador)
        {
            banco.Update(colaborador);
            banco.Entry(colaborador).Property(a => a.Senha).IsModified = false;
            banco.SaveChanges();
        }

        public void AtualizarSenha(Colaborador colaborador)
        {
            banco.Update(colaborador);
            banco.Entry(colaborador).Property(a => a.Nome).IsModified = false;
            banco.Entry(colaborador).Property(a => a.Email).IsModified = false;
            banco.Entry(colaborador).Property(a => a.Tipo).IsModified = false;
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

        public List<Colaborador> ObterColaboradorPorEmail(string email)
        {
            return banco.Colaboradores.Where(x => x.Email == email).AsNoTracking().ToList();
        }

        public IPagedList<Colaborador> ObterTodosColaboradores(int? pagina)
        {
            int numeroPaginas = pagina ?? 1;
            return banco.Colaboradores.Where(x => x.Tipo != ColaboradorTipoConstant.Gerente).ToPagedList<Colaborador>(numeroPaginas, conf.GetValue<int>("RegistroPorPagina"));
        }

    }
}
