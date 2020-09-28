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
    public GameObject questBox;

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

        foreach (string sentence in dialogue.sentences)
        //Enqueues every sentence in dialogues sentences
        {
            sentences.Enqueue(sentence);
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
    }
    public void EndDialogue()
    //ends the dialogue by sending a bool to the animator that then removes the dialogue box
    {
        animator.SetBool("isActive", false);
        buttonParent.SetActive(false);
    }
    public bool SendDialogueActive()
    //sends a bool based on if a dialogue is active or not
    {
        return animator.GetBool("isActive");
    }

    bool hasQuest;
    public void test(bool receivedParamater)
    {
        hasQuest = receivedParamater;
    }
    public void InitiateQuest()
    {
        questBox.SetActive(true);
    }
    public void EndQuest()
    {
        questBox.SetActive(false);
    }
}
