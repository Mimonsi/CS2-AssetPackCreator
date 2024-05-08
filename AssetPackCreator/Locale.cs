using Newtonsoft.Json;

namespace AssetPackCreator;

public class Locale(string id)
{
    public string Id { get; set; } = id;
    private Dictionary<string, (string, string)> _strings = new();

    public string GetAssetName(Asset asset)
    {
        if (!_strings.ContainsKey(asset.prefabName))
            return "";
        return _strings[asset.prefabName].Item1;
    }

    public string GetAssetDescription(Asset asset)
    {
        if (!_strings.ContainsKey(asset.prefabName))
            return "";
        return _strings[asset.prefabName].Item2;
    }

    public void SetAssetName(Asset selectedAsset, string text)
    {
        if (!_strings.ContainsKey(selectedAsset.prefabName))
            _strings.Add(selectedAsset.prefabName, ("", ""));
        var x = _strings[selectedAsset.prefabName];
        x.Item1 = text;
        _strings[selectedAsset.prefabName] = x;
        Save();
    }

    public void SetAssetDescription(Asset selectedAsset, string text)
    {
        if (!_strings.ContainsKey(selectedAsset.prefabName))
            _strings.Add(selectedAsset.prefabName, ("", ""));
        var x = _strings[selectedAsset.prefabName];
        x.Item2 = text;
        _strings[selectedAsset.prefabName] = x;
        Save();
    }

    public void Load()
    {
        var file = $"Resources/lang/{Id}.json";
        if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), file)))
        {
            _strings = ParseFile(file);
        }
    }

    public void Save()
    {
        bool empty = true;
        var dict = new Dictionary<string, string>();
        foreach (var item in _strings)
        {
            if (!string.IsNullOrEmpty(item.Value.Item1) || !string.IsNullOrEmpty(item.Value.Item2))
                empty = false;

            if (string.IsNullOrEmpty(item.Value.Item1) && string.IsNullOrEmpty(item.Value.Item2))
                continue;
            dict.Add($"Assets.NAME[{item.Key}]", item.Value.Item1);
            dict.Add($"Assets.DESCRIPTION[{item.Key}]", item.Value.Item2);
        }

        // Don't save empty files
        if (empty)
            return;

        var json = JsonConvert.SerializeObject(dict, Formatting.Indented);
        File.WriteAllText($"Resources/lang/{Id}.json", json);
    }

    private static Dictionary<string, (string, string)> ParseFile(string file)
    {
        var dict = new Dictionary<string, (string, string)>();
        var json = File.ReadAllText(file);
        var data = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

        foreach (var item in data)
        {
            if (item.Key.StartsWith("Assets.NAME["))
            {
                var prefabName = item.Key.Replace("Assets.NAME[", "").Replace("]", "");
                if (!dict.ContainsKey(prefabName))
                    dict.Add(prefabName, ("", ""));
                var x = dict[prefabName];
                x.Item1 = item.Value;
                dict[prefabName] = x;
            }
            if (item.Key.StartsWith("Assets.DESCRIPTION["))
            {
                var prefabName = item.Key.Replace("Assets.DESCRIPTION[", "").Replace("]", "");
                if (!dict.ContainsKey(prefabName))
                    dict.Add(prefabName, ("", ""));
                var x = dict[prefabName];
                x.Item2 = item.Value;
                dict[prefabName] = x;
            }
        }
        return dict;
    }

    public void RenamePrefab(string oldName, string newName)
    {
        if (_strings.ContainsKey(oldName))
        {
            var x = _strings[oldName];
            _strings.Remove(oldName);
            _strings.Add(newName, x);
            Save();
        }
    }
}