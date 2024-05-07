namespace AssetPackCreator
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            cmdRenameProject = new Button();
            txtProjectName = new TextBox();
            label1 = new Label();
            groupRename = new GroupBox();
            groupAddAssets = new GroupBox();
            cmdApplyAssetName = new Button();
            cmdAddThumbnail = new Button();
            cmdRemoveSelectedAsset = new Button();
            assetThumbnailBox = new PictureBox();
            txtPrefabName = new TextBox();
            label3 = new Label();
            cmdBrowseAssets = new Button();
            lbAssets = new ListBox();
            label2 = new Label();
            selectAssetsDialog = new OpenFileDialog();
            addThumbnailDialog = new OpenFileDialog();
            statusStrip = new StatusStrip();
            toolStripMenuButton = new ToolStripSplitButton();
            toolStripMenu_OpenAppDir = new ToolStripMenuItem();
            statusLabel = new ToolStripStatusLabel();
            groupPrepare = new GroupBox();
            label4 = new Label();
            cmdBrowseGamePath = new Button();
            txtCities2Location = new TextBox();
            browseGamePathDialog = new OpenFileDialog();
            groupPDXCredentials = new GroupBox();
            cbSavePassword = new CheckBox();
            label6 = new Label();
            txtPdxPw = new TextBox();
            label5 = new Label();
            txtPdxMail = new TextBox();
            groupPublishConfig = new GroupBox();
            label11 = new Label();
            txtPublishModId = new TextBox();
            cmdMarkdownPreview = new Button();
            label10 = new Label();
            packThumbnailBox = new PictureBox();
            label9 = new Label();
            txtPublishLongDescription = new TextBox();
            label8 = new Label();
            txtPublishShortDescription = new TextBox();
            label7 = new Label();
            txtPublishDisplayName = new TextBox();
            mainTabControl = new TabControl();
            tabPrepare = new TabPage();
            tabAssets = new TabPage();
            groupAssetLocatization = new GroupBox();
            label14 = new Label();
            txtLocalizedDescription = new TextBox();
            label13 = new Label();
            txtLocalizedName = new TextBox();
            label12 = new Label();
            comboLocale = new ComboBox();
            tabPublishConfig = new TabPage();
            tabPublish = new TabPage();
            cmdUpdatePublishedConfiguration = new Button();
            cmdPublishNewMod = new Button();
            cmdPublishNewVersion = new Button();
            groupRename.SuspendLayout();
            groupAddAssets.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)assetThumbnailBox).BeginInit();
            statusStrip.SuspendLayout();
            groupPrepare.SuspendLayout();
            groupPDXCredentials.SuspendLayout();
            groupPublishConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)packThumbnailBox).BeginInit();
            mainTabControl.SuspendLayout();
            tabPrepare.SuspendLayout();
            tabAssets.SuspendLayout();
            groupAssetLocatization.SuspendLayout();
            tabPublishConfig.SuspendLayout();
            tabPublish.SuspendLayout();
            SuspendLayout();
            // 
            // cmdRenameProject
            // 
            cmdRenameProject.Enabled = false;
            cmdRenameProject.Location = new Point(6, 81);
            cmdRenameProject.Name = "cmdRenameProject";
            cmdRenameProject.Size = new Size(256, 34);
            cmdRenameProject.TabIndex = 0;
            cmdRenameProject.Text = "Apply Renaming";
            cmdRenameProject.UseVisualStyleBackColor = true;
            cmdRenameProject.Click += cmdRenameProject_Click;
            // 
            // txtProjectName
            // 
            txtProjectName.Location = new Point(6, 46);
            txtProjectName.Name = "txtProjectName";
            txtProjectName.Size = new Size(256, 29);
            txtProjectName.TabIndex = 1;
            txtProjectName.TextChanged += txtProjectName_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 19);
            label1.Name = "label1";
            label1.Size = new Size(256, 21);
            label1.TabIndex = 2;
            label1.Text = "Please enter the name of your pack:";
            // 
            // groupRename
            // 
            groupRename.Controls.Add(label1);
            groupRename.Controls.Add(cmdRenameProject);
            groupRename.Controls.Add(txtProjectName);
            groupRename.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            groupRename.Location = new Point(6, 99);
            groupRename.Name = "groupRename";
            groupRename.Size = new Size(279, 130);
            groupRename.TabIndex = 3;
            groupRename.TabStop = false;
            groupRename.Text = "Rename";
            // 
            // groupAddAssets
            // 
            groupAddAssets.Controls.Add(cmdApplyAssetName);
            groupAddAssets.Controls.Add(cmdAddThumbnail);
            groupAddAssets.Controls.Add(cmdRemoveSelectedAsset);
            groupAddAssets.Controls.Add(assetThumbnailBox);
            groupAddAssets.Controls.Add(txtPrefabName);
            groupAddAssets.Controls.Add(label3);
            groupAddAssets.Controls.Add(cmdBrowseAssets);
            groupAddAssets.Controls.Add(lbAssets);
            groupAddAssets.Controls.Add(label2);
            groupAddAssets.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            groupAddAssets.Location = new Point(3, 3);
            groupAddAssets.Name = "groupAddAssets";
            groupAddAssets.Size = new Size(431, 286);
            groupAddAssets.TabIndex = 4;
            groupAddAssets.TabStop = false;
            groupAddAssets.Text = "Add Assets";
            // 
            // cmdApplyAssetName
            // 
            cmdApplyAssetName.Enabled = false;
            cmdApplyAssetName.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmdApplyAssetName.Location = new Point(266, 157);
            cmdApplyAssetName.Name = "cmdApplyAssetName";
            cmdApplyAssetName.Size = new Size(152, 33);
            cmdApplyAssetName.TabIndex = 8;
            cmdApplyAssetName.Text = "Apply Name";
            cmdApplyAssetName.UseVisualStyleBackColor = true;
            cmdApplyAssetName.Click += cmdApplyAssetName_Click;
            // 
            // cmdAddThumbnail
            // 
            cmdAddThumbnail.Enabled = false;
            cmdAddThumbnail.Location = new Point(112, 237);
            cmdAddThumbnail.Name = "cmdAddThumbnail";
            cmdAddThumbnail.Size = new Size(182, 32);
            cmdAddThumbnail.TabIndex = 6;
            cmdAddThumbnail.Text = "Add Thumbnail";
            cmdAddThumbnail.UseVisualStyleBackColor = true;
            cmdAddThumbnail.Click += cmdAddThumbnail_Click;
            // 
            // cmdRemoveSelectedAsset
            // 
            cmdRemoveSelectedAsset.Enabled = false;
            cmdRemoveSelectedAsset.Location = new Point(300, 237);
            cmdRemoveSelectedAsset.Name = "cmdRemoveSelectedAsset";
            cmdRemoveSelectedAsset.Size = new Size(118, 32);
            cmdRemoveSelectedAsset.TabIndex = 5;
            cmdRemoveSelectedAsset.Text = "Delete Asset";
            cmdRemoveSelectedAsset.UseVisualStyleBackColor = true;
            cmdRemoveSelectedAsset.Click += cmdRemoveSelectedAsset_Click;
            // 
            // assetThumbnailBox
            // 
            assetThumbnailBox.Location = new Point(6, 169);
            assetThumbnailBox.Name = "assetThumbnailBox";
            assetThumbnailBox.Size = new Size(100, 100);
            assetThumbnailBox.SizeMode = PictureBoxSizeMode.Zoom;
            assetThumbnailBox.TabIndex = 7;
            assetThumbnailBox.TabStop = false;
            assetThumbnailBox.Click += assetThumbnailBox_Click;
            // 
            // txtPrefabName
            // 
            txtPrefabName.Location = new Point(112, 193);
            txtPrefabName.Name = "txtPrefabName";
            txtPrefabName.Size = new Size(306, 29);
            txtPrefabName.TabIndex = 7;
            txtPrefabName.TextChanged += txtPrefabName_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(112, 169);
            label3.Name = "label3";
            label3.Size = new Size(108, 21);
            label3.TabIndex = 5;
            label3.Text = "Prefab Name: ";
            label3.Click += label3_Click;
            // 
            // cmdBrowseAssets
            // 
            cmdBrowseAssets.Location = new Point(317, 19);
            cmdBrowseAssets.Name = "cmdBrowseAssets";
            cmdBrowseAssets.Size = new Size(93, 32);
            cmdBrowseAssets.TabIndex = 4;
            cmdBrowseAssets.Text = "Browse";
            cmdBrowseAssets.UseVisualStyleBackColor = true;
            cmdBrowseAssets.Click += cmdBrowseAssets_Click;
            // 
            // lbAssets
            // 
            lbAssets.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbAssets.FormattingEnabled = true;
            lbAssets.HorizontalScrollbar = true;
            lbAssets.ItemHeight = 15;
            lbAssets.Location = new Point(6, 57);
            lbAssets.Name = "lbAssets";
            lbAssets.Size = new Size(404, 94);
            lbAssets.TabIndex = 3;
            lbAssets.SelectedIndexChanged += lbAssets_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 19);
            label2.Name = "label2";
            label2.Size = new Size(287, 21);
            label2.TabIndex = 2;
            label2.Text = "Please select asset files you want to add:";
            // 
            // selectAssetsDialog
            // 
            selectAssetsDialog.DefaultExt = "Prefab";
            selectAssetsDialog.FileName = "NiceAsset.Prefab";
            selectAssetsDialog.Filter = "Asset Prefabs | *.Prefab";
            selectAssetsDialog.Multiselect = true;
            // 
            // addThumbnailDialog
            // 
            addThumbnailDialog.FileName = "Thumbnail";
            addThumbnailDialog.Filter = "PNG Files | *.png|Scaled Vector Graphics | *.svg";
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { toolStripMenuButton, statusLabel });
            statusStrip.Location = new Point(0, 462);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(763, 22);
            statusStrip.TabIndex = 5;
            statusStrip.Text = "statusStrip1";
            // 
            // toolStripMenuButton
            // 
            toolStripMenuButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripMenuButton.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenu_OpenAppDir });
            toolStripMenuButton.Image = (Image)resources.GetObject("toolStripMenuButton.Image");
            toolStripMenuButton.ImageTransparentColor = Color.Magenta;
            toolStripMenuButton.Name = "toolStripMenuButton";
            toolStripMenuButton.Size = new Size(32, 20);
            toolStripMenuButton.Text = "Menu";
            toolStripMenuButton.ToolTipText = "Open Menu with Options";
            toolStripMenuButton.ButtonClick += toolStripMenuButton_ButtonClick;
            // 
            // toolStripMenu_OpenAppDir
            // 
            toolStripMenu_OpenAppDir.Name = "toolStripMenu_OpenAppDir";
            toolStripMenu_OpenAppDir.Size = new Size(218, 22);
            toolStripMenu_OpenAppDir.Text = "Open Application Directory";
            toolStripMenu_OpenAppDir.Click += toolStripMenu_OpenAppDir_Click;
            // 
            // statusLabel
            // 
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(39, 17);
            statusLabel.Text = "Ready";
            // 
            // groupPrepare
            // 
            groupPrepare.Controls.Add(label4);
            groupPrepare.Controls.Add(cmdBrowseGamePath);
            groupPrepare.Controls.Add(txtCities2Location);
            groupPrepare.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            groupPrepare.Location = new Point(3, 3);
            groupPrepare.Name = "groupPrepare";
            groupPrepare.Size = new Size(725, 90);
            groupPrepare.TabIndex = 4;
            groupPrepare.TabStop = false;
            groupPrepare.Text = "Prepare";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 19);
            label4.Name = "label4";
            label4.Size = new Size(276, 21);
            label4.TabIndex = 2;
            label4.Text = "Please enter Cities Skylines 2 Location:";
            // 
            // cmdBrowseGamePath
            // 
            cmdBrowseGamePath.Location = new Point(560, 46);
            cmdBrowseGamePath.Name = "cmdBrowseGamePath";
            cmdBrowseGamePath.Size = new Size(152, 29);
            cmdBrowseGamePath.TabIndex = 0;
            cmdBrowseGamePath.Text = "Browse";
            cmdBrowseGamePath.UseVisualStyleBackColor = true;
            cmdBrowseGamePath.Click += cmdBrowseGamePath_Click;
            // 
            // txtCities2Location
            // 
            txtCities2Location.Location = new Point(6, 46);
            txtCities2Location.Name = "txtCities2Location";
            txtCities2Location.Size = new Size(554, 29);
            txtCities2Location.TabIndex = 1;
            txtCities2Location.TextChanged += txtCities2Location_TextChanged;
            // 
            // browseGamePathDialog
            // 
            browseGamePathDialog.FileName = "Cities2.exe";
            browseGamePathDialog.Filter = "Cities Skylines 2 | Cities2.exe";
            // 
            // groupPDXCredentials
            // 
            groupPDXCredentials.Controls.Add(cbSavePassword);
            groupPDXCredentials.Controls.Add(label6);
            groupPDXCredentials.Controls.Add(txtPdxPw);
            groupPDXCredentials.Controls.Add(label5);
            groupPDXCredentials.Controls.Add(txtPdxMail);
            groupPDXCredentials.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            groupPDXCredentials.Location = new Point(6, 6);
            groupPDXCredentials.Name = "groupPDXCredentials";
            groupPDXCredentials.Size = new Size(279, 147);
            groupPDXCredentials.TabIndex = 4;
            groupPDXCredentials.TabStop = false;
            groupPDXCredentials.Text = "Paradox Credentials";
            // 
            // cbSavePassword
            // 
            cbSavePassword.AutoSize = true;
            cbSavePassword.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cbSavePassword.Location = new Point(164, 75);
            cbSavePassword.Name = "cbSavePassword";
            cbSavePassword.Size = new Size(115, 21);
            cbSavePassword.TabIndex = 6;
            cbSavePassword.Text = "Save password";
            cbSavePassword.UseVisualStyleBackColor = true;
            cbSavePassword.CheckedChanged += cbSavePassword_CheckedChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(6, 75);
            label6.Name = "label6";
            label6.Size = new Size(157, 17);
            label6.TabIndex = 5;
            label6.Text = "Paradox Mods Password:";
            // 
            // txtPdxPw
            // 
            txtPdxPw.Location = new Point(6, 95);
            txtPdxPw.Name = "txtPdxPw";
            txtPdxPw.Size = new Size(264, 29);
            txtPdxPw.TabIndex = 4;
            txtPdxPw.UseSystemPasswordChar = true;
            txtPdxPw.TextChanged += txtPdxPw_TextChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(3, 23);
            label5.Name = "label5";
            label5.Size = new Size(190, 17);
            label5.TabIndex = 3;
            label5.Text = "Paradox Mods E-Mail Address:";
            // 
            // txtPdxMail
            // 
            txtPdxMail.Location = new Point(6, 43);
            txtPdxMail.Name = "txtPdxMail";
            txtPdxMail.Size = new Size(264, 29);
            txtPdxMail.TabIndex = 0;
            txtPdxMail.TextChanged += txtPdxMail_TextChanged;
            // 
            // groupPublishConfig
            // 
            groupPublishConfig.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupPublishConfig.Controls.Add(label11);
            groupPublishConfig.Controls.Add(txtPublishModId);
            groupPublishConfig.Controls.Add(cmdMarkdownPreview);
            groupPublishConfig.Controls.Add(label10);
            groupPublishConfig.Controls.Add(packThumbnailBox);
            groupPublishConfig.Controls.Add(label9);
            groupPublishConfig.Controls.Add(txtPublishLongDescription);
            groupPublishConfig.Controls.Add(label8);
            groupPublishConfig.Controls.Add(txtPublishShortDescription);
            groupPublishConfig.Controls.Add(label7);
            groupPublishConfig.Controls.Add(txtPublishDisplayName);
            groupPublishConfig.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            groupPublishConfig.Location = new Point(0, 0);
            groupPublishConfig.Margin = new Padding(0);
            groupPublishConfig.Name = "groupPublishConfig";
            groupPublishConfig.Size = new Size(733, 416);
            groupPublishConfig.TabIndex = 4;
            groupPublishConfig.TabStop = false;
            groupPublishConfig.Text = "Publish Configuration (will be displayed in PDX Mods)";
            // 
            // label11
            // 
            label11.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label11.Location = new Point(9, 383);
            label11.Name = "label11";
            label11.Size = new Size(66, 21);
            label11.TabIndex = 11;
            label11.Text = "Mod ID:";
            // 
            // txtPublishModId
            // 
            txtPublishModId.Location = new Point(81, 377);
            txtPublishModId.Name = "txtPublishModId";
            txtPublishModId.Size = new Size(181, 29);
            txtPublishModId.TabIndex = 10;
            // 
            // cmdMarkdownPreview
            // 
            cmdMarkdownPreview.Location = new Point(637, 88);
            cmdMarkdownPreview.Name = "cmdMarkdownPreview";
            cmdMarkdownPreview.Size = new Size(75, 29);
            cmdMarkdownPreview.TabIndex = 9;
            cmdMarkdownPreview.Text = "Preview";
            cmdMarkdownPreview.UseVisualStyleBackColor = true;
            cmdMarkdownPreview.Click += CmdMarkdownPreviewClick;
            // 
            // label10
            // 
            label10.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label10.Location = new Point(6, 95);
            label10.Name = "label10";
            label10.Size = new Size(250, 21);
            label10.TabIndex = 8;
            label10.Text = "Asset Pack Thumbnail: (Click to Change)";
            // 
            // packThumbnailBox
            // 
            packThumbnailBox.Location = new Point(9, 119);
            packThumbnailBox.Name = "packThumbnailBox";
            packThumbnailBox.Size = new Size(250, 250);
            packThumbnailBox.SizeMode = PictureBoxSizeMode.Zoom;
            packThumbnailBox.TabIndex = 7;
            packThumbnailBox.TabStop = false;
            packThumbnailBox.Click += PackThumbnailBoxClick;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(281, 91);
            label9.Name = "label9";
            label9.Size = new Size(363, 21);
            label9.TabIndex = 6;
            label9.Text = "Asset Pack Long Description: (Supports Markdown)";
            // 
            // txtPublishLongDescription
            // 
            txtPublishLongDescription.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            txtPublishLongDescription.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPublishLongDescription.Location = new Point(281, 115);
            txtPublishLongDescription.Multiline = true;
            txtPublishLongDescription.Name = "txtPublishLongDescription";
            txtPublishLongDescription.Size = new Size(431, 295);
            txtPublishLongDescription.TabIndex = 5;
            txtPublishLongDescription.TextChanged += txtPublishLongDescription_TextChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(278, 22);
            label8.Name = "label8";
            label8.Size = new Size(210, 21);
            label8.TabIndex = 4;
            label8.Text = "Asset Pack Short Description:";
            // 
            // txtPublishShortDescription
            // 
            txtPublishShortDescription.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPublishShortDescription.Location = new Point(281, 46);
            txtPublishShortDescription.Name = "txtPublishShortDescription";
            txtPublishShortDescription.Size = new Size(431, 25);
            txtPublishShortDescription.TabIndex = 3;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(3, 22);
            label7.Name = "label7";
            label7.Size = new Size(186, 21);
            label7.TabIndex = 2;
            label7.Text = "Asset Pack Display Name:";
            // 
            // txtPublishDisplayName
            // 
            txtPublishDisplayName.Location = new Point(9, 46);
            txtPublishDisplayName.Name = "txtPublishDisplayName";
            txtPublishDisplayName.Size = new Size(256, 29);
            txtPublishDisplayName.TabIndex = 1;
            // 
            // mainTabControl
            // 
            mainTabControl.Controls.Add(tabPrepare);
            mainTabControl.Controls.Add(tabAssets);
            mainTabControl.Controls.Add(tabPublishConfig);
            mainTabControl.Controls.Add(tabPublish);
            mainTabControl.Location = new Point(12, 12);
            mainTabControl.Name = "mainTabControl";
            mainTabControl.SelectedIndex = 0;
            mainTabControl.Size = new Size(745, 444);
            mainTabControl.TabIndex = 6;
            // 
            // tabPrepare
            // 
            tabPrepare.BackColor = SystemColors.Control;
            tabPrepare.Controls.Add(groupPrepare);
            tabPrepare.Controls.Add(groupRename);
            tabPrepare.Location = new Point(4, 24);
            tabPrepare.Name = "tabPrepare";
            tabPrepare.Padding = new Padding(3);
            tabPrepare.Size = new Size(737, 416);
            tabPrepare.TabIndex = 0;
            tabPrepare.Text = "Step 1: Prepare";
            tabPrepare.Click += tabPrepare_Click;
            // 
            // tabAssets
            // 
            tabAssets.BackColor = SystemColors.Control;
            tabAssets.Controls.Add(groupAssetLocatization);
            tabAssets.Controls.Add(groupAddAssets);
            tabAssets.Location = new Point(4, 24);
            tabAssets.Name = "tabAssets";
            tabAssets.Size = new Size(737, 416);
            tabAssets.TabIndex = 3;
            tabAssets.Text = "Step 2: Assets & Thumbnails";
            // 
            // groupAssetLocatization
            // 
            groupAssetLocatization.Controls.Add(label14);
            groupAssetLocatization.Controls.Add(txtLocalizedDescription);
            groupAssetLocatization.Controls.Add(label13);
            groupAssetLocatization.Controls.Add(txtLocalizedName);
            groupAssetLocatization.Controls.Add(label12);
            groupAssetLocatization.Controls.Add(comboLocale);
            groupAssetLocatization.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            groupAssetLocatization.Location = new Point(437, 3);
            groupAssetLocatization.Name = "groupAssetLocatization";
            groupAssetLocatization.Size = new Size(297, 404);
            groupAssetLocatization.TabIndex = 9;
            groupAssetLocatization.TabStop = false;
            groupAssetLocatization.Text = "Asset Localization (coming soon)";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(6, 134);
            label14.Name = "label14";
            label14.Size = new Size(92, 21);
            label14.TabIndex = 12;
            label14.Text = "Description:";
            // 
            // txtLocalizedDescription
            // 
            txtLocalizedDescription.Location = new Point(6, 158);
            txtLocalizedDescription.Multiline = true;
            txtLocalizedDescription.Name = "txtLocalizedDescription";
            txtLocalizedDescription.Size = new Size(285, 240);
            txtLocalizedDescription.TabIndex = 11;
            txtLocalizedDescription.TextChanged += txtLocalizedDescription_TextChanged;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(6, 78);
            label13.Name = "label13";
            label13.Size = new Size(110, 21);
            label13.TabIndex = 10;
            label13.Text = "Display Name:";
            // 
            // txtLocalizedName
            // 
            txtLocalizedName.Location = new Point(6, 102);
            txtLocalizedName.Name = "txtLocalizedName";
            txtLocalizedName.Size = new Size(285, 29);
            txtLocalizedName.TabIndex = 9;
            txtLocalizedName.TextChanged += txtLocalizedName_TextChanged;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(6, 31);
            label12.Name = "label12";
            label12.Size = new Size(57, 21);
            label12.TabIndex = 1;
            label12.Text = "Locale:";
            // 
            // comboLocale
            // 
            comboLocale.DropDownStyle = ComboBoxStyle.DropDownList;
            comboLocale.FormattingEnabled = true;
            comboLocale.Location = new Point(69, 28);
            comboLocale.Name = "comboLocale";
            comboLocale.Size = new Size(222, 29);
            comboLocale.TabIndex = 0;
            comboLocale.SelectedIndexChanged += comboLocale_SelectedIndexChanged;
            // 
            // tabPublishConfig
            // 
            tabPublishConfig.BackColor = SystemColors.Control;
            tabPublishConfig.Controls.Add(groupPublishConfig);
            tabPublishConfig.Location = new Point(4, 24);
            tabPublishConfig.Name = "tabPublishConfig";
            tabPublishConfig.Padding = new Padding(3);
            tabPublishConfig.Size = new Size(737, 416);
            tabPublishConfig.TabIndex = 1;
            tabPublishConfig.Text = "Step 3: Publish Configuration";
            // 
            // tabPublish
            // 
            tabPublish.BackColor = SystemColors.Control;
            tabPublish.Controls.Add(groupPDXCredentials);
            tabPublish.Controls.Add(cmdUpdatePublishedConfiguration);
            tabPublish.Controls.Add(cmdPublishNewMod);
            tabPublish.Controls.Add(cmdPublishNewVersion);
            tabPublish.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tabPublish.Location = new Point(4, 24);
            tabPublish.Name = "tabPublish";
            tabPublish.Padding = new Padding(3);
            tabPublish.Size = new Size(737, 416);
            tabPublish.TabIndex = 2;
            tabPublish.Text = "Step 4: Publish";
            // 
            // cmdUpdatePublishedConfiguration
            // 
            cmdUpdatePublishedConfiguration.Location = new Point(6, 303);
            cmdUpdatePublishedConfiguration.Name = "cmdUpdatePublishedConfiguration";
            cmdUpdatePublishedConfiguration.Size = new Size(279, 66);
            cmdUpdatePublishedConfiguration.TabIndex = 2;
            cmdUpdatePublishedConfiguration.Text = "Update Published Configuration";
            cmdUpdatePublishedConfiguration.UseVisualStyleBackColor = true;
            cmdUpdatePublishedConfiguration.Click += cmdUpdatePublishedConfiguration_Click;
            // 
            // cmdPublishNewMod
            // 
            cmdPublishNewMod.Location = new Point(6, 159);
            cmdPublishNewMod.Name = "cmdPublishNewMod";
            cmdPublishNewMod.Size = new Size(279, 66);
            cmdPublishNewMod.TabIndex = 0;
            cmdPublishNewMod.Text = "Publish New Mod";
            cmdPublishNewMod.UseVisualStyleBackColor = true;
            cmdPublishNewMod.Click += cmdPublishNewMod_Click;
            // 
            // cmdPublishNewVersion
            // 
            cmdPublishNewVersion.Location = new Point(6, 231);
            cmdPublishNewVersion.Name = "cmdPublishNewVersion";
            cmdPublishNewVersion.Size = new Size(279, 66);
            cmdPublishNewVersion.TabIndex = 1;
            cmdPublishNewVersion.Text = "Publish New Version";
            cmdPublishNewVersion.UseVisualStyleBackColor = true;
            cmdPublishNewVersion.Click += cmdPublishNewVersion_Click;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(763, 484);
            Controls.Add(mainTabControl);
            Controls.Add(statusStrip);
            Name = "Main";
            Text = " ";
            Load += Main_Load;
            groupRename.ResumeLayout(false);
            groupRename.PerformLayout();
            groupAddAssets.ResumeLayout(false);
            groupAddAssets.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)assetThumbnailBox).EndInit();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            groupPrepare.ResumeLayout(false);
            groupPrepare.PerformLayout();
            groupPDXCredentials.ResumeLayout(false);
            groupPDXCredentials.PerformLayout();
            groupPublishConfig.ResumeLayout(false);
            groupPublishConfig.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)packThumbnailBox).EndInit();
            mainTabControl.ResumeLayout(false);
            tabPrepare.ResumeLayout(false);
            tabAssets.ResumeLayout(false);
            groupAssetLocatization.ResumeLayout(false);
            groupAssetLocatization.PerformLayout();
            tabPublishConfig.ResumeLayout(false);
            tabPublish.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button cmdRenameProject;
        private TextBox txtProjectName;
        private Label label1;
        private GroupBox groupRename;
        private GroupBox groupAddAssets;
        private Label label2;
        private Button cmdBrowseAssets;
        private ListBox lbAssets;
        private Button cmdRemoveSelectedAsset;
        private OpenFileDialog selectAssetsDialog;
        private Button cmdAddThumbnail;
        private OpenFileDialog addThumbnailDialog;
        private TextBox txtPrefabName;
        private Label label3;
        private PictureBox assetThumbnailBox;
        private Button cmdApplyAssetName;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusLabel;
        private GroupBox groupPrepare;
        private Label label4;
        private Button cmdBrowseGamePath;
        private TextBox txtCities2Location;
        private OpenFileDialog browseGamePathDialog;
        private ToolStripSplitButton toolStripMenuButton;
        private ToolStripMenuItem toolStripMenu_OpenAppDir;
        private GroupBox groupPDXCredentials;
        private TextBox txtPdxMail;
        private Label label5;
        private Label label6;
        private TextBox txtPdxPw;
        private CheckBox cbSavePassword;
        private GroupBox groupPublishConfig;
        private Label label7;
        private Button cmdPublishNewMod;
        private TextBox txtPublishDisplayName;
        private Label label8;
        private TextBox txtPublishShortDescription;
        private Label label9;
        private TextBox txtPublishLongDescription;
        private Label label10;
        private PictureBox packThumbnailBox;
        private TabControl mainTabControl;
        private TabPage tabPrepare;
        private TabPage tabPublishConfig;
        private Button cmdMarkdownPreview;
        private TabPage tabPublish;
        private TextBox txtPublishModId;
        private Label label11;
        private Button cmdUpdatePublishedConfiguration;
        private Button cmdPublishNewVersion;
        private TabPage tabAssets;
        private GroupBox groupAssetLocatization;
        private Label label12;
        private ComboBox comboLocale;
        private Label label14;
        private TextBox txtLocalizedDescription;
        private Label label13;
        private TextBox txtLocalizedName;
    }
}
