using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetPackCreator
{
    public class Asset
    {
        public string prefabName;
        public string thumbnailExt;

        public bool HasThumbnail()
        {
            return !string.IsNullOrEmpty(thumbnailExt);
        }

        public void UpdateThumbnailInPrefab()
        {
            /*MessageBox.Show(prefabPath);
            string text = "";
            using (StreamReader sr = new StreamReader(prefabPath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var prefix = "\"m_Icon\": ";
                    var suffix = "\",\n";
                    if (line.Contains(prefix) && line.Contains(suffix))
                    {
                        // Replace text between prefix and suffix by thumbnailPath
                        var start = line.IndexOf(prefix) + prefix.Length;
                        var end = line.IndexOf(suffix, start);
                        line = line.Substring(0, start) + thumbnailPath + line.Substring(end);
                    }
                    text += line + "\n";
                }
            }

            using (StreamWriter sw = new StreamWriter(prefabPath + ".copy.Prefab"))
            {
                sw.Write(text);
            }*/
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
