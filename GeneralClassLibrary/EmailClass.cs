using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;
using System.IO;
using DocumentFormat.OpenXml;
using System.Security;

namespace GeneralClassLibrary
{
    public class EmailClass
    {
        public static string sendEmail(string emailFrom, string emailTo, SecureString pass, string body, Dictionary<string, string> confDic, string subject= "Test message")
        {
            try
            {
                // адрес smtp-сервера и порт, с которого будем отправлять письмо. Внимание зависит от того где созданна ваша почта
                SmtpClient mySmtpClient = new SmtpClient("smtp.yandex.ru", 25);

                // set smtp-client with basicAuthentication
                mySmtpClient.UseDefaultCredentials = false;
                mySmtpClient.Credentials = new NetworkCredential(emailFrom, pass);
                mySmtpClient.EnableSsl = true;

                // add from,to mailaddresses
                MailAddress from = new MailAddress(emailFrom, "Test");
                MailAddress to = new MailAddress(emailTo, "TestToName");
                MailMessage myMail = new MailMessage(from, to);

                // add ReplyTo
                //MailAddress replyTo = new MailAddress("reply@example.com");
                //myMail.ReplyToList.Add(replyTo);

                // set subject and encoding
                myMail.Subject = subject;

                // set body-message and encoding
                List<LinkedResource> lLinkedResource = new List<LinkedResource>();
                foreach (KeyValuePair<string, string> entry in confDic)
                {
                    if (entry.Key.Contains("$img"))
                    {
                        LinkedResource res = new LinkedResource(entry.Value);                        
                        res.ContentId = Guid.NewGuid().ToString();
                        lLinkedResource.Add(res);

                        string htmlBody = @"<img src='cid:" + res.ContentId + @"'/>";

                        body = body.Replace(entry.Key, htmlBody);
                    }
                    else
                        body = body.Replace(entry.Key, entry.Value);//заменяем в нем все шаблонные места из файла конфига
                }
                
                AlternateView alternateView = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);

                lLinkedResource.ForEach(alternateView.LinkedResources.Add);//тоже самое что foreach (LinkedResource llr in lLinkedResource) alternateView.LinkedResources.Add(llr);
                
                myMail.AlternateViews.Add(alternateView);

                // text or html
                myMail.IsBodyHtml = true;

                mySmtpClient.Send(myMail);
                return "Сообщение отправленно";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        private static AlternateView GetEmbeddedImage(String imgPath)
        {
            LinkedResource res = new LinkedResource(imgPath);
            res.ContentId = Guid.NewGuid().ToString();
            string htmlBody = @"<img src='cid:" + res.ContentId + @"'/>";//<h1>Picture</h1><img src='cid:0bcf13e7-cb04-4b60-8c6e-a509679c5aa7'/>

            AlternateView alternateView = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);
            alternateView.LinkedResources.Add(res);
            return alternateView;
        }

        private static void ChangeHtml(string PathToHtml, Dictionary<string, string>  confDic)
        {
            string htmlStr = File.ReadAllText(PathToHtml);//полностью считываем html файл 

            foreach (KeyValuePair<string, string> entry in confDic)
            {
                htmlStr = htmlStr.Replace(entry.Key, entry.Value);//заменяем в нем все шаблонные места из файла конфига
            }

            File.WriteAllText(PathToHtml, htmlStr);//переписываем html файл
        }
    }
    
}
