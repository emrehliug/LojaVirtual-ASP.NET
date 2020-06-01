using LojaVirtual.Database;
using LojaVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using LojaVirtual.Repositories.Interfaces;

namespace LojaVirtual.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private LojaVirtualContext banco;
        public ClienteRepository(LojaVirtualContext DIbanco)
        {
            banco = DIbanco;
        }
        public void Atualizar(Cliente cliente)
        {
            banco.Update(cliente);
            banco.SaveChanges();
        }

        public void Cadastrar(Cliente cliente)
        {
            banco.Add(cliente);
            banco.SaveChanges();
        }

        public void Excluir(int Id)
        {
            Cliente cliente = ObterCliente(Id);
            banco.Remove(cliente);
            banco.SaveChanges();
        }

        public Cliente Login(string email, string senha)
        {
            Cliente cliente = banco.Clientes.Where(x => x.Email == email && x.Senha == senha).FirstOrDefault();
            return cliente;
        }

        public Cliente ObterCliente(int Id)
        {
            return banco.Clientes.Find(Id);
        }

        public IEnumerable<Cliente> TodosClientes()
        {
            return banco.Clientes.ToList();
        }
    }
}
