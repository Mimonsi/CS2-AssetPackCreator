using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetPackCreator
{
    public class Asset
    {
        public AssetPack pack;
        public string prefabName;
        public string thumbnailExt;

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
        }

        public void DeleteThumbnail()
        {
            if (HasThumbnail())
                File.Delete(thumbnailPath);
            UpdateThumbnailInPrefab();
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

            using (StreamWriter sw = new StreamWriter(prefabPath + ".copy.Prefab"))
            {
                sw.Write(text);
            }
        }

        public override string ToString()
        {
            if (HasThumbnail())
            {
                return $"\u2705 {prefabName}";
            }
            return $"\u274c {prefabName}";
        }
    }
}
