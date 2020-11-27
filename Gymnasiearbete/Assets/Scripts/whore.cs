using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Mail;
public class whore : MonoBehaviour
{
    public LightController lC;
    public MusicController mC;
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
        var smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential("testet69420@gmail.com", "Jagvetinte852"),
            EnableSsl = true,
        };

        Debug.Log("sending");
        smtpClient.Send("testet69420@gmail.com", "testet69420@gmail.com", "Sent from unity", $"Time of day is {lC.GetTime()}. Music played is ");

    }
}