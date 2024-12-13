using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Scaler : MonoBehaviour
{
    [SerializeField] float duration = 1f;
    Vector3 refScale;


    private void Awake()
    {
        refScale = transform.localScale;
        transform.DOScale(Vector3.one,duration);
    }

    private void OnDestroy()
    {
        transform.DOScale(refScale, duration);
    }
}
