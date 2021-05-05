using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ARPGGamepadCore;
using ARPGGamepadCore.Translators;

namespace ARPGGamepadWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public AppViewModel ViewModel { get; set; }

        IGamepadHelper gamepadHelper;
        ProfileManager profileManager;
        Timer timer;

        IGamepadTranslator gamepadAimOverlayTranslator;
        IGamepadTranslator gamepadBasicTranslator;

        //private bool running = false;

        public MainWindow(IInputHelper inputHelper, IOverlayHelper overlayHelper, IGamepadHelper gamepadHelper)
        {
            InitializeComponent();

            this.gamepadHelper = gamepadHelper;
            this.gamepadHelper.OnConnected += GamepadHelper_OnConnected;
            this.gamepadHelper.OnDisconnected += GamepadHelper_OnDisconnected;
            this.gamepadHelper.OnButtonUp += GamepadHelper_OnButtonUp;
            this.gamepadHelper.OnButtonDown += GamepadHelper_OnButtonDown;
            this.gamepadHelper.OnAnalogUpdate += GamepadHelper_OnAnalogUpdate;

            timer = new Timer(5);
            timer.Elapsed += Timer_Elapsed;

            profileManager = new ProfileManager(AppDomain.CurrentDomain.BaseDirectory, (int)SystemParameters.PrimaryScreenWidth, (int)SystemParameters.PrimaryScreenHeight);
            profileManager.ReloadProfiles();
            gamepadBasicTranslator = new BasicTranslator(inputHelper);
            gamepadAimOverlayTranslator = new OverlayAimTranslator(inputHelper, overlayHelper);

            ViewModel = new AppViewModel
            {
                ProfileManager = profileManager,
                Profile = profileManager.Profiles[ProfileManager.DefaultProfile],
                GamepadTranslator = gamepadAimOverlayTranslator,
                GamepadIndex = 0,
                Gamepads = new List<KeyValuePair<int, string>>
                {
                    new KeyValuePair<int, string>(0, "Gamepad 0"),
                    new KeyValuePair<int, string>(1, "Gamepad 1"),
                    new KeyValuePair<int, string>(2, "Gamepad 2"),
                    new KeyValuePair<int, string>(3, "Gamepad 3"),
                },
                Running = false
            };
            
            DataContext = ViewModel;
            ReloadConfigurations();

            ViewModel.Status = "Startup finished";
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                try
                {
                    gamepadHelper.Update();
                    if (gamepadHelper.Connected && ViewModel.GamepadTranslator != null)
                    {
                        ViewModel.GamepadTranslator.Process(ViewModel.Profile.SelectedResolution);
                    }
                }
                catch (Exception ex)
                {
                    timer.Stop();
                    ViewModel.Status = $"Error: {ex.Message}";
                }
            });
        }

        public void ReloadConfigurations()
        {
            profileManager.ReloadProfiles();

            if (profileManager.Profiles.Count > 0)
                ViewModel.Profile = profileManager.Profiles[ProfileManager.DefaultProfile];
            
            ViewModel.Status = $"Loaded {ViewModel.Profile.Name} profile correctly";
        }

        private void GamepadHelper_OnAnalogUpdate(object sender, GamepadHelperEventArgs e)
        {
            if (ViewModel.Running)
                ViewModel.GamepadTranslator.SendThumbstickChange(e.XPosition, e.YPosition, e.AnalogConfig, e.Side);
        }

        private void GamepadHelper_OnButtonDown(object sender, GamepadHelperEventArgs e)
        {
            if (ViewModel.Running)
                ViewModel.GamepadTranslator.SendButtonDown(e.Button);
        }

        private void GamepadHelper_OnButtonUp(object sender, GamepadHelperEventArgs e)
        {
            if (ViewModel.Running)
                ViewModel.GamepadTranslator.SendButtonUp(e.Button);
        }

        private void GamepadHelper_OnDisconnected(object sender, EventArgs e)
        {
            ViewModel.Status = $"Device {gamepadHelper.DeviceId} is not connected.";
        }

        private void GamepadHelper_OnConnected(object sender, EventArgs e)
        {
            ViewModel.Status = $"Device {gamepadHelper.DeviceId} is connected.";
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (VirtualAimMode.IsChecked.HasValue && VirtualAimMode.IsChecked.Value)
                ViewModel.GamepadTranslator = gamepadAimOverlayTranslator;
            else
                ViewModel.GamepadTranslator = gamepadBasicTranslator;

            ViewModel.Running = !ViewModel.Running;
            StartButton.Content = ViewModel.Running ? "Stop" : "Start";
            if (ViewModel.Running)
            {
                gamepadHelper.OpenGamepad(ViewModel.GamepadIndex, ViewModel.Profile);
                ViewModel.GamepadTranslator.Start();
                timer.Start();
            }
            else
            {
                timer.Stop();
                ViewModel.GamepadTranslator.Stop();
            }
        }

        private void TheWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ViewModel.GamepadTranslator.Stop();
            if (timer.Enabled)
                timer.Stop();
            timer.Dispose();
            gamepadBasicTranslator.Dispose();
            gamepadAimOverlayTranslator.Dispose();
        }

        private void TheWindow_Loaded(object sender, RoutedEventArgs e)
        {
            gamepadBasicTranslator.Init(this);
            gamepadAimOverlayTranslator.Init(this);
        }

        private void GamepadSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                var selectedItem = (KeyValuePair<int, string>)e.AddedItems[0];
                ViewModel.GamepadIndex = selectedItem.Key;
                gamepadHelper.OpenGamepad(ViewModel.GamepadIndex, ViewModel.Profile);
                ViewModel.Status = $"Selected the gamepad at index {ViewModel.GamepadIndex}";
            }
        }

        private void ProfileSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                var selectedItem = (KeyValuePair<string, GamepadProfile>)e.AddedItems[0];
                ViewModel.Profile = selectedItem.Value;
                ResolutionSelector.SelectedIndex = 0;
                ViewModel.Status = $"Selected game profile";
            }
        }

        private void ResolutionSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                var selectedItem = (ResolutionConfig)e.AddedItems[0];
                ViewModel.SelectedResolution = selectedItem;
            }
        }

    }
}
