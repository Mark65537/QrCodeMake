using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;
using System.IO;
using DocumentFormat.OpenXml;
using System.Security;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Windows.Forms;
using QrCodeMakelib;

namespace GeneralClassLibrary
{
    public class MailClass
    {
        //перебрать в цыкле параметр переменную с универсальным типом
        public static string sendEmail(MailMessageInfo mMI, List<Options> confDic)
        {
            try
            {
                // адрес smtp-сервера и порт, с которого будем отправлять письмо. Внимание зависит от того где созданна ваша почта
                SmtpClient mySmtpClient = new SmtpClient($"smtp.{mMI.provider}.ru", 25);//есть еще 465 и 587

                // set smtp-client with basicAuthentication
                mySmtpClient.UseDefaultCredentials = false;
                mySmtpClient.Credentials = new NetworkCredential(mMI.emailFrom, mMI.pass);
                mySmtpClient.EnableSsl = true;

                // add from,to mailaddresses
                MailAddress from = new MailAddress(mMI.emailFrom, "Test");
                MailAddress to = new MailAddress(mMI.emailTo, "TestToName");
                MailMessage myMail = new MailMessage(from, to);

                // add ReplyTo
                //MailAddress replyTo = new MailAddress("reply@example.com");
                //myMail.ReplyToList.Add(replyTo);

                // set subject and encoding
                myMail.Subject = mMI.subject;

                // set body-message and encoding
                List<LinkedResource> lLinkedResource = new List<LinkedResource>();
                //переделать этот код так что бы вместо Dictionary<string, string> confDic, в цикл передовался List<Options> opts
                foreach (var opt in confDic)
                {
                    //поставить условие если entry является цифрой, то продолжить цикл
                    if (int.TryParse(opt.name, out int result))
                    {
                        continue;
                    }
                    if (opt.name.Contains("Img") && File.Exists(opt.val))
                    {
                            LinkedResource res = new LinkedResource(opt.val);
                            res.ContentId = Guid.NewGuid().ToString();
                            lLinkedResource.Add(res);

                            string htmlBody = @"<img src='cid:" + res.ContentId + @"'/>";

                            mMI.body = mMI.body.Replace(opt.name, htmlBody);                        
                    }
                    else
                        mMI.body = mMI.body.Replace(opt.name, opt.val);//заменяем в нем все шаблонные места из файла конфига
                }
                
                AlternateView alternateView = AlternateView.CreateAlternateViewFromString(mMI.body, null, MediaTypeNames.Text.Html);

                lLinkedResource.ForEach(alternateView.LinkedResources.Add);//тоже самое что foreach (LinkedResource llr in lLinkedResource) alternateView.LinkedResources.Add(llr);
                
                myMail.AlternateViews.Add(alternateView);

                // text or html
                myMail.IsBodyHtml = true;

                mySmtpClient.Send(myMail);
                return "Сообщение отправленно\n";
            }
            catch (Exception ex)
            {
                return ex.Message;
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
