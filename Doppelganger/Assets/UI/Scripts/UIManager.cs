using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.Device;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] TextMeshProUGUI doppelgangerHeadTextUI;
    [SerializeField] TextMeshProUGUI tVScreenLabelTextUI;
    [SerializeField] DialogueManager d_manager;
    [SerializeField] Sprite muteSprite;
    [SerializeField] Sprite unMuteSprite;
    [SerializeField] Button muteButton;
    [SerializeField] GameObject dialogueElements;
    [SerializeField] TextMeshProUGUI doppelgangerText;
    [SerializeField] GameObject remote;
    [SerializeField] GameObject videoDesc;
    [SerializeField] RemoteVideoControl remoteControl;
    [SerializeField] GameObject carouselIndicatorPrefab;
    [SerializeField] Transform carouselIndicatorParent;
    [SerializeField] TVScreen tVScreen;

    [SerializeField]  List<GameObject> carouselIndicatorsLst = new List<GameObject>();
    [SerializeField]  List<Graphic> carouselIndicatorGraphE = new List<Graphic>();

    int nbreOfVideos;

    void OnEnable()
    {
        d_manager.onTextChnaged += SetText;
        d_manager.onLastNodeReached += HideDialogueUI;
        tVScreen.onClickZoomIn += HideDialogueUI;
        tVScreen.onClickZoomOut += ShowDialogueUI;
        remoteControl.onVideoPlayed += UpdateUI;

    }

    void OnDisable()
    {
        d_manager.onTextChnaged -= SetText;
        d_manager.onLastNodeReached -= HideDialogueUI;
        tVScreen.onClickZoomIn -= HideDialogueUI;
        tVScreen.onClickZoomOut -= ShowDialogueUI;
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

    public void HideDialogueUI()
    {
        dialogueElements.SetActive(false);
    }

    public void ShowDialogueUI()
    {
        if (d_manager.IsInDialogue())
            dialogueElements.SetActive(true);
        else
            Debug.Log("Display Cv and linkedin");
    }

    public void ClearSubtitles()
    {
        doppelgangerText.text = string.Empty;
    }

    public void SetDoppelGangerHeadText(string headText)
    {
        doppelgangerHeadTextUI.text = headText;
    }

    public void SetTVScreenLabelText(string labelText)
    {
        tVScreenLabelTextUI.text = labelText;
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

    public void IsButtonMuted(bool state)
    {
        if (state)
            muteButton.image.sprite = muteSprite;
        else
            muteButton.image.sprite = unMuteSprite;
    }
}
