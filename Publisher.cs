using System.Diagnostics;
using System.Xml;
using Microsoft.Build.Execution;

namespace AssetPackCreator;

public class Publisher
{

    private static void ExecutePublishCommand(string projectPath, string command)
    {
        if (MessageBox.Show($"Do you want to execute the {command} command?", "Publish Mod", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information) != DialogResult.Yes)
            return;
        var publishCommand = $"dotnet publish -p:PublishProfile={command}.pubxml --configuration Release";
        // Execute command from project path in cmd
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/k {publishCommand}",
                RedirectStandardOutput = false,
                RedirectStandardError = false,
                UseShellExecute = false,
                CreateNoWindow = false,
                WorkingDirectory = projectPath
            }
        };

        process.Start();
    }

    public static void PublishNewVersion(string projectPath)
    {
        ExecutePublishCommand(projectPath, "PublishNewVersion");
    }

    public static void UpdatePublishedConfiguration(string projectPath)
    {
        ExecutePublishCommand(projectPath, "UpdatePublishedConfiguration");
    }

    //public static void PublishNewMod(string gamePath, string publishConfigPath, string pdxMail, string pdxPassword, string localModPath)
    public static void PublishNewMod(string projectPath)
    {
        ExecutePublishCommand(projectPath, "PublishNewMod");

        /*
        ""D:\Games\steamapps\common\Cities Skylines II\Cities2_Data\StreamingAssets\~Tooling~\ModPublisher\ModPublisher.exe" Publish "C:\Users\Konsi\Documents\CS2-Modding\CS2-AssetPackCreator\bin\Debug\net8.0-windows\Properties\PublishConfiguration.xml" -l "C:\Users\Konsi\Desktop\pdx_account.txt" -c "C:\Users\Konsi\AppData\LocalLow\Colossal Order\Cities Skylines II\Mods\TestAssetPack" -v"

        // get directory of game path
        var gameDir = Path.GetDirectoryName(gamePath);
        var modPublisherPath = Path.Combine(gameDir, @"Cities2_Data\StreamingAssets\~Tooling~\ModPublisher\ModPublisher.exe");
        // Store in temp folder
        var pdxAccountFile = Path.Combine(Path.GetTempPath(), "pdx_account.txt");
        File.WriteAllText(pdxAccountFile, $"{pdxMail}\n{pdxPassword}");

        var command = "Publish";
        var fullCommand = $"\"\"{modPublisherPath}\" {command} " +
                          $"\"{publishConfigPath}\" " +
                          $"-l \"{pdxAccountFile}\" " +
                          $"-c \"{localModPath}\" " +
                          $"-v\"";


        if (MessageBox.Show("Do you want to execute the following publish command? This will publish your mod on Paradox Mods if successful. \n" + fullCommand, "Publish Mod", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information) != DialogResult.Yes)
            return;
        Main.UpdateStatus("Publishing mod...");
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/k {fullCommand}", // Verwende /k, um das Konsolenfenster geöffnet zu halten
                RedirectStandardOutput = false,
                RedirectStandardError = false,
                UseShellExecute = false,
                CreateNoWindow = false
            }
        };

        process.Start();
        Main.UpdateStatus("Publish started in external window");
                 */
    }
}