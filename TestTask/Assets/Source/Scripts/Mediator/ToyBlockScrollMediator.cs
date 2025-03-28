using System;
using Zenject;

public class ToyBlockScrollMediator
{
    [Inject] private ToyBlockConfigurationProxy toyBlockConfigurationProxy;

    public event Action<ToyBlockData[]> OnToyBlockDataUpdatedEvent;

    public void Initialize()
    {
        var toyBlockDatas = toyBlockConfigurationProxy.GetToyBlockDatas();
        OnToyBlockDataUpdatedEvent?.Invoke(toyBlockDatas);
    }
}