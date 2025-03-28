using UnityEngine;
using Zenject;

public class ToyBlockInteractiveSpawner : MonoBehaviour
{
    [SerializeField] private Transform toyBlockParent;
    [Inject] private ToyBlockInteractiveSpawnerMediator toyBlockSpawnerMediator;

    private void OnEnable()
    {
        toyBlockSpawnerMediator.OnToyBlockInteractiveSpawnedEvent += OnToyBlockInteractiveSpawnedHandler;
    }

    private void OnDisable()
    {
        toyBlockSpawnerMediator.OnToyBlockInteractiveSpawnedEvent -= OnToyBlockInteractiveSpawnedHandler;
    }

    private void OnToyBlockInteractiveSpawnedHandler(ToyBlockInteractive toyBlockInteractive)
    {
        toyBlockInteractive.transform.SetParent(toyBlockParent);
        toyBlockInteractive.transform.localScale = Vector3.one;
    }
}