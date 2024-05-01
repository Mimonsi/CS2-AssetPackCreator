using System.Web;

namespace AssetPackCreator
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        private Dictionary<int, Asset> addedAssets = new Dictionary<int, Asset>();
        private string assetPackName = "";
        //private string assetPackDirectory = @"C:\Users\Konsi\Documents\CS2-Modding\CS2-CustomAssetPack\CustomAssetPack";
        // current dir
        private string assetPackDirectory = Directory.GetCurrentDirectory();
        private void cmdRenameProject_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtProjectName.Text) || txtProjectName.Text.Contains(" ") || !txtProjectName.Text.ToLower().EndsWith("assetpack"))
            {
                MessageBox.Show("Please enter a name that ends with 'AssetPack' and does not contain any spaces or other characters", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string oldSolutionName = assetPackName;
            string newSolutionName = txtProjectName.Text;


            if (MessageBox.Show("Detected Solution file: " + oldSolutionName + ".sln. Would you like to continue renaming it to " + newSolutionName + ".sln?", "Info", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                File.Move($"{oldSolutionName}.sln", $"{newSolutionName}.sln");

                File.Move($"{oldSolutionName}.csproj", $"{newSolutionName}.csproj");

                string csprojContent = File.ReadAllText($"{newSolutionName}.sln");
                csprojContent = csprojContent.Replace(oldSolutionName, newSolutionName);
                File.WriteAllText($"{newSolutionName}.sln", csprojContent);

                MessageBox.Show("Solution successfully renamed.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error renaming solution: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Main_Load(object sender, EventArgs e)
        {
            groupRename.Enabled = false;
            groupAddAssets.Enabled = false;
            // Look for sln file in current folder
            foreach (string file in Directory.GetFiles(assetPackDirectory))
            {
                if (file.EndsWith(".sln"))
                {
                    assetPackName = Path.GetFileNameWithoutExtension(file);
                    break;
                }
            }
            txtProjectName.Text = assetPackName;
            selectAssetsDialog.InitialDirectory = $@"C:\Users\{Environment.UserName}\AppData\LocalLow\Colossal Order\Cities Skylines II\StreamingAssets~";

            cmdStep1_Click(sender, e);
        }

        private void cmdBrowseAssets_Click(object sender, EventArgs e)
        {
            if (selectAssetsDialog.ShowDialog() != DialogResult.OK)
                return;
            foreach (string s in selectAssetsDialog.FileNames)
            {

                // Copy files to local /Resource/assets folder
                //var baseAssetDir = @"C:\Users\Konsi\Documents\CS2-Modding\CS2-CustomAssetPack\CustomAssetPack";
                var baseAssetDir = Directory.GetCurrentDirectory();
                string dest = Path.Combine(baseAssetDir, "Resources", "assets", Path.GetFileNameWithoutExtension(s));

                string initialName = Path.GetFileNameWithoutExtension(s);
                if (!lbAssets.Items.Contains(initialName))
                {
                    if (!Directory.Exists(dest))
                        Directory.CreateDirectory(dest);
                    File.Copy(s, Path.Combine(dest, Path.GetFileName(s)), true);
                    File.Copy(s + ".cid", Path.Combine(dest, Path.GetFileName(s) + ".cid"), true);
                    int index = lbAssets.Items.Add(initialName);
                    addedAssets.Add(index, new Asset()
                    {
                        dir = dest,
                        prefabName = initialName
                    });
                }
            }
        }

        private void cmdRemoveSelectedAsset_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < addedAssets.Count; i++)
            {
                if (addedAssets[i].dir == lbAssets.SelectedItem.ToString())
                {
                    Directory.Delete(addedAssets[i].dir, true);
                    addedAssets.Remove(i);
                    lbAssets.Items.Remove(i);
                    break;
                }
            }
        }

        private void cmdAddThumbnail_Click(object sender, EventArgs e)
        {
            Asset selected = addedAssets[lbAssets.SelectedIndex];
            if (cmdAddThumbnail.Text == "Remove Thumbnail")
            {
                selected.thumbnailPath = "";
                thumbnailBox.ImageLocation = selected.thumbnailPath;
                selected.UpdateThumbnailInPrefab();
            }
            else
            {
                var x = addThumbnailDialog.ShowDialog();
                if (x == DialogResult.OK)
                {

                    if (!string.IsNullOrEmpty(addThumbnailDialog.FileName))
                    {
                        var baseAssetDir = Directory.GetCurrentDirectory();
                        string dest = Path.Combine(selected.dir, Path.GetFileName(addThumbnailDialog.FileName));
                        File.Copy(addThumbnailDialog.FileName, dest, true);
                        selected.thumbnailPath = dest;
                        thumbnailBox.ImageLocation = selected.thumbnailPath;
                        selected.UpdateThumbnailInPrefab();
                    }
                }
            }
            int i = lbAssets.SelectedIndex;
            lbAssets.SelectedIndex = -1;
            lbAssets.SelectedIndex = i;
        }


        private void lbAssets_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = lbAssets.SelectedIndex;
            if (index == -1)
            {
                cmdAddThumbnail.Enabled = false;
                cmdRemoveSelectedAsset.Enabled = false;
                return;
            }
            cmdRemoveSelectedAsset.Enabled = true;
            cmdAddThumbnail.Enabled = true;
            Asset selected = addedAssets[index];
            txtPrefabName.Text = selected.prefabName;
            if (selected.HasThumbnail())
            {
                thumbnailBox.ImageLocation = selected.thumbnailPath;
                cmdAddThumbnail.Text = "Remove Thumbnail";
            }
            else
            {
                thumbnailBox.ImageLocation = "";
                cmdAddThumbnail.Text = "Add Thumbnail";
            }

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void cmdStep1_Click(object sender, EventArgs e)
        {
            groupAddAssets.Enabled = false;
            groupRename.Enabled = true;
        }

        private void cmdStep2_Click(object sender, EventArgs e)
        {
            groupAddAssets.Enabled = true;
            groupRename.Enabled = false;
        }

        private void cmdApplyAssetName_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrEmpty(txtPrefabName.Text) || txtPrefabName.Text.Length < 5)
            {
                MessageBox.Show("Please enter at least 5 characters", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Asset selected = addedAssets[lbAssets.SelectedIndex];

            string oldPrefabName = selected.prefabName;
            string oldDir = selected.dir;
            selected.prefabName = txtPrefabName.Text;

            string newDir = Path.Combine(Path.GetDirectoryName(oldDir), selected.prefabName);
            Directory.Move(oldDir, newDir);
            selected.dir = newDir;

            File.Move(Path.Combine(newDir, oldPrefabName + ".Prefab"), Path.Combine(newDir, selected.prefabName + ".Prefab"));
            File.Move(Path.Combine(newDir, oldPrefabName + ".Prefab.cid"), Path.Combine(newDir, selected.prefabName + ".Prefab.cid"));

            selected.thumbnailPath = selected.thumbnailPath.Replace(oldDir, newDir);
            selected.UpdateThumbnailInPrefab();
        }

        private void txtPrefabName_TextChanged(object sender, EventArgs e)
        {
            if (lbAssets.SelectedIndex < 0)
                return;
            Asset selected = addedAssets[lbAssets.SelectedIndex];
            cmdApplyAssetName.Enabled = txtPrefabName.Text != selected.prefabName;
        }
    }
}
