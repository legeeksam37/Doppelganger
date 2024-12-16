using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Scaler : MonoBehaviour
{
    [SerializeField] float duration = 1f;
    [SerializeField] float enableSscale = 1f;
    [SerializeField] float diableScale = 0.5f;

    private void OnEnable()
    {
        transform.DOScale(enableSscale, duration);
    }

    private void OnDisable()
    {
        transform.localScale = new Vector3(diableScale, diableScale, diableScale);
    }
}
