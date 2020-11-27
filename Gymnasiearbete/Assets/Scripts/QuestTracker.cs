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
    public DialogueManager dialogueManager;

    public Text questName;
    public Text questInfo;

    public GameObject questBox;

    public MainQuestController mainQuestController;

    public List<bool> greetQuest = new List<bool>();
    private void Update()
    {
    }

    public void GreetQuest()
    {

        if (greetQuest.Count == SC_TPSController.NPCS.Count)
        {
            SC_TPSController.NPCS["male_blacksmith"].GetComponent<Generic_NPC>().hasQuest = true;
            SC_TPSController.NPCS["female_square_1"].GetComponent<Generic_NPC>().hasQuest = false;
            EndQuest();
        }
    }

    void DoThing()
    {
        questBox.SetActive(false);
    }

    public void InitiateQuest()
    {
        try
        {
            questName.text = SC_TPSController.NPCS[Player.GetComponent<SC_TPSController>().RayHit.name].GetComponent<Generic_NPC>().dialogue.questName;
            questInfo.text = SC_TPSController.NPCS[Player.GetComponent<SC_TPSController>().RayHit.name].GetComponent<Generic_NPC>().dialogue.questInfo;
        }
        catch
        {
            questName.text = SC_TPSController.NPCS[Player.GetComponent<SC_TPSController>().RayHit.name].GetComponent<JonasController>().dialogue.questName;
            questInfo.text = SC_TPSController.NPCS[Player.GetComponent<SC_TPSController>().RayHit.name].GetComponent<JonasController>().dialogue.questInfo;
        }

        quests.Add(questName.text, false);
        Player.GetComponent<SC_TPSController>().QuestGiver = SC_TPSController.NPCS[Player.GetComponent<SC_TPSController>().RayHit.name];
        questBox.SetActive(true);
        dialogueManager.EndDialogue();
        dialogueManager.GetComponent<DialogueManager>().activeQuest = true;
    }
    public void EndQuest()
    {
        questInfo.text = "Quest Complete";
        Invoke("DoThing", 5.0f);
        quests[questName.text] = true;
        //  questBox.SetActive(false);
        dialogueManager.GetComponent<DialogueManager>().activeQuest = false;
    }

    private int noCount = 0;
    public void FinalQuestNo()
    {
        dialogueManager.GetComponent<DialogueManager>().buttonParent.SetActive(false);
        if (quests.ContainsKey("Mind of the King") && quests["Mind of the King"])
        {
            noCount++;
            if(noCount >= 2)
            {
                dialogueManager.goodEnd = true;
                dialogueManager.EndDialogue();
                //End game register choice
                return;
            }
            dialogueManager.AssignSentence("Please do reconsider. This is all for the sake of your village");
            dialogueManager.DisplayNextSentence();
        }
        else
        {
            dialogueManager.EndDialogue();
        }
    }
}
