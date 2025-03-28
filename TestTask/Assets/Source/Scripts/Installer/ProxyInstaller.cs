using Zenject;

public class ProxyInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ToyBlockConfigurationProxy>().AsSingle();
    }
}