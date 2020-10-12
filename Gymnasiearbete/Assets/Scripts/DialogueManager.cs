using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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


    public GameObject player;

    //creates Animator
    public Animator animator;

    //creates a string queue containging sentences
    private Queue<string> sentences;
    // Start is called before the first frame update
    void Start()
    {
        //initializes the sentences queue
        sentences = new Queue<string>();
    }
    //public bool isActive = false;
    public void StartDialogue(Dialogue dialogue)
    //method for setting dialogue to UI
    {
        //sets the animator bool "isActive" to true
        animator.SetBool("isActive", true);

        //sets the name of the object interacted with to the text of nametext
        nameText.text = dialogue.name;

        //clears the queue sentences
        sentences.Clear();
        if (!player.GetComponent<SC_TPSController>().NPCS[player.GetComponent<SC_TPSController>().RayHit.name].GetComponent<Generic_NPC>().greeted)
        {
            foreach (string sentence in dialogue.initialSentences)
            //Enqueues every sentence in dialogues sentences
            {
                sentences.Enqueue(sentence);
            }
        }
        else
        {
            foreach(string sentence in dialogue.genericSentences)
            {
                sentences.Enqueue(sentence);
            }
        }
        //displays the first sentence
        DisplayNextSentence();
    }
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        //checks if we have more sentences, if not we end dialogue
        {
            if (!hasQuest)
            //checks if the npc in question has a quest
            {
                EndDialogue();
                return;
            }
            else
            {
                TriggerChoices();
                return;
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
        player.GetComponent<SC_TPSController>().NPCS[player.GetComponent<SC_TPSController>().RayHit.name].GetComponent<Generic_NPC>().greeted = true;
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



}
