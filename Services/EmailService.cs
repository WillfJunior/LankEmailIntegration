using Domain.Core.Entities;
using Domain.Core.Repository;
using Domain.Core.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly INotaRepository _notaRepository;
        private readonly IClienteRepository _clienteRepository;

        public EmailService(IConfiguration configuration, IClienteRepository clienteRepository, INotaRepository notaRepository)
        {
            _configuration = configuration;
            _clienteRepository = clienteRepository;
            _notaRepository = notaRepository;
        }

        public async Task SendEmailAsync(Notas notas)
        {
            

            var cliente = await _clienteRepository.GetByIdAsync(notas.ClienteId);

            try
            {
                if (cliente is not null)
                {
                    int hora = DateTime.Now.Hour;

                    var saudacoes = new string[] { "Bom dia", "Bom dia", "Boa tarde", "Boa noite" };
                    var destinatario = _configuration.GetSection("Email:Destinatario").Value;
                    var remetente = _configuration.GetSection("Email:Remetente").Value;
                    var copia = _configuration.GetSection("Email:Copia").Value;
                    var senha = _configuration.GetSection("Email:Senha").Value;
                    var smtp = _configuration.GetSection("Email:Smtp").Value;
                    var porta = _configuration.GetSection("Email:Porta").Value;
                    var assunto = $"Emissão de Nota Fiscal - {cliente.Nome} ";
                    var saudacao = saudacoes[hora / 6];
                    var mensagem = string.IsNullOrEmpty(notas.Observacoes) ? $"{saudacao}! \n\nPor favor emitir nota fiscal para o cliente abaixo: \n\n " +
                        $"Cliente: {cliente.Nome} \n" +
                        $"CNPJ: {cliente.CNPJ} \n" +
                        $"Valor: {notas.Valor.ToString("C")} \n\n" +
                        $"Grato., \n" +
                        $"Willians Junior (W J Frutuoso Junior Solucoes)" : $"{saudacao}! \n\nPor favor emitir nota fiscal para o cliente abaixo: \n\n " +
                                                                            $"Cliente: {cliente.Nome} \n" +
                                                                            $"CNPJ: {cliente.CNPJ} \n" +
                                                                            $"Valor: {notas.Valor.ToString("C")} \n\n" +
                                                                            $"Incluir no campo Observações da nota: {notas.Observacoes}\n\n"+
                                                                            $"Grato., \n" +
                                                                            $"Willians Junior (W J Frutuoso Junior Solucoes)";

                    var email = new MailMessage(remetente, destinatario, assunto, mensagem);
                    email.CC.Add(copia);
                    var smtpClient = new SmtpClient(smtp, Convert.ToInt32(porta));

                    var credentials = new System.Net.NetworkCredential(remetente, senha);
                    smtpClient.Credentials = credentials;
                    smtpClient.EnableSsl = false;

                    smtpClient.Send(email);

                    await _notaRepository.CreateAsync(notas);

                    
                        
                }
            }
            catch (Exception)
            {

                throw new ArgumentException("Erro ao criar nota fiscal");
            }




        }
    }
}
