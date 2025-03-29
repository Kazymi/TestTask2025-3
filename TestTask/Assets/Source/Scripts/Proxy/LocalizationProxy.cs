using System.Collections;
using UnityEngine;
using Zenject;

public class LocalizationProxy
{
    [Inject] private LocalizationLibrary localizationLibrary;

    public string GetLocalization(string key)
    {
        foreach (var localization in localizationLibrary.Localizations)
        {
            if (localization.LocationID == key)
            {
                foreach (var localizationConfiguration in localization.LocalizationConfigurations)
                {
                    //TODO Here you need to check the selected language
                    //  if (SaveData.LocalizationType == localizationConfiguration.LocalizationType)
                    return localizationConfiguration.Text;
                }
            }
        }

        Debug.LogError($"Failed, not found, localization needed, {key}");
        return "Localization error";
    }
}