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
    }

    public void GreetQuest()
    {

        if (greetQuest.Count == Player.GetComponent<SC_TPSController>().NPCS.Count)
        {
            Player.GetComponent<SC_TPSController>().NPCS["Hans"].GetComponent<Generic_NPC>().hasQuest = true;
            EndQuest();
        }
    }

    void DoThing()
    {
        questBox.SetActive(false);
    }

    public void InitiateQuest()
    {

        //!! ADD SO ONLY ONE QUEST CAN BE ACTIVE AT A TIME!! !!GAME BREAKING!! Or at least make the player able to toggle between viewing different quests
        questName.text = Player.GetComponent<SC_TPSController>().NPCS[Player.GetComponent<SC_TPSController>().RayHit.name].GetComponent<Generic_NPC>().dialogue.questName;
        questInfo.text = Player.GetComponent<SC_TPSController>().NPCS[Player.GetComponent<SC_TPSController>().RayHit.name].GetComponent<Generic_NPC>().dialogue.questInfo;
        quests.Add(questName.text, false);
        Player.GetComponent<SC_TPSController>().QuestGiver = Player.GetComponent<SC_TPSController>().NPCS[Player.GetComponent<SC_TPSController>().RayHit.name];
        questBox.SetActive(true);
        DialogueManager.EndDialogue();
        DialogueManager.GetComponent<DialogueManager>().activeQuest = true;
    }
    public void EndQuest()
    {
        questInfo.text = "Quest Complete";
        Invoke("DoThing", 5.0f);
        quests[questName.text] = true;
        //  questBox.SetActive(false);
        DialogueManager.GetComponent<DialogueManager>().activeQuest = false;
    }
}
