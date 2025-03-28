using System;
using UnityEngine;
using Zenject;

public class TrashScreenMediator : ScreenMediatorBase, IInitializable
{
    [Inject] private ItemDragMediator itemDragMediator;

    public event Action<Transform> OnItemDroppedOnTrashScreenEvent;

    public void Initialize()
    {
        itemDragMediator.OnDragFinishEvent += OnDragFinishHandler;
    }

    private void OnDragFinishHandler(IDraggable dragItem)
    {
        ItemDrop(dragItem);
    }
    
    private void ItemDrop(IDraggable dragItem)
    {
        if(IsLocatedWithinArena(dragItem.dragTransform.position))
        {
            OnItemDroppedOnTrashScreenEvent?.Invoke(dragItem.dragTransform);
        }
    }
}