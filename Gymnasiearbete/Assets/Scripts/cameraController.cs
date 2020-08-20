using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{

    public GameObject player;
    public Vector3 _offset;
    // Start is called before the first frame update
    void Start()
    {
        _offset = new Vector3(0, 2, -5);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(player.transform);

        if (player.transform.position.y >= -4)
        {
            transform.position = Vector3.Slerp(transform.position, player.transform.position + _offset, 0.1f);
        }
    }
}
