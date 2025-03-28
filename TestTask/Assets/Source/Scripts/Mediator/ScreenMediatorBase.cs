using UnityEngine;

public class ScreenMediatorBase
{
    private RectTransform attachedTrashArea;
    private Camera camera;

    public void Initialize(RectTransform trashArea)
    {
        camera = Camera.main;
        attachedTrashArea = trashArea;
    }

    public bool IsLocatedWithinArena(Vector3 position)
    {
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(camera, position);
        return RectTransformUtility.RectangleContainsScreenPoint(attachedTrashArea, screenPoint, camera);
    }
}