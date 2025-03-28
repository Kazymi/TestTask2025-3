using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;


public class ToyBlockUI : MonoPooled,IPointerDownHandler,IPointerUpHandler
{
    [SerializeField] private Button button;
    [SerializeField] private Image toyBlockImage;

    [Inject] private ToyBlockUIMediator toyBlockUIMediator;
    private int attachedIndex;
    
    public void Initialize(Sprite itemSprite, int indexOfBlock)
    {
        attachedIndex = indexOfBlock;
        toyBlockImage.sprite = itemSprite;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
       toyBlockUIMediator.OnPointerDown(attachedIndex);   
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        toyBlockUIMediator.OnPointerExit(attachedIndex);  
    }
}