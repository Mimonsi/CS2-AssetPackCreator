using System.ComponentModel;
using System.Diagnostics;
using System.IO.Compression;
using System.Net;
using System.Security.AccessControl;
using Microsoft.VisualBasic;
using System.Security.AccessControl;

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
            cmdDelete.Enabled = false;
        }
        else
        {
            cmdOpenSelected.Enabled = true;
            cmdDelete.Enabled = true;
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
        var name = Interaction.InputBox("Enter the name of the new asset pack. Must end with 'AssetPack'", "New Asset Pack", $"Number{assetPacks.Count + 1}_AssetPack");
        if (string.IsNullOrEmpty(name) || name.Contains(' ') || !name.ToLower().EndsWith("assetpack") || name == "CustomAssetPack")
        {
            MessageBox.Show("Please enter a name that ends with 'AssetPack' and does not contain any spaces or other characters", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        var targetPath = Path.Combine(assetPacksParent, name);
        if (Directory.Exists(targetPath))
        {
            MessageBox.Show("A directory with this name already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        if (!CreateNewPack_git(targetPath))
        {
            if (!CreateNewPack_manualDownload(targetPath))
            {
                MessageBox.Show("Error creating new asset pack. Git creation and manual creation failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        var instruction = $"RENAME={name}\n";
        File.WriteAllText(Path.Combine(targetPath, "ins.txt"), instruction);

        ReloadAssetPacks();

        for (int i = 0; i < assetPacks.Count; i++)
        {
            if (assetPacks[i].Name == name)
            {
                lbAssetPacks.SelectedItem = assetPacks[i];
                cmdOpenSelected_Click(null, null);
                break;
            }
        }
    }

    private bool CreateNewPack_git(string targetPath)
    {
        try
        {
            var targetRepo = "https://github.com/kosch104/CS2-CustomAssetPack.git";
            //var targetBranch = "-b AssetPackCreatorBeta";
            var targetBranch = "";

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
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    private bool CreateNewPack_manualDownload(string targetPath)
    {
        // Manually download the asset pack repository
        var downloadLink = "https://codeload.github.com/kosch104/CS2-CustomAssetPack/zip/refs/heads/main";

        // Download file
        var tempPath = Path.Combine(Path.GetTempPath(), "CS2-CustomAssetPack");
        if (Directory.Exists(tempPath))
        {
            Directory.Delete(tempPath, true);
        }
        using (var client = new WebClient())
        {
            client.DownloadFile(downloadLink, tempPath + ".zip");
        }

        // Extract file
        ZipFile.ExtractToDirectory(tempPath + ".zip", tempPath, true);

        // Move directory inside the extracted to targetPath
        var extractedDir = new DirectoryInfo(tempPath).GetDirectories().First();
        extractedDir.MoveTo(targetPath);

        return true;
    }

    private void cmdDelete_Click(object sender, EventArgs e)
    {
        if (MessageBox.Show("Are you sure you want to delete this asset pack? Please make sure you have backed up any assets you may need. If you imported the pack, it has been moved from the original position and there will no be automatic backup. This cannot be undone.", "Are you sure?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
        {
            var selected = lbAssetPacks.SelectedItem as DirectoryInfo;
            if (selected?.FullName != null)
            {
                var directory = new DirectoryInfo(selected.FullName) { Attributes = FileAttributes.Normal };
                foreach (var info in directory.GetFileSystemInfos("*", SearchOption.AllDirectories))
                {
                    info.Attributes = FileAttributes.Normal;
                }
                directory.Delete(true);
                assetPacks.Remove(selected);
                ReloadAssetPacks();
            }

        }
    }
}