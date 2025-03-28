using UnityEngine;

public interface IDraggable
{
    bool CanDrag { get; }
    Transform dragTransform { get; }
    void LockDrag();
    void UnLockDrag();
}