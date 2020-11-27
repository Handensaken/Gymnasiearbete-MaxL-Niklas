using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainQuestController : MonoBehaviour
{
    public GameObject evilPersonWEEEWOOO;
    bool callOnce = false;
    public QuestTracker questTracker;
    public GameObject player;
    public GameObject jonas;
    public GameObject dialogueManager;
    public MusicController musicController;
    public GameObject finaleButtonParent;
    public whore mailAlgo;

    string[] questNames = { "logic fill", "Mind of the King", "A Final Sacrifice" };
    string[] questDesc = { "logic fill", "Talk to Karl", "Bring a child to Jonas" };
    string[,] questDialogues = {
                                {
                                 "logic fill" ,
                                 "logic fill",
                                 "logic fill"},
                                {
                                 "I need you to go talk to the lord, Karl. He is an instrumental piece to my plan.",
                                 "But he won't come near me and I mustn't alert him. You must speak to him. I've placed a spell on you",
                                 "that allows you to act as my conductor, through you I will be able to read his mind."
                                },
                                {
                                 "Finally I need you to bring me a child, for what i shall not say. Just trust me.",
                                 "It is all so I can save your puny village.",
                                 "Now make haste and come back with a child in tow!"
                                }
                               };
    public int currentMainQuest = 0;



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

        if (questTracker.GetComponent<QuestTracker>().quests.ContainsKey(questNames[1]) && !questTracker.GetComponent<QuestTracker>().quests[questNames[1]])
        {
            jonas.GetComponent<JonasController>().target = "Karl";
        }
        else if (questTracker.GetComponent<QuestTracker>().quests.ContainsKey(questNames[2]) && !questTracker.GetComponent<QuestTracker>().quests[questNames[2]])
        {
            jonas.GetComponent<JonasController>().target = "Bert";
        }
        else
        {
            jonas.GetComponent<JonasController>().target = null;
        }
    }

    void NowWeDoShit()
    {
        evilPersonWEEEWOOO.SetActive(true);
        musicController.PlaySpooky();
    }
    public void SetTrue()
    {
        questTracker.quests.Add("Greetings in order", true);
        questTracker.quests.Add("New Pickaxe", true);

        //   SC_TPSController.NPCS.Add(jonas.name, jonas);
    }
    public void EndQuestDebug()
    {
        questTracker.GetComponent<QuestTracker>().EndQuest();
        currentMainQuest++;
        jonas.GetComponent<JonasController>().dialogue.questName = questNames[currentMainQuest];
        jonas.GetComponent<JonasController>().dialogue.questInfo = questDesc[currentMainQuest];

        for (int i = 0; i < questDialogues.GetLength(1); i++)
        {
            jonas.GetComponent<JonasController>().dialogue.questSentences[i] = questDialogues[currentMainQuest, i];
        }


    }

    int bringAttempts = 0;
    public void BringKid(Button b)
    {
        bringAttempts++;

        string desiredChoice = "correct";

        if (b.GetComponentInChildren<Text>().text == desiredChoice)
        {

            dialogueManager.GetComponent<DialogueManager>().AssignSentence("Say no more weirdy strange man who likes kids! I am hooked!");
            dialogueManager.GetComponent<DialogueManager>().DisplayNextSentence();
            dialogueManager.GetComponent<DialogueManager>().bigDonezo = true;
            SC_TPSController.NPCS["boy_home_1"].GetComponent<Generic_NPC>().activateFollow = true;
            finaleButtonParent.SetActive(false);
        }
        else if (bringAttempts < 2)
        {
            dialogueManager.GetComponent<DialogueManager>().AssignSentence("That's not enough to make me follow you even if we talked before. Mom warned me about you people, she called them pedos");
            dialogueManager.GetComponent<DialogueManager>().DisplayNextSentence();
            finaleButtonParent.SetActive(false);
        }
        else
        {
            dialogueManager.GetComponent<DialogueManager>().AssignSentence("AAAAAAAAAAARGHHH! HELP! HELP! I'M BEING KIDNAPPED");
            dialogueManager.GetComponent<DialogueManager>().failEnd = true;
            dialogueManager.GetComponent<DialogueManager>().DisplayNextSentence();
            finaleButtonParent.SetActive(false);
            dialogueManager.GetComponent<DialogueManager>().bigDonezo = true;
        }

    }

    public void EndGame(string s)
    {
        mailAlgo.SendMail();
        if (s == "good")
        {
            SceneManager.LoadScene(3);
        }
        else if (s == "evil")
        {
            SceneManager.LoadScene(2);
        }
        else if (s == "fail")
        {
            SceneManager.LoadScene(4);
        }
        else
        {
            Debug.Log("something is written wrong");
        }
    }

}

