using AutoMapper.Internal;
using HosptitalManagmentSystem.Helpers;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace HosptitalManagmentSystem.Services
{
	public class EmailService
	{

		private readonly MailSettings _mailSettings;
		private readonly IConfiguration _config;

		public EmailService(IOptions<MailSettings> mailSettings, IConfiguration config)
		{
	
			_mailSettings = mailSettings.Value;
			_config = config;
		}
		public async Task SendEmailAsync(string name, string emailAddress, string password)
		{
			var FromMail = _config.GetSection("MailSettings")["FromMail"];
			var DisplayName = _config.GetSection("MailSettings")["DisplayName"];
		
			var email = new MimeMessage();
			//var port = _config.GetSection("MailSettings")["Port"];
			//var Host = _config.GetSection("MailSettings")[""];
			email.From.Add(new MailboxAddress(DisplayName, FromMail));
			email.To.Add(MailboxAddress.Parse(emailAddress));
			
			email.Subject = "Logins for HospitalManagmentSystem";
			var builder = new BodyBuilder();
			email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
			{
				Text = $@"
        Hi {name},<br><br>
        Your credentials to the Hospital Management System are:<br>
        <b>Username:</b> {emailAddress}<br>
        <b>Password:</b> {password}<br><br>
        Please log in and change your password after your first login."
			};

			using var smtp = new SmtpClient();
			smtp.Connect(_mailSettings.Host, _mailSettings.Port, _mailSettings.UseSSL);
			smtp.Authenticate(_mailSettings.UserMail, _mailSettings.Password);
			await smtp.SendAsync(email);
			smtp.Disconnect(true);
		}
	}
}

