using System.Diagnostics;
using System.Text;
using System.Xml;
using Microsoft.Build.Execution;
// ReSharper disable LocalizableElement

namespace AssetPackCreator;

public enum PublishResult
{
    Success,
    Error,
    Cancelled
}

public class Publisher
{
    private static (string, string) ExecutePublishCommand(string projectPath, string command)
    {


        if (MessageBox.Show($"Do you want to execute the {command} command?", "Publish Mod", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information) != DialogResult.Yes)
            return ("", "");

        var publishCommand = $"dotnet publish -p:PublishProfile={command}.pubxml --configuration Release";

        // Output-Strings für Standard-Output und Standard-Error
        var output = new StringBuilder();
        var error = new StringBuilder();

        // Execute command from project path in cmd
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/c {publishCommand}", // "/c" beendet die cmd.exe nach der Ausführung des Befehls
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                WorkingDirectory = projectPath
            }
        };

        process.OutputDataReceived += (sender, e) =>
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                output.AppendLine(e.Data);
                Console.WriteLine(e.Data); // Optional: Ausgabe in der Konsole
            }
        };

        process.ErrorDataReceived += (sender, e) =>
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                error.AppendLine(e.Data);
                Console.WriteLine(e.Data); // Optional: Ausgabe in der Konsole
            }
        };

        process.Start();

        // Beginne mit dem Lesen von Standard-Output und Standard-Error
        process.BeginOutputReadLine();
        process.BeginErrorReadLine();

        process.WaitForExit();

        return (output.ToString(), error.ToString());
    }


    public static PublishResult PublishNewVersion(string projectPath)
    {
        Main.UpdateStatus("Executing PublishNewVersion command...");
        var result = ExecutePublishCommand(projectPath, "PublishNewVersion");
        if (result == ("", ""))
            return PublishResult.Cancelled;
        try
        {
            var output = result.Item1;
            var successString = "Publishing new version updating process finished";
            if (output.Contains(successString))
            {
                MessageBox.Show($"New version published successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return PublishResult.Success;
            }
            else
            {
                // Remove everything before "Start publishing process"
                output = output.Substring(output.IndexOf("Start publishing new version process", StringComparison.Ordinal));
                Clipboard.SetText(output);
                MessageBox.Show("Error publishing new version: (full message copied to clipboard)" + output, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        catch (Exception e)
        {
            Clipboard.SetText(result.Item1);
            MessageBox.Show("Unknown Error executing command. Full output: " + result.Item1, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        return PublishResult.Error;
    }

    public static PublishResult UpdatePublishedConfiguration(string projectPath)
    {
        Main.UpdateStatus("Executing UpdatePublishedConfiguration command...");
        var result = ExecutePublishCommand(projectPath, "UpdatePublishedConfiguration");
        if (result == ("", ""))
            return PublishResult.Cancelled;
        try
        {
            var output = result.Item1;
            var successString = "Mod configuration updating process finished successfully";
            if (output.Contains(successString))
            {
                MessageBox.Show($"Mod configuration updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return PublishResult.Success;
            }
            else
            {
                // Remove everything before "Start publishing process"
                output = output.Substring(output.IndexOf("Start mod configuration updating process", StringComparison.Ordinal));
                Clipboard.SetText("Error publishing updated configuration: " + output);
                MessageBox.Show("Error publishing updated configuration: (full message copied to clipboard)" + output, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        catch (Exception e)
        {
            Clipboard.SetText(result.Item1);
            MessageBox.Show("Unknown Error executing command. Full output: " + result.Item1, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        return PublishResult.Error;
    }

    //public static void PublishNewMod(string gamePath, string publishConfigPath, string pdxMail, string pdxPassword, string localModPath)
    public static PublishResult PublishNewMod(string projectPath, out int modId)
    {
        Main.UpdateStatus("Executing PublishNewMod command...");
        var result = ExecutePublishCommand(projectPath, "PublishNewMod");
        modId = -1;
        if (result == ("", ""))
            return PublishResult.Cancelled;
        try
        {
            var output = result.Item1;
            var successString = "Mod publishing process finished successfully";
            if (output.Contains(successString))
            {
                Main.UpdateStatus("Mod published successfully \ud83d\ude0a");
                //The mod published with id=80386
                // Get mod id from that string
                var modIdString = output.Substring(output.IndexOf("The mod published with Id=", StringComparison.Ordinal) + "The mod published with Id=".Length);
                modIdString = modIdString.Substring(0, modIdString.IndexOf("\n", StringComparison.Ordinal));
                modId =  int.Parse(modIdString);
                MessageBox.Show($"Mod published successfully with id {modId}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return PublishResult.Success;
            }
            else
            {
                // Remove everything before "Start publishing process"
                output = output.Substring(output.IndexOf("Start publishing process", StringComparison.Ordinal));
                Clipboard.SetText("Error publishing mod: " + output);
                MessageBox.Show("Error publishing mod: (full message copied to clipboard)" + output, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        catch (Exception e)
        {
            Clipboard.SetText(result.Item1);
            MessageBox.Show("Unknown Error executing command. Full output: " + result.Item1, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        return PublishResult.Error;
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