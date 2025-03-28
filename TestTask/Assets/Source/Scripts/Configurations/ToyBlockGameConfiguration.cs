using UnityEngine;

[CreateAssetMenu(menuName = "Configurations/Create ToyBlockGameConfiguration", fileName = "ToyBlockGameConfiguration", order = 0)]
public class ToyBlockGameConfiguration : ScriptableObject
{
    [field:SerializeField] public float SizeOfToyBlockY { get; private set; }
    [field:SerializeField] public float SizeOfToyBlockX { get; private set; }
}