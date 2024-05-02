using System.Web;
// ReSharper disable LocalizableElement

namespace AssetPackCreator
{
    public partial class Main : Form
    {
        public static Main Instance;
        public static int delay = 100;
        public static Settings settings;
        public static PublishConfig publishConfig;
        public Main()
        {
            InitializeComponent();
            Instance = this;
            settings = Settings.Load();
            publishConfig = PublishConfig.Load();

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

            // Load Publish Configuration
            txtPublishDisplayName.DataBindings.Add("Text", publishConfig, "DisplayName", false, DataSourceUpdateMode.OnPropertyChanged);
            txtPublishShortDescription.DataBindings.Add("Text", publishConfig, "ShortDescription", false, DataSourceUpdateMode.OnPropertyChanged);
            txtPublishLongDescription.DataBindings.Add("Text", publishConfig, "LongDescription", false, DataSourceUpdateMode.OnPropertyChanged);
            packThumbnailBox.ImageLocation = publishConfig.ThumbnailPath;
        }

        private void DisableAllGroups()
        {
            groupPrepare.Enabled = false;
            groupRename.Enabled = false;
            groupAddAssets.Enabled = false;
            groupPDXCredentials.Enabled = false;
            cmdStep1.Enabled = false;
            groupPublishConfig.Enabled = false;
        }


        private void ChooseStep(int step)
        {
            DisableAllGroups();
            switch (step)
            {
                case 1:
                    groupPrepare.Enabled = true;
                    mainTabControl.SelectTab(0);
                    break;
                case 2:
                    groupRename.Enabled = true;
                    mainTabControl.SelectTab(0);
                    break;
                case 3:
                    groupAddAssets.Enabled = true;
                    mainTabControl.SelectTab(0);
                    break;
                case 4:
                    groupPDXCredentials.Enabled = true;
                    mainTabControl.SelectTab(0);
                    break;
                case 5:
                    groupPublishConfig.Enabled = true;
                    mainTabControl.SelectTab(1);
                    break;
                default:
                    MessageBox.Show("Unknown step", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                assetThumbnailBox.ImageLocation = selected.thumbnailPath;
                cmdAddThumbnail.Text = "Remove Thumbnail";
            }
            else
            {
                assetThumbnailBox.ImageLocation = "";
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
                assetThumbnailBox.ImageLocation = selected.thumbnailPath;
            }
            else // Add Thumbnail
            {
                var x = addThumbnailDialog.ShowDialog();
                if (x == DialogResult.OK)
                {
                    if (!string.IsNullOrEmpty(addThumbnailDialog.FileName))
                    {
                        selected.AddThumbnail(addThumbnailDialog.FileName);
                        assetThumbnailBox.ImageLocation = selected.thumbnailPath;
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
            Asset? selected = (Asset)lbAssets.SelectedItem;
            if (selected == null)
                return;
            if (string.IsNullOrEmpty(txtPrefabName.Text) || txtPrefabName.Text.Length < 5)
            {
                MessageBox.Show("Please enter at least 5 characters", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            selected.Rename(txtPrefabName.Text);
        }

        private void txtPrefabName_TextChanged(object sender, EventArgs e)
        {
            Asset? selected = (Asset)lbAssets.SelectedItem;
            if (selected == null)
                return;
            cmdApplyAssetName.Enabled = !pack.ContainsAssetWithName(txtPrefabName.Text);
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

        private void cmdStep5_Click(object sender, EventArgs e)
        {
            ChooseStep(5);
        }

        private void cmdPublishConfigSave_Click(object sender, EventArgs e)
        {

        }

        private void cmdPublishAddRemoveThumbnail_Click(object sender, EventArgs e)
        {

        }

        private void PackThumbnailBoxClick(object sender, EventArgs e)
        {
            if (addThumbnailDialog.ShowDialog() != DialogResult.OK)
                return;
            if (!string.IsNullOrEmpty(addThumbnailDialog.FileName) && File.Exists(addThumbnailDialog.FileName))
            {
                publishConfig.ChangeThumbnail(addThumbnailDialog.FileName);
                packThumbnailBox.ImageLocation = publishConfig.ThumbnailPath;

            }
        }

        private void assetThumbnailBox_Click(object sender, EventArgs e)
        {

        }

        private void txtPublishLongDescription_TextChanged(object sender, EventArgs e)
        {
            var md = txtPublishLongDescription.Text;
            markdownPreviewHtml = Markdig.Markdown.ToHtml(md);
        }

        public static string markdownPreviewHtml;
        private void CmdMarkdownPreviewClick(object sender, EventArgs e)
        {
            MarkdownPreview preview = new MarkdownPreview();
            preview.Show();
        }
    }
}
