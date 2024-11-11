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
        remoteControl.onVideoPlayed += DoRotation;
    }

    private void OnDisable()
    {
        remoteControl.onVideoPlayed -= DoRotation;
    }


    private void Update()
    {

    }

    void DoRotation()
    {
        // DORotate(new Vector3(0f, 190f, 0f), 5f);
        transform.DORotate(new Vector3(0f, 190f, 0f), 2f);
    }
}
