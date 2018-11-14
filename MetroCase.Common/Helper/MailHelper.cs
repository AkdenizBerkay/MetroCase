using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace MetroCase.Common.Helper
{
    public class MailHelper
    {
        public static bool SendMail(string body, List<string> to, string subject, bool ishtml = true,string imgurl="")
        {
            bool result = false;

            try
            {
                var message = new MailMessage();
                string mailaddress = EncodeDecode.Decode(ConfigHelper.Get<string>("MailUser"));
                message.From = new MailAddress(mailaddress);

                foreach (string s in to)
                {
                    message.To.Add(new MailAddress(s));
                }
                if (!string.IsNullOrEmpty(imgurl))
                {
                    message.AlternateViews.Add(getEmbeddedImage(imgurl));
                }
                message.Subject = subject;
                message.IsBodyHtml = ishtml;
                message.Body = body;
                


                var smpt = new SmtpClient(EncodeDecode.Decode(ConfigHelper.Get<string>("MailHost")),
                    Convert.ToInt32(EncodeDecode.Decode(ConfigHelper.Get<string>("MailPort"))));
                smpt.EnableSsl = true;
                smpt.Credentials = new NetworkCredential(EncodeDecode.Decode(ConfigHelper.Get<string>("MailUser")),
                    EncodeDecode.Decode(ConfigHelper.Get<string>("MailPass")));

                smpt.Send(message);
                result = true;
        }
            catch(Exception ex) {
                
            }

            return result;
        }

        private static AlternateView getEmbeddedImage(string imgurl)
        {
            LinkedResource res = new LinkedResource(imgurl);
            res.ContentId = Guid.NewGuid().ToString();
            string htmlBody = @"<img src='cid:" + res.ContentId + @"'/>";
            AlternateView alternateView = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);
            alternateView.LinkedResources.Add(res);
            return alternateView;
        }
    }
}
