using System.ComponentModel;
// ReSharper disable LocalizableElement

namespace AssetPackCreator;

    public class AssetPack
    {
        private static readonly string[] supportedThumbnailExtensions = new string[] { ".png", ".svg" };
        public BindingList<Asset> assets = new();

        public string name;
        public DirectoryInfo baseDirectory;

        public static AssetPack New(string name, string baseDirectoryPath)
        {
            Main.UpdateStatus("Creating Empty Asset Pack");
            return new AssetPack(name, baseDirectoryPath);
        }

        public static AssetPack Load(string name, string baseDirectoryPath)
        {
            Main.UpdateStatus("Loading existing Asset Pack");
            AssetPack pack = new AssetPack(name, baseDirectoryPath);
            try
            {
                foreach(DirectoryInfo assetDir in new DirectoryInfo(baseDirectoryPath).GetDirectories())
                {
                    Main.UpdateStatus($"Loading Asset: {assetDir.Name}");
                    Asset asset = new Asset
                    {
                        pack = pack,
                        prefabName = assetDir.Name,
                        thumbnailExt = "",
                    };
                    foreach(string ext in supportedThumbnailExtensions)
                    {
                        if (File.Exists(Path.Combine(assetDir.FullName, assetDir.Name + ext)))
                        {
                            asset.thumbnailExt = ext;
                            break;
                        }
                    }
                    asset.displayText = asset.HasThumbnail() ? $"\u2705 {asset.prefabName}" : $"\u274c {asset.prefabName}";
                    asset.UpdateThumbnailInPrefab();
                    asset.pack.assets.Add(asset);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Error loading existing Asset Pack: {e.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Main.UpdateStatus($"Loaded existing Asset Pack with {pack.assets.Count} assets");
            return pack;
        }

        private AssetPack(string name, string baseDirectoryPath)
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
            baseDirectory = new DirectoryInfo(Path.Combine(baseDirectory.Parent.FullName, newName));
            foreach(Asset a in assets)
            {
                a.UpdateThumbnailInPrefab();
            }
        }

        public void AddAsset(string path)
        {
            if (!File.Exists(path + ".cid"))
            {
                MessageBox.Show($"Asset .cid file is not at expected location: {path + ".cid"}. Asset was not added", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string prefabName = Path.GetFileNameWithoutExtension(path);
            string prefabDir = Path.Combine(baseDirectory.FullName, prefabName);
            foreach(Asset a in assets)
            {
                if (a.prefabName == prefabName)
                {
                    MessageBox.Show($"Asset {prefabName} already exists in the pack", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (!Directory.Exists(prefabDir))
                Directory.CreateDirectory(prefabDir);
            File.Copy(path, Path.Combine(prefabDir, prefabName + ".Prefab"), true);
            File.Copy(path + ".cid", Path.Combine(prefabDir, prefabName + ".Prefab.cid"), true);
            Asset asset = new Asset()
            {
                pack = this,
                prefabName = prefabName,
                thumbnailExt = "",
            };
            assets.Add(asset);
        }

        public void RemoveAsset(Asset asset)
        {
            Directory.Delete(Path.Combine(baseDirectory.FullName, asset.prefabName), true);
            assets.Remove(asset);
        }

        public bool ContainsAssetWithName(string prefabName)
        {
            foreach(Asset a in assets)
            {
                if (a.prefabName == prefabName)
                    return true;
            }
            return false;
        }

        public bool ContainsExampleAsset()
        {
            foreach(Asset a in assets)
            {
                if (a.prefabName == "CarProp")
                    return true;
            }
            return false;
        }
    }

