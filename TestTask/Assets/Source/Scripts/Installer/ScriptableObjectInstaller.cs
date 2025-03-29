using System.Collections;
using UnityEngine;
using Zenject;

public class ScriptableObjectInstaller : MonoInstaller
{
    [SerializeField] private ToyBlocksConfiguration toyBlocksConfiguration;
    [SerializeField] private ToyBlockGameConfiguration toyBlockGameConfiguration;
    [SerializeField] private LocalizationLibrary localizationLibrary;

    public override void InstallBindings()
    {
        Container.Bind<ToyBlocksConfiguration>().FromScriptableObject(toyBlocksConfiguration).AsSingle();
        Container.Bind<ToyBlockGameConfiguration>().FromScriptableObject(toyBlockGameConfiguration).AsSingle();
        Container.Bind<LocalizationLibrary>().FromScriptableObject(localizationLibrary).AsSingle();
    }
}
