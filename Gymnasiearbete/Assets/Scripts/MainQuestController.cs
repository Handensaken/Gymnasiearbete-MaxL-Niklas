using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainQuestController : MonoBehaviour
{
    public GameObject evilPersonWEEEWOOO;
    bool callOnce = false;
    public QuestTracker questTracker;
    public GameObject player;
    public GameObject jonas;

    string[] questNames = { "logic fill", "Mind of the King", "Main quest 3" };
    string[] questDesc = { "logic fill", "Main Quest 2 description", "A Final Sacrifice" };
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
                                 "is nam o clock so", 
                                 "better start namming you fukkers", 
                                 "YEP (Haha it's a twitch emote I can't blame you if you don't understand, women don't fit in there"
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
    }
    public void SetTrue()
    {
        questTracker.quests.Add("Greetings in order", true);
        questTracker.quests.Add("New Pickaxe", true);

     //   SC_TPSController.NPCS.Add(jonas.name, jonas);

    }
    public void EndQuestDebug()
    {
        Debug.Log("ended quest");
        questTracker.GetComponent<QuestTracker>().EndQuest();
        currentMainQuest++;
        jonas.GetComponent<JonasController>().dialogue.questName = questNames[currentMainQuest];
        jonas.GetComponent<JonasController>().dialogue.questInfo = questDesc[currentMainQuest];

        for (int i = 0; i < questDialogues.GetLength(1); i++)
        {
            jonas.GetComponent<JonasController>().dialogue.questSentences[i] = questDialogues[currentMainQuest, i];
        }
      

    }

}

