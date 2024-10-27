using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public List<Node> dialogueNodes = new List<Node>();
    List<Node> nextNodesList = new List<Node>();

    void Start()
    {
        Node nextNode = null;

        Node firstNode = dialogueNodes[0];
  
        Debug.Log("Id : " + dialogueNodes[0].id + ", Text : " + dialogueNodes[0].dialogueText);

        for (int i = 0; i <= dialogueNodes[0].nextNodes.Count - 1; i++)
        {
            // Debug.Log("next node : " + dialogueNodes[0].nextNodes[i]);
            nextNode = GetNodeById(dialogueNodes[0].nextNodes[i]);
            nextNodesList.Add(nextNode);
        }

        Debug.Log("Next nodes number : "+nextNodesList.Count);

        //foreach (Node nextNodesObject in nextNodesList)
        //{
        //    Debug.Log("next node id : "+nextNodesObject.id+", next node text : "+ nextNodesObject.dialogueText);
        //}

        for (int i = 0; i <= nextNodesList.Count - 1; i++)
        {
            Debug.Log("next node id : " + nextNodesList[i].id + ", next node text : " + nextNodesList[i].dialogueText);
        }

        //foreach (Node node in dialogueNodes)
        //{
        //    Debug.Log("Id : "+node.id+", Text : "+node.dialogueText);
        //    for (int i = 0; i <= node.nextNodes.Count-1; i++)
        //    {
        //        Debug.Log("next node : " + node.nextNodes[i]);
        //        nextNode = GetNextNode(node.nextNodes[i]);
        //    }
        //}
        // Debug.Log("Next Node Id : " + nextNode.id + ", Text : " + nextNode.dialogueText);
    }


    Node GetNodeById(int id)
    {
        Node nodeObject = null;

        foreach(Node node in dialogueNodes)
        {
            if (node.id == id)
                nodeObject = node;
            else
                Debug.Log("Node didn't found by id");
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
