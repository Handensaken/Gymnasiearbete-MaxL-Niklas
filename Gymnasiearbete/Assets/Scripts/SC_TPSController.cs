using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SC_TPSController : MonoBehaviour
{

    public float speed = 7.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Transform playerCameraParent;
    public float lookSpeed = 6.0f;
    public float lookXLimit = 60.0f;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    Vector2 rotation = Vector2.zero;

    [HideInInspector]
    public bool canMove = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        rotation.y = transform.eulerAngles.y;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (characterController.isGrounded)
        {
            // We are grounded, so recalculate move direction based on axes
            Vector3 forward = transform.TransformDirection(Vector3.forward);    //Creates vectors for ease of use
            Vector3 right = transform.TransformDirection(Vector3.right);
            float curSpeedX = canMove ? speed * Input.GetAxis("Vertical") : 0;      //Sets current speed of X to input * speed if the character can move
            float curSpeedY = canMove ? speed * Input.GetAxis("Horizontal") : 0;    //-11- but for Y axis
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);      //puts vectors together 

            if (Input.GetButton("Jump") && canMove)
            {
                moveDirection.y = jumpSpeed;    //makes player jump
            }
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
    }
}
