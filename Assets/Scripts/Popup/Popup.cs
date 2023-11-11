using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    [SerializeField] private Text _nameText;
    [SerializeField] private Text _messageText;
    [SerializeField] private Button _button1;
    [SerializeField] private Button _button2;
    [SerializeField] private Button _button3;

    private PopupData _popupData;

    public void UpdateText(PopupData popupData)
    {
        _popupData = popupData;

        _nameText.text = _popupData.Name;
        _messageText.text = _popupData.Message;

        _button1.gameObject.SetActive(_popupData.ShowOKButton1);
        _button2.gameObject.SetActive(_popupData.ShowOKButton2);
        _button3.gameObject.SetActive(_popupData.ShowOKButton3);
    }

    private void OnEnable()
    {
        _button1.onClick.AddListener(() =>
        {
            _popupData.Action?.Invoke();
            Time.timeScale = Constants.ONE;
        });

        _button2.onClick.AddListener(() =>
        {
            _popupData.Action1?.Invoke();
            Time.timeScale = Constants.ONE;
        });

        _button3.onClick.AddListener(() => Hide());
    }

    private void OnDisable()
    {
        _button1.onClick.RemoveAllListeners();
        _button2.onClick.RemoveAllListeners();
        _button3.onClick.RemoveAllListeners();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        Time.timeScale = Constants.ZERO;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        Time.timeScale = Constants.ONE;
    }
}