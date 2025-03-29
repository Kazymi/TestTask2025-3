using TMPro;
using UnityEngine;

public class PopupText : MonoPooled
{
    [SerializeField] private TMP_Text popupText;

    public TMP_Text Text => popupText;

    public void Initialize(string text)
    {
        popupText.text = text;
    }
}