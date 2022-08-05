using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
public class DifferentVersionCinfirmDialogBehaviour : MonoBehaviour
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
    

    [HideInInspector]
    public bool IsActive = false;

    Action<bool> tempCallBack;

    //singleton instance--------------
    public static DifferentVersionCinfirmDialogBehaviour Instance;
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

    //above things are same as old version
   // public Action<bool> callback;
    //now what we do is: instead of using method chaining we will get all data at once in create method of generic menu
    public void CreateMenu(Action NegativeButtonClickAction,Action PositiveButtonClickedAction, Action OncloseButtonClicked, string title, string message
        , Color btnColor,
        string positiveButtonText, string negativeButtonText, float fadeDuration = 0.15f )
    {
        dialog.Title = title;        
        dialog.Message = message;
        dialog.CloseButtonClickAction = OncloseButtonClicked;
        dialog.PositiveButtonClickAction= PositiveButtonClickedAction;
        dialog.NegativeButtonClickAction = NegativeButtonClickAction;
        dialog.CloseButtonClickAction = OncloseButtonClicked;
        //dialog.HasButtons = buttonVisibility;
        dialog.PositiveButtonText = positiveButtonText;
        dialog.NegativeButtonText = negativeButtonText;
        dialog.FadeDuration = fadeDuration;
        dialog.buttonColor = btnColor;

        Show();
    }

    public void CreateMenuWithCallback(string title, string msg, bool isAllowCloseFromOutside = false, float autoClose = 2f, Action<bool> callback= null, string leftBtn="", string rightBtn="")
    {
        tempCallBack = callback;

        uititleText.text = title;
        uiMessageText.text = msg;
        uiPositiveButtonText.text = rightBtn;

        uiNegativeButtonText.text = leftBtn;

        uiCanvas.SetActive(true);
        StartCoroutine(FadeIn(.12f));

    }



    public void CreateMenuWithCallBackNew(string title, string message, Color buttonColor, Action<bool> callback = null, string positiveButtonText="", string negativeButtonText="",float fadeDuration = 0.12f)
    {
        tempCallBack = callback;

        uititleText.text = title;
        uiMessageText.text = message;


        if(string.IsNullOrEmpty(negativeButtonText))
        {
            uiNegativeButton.gameObject.SetActive(false);
        }
        else
        {
            uiNegativeButtonText.text = negativeButtonText;
        }


        if(string.IsNullOrEmpty(positiveButtonText))
        {
            uiPositiveButton.gameObject.SetActive(false);
        }
        else
        {
            uiPositiveButtonText.text = positiveButtonText;
        }
        

        Color colorForNegativeButton = buttonColor;
        colorForNegativeButton.a = .2f;

        uiNegativeButtonImage.color = colorForNegativeButton;

        uiPositiveButtonImage.color = buttonColor;


        IsActive = true;
        uiCanvas.SetActive(true);
       

        StartCoroutine(FadeIn(fadeDuration));
    }

    public void PositiveButtonClicked()
    {
        tempCallBack(true);
    }
    public void NegativeButtonClicked()
    {
        tempCallBack(false);
    }
    public void CloseButtonClick()
    {
        StartCoroutine(FadeOut(.12f));
    }


    public void CreateMenuWithoutActions(string title, string message
        , Color btnColor,
        string positiveButtonText, string negativeButtonText, float fadeDuration = 0.15f)
    {
        Debug.Log("succes 1");
        dialog.Title = title;
        Debug.Log("succes 1");
        dialog.Message = message;
        Debug.Log("succes 2");
        //dialog.HasButtons = buttonVisibility;
        dialog.PositiveButtonText = positiveButtonText;
        Debug.Log("succes 3");
        dialog.NegativeButtonText = negativeButtonText;
        dialog.FadeDuration = fadeDuration;
        dialog.buttonColor = btnColor;

        Debug.Log("succes 1");
        Show();
    }



    public void Show()
    {
        Debug.Log("ShowCalled");
        dialogsQueue.Enqueue(dialog);
        ResetDialog();

        if (!IsActive)
            FillDialogAndShow();
    }


    void FillDialogAndShow()
    {
        tempDialog = dialogsQueue.Dequeue();

        //uiPositiveButton.onClick.RemoveAllListeners();
        //uiPositiveButton.onClick.AddListener(delegate { tempDialog.PositiveButtonClickAction(); });

        //uiPositiveButton.onClick.AddListener(() => tempDialog.PositiveButtonClickAction());

        //uiNegativeButton.onClick.RemoveAllListeners();
        //uiNegativeButton.onClick.AddListener(delegate { tempDialog.NegativeButtonClickAction(); });

        //uiCloseButton.onClick.RemoveAllListeners();
        //uiCloseButton.onClick.AddListener(delegate { tempDialog.CloseButtonClickAction(); Hide(); });

        if (string.IsNullOrEmpty(tempDialog.Message.Trim()))
        {
            Debug.LogError("[DialogUI] dialog's text can't be empty.... use<b>.SetMessage(...)</b>");
            return;
        }

        uititleText.text = tempDialog.Title;

        //trim text
        if (tempDialog.Message.Length > maxMessageLetters)
        {
            uiMessageText.text = tempDialog.Message.Substring(0, maxMessageLetters - 3) + "...";
        }
        else
        {
            uiMessageText.text = tempDialog.Message;
        }

        uiButtonsParent.SetActive(tempDialog.HasButtons);

        if (string.IsNullOrEmpty(tempDialog.NegativeButtonText))
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

        Color negativecolor = tempDialog.buttonColor;
        negativecolor.a = 0.12f;

        uiNegativeButtonImage.color = negativecolor;

        uiPositiveButtonImage.color = tempDialog.buttonColor;

        IsActive = true;
        uiCanvas.SetActive(true);
        StartCoroutine(FadeIn(tempDialog.FadeDuration));


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

        //Hide();
    }

    //-------------------------------------------------------
    IEnumerator Fade(CanvasGroup cGroup, float startAlpha, float endAlpha, float duration)
    {
        if (startAlpha == endAlpha)
        {
            Debug.LogError("Fade() : startAlpha must be different than endAlpha");
            yield break;
        }

        float startTime = Time.time;
        float alpha = startAlpha;

        if (duration > 0f)
        {
            //Anim start
            while (alpha != endAlpha)
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
        //Anim start
        yield return Fade(uiCanvasGroup, 0f, 1f, duration);

        //*******************
        //uiCloseButton.onClick.AddListener(() => InvokeEventAndHideDialog(() => { tempDialog.CloseButtonClickAction(); Hide(); }));
        //uiPositiveButton.onClick.AddListener(() => InvokeEventAndHideDialog(tempDialog.PositiveButtonClickAction));
        //uiNegativeButton.onClick.AddListener(() => InvokeEventAndHideDialog(tempDialog.NegativeButtonClickAction));
        //*****************


        //uiCloseButton.onClick.AddListener(delegate { Debug.Log("smit"); Debug.Log(1); Fade(uiCanvasGroup, 0f, 1f, duration); });

        //uiCloseButton.onClick.AddListener(() => { Debug.Log("paresh"); Debug.Log(1); });
    }

    IEnumerator FadeOut(float duration)
    {
        //Anim start
        yield return Fade(uiCanvasGroup, 1f, 0f, duration);

        if (dialogsQueue.Count != 0)
        {
            FillDialogAndShow();
        }
        else
        {
            uiCanvas.SetActive(false);
        }
    }
    //take boolean in the action, and then if the boolean is passed true 
}
