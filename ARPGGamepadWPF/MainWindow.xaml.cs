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

        IGamepadTranslator gamepadTranslator;

        private bool running = false;

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

            profileManager = new ProfileManager(AppDomain.CurrentDomain.BaseDirectory, (int)SystemParameters.FullPrimaryScreenWidth, (int)SystemParameters.FullPrimaryScreenHeight);
            gamepadTranslator = new OverlayAimTranslator(inputHelper, overlayHelper);

            ViewModel = new AppViewModel
            {
                ProfileManager = profileManager,
                Profile = profileManager.Profiles[ProfileManager.DefaultProfile]
            };
            DataContext = ViewModel;
            ReloadConfigurations();

            //hardcoded selections for initial tests
            ViewModel.Profile = profileManager.Profiles["POE"];
            ViewModel.Profile.SelectedResolution = ViewModel.Profile.Resolutions[0];
            gamepadHelper.OpenGamepad(0, ViewModel.Profile);
            StatusBarMessage.Text = $"Loaded {ViewModel.Profile.Name} profile correctly";
            //hardcoded selections end
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                try
                {
                    gamepadHelper.Update();
                    if (gamepadHelper.Connected && gamepadTranslator != null)
                    {
                        gamepadTranslator.Process(ViewModel.Profile.SelectedResolution);
                    }
                }
                catch (Exception ex)
                {
                    timer.Stop();
                    StatusBarMessage.Text = $"Error: {ex.Message}";
                }
            });
        }

        public void ReloadConfigurations()
        {
            profileManager.ReloadProfiles();

            if (profileManager.Profiles.Count > 0)
                ViewModel.Profile = profileManager.Profiles[ProfileManager.DefaultProfile];
            
            StatusBarMessage.Text = $"Loaded {ViewModel.Profile.Name} profile correctly";
        }

        private void GamepadHelper_OnAnalogUpdate(object sender, GamepadHelperEventArgs e)
        {
            if (running)
                gamepadTranslator.SendThumbstickChange(e.XPosition, e.YPosition, e.AnalogConfig, e.Side);
        }

        private void GamepadHelper_OnButtonDown(object sender, GamepadHelperEventArgs e)
        {
            if (running)
                gamepadTranslator.SendButtonDown(e.Button);
        }

        private void GamepadHelper_OnButtonUp(object sender, GamepadHelperEventArgs e)
        {
            if (running)
                gamepadTranslator.SendButtonUp(e.Button);
        }

        private void GamepadHelper_OnDisconnected(object sender, EventArgs e)
        {
            StatusBarMessage.Text = $"Device {gamepadHelper.DeviceId} is not connected.";
        }

        private void GamepadHelper_OnConnected(object sender, EventArgs e)
        {
            StatusBarMessage.Text = $"Device {gamepadHelper.DeviceId} is connected.";
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            running = !running;
            StartButton.Content = running ? "Stop" : "Start";
            if (running)
            {
                timer.Start();
                gamepadTranslator.Start();
            }
            else
            {
                gamepadTranslator.Stop();
                timer.Stop();
            }
        }

        private void TheWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            gamepadTranslator.Stop();
            if (timer.Enabled)
                timer.Stop();
            timer.Dispose();
            gamepadTranslator.Dispose();
        }

        private void TheWindow_Loaded(object sender, RoutedEventArgs e)
        {
            gamepadTranslator.Init(this);
        }
    }
}
