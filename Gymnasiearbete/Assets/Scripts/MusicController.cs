using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioSource[] spookyAudioSources;
    public AudioSource initialMusic;
    // Start is called before the first frame update
    void Start()
    {
        initialMusic.Play();
        initialMusic.volume = 0.15f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    int test;
    public void PlaySpooky()
    {
        initialMusic.Stop();
        test = Random.Range(0, 4);
        spookyAudioSources[test].Play();
        Debug.Log("Now playing song " + spookyAudioSources[test].name);
    }
    public string GetMusic()
    {
        return spookyAudioSources[test].name;
    }
}
