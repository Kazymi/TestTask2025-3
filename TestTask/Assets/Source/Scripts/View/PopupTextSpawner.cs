using DG.Tweening;
using UnityEngine;
using Zenject;

public class PopupTextSpawner : MonoBehaviour
{
    [SerializeField] private Transform popupTextPosition;

    [Header("Animation")] [SerializeField] private float upAnimationDuration = 0.7f;
    [SerializeField] private float upAnimation = 150f;
    [Inject] private PopupTextSpawnerMediator popupTextSpawnerMediator;

    private void OnEnable()
    {
        popupTextSpawnerMediator.OnPopupTextSpawnedEvent += OnPopupTextSpawnedHandler;
    }

    private void OnPopupTextSpawnedHandler(PopupText popupText)
    {
        popupText.transform.SetParent(popupTextPosition);
        popupText.transform.localScale=Vector3.one;
        popupText.transform.localPosition = Vector3.zero;
        var movePos = popupText.transform.localPosition;
        movePos += Vector3.up * upAnimation;
        var sequence = DOTween.Sequence();

        sequence.Append(popupText.transform.DOLocalMove(movePos, upAnimationDuration));
        //0.6 and 0.4 smooth fade out towards the end, where 0.6 is the start and 0.4 is the time
        sequence.Join(DOVirtual.DelayedCall(upAnimationDuration * 0.6f,
            () => { popupText.Text.DOFade(0, upAnimationDuration * 0.4f); }));
        sequence.AppendInterval(upAnimationDuration * 0.4f);
        sequence.OnComplete(() =>
        {
            popupText.Text.color =
                new Color(popupText.Text.color.r, popupText.Text.color.g, popupText.Text.color.b, 1f);
            popupText.ReturnToPool();
        });
    }
}