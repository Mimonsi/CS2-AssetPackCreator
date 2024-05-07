using System.ComponentModel;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace AssetPackCreator;

public class Localization
{
    public readonly BindingList<Locale> Locales = [];

    public Localization()
    {
        string[] availableLocales =
        [
            "de-DE", "en-US", "es-ES", "fr-FR", "it-IT", "ja-JP", "ko-KR", "pl-PL", "pt-BR", "ru-RU", "zh-HANS",
            "zh-HANT"
        ];
        foreach(string s in availableLocales)
        {
            var l = new Locale(s);
            l.Load();
            Locales.Add(l);
        }
    }

    public Locale GetLocale(string id)
    {
        return Locales.FirstOrDefault(l => l.Id == id);
    }

    public void LoadAll()
    {
        foreach (var locale in Locales)
        {
            locale.Load();
        }
    }

    public void SaveAll()
    {
        foreach (var locale in Locales)
        {
            locale.Save();
        }
    }
}