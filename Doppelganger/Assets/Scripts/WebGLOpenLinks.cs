using UnityEngine;
using System.Runtime.InteropServices;

public class WebGLOpenLinks : MonoBehaviour
{

    public static void OpenURL()
    {
        #if UNITY_WEBGL && !UNITY_EDITOR
            OpenTab("https://www.linkedin.com");
        #else
            Debug.Log("WebGL only: Opening https://www.linkedin.com");
        #endif
    }

    [DllImport("__Internal")]
    private static extern void OpenTab(string url);
   
}
