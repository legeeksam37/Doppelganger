using System.Runtime.InteropServices;
using UnityEngine;

public class DeviceManager : MonoBehaviour
{
    string debugText;

    [DllImport("__Internal")]
    private static extern string GetBrowserPlatform();
    private void Start()
    {
        #if UNITY_WEBGL && !UNITY_EDITOR
        GetDevice();
        #endif
    }

    void GetDevice()
    {
        debugText = "Device " + GetBrowserPlatform();
        Debug.Log("plateform : "+GetBrowserPlatform());
    }

    /*void OnGUI()
    {
        GetDevice();
        GUIStyle style = new GUIStyle();
        style.fontSize = 20;
        style.normal.textColor = Color.black;
        GUI.Label(new Rect(10, 100, 500, 20), debugText, style);
    }*/
}
