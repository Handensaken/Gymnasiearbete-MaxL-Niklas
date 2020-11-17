using System.Net;
using System;
using System.Net.Mail;

namespace email
{
    class Program
    {
        static void Main(string[] args)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("testet69420@gmail.com", "Jagvetinte852"),
                EnableSsl = true,
            };


            smtpClient.Send("testet69420@gmail.com", "testet69420@gmail.com", "", "");


        }
    }
}
