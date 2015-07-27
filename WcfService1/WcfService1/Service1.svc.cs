using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Net;
using System.Net.Mail;



namespace WcfService1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            this.SendTextMessage("teste", "Mesagem de teste", 554896374614);
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {

                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }


 
        public void SendTextMessage(string subject, string message, long telephoneNumer)
        {
            // login details for gmail acct.
            const string sender = "gguadanhim@gmail.com";
            const string password = "stadium2008";
 
            // find the carriers sms gateway for the recipent. txt.att.net is for AT&T customers.
            string carrierGateway = "txt.att.net";
 
            // this is the recipents number @ carrierGateway that gmail use to deliver message.
            string recipent = string.Concat(new object[]{
            telephoneNumer,
            '@',
            carrierGateway
            });
 
            // form the text message and send
            using (MailMessage textMessage = new MailMessage(sender, recipent, subject, message))
            {
                using (SmtpClient textMessageClient = new SmtpClient("smtp.gmail.com", 587))
                {
                    textMessageClient.UseDefaultCredentials = false;
                    textMessageClient.EnableSsl = true;
                    textMessageClient.Credentials = new NetworkCredential(sender, password);
                    textMessageClient.Send(textMessage);
                }
            }
        }
    }
}
