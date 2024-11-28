using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{
    [SerializeField] UIManager uiManager;

    public Action onMuted;
    public void OnClickMute()
    {

        if (AudioListener.volume > 0)
        {
            AudioListener.volume = 0f;
            uiManager.IsButtonMuted(true);
        }
        else
        {
            AudioListener.volume = 1f;
            uiManager.IsButtonMuted(false);
        }

        onMuted?.Invoke();
    }

}
