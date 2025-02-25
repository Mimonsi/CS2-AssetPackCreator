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

        public static void TriggerSavedText()
        {
            Instance.saveLabel.Text = "Changes saved";
            Instance.changesSavedTimer.Enabled = true;
            Instance.changesSavedTimer.Stop();
            Instance.changesSavedTimer.Start();
        }

        private void ExecuteInstructions()
        {
            try
            {
                var instructionsFile = Path.Combine(Directory.GetCurrentDirectory(), "ins.txt");
                if (File.Exists(instructionsFile))
                {
                    UpdateStatus("Executing Startup Instructions");
                    using StreamReader sr = new StreamReader("ins.txt");
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] parts = line.Split('=');
                        var command = parts[0];
                        var value = parts[1];
                        if (command == "RENAME")
                        {
                            txtProjectName.Text = value;
                            RenameProject(true);
                        }
                    }
                }

                File.Delete(instructionsFile);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Error executing instructions: {e.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

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

            lbImages.DataSource = publishConfig.Screenshots;

            // Load Settings
            txtCities2Location.Text = settings.Cities2Path;
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

            ExecuteInstructions();
        }

        private void RenameProject(bool skipConfirmation = false)
        {
            if (string.IsNullOrEmpty(txtProjectName.Text) || txtProjectName.Text.Contains(' ') || !txtProjectName.Text.ToLower().EndsWith("assetpack") || txtProjectName.Text == "CustomAssetPack")
            {
                MessageBox.Show("Please enter a name that ends with 'AssetPack' and does not contain any spaces or other characters", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string oldSolutionName = pack.name;
            pack.Rename(txtProjectName.Text);
            string newSolutionName = txtProjectName.Text;

            if (!skipConfirmation)
            {
                if (MessageBox.Show($"Detected Solution file: {oldSolutionName}.sln. Would you like to continue renaming it to {newSolutionName}.sln?", "Info", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information) != DialogResult.Yes)
                {
                    return;
                }
            }

            try
            {
                File.Move($"{oldSolutionName}.sln", $"{newSolutionName}.sln");

                File.Move($"{oldSolutionName}.csproj", $"{newSolutionName}.csproj");

                string csprojContent = File.ReadAllText($"{newSolutionName}.sln");
                csprojContent = csprojContent.Replace(oldSolutionName, newSolutionName);
                File.WriteAllText($"{newSolutionName}.sln", csprojContent);

                if (!skipConfirmation)
                    MessageBox.Show("Solution successfully renamed.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error renaming solution: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdRenameProject_Click(object sender, EventArgs e)
        {
            RenameProject();
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


        private void cmdBrowseAssetsFolder_Click(object sender, EventArgs e)
        {
            if (selectAssetsFolderDialog.ShowDialog() != DialogResult.OK)
                return;
            foreach (string fileName in Directory.GetFiles(selectAssetsFolderDialog.SelectedPath, "*.Prefab", SearchOption.AllDirectories))
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
            UpdateStatus("Asset name updated");
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
            UpdateStatus("Showing Game Path Dialog");
            browseGamePathDialog.InitialDirectory = txtCities2Location.Text;
            if (browseGamePathDialog.ShowDialog() == DialogResult.OK)
            {
                txtCities2Location.Text = browseGamePathDialog.FileName;
                UpdateStatus("Game Path updated");
            }
            else
            {
                UpdateStatus("Game Path not updated");
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

        private void CmdPublishConfigMarkdownPreviewClick(object sender, EventArgs e)
        {
            MarkdownPreview preview = new MarkdownPreview();
            markdownPreviewHtml = Markdig.Markdown.ToHtml(txtPublishLongDescription.Text);
            preview.Show();
        }

        private void CmdPublishMarkdownPreviewClick(object sender, EventArgs e)
        {
            MarkdownPreview preview = new MarkdownPreview();
            markdownPreviewHtml = Markdig.Markdown.ToHtml(txtPublishChangeLog.Text);
            preview.Show();
        }

        private bool CheckIsDifferentFromExample()
        {
            if (txtPublishLongDescription.Text.Contains("This asset packs adds xxx and yyy to the game"))
                return false;
            if (txtPublishDisplayName.Text.Contains("Example Pack") ||
                txtPublishDisplayName.Text.Contains("do not subscribe"))
                return false;
            if (txtPublishShortDescription.Text.Contains("Example Pack Description"))
                return false;
            if (pack.ContainsExampleAsset())
                return false;
            return true;
        }
        
        private bool CheckIfValidConfig()
        {
            if (string.IsNullOrEmpty(txtPublishDisplayName.Text))
                return false;
            if (string.IsNullOrEmpty(txtPublishShortDescription.Text))
                return false;
            if (string.IsNullOrEmpty(txtPublishLongDescription.Text))
                return false;
            return true;
        }

        private void cmdPublishNewMod_Click(object sender, EventArgs e)
        {
            if (!CheckIsDifferentFromExample())
            {
                MessageBox.Show("Please remove the Example Car Prop and enter real data for publishing", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!CheckIfValidConfig())
            {
                MessageBox.Show("Please enter valid data for publishing. Make sure all required fields have a value", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (publishConfig.ModId != "0")
            {
                publishConfig.ModId = "0";
                MessageBox.Show("Mod ID has been set to 0. This is required for new mods. The publishing will continue now.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            UseWaitCursor = true;
            var result = Publisher.PublishNewMod(Directory.GetCurrentDirectory(), out int modId);
            UpdateStatus("Waiting for PDX Cache to update...");
            int waited = 0;
            while (waited < 3000)
            {
                Application.DoEvents();
                Thread.Sleep(250);
                waited += 250;
            }
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
            if (!CheckIfValidConfig())
            {
                MessageBox.Show("Please enter valid data for publishing. Make sure all required fields have a value", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(txtPublishChangeLog.Text))
            {
                MessageBox.Show("Please enter a changelog", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            UseWaitCursor = true;
            var result = Publisher.PublishNewVersion(Directory.GetCurrentDirectory());
            UpdateStatus("Waiting for PDX Cache to update...");
            int waited = 0;
            while (waited < 3000)
            {
                Application.DoEvents();
                Thread.Sleep(250);
                waited += 250;
            }
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
            if (!CheckIfValidConfig())
            {
                MessageBox.Show("Please enter valid data for publishing. Make sure all required fields have a value", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(txtPublishChangeLog.Text))
            {
                MessageBox.Show("Please enter a changelog", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            UseWaitCursor = true;
            var result = Publisher.UpdatePublishedConfiguration(Directory.GetCurrentDirectory());
            UpdateStatus("Waiting for PDX Cache to update...");
            int waited = 0;
            while (waited < 3000)
            {
                Application.DoEvents();
                Thread.Sleep(250);
                waited += 250;
            }
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
        
        private void cmdBuildMod_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Function not fully implemented yet. Using workaround by publishing with invalid id. You may ignore all errors that follow.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            //var description = publishConfig.ShortDescription; 
            //publishConfig.ShortDescription = "";
            Publisher.BuildMod(Directory.GetCurrentDirectory());
            //publishConfig.ShortDescription = description;
            UpdateStatus("Mod built successfully");
            try
            {
                Process.Start($"C:/Users/{Environment.UserName}/AppData/LocalLow/Colossal Order/Cities Skylines II//Mods");
            }
            catch(Exception)
            {
                // Ignored
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
                var assetDisplayName = selectedLocale.GetAssetName(selectedAsset);
                if (string.IsNullOrEmpty(assetDisplayName))
                    assetDisplayName = selectedAsset.prefabName;
                txtLocalizedName.Text = assetDisplayName;
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

        private void label10_Click(object sender, EventArgs e)
        {
            PackThumbnailBoxClick(sender, e);
        }

        private void changesSavedTimer_Tick(object sender, EventArgs e)
        {
            saveLabel.Text = "";
            changesSavedTimer.Stop();
        }

        private void selectAssetsFolderDialog_HelpRequest(object sender, EventArgs e)
        {

        }

        private void lbAssets_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                // Check if folder
                if (Directory.Exists(file))
                {
                    foreach (string fileName in Directory.GetFiles(file, "*.Prefab", SearchOption.AllDirectories))
                    {
                        pack.AddAsset(fileName);
                    }
                }
                else
                {
                    if (file.EndsWith(".Prefab"))
                        pack.AddAsset(file);
                }
            }
        }

        private void lbAssets_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void lbImages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbImages.SelectedItem == null || string.IsNullOrEmpty(lbImages.SelectedItem.ToString()))
            {
                cmdRemoveImage.Enabled = false;
            }
            else
            {
                cmdRemoveImage.Enabled = true;
                imageBox.ImageLocation = Path.Combine(Directory.GetCurrentDirectory(), lbImages.SelectedItem.ToString()); ;
            }

        }

        private void cmdAddImage_Click(object sender, EventArgs e)
        {
            if (addThumbnailDialog.ShowDialog() != DialogResult.OK)
                return;
            if (!string.IsNullOrEmpty(addThumbnailDialog.FileName) && File.Exists(addThumbnailDialog.FileName))
            {
                FileInfo fi = new FileInfo(addThumbnailDialog.FileName);
                if (fi.Length > 2048 * 1024)
                {
                    MessageBox.Show("File size must not exceed 2MB", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var safeName = fi.Name.Replace(" ", "_");
                var copiedFile = Path.Combine(Directory.GetCurrentDirectory(), "Properties", safeName);
                File.Copy(fi.FullName, copiedFile, true);
                imageBox.ImageLocation = copiedFile;
                if (!publishConfig.Screenshots.Contains(Path.Combine("Properties", safeName)))
                    publishConfig.AddScreenshot(Path.Combine("Properties", safeName));
            }
        }

        private void cmdRemoveImage_Click(object sender, EventArgs e)
        {
            if (lbImages.SelectedIndex == -1)
                return;
            string path = Path.Combine(Directory.GetCurrentDirectory(), (string)lbImages.SelectedItem);
            publishConfig.RemoveScreenshot((string)lbImages.SelectedItem);
            try
            {
                File.Delete(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
