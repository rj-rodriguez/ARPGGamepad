using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.IO;

namespace ARPGGamepadCore
{
    public class ProfileManager
    {
        public const string DefaultProfile = "Default";
        public Dictionary<string, GamepadProfile> Profiles { get; init; }
        public Dictionary<string, string> Filenames { get; init; }
        public string ProfilesPath { get; init; }
        public int DefaultScreenWidth { get; init; }
        public int DefaultScreenHeight { get; init; }

        public ProfileManager(string execPath, int defaultScreenWidth, int defaultScreenHeight)
        {
            DefaultScreenWidth = defaultScreenWidth;
            DefaultScreenHeight = defaultScreenHeight;
            ProfilesPath = execPath + @"\Profiles";

            Profiles = new Dictionary<string, GamepadProfile>();
            Filenames = new Dictionary<string, string>();
        }

        public void ReloadProfiles()
        {
            var config = new GamepadProfile(DefaultScreenWidth, DefaultScreenHeight);

            List<string> files = System.IO.Directory.GetFiles(ProfilesPath, "*.json").ToList();

            Profiles.Clear();
            Profiles.Add(DefaultProfile, config);
            Filenames.Clear();
            files.ForEach(file =>
            {
                GamepadProfile fileConfig = LoadProfile(file);
                Profiles.Add(fileConfig.Name, fileConfig);
                Filenames.Add(fileConfig.Name, file);
            });
        }

        public GamepadProfile LoadProfile(string file)
        {
            var jsonValue = File.ReadAllText(file);
            return JsonSerializer.Deserialize<GamepadProfile>(jsonValue, new JsonSerializerOptions { IgnoreReadOnlyProperties = true });
        }

        public string GetProfileFilename(string name)
        {
            if (Filenames.ContainsKey(name))
                return Filenames[name];
            return string.Empty;
        }

        public void SaveProfile(GamepadProfile config, string saveName, bool overwrite = false)
        {
            if (String.IsNullOrWhiteSpace(saveName))
                throw new ArgumentException("File needs a name to be saved");

            if (!overwrite && Profiles.ContainsKey(config.Name))
                throw new ArgumentException("Profile name already exists");

            var jsonData = JsonSerializer.Serialize<GamepadProfile>(config, new JsonSerializerOptions { WriteIndented = true, IgnoreReadOnlyProperties = true });

            using (StreamWriter sw = new StreamWriter(saveName))
            {
                sw.Write(jsonData);
            }
        }

        public void UpdateProfile(GamepadProfile profile)
        {
            if (!Profiles.ContainsKey(profile.Name))
                Profiles.Add(profile.Name, profile);
            else
                Profiles[profile.Name] = profile;
        }
    }
}
