namespace AssetPackWorkspaces;

partial class Form1
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
        lbAssetPacks = new ListBox();
        label1 = new Label();
        cmdCreateNew = new Button();
        cmdOpenSelected = new Button();
        cmdImportAssetPack = new Button();
        importDialog = new FolderBrowserDialog();
        cmdDelete = new Button();
        SuspendLayout();
        // 
        // lbAssetPacks
        // 
        lbAssetPacks.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lbAssetPacks.FormattingEnabled = true;
        lbAssetPacks.ItemHeight = 21;
        lbAssetPacks.Location = new Point(12, 33);
        lbAssetPacks.Name = "lbAssetPacks";
        lbAssetPacks.Size = new Size(513, 130);
        lbAssetPacks.TabIndex = 0;
        lbAssetPacks.SelectedIndexChanged += lbAssetPacks_SelectedIndexChanged;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(12, 9);
        label1.Name = "label1";
        label1.Size = new Size(118, 21);
        label1.TabIndex = 1;
        label1.Text = "My Asset Packs:";
        // 
        // cmdCreateNew
        // 
        cmdCreateNew.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        cmdCreateNew.Location = new Point(12, 222);
        cmdCreateNew.Name = "cmdCreateNew";
        cmdCreateNew.Size = new Size(513, 47);
        cmdCreateNew.TabIndex = 2;
        cmdCreateNew.Text = "Create New Asset Pack";
        cmdCreateNew.UseVisualStyleBackColor = true;
        cmdCreateNew.Click += cmdCreateNew_Click;
        // 
        // cmdOpenSelected
        // 
        cmdOpenSelected.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        cmdOpenSelected.Location = new Point(12, 169);
        cmdOpenSelected.Name = "cmdOpenSelected";
        cmdOpenSelected.Size = new Size(513, 47);
        cmdOpenSelected.TabIndex = 3;
        cmdOpenSelected.Text = "Open Selected Pack";
        cmdOpenSelected.UseVisualStyleBackColor = true;
        cmdOpenSelected.Click += cmdOpenSelected_Click;
        // 
        // cmdImportAssetPack
        // 
        cmdImportAssetPack.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        cmdImportAssetPack.Location = new Point(12, 275);
        cmdImportAssetPack.Name = "cmdImportAssetPack";
        cmdImportAssetPack.Size = new Size(251, 47);
        cmdImportAssetPack.TabIndex = 4;
        cmdImportAssetPack.Text = "Import Existing Asset Pack";
        cmdImportAssetPack.UseVisualStyleBackColor = true;
        cmdImportAssetPack.Click += cmdImportAssetPack_Click;
        // 
        // cmdDelete
        // 
        cmdDelete.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        cmdDelete.Location = new Point(269, 275);
        cmdDelete.Name = "cmdDelete";
        cmdDelete.Size = new Size(256, 47);
        cmdDelete.TabIndex = 5;
        cmdDelete.Text = "Delete selected Asset Pack";
        cmdDelete.UseVisualStyleBackColor = true;
        cmdDelete.Click += cmdDelete_Click;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(9F, 21F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(537, 346);
        Controls.Add(cmdDelete);
        Controls.Add(cmdImportAssetPack);
        Controls.Add(cmdOpenSelected);
        Controls.Add(cmdCreateNew);
        Controls.Add(label1);
        Controls.Add(lbAssetPacks);
        Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
        Margin = new Padding(4);
        Name = "Form1";
        Text = "Asset Pack Workspaces";
        Load += Form1_Load;
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private ListBox lbAssetPacks;
    private Label label1;
    private Button cmdCreateNew;
    private Button cmdOpenSelected;
    private Button cmdImportAssetPack;
    private FolderBrowserDialog importDialog;
    private Button cmdDelete;
}