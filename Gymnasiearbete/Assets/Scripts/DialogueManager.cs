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
    // Start is called before the first frame update
    void Start()
    {
        //initializes the sentences queue
        sentences = new Queue<string>();
    }
    //public bool isActive = false;
    public void StartDialogue(Dialogue d)
    //method for setting dialogue to UI
    {
        isWell = false;
        dialogue = d;
        //sets the animator bool "isActive" to true
        animator.SetBool("isActive", true);

        //sets the name of the object interacted with to the text of nametext
        nameText.text = dialogue.name;

        //clears the queue sentences
        sentences.Clear();
        try
        {

            if (!player.GetComponent<SC_TPSController>().NPCS[player.GetComponent<SC_TPSController>().RayHit.name].GetComponent<Generic_NPC>().greeted && !activeQuest && player.GetComponent<SC_TPSController>().NPCS[player.GetComponent<SC_TPSController>().RayHit.name].GetComponent<Generic_NPC>().hasQuest)
            {
                InitialQuestSentences();
            }
            else if (!player.GetComponent<SC_TPSController>().NPCS[player.GetComponent<SC_TPSController>().RayHit.name].GetComponent<Generic_NPC>().greeted)
            {
                InitialSentences();
            }
            else if (!activeQuest && player.GetComponent<SC_TPSController>().NPCS[player.GetComponent<SC_TPSController>().RayHit.name].GetComponent<Generic_NPC>().hasQuest)
            {
                QuestSentences();
            }

            else
            {
                GenericSentences();
            }
        }
        catch
        {
            if (!player.GetComponent<SC_TPSController>().NPCS[player.GetComponent<SC_TPSController>().RayHit.name].GetComponent<JonasController>().greeted && !activeQuest && player.GetComponent<SC_TPSController>().NPCS[player.GetComponent<SC_TPSController>().RayHit.name].GetComponent<JonasController>().hasQuest)
            {
                InitialQuestSentences();
            }
            else if (!player.GetComponent<SC_TPSController>().NPCS[player.GetComponent<SC_TPSController>().RayHit.name].GetComponent<JonasController>().greeted)
            {
                InitialSentences();
            }
            else if (!activeQuest && player.GetComponent<SC_TPSController>().NPCS[player.GetComponent<SC_TPSController>().RayHit.name].GetComponent<JonasController>().hasQuest)
            {
                QuestSentences();
            }

            else
            {
                GenericSentences();
            }
        }
        //displays the first sentence
        DisplayNextSentence();
    }

    void GenericSentences()
    {
        foreach (string sentece in dialogue.genericSentences)
        {
            sentences.Enqueue(sentence);
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
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        //checks if we have more sentences, if not we end dialogue
        {

            if (isWell)
            {
                ChoiceMaking();
                //Calling methof ChoiceMaking to start code to let the player choose the correct things to say in a certain order
                //to complete the quest the player must say the right things in the right order. 
                //the questgiver will say what this is. 
                //for this a new UI element need to be made. A string Queue and a string will be made.the Queue will append to the string with Queue.Enqueue()
                //then it will be checked if the resulting string is correct or not and the completion of the quest if it is, else the string will be reset.
                //IN ADDITION To start the well dialogue this mission must be ACTIVE not only exist in the quests dictionary.

                //Aight that's it for me today. Max signing off at 02:06 despite having to get up in about 3.5 hours.
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
                    // return;
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
    public void EndDialogue()
    //ends the dialogue by sending a bool to the animator that then removes the dialogue box
    {
        if (!isWell)
        {
            try
            {
                player.GetComponent<SC_TPSController>().NPCS[player.GetComponent<SC_TPSController>().RayHit.name].GetComponent<Generic_NPC>().greeted = true;
            }
            catch
            {
                player.GetComponent<SC_TPSController>().NPCS[player.GetComponent<SC_TPSController>().RayHit.name].GetComponent<JonasController>().greeted = true;
            }
        }
        animator.SetBool("isActive", false);
        buttonParent.SetActive(false);
        player.gameObject.GetComponent<SC_TPSController>().canMove = true;
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
        isWell = true;
        Debug.Log("GOOOOOD MORNING VIETNAAAAAMM");
        string[] wellSentences = { "test", "and another test" };
        animator.SetBool("isActive", true);

        nameText.text = "Well";

        sentences.Clear();
        foreach (string sentence in wellSentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();

    }
    private void ChoiceMaking()
    {

    }

}
