using UnityEngine;
using UnityEngine.Video;

public class RemoteVideoControl : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] string videoName;
    bool firstPlay = true;


    public void Play()
    {
        if (firstPlay)
        {
            string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, videoName);
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
        
    }

    public void PreviousVideo()
    { 
    
    }
}
