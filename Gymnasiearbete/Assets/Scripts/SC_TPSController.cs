using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class SC_TPSController : MonoBehaviour
{

    public float defaultSpeed = 7.5f;
    public float speed = 0.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Transform playerCameraParent;
    public float lookSpeed = 6.0f;
    public float lookXLimit = 60.0f;

    public LayerMask whatIsGround;
    public float groundDistance = 0.3f;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    Vector2 rotation = Vector2.zero;
    public Animator thisAnim;

    public GameObject[] NPCGameObjects;
    public GameObject DialogueManager;


    Dictionary<string, GameObject> NPCS = new Dictionary<string, GameObject>();

    [HideInInspector]
    public bool canMove = true;
    //Start is called before the first frame update
    void Start()
    {

        characterController = GetComponent<CharacterController>();
        //Create string, GameObject dictionary from GameObject array
        foreach (GameObject NPC in NPCGameObjects)
        {
            NPC.name = NPC.GetComponent<Generic_NPC>().GiveName();
            NPCS.Add(NPC.name, NPC);
        }


        speed = defaultSpeed;
        rotation.y = transform.eulerAngles.y;
        Cursor.lockState = CursorLockMode.Locked;
        thisAnim.GetComponent<Animator>();
    }

    void Update()
    {
        float exportSpeed = 0;


        if (characterController.isGrounded)
        {
            // We are grounded, so recalculate move direction based on axes
            Vector3 forward = transform.TransformDirection(Vector3.forward);    //Creates vectors for ease of use
            Vector3 right = transform.TransformDirection(Vector3.right);
            float curSpeedX = canMove ? speed * Input.GetAxis("Vertical") : 0;      //Sets current speed of X to input * speed if the character can move
            float curSpeedY = canMove ? speed * Input.GetAxis("Horizontal") : 0;    //-11- but for Y axis
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);      //puts vectors together 

            //trying to find a reliable way to dynamically find speed
            if (moveDirection == new Vector3(0.0f, 0.0f, 0.0f))
            {
                exportSpeed = 0.0f;
            }


            if (Input.GetButton("Jump") && canMove)
            {
                moveDirection.y = jumpSpeed;    //makes player jump
                thisAnim.SetTrigger("jump");    //sets trigger jump for the jump animation
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = 15;
                if (moveDirection != new Vector3(0.0f, 0.0f, 0.0f))
                {
                    exportSpeed = speed;
                }
            }
            else
            {
                speed = defaultSpeed;
                if (moveDirection != new Vector3(0.0f, 0.0f, 0.0f))
                {
                    exportSpeed = defaultSpeed;
                }
            }
            thisAnim.SetBool("grounded", true);

        }
        else
        {
            thisAnim.SetBool("grounded", false);
        }
        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        moveDirection.y -= gravity * Time.deltaTime;

        characterController.Move(moveDirection * Time.deltaTime); // Moves the player

        // Player and Camera rotation
        if (transform.position.y > -4)
        {
            rotation.y += Input.GetAxis("Mouse X") * lookSpeed;
            rotation.x += -Input.GetAxis("Mouse Y") * lookSpeed;        //gets rotation from mouse movement
            rotation.x = Mathf.Clamp(rotation.x, -lookXLimit, lookXLimit);  //clamps rotation x between looklimits
            playerCameraParent.localRotation = Quaternion.Euler(rotation.x, 0, 0);  //applies rotation to transform
            transform.eulerAngles = new Vector2(0, rotation.y); //applies y rotation to transform
        }
        //Set float spped for the animation
        thisAnim.SetFloat("speed", exportSpeed);
        bool activeDialogue = DialogueManager.GetComponent<DialogueManager>().GiveBool();
        //Manage dialogue
        if (activeDialogue)
        {
            if (/*Input.GetKeyDown(KeyCode.E) ||*/ Input.GetKeyDown(KeyCode.Return))
            {
                DialogueManager.GetComponent<DialogueManager>().DisplayNextSentence();
            }
        }
    }
    //Fixed Update is used for physics calculations
    void FixedUpdate()
    {
        Vector3 currentPos = transform.position;
        currentPos.y += 2.0f;
        RaycastHit ray;

        Color color = Color.red;

        Debug.DrawRay(currentPos, transform.forward * 3, color, 0.0f);
        if (Physics.Raycast(currentPos, transform.forward, out ray, 3.0f))
        {
            if (ray.collider.CompareTag("person"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    NPCS[ray.collider.gameObject.name].GetComponent<Generic_NPC>().SendDialogueBool(true);
                    NPCS[ray.collider.gameObject.name].GetComponent<Generic_NPC>().TriggerDialogue();
                }
            }
        }
    }
}
