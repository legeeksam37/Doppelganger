using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] DialogueManager d_manager;
    [SerializeField] Sprite muteSprite;
    [SerializeField] Sprite unMuteSprite;
    [SerializeField] Button muteButton;
    [SerializeField] GameObject doppelgangerTextCanvas;
    [SerializeField] TextMeshProUGUI doppelgangerText;
    [SerializeField] GameObject remote;
    [SerializeField] GameObject videoDesc;
    [SerializeField] RemoteVideoControl remoteControl;
    [SerializeField] GameObject carouselIndicatorPrefab;
    [SerializeField] Transform carouselIndicatorParent;

    int nbreOfVideos;
    [SerializeField]  List<GameObject> carouselIndicatorsLst = new List<GameObject>();
    [SerializeField]  List<Graphic> carouselIndicatorGraphE = new List<Graphic>();

    void OnEnable()
    {
        d_manager.onTextChnaged += SetText;
        d_manager.onLastNodeReached += HideSubTextUI;
        remoteControl.onVideoPlayed += UpdateUI;

    }

    void OnDisable()
    {
        d_manager.onTextChnaged -= SetText;
        d_manager.onLastNodeReached += HideSubTextUI;
        remoteControl.onVideoPlayed -= UpdateUI;
    }

    void Start()
    {
        SetText();
        nbreOfVideos = remoteControl.GetNbreOfVideos();
        InstanciateCarouselIndicator();
        carouselIndicatorGraphE[0].DOFade(1, 0.5f);
    }
    void SetText()
    {
        text.text = d_manager.GetDialogueText();
    }

    void UpdateUI()
    {
        remote.SetActive(true);
        videoDesc.SetActive(true);
    }

    public void HideSubTextUI()
    {
        doppelgangerTextCanvas.SetActive(false);
    }

    public void ShowSubTitles()
    {
        doppelgangerTextCanvas.SetActive(true);
    }

    public void ClearSubtitles()
    {
        doppelgangerText.text = string.Empty;
    }

    void InstanciateCarouselIndicator()
    {
        GameObject carouselIndicator;
        GameObject indicatorImage;
        Graphic graphicElement;

        for (int i = 0; i < nbreOfVideos; i++)
        {
            carouselIndicator =  Instantiate(carouselIndicatorPrefab, carouselIndicatorParent);
            indicatorImage = carouselIndicator.transform.GetChild(0).gameObject;
            graphicElement = indicatorImage.GetComponent<Graphic>();
            carouselIndicatorsLst.Add(carouselIndicator);
            carouselIndicatorGraphE.Add(graphicElement);
        }

    }

    public void UpdateIndicator(int index, string position)
    {
        if (position == "next")
        {
            if (index == 0)
            {
                carouselIndicatorGraphE[nbreOfVideos-1].DOFade(0, 0.5f);
                carouselIndicatorGraphE[index].DOFade(1, 0.5f);
            }
            else
            {
                carouselIndicatorGraphE[index - 1].DOFade(0, 0.5f);
                carouselIndicatorGraphE[index].DOFade(1, 0.5f);
            }

        } else if (position == "prev")
        {
            carouselIndicatorGraphE[index + 1].DOFade(0, 0.5f);
            carouselIndicatorGraphE[index].DOFade(1, 0.5f);
        }
    }

    void ResetIndicators()
    {
        for (int i = 0; i < carouselIndicatorsLst.Count; i++)
        {

        }
    }

    public void IsButtonMuted(bool state)
    {
        if (state)
            muteButton.image.sprite = muteSprite;
        else
            muteButton.image.sprite = unMuteSprite;
    }
}
