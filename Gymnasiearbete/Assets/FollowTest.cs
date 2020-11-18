using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowTest : MonoBehaviour
{
    public float speed = 20;
    public Transform target;
    Vector3 offset = new Vector3(3,-1,3);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position-offset, speed * Time.deltaTime);
        transform.LookAt(target);
    }

   

}
