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


    public void Play()
    {
   
        if (firstPlay)
        {
            if (singleVideo)
            {
                string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, videoName);
                Debug.Log(videoPath);

                videoPlayer.url = videoPath;
                videoPlayer.Play();
                firstPlay = false;
            }
            else
            {

                string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, names[index]);
                Debug.Log(videoPath);
                videoPlayer.url = videoPath;
                videoPlayer.Play();
                firstPlay = false;

            }
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

        if (index >= names.Count)
        {
            return;
        }
        else
        {
           index++;
            Play();
        }
 
    }

    public void PreviousVideo()
    {
        if (index <= 0)
        {
            return;
        }
        else
        {
            index--;
            Play();
        }

    }
}
