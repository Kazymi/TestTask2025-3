using System.Linq;
using UnityEngine;
using Zenject;

public class ToyBlockConfigurationProxy
{
    [Inject] private ToyBlocksConfiguration toyBlocksConfiguration;

    public ToyBlockData GetToyBlockDataById(int indexOfBlock)
    {
        var returnedBlock = toyBlocksConfiguration.ToyBlockDatas.FirstOrDefault(t => t.Index == indexOfBlock);
        if (returnedBlock != null)
        {
            return returnedBlock;
        }

        Debug.LogError($"Failed to find '{indexOfBlock}' in the list; returned the first element instead");
        var first = toyBlocksConfiguration.ToyBlockDatas.FirstOrDefault();
        if (first == null)
        {
            Debug.LogError("Failed return first element, toy configuration null");
        }

        return first;
    }

    public ToyBlockData[] GetToyBlockDatas()
    {
        return toyBlocksConfiguration.ToyBlockDatas;
    }
}