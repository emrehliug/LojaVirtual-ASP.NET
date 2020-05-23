using LojaVirtual.Database;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Repositories
{
    public class NewsletterRepository : INewsletterRepository
    {
        private LojaVirtualContext banco;
        public NewsletterRepository(LojaVirtualContext _banco)
        {
            banco = _banco;
        }
        public void Cadastrar(NewsletterEmail newsletter)
        {
            banco.NewsletterEmails.Add(newsletter);
            banco.SaveChanges();
        }

        public IEnumerable<NewsletterEmail> ObterTodasNewsletter()
        {
            return banco.NewsletterEmails.ToList();
        }
    }
}
