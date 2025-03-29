using System;
using Zenject;

public class ItemDropProxy : IInitializable
{
    [Inject] private ItemDragMediator itemDragMediator;
    [Inject] private TrashScreenMediator trashScreenMediator;
    [Inject] private ForToyBlockScreenMediator forToyBlockScreenMediator;

    public event Action<IDraggable> OnDropForToyBlockScreenEvent; 
    public event Action<IDraggable> OnDropForTrashScreenEvent;
    public event Action<IDraggable> OnDropNotAPlayingFieldEvent; 
    public void Initialize()
    {
        itemDragMediator.OnDragFinishEvent += OnDragFinishHandler;
    }

    private void OnDragFinishHandler(IDraggable draggable)
    {
        //TODO move conditions to a separate module
        if (forToyBlockScreenMediator.IsLocatedWithinArena(draggable.dragTransform.position))
        {
            OnDropForToyBlockScreenEvent?.Invoke(draggable);
            return;
        }

        if (trashScreenMediator.IsLocatedWithinArena(draggable.dragTransform.position))
        {
            OnDropForTrashScreenEvent?.Invoke(draggable);
            return;
        }
        
        //Here the points where the cube is dropped are tracked.
        //if the cube is released outside the playing area, it disappears.
        OnDropNotAPlayingFieldEvent?.Invoke(draggable);
        
    }
}