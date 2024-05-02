using System.Web;
// ReSharper disable LocalizableElement

namespace AssetPackCreator
{
    public partial class Main : Form
    {
        public static Main Instance;
        public static int delay = 100;
        public static Settings settings;
        public Main()
        {
            InitializeComponent();
            Instance = this;
            settings = Settings.Load();
        }

        private AssetPack pack;

        public static void UpdateStatus(string text)
        {
            Instance.statusLabel.Text = text;
            Thread.Sleep(delay);
        }

        private void Main_Load(object sender, EventArgs e)
        {
            ChooseStep(1);
            // Look for sln file in current folder
            foreach (string file in Directory.GetFiles(Directory.GetCurrentDirectory()))
            {
                if (file.EndsWith(".sln"))
                {
                    txtProjectName.Text = Path.GetFileNameWithoutExtension(file);
                    break;
                }
            }
            var assetsDir = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "assets", txtProjectName.Text);

            if (Directory.Exists(assetsDir))
                pack = AssetPack.Load(txtProjectName.Text, assetsDir);
            else
                pack = AssetPack.New(txtProjectName.Text, assetsDir);

            lbAssets.DataSource = pack.assets;
            lbAssets.DisplayMember = "displayText";
            selectAssetsDialog.InitialDirectory = $@"C:\Users\{Environment.UserName}\AppData\LocalLow\Colossal Order\Cities Skylines II\StreamingAssets~";

            // Load Settings
            txtCities2Location.Text = settings.Cities2Path;
            txtPdxMail.Text = settings.PdxMail;
            txtPdxPw.Text = settings.PdxPassword;
            cbSavePassword.Checked = settings.SavePassword;
        }

        private void ChooseStep(int step)
        {
            switch (step)
            {
                case 1:
                    groupPrepare.Enabled = true;
                    groupRename.Enabled = false;
                    groupAddAssets.Enabled = false;
                    groupPDXCredentials.Enabled = false;
                    cmdStep1.Enabled = true;
                    break;
                case 2:
                    groupPrepare.Enabled = false;
                    groupRename.Enabled = true;
                    groupAddAssets.Enabled = false;
                    groupPDXCredentials.Enabled = false;
                    cmdStep1.Enabled = false;
                    break;
                case 3:
                    groupPrepare.Enabled = false;
                    groupRename.Enabled = false;
                    groupAddAssets.Enabled = true;
                    groupPDXCredentials.Enabled = false;
                    break;
                case 4:
                    groupPrepare.Enabled = false;
                    groupRename.Enabled = false;
                    groupAddAssets.Enabled = false;
                    groupPDXCredentials.Enabled = true;
                    break;
                default:
                    MessageBox.Show("?");
                    break;
            }
        }

        private void cmdRenameProject_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtProjectName.Text) || txtProjectName.Text.Contains(" ") || !txtProjectName.Text.ToLower().EndsWith("assetpack") || txtProjectName.Text == "CustomAssetPack")
            {
                MessageBox.Show("Please enter a name that ends with 'AssetPack' and does not contain any spaces or other characters", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string oldSolutionName = pack.name;
            pack.Rename(txtProjectName.Text);
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

        private void cmdBrowseAssets_Click(object sender, EventArgs e)
        {
            if (selectAssetsDialog.ShowDialog() != DialogResult.OK)
                return;
            foreach (string fileName in selectAssetsDialog.FileNames)
            {
                pack.AddAsset(fileName);
            }
        }


        private void lbAssets_SelectedIndexChanged(object sender, EventArgs e)
        {
            Asset? selected = (Asset)lbAssets.SelectedItem;
            if (selected == null)
            {
                cmdAddThumbnail.Enabled = false;
                cmdRemoveSelectedAsset.Enabled = false;
                return;
            }
            cmdRemoveSelectedAsset.Enabled = true;
            cmdAddThumbnail.Enabled = true;
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

        private void cmdRemoveSelectedAsset_Click(object sender, EventArgs e)
        {
            Asset? selected = (Asset)lbAssets.SelectedItem;
            pack.RemoveAsset(selected);
        }

        private void cmdAddThumbnail_Click(object sender, EventArgs e)
        {
            Asset? selected = (Asset)lbAssets.SelectedItem;
            if (selected == null)
                return;
            if (cmdAddThumbnail.Text == "Remove Thumbnail") // Remove Thumbnail
            {
                selected.DeleteThumbnail();
                selected.thumbnailExt = "";
                thumbnailBox.ImageLocation = selected.thumbnailPath;
            }
            else // Add Thumbnail
            {
                var x = addThumbnailDialog.ShowDialog();
                if (x == DialogResult.OK)
                {
                    if (!string.IsNullOrEmpty(addThumbnailDialog.FileName))
                    {
                        selected.AddThumbnail(addThumbnailDialog.FileName);
                        thumbnailBox.ImageLocation = selected.thumbnailPath;
                    }
                }
            }
            int i = lbAssets.SelectedIndex;
            lbAssets.SelectedIndex = -1;
            lbAssets.SelectedIndex = i;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void cmdStep2_Click(object sender, EventArgs e)
        {
            ChooseStep(2);
        }

        private void cmdStep3_Click(object sender, EventArgs e)
        {
            ChooseStep(3);
        }

        private void cmdApplyAssetName_Click(object sender, EventArgs e)
        {
            /*if (string.IsNullOrEmpty(txtPrefabName.Text) || txtPrefabName.Text.Length < 5)
            {
                MessageBox.Show("Please enter at least 5 characters", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Asset selected = addedAssets[lbAssets.SelectedIndex];

            string oldPrefabName = selected.prefabName;
            string oldDir = selected.dir;
            string oldThumbnailPath = selected.thumbnailPath;

            selected.prefabName = txtPrefabName.Text;

            string newDir = Path.Combine(Path.GetDirectoryName(oldDir), selected.prefabName);
            File.Move(Path.Combine(oldDir, oldPrefabName + ".Prefab"), Path.Combine(oldDir, selected.prefabName + ".Prefab"));
            File.Move(Path.Combine(oldDir, oldPrefabName + ".Prefab.cid"), Path.Combine(oldDir, selected.prefabName + ".Prefab.cid"));
            if (!string.IsNullOrEmpty(oldThumbnailPath))
            {
                File.Move(Path.Combine(oldThumbnailPath), Path.Combine(oldDir, Path.GetFileName(oldThumbnailPath)));
            }

            Directory.Move(oldDir, newDir);
            selected.dir = newDir;


            if (selected.HasThumbnail())
            {
                File.Move(selected.thumbnailPath, Path.Combine(newDir, Path.GetFileName(selected.thumbnailPath)));
                selected.thumbnailPath = Path.Combine(newDir, Path.GetFileName(selected.thumbnailPath));
                selected.UpdateThumbnailInPrefab();
            }*/

        }

        private void txtPrefabName_TextChanged(object sender, EventArgs e)
        {
            /*if (lbAssets.SelectedIndex < 0)
                return;
            Asset selected = addedAssets[lbAssets.SelectedIndex];
            cmdApplyAssetName.Enabled = txtPrefabName.Text != selected.prefabName;*/
        }

        private void txtProjectName_TextChanged(object sender, EventArgs e)
        {
            if (pack == null)
                return;
            cmdRenameProject.Enabled = txtProjectName.Text != pack.name;
        }

        private void cmdStep1_Click(object sender, EventArgs e)
        {
            ChooseStep(1);
        }

        private void cmdBrowseGamePath_Click(object sender, EventArgs e)
        {
            browseGamePathDialog.InitialDirectory = txtCities2Location.Text;
            if (browseGamePathDialog.ShowDialog() == DialogResult.OK)
            {
                txtCities2Location.Text = browseGamePathDialog.FileName;
            }
        }

        private void txtCities2Location_TextChanged(object sender, EventArgs e)
        {
            // Game path [...]\Cities Skylines II\Cities2.exe
            // GameDLL path [...]\Cities Skylines II\Cities2_Data\Managed\Game.dll
            try
            {
                var gameDir = Path.GetDirectoryName(txtCities2Location.Text);
                var gameDllPath = Path.Combine(gameDir, "Cities2_Data", "Managed", "Game.dll");
                if (File.Exists(gameDllPath))
                {
                    settings.Cities2Path = txtCities2Location.Text;
                    groupPrepare.Text = "Prepare \u2705";
                    ChooseStep(2);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void toolStripMenuButton_ButtonClick(object sender, EventArgs e)
        {

        }

        private void toolStripMenu_OpenAppDir_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", Directory.GetCurrentDirectory());
        }

        private void txtPdxMail_TextChanged(object sender, EventArgs e)
        {
            settings.PdxMail = txtPdxMail.Text;
        }

        private void txtPdxPw_TextChanged(object sender, EventArgs e)
        {
            settings.PdxPassword = txtPdxPw.Text;
        }

        private void cbSavePassword_CheckedChanged(object sender, EventArgs e)
        {
            settings.SavePassword = cbSavePassword.Checked;
        }

        private void cmdStep4_Click(object sender, EventArgs e)
        {
            ChooseStep(4);
        }
    }
}
