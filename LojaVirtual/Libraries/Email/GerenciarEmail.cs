﻿using LojaVirtual.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.Email
{
    public class GerenciarEmail
    {
        private IConfiguration _configuration;
        private SmtpClient _smtp;

        public GerenciarEmail(SmtpClient smtp, IConfiguration configuration)
        {
            _smtp = smtp;
            _configuration = configuration;
        }

        public void EnviarContatoPorEmail(Contato contato)
        {
         
            string corpoMsg = string.Format("<h2>Contato - LojaVirtual</h2>" +
                "<b>Nome: </b> {0} <br />" +
                "<b>E-mail: </b> {1} <br />" +
                "<b>Texto: </b> {2} <br />" +
                "<br /> E-mail enviado automaticamente do site LojaVirtual.",
                contato.Nome,
                contato.Email,
                contato.Texto
            );

            /*
             * MailMessage -> Construir a mensagem
             */
            MailMessage mensagem = new MailMessage();
            mensagem.From = new MailAddress(_configuration.GetValue<string>("Email:UserName"));
            mensagem.To.Add(_configuration.GetValue<string>("Email:UserName"));
            mensagem.Subject = "Contato - LojaVirtual - E-mail: " + contato.Email;
            mensagem.Body = corpoMsg;
            mensagem.IsBodyHtml = true;

            //Enviar Mensagem via SMTP
            _smtp.Send(mensagem);
        }

        public void EnviarSenhaNovaPorEmail(Colaborador colaborador)
        {
            string corpoMsg = string.Format("<h2>Nova Senha Colaborador - LojaVirtual</h2>" +
                "Foi solicitado e gerado uma nova senha para seu acesso ao Painel da Loja. <br />" +
                "Sua nova senha é:" +
                "<h3>{0}</h3>" +
                "<br /> E-mail enviado automaticamente do site LojaVirtual.<br />Quaisquer duvida, entre em contato com seu superior direto.",
                colaborador.Senha
            );

            MailMessage mensagem = new MailMessage();
            mensagem.From = new MailAddress(_configuration.GetValue<string>("Email:UserName"));
            mensagem.To.Add(colaborador.Email);
            mensagem.Subject = "Colaborador:"+ colaborador.Nome +" - LojaVirtual - Senha";
            mensagem.Body = corpoMsg;
            mensagem.IsBodyHtml = true;

            //Enviar Mensagem via SMTP
            _smtp.Send(mensagem);
        }

        public void EnviarSenhaNovoCadastro(Colaborador colaborador)
        {
            string corpoMsg = string.Format("<h2>Seja bem vindo Colaborador - LojaVirtual</h2>" +
                "Você foi aceito e selecionado para fazer parte da nossa compania, foi gerado uma senha para seu acesso ao portal do Colaborador. <br />" +
                "Utilize seu E-mail e senha para logar <br />" +
                "Sua senha é:" +
                "<h3>{0}</h3>" +
                "<br /> E-mail enviado automaticamente do site LojaVirtual.<br />Quaisquer duvida, entre em contato com seu superior direto.",
                colaborador.Senha
            );

            MailMessage mensagem = new MailMessage();
            mensagem.From = new MailAddress(_configuration.GetValue<string>("Email:UserName"));
            mensagem.To.Add(colaborador.Email);
            mensagem.Subject = "Colaborador:" + colaborador.Nome + " - LojaVirtual - Senha";
            mensagem.Body = corpoMsg;
            mensagem.IsBodyHtml = true;

            //Enviar Mensagem via SMTP
            _smtp.Send(mensagem);
        }
    }
}
