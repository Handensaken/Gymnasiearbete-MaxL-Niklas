using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Screen.width / 2, Screen.height / 2);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
