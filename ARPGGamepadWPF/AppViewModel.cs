using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ARPGGamepadCore;

namespace ARPGGamepadWPF
{
    public class DesignAppViewModel
    {
        public DesignAppViewModel()
        {
            ProfileManager = new ProfileManager(AppDomain.CurrentDomain.BaseDirectory, (int)SystemParameters.PrimaryScreenWidth, (int)SystemParameters.PrimaryScreenHeight);
            ProfileManager.Profiles.Add(ProfileManager.DefaultProfile, new GamepadProfile());
            Profile = ProfileManager.Profiles[ProfileManager.DefaultProfile];
            SelectedResolution = Profile.Resolutions[0];
            Gamepads = new List<KeyValuePair<int, string>>
            {
                new KeyValuePair<int, string>(0, "Gamepad 0"),
                new KeyValuePair<int, string>(1, "Gamepad 1")
            };
        }

        public ProfileManager ProfileManager { get; set;}
        public GamepadProfile Profile { get; set; }
        public ResolutionConfig SelectedResolution { get; set; }
        public IGamepadTranslator GamepadTranslator { get; set; }
        public int GamepadIndex => 0;
        public string Status => "The last status of the app";
        public List<KeyValuePair<int, string>> Gamepads { get; set; }
        public bool Running => false;
    }

    public class AppViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private ProfileManager profileManager;
        public ProfileManager ProfileManager
        {
            get => profileManager;
            set { profileManager = value; OnPropertyChanged(); }
        }

        private GamepadProfile profile;
        public GamepadProfile Profile
        {
            get => profile;
            set { profile = value; OnPropertyChanged(); SelectedResolution = value.Resolutions[0]; }
        }

        public ResolutionConfig SelectedResolution
        {
            get => Profile.SelectedResolution;
            set { Profile.SelectedResolution = value; OnPropertyChanged(); }
        }

        private IGamepadTranslator gamepadTranslator;
        public IGamepadTranslator GamepadTranslator
        {
            get => gamepadTranslator;
            set { gamepadTranslator = value; OnPropertyChanged(); }
        }

        public List<KeyValuePair<int, string>> Gamepads { get; set; }
        private int gamepadIndex = 0;
        public int GamepadIndex
        {
            get => gamepadIndex;
            set { gamepadIndex = value; OnPropertyChanged(); }
        }

        private string status;
        public string Status
        {
            get => status;
            set { status = value; OnPropertyChanged(); }
        }

        private bool running;
        public bool Running
        {
            get => running;
            set { running = value; OnPropertyChanged(); OnPropertyChanged("Idle"); }
        }
        public bool Idle => !Running;

    }
}
