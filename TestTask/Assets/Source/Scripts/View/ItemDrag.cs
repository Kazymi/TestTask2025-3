using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ItemDrag : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollbar;
    [SerializeField] private Canvas canvasRectTransform;
    [SerializeField] private RectTransform dragArea;
    [Inject] private ItemDragMediator itemDragMediator;

    private void OnEnable()
    {
        itemDragMediator.OnDragStartEvent += OnDragStartHandler;
        itemDragMediator.OnDragEvent += ItemDragMediatorOnOnDragHandler;
        itemDragMediator.OnDragStatusChangeEvent += OnDragStatusChangeHandler;
    }

    private void OnDisable()
    {
        itemDragMediator.OnDragStartEvent -= OnDragStartHandler;
        itemDragMediator.OnDragEvent -= ItemDragMediatorOnOnDragHandler;
        itemDragMediator.OnDragStatusChangeEvent -= OnDragStatusChangeHandler;
    }

    private void OnDragStartHandler(IDraggable draggable)
    {
        draggable.dragTransform.SetParent(dragArea);
    }

    private void OnDragStatusChangeHandler(bool isDragging)
    {
        scrollbar.enabled = !isDragging;
    }
    
    private void ItemDragMediatorOnOnDragHandler((Vector2 mousePosition, IDraggable dragObject) dragParameters)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRectTransform.transform as RectTransform,
            dragParameters.mousePosition,
            canvasRectTransform.worldCamera,
            out Vector2 localPoint);
        dragParameters.dragObject.dragTransform.localPosition = new Vector3(localPoint.x, localPoint.y, 0);
    }
}