using UnityEngine;
using Zenject;

//Zenject Pool is overkill for simple tasks — a custom pool is faster, more flexible, and dependency-free.
public class PoolInstaller : MonoInstaller
{
    [SerializeField] private Transform poolTransform;
    [SerializeField] private ToyBlockUI toyBlockUI;

    public override void InstallBindings()
    {
        var factoryToyBlockUI = Container.Instantiate<FactoryMonoDIObject<ToyBlockUI>>();
        factoryToyBlockUI.Initialize(toyBlockUI.gameObject, poolTransform);
        var poolToyBlockUI = new Pool<ToyBlockUI>(factoryToyBlockUI);
        Container.Bind<Pool<ToyBlockUI>>().FromInstance(poolToyBlockUI);
    }
}