using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Android;

public class TestFunction : MonoBehaviour
{

    Graphic graphicElement;
    bool faded = false;

    void Start()
    {
        graphicElement = GetComponent<Graphic>();
    }

    public void DownloadFile()
    {
        Application.OpenURL(Application.streamingAssetsPath + "/files/CV_Sami_YAHYA_BEY.pdf");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!faded)
            {
                graphicElement.DOFade(0, 0.5f);
                faded = true;
            }
            else
            {
                graphicElement.DOFade(1, 0.5f);
                faded= false;
            }
                
        }
    }

}
