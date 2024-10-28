using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIDialogueManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] DialogueManager d_manager;

    void OnEnable()
    {
        d_manager.onTextChnaged += SetText;

    }

    void OnDisable()
    {
        d_manager.onTextChnaged -= SetText;
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
}
