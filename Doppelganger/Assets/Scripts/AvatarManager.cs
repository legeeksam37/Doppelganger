using UnityEngine;
using UnityEngine.AI;
using System;
using DG.Tweening;

public class AvatarManager : MonoBehaviour
{
    [SerializeField] Transform dest;
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] UIManager uiManager;
    [SerializeField] TVScreen tvScreen;
    [SerializeField] float speed;
    [SerializeField] float rotSpeed;
    [SerializeField] Transform playerRig;
    [SerializeField] bool verbose;

    public Action onTargetReached;

    NavMeshAgent agent;
    Animator animator;
    Outline outlineEffect;
    GameObject doppelgangerHeadText;
    const string TAG = "AvatarMovement";
    float dist = 0;
    bool hasReachedtTarget;
    bool isMouseOver = false;
    bool isPresenting;
    bool canInteract;
    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        doppelgangerHeadText = transform.GetChild(0).gameObject;

        if (doppelgangerHeadText == null)
            Debug.LogError(TAG+" No Head text at position 0 as child object !");

        dialogueManager.onSkipDialogueNode += CheckAnimSate;
        tvScreen.onClickZoomIn += StopInteraction;
        tvScreen.onClickZoomOut += ResumeInteraction;
    }

    void OnDisable()
    {
        dialogueManager.onSkipDialogueNode -= CheckAnimSate;
        tvScreen.onClickZoomIn -= StopInteraction;
        tvScreen.onClickZoomOut -= ResumeInteraction;
    }

    void StopInteraction()
    {
        canInteract = false;
    }

    void ResumeInteraction()
    {
        canInteract = true;
    }
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        outlineEffect = GetComponent<Outline>();

        canInteract = true;

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

        if (dist <= 0.5f)
        {
            agent.isStopped = true;
            LookAtTarget();

            if (!hasReachedtTarget)
            {
                onTargetReached?.Invoke();
                hasReachedtTarget = true;
                isPresenting = true;
                SetWalkAnim(false);
            }
        }

        if (isMouseOver && Input.GetMouseButtonDown(0))
        {
            if (verbose)
                Debug.Log(TAG + " Clicked on avatar");

            if (dialogueManager.EndReached())
            {
                dialogueManager.RestartDialogue();
            }
            else
            {
                dialogueManager.CancelDialogue();
            }

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

    void CheckAnimSate()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Wave"))
        {
            animator.Play("Idle"); // Transition to "Idle" if the animation is currently playing
        }
    }

    public void Move()
    {
        agent.speed = speed;
        agent.SetDestination(dest.position);
        SetWalkAnim(true);
    }

    public bool IsPresenting()
    {
        return isPresenting;
    }

    private void OnMouseEnter()
    {        
        if (!canInteract)
        {
            return;
        }

        if (isPresenting)
        {
            outlineEffect.enabled = true;
            isMouseOver = true;
            doppelgangerHeadText.SetActive(true);

            if (dialogueManager.EndReached())
            {
                uiManager.SetDoppelGangerHeadText("Restart the dialogue");
            }
            else
            {
                uiManager.SetDoppelGangerHeadText("Stop the dialogue");
            }

        }
        else
        {
            return;
        }
    }

    private void OnMouseExit()
    {
        if (!canInteract)
        {
            return;
        }

        if (isPresenting)
        {
            outlineEffect.enabled = false;
            isMouseOver = false;
            doppelgangerHeadText.SetActive(false);
        }
        else
        {
            return;
        }
    }

}
