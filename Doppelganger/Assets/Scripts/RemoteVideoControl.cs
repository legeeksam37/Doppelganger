using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class RemoteVideoControl : MonoBehaviour
{
    [SerializeField] List<MediaContent> mediaContents;

    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] SoundManager soundManager;
    [SerializeField] UIManager uiManager;
    [SerializeField] AvatarManager avatar;
    [SerializeField] TextMeshProUGUI titleTextUI;
    [SerializeField] TextMeshProUGUI descTextUI;
    [SerializeField] bool verbose;

    public Action onVideoPlayed;
    bool firstPlay = true;
    bool nextVideo = false;
    int index = 0;
    const string TAG = "RemoteVideoControl";

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
            for (int i = 0; i < mediaContents.Count; i++)
            {
                Debug.Log("Media content id :" + mediaContents[i].id);
                Debug.Log("Media content title :" + mediaContents[i].title);
                Debug.Log("Media content description :" + mediaContents[i].description);
                Debug.Log("Media content video url :" + mediaContents[i].videoName);
            }
        }

    }

    void PlayFromUrl()
    {
        string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, mediaContents[index].videoName);

        if (verbose)
            Debug.Log(videoPath);

        videoPlayer.url = videoPath;
        videoPlayer.Play();
        DisplayText();
    }

    public void PlayVideo()
    {
        if (firstPlay)
        {
            PlayFromUrl();
            firstPlay = false;
            onVideoPlayed?.Invoke();
        }
        else if (nextVideo)
        {
            PlayFromUrl();
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
        

        if (index >= mediaContents.Count - 1)
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
        return mediaContents.Count;
    }
    void DisplayText()
    {
        titleTextUI.text = mediaContents[index].title;
        descTextUI.text = mediaContents[index].description;
    }
}
