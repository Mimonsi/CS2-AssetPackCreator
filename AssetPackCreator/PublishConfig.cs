using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml;

namespace AssetPackCreator
{
    public class PublishConfig
    {
        private static readonly string configFile = "Properties/PublishConfiguration.xml";
        private string _modId;
        public string ModId
        {
            get => _modId;
            set
            {
                _modId = value;
                Save();
            }
        }

        private string _displayName;
        public string DisplayName
        {
            get => _displayName;
            set
            {
                _displayName = value;
                Save();
            }
        }

        private string _shortDescription;
        public string ShortDescription
        {
            get => _shortDescription;
            set
            {
                _shortDescription = value;
                Save();
            }
        }

        private string _longDescription;
        public string LongDescription
        {
            get => _longDescription;
            set
            {
                _longDescription = value;
                Save();
            }
        }

        private string _thumbnail;
        public string Thumbnail
        {
            get => _thumbnail;
            set
            {
                _thumbnail = value;
                Save();
            }
        }

        private BindingList<string> _screenshots = new();
        public BindingList<string> Screenshots
        {
            get => _screenshots;
        }

        public void AddScreenshot(string screenshot)
        {
            if (string.IsNullOrEmpty(screenshot))
                return;
            if (screenshot.Contains(" "))
            {
                MessageBox.Show("Error: Screenshot path contains spaces. They have been replaced by underscores.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                screenshot = screenshot.Replace(" ", "_");
            }
            screenshot = screenshot.Replace("\\", "/");
            _screenshots.Add(screenshot);
            Save();
        }

        public void RemoveScreenshot(string lbImagesSelectedItem)
        {
            _screenshots.Remove(lbImagesSelectedItem);
            Save();
        }

        private List<string> _tags;
        public List<string> Tags
        {
            get => _tags;
            set
            {
                _tags = value;
                Save();
            }
        }

        private string _forumLink;
        public string ForumLink
        {
            get => _forumLink;
            set
            {
                _forumLink = value;
                Save();
            }
        }

        private string _modVersion;
        public string ModVersion
        {
            get => _modVersion;
            set
            {
                _modVersion = value;
                Save();
            }
        }

        private string _gameVersion;
        public string GameVersion
        {
            get => _gameVersion;
            set
            {
                _gameVersion = value;
                Save();
            }
        }

        private List<string> _dependencies;
        public List<string> Dependencies
        {
            get => _dependencies;
            set
            {
                _dependencies = value;
                Save();
            }
        }

        private string _changeLog;
        public string ChangeLog
        {
            get => _changeLog;
            set
            {
                _changeLog = value;
                Save();
            }
        }

        private Dictionary<string, string> _externalLinks;
        public Dictionary<string, string> ExternalLinks
        {
            get => _externalLinks;
            set
            {
                _externalLinks = value;
                Save();
            }
        }

        public static PublishConfig Load()
        {
            PublishConfig config = new PublishConfig();
            if (!File.Exists(configFile))
            {
                return config;
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(configFile);

            XmlNode root = doc.SelectSingleNode("Publish");
            XmlNodeList nodes = root.ChildNodes;
            foreach (XmlNode node in nodes)
            {
                switch (node.Name)
                {
                    case "ModId":
                        config.ModId = node.Attributes["Value"].Value;
                        break;
                    case "DisplayName":
                        config.DisplayName = node.Attributes["Value"].Value;
                        break;
                    case "ShortDescription":
                        config.ShortDescription = node.Attributes["Value"].Value;
                        break;
                    case "LongDescription":
                        config.LongDescription = node.InnerText.Trim();
                        break;
                    case "Thumbnail":
                        config.Thumbnail = node.Attributes["Value"].Value;
                        break;
                    case "Screenshot":
                        config.AddScreenshot(node.Attributes["Value"].Value);
                        break;
                    case "Tag":
                        config.Tags ??= new List<string>();
                        config.Tags.Add(node.Attributes["Value"].Value);
                        break;
                    case "ForumLink":
                        config.ForumLink = node.Attributes["Value"].Value;
                        break;
                    case "ModVersion":
                        config.ModVersion = node.Attributes["Value"].Value;
                        break;
                    case "GameVersion":
                        config.GameVersion = node.Attributes["Value"].Value;
                        break;
                    case "Dependency":
                        config.Dependencies ??= new List<string>();
                        config.Dependencies.Add(node.Attributes["Id"].Value);
                        break;
                    case "ChangeLog":
                        config.ChangeLog = node.InnerText.Trim();
                        break;
                    case "ExternalLink":
                        config.ExternalLinks ??= new Dictionary<string, string>();
                        config.ExternalLinks.Add(node.Attributes["Type"].Value, node.Attributes["Url"].Value);
                        break;
                }
            }

            config.EnableSaving = true;
            return config;
        }
        public bool EnableSaving { get; set; } = false;

        public void Save()
        {
            if (!EnableSaving)
                return;
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("Publish");
            doc.AppendChild(root);

            XmlElement modIdElement = doc.CreateElement("ModId");
            modIdElement.SetAttribute("Value", ModId);
            root.AppendChild(modIdElement);

            XmlElement displayNameElement = doc.CreateElement("DisplayName");
            displayNameElement.SetAttribute("Value", DisplayName);
            root.AppendChild(displayNameElement);

            XmlElement shortDescriptionElement = doc.CreateElement("ShortDescription");
            shortDescriptionElement.SetAttribute("Value", ShortDescription);
            root.AppendChild(shortDescriptionElement);

            XmlElement longDescriptionElement = doc.CreateElement("LongDescription");
            longDescriptionElement.InnerText = LongDescription;
            root.AppendChild(longDescriptionElement);

            XmlElement thumbnailElement = doc.CreateElement("Thumbnail");
            thumbnailElement.SetAttribute("Value", Thumbnail);
            root.AppendChild(thumbnailElement);

            if (Screenshots != null)
            {
                foreach (string screenshot in Screenshots)
                {
                    XmlElement screenshotElement = doc.CreateElement("Screenshot");
                    screenshotElement.SetAttribute("Value", screenshot);
                    root.AppendChild(screenshotElement);
                }
            }

            if (Tags != null)
            {
                foreach (string tag in Tags)
                {
                    XmlElement tagElement = doc.CreateElement("Tag");
                    tagElement.SetAttribute("Value", tag);
                    root.AppendChild(tagElement);
                }
            }

            XmlElement forumLinkElement = doc.CreateElement("ForumLink");
            forumLinkElement.SetAttribute("Value", ForumLink);
            root.AppendChild(forumLinkElement);

            XmlElement modVersionElement = doc.CreateElement("ModVersion");
            modVersionElement.SetAttribute("Value", ModVersion);
            root.AppendChild(modVersionElement);

            XmlElement gameVersionElement = doc.CreateElement("GameVersion");
            gameVersionElement.SetAttribute("Value", GameVersion);
            root.AppendChild(gameVersionElement);

            if (Dependencies != null)
            {
                foreach (string dependency in Dependencies)
                {
                    XmlElement dependencyElement = doc.CreateElement("Dependency");
                    dependencyElement.SetAttribute("Id", dependency);
                    root.AppendChild(dependencyElement);
                }
            }

            XmlElement changeLogElement = doc.CreateElement("ChangeLog");
            changeLogElement.InnerText = ChangeLog;
            root.AppendChild(changeLogElement);

            if (ExternalLinks != null)
            {
                foreach (KeyValuePair<string, string> externalLink in ExternalLinks)
                {
                    XmlElement externalLinkElement = doc.CreateElement("ExternalLink");
                    externalLinkElement.SetAttribute("Type", externalLink.Key);
                    externalLinkElement.SetAttribute("Url", externalLink.Value);
                    root.AppendChild(externalLinkElement);
                }
            }

            try
            {
                doc.Save(configFile);
                Main.TriggerSavedText();
            }
            catch(IOException)
            {
                MessageBox.Show("Error: Saving Publish Config failed. Please try again or restart the program as administrator, if access was denied.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public string ThumbnailPath => $"{Directory.GetCurrentDirectory()}/{Thumbnail}";
        public void ChangeThumbnail(string fileName)
        {
            Thumbnail = $"Properties/{Path.GetFileName(fileName)}";
            File.Copy(fileName, Thumbnail, true);
        }
    }
}
