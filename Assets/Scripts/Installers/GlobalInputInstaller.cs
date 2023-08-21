using Zenject;

public class GlobalInputInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindInput();
    }
    
    private void BindInput()
    {
        Container.Bind<IInput>().To<InputHandlerKeyboard>().AsSingle();
    }
}