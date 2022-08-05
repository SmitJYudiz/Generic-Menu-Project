using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ConfirmDialogUiBehaviour : MonoBehaviour
{
    [SerializeField] GameObject uiCanvas;
    [SerializeField] Button uiCloseButton;

    [SerializeField] TextMeshProUGUI uititleText;
    [SerializeField] TextMeshProUGUI uiMessageText;

    [SerializeField] GameObject uiButtonsParent;

    [SerializeField] Button uiPositiveButton;
    [SerializeField] Button uiNegativeButton;

    TextMeshProUGUI uiNegativeButtonText;
    TextMeshProUGUI uiPositiveButtonText;

    Image uiNegativeButtonImage;
    Image uiPositiveButtonImage;

    CanvasGroup uiCanvasGroup;

    //Dialog properties -------------------------------:
    //Default Values:
    [Space(20f)]
    [Header("Dialog's Defaults:")]

    [SerializeField] int maxMessageLetters = 300;
    [SerializeField] bool _defaultHasButtons = true;
    [SerializeField] string _defaultNegativeButtonText;
    [SerializeField] string _defaultPositiveButtonText;

    // [SerializeField] DialogButtonColor _defaultButtonColor = DialogButtonColor.Black;
    Queue<Dialog> dialogsQueue = new Queue<Dialog>();

    Dialog dialog, tempDialog;

    [Space(20f)]
    [SerializeField] Color[] dialogButtonColors;

    [HideInInspector]
    public bool IsActive = false;

    //singleton instance--------------
    public static ConfirmDialogUiBehaviour Instance;
    private void Awake()
    {
        Instance = this;

        //u nigga; it was only transferring tmpro from one place to another...
        uiNegativeButtonText = uiNegativeButton.GetComponentInChildren<TextMeshProUGUI>();
        uiPositiveButtonText = uiPositiveButton.GetComponentInChildren<TextMeshProUGUI>();

        uiNegativeButtonImage = uiNegativeButton.GetComponentInChildren<Image>();
        uiPositiveButtonImage = uiPositiveButton.GetComponentInChildren<Image>();

        uiCanvasGroup = uiCanvas.GetComponent<CanvasGroup>();

        ResetDialog();
    }

    //-------------------------------------------------------
    public ConfirmDialogUiBehaviour SetTitle(string title)
    {
        dialog.Title = title;
        return Instance;
    }

    public ConfirmDialogUiBehaviour SetMessage(string message)
    {
        dialog.Message = message;
        return Instance;
    }

    public ConfirmDialogUiBehaviour SetButtonVisibility(bool visibily = true)
    {
        dialog.HasButtons = visibily;
        return Instance;
    }

    public ConfirmDialogUiBehaviour SetButtonsColor(DialogButtonColor color = DialogButtonColor.Black)
    {
        //dialog.ButtonsColor = color;
        return Instance;
    }

    public ConfirmDialogUiBehaviour SetPositiveButtonText(string text)
    {
        dialog.PositiveButtonText = text;
        return Instance;
    }

    public ConfirmDialogUiBehaviour SetNegativeButtonText(string text)
    {
        dialog.NegativeButtonText = text;
        return Instance;
    }

    public ConfirmDialogUiBehaviour SetFadeduration(float duration = 0.15f)
    {
        dialog.FadeDuration = duration;
        return Instance;
    }

    public ConfirmDialogUiBehaviour OnCloseButtonClicked(Action action)
    {
        dialog.CloseButtonClickAction = action;
        return Instance;
    }

    public ConfirmDialogUiBehaviour OnPositiveButtonClicked(Action action)
    {
        dialog.PositiveButtonClickAction = action;
        return Instance;
    }

    public ConfirmDialogUiBehaviour OnNegativeButtonclicked(Action action)
    {
        dialog.NegativeButtonClickAction = action;
        return Instance;
    }

    //----------------------------------------------
    public void Show()
    {
        dialogsQueue.Enqueue(dialog);
        ResetDialog();

        if (!IsActive)
            FillDialogAndShow();
    }

    void FillDialogAndShow()
    {
        tempDialog = dialogsQueue.Dequeue();

        //data validation
        if (string.IsNullOrEmpty(tempDialog.Message.Trim()))
        {
            Debug.LogError("[DialogUI] dialog's text can't be empty.... use<b>.SetMessage(...)</b>");
            return;
        }

        uititleText.text = tempDialog.Title;

        //trim text
        if(tempDialog.Message.Length > maxMessageLetters)
        {
            uiMessageText.text = tempDialog.Message.Substring(0, maxMessageLetters - 3) + "...";
        }
        else
        {
            uiMessageText.text = tempDialog.Message;
        }

        uiButtonsParent.SetActive(tempDialog.HasButtons);

        //don't show buttons if the text on them is not given by method
        if(string.IsNullOrEmpty(tempDialog.NegativeButtonText))
        {
            //then don't show the negative button
            uiNegativeButton.gameObject.SetActive(false);
        }
        else
        {
            uiNegativeButtonText.text = tempDialog.NegativeButtonText;
        }

        //same for positive button
        if (string.IsNullOrEmpty(tempDialog.PositiveButtonText))
        {
            //then don't show the negative button
            uiPositiveButton.gameObject.SetActive(false);
        }
        else
        {
            uiPositiveButtonText.text = tempDialog.PositiveButtonText;
        }

      


        //Color transparentColor = dialogButtonColors [ ( int )tempDialog.ButtonsColor ];
        //above line giving error: fix that, below is only the temporary solution:
        //Color transparentColor = Color.magenta;
		//	transparentColor.a = .12f;
			//uiNegativeButtonImage.color = transparentColor;
        //uiPositiveButtonImage.color = dialogButtonColors [ ( int )tempDialog.ButtonsColor ];
        //above line giving error: fix that, below is only the temporary solution:
        //uiPositiveButtonImage.color = Color.magenta;


        IsActive = true;
        uiCanvas.SetActive(true);
        //StartCoroutine(FadeIn(tempDialog.FadeDuration));
    }

    public void Hide()
    {
        uiCloseButton.onClick.RemoveAllListeners();
        uiNegativeButton.onClick.RemoveAllListeners();
        uiPositiveButton.onClick.RemoveAllListeners();

        IsActive = false;
        StopAllCoroutines();
        StartCoroutine(FadeOut(tempDialog.FadeDuration));
    }

  

    void ResetDialog()
    {
        dialog = new Dialog();

        //dialog.FadeDuration =
        dialog.HasButtons = _defaultHasButtons;
        //dialog.ButtonsColor = _defaultButtonsColor;
        dialog.PositiveButtonText = _defaultPositiveButtonText;
        dialog.NegativeButtonText = _defaultNegativeButtonText;
    }

    //-------------------------------------------------------

    void InvokeEventAndHideDialog(Action action)
    {
        action?.Invoke();

        Hide();
    }

    //-------------------------------------------------------
    IEnumerator Fade(CanvasGroup cGroup, float startAlpha, float endAlpha, float duration)
    {
        if(startAlpha == endAlpha)
        {
            Debug.LogError("Fade() : startAlpha must be different than endAlpha");
            yield break;
        }

        float startTime = Time.time;
        float alpha = startAlpha;

        if(duration > 0f)
        {
            //Anim start
            while(alpha != endAlpha)
            {
                alpha = Mathf.Lerp(startAlpha, endAlpha, (Time.time - startTime) / duration);
                cGroup.alpha = alpha;
                yield return null;
            }
        }
        else
        {
            cGroup.alpha = endAlpha;
            
        }
        yield break;
    }

    IEnumerator FadeIn(float duration)
    {
        yield return Fade(uiCanvasGroup, 0f, 1f, duration);

        uiCloseButton.onClick.AddListener(delegate { Debug.Log("smit"); Debug.Log(1); Fade(uiCanvasGroup, 0f, 1f, duration); });

        uiCloseButton.onClick.AddListener( ()=> { Debug.Log("paresh"); Debug.Log(1); });
    }

    IEnumerator FadeOut(float duration)
    {
        //Anim start
        yield return Fade(uiCanvasGroup, 1f, 0f, duration);

        if(dialogsQueue.Count !=0)
        {
            FillDialogAndShow();
        }
        else
        {
            uiCanvas.SetActive(false);
        }
    }
}
