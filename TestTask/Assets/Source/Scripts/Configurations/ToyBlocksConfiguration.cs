using UnityEngine;

[CreateAssetMenu(menuName = "Configuration/Create ToyBlocksConfiguration", fileName = "ToyBlocksConfiguration",
    order = 0)]
public class ToyBlocksConfiguration : ScriptableObject
{
    [field: SerializeField] public ToyBlockData[] ToyBlockDatas { get; private set; }
}