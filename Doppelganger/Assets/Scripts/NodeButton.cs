using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NodeButton : MonoBehaviour
{
    Button button;
    DialogueManager d_manager;
    const string TAG = "NodeButton";
    // Start is called before the first frame update
    void Start()
    {
        d_manager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();

        if (d_manager == null)
            Debug.LogError(TAG+" Dialogue manager not found");

        button = GetComponent<Button>();

        if (button != null)
        {
            button.onClick.AddListener(OnButtonClicked);
        }
        else
        {
            Debug.LogError(TAG+" Button componenent not found");
        }

    }

    // Update is called once per frame
    void OnButtonClicked()
    {
        Debug.Log("Button is clicked !! ");
        d_manager.RunDialogueNodes();
    }
}
