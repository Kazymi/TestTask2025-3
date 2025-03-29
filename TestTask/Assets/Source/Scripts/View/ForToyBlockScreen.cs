using System;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class ForToyBlockScreen : MonoBehaviour
{
    [SerializeField] private RectTransform forToyBlockArea;
    [SerializeField] private Transform floorPosition;

    [Inject] private ForToyBlockScreenMediator forToyBlockScreenMediator;

    private void OnEnable()
    {
        forToyBlockScreenMediator.OnItemAttachToTowerEvent += OnItemAttachToTowerHandler;
        forToyBlockScreenMediator.OnItemOutsideAreaEvent += OnItemAttachToTowerHandler;
    }

    private void OnItemAttachToTowerHandler(IDraggable outsideAreaObject)
    {
        outsideAreaObject.LockDrag();
        outsideAreaObject.dragTransform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.OutBounce).OnComplete(() =>
        {
            var iPooled = outsideAreaObject.dragTransform.GetComponent<IPooledObject>();
            if (iPooled != null)
            {
                iPooled.ReturnToPool();
                outsideAreaObject.dragTransform.localScale=Vector3.one;
                outsideAreaObject.UnLockDrag();
            }
            else
            {
                Destroy(outsideAreaObject.dragTransform.gameObject);
            }
        });
    }

    private void OnItemAttachToTowerHandler((IDraggable droppedItem, Vector3 dropPosition) dropItemParameters)
    {
        var sequence = DOTween.Sequence();
        dropItemParameters.droppedItem.LockDrag();
        sequence.Append(dropItemParameters.droppedItem.dragTransform.transform.DOLocalMove(dropItemParameters.dropPosition, 0.5f)
            .SetEase(Ease.OutBounce));
        sequence.OnComplete(() =>
        {
            dropItemParameters.droppedItem.UnLockDrag();
        });
    }

    private void Awake()
    {
        forToyBlockScreenMediator.Initialize(forToyBlockArea);
        forToyBlockScreenMediator.SetStartPosition(floorPosition);
    }
}