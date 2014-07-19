using System.Configuration;

namespace Loosely.CustomerPortal.Backend
{
  class EmailHelper
  {
    readonly static string gmailAccount = ConfigurationManager.AppSettings.Get("Gmail.Account");
    readonly static string gmailPassword = ConfigurationManager.AppSettings.Get("Gmail.Password");

    public static void Send(string customerEmail, string subject, string messageBody)
    {
      var client = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
      client.EnableSsl = true;
      client.Credentials = new System.Net.NetworkCredential(gmailAccount, gmailPassword);
      client.Send(gmailAccount, customerEmail, subject, messageBody);
    }
  }
}
