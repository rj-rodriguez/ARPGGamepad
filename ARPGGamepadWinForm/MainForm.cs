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
        GamepadProfile config;
        ProfileManager configMan;
        ResolutionConfig selectedResolution;

        private List<KeyValuePair<string, ResolutionConfig>> resolutions;
        private List<KeyValuePair<string, ButtonConfig>> buttonsConfig;

        public MainForm()
        {
            InitializeComponent();

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

            configMan = new ProfileManager(Path.GetDirectoryName(Application.ExecutablePath), Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            txtWidthResolution.Text = configMan.DefaultScreenWidth.ToString();
            txtHeightResolution.Text = configMan.DefaultScreenHeight.ToString();

            ReloadConfigurations();
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
                    cbMouseKeyboard.SelectedItem = (ARPGGamepadCore.KeyboardKeys)ARPGGamepadKeys.GetLetterKey(selectedResolution.LeftAnalog.Button.Key);
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
                    cbRMouseKeyboard.SelectedItem = (ARPGGamepadCore.KeyboardKeys)ARPGGamepadKeys.GetLetterKey(selectedResolution.RightAnalog.Button.Key);
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
                Button = selectedResolution.LeftAnalog.Button == null ? null : selectedResolution.LeftAnalog.Button with
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
                Button = selectedResolution.RightAnalog.Button == null ? null : selectedResolution.RightAnalog.Button with
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

            //ToUIConfiguration();
        }

        private void selResolution_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToGamepadConfiguration();

            var resolution = ((KeyValuePair<string, ResolutionConfig>)selResolution.SelectedItem).Value;
            if (resolution != null)
            {
                this.selectedResolution = resolution;
            }
            ToUIConfiguration();
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
                        cboKey.SelectedItem = (ARPGGamepadCore.KeyboardKeys)ARPGGamepadKeys.GetLetterKey(buttonConfig.Key);
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
