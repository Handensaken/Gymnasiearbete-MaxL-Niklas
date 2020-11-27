using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueScaler : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
            transform.position = new Vector3(Screen.width/2, ((Screen.height + -Screen.height)/2)-40);
    }
}
