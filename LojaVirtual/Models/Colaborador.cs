using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Models
{
    public class Colaborador
    {
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Display(Name = "Código")]
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }


        public string ConfirmaSenha { get; set; }
        //prop tipo tem como objetivo definir C = Comum / G = Gerente 
        public string Tipo { get; set; }
    }
}
