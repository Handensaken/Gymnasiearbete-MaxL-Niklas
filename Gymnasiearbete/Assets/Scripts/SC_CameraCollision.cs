using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_CameraCollision : MonoBehaviour
{
    public Transform referenceTransform;    //parent's transform
    public float collisionOffset = 0.2f; //To prevent Camera from clipping through Objects

    Vector3 defaultPos;
    Vector3 directionNormalized;
    Transform parentTransform; 
    float defaultDistance;

    public GameObject player;   //player
    // Start is called before the first frame update
    void Start()
    {
        defaultPos = transform.localPosition;
        directionNormalized = defaultPos.normalized;
        parentTransform = transform.parent;
        defaultDistance = Vector3.Distance(defaultPos, Vector3.zero);   //sets default distance to the distance for the camera
    }

    // FixedUpdate for physics calculations
    void FixedUpdate()
    {
        Vector3 currentPos = defaultPos;
        RaycastHit hit;
        Vector3 dirTmp = parentTransform.TransformPoint(defaultPos) - referenceTransform.position;       //creates direction vector based on defaultPos's world position and referenceTransform's position
                                                                                                        // where referenceTransform's position is the position of the player and parentTransform's default position is the position of the camera
      
        if (Physics.SphereCast(referenceTransform.position, collisionOffset, dirTmp, out hit, defaultDistance))     //if a collider is between the camera and player SphereCast returns true and runs of statement
        {
            currentPos = (directionNormalized * (hit.distance - collisionOffset));  //sets the camera's new position based on raycast hit on collider with an offset to minimalize clipping
        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, currentPos, Time.deltaTime * 15f);  //moves camera object to position set above
    }
    private void LateUpdate()
    {
        if (transform.position.y <= -4) //Stops the camera and looks after the player when falling
        {
            transform.LookAt(player.transform);     //rotates the camera to always look at the player 
            transform.position = new Vector3(transform.position.x, -4, transform.position.z);   //freezes camera y-position 
        }
    }

}
