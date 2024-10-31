using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class RemoteVideoControl : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] string videoName;

    //[SerializeField] List<VideoPlayer> videos;
    [SerializeField] List<string> names; 

    bool firstPlay = true;
    public bool singleVideo;

    int index = 0;

    private void Start()
    {
        for (int i = 0; i <= names.Count -1; i++)
        {
            Debug.Log(i + ": " + names[i]);
        }
    }

    public void PlayVideo()
    {
        if (firstPlay)
        {
            string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, names[index]);
            Debug.Log(videoPath);
            videoPlayer.url = videoPath;
            videoPlayer.Play();
            firstPlay = false;
        }
        else
        {
            videoPlayer.Play();

        }
    }

    public void Play()
    {
   
        if (firstPlay)
        {
            string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, videoName);
            Debug.Log(videoPath);

            videoPlayer.url = videoPath;
            videoPlayer.Play();
        
        }
        else
        {
            videoPlayer.Play();
  
        }
    }

    public void Stop()
    {
        videoPlayer.Pause();
    }

    public void Reset()
    {
        videoPlayer.Stop();
    }

    public void NextVideo()
    {
        Debug.Log("Index : " + index);
        if (index >= names.Count-1)
        {
            index = 0;
            PlayVideo();
            return;
        }
        else
        {
            Debug.Log("Next video : ");
            index++;
            PlayVideo();
        }
 
    }

    public void PreviousVideo()
    {
        Debug.Log("Index : " + index);
        if (index <= 0)
        {
            return;
        }
        else
        {
            index--;
            PlayVideo();
        }

    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.N))
        {
            Debug.Log("Next");
            NextVideo();
        }
    }
}
