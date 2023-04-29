using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Text;
using System.Diagnostics;
using Microsoft.Extensions.Options;

namespace MailApp
{
    public class SMTPMailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        private readonly IWebHostEnvironment _env;
        public SMTPMailService(IOptions<MailSettings> mailSettings, IWebHostEnvironment env)
        {
            _mailSettings = mailSettings.Value;
            _env = env;
        }

        public async Task<bool> SendDiscountMessageAsync(MailModel mailModel)
        {
            var imagePath = _env.WebRootPath + "/DiscountMailImages";
            MailMessage mail = new MailMessage()
            {
                IsBodyHtml = mailModel.IsBodyHtml,
                Subject = mailModel.Subject,
                From = new MailAddress(_mailSettings.From, _mailSettings.DisplayName, Encoding.UTF8)
            };

            mail.AlternateViews.Add
                (GetEmbeddedImage(imagePath, mailModel.Body));

            foreach (var to in mailModel.Tos)
                mail.To.Add(to);

            await SendMessageAsync(mail);
            return true;
        }

        public async Task<bool> SendCustomMessageAsync(MailModel mailModel)
        {
            MailMessage mail = new MailMessage()
            {
                IsBodyHtml = mailModel.IsBodyHtml,
                Subject = mailModel.Subject,
                From = new MailAddress(_mailSettings.From, _mailSettings.DisplayName, Encoding.UTF8),
                Body = mailModel.Body
            };

            foreach (var to in mailModel.Tos)
                mail.To.Add(to);

            await SendMessageAsync(mail);
            return true;
        }
        private async Task SendMessageAsync(MailMessage mail)
        {
            try
            {
                var password = _mailSettings.Password;
                var imagePath = _env.WebRootPath + "/DiscountMailImages";

                SmtpClient smtp = new()
                {
                    Credentials = new NetworkCredential(_mailSettings.From, password),
                    Port = _mailSettings.SMTPPort,
                    EnableSsl = _mailSettings.SMTPEnableSsl,
                    Host = _mailSettings.SMTPHost
                };

                await smtp.SendMailAsync(mail);
            }
            catch (Exception exception)
            {
                Debug.Fail(exception.Message);
            }
        }

        private AlternateView GetEmbeddedImage(String path, String body)
        {
            String[] imagesPath = Directory.GetFiles(path);

            AlternateView view = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
            foreach (var imagePath in imagesPath)
            {
                LinkedResource res = new LinkedResource(imagePath);
                res.ContentId = Path.GetFileNameWithoutExtension(imagePath);
                view.LinkedResources.Add(res);
            }

            return view;
        }
    }
}
