using System;
using Zenject;

public class PopupTextSpawnerMediator : IInitializable
{
    [Inject] private Pool<PopupText> popupTextPool;
    [Inject] private PopupTextSpawnerProxy popupTextSpawnerProxy;

    public event Action<PopupText> OnPopupTextSpawnedEvent;

    public void Initialize()
    {
        popupTextSpawnerProxy.OnSpawnPopupTextEvent += OnSpawnPopupTextHandler;
    }

    private void OnSpawnPopupTextHandler(string localizatedText)
    {
        var newText = popupTextPool.Pull();
        newText.Initialize(localizatedText);
        OnPopupTextSpawnedEvent?.Invoke(newText);
    }
}