using UnityEngine;
using System.Runtime.InteropServices;

public class WebGLOpenLinks : MonoBehaviour
{

    public static void OpenURL()
    {
        #if UNITY_WEBGL && !UNITY_EDITOR
            OpenTab("https://fr.linkedin.com/in/sami-yayb");
        #else
            Debug.Log("WebGL only");
        #endif
    }

    [DllImport("__Internal")]
    private static extern void OpenTab(string url);
   
}
