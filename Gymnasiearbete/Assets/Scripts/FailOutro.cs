using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class FailOutro : MonoBehaviour
{
    public Queue<string> introText = new Queue<string>();
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        string[] allText = { "Aight that's it, thanks for playing mate", "Originally there was a story here but given that it was cringy af I removed it" };
        StartCoroutine(DelayTest(allText));
    }
    // Update is called once per frame
    void Update()
    {

    }
    List<string> hello = new List<string>();
    IEnumerator DelayTest(string[] a)
    {
        foreach (string sentence in a)
        {
            hello.Add("");
            foreach (char c in sentence)
            {
                text.text += c;
                yield return new WaitForSeconds(0.1f);
                hello.Add(text.text);
            }
            yield return new WaitForSeconds(1);
            //text.text = "";
            for (int i = hello.Count; i > 0; i--)
            {
                text.text = hello[i - 1];
                yield return new WaitForSeconds(0.01f);
            }
            hello.Clear();
        }
        SceneManager.LoadScene(6);
        yield break;
    }
}
