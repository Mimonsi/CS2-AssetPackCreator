using System.Diagnostics;
using System.Text;
using System.Xml;
using Microsoft.Build.Execution;
// ReSharper disable LocalizableElement

namespace AssetPackCreator;

public enum PublishResult
{
    Success,
    Failed,
    Error,
    Cancelled
}

public class Publisher
{
    private static (string, string) ExecutePublishCommand(string projectPath, string command, bool silent = false)
    {
        if (!silent && MessageBox.Show($"Do you want to execute the {command} command?", "Publish Mod", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information) != DialogResult.Yes)
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

    private static bool TryGiveHelpFullErrorInfo(string message)
    {
        if (message.Contains("Details for the mod") && message.Contains("could not be retrieved"))
        {
            MessageBox.Show("Incorrect mod idea. User does not have permission to update this mod. Full log has been copied to clipboard.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return true;
        }
        if (message.Contains("User version already exists"))
        {
            MessageBox.Show("Mod version already exists. Please update the version number and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return true;
        }
        if (message.Contains("Updates are disabled for this mod"))
        {
            MessageBox.Show("Updates are disabled for this mod. Please make sure your mod is available (not banned). Set mod ID to 0 and use PublishNewMod to publish as new mod.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return true;
        }
        return false;
    }

    private static void HandlePublishError(string output, string command)
    {
        if (!string.IsNullOrEmpty(output)) 
            Clipboard.SetText(output);
        var splitAt = "Recommended Game Version: ";
        var failedText = output.Contains(splitAt) ? output.Substring(output.IndexOf(splitAt, StringComparison.Ordinal) + splitAt.Length) : output;
        if (!TryGiveHelpFullErrorInfo(output))
            MessageBox.Show($"Error executing command {command}. Full error message has been copied to the clipboard, please share it if you need help. Error: \n" + failedText, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
        }
        catch (Exception e)
        {
            MessageBox.Show("An error has occured: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        } 
        HandlePublishError(result.Item1, "PublishNewVersion");
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
        }
        catch (Exception e)
        {
            MessageBox.Show("An error has occured: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        } 
        HandlePublishError(result.Item1, "UpdatePublishedConfiguration");
        return PublishResult.Error;
    }
    
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
        }
        catch (Exception e)
        {
            MessageBox.Show("An error has occured: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        } 
        HandlePublishError(result.Item1, "PublishNewMod");
        return PublishResult.Error;
    }

    public static PublishResult BuildMod(string projectPath)
    {
        Main.UpdateStatus("Building mod...");
        var result = ExecutePublishCommand(projectPath, "BuildMod", true); // Build mod is not valid, therefore the publish fails and the mod is still built :)
        return PublishResult.Success;
    }
}