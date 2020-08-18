using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float xInput;
    public float zInput;
    public float movementSpeed;
    public Vector3 movement;
    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = 2;
    }

    // Update is called once per frame
    void Update()
    {
        xInput = Input.GetAxis("Horizontal");       //Gets Input for use on X-axis
        zInput = Input.GetAxis("Vertical");         //Gets input for use on Z-axis
        movement = Vector3.forward * zInput * movementSpeed * Time.deltaTime + Vector3.right * xInput * movementSpeed * Time.deltaTime;     //uses prebuilt vectors and adapts them to create a movement vector

        transform.Translate(movement);      //Applies new movement vector to gameObject
    }
}
