using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class RemoteVideoControl : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] AvatarManager avatar;
    [SerializeField] string videoName;
    [SerializeField] TextMeshProUGUI descTextUI;
    [SerializeField] bool verbose;
    [SerializeField] List<string> names;
    [SerializeField] List<string> desc;

    public Action onVideoPlayed;
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
        if (descTextUI == null)
            Debug.LogError("Text field is not serialized");

        if (verbose)
        {
            for (int i = 0; i <= names.Count - 1; i++)
            {
                Debug.Log(i + ": " + names[i]);
            }
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
            DisplayText();
            firstPlay = false;
            onVideoPlayed?.Invoke();
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
        if (index >= names.Count-1)
        {
            index = 0;
            nextVideo = true;
            PlayVideo();
            DisplayText();
            nextVideo = false;
            return;
        }
        else
        {
            nextVideo = true;
            index++;
            PlayVideo();
            DisplayText();
            nextVideo = false;
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
            nextVideo = true;
            PlayVideo();
            DisplayText();
            nextVideo = false;
        }

    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.N))
        {
            NextVideo();
        }
    }

    void DisplayText()
    {
        descTextUI.text = desc[index];
    }
}
