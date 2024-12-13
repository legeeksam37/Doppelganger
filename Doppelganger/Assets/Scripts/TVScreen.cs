using System;
using UnityEngine;

public class TVScreen : MonoBehaviour
{
    const string TAG = "TVScreen";

    [SerializeField] UIManager uiManager;
    [SerializeField] AvatarManager avatarManager;
    [SerializeField] CameraManager cameraManager;

    public Action onClickZoomIn;
    public Action onClickZoomOut;

    GameObject zoomTextLabel;
    Outline outlineEffect;
    bool isFocused;
    bool canClick;
    private void OnEnable()
    {
        zoomTextLabel = transform.GetChild(0).gameObject;

        if (zoomTextLabel == null)
            Debug.LogError(TAG + " No Head text at position 0 as child object !");

        outlineEffect = GetComponent<Outline>();
        outlineEffect.enabled = false;
    }

    private void OnMouseOver()
    {
        

        if (avatarManager.IsPresenting())
        {
            uiManager.SetTVScreenLabelText("Click to Zoom in/out");
            zoomTextLabel.SetActive(true);
            canClick = true;
            outlineEffect.enabled = true;
            
        }
    }

    private void OnMouseExit()
    {
        zoomTextLabel.SetActive(false);
        outlineEffect.enabled = false;
        canClick = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canClick && Input.GetMouseButtonDown(0))
        {
            if (!isFocused)
            {
                cameraManager.FocusOnTv();
                onClickZoomIn?.Invoke();
                isFocused = true;
            }
            else if (isFocused)
            {
                cameraManager.SetCameraPresentingPosition();
                onClickZoomOut?.Invoke();
                isFocused = false;
            }

        }
    }
}
