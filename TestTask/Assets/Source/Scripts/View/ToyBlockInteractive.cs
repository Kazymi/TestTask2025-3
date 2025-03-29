using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class ToyBlockInteractive : MonoPooled, IPointerUpHandler, IPointerDownHandler, IDraggable
{
    [SerializeField] private Image toyBlockImage;

    [Inject] private ToyBlockInteractiveMediator toyBlockInteractiveMediator;
    private int attachedIndex; //If you need to check the cube number later

    public bool CanDrag { get; private set; } = true;
    public Transform dragTransform => transform;

    public void LockDrag()
    {
        CanDrag = false;
    }

    public void UnLockDrag()
    {
        CanDrag = true;
    }

    public void Initialize(int toyBlockIndex, Sprite itemSprite)
    {
        attachedIndex = toyBlockIndex;
        toyBlockImage.sprite = itemSprite;
    }

    public override void Initialize()
    {
        base.Initialize();
        transform.DOKill();
        transform.localScale=Vector3.one;
        UnLockDrag();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        toyBlockInteractiveMediator.OnToyBlockInteractPointerUp(this);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (CanDrag)
        {
            toyBlockInteractiveMediator.OnToyBlockInteractPointerDown(this);
        }
    }
}