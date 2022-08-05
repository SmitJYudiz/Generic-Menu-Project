using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;
using System;

//this class will hold all dialog properties
public class Dialog
{
    public string Title;      //defaults given in case if they are not set
    public string Message;
    

    public bool HasButtons;
    public string PositiveButtonText;
    public string NegativeButtonText;

    //public DialogButtonColor ButtonsColor;

    public Color buttonColor;

    public float FadeDuration;

    public Action CloseButtonClickAction = null;
    public Action NegativeButtonClickAction = null;
    public Action PositiveButtonClickAction = null;
     
}



public enum DialogButtonColor
{  
    Black,
    Purple,
    Magenta,
    Blue,
    Green,
    Yellow,
    Orange,
    Red,
    Gray
}


//old dialog behaviour script:
/*public class DialogUIBehaviour : MonoBehaviour
{
    //this knowledge is delivered from Hamza hermou (youtube)
    [SerializeField]
    TextMeshProUGUI titleUIText;

    [SerializeField]
    TextMeshProUGUI messageUIText;

    [SerializeField]
    Button closeUIButton;

    [SerializeField] Canvas canvas;

    //we want one instance of the class DialogUIBehaviour, so we will use singleton
    public static DialogUIBehaviour Instance;
    private void Awake()
    {
        Instance = this;

        //add close event listener
        closeUIButton.onClick.RemoveAllListeners();
        closeUIButton.onClick.AddListener(Hide);
    }

    Dialog dialog = new Dialog();

    //set dialog Title
    public DialogUIBehaviour SetTitle(string titleToSet)
    {
        dialog.Title = titleToSet;
        return this;
    }

    public DialogUIBehaviour SetMessage(string messageToSet)
    {
        dialog.Message = messageToSet;
        return this;
    }

    //public DialogUIBehaviour SetCloseButtonText(string closeText)
    //{
    //    dialog.CloseBtnText = closeText;
    //    return this;
    //}

    public void Show()
    {
        titleUIText.text = dialog.Title;
        messageUIText.text = dialog.Message;

        closeUIButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = dialog.CloseBtnText;

        canvas.enabled=true;
    }

    public void Hide()
    {
        canvas.enabled = false;

        //reset the dialog
        dialog = new Dialog();
    }
}
*/