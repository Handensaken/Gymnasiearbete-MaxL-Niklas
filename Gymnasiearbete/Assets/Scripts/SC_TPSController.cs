using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.ComponentModel;
using System.ComponentModel.Design;


[RequireComponent(typeof(CharacterController))]
public class SC_TPSController : MonoBehaviour
{

    public GameObject logError;
    //sets the default speed for the player
    public float defaultSpeed = 7.5f;
    public float sprintSpeed = 15;
    //creates float to keep track of the current speed
    public float speed = 0.0f;
    //creates float deciding the speed of a jump
    public float jumpSpeed = 8.0f;
    //creates float deciding how high the gravity is
    public float gravity = 20.0f;
    //creates CharacterController
    CharacterController characterController;
    //creates moveDirection vector and sets it to 0
    Vector3 moveDirection = Vector3.zero;
    //creates rotation vector and sets it to 0
    Vector2 rotation = Vector2.zero;

    public GameObject Tracker;

    //creates Transform to refernce parent object
    public Transform playerCameraParent;
    //creates float deciding how fast you look around you
    public float lookSpeed = 6.0f;
    //creates limit to how far up/down you can look
    public float lookXLimit = 60.0f;

    //creates variale of Animator type
    public Animator thisAnim;

    //public GameObject array (NPCs assigned in inpector)
    public GameObject[] NPCGameObjects;
    //creates GameObject to use DialogueManager
    public GameObject DialogueManager;


    public GameObject fuckerMan;

    //creates bool to read if ray exists
    bool rayExists = false;
    //creates RaycastHit object to store ray data
    RaycastHit rayData;
    //creates RayHit GameObject 
    public GameObject RayHit;
    //creates bool for checking wether an object is valid or not
    bool validObject = false;

    //creates empty dictionary of type <string, GameObject>
    public static Dictionary<string, GameObject> NPCS = new Dictionary<string, GameObject>();

    public GameObject QuestTracker;

    public GameObject QuestGiver;


    public GameObject mainQuestController;


    //bool to define if player can move
    public bool canMove = true;
    //Start is called before the first frame update
    void Start()
    {
        //gets CharacterController
        characterController = GetComponent<CharacterController>();
        /*foreach (GameObject NPC in NPCGameObjects)
        //Create <string, GameObject> dictionary from GameObject array
        {
            NPC.name = NPC.GetComponent<Generic_NPC>().GiveName();
            NPCS.Add(NPC.name, NPC);
        }*/

        //sets speed to be the default speed
        speed = defaultSpeed;
        //gets rotation based on angles for Y axis.
        rotation.y = transform.eulerAngles.y;
        //locks the cursor to the middle of the screen and hides it there
         Cursor.lockState = CursorLockMode.Locked;
        //gets animator
        thisAnim.GetComponent<Animator>();
    }

    void Update()
    {
        //prepares variable to send speed to animator
        float exportSpeed = 0;

        if (characterController.isGrounded)
        // if the player is grounded, recalculate move direction based on axes
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward);    //Creates vectors for ease of use
            Vector3 right = transform.TransformDirection(Vector3.right);
            float curSpeedX = canMove ? speed * Input.GetAxis("Vertical") : 0;      //Sets current speed of X to input * speed if the character can move
            float curSpeedY = canMove ? speed * Input.GetAxis("Horizontal") : 0;    //-11- but for Y axis
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);      //puts vectors together 

            if (Input.GetButton("Jump") && canMove)
            //makes player jump and sends jump trigger to animator
            {
                moveDirection.y = jumpSpeed;
                thisAnim.SetTrigger("jump");
            }
            if (Input.GetKey(KeyCode.LeftShift))
            //checks so the player is moving and if so increases speed and exportSpeed
            {
                speed = sprintSpeed;
            }
            else
            //otherwise set speed to base speed and as long as the player is moving base speed is exported
            {
                speed = defaultSpeed;
            }
            if (moveDirection != new Vector3(0.0f, 0.0f, 0.0f))
            {
                exportSpeed = speed;
            }

            if (moveDirection == new Vector3(0.0f, 0.0f, 0.0f))
            //trying to find a reliable way to dynamically send speed to animator
            {
                exportSpeed = 0.0f;
            }
            //exports speed to animator
            thisAnim.SetFloat("speed", exportSpeed);

            //lets the jump animation play or initiates land animation
            thisAnim.SetBool("grounded", true);

        }
        else
        //initiates mid air animation
        {
            thisAnim.SetBool("grounded", false);
        }
        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        moveDirection.y -= gravity * Time.deltaTime;

        characterController.Move(moveDirection * Time.deltaTime); // Moves the player
        bool activeDialogue = DialogueManager.GetComponent<DialogueManager>().SendDialogueActive();

        // Player and Camera rotation
        if (!activeDialogue)
        {
            rotation.y += Input.GetAxis("Mouse X") * lookSpeed;
            rotation.x += -Input.GetAxis("Mouse Y") * lookSpeed;        //gets rotation from mouse movement'
        }
        rotation.x = Mathf.Clamp(rotation.x, -lookXLimit, lookXLimit);  //clamps rotation x between looklimits
        playerCameraParent.localRotation = Quaternion.Euler(rotation.x, 0, 0);  //applies rotation to transform
        transform.eulerAngles = new Vector2(0, rotation.y); //applies y rotation to transform


        //Gets bool to check if a dialogue is active if it is, lets player continue the dialogue;
        if (activeDialogue)
        {
            if (/*Input.GetKeyDown(KeyCode.E) ||*/ Input.GetKeyDown(KeyCode.Return))
            {
                DialogueManager.GetComponent<DialogueManager>().DisplayNextSentence();
            }
        }

        //lets the player interact with a valid GameObject in range
        if (Input.GetKeyDown(KeyCode.E) && validObject && !activeDialogue && (Vector3.Distance(rayData.collider.gameObject.transform.position, transform.position) <= 7))
        {


            DialogueManager.GetComponent<DialogueManager>().targetQuestDialogueBool = false;
            if (rayData.collider.CompareTag("person"))
            {

                if (QuestTracker.GetComponent<QuestTracker>().quests.ContainsKey("New Pickaxe"))
                {
                    if (!QuestTracker.GetComponent<QuestTracker>().quests["New Pickaxe"])
                    {
                        if (RayHit.GetComponent<Generic_NPC>().dialogue.name == NPCS["male_blacksmith"].GetComponent<Generic_NPC>().target)
                        {

                            QuestTracker.GetComponent<QuestTracker>().EndQuest();
                            DialogueManager.GetComponent<DialogueManager>().targetQuestDialogueBool = true;
                            string[] tempArr = { "Thank you dear! Now I know to cook him a large dinner" };
                            DialogueManager.GetComponent<DialogueManager>().SetNewSentences(tempArr);
                            SC_TPSController.NPCS["male_blacksmith"].GetComponent<Generic_NPC>().hasQuest = false;
                            QuestTracker.GetComponent<QuestTracker>().questMarks[3].SetActive(false);
                        }
                        else
                        {
                            DialogueManager.GetComponent<DialogueManager>().targetQuestDialogueBool = false;
                        }

                    }

                }

                if (QuestTracker.GetComponent<QuestTracker>().quests.ContainsKey("Mind of the King"))
                {
                    if (!QuestTracker.GetComponent<QuestTracker>().quests["Mind of the King"])
                    {
                        if (RayHit.GetComponent<Generic_NPC>().dialogue.name == NPCS["Jonas"].GetComponent<JonasController>().target)
                        {
                            mainQuestController.GetComponent<MainQuestController>().EndQuestDebug();
                            DialogueManager.GetComponent<DialogueManager>().targetQuestDialogueBool = true;
                            string[] tempArr = { "Is this about that weird mage that turned up!?", "No I don't know him! I don't know why he's here", "Yes I'll make sure he leaves soon." };
                            DialogueManager.GetComponent<DialogueManager>().SetNewSentences(tempArr);
                            QuestTracker.GetComponent<QuestTracker>().questMarks[6].SetActive(false);
                            QuestTracker.GetComponent<QuestTracker>().questMarks[11].SetActive(true);
                        }
                        else
                        {
                            DialogueManager.GetComponent<DialogueManager>().targetQuestDialogueBool = false;
                        }
                    }
                }
                if (QuestTracker.GetComponent<QuestTracker>().quests.ContainsKey("A Final Sacrifice"))
                {
                    if (!QuestTracker.GetComponent<QuestTracker>().quests["A Final Sacrifice"])
                    {
                        if (rayData.collider.gameObject.name == "Jonas")
                        {
                            if (RayHit.GetComponent<JonasController>().dialogue.name == "Jonas" && Vector3.Distance(NPCS["boy_home_1"].transform.position, NPCS["Jonas"].transform.position) <= 20)
                            {
                                Debug.Log("Denna borde funka");
                                DialogueManager.GetComponent<DialogueManager>().evilEnd = true;
                                DialogueManager.GetComponent<DialogueManager>().targetQuestDialogueBool = true;
                                string[] tempArr = { "Good. Good. He will make a fine tribute.", "Now make yourself scarce and don't look back." };
                                DialogueManager.GetComponent<DialogueManager>().SetNewSentences(tempArr);
                            }
                            else
                            {
                                DialogueManager.GetComponent<DialogueManager>().targetQuestDialogueBool = false;
                            }
                        }
                        else
                        {
                            if (RayHit.GetComponent<Generic_NPC>().dialogue.name == "Bert")
                            {
                                DialogueManager.GetComponent<DialogueManager>().targetQuestDialogueBool = true;
                                string[] tempArr = { "Ay yo what's popping?" };
                                DialogueManager.GetComponent<DialogueManager>().SetNewSentences(tempArr);

                            }
                            else
                            {
                                DialogueManager.GetComponent<DialogueManager>().targetQuestDialogueBool = false;
                            }
                        }



                    }
                }

                //iamlosingmymind();



                if (rayData.collider.gameObject == fuckerMan)
                {
                    NPCS[RayHit.name].GetComponent<JonasController>().RecieveDialogueBool(true);
                    NPCS[RayHit.name].GetComponent<JonasController>().TriggerDialogue();
                }
                else
                {
                    NPCS[RayHit.name].GetComponent<Generic_NPC>().RecieveDialogueBool(true);
                    NPCS[RayHit.name].GetComponent<Generic_NPC>().TriggerDialogue();
                }


            }
            if (rayData.collider.CompareTag("well"))
            {
                DialogueManager.GetComponent<DialogueManager>().InitiateWellQuest();
            }
        }
        if (rayExists && activeDialogue)
        //if statement that cancels dialogue if the player moves to far from source
        {
            if (Vector3.Distance(transform.position, rayData.collider.gameObject.transform.position) > 7)
            {
                DialogueManager.GetComponent<DialogueManager>().EndDialogue();
            }
        }
    }
    /*void doThing()
    {

    }*/



    public GameObject keyIndicator;
    void PressInstructions()
    {
        keyIndicator.SetActive(true);
    }
    //Fixed Update is used for physics calculations
    void FixedUpdate()
    {
        //sets up variables for upcomming raycast
        Vector3 currentPos = transform.position;
        currentPos.y += 2.0f;
        RaycastHit ray;

        //visible color to visualize ray
        Color color = Color.red;
        //vizualize ray (only in editor)
        Debug.DrawRay(currentPos, transform.forward * 5, color, 0.0f);
        //Cast ray from players position forward, put the data from hit object in ray, maximum distance is 3.0
        if (Physics.Raycast(currentPos, transform.forward, out ray, 5.0f))
        {
            //if ray hits a person let the GameObject RayHit get the gameObject value of the ray. Also sets validObject to true so it can be interacted with
            if (ray.collider.CompareTag("person"))
            {
                RayHit = ray.collider.gameObject;
                rayData = ray;
                validObject = true;
                rayExists = true;
                if (!DialogueManager.GetComponent<DialogueManager>().animator.GetBool("isActive"))
                {
                    PressInstructions();
                }
                else { keyIndicator.SetActive(false); }
            }
            else if (QuestTracker.GetComponent<QuestTracker>().quests.ContainsKey("The Sentient Well"))
            {
                if (!QuestTracker.GetComponent<QuestTracker>().quests["The Sentient Well"])
                {

                    if (ray.collider.CompareTag("well"))
                    {

                        RayHit = ray.collider.gameObject;
                        rayData = ray;
                        validObject = true;
                        rayExists = true;
                        if (!DialogueManager.GetComponent<DialogueManager>().animator.GetBool("isActive"))
                        {
                            PressInstructions();
                        }
                        else { keyIndicator.SetActive(false); }
                    }
                }
            }
            else
            {
                validObject = false;
            }
        }
        else
        {
            keyIndicator.SetActive(false);
        }
    }
}
