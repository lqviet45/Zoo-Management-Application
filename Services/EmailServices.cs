﻿using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using ServiceContracts;
using ServiceContracts.DTO.EmailDTO;

namespace Services
{
	public class EmailServices : IEmailServices
	{

		private readonly IConfiguration _configuration;

        public EmailServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmail(EmailDto emailDto)
		{
			var email = new MimeMessage();

			email.From.Add(MailboxAddress.Parse(_configuration.GetSection("EmailUserName").Value));
			email.To.Add(MailboxAddress.Parse(emailDto.To));
			email.Subject = emailDto.Subject;

			email.Body = new TextPart(TextFormat.Html)
			{
				Text = emailDto.Body
			};

			using var smtp = new SmtpClient();
			await smtp.ConnectAsync(
				_configuration.GetSection("EmailHost").Value,
				587,
				SecureSocketOptions.StartTls);

			smtp.Authenticate(_configuration.GetSection("EmailUserName").Value,
				_configuration.GetSection("EmailPassword").Value);

			await smtp.SendAsync(email);
			smtp.Disconnect(true);
		}
	}
}
