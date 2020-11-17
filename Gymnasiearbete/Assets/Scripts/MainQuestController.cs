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

    string[] questNames = { "logic fill", "Main quest 2", "Main quest 3" };
    string[] questDesc = { "logic fill", "Main Quest 2 description", "Main quest 3 description" };
    string[,] questDialogues = { {"logic fill" , "logic fill", ""}, {"hihuhihuhihuhihihuhihu", "hehoheheohehohehohehohe","hahahahahahhaha"}, { "is nam o clock so", "better start namming you fukkers" ,"YEP (Haha it's a twitch emote I can't blame you if you don't understand, women don't fit in there"} };
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

        if (questTracker.GetComponent<QuestTracker>().quests.ContainsKey("Main quest 2") && !questTracker.GetComponent<QuestTracker>().quests["Main quest 2"])
        {
            jonas.GetComponent<JonasController>().target = "Karl";
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

