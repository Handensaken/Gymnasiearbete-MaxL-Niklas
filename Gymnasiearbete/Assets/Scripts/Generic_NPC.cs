using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Generic_NPC : MonoBehaviour
{
    //creates GameObject DialogueManager
    public GameObject DialogueManager;


    public GameObject questTracker;

    //creates activeDialogue bool and sets it to false
    bool activeDialogue = false;

    public bool hasQuest;

    //creates Transform to reference the player
    public Transform Player;

    //creates float to define radius NPC can find a new position in
    public float wanderRadius;
    //creates a float to define how often an NPC will find a new position
    public float wanderTimer;
    //creates a vector used to store the new position
    public Vector3 newPosition;
    //creates a NavMeshAgent so the NPC can interact with the NavMesh applied to terrain
    private NavMeshAgent agent;
    //creates a float to be used as a timer.
    private float timer;

    //creates float speed for exporting to Animator
    public float speed;
    //creates Dialogue
    public Dialogue dialogue;
    //creates Animator to allow script to communicate with animator
    public Animator anim;

    public bool greeted;
    bool addOnce = false;



    private void Start()
    {
        greeted = false;
    }
    // Start is called before the first frame update
    void OnEnable()
    {
        wanderTimer = Random.Range(5, 40);
        //gets NavMeshAgent
        agent = GetComponent<NavMeshAgent>();
        //sets timer to be equal to wander timer so NPC moves immediatley (not necessary)
        timer = wanderTimer;
        //sets the default new position to be 0.0 on all axes
        newPosition = new Vector3(0.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!activeDialogue)
        //checks if the character is engaged in dialogue and stops movement if it is
        {
            //makes the timer go up
            timer += Time.deltaTime;
            if (timer >= wanderTimer)
            //checks if timer is equal to or greater than wanderTimer and calls method to find new position. Then moves to said position and resets timer
            {
                newPosition = RandomNavSphere(transform.position, wanderRadius, -1);
                agent.SetDestination(newPosition);
                timer = 0;
                wanderTimer = Random.Range(5, 40);
            }
            if (transform.position != newPosition)
            //checks if the NPC has arrived at it's new position and if not sets speed to the agent's speed to later be used in an Animator
            {
                speed = agent.speed;
            }
            else
            {
                speed = 0;
            }
        }
        else
        {
            // !!NOT WORKING!! transform.LookAt(Player);
            speed = 0;
            activeDialogue = DialogueManager.GetComponent<DialogueManager>().SendDialogueActive();
        }
        if (greeted)
        {
            if (!addOnce)
            {
                Debug.Log("added");
                questTracker.GetComponent<QuestTracker>().greetQuest.Add(true);
                addOnce = true;
            }
            //greeted = true;
        }
        anim.SetFloat("speed", speed);
    }
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    //new method for finding new position
    {
        //sets a random direction based on a random point inside a sphere with a base radius of one that is changed with the dist variable
        Vector3 randDirection = Random.insideUnitSphere * dist;
        //adds origin to randDirection
        randDirection += origin;
        //creates a NavMeshHit to store information from SamplePosition
        NavMeshHit navHit;
        //Samples the position closest to the source position in this case randDirection with a distance of dist and puts data in navHit.
        //layermask is kept at -1 as there are no other navmeshes to conflict
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        //returns the position recieved from SamplePosition
        return navHit.position;
    }

    public void TriggerDialogue()
    //method to find DialogueManager object and initiate it's method StartDialogue with the inspector assigned dialogue as paramater
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        FindObjectOfType<DialogueManager>().checkQuest(hasQuest);
        //research singleton pattern
    }
    public string GiveName()
    //method to allow other objects to easily recieve the name of the object they are interacting with
    {
        return dialogue.name;
    }
    public void RecieveDialogueBool(bool reciever)
    //method to recieve a bool to change dialogue status 
    {
        activeDialogue = reciever;
    }
}
