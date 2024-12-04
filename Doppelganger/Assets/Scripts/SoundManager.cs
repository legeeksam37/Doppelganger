using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{
    [SerializeField] UIManager uiManager;
    [SerializeField] AudioSource doppelgangerAudioSource;

    public Action onMuted;

    bool isSoundMuted = false;
    public void OnClickMute()
    {

        if (AudioListener.volume > 0)
        {
            AudioListener.volume = 0f;
            isSoundMuted = true;
            uiManager.IsButtonMuted(true);
        }
        else
        {
            AudioListener.volume = 1f;
            isSoundMuted = false;
            uiManager.IsButtonMuted(false);
        }

       
    }

    public void StopDoppelgangerAudio()
    {
        doppelgangerAudioSource.Stop();
    }

    public bool IsSoundMuted()
    { 
        return isSoundMuted; 
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            StopDoppelgangerAudio();
        }
    }
}
