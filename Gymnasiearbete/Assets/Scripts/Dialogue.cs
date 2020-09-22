using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//makes it visible in the inspector for ease of use
[System.Serializable]
public class Dialogue
{
    //creates a string containing the name of an object
    public string name;
    //makes string array input fields look a lot nicer
    [TextArea(3, 10)]
    //creates a string array containing what the object may say when interacted with
    public string[] sentences;

}
