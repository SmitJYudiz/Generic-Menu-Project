using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Stage2DemoBehaviour : MonoBehaviour
{
    [SerializeField]
    Button showMessageButton;

    [Multiline]
    [SerializeField] string longText;

    private void Start()
    {
        //to avoid memory leaks and also to avoid assigning events more than once;
        showMessageButton.onClick.RemoveAllListeners();

        //showMessageButton.onClick.AddListener(CallbackMenu);

        //showMessageButton.onClick.AddListener(BothButtons);

        //showMessageButton.onClick.AddListener(OnlyNegativeButton);

        //showMessageButton.onClick.AddListener(OnlyPositiveButton);


        showMessageButton.onClick.AddListener(delegate {

            DifferentVersionCinfirmDialogBehaviour.Instance.CreateMenuWithCallBackNew("Test Trial", "test message", Color.red, (temp) =>
            {
                if (temp)
                {
                   //positive button case
                   Debug.Log("Positive called");
                }
                else
                {
                   //negative button case
                   Debug.Log("negative called");
                }
            },
            "yes please!", "No, Never");
        });
    }




    public void CallbackMenu()
    {
        DifferentVersionCinfirmDialogBehaviour.Instance.CreateMenuWithCallback("Temp Title", "temp msg", true, 5, (val) => {
            if (val)
            {
                //means it is called from positive button, so here write all the statements/actions to do which should be executed when positive button is pressed
                Debug.Log("Yes clicked");
            }
            else
            {
                //this is the case of negative button
                //means it is called from positive button, so here write all the statements/actions to do which should be executed when positive button is pressed
                Debug.Log("No clicked");
            }

        }, "Yes", "NO");
    }

    public void BothButtons()
    {
        DifferentVersionCinfirmDialogBehaviour.Instance.CreateMenu(() => Debug.Log("negative clicked"), () => Debug.Log("positive clicked"),
           () => Debug.Log("Close Clicked"), "Warning", "Hi Buddy", Color.red, "yay", "nai", 0.22f);
    }

    public void OnlyPositiveButton()
    {
        DifferentVersionCinfirmDialogBehaviour.Instance.CreateMenu(() => Debug.Log("negative clicked"), () => Debug.Log("positive clicked"),
           () => Debug.Log("Close Clicked"), "Warning", "Hi Buddy", Color.red, "yay", "", 0.22f);
        Debug.Log("new method called");
        //DifferentVersionCinfirmDialogBehaviour.Instance.CreateMenuWithoutActions( "Warning", "Hi Buddy", Color.red, "yay", "Nai", 0.22f);

    }

    public void OnlyNegativeButton()
    {
        DifferentVersionCinfirmDialogBehaviour.Instance.CreateMenu(() => Debug.Log("negative clicked"), () => Debug.Log("positive clicked"),
           () => Debug.Log("Close Clicked"), "Warning", "Hi Buddy", Color.red, "", "Nai", 0.22f);
    }
}

    //public void OnShowMessageButtonClicked()
    //{
    //    Debug.Log("Show message Button Clicked");

    //    ConfirmDialogUiBehaviour.Instance.SetTitle("Message 1")
    //        .SetMessage(longText)
    //        .SetButtonsColor(DialogButtonColor.Orange)
    //        .SetFadeduration(0.6f)
    //        .OnPositiveButtonClicked(() => Debug.Log("positive button clicked"))
    //        .OnNegativeButtonclicked(delegate { Debug.Log("Negative button clicked"); })
    //        .SetPositiveButtonText("Yay")            
    //        .Show();

    //    //DialogUIBehaviour.Instance.SetTitle("Warning")
    //    //    .SetMessage("enough is enough, stop wasting my net ;( ")
    //    //    .SetCloseButtonText("Okay, Sorry")
    //    //    .Show();
    //}

   

