using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AssetPackCreator
{
    public class Asset : INotifyPropertyChanged
    {
        public AssetPack pack;
        public string prefabName;
        public string thumbnailExt;
        public string displayText;

        public string assetPath => $@"{pack.baseDirectory.FullName}\{prefabName}";
        public string prefabPath => $@"{assetPath}\{prefabName}.Prefab";
        public string thumbnailPath => $@"{assetPath}\{prefabName}{thumbnailExt}";

        public bool HasThumbnail()
        {
            return !string.IsNullOrEmpty(thumbnailExt) && File.Exists(thumbnailPath);
        }

        public void AddThumbnail(string fileName)
        {
            thumbnailExt = Path.GetExtension(fileName);
            File.Copy(fileName, $"{thumbnailPath}", true);
            UpdateThumbnailInPrefab();
            SetField(ref displayText, $"\u2705 {prefabName}");
        }

        public void DeleteThumbnail()
        {
            if (HasThumbnail())
                File.Delete(thumbnailPath);
            UpdateThumbnailInPrefab();
            SetField(ref displayText, $"\u274c {prefabName}");
        }

        public void Rename(string newPrefabName)
        {
            string newAssetPath = $@"{pack.baseDirectory.FullName}\{newPrefabName}";

            string oldPrefabName = new String(prefabName);

            // Asset directory
            Directory.Move(assetPath, newAssetPath);

            File.Move($@"{newAssetPath}\{oldPrefabName}.Prefab", $@"{newAssetPath}\{newPrefabName}.Prefab");
            File.Move($@"{newAssetPath}\{oldPrefabName}.Prefab.cid", $@"{newAssetPath}\{newPrefabName}.Prefab.cid");
            File.Move($@"{newAssetPath}\{oldPrefabName}{thumbnailExt}", $@"{newAssetPath}\{newPrefabName}{thumbnailExt}");

            SetField(ref prefabName, newPrefabName);
            UpdateNameInPrefab();


        }

        public string GetIconPath()
        {
            return $"coui://customassets/{pack.name}/{prefabName}/{prefabName}{thumbnailExt}";
        }

        public void UpdateThumbnailInPrefab()
        {
            string text = "";
            using (StreamReader sr = new StreamReader(prefabPath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var prefix = "\"m_Icon\": ";
                    var suffix = "\",";
                    if (line.Contains(prefix) && line.Contains(suffix))
                    {
                        // Replace text between prefix and suffix by thumbnailPath
                        var start = line.IndexOf(prefix) + prefix.Length;
                        var end = line.IndexOf(suffix, start);
                        line = line.Substring(0, start) + GetIconPath() + line.Substring(end);
                    }
                    text += line + "\n";
                }
            }

            using (StreamWriter sw = new StreamWriter(prefabPath))
            {
                sw.Write(text);
            }
        }

        public void UpdateNameInPrefab()
        {
            bool alreadyReplaced = false;
            string text = "";
            using (StreamReader sr = new StreamReader(prefabPath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var prefix = "\"name\": ";
                    var suffix = "\",";
                    if (!alreadyReplaced && line.Contains(prefix) && line.Contains(suffix))
                    {
                        // Replace text between prefix and suffix by prefabName
                        var start = line.IndexOf(prefix) + prefix.Length;
                        var end = line.IndexOf(suffix, start);
                        line = line.Substring(0, start) + prefabName + line.Substring(end);
                        alreadyReplaced = true;
                    }
                    text += line + "\n";
                }
            }

            using (StreamWriter sw = new StreamWriter(prefabPath))
            {
                sw.Write(text);
            }
        }

        public override string ToString()
        {
            if (HasThumbnail())
            {
                return $"✔️ {prefabName}";
            }
            return $"❌ {prefabName}";
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
