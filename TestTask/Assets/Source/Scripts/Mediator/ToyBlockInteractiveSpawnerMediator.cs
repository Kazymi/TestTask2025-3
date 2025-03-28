using System;
using Zenject;

public class ToyBlockInteractiveSpawnerMediator
{
    [Inject] private ToyBlockConfigurationProxy toyBlockConfigurationProxy;
    [Inject] private Pool<ToyBlockInteractive> poolToyBlockInteractive;

    public event Action<ToyBlockInteractive> OnToyBlockInteractiveSpawnedEvent;

    public ToyBlockInteractive SpawnToyBlockInteractable(int toyBlockIndex)
    {
        var clickedToyData = toyBlockConfigurationProxy.GetToyBlockDataById(toyBlockIndex);
        var newInteractableObject = poolToyBlockInteractive.Pull();
        newInteractableObject.Initialize(toyBlockIndex, clickedToyData.SpriteOfBlock);
        OnToyBlockInteractiveSpawnedEvent?.Invoke(newInteractableObject);
        return newInteractableObject;
    }
}