using System;
using UnityEngine;

[Serializable]
public class ToyBlockData
{
    [field: SerializeField] public int Index { get; private set; }
    [field: SerializeField] public Sprite SpriteOfBlock { get; private set; }
}