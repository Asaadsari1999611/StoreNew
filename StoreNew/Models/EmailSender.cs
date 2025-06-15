namespace StoreNew.Models
{
	using System.Net;
	using System.Net.Mail;
	using System.Threading.Tasks;

	public class EmailSender
	{
		public async Task SendEmailAsync(string toEmail, string subject, string body)
		{
			var fromEmail = "asaadmsari724@gmail.com"; // بريدك
			var appPassword = "mujx pceh twvj zgsh"; // كلمة مرور التطبيق من Google

			var smtpClient = new SmtpClient("smtp.gmail.com")
			{
				Port = 587,
				EnableSsl = true,
				Credentials = new NetworkCredential(fromEmail, appPassword)
			};

			var mailMessage = new MailMessage(fromEmail, toEmail, subject, body)
			{
				IsBodyHtml = true
			};

			await smtpClient.SendMailAsync(mailMessage);
		}
	}
}