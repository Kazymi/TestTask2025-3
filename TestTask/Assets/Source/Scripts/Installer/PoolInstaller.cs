using UnityEngine;
using Zenject;

//Zenject Pool is overkill for simple tasks — a custom pool is faster, more flexible, and dependency-free.
public class PoolInstaller : MonoInstaller
{
    [SerializeField] private Transform poolTransform;
    [SerializeField] private ToyBlockUI toyBlockUI;
    [SerializeField] private PopupText popupText;
    [SerializeField] private ToyBlockInteractive toyBlockInteractive;

    public override void InstallBindings()
    {
        var factoryToyBlockUI = Container.Instantiate<FactoryMonoDIObject<ToyBlockUI>>();
        factoryToyBlockUI.Initialize(toyBlockUI.gameObject, poolTransform);
        var poolToyBlockUI = new Pool<ToyBlockUI>(factoryToyBlockUI);
        Container.Bind<Pool<ToyBlockUI>>().FromInstance(poolToyBlockUI);

        var factoryToyBlockInteractive = Container.Instantiate<FactoryMonoDIObject<ToyBlockInteractive>>();
        factoryToyBlockInteractive.Initialize(toyBlockInteractive.gameObject, poolTransform);
        var poolToyBlockInteractive = new Pool<ToyBlockInteractive>(factoryToyBlockInteractive);
        Container.Bind<Pool<ToyBlockInteractive>>().FromInstance(poolToyBlockInteractive);

        var factoryPopupText = Container.Instantiate<FactoryMonoDIObject<PopupText>>();
        factoryPopupText.Initialize(popupText.gameObject, poolTransform);
        var poolPopupText = new Pool<PopupText>(factoryPopupText);
        Container.Bind<Pool<PopupText>>().FromInstance(poolPopupText);
    }
}