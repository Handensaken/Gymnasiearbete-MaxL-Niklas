using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class DialogueManager : MonoBehaviour
{
    //creates Text variable for containing the name
    public Text nameText;
    //creates Text variable for containing the dialogue
    public Text dialogueText;
    //creates string variable used for storing dialogue
    string sentence;
    //Button array containing choices.
    public Button[] choiceButtons;
    //Gameobject containing buttons
    public GameObject buttonParent;
    //Gameobject containing quest box

    bool hasQuest;

    //Sets up bool to keep track of whether a quest is active
    public bool activeQuest = false;

    public GameObject player;

    //creates Animator
    public Animator animator;

    //creates a string queue containging sentences
    private Queue<string> sentences;

    Dialogue dialogue;


    public GameObject questTracker;
    public GameObject mainQuestController;
    public Queue<string> test = new Queue<string>();
    public string result = "";
    private string correctResult = "147";
    public string[,] wellInteractions = { { "1", "2", "3" }, { "4", "5", "6" }, { "7", "8", "9" } };
    public GameObject mainQuestWellParent;
    public List<Button> buttonList = new List<Button>();    //lazy gang rise up we assigning in inspector boys
    int optionNumber = -1;
    bool donezo = false;

    public bool targetQuestDialogueBool = false;
    public GameObject finaleButtonParent;

    // Start is called before the first frame update
    void Start()
    {
        //initializes the sentences queue
        sentences = new Queue<string>();
    }
    //public bool isActive = false;


    string[] targetQuestDialogue;
    public void StartDialogue(Dialogue d)
    //method for setting dialogue to UI
    {
        Cursor.lockState = CursorLockMode.None;
        isWell = false;
        dialogue = d;
        //sets the animator bool "isActive" to true
        animator.SetBool("isActive", true);

        //sets the name of the object interacted with to the text of nametext
        nameText.text = dialogue.name;

        //clears the queue sentences
        sentences.Clear();

        if (targetQuestDialogueBool)
        {
            TargetQuestSentences();
        }
        else
        {
            if(player.GetComponent<SC_TPSController>().RayHit.name != "Jonas")
            {

                if (!SC_TPSController.NPCS[player.GetComponent<SC_TPSController>().RayHit.name].GetComponent<Generic_NPC>().greeted && !activeQuest && SC_TPSController.NPCS[player.GetComponent<SC_TPSController>().RayHit.name].GetComponent<Generic_NPC>().hasQuest)
                {
                    InitialQuestSentences();
                }
                else if (!SC_TPSController.NPCS[player.GetComponent<SC_TPSController>().RayHit.name].GetComponent<Generic_NPC>().greeted)
                {
                    InitialSentences();
                }
                else if (!activeQuest && SC_TPSController.NPCS[player.GetComponent<SC_TPSController>().RayHit.name].GetComponent<Generic_NPC>().hasQuest)
                {
                    QuestSentences();
                }
                else if (activeQuest && SC_TPSController.NPCS[player.GetComponent<SC_TPSController>().RayHit.name].GetComponent<Generic_NPC>().hasQuest)
                {
                    HasQuestSentences();
                }
                else
                {
                    Debug.Log("generic");
                    GenericSentences();
                }
            }
            else
            {
                if (!SC_TPSController.NPCS[player.GetComponent<SC_TPSController>().RayHit.name].GetComponent<JonasController>().greeted && !activeQuest && SC_TPSController.NPCS[player.GetComponent<SC_TPSController>().RayHit.name].GetComponent<JonasController>().hasQuest)
                {
                    Debug.Log("enqueueing jonas's initial quest");
                    InitialQuestSentences();
                }
                else if (!SC_TPSController.NPCS[player.GetComponent<SC_TPSController>().RayHit.name].GetComponent<JonasController>().greeted)
                {
                    Debug.Log("enqueueing jonas's inital");
                    InitialSentences();
                }
                else if (!activeQuest && SC_TPSController.NPCS[player.GetComponent<SC_TPSController>().RayHit.name].GetComponent<JonasController>().hasQuest)
                {
                    Debug.Log("enqueueing jonas's quest ");
                    QuestSentences();
                }
                else if (activeQuest)
                {
                    HasQuestSentences();
                }
                else
                {
                    Debug.Log("enqueueing jonas's generic");
                    GenericSentences();
                }
            }
        }
        //displays the next sentence
        DisplayNextSentence();
    }

    public void SetNewSentences(string[] s)
    {
        targetQuestDialogue = s;
    }

    void GenericSentences()
    {
        foreach (string sentece in dialogue.genericSentences)
        {
            sentences.Enqueue(sentece);
        }
    }
    void InitialSentences()
    {
        foreach (string sentence in dialogue.initialSentences)
        {
            sentences.Enqueue(sentence);
        }
    }
    void QuestSentences()
    {
        foreach (string sentence in dialogue.questSentences)
        {
            sentences.Enqueue(sentence);
        }
    }
    void HasQuestSentences()
    {
        foreach (string sentence in dialogue.hasQuestSentences)
        {
            sentences.Enqueue(sentence);
        }
    }
    void InitialQuestSentences()
    {
        foreach (string sentence in dialogue.initialQuestSentences)
        {
            sentences.Enqueue(sentence);
        }
    }
    void TargetQuestSentences()
    {
        foreach (string sentence in targetQuestDialogue)
        {
            sentences.Enqueue(sentence);
        }
    }


    public bool bigDonezo = false;
    public bool failEnd = false;
    public void DisplayNextSentence()
    {
        if (sentences.Count <= 0)
        //checks if we have more sentences, if not we end dialogue
        {
            if (targetQuestDialogueBool)
            {
                if (questTracker.GetComponent<QuestTracker>().quests.ContainsKey("A Final Sacrifice"))
                {
                    if (!questTracker.GetComponent<QuestTracker>().quests["A Final Sacrifice"] && !bigDonezo)
                    {
                        //activate new buttons
                        finaleButtonParent.SetActive(true);
                        player.gameObject.GetComponent<SC_TPSController>().canMove = false;
                        return;
                    }
                }
                EndDialogue();
                return;
            }
            else if (isWell && !donezo)
            {
                ChoiceMaking();
                //IN ADDITION To start the well dialogue this mission must be ACTIVE not only exist in the quests dictionary.
                return;
            }
            else
            {
                if (!hasQuest)
                //checks if the npc in question has a quest
                {
                    EndDialogue();
                    return;
                }
                else if (!activeQuest)
                {
                    TriggerChoices();
                    return;
                }
                else
                {
                    HasQuestSentences();
                    EndDialogue();
                    return;
                }
            }
        }
        //removes the first index of sentences and returns it into the string sentence
        sentence = sentences.Dequeue();
        //sets the dialogue text's text to be sentence
        dialogueText.text = sentence;
    }
    public void TriggerChoices()
    {
        buttonParent.SetActive(true);
        player.gameObject.GetComponent<SC_TPSController>().canMove = false;
    }
    public bool goodEnd = false;
    public bool evilEnd = false;
    public void EndDialogue()
    //ends the dialogue by sending a bool to the animator that then removes the dialogue box
    {
        if (!isWell)
        {
            if(player.GetComponent<SC_TPSController>().RayHit.name!="Jonas")
            {
                SC_TPSController.NPCS[player.GetComponent<SC_TPSController>().RayHit.name].GetComponent<Generic_NPC>().greeted = true;
            }
            else
            {
                SC_TPSController.NPCS[player.GetComponent<SC_TPSController>().RayHit.name].GetComponent<JonasController>().greeted = true;
            }
        }

        animator.SetBool("isActive", false);
        finaleButtonParent.SetActive(false);
        buttonParent.SetActive(false);
        player.gameObject.GetComponent<SC_TPSController>().canMove = true;
        sentences.Clear();
        Cursor.lockState = CursorLockMode.Locked;
        if (failEnd)
        {
            mainQuestController.GetComponent<MainQuestController>().EndGame("fail");
        }
        else if (goodEnd)
        {
            mainQuestController.GetComponent<MainQuestController>().EndGame("good");
        }
        else if (evilEnd)
        {
            mainQuestController.GetComponent<MainQuestController>().EndGame("evil");
        }
    }
    public bool SendDialogueActive()
    //sends a bool based on if a dialogue is active or not
    {
        return animator.GetBool("isActive");
    }

    public void checkQuest(bool receivedParamater)
    {
        hasQuest = receivedParamater;
    }

    private bool isWell;
    public void InitiateWellQuest()
    {
        Cursor.lockState = CursorLockMode.None;
        hasQuest = false;
        isWell = true;
        string[] wellSentences = { "I", "AM", "WELL", "SPEAK" };
        animator.SetBool("isActive", true);

        nameText.text = "Well";

        sentences.Clear();
        foreach (string sentence in wellSentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();

    }
    public void ChoiceMaking()
    {
        mainQuestWellParent.SetActive(true);

        optionNumber++;
        if (optionNumber > wellInteractions.GetLength(0) - 1)
        {
            if (result == correctResult)
            {
                donezo = true;
                mainQuestWellParent.SetActive(false);
                player.gameObject.GetComponent<SC_TPSController>().canMove = true;
                mainQuestController.GetComponent<MainQuestController>().EndQuestDebug();
                sentences.Enqueue(result);
                sentences.Enqueue("THE WELL IS PLEASED");
                DisplayNextSentence();
                questTracker.GetComponent<QuestTracker>().questMarks[11].SetActive(true);
                questTracker.GetComponent<QuestTracker>().questMarks[10].SetActive(false);
                return;
            }
            else
            {
                mainQuestWellParent.SetActive(false);
                sentences.Enqueue(result);
                result = "";
                optionNumber = -1;
                sentences.Enqueue("THE WELL IS DISPLEASED");
                DisplayNextSentence();
                return;
            }
        }
        mainQuestWellParent.SetActive(true);
        player.gameObject.GetComponent<SC_TPSController>().canMove = false;
        buttonList[0].GetComponentInChildren<Text>().text = wellInteractions[optionNumber, 0];
        buttonList[1].GetComponentInChildren<Text>().text = wellInteractions[optionNumber, 1];
        buttonList[2].GetComponentInChildren<Text>().text = wellInteractions[optionNumber, 2];


    }

    public void Choice(Button test)
    {
        result += test.GetComponentInChildren<Text>().text;
        ChoiceMaking();
    }




    public void AssignSentence(string sentence)
    {
        sentences.Enqueue(sentence);
    }
}
