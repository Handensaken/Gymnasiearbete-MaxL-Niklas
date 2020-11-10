﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainQuestController : MonoBehaviour
{
    public GameObject evilPersonWEEEWOOO;
    bool callOnce = false;
    public QuestTracker questTracker;
    public GameObject player;
    public GameObject jonas;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (questTracker.quests.ContainsKey("Greetings in order") && questTracker.quests.ContainsKey("New Pickaxe"))
        {
            if (questTracker.quests["Greetings in order"] && questTracker.quests["New Pickaxe"])
            {
                if (!callOnce)
                {
                    NowWeDoShit();
                    callOnce = true;
                }
            }
        }
    }

    void NowWeDoShit()
    {
        evilPersonWEEEWOOO.SetActive(true);
    }
    public void SetTrue()
    {
        questTracker.quests.Add("Greetings in order", true);
        questTracker.quests.Add("New Pickaxe", true);

        player.GetComponent<SC_TPSController>().NPCS.Add(jonas.name, jonas);

    }
    public void MainQuest1()
    {
        questTracker.GetComponent<QuestTracker>().EndQuest();
        jonas.GetComponent<JonasController>().dialogue.questName = "SCHEISSEFRAUEN";
        jonas.GetComponent<JonasController>().dialogue.questInfo = "SCHEISSEFRAUEN BESKRIVNUNG";
    }

}

