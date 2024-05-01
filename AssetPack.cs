using System.ComponentModel;

namespace AssetPackCreator;

    public class AssetPack
    {
        private static readonly string[] supportedThumbnailExtensions = new string[] { ".png", ".svg" };
        public BindingList<Asset> assets = new();

        public string name;
        public DirectoryInfo baseDirectory;

        public AssetPack(string name, string baseDirectoryPath)
        {
            this.name = name;
            baseDirectory = new DirectoryInfo(baseDirectoryPath);
            if (!baseDirectory.Exists)
                baseDirectory.Create();
        }

        public void Rename(string newName)
        {
            if (name == newName)
                return;
            name = newName;
            Directory.Move(baseDirectory.FullName, Path.Combine(baseDirectory.Parent.FullName, newName));
        }

        public void AddAsset(string path)
        {
            string prefabName = Path.GetFileNameWithoutExtension(path);
            string prefabDir = Path.Combine(baseDirectory.FullName, prefabName);
            foreach(Asset a in assets)
            {
                if (a.prefabName == prefabName)
                {
                    MessageBox.Show("Asset already exists in the pack", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (!Directory.Exists(prefabDir))
                Directory.CreateDirectory(prefabDir);
            File.Copy(path, Path.Combine(prefabDir, prefabName + ".Prefab"), true);
            File.Copy(path + ".cid", Path.Combine(prefabDir, prefabName + ".Prefab.cid"), true);
            Asset asset = new Asset()
            {
                prefabName = prefabName,
                thumbnailExt = "",
            };
            assets.Add(asset);
        }

    }

