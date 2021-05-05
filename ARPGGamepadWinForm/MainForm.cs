using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using XGamePad;
using System.IO;
using ARPGGamepadCore;
using ARPGGamepadCore.Translators;

namespace ARPGGamepadWinForm
{    
    public partial class MainForm : Form
    {
        Gamepad gamepad;
        Timer timer;
        GamepadProfile config;
        ProfileManager configMan;
        ResolutionConfig selectedResolution;
        IInputHelper inputHelper;
        IOverlayHelper overlayHelper;

        IGamepadTranslator gamepadTranslator;
        IGamepadTranslator basicTranslator;
        IGamepadTranslator twinStickTranslator;

        private List<KeyValuePair<string, ResolutionConfig>> resolutions;
        private List<KeyValuePair<string, ButtonConfig>> buttonsConfig;

        public MainForm()
        {
            InitializeComponent();

            inputHelper = new InputHelper();
            overlayHelper = new OverlayHelper();

            txtNotes.Text = @"Notes:
- Keyboard keys D0 - D9 stand for the normal Numeric keys 0 - 9.

- Virtual Aim Mode only works if the game is running in Windowed or Windowed Fullscreen Mode, enable and then reset the ON checkbox.";

            cboMouse.DataSource = Enum.GetValues(typeof(ARPGGamepadCore.MouseClick));
            cboModifier.DataSource = Enum.GetValues(typeof(ARPGGamepadCore.Modifiers));
            cboKey.DataSource = Enum.GetValues(typeof(ARPGGamepadCore.KeyboardKeys));

            cbMouseButton.DataSource = Enum.GetValues(typeof(ARPGGamepadCore.MouseClick));
            cbMouseModifier.DataSource = Enum.GetValues(typeof(ARPGGamepadCore.Modifiers));
            cbMouseKeyboard.DataSource = Enum.GetValues(typeof(ARPGGamepadCore.KeyboardKeys));
            cbRMouseButton.DataSource = Enum.GetValues(typeof(ARPGGamepadCore.MouseClick));
            cbRMouseModifier.DataSource = Enum.GetValues(typeof(ARPGGamepadCore.Modifiers));
            cbRMouseKeyboard.DataSource = Enum.GetValues(typeof(ARPGGamepadCore.KeyboardKeys));

            selGamepad.SelectedIndex = 0;

            configMan = new ProfileManager(Path.GetDirectoryName(Application.ExecutablePath), Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            txtWidthResolution.Text = configMan.DefaultScreenWidth.ToString();
            txtHeightResolution.Text = configMan.DefaultScreenHeight.ToString();

            ReloadConfigurations();

            twinStickTranslator = new OverlayAimTranslator(inputHelper, overlayHelper);
            basicTranslator = new BasicTranslator(inputHelper);
            gamepadTranslator = basicTranslator;
            ToGamepadConfiguration();
        }

        private void ReloadConfigurations()
        {
            configMan.ReloadProfiles();
            var profiles = configMan.Profiles.Select(item => new KeyValuePair<string, GamepadProfile>(item.Value.Name, item.Value)).ToList();
            selProfile.DataSource = new BindingSource(profiles, null);
            selProfile.DisplayMember = "Key";
            selProfile.ValueMember = "Value";
        }

        private void selGamepad_SelectedIndexChanged(object sender, EventArgs e)
        {
            OpenGamepad(selGamepad.SelectedIndex);
        }

        private void ToUIConfiguration()
        {
            txtName.Text = config.Name;
            txtAspectX.Text = selectedResolution.LeftAnalog.AspectRatioX.ToString();
            txtAspectY.Text = selectedResolution.LeftAnalog.AspectRatioY.ToString();
            txtRadius.Text = selectedResolution.LeftAnalog.Radius.ToString();
            chkFixed.Checked = selectedResolution.LeftAnalog.FixedRadius;
            chkPressed.Checked = selectedResolution.LeftAnalog.KeepPressed;
            chkSpringMode.Checked = selectedResolution.LeftAnalog.SpringMode;
            txtInnerRadius.Text = selectedResolution.LeftAnalog.InnerRadius.ToString();
            txtDeadzone.Text = selectedResolution.LeftAnalog.DeadZone.ToString();
            if (selectedResolution.LeftAnalog.Button != null)
            {
                if (!String.IsNullOrEmpty(selectedResolution.LeftAnalog.Button.Key))
                {
                    cbMouseKeyboard.SelectedItem = (ARPGGamepadCore.KeyboardKeys)inputHelper.GetLetterKey(selectedResolution.LeftAnalog.Button.Key);
                }
                else
                {
                    cbMouseKeyboard.SelectedItem = ARPGGamepadCore.KeyboardKeys.None;
                }
                cbMouseButton.SelectedItem = selectedResolution.LeftAnalog.Button.MouseClick;
                cbMouseModifier.SelectedItem = selectedResolution.LeftAnalog.Button.Modifier;
            }
            txtOffsetX.Text = selectedResolution.LeftAnalog.OffsetX.ToString();
            txtOffsetY.Text = selectedResolution.LeftAnalog.OffsetY.ToString();

            txtRAspectX.Text = selectedResolution.RightAnalog.AspectRatioX.ToString();
            txtRAspectY.Text = selectedResolution.RightAnalog.AspectRatioY.ToString();
            txtRRadius.Text = selectedResolution.RightAnalog.Radius.ToString();
            chkRFixed.Checked = selectedResolution.RightAnalog.FixedRadius;
            chkRPressed.Checked = selectedResolution.RightAnalog.KeepPressed;
            txtRInnerRadius.Text = selectedResolution.RightAnalog.InnerRadius.ToString();
            txtRDeadzone.Text = selectedResolution.RightAnalog.DeadZone.ToString();
            if (selectedResolution.RightAnalog.Button != null)
            {
                if (!String.IsNullOrEmpty(selectedResolution.RightAnalog.Button.Key))
                {
                    cbRMouseKeyboard.SelectedItem = (ARPGGamepadCore.KeyboardKeys)inputHelper.GetLetterKey(selectedResolution.RightAnalog.Button.Key);
                }
                else
                {
                    cbRMouseKeyboard.SelectedItem = ARPGGamepadCore.KeyboardKeys.None;
                }
                cbRMouseButton.SelectedItem = selectedResolution.RightAnalog.Button.MouseClick;
                cbRMouseModifier.SelectedItem = selectedResolution.RightAnalog.Button.Modifier;
            }
            txtROffsetX.Text = selectedResolution.RightAnalog.OffsetX.ToString();
            txtROffsetY.Text = selectedResolution.RightAnalog.OffsetY.ToString();
        }

        private void ToGamepadConfiguration()
        {
            if (selectedResolution == null)
            {
                return;
            }

            var leftAnalog = selectedResolution.LeftAnalog with 
            {
                AspectRatioX = double.Parse(txtAspectX.Text),
                AspectRatioY = double.Parse(txtAspectY.Text),
                Radius = double.Parse(txtRadius.Text),
                FixedRadius = chkFixed.Checked,
                KeepPressed = chkPressed.Checked,
                InnerRadius = double.Parse(txtInnerRadius.Text),
                DeadZone = float.Parse(txtDeadzone.Text),
                SpringMode = chkSpringMode.Checked,
                OffsetX = int.Parse(txtOffsetX.Text),
                OffsetY = int.Parse(txtOffsetY.Text),
                Button = selectedResolution.LeftAnalog.Button with
                {
                    Button = selectedResolution.LeftAnalog.Button?.Button ?? "LeftAnalog",
                    Key = cbMouseKeyboard.SelectedValue.ToString(),
                    MouseClick = (ARPGGamepadCore.MouseClick)cbMouseButton.SelectedValue,
                    Modifier = (ARPGGamepadCore.Modifiers)cbMouseModifier.SelectedValue,
                }
            };
            var rightAnalog = selectedResolution.RightAnalog with
            {
                AspectRatioX = double.Parse(txtRAspectX.Text),
                AspectRatioY = double.Parse(txtRAspectY.Text),
                Radius = double.Parse(txtRRadius.Text),
                FixedRadius = chkRFixed.Checked,
                KeepPressed = chkRPressed.Checked,
                InnerRadius = double.Parse(txtRInnerRadius.Text),
                DeadZone = float.Parse(txtRDeadzone.Text),
                OffsetX = int.Parse(txtROffsetX.Text),
                OffsetY = int.Parse(txtROffsetY.Text),
                Button = selectedResolution.RightAnalog.Button with
                {
                    Button = selectedResolution.RightAnalog.Button?.Button ?? "RightAnalog",
                    Key = cbMouseKeyboard.SelectedValue.ToString(),
                    MouseClick = (ARPGGamepadCore.MouseClick)cbMouseButton.SelectedValue,
                    Modifier = (ARPGGamepadCore.Modifiers)cbMouseModifier.SelectedValue
                }
            };
            selectedResolution = selectedResolution with
            {
                LeftAnalog = leftAnalog,
                RightAnalog = rightAnalog
            };

            config.Name = txtName.Text;
        }


        #region General gamepad helpers
        public void OpenGamepad(int gamepadIndex)
        {
            txtStatus.Text = "Accessing gamepad device " + gamepadIndex + "\r";
            gamepad = new Gamepad(gamepadIndex);

            gamepad.OnRightThumbUpdate += new Gamepad.ThumbHandler(this.OnThumbUpdate);
            gamepad.OnLeftThumbUpdate += new Gamepad.ThumbHandler(this.OnThumbUpdate);
            gamepad.OnConnect += new Gamepad.ConnectionHandler(this.OnConnect);
            gamepad.OnDisconnect += new Gamepad.ConnectionHandler(this.OnDisconnect);
            gamepad.OnAButtonPress += new Gamepad.ButtonHandler(this.OnButtonPress);
            gamepad.OnAButtonRelease += new Gamepad.ButtonHandler(this.OnButtonPress);
            gamepad.OnBButtonPress += new Gamepad.ButtonHandler(this.OnButtonPress);
            gamepad.OnBButtonRelease += new Gamepad.ButtonHandler(this.OnButtonPress);
            gamepad.OnXButtonPress += new Gamepad.ButtonHandler(this.OnButtonPress);
            gamepad.OnXButtonRelease += new Gamepad.ButtonHandler(this.OnButtonPress);
            gamepad.OnYButtonPress += new Gamepad.ButtonHandler(this.OnButtonPress);
            gamepad.OnYButtonRelease += new Gamepad.ButtonHandler(this.OnButtonPress);
            gamepad.OnBackButtonPress += new Gamepad.ButtonHandler(this.OnButtonPress);
            gamepad.OnBackButtonRelease += new Gamepad.ButtonHandler(this.OnButtonPress);
            gamepad.OnStartButtonPress += new Gamepad.ButtonHandler(this.OnButtonPress);
            gamepad.OnStartButtonRelease += new Gamepad.ButtonHandler(this.OnButtonPress);
            gamepad.OnLeftShoulderButtonPress += new Gamepad.ButtonHandler(this.OnButtonPress);
            gamepad.OnLeftShoulderButtonRelease += new Gamepad.ButtonHandler(this.OnButtonPress);
            gamepad.OnRightShoulderButtonPress += new Gamepad.ButtonHandler(this.OnButtonPress);
            gamepad.OnRightShoulderButtonRelease += new Gamepad.ButtonHandler(this.OnButtonPress);
            gamepad.OnLeftThumbButtonPress += new Gamepad.ButtonHandler(this.OnButtonPress);
            gamepad.OnLeftThumbButtonRelease += new Gamepad.ButtonHandler(this.OnButtonPress);
            gamepad.OnRightThumbButtonPress += new Gamepad.ButtonHandler(this.OnButtonPress);
            gamepad.OnRightThumbButtonRelease += new Gamepad.ButtonHandler(this.OnButtonPress);
            gamepad.OnDPadPress += new Gamepad.DPadHandler(this.OnDPadPress);
            gamepad.OnDPadRelease += new Gamepad.DPadHandler(this.OnDPadPress);
            gamepad.OnDPadChange += new Gamepad.DPadHandler(this.OnDPadPress);
            gamepad.OnLeftTriggerPress += new Gamepad.TriggerHandler(this.OnTriggerPress);
            gamepad.OnLeftTriggerRelease += new Gamepad.TriggerHandler(this.OnTriggerPress);
            gamepad.OnRightTriggerPress += new Gamepad.TriggerHandler(this.OnTriggerPress);
            gamepad.OnRightTriggerRelease += new Gamepad.TriggerHandler(this.OnTriggerPress);

            timer = new Timer();
            timer.Interval = 5;
            timer.Tick += new EventHandler(OnTick);
            timer.Start();

            txtStatus.Text = String.Format("Gamepad {0} Accessed", gamepadIndex);
        }

        private ButtonConfig GetCurrentButton(GamepadButtons button)
        {
            ButtonConfig currentButton = null;
            switch (button)
            {
                case GamepadButtons.DPadLeft:
                    currentButton = config.DLeft;
                    break;
                case GamepadButtons.DPadRight:
                    currentButton = config.DRight;
                    break;
                case GamepadButtons.DPadUp:
                    currentButton = config.DUp;
                    break;
                case GamepadButtons.DPadDown:
                    currentButton = config.DDown;
                    break;
                case GamepadButtons.A:
                    currentButton = config.A;
                    break;
                case GamepadButtons.B:
                    currentButton = config.B;
                    break;
                case GamepadButtons.X:
                    currentButton = config.X;
                    break;
                case GamepadButtons.Y:
                    currentButton = config.Y;
                    break;
                case GamepadButtons.Back:
                    currentButton = config.Select;
                    break;
                case GamepadButtons.Start:
                    currentButton = config.Start;
                    break;
                case GamepadButtons.LeftShoulder:
                    currentButton = config.LB;
                    break;
                case GamepadButtons.RightShoulder:
                    currentButton = config.RB;
                    break;
                case GamepadButtons.LeftThumb:
                    currentButton = config.LC;
                    break;
                case GamepadButtons.RightThumb:
                    currentButton = config.RC;
                    break;
            }

            return currentButton;
        }

        private ButtonConfig lastDPadButton = null;
        private void OnDPadPress(object sender, GamepadDPadEventArgs args)
        {
            ButtonConfig currentButton = GetCurrentButton(args.Buttons);

            if (lastDPadButton == null)
            {
                if (currentButton != null)
                {
                    gamepadTranslator.SendButtonDown(currentButton);
                }
                lastDPadButton = currentButton;
            }
            else
            {
                if (lastDPadButton != currentButton)
                {
                    if (lastDPadButton != null)
                    {
                        gamepadTranslator.SendButtonUp(lastDPadButton);
                        
                    }
                    if (currentButton != null)
                    {
                        gamepadTranslator.SendButtonDown(currentButton);
                    }
                    lastDPadButton = currentButton;
                }
            }

        }

        private void OnButtonPress(object sender, GamepadButtonEventArgs args)
        {
            ButtonConfig currentButton = GetCurrentButton(args.Button);

            if (currentButton != null && args.IsPressed)
            {
                gamepadTranslator.SendButtonDown(currentButton);
            }
            else
            {
                gamepadTranslator.SendButtonUp(currentButton);
            }
        }

        private void OnTriggerPress(object sender, GamepadTriggerEventArgs args)
        {
            ButtonConfig currentButton = args.Trigger == GamepadTriggers.Left ? config.LT : config.RT;
            
            if (args.Value > 0)
            {
                gamepadTranslator.SendButtonDown(currentButton);
            }
            else
            {
                gamepadTranslator.SendButtonUp(currentButton);
            }
        }

        void OnThumbUpdate(object sender, GamepadThumbEventArgs evt)
        {
            if (chkON.Checked)
            {
                gamepadTranslator.SendThumbstickChange(evt.XPosition, evt.YPosition,
                    evt.Thumb == GamepadThumbs.Right ? selectedResolution.RightAnalog : selectedResolution.LeftAnalog, evt.Thumb);
            }
        }
        
        private void OnConnect(object sender, GamepadEventArgs evt)
        {
            txtStatus.Text = "Device " + evt.DeviceID + " is connected";
        }

        private void OnDisconnect(object sender, GamepadEventArgs evt)
        {
            txtStatus.Text = "Device " + evt.DeviceID + " is not connected.";
        }
        #endregion

        private void OnTick(object sender, EventArgs evt)
        {
            try
            {
                gamepad.Update();

                if (gamepad.IsConnected && gamepadTranslator != null)
                {
                    gamepadTranslator.Process(selectedResolution);
                }
            }
            catch (Win32Exception e)
            {
                timer.Stop();
                txtStatus.Text = "Error: " + e;
            }
        }

        private void chkON_CheckedChanged(object sender, EventArgs e)
        {
            if (chkON.Checked)
            {
                if (chkTwinstickMode.Checked)
                {
                    basicTranslator.Dispose();
                    twinStickTranslator.Init(this);
                    gamepadTranslator = twinStickTranslator;
                }
                else
                {
                    twinStickTranslator.Dispose();
                    basicTranslator.Init(this);
                    gamepadTranslator = basicTranslator;
                }

                ToGamepadConfiguration();
            }
            else
            {
                if (gamepadTranslator != null)
                {
                    gamepadTranslator.Dispose();
                    gamepadTranslator = null;
                }
            }
        }

        private void UpdateResolutionsOnCombo(GamepadProfile profile)
        {
            resolutions = new List<KeyValuePair<string, ResolutionConfig>>();
            var resData = profile.Resolutions.Select(x => new KeyValuePair<string, ResolutionConfig>(String.Format("{0}x{1}", x.ScreenWidth, x.ScreenHeight), x));
            resolutions.AddRange(resData);
            selResolution.DataSource = new BindingSource(resolutions, null);
        }

        private void selProfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            var profile = ((KeyValuePair<string, GamepadProfile>)selProfile.SelectedItem).Value;
            if (profile != null)
            {
                this.config = profile;
                txtName.Text = profile.Name;
            }

            UpdateResolutionsOnCombo(profile);
            selResolution.DisplayMember = "Key";
            selResolution.ValueMember = "Value";

            buttonsConfig = new List<KeyValuePair<string, ButtonConfig>>();
            buttonsConfig.Add(new KeyValuePair<string, ButtonConfig>("Dpad UP", config.DUp));
            buttonsConfig.Add(new KeyValuePair<string, ButtonConfig>("Dpad DOWN", config.DDown));
            buttonsConfig.Add(new KeyValuePair<string, ButtonConfig>("Dpad LEFT", config.DLeft));
            buttonsConfig.Add(new KeyValuePair<string, ButtonConfig>("Dpad RIGHT", config.DRight));
            buttonsConfig.Add(new KeyValuePair<string, ButtonConfig>("A", config.A));
            buttonsConfig.Add(new KeyValuePair<string, ButtonConfig>("B", config.B));
            buttonsConfig.Add(new KeyValuePair<string, ButtonConfig>("X", config.X));
            buttonsConfig.Add(new KeyValuePair<string, ButtonConfig>("Y", config.Y));
            buttonsConfig.Add(new KeyValuePair<string, ButtonConfig>("Left Trigger", config.LT));
            buttonsConfig.Add(new KeyValuePair<string, ButtonConfig>("Left Bumper", config.LB));
            buttonsConfig.Add(new KeyValuePair<string, ButtonConfig>("Left Click", config.LC));
            buttonsConfig.Add(new KeyValuePair<string, ButtonConfig>("Right Trigger", config.RT));
            buttonsConfig.Add(new KeyValuePair<string, ButtonConfig>("Right Bumper", config.RB));
            buttonsConfig.Add(new KeyValuePair<string, ButtonConfig>("Right Click", config.RC));
            buttonsConfig.Add(new KeyValuePair<string, ButtonConfig>("Select", config.Select));
            buttonsConfig.Add(new KeyValuePair<string, ButtonConfig>("Start", config.Start));

            lstButtons.DataSource = new BindingSource(buttonsConfig, null);
            lstButtons.DisplayMember = "Key";
            lstButtons.ValueMember = "Value";
        }

        private void selResolution_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToGamepadConfiguration();

            var resolution = ((KeyValuePair<string, ResolutionConfig>)selResolution.SelectedItem).Value;
            if (resolution != null)
            {
                this.selectedResolution = resolution;
            }
            if (chkON.Checked)
            {
                ToUIConfiguration();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ToGamepadConfiguration();

            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Json File|*.json";
            saveDialog.Title = "Save the profile";
            saveDialog.ShowDialog();

            if (!String.IsNullOrWhiteSpace(saveDialog.FileName))
            {
                configMan.SaveProfile(config, saveDialog.FileName);
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            ReloadConfigurations();
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            var newResolutionKey = "";

            try
            {
                if (DialogHelper.InputBox("Add Resolution", "Enter new resolution in format <Width>x<Height>", ref newResolutionKey) == System.Windows.Forms.DialogResult.OK)
                {
                    var resData = newResolutionKey.ToLower().Split('x');
                    var newRes = new ResolutionConfig(int.Parse(resData[0]), int.Parse(resData[1]));
                    
                    config.Resolutions.Add(newRes);
                    UpdateResolutionsOnCombo(config);
                }
            }
            catch (Exception ex)
            {
                txtStatus.Text = "Error, invalid format for resolution or is a duplicate resolution; " + ex.Message;
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (gamepadTranslator != null)
            {
                gamepadTranslator.Dispose();
            }
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            var formAbout = new About();
            formAbout.ShowDialog();
        }

        private void lstButtons_SelectedIndexChanged(object sender, EventArgs e)
        {
            var list = sender as ListBox;

            if (list != null)
            {
                var buttonConfig = list.SelectedValue as ButtonConfig;

                if (buttonConfig != null)
                {
                    if (!String.IsNullOrEmpty(buttonConfig.Key))
                    {
                        cboKey.SelectedItem = (ARPGGamepadCore.KeyboardKeys)inputHelper.GetLetterKey(buttonConfig.Key);
                    }
                    else
                    {
                        cboKey.SelectedItem = ARPGGamepadCore.KeyboardKeys.None;
                    }
                    cboMouse.SelectedItem = buttonConfig.MouseClick;
                    cboModifier.SelectedItem = buttonConfig.Modifier;
                    chkToggle.Checked = buttonConfig.Toggle;
                }

            }
        }

        private void cboKey_SelectedIndexChanged(object sender, EventArgs e)
        {
            var combo = sender as ComboBox;

            if (combo != null)
            {
                if (lstButtons.SelectedItem != null)
                {
                    var newConfig = ((ButtonConfig)lstButtons.SelectedValue) with { Key = combo.SelectedValue.ToString() };
                    config.UpdateButtonConfig(newConfig);
                    //((ButtonConfig)lstButtons.SelectedValue).Key = combo.SelectedValue.ToString();
                }
            }

        }

        private void cboMouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            var combo = sender as ComboBox;

            if (combo != null)
            {
                if (lstButtons.SelectedItem != null)
                {
                    var newConfig = ((ButtonConfig)lstButtons.SelectedValue) with { MouseClick = (ARPGGamepadCore.MouseClick)combo.SelectedValue };
                    config.UpdateButtonConfig(newConfig);
                    //((ButtonConfig)lstButtons.SelectedValue).MouseClick = (ARPGGamepadCore.MouseClick)combo.SelectedValue;
                }
            }
        }

        private void cboModifier_SelectedIndexChanged(object sender, EventArgs e)
        {
            var combo = sender as ComboBox;

            if (combo != null)
            {
                if (lstButtons.SelectedItem != null)
                {
                    var newConfig = ((ButtonConfig)lstButtons.SelectedValue) with { Modifier = (ARPGGamepadCore.Modifiers)combo.SelectedValue };
                    config.UpdateButtonConfig(newConfig);
                    //((ButtonConfig)lstButtons.SelectedValue).Modifier = (ARPGGamepadCore.Modifiers)combo.SelectedValue;
                }
            }
        }

        private void chkToggle_CheckedChanged(object sender, EventArgs e)
        {
            var chkbox = sender as CheckBox;

            if (chkbox != null)
            {
                if (lstButtons.SelectedItem != null)
                {
                    var newConfig = ((ButtonConfig)lstButtons.SelectedValue) with { Toggle = chkbox.Checked };
                    config.UpdateButtonConfig(newConfig);
                    //((ButtonConfig)lstButtons.SelectedValue).Toggle = chkbox.Checked;
                }
            }
        }
    }
}
