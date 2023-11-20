using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace StudentIdCard.Persistence.Services
{
    public class SmsService
    {

        public string Sms(string PostAddress, string xmlData)
        {
            try
            {
                WebClient wUpload = new WebClient();
                HttpWebRequest request = WebRequest.Create(PostAddress) as HttpWebRequest;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                Byte[] bPostArray = Encoding.UTF8.GetBytes(xmlData);
                Byte[] bResponse = wUpload.UploadData(PostAddress, "POST", bPostArray);
                Char[] sReturnChars = Encoding.UTF8.GetChars(bResponse);
                string sWebPage = new string(sReturnChars);
                return sWebPage;
            }
            catch
            {
                return "-1";
            }
        }
        public void SendSms(string tel,string sms)
        {
            string ss = "";
            ss += "<?xml version='1.0' encoding='UTF-8'?>";
            ss += "<mainbody>";
            ss += "<header>";
            ss += "<company dil='TR'>Netgsm</company>";
            ss += "<usercode>2493nebioglubilisim</usercode>";
            ss += "<password>F%577AA</password>";
            ss += "<type>1:n</type>";
            ss += "<msgheader>Baslik</msgheader>";
            ss += "</header>";
            ss += "<body>";
            ss += "<msg>";
            ss += $"<![CDATA[{sms}]]>";
            ss += "</msg>";
            ss += $"<no>{tel}</no>";
            ss += $"<no>{tel}</no>";
            ss += "</body>  ";
            ss += "</mainbody>";
            Sms("https://api.netgsm.com.tr/sms/send/xml", ss);
        }
    }
}
