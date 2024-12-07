using UnityEngine;

[CreateAssetMenu(fileName = "New Media Content", menuName = "Media/MediaContent")]
public class MediaContent : ScriptableObject
{
    public int id;
    public string title;  
    public string description;
    public string videoName;
}