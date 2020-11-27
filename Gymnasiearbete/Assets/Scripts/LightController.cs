using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public Transform lightSource;
    private string time = "";
    // Start is called before the first frame update
    void Start()
    {
        switch (Random.Range(1,5))
        {
            case 1:
                {
                    time = "dawn";
                    Debug.Log("1");
                    lightSource.transform.rotation = Quaternion.Euler(0,0,0);
                    return;
                }
            case 2:
                {
                    time = "day";
                    Debug.Log("2");
                    lightSource.transform.rotation = Quaternion.Euler(90,0,0);
                    return;
                }
            case 3:
                {
                    time = "dusk";
                    Debug.Log("3");
                    lightSource.transform.rotation = Quaternion.Euler(180, 0, 0);
                    return;
                }
            case 4:
                {
                    time = "night";
                    Debug.Log("4");
                    lightSource.transform.rotation = Quaternion.Euler(270,0,0);
                    return;
                }
            default:
                {
                    time = "night";
                    Debug.Log("default");
                    lightSource.transform.rotation = Quaternion.Euler(270, 0, 0);
                    return;
                }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetTime()
    {
        return time;
    }

}
