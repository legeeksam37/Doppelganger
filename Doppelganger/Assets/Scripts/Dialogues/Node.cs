using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Node", menuName = "Dialogue/Node")]
public class Node : ScriptableObject
{
    public int id;
    public string dialogueText;  // The dialogue text for this node
    public string dialogueContent;
    public AudioClip clip;  // The actual audio file of the text
    public List<int> nextNodes = new List<int>(); //the ids of the next nodes
    public bool hasInteraction;
    public bool endNode;
    
}