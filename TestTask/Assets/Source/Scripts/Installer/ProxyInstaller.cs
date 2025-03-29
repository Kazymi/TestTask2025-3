using Zenject;

public class ProxyInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ToyBlockConfigurationProxy>().AsSingle();
        Container.Bind<LocalizationProxy>().AsSingle();
        Container.BindInterfacesAndSelfTo<ItemDropProxy>().AsSingle();
        Container.BindInterfacesAndSelfTo<PopupTextSpawnerProxy>().AsSingle();
    }
}