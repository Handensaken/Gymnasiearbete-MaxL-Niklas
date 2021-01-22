using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class whore : MonoBehaviour
{
    public LightController lC;
    public MusicController mC;
    // Start is called before the first frame update

    public void SendMail(string s)
    {

        StartCoroutine(UploadForm(s));
        
    }
    IEnumerator UploadForm(string s)
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("entry.1048841106", $"Time of day is {lC.GetTime()}. Music played is {mC.GetMusic()}. Ending: {s}"));
        var conn = UnityWebRequest.Post("https://docs.google.com/forms/u/0/d/e/1FAIpQLSc4M9amR3mnFkKQzgnOBvQn52osEdghI1PRkEVlukaJ3tNC3A/formResponse", formData);
        yield return conn.SendWebRequest();
        if(conn.isNetworkError || conn.isHttpError)
        {
            Debug.Log(conn.error + "WWWEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEW WEEEEEEEEEEEEEOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOo");
        }
        else
        {
            Debug.Log("Form Sent HAHAHAHAHAHAHHAHAHAHAHAHHAHAHAHAHHAHAHAHAHAHAHAHAHAHAHAHAH");
        }
    }
}