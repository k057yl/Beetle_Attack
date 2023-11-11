using UnityEngine;
using Zenject;

public class PopupInstaller : MonoInstaller
{
    [SerializeField] private GameObject _popupPrefab;

    public override void InstallBindings()
    {
        BindPopup();
    }

    private void BindPopup()
    {
        Container
            .Bind<Popup>()
            .FromComponentInNewPrefab(_popupPrefab)
            .AsSingle();
    }
}