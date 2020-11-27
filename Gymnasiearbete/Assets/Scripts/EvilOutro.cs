using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class EvilOutro : MonoBehaviour
{
    public Queue<string> introText = new Queue<string>();
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        string[] allText = { "Bert oh Bert", "Poor soul.", "I decided to help Jonas in exchange for him helping the village","He had promised to save the village from a great calamity.", "If I just would have known...", "The Demon King's armies were headed our direction. That was the calamity Joans had referred to.", "Because I trusted Jonas, I led an innocent child to him. He killed that child.", "Bert oh Bert...", "He used that child as a sacrifice for using powerful environmental tier magic.", "The next thing I knew a large flood of lava was headed for the village.", "Was this Jonas's work? That I do not know.","You see in the same moment the Demon King's army appeared on the horizon and was met by the Kingdom's Angelic Guard.", "They bought enough time for most to escape unscathed. But that is a tale for another time.","Still I do not know wether the flood was because of Jonas or the Demon King, I never met Jonas again after the incident.","Bert oh Bert..." };
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
