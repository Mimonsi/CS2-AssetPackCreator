using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetPackCreator
{
    internal class Asset
    {
        public Asset() { }
        public string dir;
        public string prefabName;
        public string thumbnailPath;

        public bool HasThumbnail() { return !string.IsNullOrEmpty(thumbnailPath); }

        public void UpdateThumbnailInPrefab()
        {
            MessageBox.Show(Path.Combine(Path.Combine(dir, prefabName), prefabName + ".Prefab"));
            using (StreamReader sr = new StreamReader(Path.Combine(Path.Combine(dir, prefabName), prefabName + ".Prefab")))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Contains("<thumbnail>"))
                    {
                        break;
                    }
                }
            }
        }
    }
}
