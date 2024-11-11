using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] DialogueManager d_manager;
    [SerializeField] GameObject remote;
    [SerializeField] GameObject videoDesc;
    [SerializeField] RemoteVideoControl remoteControl;

    void OnEnable()
    {
        d_manager.onTextChnaged += SetText;
        remoteControl.onVideoPlayed += ShowRemote;
        remoteControl.onVideoPlayed += ShowVideoDesc;

    }

    void OnDisable()
    {
        d_manager.onTextChnaged -= SetText;
        remoteControl.onVideoPlayed -= ShowRemote;
        remoteControl.onVideoPlayed -= ShowVideoDesc;
    }

    void Start()
    {
        SetText();

    }

    void Update()
    {
        
    }

    void SetText()
    {
        text.text = d_manager.GetDialogueText();
        Debug.Log("Action called");
    }

    void ShowRemote()
    {
        remote.SetActive(true);
    }

    void ShowVideoDesc()
    {
        videoDesc.SetActive(true);
    }
}
