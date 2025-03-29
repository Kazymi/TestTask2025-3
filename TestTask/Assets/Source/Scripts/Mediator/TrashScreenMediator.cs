using System;
using UnityEngine;
using Zenject;

public class TrashScreenMediator : ScreenMediatorBase, IInitializable
{
    [Inject] private ItemDropProxy itemDropProxy;

    public event Action<Transform> OnItemDroppedOnTrashScreenEvent;

    public void Initialize()
    {
        itemDropProxy.OnDropForTrashScreenEvent += OnDropForTrashScreenHandler;
        itemDropProxy.OnDropNotAPlayingFieldEvent +=
            OnDropForTrashScreenHandler; // If you throw an item outside the play area, it will also fly into the trash. You can change it to another mechanic
    }

    private void OnDropForTrashScreenHandler(IDraggable dragItem)
    {
        dragItem.LockDrag();
        OnItemDroppedOnTrashScreenEvent?.Invoke(dragItem.dragTransform);
    }
}