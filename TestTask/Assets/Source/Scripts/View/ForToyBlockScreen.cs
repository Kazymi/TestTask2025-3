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

    private void OnItemAttachToTowerHandler(Transform outsideAreaObject)
    {
        outsideAreaObject.DOScale(Vector3.zero, 0.5f).SetEase(Ease.OutBounce).OnComplete(() =>
        {
            var iPooled = outsideAreaObject.GetComponent<IPooledObject>();
            if (iPooled != null)
            {
                outsideAreaObject.localScale=Vector3.one;
                iPooled.ReturnToPool();
            }
            else
            {
                Destroy(outsideAreaObject.gameObject);
            }
        });
    }

    private void OnItemAttachToTowerHandler(
        (Transform droppedItem, Vector3 dropPosition) dropItemParameters)
    {
        var sequence = DOTween.Sequence();
        sequence.Append(dropItemParameters.droppedItem.transform.DOLocalMove(dropItemParameters.dropPosition, 0.5f)
            .SetEase(Ease.OutBounce));
    }

    private void Awake()
    {
        forToyBlockScreenMediator.Initialize(forToyBlockArea);
        forToyBlockScreenMediator.SetStartPosition(floorPosition);
    }
}