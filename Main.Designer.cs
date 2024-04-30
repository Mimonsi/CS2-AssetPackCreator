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
            cmdRenameProject = new Button();
            txtProjectName = new TextBox();
            label1 = new Label();
            groupRename = new GroupBox();
            groupBox1 = new GroupBox();
            cmdStep2 = new Button();
            cmdStep1 = new Button();
            groupAddAssets = new GroupBox();
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
            groupRename.SuspendLayout();
            groupBox1.SuspendLayout();
            groupAddAssets.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)thumbnailBox).BeginInit();
            SuspendLayout();
            // 
            // cmdRenameProject
            // 
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
            groupRename.Location = new Point(218, 12);
            groupRename.Name = "groupRename";
            groupRename.Size = new Size(279, 130);
            groupRename.TabIndex = 3;
            groupRename.TabStop = false;
            groupRename.Text = "Rename";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(cmdStep2);
            groupBox1.Controls.Add(cmdStep1);
            groupBox1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(200, 604);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "Creating your Asset Pack";
            // 
            // cmdStep2
            // 
            cmdStep2.Location = new Point(6, 96);
            cmdStep2.Name = "cmdStep2";
            cmdStep2.Size = new Size(188, 62);
            cmdStep2.TabIndex = 4;
            cmdStep2.Text = "Step 2: Add Assets to your Pack";
            cmdStep2.UseVisualStyleBackColor = true;
            // 
            // cmdStep1
            // 
            cmdStep1.Location = new Point(6, 28);
            cmdStep1.Name = "cmdStep1";
            cmdStep1.Size = new Size(188, 62);
            cmdStep1.TabIndex = 3;
            cmdStep1.Text = "Step 1: Name your Asset Pack";
            cmdStep1.UseVisualStyleBackColor = true;
            // 
            // groupAddAssets
            // 
            groupAddAssets.Controls.Add(cmdAddThumbnail);
            groupAddAssets.Controls.Add(cmdRemoveSelectedAsset);
            groupAddAssets.Controls.Add(thumbnailBox);
            groupAddAssets.Controls.Add(txtPrefabName);
            groupAddAssets.Controls.Add(label3);
            groupAddAssets.Controls.Add(cmdBrowseAssets);
            groupAddAssets.Controls.Add(lbAssets);
            groupAddAssets.Controls.Add(label2);
            groupAddAssets.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            groupAddAssets.Location = new Point(503, 12);
            groupAddAssets.Name = "groupAddAssets";
            groupAddAssets.Size = new Size(431, 286);
            groupAddAssets.TabIndex = 4;
            groupAddAssets.TabStop = false;
            groupAddAssets.Text = "Add Assets";
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
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(965, 628);
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
            ResumeLayout(false);
        }

        #endregion

        private Button cmdRenameProject;
        private TextBox txtProjectName;
        private Label label1;
        private GroupBox groupRename;
        private GroupBox groupBox1;
        private Button cmdStep1;
        private GroupBox groupAddAssets;
        private Label label2;
        private Button cmdBrowseAssets;
        private ListBox lbAssets;
        private Button cmdRemoveSelectedAsset;
        private Button cmdStep2;
        private OpenFileDialog selectAssetsDialog;
        private Button cmdAddThumbnail;
        private OpenFileDialog addThumbnailDialog;
        private TextBox txtPrefabName;
        private Label label3;
        private PictureBox thumbnailBox;
    }
}
