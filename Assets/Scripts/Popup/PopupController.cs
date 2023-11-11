using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class PopupController : MonoBehaviour
{
    [Inject] private IInput _input;
    [Inject] private Popup _popup;

    public bool IsFollow = false;

    private void Start()
    {
        _popup.Hide();
    }

    void Update()
    {
        if (_input.IsExitTriggered())
        {
            ExitGame();
        }
    }

    private void ExitGame()
    {
        Action action = () => ExitRealy();
        Action action1 = () => _popup.Hide();

        var popupData = new PopupData
        {
            Name = "Gala",
            Message = "Meo-Meo",
            Action = action,
            Action1 = action1,
            ShowOKButton1 = true,
            ShowOKButton2 = true,
            ShowOKButton3 = true
        };

        _popup.UpdateText(popupData);
        _popup.Show();

        StartCoroutine(HidePopupAfterDelay());
    }

    private void ExitRealy()
    {
        var popupData = new PopupData
        {
            Name = "Gala",
            Message = "Gala, yop tvoyu mat^",
            ShowOKButton1 = false,
            ShowOKButton2 = false,
            ShowOKButton3 = false
        };

        _popup.UpdateText(popupData);
        _popup.Show();
    }

    private IEnumerator HidePopupAfterDelay()
    {
        yield return new WaitForSeconds(Constants.ONE);
        _popup.Hide();
    }

    public void FollowNPC()
    {
        Action action = () =>
        {
            FollowOK();
            Debug.Log("Action_Yes");
        };

        Action action1 = () =>
        {
            FollowNO();
            Debug.Log("Action_No");
        };

        var popupData = new PopupData
        {
            Name = "Bill",
            Message = "Get me out of here, I'm very scared",
            Action = action,
            Action1 = action1,
            ShowOKButton1 = true,
            ShowOKButton2 = true,
            ShowOKButton3 = true
        };

        _popup.UpdateText(popupData);
        _popup.Show();

        StartCoroutine(HidePopupAfterDelay());
    }

    private void FollowOK()
    {
        IsFollow = true;

        var popupData = new PopupData
        {
            Name = "Bill",
            Message = "Thank you, I owe you",
            ShowOKButton1 = true,
            ShowOKButton2 = true,
            ShowOKButton3 = true
        };

        _popup.UpdateText(popupData);
        _popup.Show();
    }

    private void FollowNO()
    {
        IsFollow = false;

        var popupData = new PopupData
        {
            Name = "Bill",
            Message = "Fuck you",
            ShowOKButton1 = true,
            ShowOKButton2 = true,
            ShowOKButton3 = true
        };

        _popup.UpdateText(popupData);
        _popup.Show();
    }
}