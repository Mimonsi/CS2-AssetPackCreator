using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetPackCreator
{
    public record Settings
    {
        private static readonly string settingsFile = "assetPackCreatorSettings.json";
        private string _cities2Path;
        public string Cities2Path
        {
            get => _cities2Path;
            set
            {
                _cities2Path = value;
                Save();
            }
        }

        private string _pdxMail;
        public string PdxMail
        {
            get => _pdxMail;
            set
            {
                _pdxMail = value;
                Save();
            }
        }

        private string _pdxPassword;
        public string PdxPassword
        {
            get => _pdxPassword;
            set
            {
                _pdxPassword = value;
                Save();
            }
        }

        private bool _savePassword;
        public bool SavePassword
        {
            get => _savePassword;
            set
            {
                _savePassword = value;
                Save();
            }
        }

        public Settings()
        {
            _cities2Path = @"C:\Program Files (x86)\Steam\steamapps\common\Cities Skylines II\Cities_Data\Cities2.exe";
        }

        public static Settings Load()
        {
            Settings s;
            if (File.Exists(settingsFile))
            {
                string json = File.ReadAllText(settingsFile);
                s = Newtonsoft.Json.JsonConvert.DeserializeObject<Settings>(json);
                if (s != null)
                    return s;
            }

            return new Settings();
        }

        public void Save()
        {
            // Copy object
            Settings toSave = this with { };
            if (!_savePassword)
                toSave._pdxPassword = "";
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(toSave);
            File.WriteAllText(settingsFile, json);
        }
    }
}
