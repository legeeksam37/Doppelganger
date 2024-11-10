using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NodeButton : MonoBehaviour
{
    Button button;
    const string TAG = "NodeButton";
    // Start is called before the first frame update
    void Start()
    {
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
    }
}
