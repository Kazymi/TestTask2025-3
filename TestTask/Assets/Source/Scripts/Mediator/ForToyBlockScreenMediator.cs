using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class ForToyBlockScreenMediator : ScreenMediatorBase, IInitializable
{
    [Inject] private ItemDragMediator itemDragMediator;
    [Inject] private ForToyBlockScreenMediator forToyBlockScreenMediator;
    [Inject] private ToyBlockGameConfiguration toyBlockGameConfiguration;

    private List<IDraggable> droppedItems = new();
    private Transform startPosition;

    public event Action<(Transform droppedItem, Vector3 dropPosition)> OnItemAttachToTowerEvent;
    public event Action<Transform> OnItemOutsideAreaEvent;

    public void Initialize()
    {
        itemDragMediator.OnDragFinishEvent += OnDragFinishHandler;
        itemDragMediator.OnDragStartEvent += OnDragStartHandler;
    }

    public void SetStartPosition(Transform startPosition)
    {
        this.startPosition = startPosition;
    }

    private void OnDragStartHandler(IDraggable draggable)
    {
        if (droppedItems.Contains(draggable))
        {
            droppedItems[droppedItems.IndexOf(draggable)] = null;
            ResizeTower();
        }
    }

    private void ResizeTower()
    {
        var itemsToRespawn = new List<IDraggable>();
        var hasFoundNullItem = false;
        foreach (var item in droppedItems)
        {
            if (item == null)
            {
                hasFoundNullItem = true;
            }

            if (hasFoundNullItem)
            {
                itemsToRespawn.Add(item);
            }
        }

        droppedItems.RemoveAll(item => itemsToRespawn.Contains(item));
        foreach (var item in itemsToRespawn.Where(item => item != null))
        {
            if (IsCanDropByYPos(item.dragTransform.localPosition.y) &&
                IsCanDropByXPos(item.dragTransform.localPosition.x))
            {
                DropNewAttachedItem(item);
            }
            else
            {
                OnItemOutsideAreaEvent?.Invoke(item.dragTransform);
            }
        }
    }

    private void DropNewAttachedItem(IDraggable dragItem, bool withRandomX = false)
    {
        dragItem.dragTransform.SetParent(startPosition);
        var yPos = toyBlockGameConfiguration.SizeOfToyBlockY * droppedItems.Count;
        var endPosition = new Vector3(dragItem.dragTransform.localPosition.x, yPos,
            dragItem.dragTransform.localPosition.z);
        if (withRandomX) //The checkbox is responsible for the randomizer of the position when falling, within half the edge.
        {
            if (droppedItems.Count != 0)
            {
                var addLocalX = droppedItems.Last().dragTransform.localPosition.x + Random.Range(
                    -toyBlockGameConfiguration.SizeOfToyBlockX / 2f, toyBlockGameConfiguration.SizeOfToyBlockX / 2f);
                endPosition.x = addLocalX;
            }
        }

        OnItemAttachToTowerEvent?.Invoke((dragItem.dragTransform, endPosition));
        droppedItems.Add(dragItem);
    }

    private void OnDragFinishHandler(IDraggable dragItem)
    {
        ItemDrop(dragItem);
    }

    private void ItemDrop(IDraggable dragItem)
    {
        if (IsLocatedWithinArena(dragItem.dragTransform.position))
        {
            var localPosition = startPosition.InverseTransformPoint(dragItem.dragTransform.position);
            if (IsCanDropByYPos(localPosition.y) && IsCanDropByXPos(localPosition.x))
            {
                DropNewAttachedItem(dragItem,true);
            }
            else
            {
                OnItemOutsideAreaEvent?.Invoke(dragItem.dragTransform);
            }
        }
    }

    private bool IsCanDropByYPos(float YPos)
    {
        return YPos > 0 + toyBlockGameConfiguration.SizeOfToyBlockY * droppedItems.Count;
    }

    private bool IsCanDropByXPos(float xPos)
    {
        if (droppedItems.Count == 0) return true;
        var lastX = droppedItems.Last();
        var localX = lastX.dragTransform.localPosition.x;
        return xPos >= localX - toyBlockGameConfiguration.SizeOfToyBlockX / 2f &&
               xPos <= localX + toyBlockGameConfiguration.SizeOfToyBlockX / 2f;
    }
}