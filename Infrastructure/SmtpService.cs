using Betalish.Application.Interfaces;
using Betalish.Common.Settings;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace Betalish.Infrastructure;

public class SmtpService() : ISmtpService
{
    public void SendEmailMessage(
        SmtpConfiguration smtpConf,
        EmailMessage email, 
        List<EmailAttachment> attachments,
        List<EmailImage> images)
    {
        var mail = new MailMessage();

        mail.From = new MailAddress(smtpConf.FromAddress, smtpConf.FromName);

        //if (!string.IsNullOrWhiteSpace(smtpConf.ReplyToAddress))
        //{
        //    mail.ReplyToList.Add(new MailAddress(smtpConf.ReplyToAddress, smtpConf.ReplyToName));
        //}

        mail.To.Add(new MailAddress(email.ToAddress, email.ToName));

        mail.Subject = email.Subject;
        //mail.Body = email.HtmlBody;
        //mail.IsBodyHtml = true;

        var textView = AlternateView.CreateAlternateViewFromString(
            email.TextBody, Encoding.UTF8, MediaTypeNames.Text.Plain);

        var htmlView = AlternateView.CreateAlternateViewFromString(
            email.HtmlBody, Encoding.UTF8, MediaTypeNames.Text.Html);

        foreach (var image in images)
        {
            MemoryStream ms = new MemoryStream(image.Data);

            var resource = new LinkedResource(ms, image.ContentType)
            {
                ContentId = $"{image.Id}",
                TransferEncoding = TransferEncoding.Base64,
                //ContentType = new ContentType(image.ContentType),
                //ContentLink = new Uri($"cid:{image.Id}")
            };

            //resource.ContentDisposition.Inline = true;
            //resource.ContentDisposition.DispositionType = DispositionTypeNames.Inline;

            //resource.ContentId = "myImageId";
            //resource.TransferEncoding = TransferEncoding.Base64;

            htmlView.LinkedResources.Add(resource);
        }

        mail.AlternateViews.Add(textView);
        mail.AlternateViews.Add(htmlView);

        foreach (var attachment in attachments)
        {
            var stream = new MemoryStream(attachment.Data, 0, attachment.Data.Length);

            //stream.Flush();
            //stream.Seek(0, 0);

            mail.Attachments.Add(new Attachment(
                stream, attachment.Name, attachment.ContentType));
        }

        SmtpClient smtp = new SmtpClient(smtpConf.SmtpHost);
        smtp.Credentials = new NetworkCredential(smtpConf.FromAddress, smtpConf.Password);
        smtp.EnableSsl = true;
        smtp.Port = smtpConf.SmtpPort;

        smtp.Send(mail);
    }
}
