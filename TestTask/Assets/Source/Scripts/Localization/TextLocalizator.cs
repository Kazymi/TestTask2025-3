using TMPro;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(TMP_Text))]
public class TextLocalizator : MonoBehaviour
{
    [SerializeField] private string id;

    [Inject] private LocalizationProxy localizationProxy;
    private TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        text.text = localizationProxy.GetLocalization(id);
    }
}