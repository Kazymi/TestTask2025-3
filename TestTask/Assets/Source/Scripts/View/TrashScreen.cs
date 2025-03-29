using System;
using DG.Tweening;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class TrashScreen : MonoBehaviour
{
    [SerializeField] private RectTransform trashArea;
    [SerializeField] private Transform trash;

    [Header("Animation parameters")] [SerializeField]
    private AnimationCurve flyingAnimationCurve;

    [SerializeField] private float flyingAnimationDuration;
    [SerializeField] private float flyingAnimationStrength;
    [SerializeField] private float trashScaleForFly;
    [SerializeField] private float trashScaleShakeStrength;
    [SerializeField] private float trashScaleShakeDuration;

    [Inject] private TrashScreenMediator trashScreenMediator;

    private void OnEnable()
    {
        trashScreenMediator.OnItemDroppedOnTrashScreenEvent += OnItemDroppedOnTrashScreenHandler;
    }

    private void OnDisable()
    {
        trashScreenMediator.OnItemDroppedOnTrashScreenEvent -= OnItemDroppedOnTrashScreenHandler;
    }

    private void Awake()
    {
        trashScreenMediator.Initialize(trashArea);
    }

    private void OnItemDroppedOnTrashScreenHandler(Transform animatedObject)
    {
        var sequence = DOTween.Sequence();
        float valFloat = 0f;
        animatedObject.SetParent(trash);
        var startPosition = animatedObject.localPosition;
        var endPosition = Vector3.zero;
        sequence.Append(DOTween.To(() => valFloat, x => valFloat = x, 1f, flyingAnimationDuration).OnUpdate(() =>
        {
            animatedObject.localPosition = Vector3.Lerp(startPosition, endPosition, valFloat);
            animatedObject.localPosition += new Vector3(flyingAnimationCurve.Evaluate(valFloat),
                flyingAnimationCurve.Evaluate(valFloat), 0f) * flyingAnimationStrength;
        }));

        sequence.Join(trash.DOScale(trashScaleForFly, flyingAnimationDuration));
        sequence.Join(animatedObject
            .DOLocalRotate(new Vector3(0, 0, Random.Range(0, 360)), flyingAnimationDuration));
        sequence.Join(animatedObject.DOScale(Vector3.zero, flyingAnimationDuration));

        sequence.Append(trash.DOShakeScale(trashScaleShakeDuration, trashScaleShakeStrength));
        sequence.Append(trash.DOScale(Vector3.one, trashScaleShakeStrength));
        sequence.OnComplete(() =>
        {
            var pooled = animatedObject.GetComponent<IPooledObject>();
            if (pooled != null)
            {
                pooled.ReturnToPool();
                animatedObject.transform.localScale = Vector3.one;
                animatedObject.transform.localRotation = Quaternion.identity;
            }
            else
            {
                Destroy(animatedObject.gameObject);
            }
        });
    }
}