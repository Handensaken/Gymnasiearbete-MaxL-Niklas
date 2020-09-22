using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Generic_NPC : MonoBehaviour
{

    public float wanderRadius;
    public float wanderTimer;

    private NavMeshAgent agent;
    private float timer;

    public float speed;

    public Vector3 newPosition;
    // Start is called before the first frame update
    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
        newPosition = new Vector3(0.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            newPosition = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPosition);
            timer = 0;
        }
        if (transform.position != newPosition)
        {
            speed = agent.speed;
        }

    }
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        //singleton pattern
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
