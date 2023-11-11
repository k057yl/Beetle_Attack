using UnityEngine;
using Zenject;

public class PopupControllerInstaller : MonoInstaller
{
    [SerializeField] private GameObject _popupControllerPrefab;

    public override void InstallBindings()
    {
        BindPopupController();
    }

    private void BindPopupController()
    {
        Container
            .Bind<PopupController>()
            .FromComponentInNewPrefab(_popupControllerPrefab)
            .AsSingle();
    }
}