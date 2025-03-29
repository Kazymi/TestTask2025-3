using System;
using Zenject;

public class PopupTextSpawnerProxy : IInitializable
{
    [Inject] private LocalizationProxy localizationProxy;
    [Inject] private ForToyBlockScreenMediator forToyBlockScreenMediator;
    [Inject] private ItemDropProxy itemDropProxy;

    public event Action<string> OnSpawnPopupTextEvent;

    public void Initialize()
    {
        
        itemDropProxy.OnDropForTrashScreenEvent += OnDropForTrashScreenHandler;
        forToyBlockScreenMediator.OnDropAtTowerEvent += OnDropAtTowerHandler;
        forToyBlockScreenMediator.OnDropOutsideTowerEvent += OnDropOutsideTowerHandler;
        itemDropProxy.OnDropNotAPlayingFieldEvent += OnDropNotAPlayingFieldHandler;
    }

    private void OnDropForTrashScreenHandler(IDraggable draggable)
    {
        OnSpawnPopupTextEvent?.Invoke(localizationProxy.GetLocalization("forTrashScreen"));
    }

    private void OnDropAtTowerHandler()
    {
        OnSpawnPopupTextEvent?.Invoke(localizationProxy.GetLocalization("forToyBlockScreen"));
    }
    
    private void OnDropOutsideTowerHandler()
    {
        OnSpawnPopupTextEvent?.Invoke(localizationProxy.GetLocalization("notAPlayingField"));
    }

    private void OnDropNotAPlayingFieldHandler(IDraggable draggable)
    {
        OnSpawnPopupTextEvent?.Invoke(localizationProxy.GetLocalization("notAPlayingField"));
    }
}