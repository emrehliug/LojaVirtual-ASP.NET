using LojaVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Repositories.Interfaces
{
    public interface IColaboradorRepository
    {
        Colaborador Login(string email, string senha);

        void Cadastrar(Colaborador colaborador);
        void Atualizar(Colaborador colaborador);
        void Excluir(int Id);


        Colaborador ObterColaborador(int Id);
        IEnumerable<Colaborador> TodosColaboradores();
    }
}
