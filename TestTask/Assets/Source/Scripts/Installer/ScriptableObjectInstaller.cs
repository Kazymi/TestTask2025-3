using System.Collections;
using UnityEngine;
using Zenject;

public class ScriptableObjectInstaller : MonoInstaller
{
    [SerializeField] private ToyBlocksConfiguration toyBlocksConfiguration;

    public override void InstallBindings()
    {
        Container.Bind<ToyBlocksConfiguration>().FromScriptableObject(toyBlocksConfiguration).AsSingle();
    }
}
