﻿using System;
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

        private bool _openModPageAfterUpdate = true;

        public bool OpenModPageAfterUpdate
        {
            get => _openModPageAfterUpdate;
            set
            {
                _openModPageAfterUpdate = value;
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
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(toSave);
            try
            {
                File.WriteAllText(settingsFile, json);
            }
            catch(IOException)
            {
                MessageBox.Show("Error: Saving Settings failed. Please try again or restart the program as administrator, if access was denied.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
