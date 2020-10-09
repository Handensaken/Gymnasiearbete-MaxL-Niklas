using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class QuestTracker : MonoBehaviour
{
    //public static List<string> quests = new List<string>();
    public Dictionary<string, bool> quests = new Dictionary<string, bool>();
    public GameObject Player;
    public DialogueManager DialogueManager;

    public Text questName;
    public Text questInfo;

    public GameObject questBox;


    public List<bool> greetQuest = new List<bool>();
    private void Update()
    {
        
        if (greetQuest.Count == Player.GetComponent<SC_TPSController>().NPCS.Count && !quests[questName.text])
        {
            questInfo.text = "Quest Complete!";

            Invoke("DoThing", 5.0f);
        }
    }

    void DoThing()
    {
        questBox.SetActive(false);
    }

    public void InitiateQuest()
    {
        questName.text = Player.GetComponent<SC_TPSController>().NPCS[Player.GetComponent<SC_TPSController>().RayHit.name].GetComponent<Generic_NPC>().dialogue.questName;
        questInfo.text = Player.GetComponent<SC_TPSController>().NPCS[Player.GetComponent<SC_TPSController>().RayHit.name].GetComponent<Generic_NPC>().dialogue.questInfo;
        quests.Add(questName.text, false);
        questBox.SetActive(true);
        DialogueManager.EndDialogue();
    }
    public void EndQuest()
    {
        quests[questName.text] = true;
        questBox.SetActive(false);
    }
}
