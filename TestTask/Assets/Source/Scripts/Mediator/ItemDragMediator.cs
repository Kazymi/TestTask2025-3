using System;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class ItemDragMediator : ITickable
{
    private IDraggable attachedDragObject;

    public event Action<(Vector2 mousePosition, IDraggable dragObject)> OnDragEvent;
    public event Action<bool> OnDragStatusChangeEvent;
    public event Action<IDraggable> OnDragFinishEvent; 
    public event Action<IDraggable> OnDragStartEvent; 

    public void TryToAttachDragObject(IDraggable dragObject)
    {
        if (attachedDragObject != null)
        {
            return;
        }

        OnDragStatusChangeEvent?.Invoke(true);
        OnDragStartEvent?.Invoke(dragObject);
        attachedDragObject = dragObject;
    }

    public void TryToDetachDragObject(IDraggable dragObject)
    {
        if (attachedDragObject == dragObject)
        {
            OnDragStatusChangeEvent?.Invoke(false);
            OnDragFinishEvent?.Invoke(attachedDragObject);
            attachedDragObject = null;
        }
    }

    public void DetachCurrentDragObject()
    {
        if (attachedDragObject != null)
        {
            OnDragStatusChangeEvent?.Invoke(false);
            OnDragFinishEvent?.Invoke(attachedDragObject);
            attachedDragObject = null;
        }
    }

    public void Tick()
    {
        if (attachedDragObject != null)
        {
            Vector2 mousePosition = Input.mousePosition;
            OnDragEvent?.Invoke((mousePosition, attachedDragObject));
        }
    }
}