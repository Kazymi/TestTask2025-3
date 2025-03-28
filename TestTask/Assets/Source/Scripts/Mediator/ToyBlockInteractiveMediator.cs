using Zenject;

public class ToyBlockInteractiveMediator
{
    [Inject] private ItemDragMediator itemDragMediator;
    public void OnToyBlockInteractPointerUp(ToyBlockInteractive toyBlockInteractive)
    {
        itemDragMediator.TryToDetachDragObject(toyBlockInteractive);
    }
    
    public void OnToyBlockInteractPointerDown(ToyBlockInteractive toyBlockInteractive)
    {
        itemDragMediator.TryToAttachDragObject(toyBlockInteractive);
    }
}