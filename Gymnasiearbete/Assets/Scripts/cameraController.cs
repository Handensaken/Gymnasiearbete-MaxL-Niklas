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
    // Start is called before the first frame update
    void Start()
    {
        _offset = new Vector3(0, 2, -5);
        hSpeed = 2.0f;
        vSpeed = 2.0f;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mhm = Input.GetAxis("Mouse X");
        //transform.LookAt(player.transform);
        if (player.transform.position.y >= -4)
        {
            transform.position = Vector3.Slerp(transform.position, player.transform.position + _offset, 0.1f);
        }
        yaw += hSpeed * mhm;
        pitch -= vSpeed * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        print(yaw);
    }
}
