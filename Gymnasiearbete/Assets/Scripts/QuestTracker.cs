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
    public GameObject questIndicator;

    public GameObject[] questMarks;

    public List<bool> greetQuest = new List<bool>();
    private void Start()
    {
        foreach (GameObject qM in questMarks)
        {
            qM.SetActive(false);
        }
        StartCoroutine(Indicate("I was told to speak with the WELLKEEPER", 24));
        questMarks[0].SetActive(true);
    }

    public void GreetQuest()
    {
        if (greetQuest.Count == SC_TPSController.NPCS.Count)
        {
            SC_TPSController.NPCS["male_blacksmith"].GetComponent<Generic_NPC>().hasQuest = true;
            SC_TPSController.NPCS["female_square_1"].GetComponent<Generic_NPC>().hasQuest = false;
            EndQuest();
            StartCoroutine(Indicate("HANS said I should go to him when I had greeted everyone", 18));
            questMarks[8].SetActive(true);
            Debug.Log(questMarks[8]);
            Debug.Log(questMarks[8].name);
        }
    }

    void DoThing()
    {
        questBox.SetActive(false);
    }

    public void InitiateQuest()
    {
        if (Player.GetComponent<SC_TPSController>().RayHit.name == "Jonas")
        {
            questName.text = SC_TPSController.NPCS[Player.GetComponent<SC_TPSController>().RayHit.name].GetComponent<JonasController>().dialogue.questName;
            questInfo.text = SC_TPSController.NPCS[Player.GetComponent<SC_TPSController>().RayHit.name].GetComponent<JonasController>().dialogue.questInfo;
        }
        else
        {
            questName.text = SC_TPSController.NPCS[Player.GetComponent<SC_TPSController>().RayHit.name].GetComponent<Generic_NPC>().dialogue.questName;
            questInfo.text = SC_TPSController.NPCS[Player.GetComponent<SC_TPSController>().RayHit.name].GetComponent<Generic_NPC>().dialogue.questInfo;
        }

        quests.Add(questName.text, false);
        Player.GetComponent<SC_TPSController>().QuestGiver = SC_TPSController.NPCS[Player.GetComponent<SC_TPSController>().RayHit.name];
        questBox.SetActive(true);
        dialogueManager.EndDialogue();
        dialogueManager.GetComponent<DialogueManager>().activeQuest = true;
        if (!quests["Greetings in order"])
        {
            for (int i = 1; i < 10; i++)
            {
                questMarks[i].SetActive(true);
            }
            questMarks[0].SetActive(false);
        }
        else if (!quests["New Pickaxe"])
        {
            questMarks[8].SetActive(false);
            questMarks[3].SetActive(true);
        }
        else if (!quests["The Sentient Well"])
        {
            questMarks[11].SetActive(false);
            questMarks[10].SetActive(true);
        }
        else if (!quests["Mind of the King"])
        {
            questMarks[11].SetActive(false);
            questMarks[6].SetActive(true);
            Debug.Log(questMarks[6].name);
            Debug.Log(questMarks[6]);
        }
        else if (!quests["A Final Sacrifice"])
        {
            questMarks[11].SetActive(false);
            questMarks[4].SetActive(true);
        }
    }
    public void EndQuest()
    {
        questInfo.text = "Quest Complete";
        Invoke("DoThing", 5.0f);
        quests[questName.text] = true;
        //  questBox.SetActive(false);
        dialogueManager.GetComponent<DialogueManager>().activeQuest = false;
    }

    public int noCount = 0;
    public void FinalQuestNo()
    {
        dialogueManager.GetComponent<DialogueManager>().buttonParent.SetActive(false);
        if (quests.ContainsKey("Mind of the King") && quests["Mind of the King"])
        {
            noCount++;
            if (noCount >= 2)
            {
                dialogueManager.goodEnd = true;
                dialogueManager.EndDialogue();
                //End game register choice
                return;
            }
            dialogueManager.AssignSentence("Please do reconsider. This is all for the sake of your village");
            dialogueManager.DisplayNextSentence();
        }
        else if (quests.ContainsKey("The Sentient Well") && quests["The Sentient Well"])
        {
            noCount++;
            if(noCount >= 2)
            {
                quests.Add("Mind of the King", true);
                mainQuestController.EndQuestDebug();
                questMarks[11].SetActive(true);
                dialogueManager.EndDialogue();
                noCount = 0;
                return;
            }
            dialogueManager.AssignSentence("FOOL! The spell is already cast. I can't have you backing out now otherwise I'd have to do it myself!");
            dialogueManager.DisplayNextSentence();
        }
        else if (quests.ContainsKey("New Pickaxe") && quests["New Pickaxe"])
        {
            noCount++;
            if (noCount >= 2)
            {
                quests.Add("The Sentient Well", true);
                mainQuestController.EndQuestDebug();
                questMarks[11].SetActive(true);
                dialogueManager.EndDialogue();
                noCount = 0;
                return;
            }
            dialogueManager.AssignSentence("Please do this for me, it would be troublesome if the townsfolk saw me.");
            dialogueManager.DisplayNextSentence();
        }
        else if (quests.ContainsKey("Greetings in order") && quests["Greetings in order"])
        {
            noCount++;
            if (noCount >= 2)
            {
                quests.Add("New Pickaxe", true);
                questMarks[11].SetActive(true);
                questMarks[8].SetActive(false);
                dialogueManager.EndDialogue();
                SC_TPSController.NPCS["male_blacksmith"].GetComponent<Generic_NPC>().hasQuest = false;
                noCount = 0;
                return;
            }
            dialogueManager.AssignSentence("Come on man... otherwise I'll have to stop working to go and tell her myself. Have a heart kid!");
            dialogueManager.DisplayNextSentence();
        }
        else
        {
            noCount++;
            if (noCount >= 2)
            {
                quests.Add("Greetings in order", true);
                for (int i = 0; i < 10; i++)
                {
                    questMarks[i].SetActive(false);
                }
                questMarks[8].SetActive(true);
                dialogueManager.EndDialogue();
                SC_TPSController.NPCS["male_blacksmith"].GetComponent<Generic_NPC>().hasQuest = true;
                SC_TPSController.NPCS["female_square_1"].GetComponent<Generic_NPC>().hasQuest = false;
                StartCoroutine(Indicate("I should at least talk to HANS NEAR THE CASTLE",18));
                noCount = 0;
                return;
            }
            dialogueManager.AssignSentence("Don't be like that, go greet everyone, at least talk to HANS if you're not greeting everyone.");
            dialogueManager.DisplayNextSentence();
        }
    }
    IEnumerator Indicate(string s, int fs)
    {
        yield return new WaitForSeconds(2);
        questIndicator.SetActive(true);
        questIndicator.GetComponentInChildren<Text>().text = s;
        questIndicator.GetComponentInChildren<Text>().fontSize = fs;
        yield return new WaitForSeconds(7);
        questIndicator.SetActive(false);

    }
}
