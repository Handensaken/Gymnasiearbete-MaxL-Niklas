using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class SC_CameraCollision : MonoBehaviour
{
    //creates transform to reference parent
    public Transform referenceTransform;
    //creates float deciding the distance offset from collision to prevent clipping
    public float collisionOffset = 0.2f;
    //creates vector defaultPos
    Vector3 defaultPos;
    //creates vector directionNormalized
    Vector3 directionNormalized;
    //another Transform directed towards parent
    Transform parentTransform;
    //creates float for a default distance between vector.zero and camera
    float defaultDistance;
    //creates GameObject referencing the player
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        //sets the default position to the transform's local position
        defaultPos = transform.localPosition;
        //normalizes the default position and puts it in directionNormalized
        directionNormalized = defaultPos.normalized;
        //assigns the parent trasform to paretTransform
        parentTransform = transform.parent;
        //sets default distance to the distance between the camera and vector.zero
        defaultDistance = Vector3.Distance(defaultPos, Vector3.zero);
    }

    // FixedUpdate for physics calculations
    void FixedUpdate()
    {
        //creates vector containing the default position
        Vector3 currentPos = defaultPos;
        //creates a RaycastHit to store infromation from the raycast
        RaycastHit hit;

        //creates direction vector based on defaultPos's world position and referenceTransform's position
        // where referenceTransform's position is the position of the player and parentTransform's default position is the position of the camera
        Vector3 dirTmp = parentTransform.TransformPoint(defaultPos) - referenceTransform.position;

        //casts a sphere that checks for objects between the player and the camera. If something is the camera's position is changed based on the
        //distance between the player and the object with an offset to minimalize clipping
        if (Physics.SphereCast(referenceTransform.position, collisionOffset, dirTmp, out hit, defaultDistance))
        {
            currentPos = (directionNormalized * (hit.distance - collisionOffset));
        }
        //moves camera object to position set above
        transform.localPosition = Vector3.Lerp(transform.localPosition, currentPos, Time.deltaTime * 15f);  
    }
}
