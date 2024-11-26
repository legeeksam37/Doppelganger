using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using DG.Tweening;

public class AvatarManager : MonoBehaviour
{
    [SerializeField] Transform dest;
    [SerializeField] float speed;
    [SerializeField] float rotSpeed;
    [SerializeField] Transform playerRig;
    [SerializeField] bool isPresenting;

    public Action onTargetReached;

    NavMeshAgent agent;
    Animator animator;
    Outline outlineEffect;
    const string TAG = "AvatarMovement";
    float dist = 0;
    bool hasReachedtTarget;
    // Start is called before the first frame update

    private void OnEnable()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        outlineEffect = GetComponent<Outline>();

        if (outlineEffect == null)
        {
            Debug.LogError(TAG + " Missing outline");
        }

        if (agent == null || dest == null || animator == null)
        {
            Debug.LogError(TAG + " unassigned variables");
            return;
        }
    }

    bool state;
    void Update()
    {
        dist = Vector3.Distance(transform.position, dest.position);
        //Debug.Log(TAG+" distance avatar object : "+dist);

        if (dist <= 0.5f)
        {
            agent.isStopped = true;
            LookAtTarget();

            if (!hasReachedtTarget)
            {
                hasReachedtTarget = true;
                isPresenting = true;
                SetWalkAnim(false);
                onTargetReached?.Invoke();
            }

        }
            
        if (Input.GetKeyDown(KeyCode.A))
        {
            SetWalkAnim(state);
            state = !state;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            Wave();
        }

    }

    public void SetWalkAnim(bool state)
    {
        animator.SetBool("walk", state);
    }

    public void TestMove()
    {
        transform.DOMove(dest.position, 5f);
    }

    public void Wave()
    {
        animator.SetTrigger("wave");
    }

    void LookAtTarget()
    {
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
        SetWalkAnim(true);
    }

    private void OnMouseEnter()
    {
        if (isPresenting)
            outlineEffect.enabled = true;
        else
            return;
    }

    private void OnMouseExit()
    {
        if (isPresenting)
            outlineEffect.enabled = false;
        else
            return;
    }
}
