using LojaVirtual.Models;
using LojaVirtual.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.Validacao
{
    public class EmailUnicoColaboradorAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string Email = (value as string).Trim();

            IColaboradorRepository colaboradorRepository = (IColaboradorRepository)validationContext.GetService(typeof(IColaboradorRepository));

            List<Colaborador> Colaboradores = colaboradorRepository.ObterColaboradorPorEmail(Email);

            Colaborador objColaborador = (Colaborador)validationContext.ObjectInstance;

            if(Colaboradores.Count > 1)
            {
                return new ValidationResult("E-mail ja existente entre nossos colaboradores!");
            }
            if(Colaboradores.Count == 1 && objColaborador.Id != Colaboradores[0].Id)
            {
                return new ValidationResult("E-mail ja existente entre nossos colaboradores!");
            }

            return ValidationResult.Success;
        }
    }
}
