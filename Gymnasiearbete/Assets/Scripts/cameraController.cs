using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{

    public GameObject player;
    public Vector3 _offset;

    public float hSpeed;
    public float vSpeed;

    public float yaw;
    public float pitch;

    public float playerDistance;
    // Start is called before the first frame update
    void Start()
    {
        _offset = new Vector3(0, 2, -5);
        hSpeed = 2.0f;
        vSpeed = 2.0f;
        playerDistance = -5.0f;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float cameraAngleY = (Mathf.PI / 180) * transform.eulerAngles.y;
        float cameraAngleX = (Mathf.PI / 180) * transform.eulerAngles.x;
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");


        yaw += hSpeed * mouseX;
        pitch -= vSpeed * mouseY;

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        if (player.transform.position.y >= -4)
        {
            _offset = new Vector3(Mathf.Sin(cameraAngleY) * playerDistance, Mathf.Sin(cameraAngleX) * 5, Mathf.Cos(cameraAngleY + cameraAngleX) * playerDistance);
            transform.position = Vector3.Slerp(transform.position, player.transform.position + _offset, 0.1f);
        }
    }
}
