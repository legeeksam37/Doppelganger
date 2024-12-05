using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class RemoteVideoControl : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] SoundManager soundManager;
    [SerializeField] UIManager uiManager;
    [SerializeField] AvatarManager avatar;
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
        soundManager.onMuted += MuteVideoSound;
        avatar.onTargetReached += PlayVideo;
    }

    private void OnDisable()
    {
        soundManager.onMuted -= MuteVideoSound;
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

            if (verbose)
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

    public void MuteVideoSound()
    {
        bool isMuted = videoPlayer.GetDirectAudioMute(0);
        videoPlayer.SetDirectAudioMute(0, !isMuted);
    }

    public void OnClickMuteVideoSound()
    {
        bool isMuted = videoPlayer.GetDirectAudioMute(0);
        videoPlayer.SetDirectAudioMute(0, !isMuted);
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
            uiManager.UpdateIndicator(index, "next");
            return;
        }
        else
        {
            nextVideo = true;
            index++;
            PlayVideo();
            DisplayText();
            nextVideo = false;
            uiManager.UpdateIndicator(index, "next");

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
            uiManager.UpdateIndicator(index, "prev");
            nextVideo = false;
        }

    }

    public int GetCurrentVideoIndex()
    {
        return index;
    }

    public int GetNbreOfVideos()
    {
        return names.Count;
    }
    void DisplayText()
    {
        descTextUI.text = desc[index];
    }
}
