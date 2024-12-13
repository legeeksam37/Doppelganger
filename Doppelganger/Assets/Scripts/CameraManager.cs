using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraManager : MonoBehaviour
{
    [SerializeField] RemoteVideoControl remoteControl;
    Vector3 targetDirection = new Vector3(0,190,0);


    private void OnEnable()
    {
        remoteControl.onVideoPlayed += SetCameraPresentingPosition;
    }

    private void OnDisable()
    {
        remoteControl.onVideoPlayed -= SetCameraPresentingPosition;
    }


    private void Update()
    {

    }

    public void SetCameraPresentingPosition()
    {
        transform.DORotate(new Vector3(0f, 190f, 0f), 1f);
        transform.DOMove(new Vector3(4.4f, 1.7f, 4.3f), 1f);
    }

    public void FocusOnTv()
    {
        transform.DORotate(new Vector3(0f, 180f, 0f), 1f);
        transform.DOMove(new Vector3(4.3f, 2.1f, 1f), 1f);
    }
}
