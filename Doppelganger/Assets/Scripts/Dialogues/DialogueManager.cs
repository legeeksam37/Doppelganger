using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] bool verbose;
    [SerializeField] UIManager uiManager;
    [SerializeField] AudioSource audioSource;
    [SerializeField] GameObject canvasParent;
    [SerializeField] GameObject buttonDialoguePrefab;
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
    List<Node> interactionsDialogueNodes = new List<Node>();
    

    void Start()
    {
        if (avatar == null)
        {
            Debug.LogError("Avatar is null");
            return;
        }

        index = 0;
        avatar.Wave();
        //RunDialogueNodes();
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
        GameObject dialogueBtn = Instantiate(buttonDialoguePrefab, canvasParent.transform);
        dialogueButtonText = dialogueBtn.GetComponentInChildren<TextMeshProUGUI>();
        dialogueButtonText.text = dialogueNodes[0].dialogueText;
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
        if (!endReached)
        {
            List<Node> nextNodesList = new List<Node>();
            if (verbose)
                Debug.Log("Index : " + index);

            if (index >= dialogueNodes.Count)
            {
                if (verbose)
                    Debug.Log("All nodes are done");

                return;
            }

            Node nextNode = null;

            Node firstNode = dialogueNodes[index];

            if (verbose)
                Debug.Log("Can interract : " + dialogueNodes[index].hasInteraction);

            if (dialogueNodes[index].hasInteraction)
            {
                avatar.Move();
                dialogueNodes[index].hasInteraction = false; // set the interaction to false so it doesn't restart it at the next dialogue 
                interactionsDialogueNodes.Add(dialogueNodes[index]); // add it to the interactions list
            }

            if (verbose)
                Debug.Log(TAG + "Current node Id : " + dialogueNodes[index].id + ", Text : " + dialogueNodes[index].dialogueText);

            PlayAudioClip();

            DisplayDialogueText();

            for (int j = 0; j <= dialogueNodes[index].nextNodes.Count - 1; j++)
            {
                if (verbose)
                    Debug.Log("next node : " + dialogueNodes[0].nextNodes[index]);

                if (name == "skip")
                {
                    CheckAndClearButtons();
                    DisplayDialogueButton();
                    uiManager.ClearSubtitles();
                    soundManager.StopDoppelgangerAudio();

                }
                else
                {
                    StartCoroutine(DisplayDialogueButtonAsync());
                    CheckAndClearButtons();
                }

                nextNode = GetNodeById(dialogueNodes[index].nextNodes[j]);
                nextNodesList.Add(nextNode);
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
                index = nextNode.id;
            }
        }      
    }

    private void DisplayDialogueButton()
    {
        inCouroutine = false;
        onSkipDialogueNode?.Invoke();
        GameObject dialogueBtn = Instantiate(buttonDialoguePrefab, canvasParent.transform);
        dialogueButtonText = dialogueBtn.GetComponentInChildren<TextMeshProUGUI>();
        //dialogueButtonText.text = dialogueNodes[index].dialogueText;

        if (index > 0)
            dialogueButtonText.text = dialogueNodes[index +1].dialogueText;
        else
            dialogueButtonText.text = dialogueNodes[index].dialogueText;
    }

    public void test()
    {
        Debug.Log("Button prefab called");
    }

    void InstanciateDialogueButton()
    {

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

    IEnumerator DisplayDialogueButtonAsync()
    {
        inCouroutine = true;
        yield return new WaitForSeconds(dialogueNodes[index].clip.length);

        if (inCouroutine)
        {
            Debug.Log(TAG + " audio has finished ! ");
            GameObject dialogueBtn = Instantiate(buttonDialoguePrefab, canvasParent.transform);
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
        NodeButton[] allButtons = FindObjectsOfType<NodeButton>();

        foreach (NodeButton button in allButtons)
        {
            Destroy(button.gameObject);
        }
    }

    public string GetDialogueText()
    {
        return dialogueContent;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RunDialogueNodes("skip");
        }
    }

}
