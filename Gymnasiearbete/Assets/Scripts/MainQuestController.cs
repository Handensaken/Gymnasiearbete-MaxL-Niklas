using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainQuestController : MonoBehaviour
{
    public GameObject evilPersonWEEEWOOO;
    bool callOnce = false;
    public QuestTracker QuestTracker;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (QuestTracker.quests.ContainsKey("Greetings in order") && QuestTracker.quests.ContainsKey("New Pickaxe"))
        {
            if (QuestTracker.quests["Greetings in order"] && QuestTracker.quests["New Pickaxe"])
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
        QuestTracker.quests.Add("Greetings in order", true);
        QuestTracker.quests.Add("New Pickaxe", true);
    }
}
