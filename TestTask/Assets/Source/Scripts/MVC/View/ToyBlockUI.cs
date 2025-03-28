using UnityEngine;
using UnityEngine.UI;

public class ToyBlockUI : MonoPooled
{
    [SerializeField] private Image touBlockImage;

    private int attachedIndex;

    public void Initialize(Sprite itemSprite, int indexOfBlock)
    {
        attachedIndex = indexOfBlock;
        touBlockImage.sprite = itemSprite;
    }
}