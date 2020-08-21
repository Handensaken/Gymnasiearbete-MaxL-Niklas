using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float xInput;
    public float zInput;
    public float movementSpeed;
    public float jumpPower;
    public float mouseX;
    public float MouseY;
    bool jumpLimiter;
    public Vector3 movement;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = 2;
        rb = GetComponent<Rigidbody>();
        jumpPower = 300;
        jumpLimiter = false;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        xInput = Input.GetAxis("Horizontal");       //Gets Input for use on X-axis
        zInput = Input.GetAxis("Vertical");         //Gets input for use on Z-axis
        movement = Vector3.forward * zInput * movementSpeed * Time.deltaTime + Vector3.right * xInput * movementSpeed * Time.deltaTime;     //uses prebuilt vectors and adapts them to create a movement vector
        transform.Translate(movement);      //Applies new movement vector to gameObject
        transform.rotation = Quaternion.identity;   //locks rotation
        if (Input.GetKeyDown(KeyCode.Space) && !jumpLimiter)
        {
            rb.AddForce(Vector3.up * jumpPower);
            jumpLimiter = true;
        }


     /*   mouseX = Input.GetAxis("Mouse X");      //gets movement of mouse on x-axis
        MouseY = Input.GetAxis("Mouse Y");      //-11- y-axis
        print(mouseX);
        print(MouseY);
       */
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            //make jump possible
            jumpLimiter = false;
        }
    }
}
