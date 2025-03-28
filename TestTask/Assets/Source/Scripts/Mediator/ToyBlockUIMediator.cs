using System;
using UnityEngine;
using Zenject;

public class ToyBlockUIMediator : ITickable
{
    [Inject] private ToyBlockInteractiveSpawnerMediator toyBlockSpawnerMediator;
    [Inject] private ItemDragMediator itemDragMediator;

    private DateTime timeToPointerDown;
    private bool isTouched;
    private bool isWaitingForFinishTouch;
    private int attachedToyBlock;

    private const float timeForNewToyBlock = 0.15f;

    public void OnPointerDown(int toyBlockIndex)
    {
        if (isTouched)
        {
            return;
        }

        isTouched = true;
        timeToPointerDown = DateTime.Now;
        attachedToyBlock = toyBlockIndex;
    }

    public void OnPointerExit(int toyBlockIndex)
    {
        if (isTouched == false || toyBlockIndex != attachedToyBlock)
        {
            return;
        }

        isTouched = false;
        var difference = DateTime.Now - timeToPointerDown;
        var seconds = difference.TotalSeconds;
        if (seconds > timeForNewToyBlock)
        {
            isWaitingForFinishTouch = true;
            var newToyBlock = toyBlockSpawnerMediator.SpawnToyBlockInteractable(attachedToyBlock);
            itemDragMediator.TryToAttachDragObject(newToyBlock);
        }
    }

    private void WaitForFinishTouch()
    {
        if (isWaitingForFinishTouch)
        {
            if (Input.touchCount > 0)
            {
                isWaitingForFinishTouch = false;
                itemDragMediator.DetachCurrentDragObject();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isWaitingForFinishTouch = false;
                itemDragMediator.DetachCurrentDragObject();
            }
        }
    }

    public void Tick()
    {
        WaitForFinishTouch();
    }
}