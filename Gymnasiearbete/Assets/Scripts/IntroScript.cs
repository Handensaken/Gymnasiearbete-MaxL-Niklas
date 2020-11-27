using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class IntroScript : MonoBehaviour
{
    public Queue<string> introText = new Queue<string>();
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        string[] allText = { "Year 978", "I had just moved into a new town. Never could I have suspected the turn of events that followed.", "Forever I will remember the conversation I first had with the young wellkeeper.", "But enough with the ramblings of an old man. Let me take you back in time.", "To a simpler time.", "To the time of my youth.", "This, is my story." };
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
                yield return new WaitForSeconds(0.15f);
                hello.Add(text.text);
            }
            yield return new WaitForSeconds(2);
            //text.text = "";
            for (int i = hello.Count; i > 0; i--)
            {
                text.text = hello[i-1];
                yield return new WaitForSeconds(0.06f);
            }
            hello.Clear();
        }
        SceneManager.LoadScene(1);
        yield break;
    }
}
