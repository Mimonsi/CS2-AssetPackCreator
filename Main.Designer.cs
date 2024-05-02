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
            groupBox1 = new GroupBox();
            cmdStep1 = new Button();
            cmdStep3 = new Button();
            cmdStep2 = new Button();
            groupAddAssets = new GroupBox();
            cmdApplyAssetName = new Button();
            cmdAddThumbnail = new Button();
            cmdRemoveSelectedAsset = new Button();
            thumbnailBox = new PictureBox();
            txtPrefabName = new TextBox();
            label3 = new Label();
            cmdBrowseAssets = new Button();
            lbAssets = new ListBox();
            label2 = new Label();
            selectAssetsDialog = new OpenFileDialog();
            addThumbnailDialog = new OpenFileDialog();
            statusStrip1 = new StatusStrip();
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
            cmdStep4 = new Button();
            groupRename.SuspendLayout();
            groupBox1.SuspendLayout();
            groupAddAssets.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)thumbnailBox).BeginInit();
            statusStrip1.SuspendLayout();
            groupPrepare.SuspendLayout();
            groupPDXCredentials.SuspendLayout();
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
            groupRename.Location = new Point(221, 108);
            groupRename.Name = "groupRename";
            groupRename.Size = new Size(279, 130);
            groupRename.TabIndex = 3;
            groupRename.TabStop = false;
            groupRename.Text = "Rename";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(cmdStep4);
            groupBox1.Controls.Add(cmdStep1);
            groupBox1.Controls.Add(cmdStep3);
            groupBox1.Controls.Add(cmdStep2);
            groupBox1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(200, 604);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "Creating your Asset Pack";
            // 
            // cmdStep1
            // 
            cmdStep1.Location = new Point(0, 28);
            cmdStep1.Name = "cmdStep1";
            cmdStep1.Size = new Size(188, 62);
            cmdStep1.TabIndex = 5;
            cmdStep1.Text = "Step 1: Prepare the Pack";
            cmdStep1.UseVisualStyleBackColor = true;
            cmdStep1.Click += cmdStep1_Click;
            // 
            // cmdStep3
            // 
            cmdStep3.Location = new Point(0, 164);
            cmdStep3.Name = "cmdStep3";
            cmdStep3.Size = new Size(188, 62);
            cmdStep3.TabIndex = 4;
            cmdStep3.Text = "Step 3: Add Assets to your Pack";
            cmdStep3.UseVisualStyleBackColor = true;
            cmdStep3.Click += cmdStep3_Click;
            // 
            // cmdStep2
            // 
            cmdStep2.Location = new Point(0, 96);
            cmdStep2.Name = "cmdStep2";
            cmdStep2.Size = new Size(188, 62);
            cmdStep2.TabIndex = 3;
            cmdStep2.Text = "Step 2: Name your Asset Pack";
            cmdStep2.UseVisualStyleBackColor = true;
            cmdStep2.Click += cmdStep2_Click;
            // 
            // groupAddAssets
            // 
            groupAddAssets.Controls.Add(cmdApplyAssetName);
            groupAddAssets.Controls.Add(cmdAddThumbnail);
            groupAddAssets.Controls.Add(cmdRemoveSelectedAsset);
            groupAddAssets.Controls.Add(thumbnailBox);
            groupAddAssets.Controls.Add(txtPrefabName);
            groupAddAssets.Controls.Add(label3);
            groupAddAssets.Controls.Add(cmdBrowseAssets);
            groupAddAssets.Controls.Add(lbAssets);
            groupAddAssets.Controls.Add(label2);
            groupAddAssets.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            groupAddAssets.Location = new Point(512, 108);
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
            // thumbnailBox
            // 
            thumbnailBox.Location = new Point(6, 169);
            thumbnailBox.Name = "thumbnailBox";
            thumbnailBox.Size = new Size(100, 100);
            thumbnailBox.SizeMode = PictureBoxSizeMode.Zoom;
            thumbnailBox.TabIndex = 7;
            thumbnailBox.TabStop = false;
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
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripMenuButton, statusLabel });
            statusStrip1.Location = new Point(0, 628);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(965, 22);
            statusStrip1.TabIndex = 5;
            statusStrip1.Text = "statusStrip1";
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
            groupPrepare.Location = new Point(218, 12);
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
            groupPDXCredentials.Location = new Point(221, 247);
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
            // cmdStep4
            // 
            cmdStep4.Location = new Point(0, 232);
            cmdStep4.Name = "cmdStep4";
            cmdStep4.Size = new Size(188, 62);
            cmdStep4.TabIndex = 6;
            cmdStep4.Text = "Step 4: Enter Paradox Mods Credentials";
            cmdStep4.UseVisualStyleBackColor = true;
            cmdStep4.Click += cmdStep4_Click;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(965, 650);
            Controls.Add(groupPDXCredentials);
            Controls.Add(groupPrepare);
            Controls.Add(statusStrip1);
            Controls.Add(groupAddAssets);
            Controls.Add(groupBox1);
            Controls.Add(groupRename);
            Name = "Main";
            Text = "Asset Pack Creator";
            Load += Main_Load;
            groupRename.ResumeLayout(false);
            groupRename.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupAddAssets.ResumeLayout(false);
            groupAddAssets.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)thumbnailBox).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            groupPrepare.ResumeLayout(false);
            groupPrepare.PerformLayout();
            groupPDXCredentials.ResumeLayout(false);
            groupPDXCredentials.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button cmdRenameProject;
        private TextBox txtProjectName;
        private Label label1;
        private GroupBox groupRename;
        private GroupBox groupBox1;
        private Button cmdStep2;
        private GroupBox groupAddAssets;
        private Label label2;
        private Button cmdBrowseAssets;
        private ListBox lbAssets;
        private Button cmdRemoveSelectedAsset;
        private Button cmdStep3;
        private OpenFileDialog selectAssetsDialog;
        private Button cmdAddThumbnail;
        private OpenFileDialog addThumbnailDialog;
        private TextBox txtPrefabName;
        private Label label3;
        private PictureBox thumbnailBox;
        private Button cmdApplyAssetName;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel statusLabel;
        private Button cmdStep1;
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
        private Button cmdStep4;
    }
}
