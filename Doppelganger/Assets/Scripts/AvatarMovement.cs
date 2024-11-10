using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AvatarMovement : MonoBehaviour
{
    [SerializeField] Transform dest;
    [SerializeField] float speed;

    NavMeshAgent agent;
    const string TAG = "AvatarMovement";
    float dist = 0;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (agent == null && dest == null)
        {
            Debug.LogError(TAG + " unassigned variables");
            return;
        }

       // Move();
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(transform.position, dest.position);
        //Debug.Log(TAG+" distance avatar object : "+dist);

        if (dist <= 2.0f)
        {
            agent.isStopped = true;
        }
            
    }

    public void Move()
    {
        agent.speed = speed;
        agent.SetDestination(dest.position);
    }
}
