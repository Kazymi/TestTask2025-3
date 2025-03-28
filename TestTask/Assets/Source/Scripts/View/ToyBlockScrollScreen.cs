using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ToyBlockScrollScreen : MonoBehaviour
{
    [SerializeField] private Transform toyBlockParent;
    [Inject] private ToyBlockScrollMediator toyBlockScrollMediator;
    [Inject] private Pool<ToyBlockUI> pooledObjectSpawner;

    private List<ToyBlockUI> pooledObjects = new List<ToyBlockUI>();

    private void OnEnable()
    {
        toyBlockScrollMediator.OnToyBlockDataUpdatedEvent += OnToyBlockDataUpdatedHandler;
    }

    private void OnDisable()
    {
        toyBlockScrollMediator.OnToyBlockDataUpdatedEvent -= OnToyBlockDataUpdatedHandler;
    }

    private void Start()
    {
        toyBlockScrollMediator.Initialize();
    }

    private void OnToyBlockDataUpdatedHandler(ToyBlockData[] toyBlockDatas)
    {
        ClearPooledObjects();
        foreach (var toyBlockData in toyBlockDatas)
        {
            var pooledObject = pooledObjectSpawner.Pull();
            pooledObject.Initialize(toyBlockData.SpriteOfBlock, toyBlockData.Index);
            pooledObject.transform.SetParent(toyBlockParent);
            pooledObject.transform.localScale=Vector3.one;
            pooledObjects.Add(pooledObject);
        }
    }

    private void ClearPooledObjects()
    {
        foreach (var pooledObject in pooledObjects)
        {
            pooledObject.ReturnToPool();
        }
    }
}