using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InidcatorPos : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Screen.width - 40, (-Screen.height+Screen.height)/2 + 40);
    }
}
