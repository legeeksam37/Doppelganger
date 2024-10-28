using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] bool verbose;
    public List<Node> dialogueNodes = new List<Node>();
    const string TAG = "DIALOGUE MANAGER ";
    int i;

    void Start()
    {
        i = 0;
        RunDialogueNodes();
    }

    void RunDialogueNodes()
    {
        List<Node> nextNodesList = new List<Node>();
        if (verbose)
            Debug.Log("Index : " + i);

        if (i >= dialogueNodes.Count)
        {
            if (verbose)
                Debug.Log("All nodes are done");

            return;
        }

        Node nextNode = null;

        Node firstNode = dialogueNodes[i];

        Debug.Log(TAG+"Current node Id : " + dialogueNodes[i].id + ", Text : " + dialogueNodes[i].dialogueText);

        for (int j = 0; j <= dialogueNodes[i].nextNodes.Count - 1; j++)
        {
            // Debug.Log("next node : " + dialogueNodes[0].nextNodes[i]);
            nextNode = GetNodeById(dialogueNodes[i].nextNodes[j]);
            nextNodesList.Add(nextNode);
        }

        if (verbose)
            Debug.Log("Next nodes number : " + nextNodesList.Count);

        for (int k = 0; k <= nextNodesList.Count - 1; k++)
        {
            Debug.Log(TAG+"next node id : " + nextNodesList[k].id + ", next node text : " + nextNodesList[k].dialogueText);
        }
        i++;
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


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RunDialogueNodes();
        }
    }
}
