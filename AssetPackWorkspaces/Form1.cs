using System.ComponentModel;
using System.Diagnostics;
using Microsoft.VisualBasic;

// ReSharper disable LocalizableElement

namespace AssetPackWorkspaces;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
    }

    private readonly string assetPacksParent = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "CS2-AssetPacks");
    private BindingList<DirectoryInfo> assetPacks = new();
    private void Form1_Load(object sender, EventArgs e)
    {
        ReloadAssetPacks();
        lbAssetPacks.DataSource = assetPacks;
        lbAssetPacks.DisplayMember = "Name";
    }

    private void ReloadAssetPacks()
    {
        assetPacks.Clear();
        DirectoryInfo di = new DirectoryInfo(assetPacksParent);
        if (!di.Exists)
        {
            di.Create();
        }
        foreach (DirectoryInfo dir in di.GetDirectories())
        {
            if (File.Exists(Path.Combine(dir.FullName, "AssetPackCreator.exe")))
            {
                assetPacks.Add(dir);
            }
        }
    }

    private void cmdOpenSelected_Click(object sender, EventArgs e)
    {
        if (lbAssetPacks.SelectedItem is DirectoryInfo selected)
        {
            //Process.Start(Path.Combine(selected.FullName, "AssetPackCreator.exe"));
            var startInfo = new ProcessStartInfo();
            startInfo.WorkingDirectory = selected.FullName;
            startInfo.FileName = Path.Combine(selected.FullName, "AssetPackCreator.exe");
            Process.Start(startInfo);
        }
    }

    private void lbAssetPacks_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lbAssetPacks.SelectedItem == null)
        {
            cmdOpenSelected.Enabled = false;
        }
        else
        {
            cmdOpenSelected.Enabled = true;
        }
    }

    private void cmdImportAssetPack_Click(object sender, EventArgs e)
    {
        if (importDialog.ShowDialog() == DialogResult.OK)
        {
            if (File.Exists(Path.Combine(importDialog.SelectedPath, "AssetPackCreator.exe")))
            {
                // Show message box with buttons "Move", "Copy" and "Cancel"
                DialogResult result = MessageBox.Show("Do you want to move the asset pack? Click 'Yes' to move", "Import Asset Pack", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    var localName = Path.GetFileName(importDialog.SelectedPath);
                    Directory.Move(importDialog.SelectedPath, Path.Combine(assetPacksParent, localName));
                    assetPacks.Add(new DirectoryInfo(importDialog.SelectedPath));
                }

            }
            else
            {
                MessageBox.Show("This folder is not a valid asset pack.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    private void cmdCreateNew_Click(object sender, EventArgs e)
    {
        // Create popup for name entry
        var name = Interaction.InputBox("Enter the name of the new asset pack. Must end with 'AssetPack'", "New Asset Pack", $"Number{assetPacks.Count+1}_AssetPack");
        if (string.IsNullOrEmpty(name) || name.Contains(' ') || !name.ToLower().EndsWith("assetpack") || name == "CustomAssetPack")
        {
            MessageBox.Show("Please enter a name that ends with 'AssetPack' and does not contain any spaces or other characters", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        var targetPath = Path.Combine(assetPacksParent, name);
        var targetRepo = "https://github.com/kosch104/CS2-CustomAssetPack.git";
        var targetBranch = "-b AssetPackCreatorBeta";

        var command = $"clone {targetBranch} {targetRepo} {targetPath}";
        var startInfo = new ProcessStartInfo
        {
            WorkingDirectory = assetPacksParent,
            FileName = "git",
            Arguments = command,
            UseShellExecute = true,
            CreateNoWindow = true,
            RedirectStandardOutput = false,
            RedirectStandardError = false,
        };
        var process = Process.Start(startInfo);
        process.WaitForExit();

        var instruction = $"RENAME={name}\n";
        File.WriteAllText(Path.Combine(targetPath, "ins.txt"), instruction);

        ReloadAssetPacks();

        for(int i = 0; i < assetPacks.Count; i++)
        {
            if (assetPacks[i].Name == name)
            {
                lbAssetPacks.SelectedItem = assetPacks[i];
                cmdOpenSelected_Click(null, null);
                break;
            }
        }

        // Execute command

    }
}