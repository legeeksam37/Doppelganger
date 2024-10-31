using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AvatarMovement : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] Transform dest;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (dest != null)
        {
            agent.SetDestination(dest.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
