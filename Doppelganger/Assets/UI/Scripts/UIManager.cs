using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] DialogueManager d_manager;
    [SerializeField] GameObject doppelgangerText;
    [SerializeField] GameObject remote;
    [SerializeField] GameObject videoDesc;
    [SerializeField] RemoteVideoControl remoteControl;

    void OnEnable()
    {
        d_manager.onTextChnaged += SetText;
        remoteControl.onVideoPlayed += UpdateUI;

    }

    void OnDisable()
    {
        d_manager.onTextChnaged -= SetText;
        remoteControl.onVideoPlayed -= UpdateUI;
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

    void UpdateUI()
    {
        remote.SetActive(true);
        doppelgangerText.SetActive(false);
        videoDesc.SetActive(true);
    }
}
