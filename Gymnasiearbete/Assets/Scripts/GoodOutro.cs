using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GoodOutro : MonoBehaviour
{
    public Queue<string> introText = new Queue<string>();
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        string[] allText = { "I staunchly decided not to trust the young Wizard.", "Jonas.", "That's a name I will never forget.", "Because I refused to help him he unleashed waves of catasrophies upon the town.", "Raining frogs, swarms of mosquitoes, alternating temperatures, et cetra.", "It was a miracle I managed to escape, otherwise I wouldn't be telling this story.", "The others weren't as lucky.", "Sometimes I can't help but blame myself for their passing..." };
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
                text.text = hello[i - 1];
                yield return new WaitForSeconds(0.06f);
            }
            hello.Clear();
        }
        SceneManager.LoadScene(6);
        yield break;
    }
}
