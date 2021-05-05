using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using ARPGGamepadCore;

namespace ARPGGamepadWinForm
{    
    public partial class MainForm : Form
    {
        private GamepadProfile profile;
        private ProfileManager profileMan;
        private ResolutionConfig selectedResolution;

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

            profileMan = new ProfileManager(Path.GetDirectoryName(Application.ExecutablePath), Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            txtWidthResolution.Text = profileMan.DefaultScreenWidth.ToString();
            txtHeightResolution.Text = profileMan.DefaultScreenHeight.ToString();

            ReloadConfigurations();
            ToGamepadConfiguration();
        }

        private void ReloadConfigurations()
        {
            profileMan.ReloadProfiles();
            var profiles = profileMan.Profiles.Select(item => new KeyValuePair<string, GamepadProfile>(item.Value.Name, item.Value)).ToList();
            selProfile.DataSource = new BindingSource(profiles, null);
        }

        private void ToUIConfiguration()
        {
            txtName.Text = profile.Name;
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
                if (!string.IsNullOrEmpty(selectedResolution.LeftAnalog.Button.Key))
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
                if (!string.IsNullOrEmpty(selectedResolution.RightAnalog.Button.Key))
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
            var index = profile.Resolutions.FindIndex(x => x.Name == selectedResolution.Name);
            profile.Resolutions[index] = selectedResolution;
            profile.Name = txtName.Text;
        }


        private void UpdateResolutionsOnCombo(GamepadProfile profile)
        {
            resolutions = new List<KeyValuePair<string, ResolutionConfig>>();
            var resData = profile.Resolutions.Select(x => new KeyValuePair<string, ResolutionConfig>(x.Name, x)).ToList();
            resolutions.AddRange(resData);
            selResolution.DataSource = new BindingSource(resolutions, null);
        }

        private void selProfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            var profile = ((KeyValuePair<string, GamepadProfile>)selProfile.SelectedItem).Value;
            profile.SelectedResolution = null;
            if (profile != null)
            {
                this.profile = profile;
                txtName.Text = profile.Name;
            }
            UpdateResolutionsOnCombo(profile);

            buttonsConfig = new List<KeyValuePair<string, ButtonConfig>>();
            buttonsConfig.Add(new KeyValuePair<string, ButtonConfig>("Dpad UP", this.profile.DUp));
            buttonsConfig.Add(new KeyValuePair<string, ButtonConfig>("Dpad DOWN", this.profile.DDown));
            buttonsConfig.Add(new KeyValuePair<string, ButtonConfig>("Dpad LEFT", this.profile.DLeft));
            buttonsConfig.Add(new KeyValuePair<string, ButtonConfig>("Dpad RIGHT", this.profile.DRight));
            buttonsConfig.Add(new KeyValuePair<string, ButtonConfig>("A", this.profile.A));
            buttonsConfig.Add(new KeyValuePair<string, ButtonConfig>("B", this.profile.B));
            buttonsConfig.Add(new KeyValuePair<string, ButtonConfig>("X", this.profile.X));
            buttonsConfig.Add(new KeyValuePair<string, ButtonConfig>("Y", this.profile.Y));
            buttonsConfig.Add(new KeyValuePair<string, ButtonConfig>("Left Trigger", this.profile.LT));
            buttonsConfig.Add(new KeyValuePair<string, ButtonConfig>("Left Bumper", this.profile.LB));
            buttonsConfig.Add(new KeyValuePair<string, ButtonConfig>("Left Click", this.profile.LC));
            buttonsConfig.Add(new KeyValuePair<string, ButtonConfig>("Right Trigger", this.profile.RT));
            buttonsConfig.Add(new KeyValuePair<string, ButtonConfig>("Right Bumper", this.profile.RB));
            buttonsConfig.Add(new KeyValuePair<string, ButtonConfig>("Right Click", this.profile.RC));
            buttonsConfig.Add(new KeyValuePair<string, ButtonConfig>("Select", this.profile.Select));
            buttonsConfig.Add(new KeyValuePair<string, ButtonConfig>("Start", this.profile.Start));

            lstButtons.DataSource = new BindingSource(buttonsConfig, null);
            lstButtons.DisplayMember = "Key";
            lstButtons.ValueMember = "Value";
        }

        private void selResolution_SelectedIndexChanged(object sender, EventArgs e)
        {
            var resolution = ((KeyValuePair<string, ResolutionConfig>)selResolution.SelectedItem).Value;
            if (resolution != null)
            {
                this.selectedResolution = resolution;
            }
            ToUIConfiguration();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ToGamepadConfiguration();
            var profileName = ((KeyValuePair<string, GamepadProfile>)selProfile.SelectedItem).Key;
            var fileName = profileMan.GetProfileFilename(profileName);
            profileMan.SaveProfile(profile, fileName, true);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ToGamepadConfiguration();

            SaveFileDialog saveDialog = new()
            {
                Filter = "Json File|*.json",
                Title = "Save the profile"
            };
            saveDialog.ShowDialog();

            if (!string.IsNullOrWhiteSpace(saveDialog.FileName))
            {
                profileMan.SaveProfile(profile, saveDialog.FileName);
                ReloadConfigurations();
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
                    
                    profile.Resolutions.Add(newRes);
                    UpdateResolutionsOnCombo(profile);
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
                    if (!string.IsNullOrEmpty(buttonConfig.Key))
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
                    profile.UpdateButtonConfig(newConfig);
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
                    profile.UpdateButtonConfig(newConfig);
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
                    profile.UpdateButtonConfig(newConfig);
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
                    profile.UpdateButtonConfig(newConfig);
                }
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            var txtName = sender as TextBox;
            if (profileMan.Profiles.ContainsKey(txtName.Text))
            {
                btnSave.Enabled = false;
                btnUpdate.Enabled = true;
            }
            else
            {
                btnSave.Enabled = true;
                btnUpdate.Enabled = false;
            }
        }
    }
}
