using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class RemoteVideoControl : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] AvatarMovement avatar;
    [SerializeField] string videoName;

    //[SerializeField] List<VideoPlayer> videos;
    [SerializeField] List<string> names; 

    bool firstPlay = true;
    bool nextVideo = false;
    int index = 0;
    const string TAG = "RemoteVideoControl";

    public bool singleVideo;

    private void OnEnable()
    {
        avatar.onTargetReached += PlayVideo;
    }

    private void OnDisable()
    {
        avatar.onTargetReached -= PlayVideo;
    }

    private void Start()
    {
        for (int i = 0; i <= names.Count -1; i++)
        {
            Debug.Log(i + ": " + names[i]);
        }
    }

    public void PlayVideo()
    {
        if (firstPlay || nextVideo)
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
        Debug.Log(TAG + ", play");

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
            nextVideo = true;
            PlayVideo();
            nextVideo = false;
            return;
        }
        else
        {
            Debug.Log("Next video : ");
            nextVideo = true;
            index++;
            PlayVideo();
            nextVideo = false;
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
            nextVideo = true;
            PlayVideo();
            nextVideo = false;
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
