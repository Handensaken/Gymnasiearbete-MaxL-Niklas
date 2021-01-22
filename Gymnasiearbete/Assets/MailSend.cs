using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MailKit;
using MimeKit;
public class MailSend : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SendMail()
    {
        Debug.Log("Sending message");

        var message = new MimeMessage();

        message.From.Add(new MailboxAddress("Sender", "testet69420@gmail.com"));
        message.To.Add(new MailboxAddress("Receiver", "testet69420@gmail.com"));
        message.Subject = "Data from game";
        message.Body = new TextPart("plain") { Text = $"This is a test" };

        using (var client = new MailKit.Net.Smtp.SmtpClient())
        {
            client.Connect("smtp.gmail.com", 587);

            ////Note: only needed if the SMTP server requires authentication
            client.Authenticate("testet69420@gmail.com", "Jagvetinte852");

            client.Send(message);
            client.Disconnect(true);
        }


    }
}