using Zenject;

public class MediatorInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ToyBlockScrollMediator>().AsSingle();
        Container.Bind<ToyBlockInteractiveSpawnerMediator>().AsSingle();
        Container.BindInterfacesAndSelfTo<ItemDragMediator>().AsSingle();
        Container.BindInterfacesAndSelfTo<ToyBlockUIMediator>().AsSingle();
        Container.Bind<ToyBlockInteractiveMediator>().AsSingle();
        Container.BindInterfacesAndSelfTo<TrashScreenMediator>().AsSingle();
        Container.BindInterfacesAndSelfTo<ForToyBlockScreenMediator>().AsSingle();
        Container.BindInterfacesAndSelfTo<PopupTextSpawnerMediator>().AsSingle();
    }
}