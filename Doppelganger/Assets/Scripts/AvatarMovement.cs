using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AvatarMovement : MonoBehaviour
{
    [SerializeField] Transform dest;
    [SerializeField] float speed;
    [SerializeField] float rotSpeed;
    [SerializeField] Transform playerRig;

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

        if (dist <= 0.5f)
        {
            agent.isStopped = true;
            LookAtTarget();
        }
            
    }

    void LookAtTarget()
    {
        Debug.Log("look at target");
        Vector3 dir = playerRig.position - transform.position;
        Quaternion rot = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), rotSpeed * Time.deltaTime);
        rot.x = 0;
        rot.z = 0;
        transform.rotation = rot;
    }

    public void Move()
    {
        agent.speed = speed;
        agent.SetDestination(dest.position);
        
    }
}
