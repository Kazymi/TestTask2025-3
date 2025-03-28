using Zenject;

public class MediatorInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ToyBlockScrollMediator>().AsSingle();
    }
}