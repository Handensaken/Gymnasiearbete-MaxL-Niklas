using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JonasController : MonoBehaviour
{
    public Dialogue dialogue;
    public GameObject questTracker;
    public GameObject DialogueManager;
    public Animator anim;
    public bool hasQuest;
    bool activeDialogue = false;
    public string target;
    public bool greeted;




    // Start is called before the first frame update
    void Start()
    {
        SC_TPSController.NPCS.Add(this.name, this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void TriggerDialogue()
    //method to find DialogueManager object and initiate it's method StartDialogue with the inspector assigned dialogue as paramater
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        FindObjectOfType<DialogueManager>().checkQuest(hasQuest);
        //research singleton pattern
    }
    public void RecieveDialogueBool(bool reciever)
    //method to recieve a bool to change dialogue status 
    {
        activeDialogue = reciever;
    }
}
