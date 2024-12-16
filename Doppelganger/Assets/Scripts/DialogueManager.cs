using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] bool verbose;
    [SerializeField] GameObject dialogueBtn;
    [SerializeField] UIManager uiManager;
    [SerializeField] AudioSource audioSource;
    [SerializeField] GameObject canvasParent;
    [SerializeField] AvatarManager avatar;
    [SerializeField] SoundManager soundManager;

    public Action onSkipDialogueNode;
    public Action onLastNodeReached;
    public Action onTextChnaged;
    public Action onTalk;
    public Action onTalkFinished;
    public List<Node> dialogueNodes = new List<Node>();

    const string TAG = "DIALOGUE MANAGER ";

    TextMeshProUGUI dialogueButtonText;
    string text = "";
    string dialogueContent;
    int index;
    bool endReached;
    bool inCouroutine;
    bool noButton;
    bool inDialogue;
    List<Node> interactionsDialogueNodes = new List<Node>();
    Coroutine dialogueCoroutine;

    void Start()
    {
        if (avatar == null)
        {
            Debug.LogError("Avatar is null");
            return;
        }

        index = 0;
        avatar.Wave();
        RunFistNode();
    }

    void OnDisable()
    {
        foreach (Node node in interactionsDialogueNodes)
        {
            node.hasInteraction = true;     
        }
    }

    void RunFistNode()
    {
        index = 0;
        endReached = false;
        dialogueBtn.SetActive(true);
        dialogueButtonText = dialogueBtn.GetComponentInChildren<TextMeshProUGUI>();
        dialogueButtonText.text = dialogueNodes[0].dialogueText;
    }

    public bool EndReached()
    {
        return endReached;
    }

    public void RestartDialogue()
    {
        CheckAndClearButtons();
        uiManager.ClearSubtitles();
        uiManager.ShowSubTitles();
        endReached = false;
        RunFistNode();
    }

    public void RunDialogueNodes(string name = null)
    {
        if (verbose)
            Debug.Log("Index : " + index+", Dialogue id : "+ dialogueNodes[index].id);

        if (endReached)
        {
            return;
        }


        List<Node> nextNodesList = new List<Node>();

        if (verbose)
            Debug.Log("Index : " + index);

        Node nextNode = null;

        Node firstNode = dialogueNodes[index];

        if (verbose)
            Debug.Log("Can interract : " + dialogueNodes[index].hasInteraction);

        if (dialogueNodes[index].hasInteraction)
        {
            avatar.Move();
            dialogueNodes[index].hasInteraction = false; // set the interaction to false so it doesn't restart it at the next dialogue 
            interactionsDialogueNodes.Add(dialogueNodes[index]); // add it to the interactions list, at the end it s reset to true
        }

        if (verbose)
            Debug.Log(TAG + "Current node Id : " + dialogueNodes[index].id + ", current index : " + index);

        PlayAudioClip();
        DisplayDialogueText();

        //if last node (no next nodes)
        if (dialogueNodes[index].nextNodes.Count == 0)
        {
           
            if (verbose)
                Debug.Log("Last node");

            if (name == "skip")
            {
                soundManager.StopDoppelgangerAudio();
            }

            CheckAndClearButtons();
            inCouroutine = false;
            endReached = true;
            uiManager.ClearSubtitles();
            onLastNodeReached?.Invoke();
            return;
           
                     
        }


        for (int j = 0; j <= dialogueNodes[index].nextNodes.Count - 1; j++)
        {
            nextNode = GetNodeById(dialogueNodes[index].nextNodes[j]);
            nextNodesList.Add(nextNode);
            

            if (name == "skip")
            {
                UpdateIndex(nextNode.id);
                CheckAndClearButtons();
                NextDialogueButton();
                uiManager.ClearSubtitles();
                soundManager.StopDoppelgangerAudio();

            }
            else
            {
                dialogueCoroutine = StartCoroutine(DisplayDialogueButtonAsync(nextNode.id));
                CheckAndClearButtons();
            }

        }

        if (verbose)
            Debug.Log("Next nodes number : " + nextNodesList.Count);

        if (verbose)
        {
            for (int k = 0; k <= nextNodesList.Count - 1; k++)
            {
                Debug.Log(TAG + "next node id : " + nextNodesList[k].id + ", next node text : " + nextNodesList[k].dialogueText);

            }
        }
    }

   public void CancelDialogue()
    {
        endReached = true;
        StopDialogueCoroutine();
        CheckAndClearButtons();
        uiManager.ClearSubtitles();
        soundManager.StopDoppelgangerAudio();
        onLastNodeReached?.Invoke();
        onTalkFinished?.Invoke();
    }

    void UpdateIndex(int newIndex)
    {
        if (dialogueNodes[index].nextNodes.Count == 0) // if there is no next node, which means it's the last of the scenario
        {
            if (verbose)
                Debug.Log("end reached");

            endReached = true;
            CheckAndClearButtons();
            onLastNodeReached?.Invoke();
            return;
        }
        else
        {
            index = newIndex;
        }
    }

    void StopDialogueCoroutine()
    {
        if (dialogueCoroutine != null)
        {
            StopCoroutine(dialogueCoroutine);

            if (verbose)
                Debug.Log("Stop coroutine");
        }
    }
    void PlayAudioClip()
    {
        if (dialogueNodes[index].clip == null)
            return;

        onTalk?.Invoke();
        audioSource.clip = dialogueNodes[index].clip;
        audioSource.Play();
        Invoke(nameof(TriggerAudioFinished), audioSource.clip.length); 

    }

    private void NextDialogueButton()
    {
        inCouroutine = false;
        StopDialogueCoroutine();
        CancelInvoke(nameof(TriggerAudioFinished));
        onTalkFinished?.Invoke();
        onSkipDialogueNode?.Invoke();
        dialogueBtn.SetActive(true);
        dialogueButtonText = dialogueBtn.GetComponentInChildren<TextMeshProUGUI>();
        dialogueButtonText.text = dialogueNodes[index].dialogueText;
    }

    IEnumerator DisplayDialogueButtonAsync(int newIndex)
    {
        inCouroutine = true;
        yield return new WaitForSeconds(dialogueNodes[index].clip.length);

        if (inCouroutine)
        {
            UpdateIndex(newIndex);
            dialogueBtn.SetActive(true);
            dialogueButtonText = dialogueBtn.GetComponentInChildren<TextMeshProUGUI>();
            dialogueButtonText.text = dialogueNodes[index].dialogueText;
        }
        else
        {
            Debug.Log("Is not in coroutine");
            yield return null;
           
        }
        inCouroutine = false;
    }

    private void TriggerAudioFinished()
    {
        // Invoke the event to notify listeners
        onTalkFinished?.Invoke();
    }

    void DisplayDialogueText()
    {
        text = dialogueNodes[index].dialogueText;
        dialogueContent = dialogueNodes[index].dialogueContent;
        onTextChnaged?.Invoke();
    }

    Node GetNodeById(int id)
    {
        Node nodeObject = null;

        foreach(Node node in dialogueNodes)
        {
            if (node.id == id)
                nodeObject = node;
        }

        return nodeObject;
    }

    Node GetNextNode(int nodeID)
    {
        Node nextNode = null;

        foreach (Node node in dialogueNodes)
        {
            if (node.id == nodeID)
                nextNode = node;
            else
                nextNode = null;
        }

        return nextNode;
    }

    List<Node> GetNextNodes(int nodeID)
    {
        List<Node> nextNodes = new List<Node>();

        foreach (Node node in dialogueNodes)
        {
            if (node.id == nodeID)
                nextNodes.Add(node);
        }

        return nextNodes;
    }

    void CheckAndClearButtons()
    {
        if (dialogueBtn.activeSelf)
        {
            dialogueBtn.SetActive(false); 
        }
    }

    bool ButtonOnScene()
    {
        NodeButton button = FindObjectOfType<NodeButton>();
        return button != null;
    }

    public string GetDialogueText()
    {
        return dialogueContent;
    }

}
