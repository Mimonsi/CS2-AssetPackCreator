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
        public string path;
        public string prefabName;
        public string thumbnailPath;

        public bool HasThumbnail() { return !string.IsNullOrEmpty(thumbnailPath); }
    }
}
