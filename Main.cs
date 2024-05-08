using System.Diagnostics;
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
        public static Localization localization;
        public Main()
        {
            InitializeComponent();
            Instance = this;
            settings = Settings.Load();
            publishConfig = PublishConfig.Load();
            localization = new Localization();
        }

        private AssetPack pack;

        public static void UpdateStatus(string text)
        {
            Instance.statusLabel.Text = text;
            Thread.Sleep(delay);
        }

        private void Main_Load(object sender, EventArgs e)
        {
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

            lbAssets.DisplayMember = "displayText";
            lbAssets.DataSource = pack.assets;
            selectAssetsDialog.InitialDirectory = $@"C:\Users\{Environment.UserName}\AppData\LocalLow\Colossal Order\Cities Skylines II\StreamingAssets~";


            comboLocale.DisplayMember = "Id";
            comboLocale.DataSource = localization.Locales;
            comboLocale.SelectedItem = localization.GetLocale("en-US");

            // Load Settings
            txtCities2Location.Text = settings.Cities2Path;
            txtPdxMail.Text = settings.PdxMail;
            txtPdxPw.Text = settings.PdxPassword;
            cbSavePassword.Checked = settings.SavePassword;
            cbOpenModPageAfterUpdate.Checked = settings.OpenModPageAfterUpdate;

            // Load Publish Configuration
            txtPublishDisplayName.DataBindings.Add("Text", publishConfig, "DisplayName", false, DataSourceUpdateMode.OnPropertyChanged);
            txtPublishShortDescription.DataBindings.Add("Text", publishConfig, "ShortDescription", false, DataSourceUpdateMode.OnPropertyChanged);
            txtPublishLongDescription.DataBindings.Add("Text", publishConfig, "LongDescription", false, DataSourceUpdateMode.OnPropertyChanged);
            txtPublishChangeLog.DataBindings.Add("Text", publishConfig, "ChangeLog", false, DataSourceUpdateMode.OnPropertyChanged);
            txtPublishModId.DataBindings.Add("Text", publishConfig, "ModId", false, DataSourceUpdateMode.OnPropertyChanged);

            txtPublishGameVersion.DataBindings.Add("Text", publishConfig, "GameVersion", false, DataSourceUpdateMode.OnPropertyChanged);
            txtPublishModVersion.DataBindings.Add("Text", publishConfig, "ModVersion", false, DataSourceUpdateMode.OnPropertyChanged);
            packThumbnailBox.ImageLocation = publishConfig.ThumbnailPath;
        }

        private void cmdRenameProject_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtProjectName.Text) || txtProjectName.Text.Contains(' ') || !txtProjectName.Text.ToLower().EndsWith("assetpack") || txtProjectName.Text == "CustomAssetPack")
            {
                MessageBox.Show("Please enter a name that ends with 'AssetPack' and does not contain any spaces or other characters", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string oldSolutionName = pack.name;
            pack.Rename(txtProjectName.Text);
            string newSolutionName = txtProjectName.Text;


            if (MessageBox.Show($"Detected Solution file: {oldSolutionName}.sln. Would you like to continue renaming it to {newSolutionName}.sln?", "Info", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information) != DialogResult.Yes)
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
            Asset? selectedAsset = (Asset?)lbAssets.SelectedItem;
            Locale? selectedLocale = (Locale?)comboLocale.SelectedItem;
            if (selectedAsset == null)
            {
                cmdAddThumbnail.Enabled = false;
                cmdRemoveSelectedAsset.Enabled = false;
                groupAssetLocatization.Enabled = false;
                return;
            }
            cmdRemoveSelectedAsset.Enabled = true;
            cmdAddThumbnail.Enabled = true;
            txtPrefabName.Text = selectedAsset.prefabName;
            groupAssetLocatization.Enabled = true;


            // Update Combo Locale
            comboLocale_SelectedIndexChanged(sender, e);


            if (selectedAsset.HasThumbnail())
            {
                assetThumbnailBox.ImageLocation = selectedAsset.thumbnailPath;
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
            Asset? selected = (Asset?)lbAssets.SelectedItem;
            if (selected == null)
                return;
            pack.RemoveAsset(selected);
        }

        private void cmdAddThumbnail_Click(object sender, EventArgs e)
        {
            Asset? selected = (Asset?)lbAssets.SelectedItem;
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
            localization.RenamePrefab(selected.prefabName, txtPrefabName.Text);
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
            Process.Start("explorer.exe", Directory.GetCurrentDirectory());
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

        private void PackThumbnailBoxClick(object sender, EventArgs e)
        {
            if (addThumbnailDialog.ShowDialog() != DialogResult.OK)
                return;
            if (!string.IsNullOrEmpty(addThumbnailDialog.FileName) && File.Exists(addThumbnailDialog.FileName))
            {
                // Check file size
                FileInfo fi = new FileInfo(addThumbnailDialog.FileName);
                if (fi.Length > 2048 * 1024)
                {
                    MessageBox.Show("File size must not exceed 2MB", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
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

        private void txtPublishChangeLog_TextChanged(object sender, EventArgs e)
        {
            var md = txtPublishChangeLog.Text;
            markdownPreviewHtml = Markdig.Markdown.ToHtml(md);
        }

        public static string markdownPreviewHtml;
        private void CmdMarkdownPreviewClick(object sender, EventArgs e)
        {
            MarkdownPreview preview = new MarkdownPreview();
            preview.Show();
        }

        private void CreatePdxAccountFile()
        {
            UpdateStatus("Creating PDX Account File");
            var pdxAccountFile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "/pdx_account.txt";
            if (!File.Exists(pdxAccountFile))
            {
                FileStream fs;
                fs = File.Create(pdxAccountFile);
                fs.Close();
            }
            File.WriteAllText(pdxAccountFile, settings.PdxMail + "\n" + settings.PdxPassword);
            UpdateStatus("PDX Account File created");
        }

        private void cmdPublishNewMod_Click(object sender, EventArgs e)
        {
            //var localModPath = Path.Combine("C:\\Users", Environment.UserName, "AppData", "LocalLow", "Colossal Order", "Cities Skylines II", "Mods", pack.name);
            //var publishConfigPath = Path.Combine(Directory.GetCurrentDirectory(), "Properties", "PublishConfiguration.xml");
            //Publisher.PublishNewMod(settings.Cities2Path, publishConfigPath, settings.PdxMail, settings.PdxPassword, localModPath);
            CreatePdxAccountFile();
            UseWaitCursor = true;
            var result = Publisher.PublishNewMod(Directory.GetCurrentDirectory(), out int modId);
            UseWaitCursor = false;
            if (result == PublishResult.Success)
            {
                txtPublishModId.Text = modId.ToString();
                if (settings.OpenModPageAfterUpdate)
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = $"https://mods.paradoxplaza.com/mods/{txtPublishModId.Text}/Windows",
                        UseShellExecute = true
                    });
                }
            }
        }

        private void cmdPublishNewVersion_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPublishChangeLog.Text))
            {
                MessageBox.Show("Please enter a changelog", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            CreatePdxAccountFile();
            UseWaitCursor = true;
            var result = Publisher.PublishNewVersion(Directory.GetCurrentDirectory());
            UseWaitCursor = false;
            if (result == PublishResult.Success && settings.OpenModPageAfterUpdate)
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = $"https://mods.paradoxplaza.com/mods/{txtPublishModId.Text}/Windows",
                    UseShellExecute = true
                });
            }
        }

        private void cmdUpdatePublishedConfiguration_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPublishChangeLog.Text))
            {
                MessageBox.Show("Please enter a changelog", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            CreatePdxAccountFile();
            UseWaitCursor = true;
            var result = Publisher.UpdatePublishedConfiguration(Directory.GetCurrentDirectory());
            UseWaitCursor = false;
            if (result == PublishResult.Success && settings.OpenModPageAfterUpdate)
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = $"https://mods.paradoxplaza.com/mods/{txtPublishModId.Text}/Windows",
                    UseShellExecute = true
                });
            }
        }

        private void tabPrepare_Click(object sender, EventArgs e)
        {
        }

        private void comboLocale_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedAsset = (Asset)lbAssets.SelectedItem;
            if (selectedAsset == null)
                return;
            var selectedLocale = (Locale)comboLocale.SelectedItem;
            if (selectedLocale == null)
            {
                txtLocalizedName.Enabled = txtLocalizedDescription.Enabled = false;
                txtLocalizedName.Text = "";
                txtLocalizedDescription.Text = "";
            }
            else
            {
                txtLocalizedName.Enabled = txtLocalizedDescription.Enabled = true;
                txtLocalizedName.Text = selectedLocale.GetAssetName(selectedAsset);
                txtLocalizedDescription.Text = selectedLocale.GetAssetDescription(selectedAsset);
            }
        }

        private void txtLocalizedName_TextChanged(object sender, EventArgs e)
        {
            var selectedLocale = (Locale)comboLocale.SelectedItem;
            var selectedAsset = (Asset)lbAssets.SelectedItem;
            if (selectedLocale == null || selectedAsset == null)
                return;

            selectedLocale.SetAssetName(selectedAsset, txtLocalizedName.Text);
        }

        private void txtLocalizedDescription_TextChanged(object sender, EventArgs e)
        {
            var selectedLocale = (Locale)comboLocale.SelectedItem;
            var selectedAsset = (Asset)lbAssets.SelectedItem;
            if (selectedLocale == null || selectedAsset == null)
                return;

            selectedLocale.SetAssetDescription(selectedAsset, txtLocalizedDescription.Text);
        }

        private void cbOpenModPageAfterUpdate_CheckedChanged(object sender, EventArgs e)
        {
            settings.OpenModPageAfterUpdate = cbOpenModPageAfterUpdate.Checked;
        }
    }
}
