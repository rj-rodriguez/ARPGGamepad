namespace ARPGGamepadWinForm
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblController = new System.Windows.Forms.Label();
            this.selGamepad = new System.Windows.Forms.ComboBox();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.txtWidthResolution = new System.Windows.Forms.TextBox();
            this.txtHeightResolution = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAspectX = new System.Windows.Forms.TextBox();
            this.txtAspectY = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtOffsetY = new System.Windows.Forms.TextBox();
            this.txtOffsetX = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtRadius = new System.Windows.Forms.TextBox();
            this.cbMouseButton = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cbRMouseButton = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtRRadius = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtROffsetY = new System.Windows.Forms.TextBox();
            this.txtROffsetX = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtRAspectY = new System.Windows.Forms.TextBox();
            this.txtRAspectX = new System.Windows.Forms.TextBox();
            this.chkON = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtDeadzone = new System.Windows.Forms.TextBox();
            this.txtRDeadzone = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.selProfile = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtRInnerRadius = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtInnerRadius = new System.Windows.Forms.TextBox();
            this.chkFixed = new System.Windows.Forms.CheckBox();
            this.chkRFixed = new System.Windows.Forms.CheckBox();
            this.label17 = new System.Windows.Forms.Label();
            this.selResolution = new System.Windows.Forms.ComboBox();
            this.chkPressed = new System.Windows.Forms.CheckBox();
            this.chkRPressed = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnReload = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.btnPlus = new System.Windows.Forms.Button();
            this.chkTwinstickMode = new System.Windows.Forms.CheckBox();
            this.btnAbout = new System.Windows.Forms.Button();
            this.lstButtons = new System.Windows.Forms.ListBox();
            this.pnlButtonConfig = new System.Windows.Forms.Panel();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.lblModifier = new System.Windows.Forms.Label();
            this.lblMouse = new System.Windows.Forms.Label();
            this.lblKeyboard = new System.Windows.Forms.Label();
            this.cboModifier = new System.Windows.Forms.ComboBox();
            this.cboMouse = new System.Windows.Forms.ComboBox();
            this.cboKey = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.cbRMouseKeyboard = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.cbMouseKeyboard = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.cbRMouseModifier = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.cbMouseModifier = new System.Windows.Forms.ComboBox();
            this.chkSpringMode = new System.Windows.Forms.CheckBox();
            this.chkToggle = new System.Windows.Forms.CheckBox();
            this.pnlButtonConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblController
            // 
            this.lblController.AutoSize = true;
            this.lblController.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblController.Location = new System.Drawing.Point(134, 11);
            this.lblController.Name = "lblController";
            this.lblController.Size = new System.Drawing.Size(65, 16);
            this.lblController.TabIndex = 5;
            this.lblController.Text = "Controller";
            // 
            // selGamepad
            // 
            this.selGamepad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selGamepad.FormattingEnabled = true;
            this.selGamepad.Items.AddRange(new object[] {
            "Gamepad 1",
            "Gamepad 2",
            "Gamepad 3",
            "Gamepad 4"});
            this.selGamepad.Location = new System.Drawing.Point(9, 9);
            this.selGamepad.Name = "selGamepad";
            this.selGamepad.Size = new System.Drawing.Size(121, 21);
            this.selGamepad.TabIndex = 17;
            this.selGamepad.SelectedIndexChanged += new System.EventHandler(this.selGamepad_SelectedIndexChanged);
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(7, 429);
            this.txtStatus.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            this.txtStatus.Size = new System.Drawing.Size(472, 20);
            this.txtStatus.TabIndex = 15;
            // 
            // txtWidthResolution
            // 
            this.txtWidthResolution.Location = new System.Drawing.Point(266, 11);
            this.txtWidthResolution.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtWidthResolution.Name = "txtWidthResolution";
            this.txtWidthResolution.ReadOnly = true;
            this.txtWidthResolution.Size = new System.Drawing.Size(68, 20);
            this.txtWidthResolution.TabIndex = 18;
            // 
            // txtHeightResolution
            // 
            this.txtHeightResolution.Location = new System.Drawing.Point(337, 11);
            this.txtHeightResolution.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtHeightResolution.Name = "txtHeightResolution";
            this.txtHeightResolution.ReadOnly = true;
            this.txtHeightResolution.Size = new System.Drawing.Size(68, 20);
            this.txtHeightResolution.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(205, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Resolution";
            // 
            // txtAspectX
            // 
            this.txtAspectX.Location = new System.Drawing.Point(77, 110);
            this.txtAspectX.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtAspectX.Name = "txtAspectX";
            this.txtAspectX.Size = new System.Drawing.Size(68, 20);
            this.txtAspectX.TabIndex = 1;
            this.txtAspectX.Text = "5";
            // 
            // txtAspectY
            // 
            this.txtAspectY.Location = new System.Drawing.Point(148, 110);
            this.txtAspectY.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtAspectY.Name = "txtAspectY";
            this.txtAspectY.Size = new System.Drawing.Size(68, 20);
            this.txtAspectY.TabIndex = 2;
            this.txtAspectY.Text = "5";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 110);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Aspect Ratio";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 133);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Offset";
            // 
            // txtOffsetY
            // 
            this.txtOffsetY.Location = new System.Drawing.Point(148, 133);
            this.txtOffsetY.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtOffsetY.Name = "txtOffsetY";
            this.txtOffsetY.Size = new System.Drawing.Size(68, 20);
            this.txtOffsetY.TabIndex = 4;
            this.txtOffsetY.Text = "-5";
            // 
            // txtOffsetX
            // 
            this.txtOffsetX.Location = new System.Drawing.Point(77, 133);
            this.txtOffsetX.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtOffsetX.Name = "txtOffsetX";
            this.txtOffsetX.Size = new System.Drawing.Size(68, 20);
            this.txtOffsetX.TabIndex = 3;
            this.txtOffsetX.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 153);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Radius";
            // 
            // txtRadius
            // 
            this.txtRadius.Location = new System.Drawing.Point(77, 153);
            this.txtRadius.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtRadius.Name = "txtRadius";
            this.txtRadius.Size = new System.Drawing.Size(68, 20);
            this.txtRadius.TabIndex = 5;
            this.txtRadius.Text = "50";
            // 
            // cbMouseButton
            // 
            this.cbMouseButton.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMouseButton.FormattingEnabled = true;
            this.cbMouseButton.Location = new System.Drawing.Point(77, 201);
            this.cbMouseButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbMouseButton.Name = "cbMouseButton";
            this.cbMouseButton.Size = new System.Drawing.Size(82, 21);
            this.cbMouseButton.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 201);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "Mouse Btn";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 86);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 13);
            this.label6.TabIndex = 22;
            this.label6.Text = "Left Analog";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(256, 86);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 13);
            this.label7.TabIndex = 33;
            this.label7.Text = "Right Analog";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(256, 201);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 13);
            this.label8.TabIndex = 32;
            this.label8.Text = "Mouse Btn";
            // 
            // cbRMouseButton
            // 
            this.cbRMouseButton.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRMouseButton.FormattingEnabled = true;
            this.cbRMouseButton.Location = new System.Drawing.Point(327, 201);
            this.cbRMouseButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbRMouseButton.Name = "cbRMouseButton";
            this.cbRMouseButton.Size = new System.Drawing.Size(82, 21);
            this.cbRMouseButton.TabIndex = 13;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(256, 153);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 13);
            this.label9.TabIndex = 30;
            this.label9.Text = "Radius";
            // 
            // txtRRadius
            // 
            this.txtRRadius.Location = new System.Drawing.Point(327, 153);
            this.txtRRadius.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtRRadius.Name = "txtRRadius";
            this.txtRRadius.Size = new System.Drawing.Size(68, 20);
            this.txtRRadius.TabIndex = 12;
            this.txtRRadius.Text = "550";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(256, 133);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 13);
            this.label10.TabIndex = 28;
            this.label10.Text = "Offset";
            // 
            // txtROffsetY
            // 
            this.txtROffsetY.Location = new System.Drawing.Point(398, 133);
            this.txtROffsetY.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtROffsetY.Name = "txtROffsetY";
            this.txtROffsetY.Size = new System.Drawing.Size(68, 20);
            this.txtROffsetY.TabIndex = 11;
            this.txtROffsetY.Text = "0";
            // 
            // txtROffsetX
            // 
            this.txtROffsetX.Location = new System.Drawing.Point(327, 133);
            this.txtROffsetX.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtROffsetX.Name = "txtROffsetX";
            this.txtROffsetX.Size = new System.Drawing.Size(68, 20);
            this.txtROffsetX.TabIndex = 10;
            this.txtROffsetX.Text = "0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(256, 110);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(68, 13);
            this.label11.TabIndex = 25;
            this.label11.Text = "Aspect Ratio";
            // 
            // txtRAspectY
            // 
            this.txtRAspectY.Location = new System.Drawing.Point(398, 110);
            this.txtRAspectY.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtRAspectY.Name = "txtRAspectY";
            this.txtRAspectY.Size = new System.Drawing.Size(68, 20);
            this.txtRAspectY.TabIndex = 9;
            this.txtRAspectY.Text = "9";
            // 
            // txtRAspectX
            // 
            this.txtRAspectX.Location = new System.Drawing.Point(327, 110);
            this.txtRAspectX.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtRAspectX.Name = "txtRAspectX";
            this.txtRAspectX.Size = new System.Drawing.Size(68, 20);
            this.txtRAspectX.TabIndex = 8;
            this.txtRAspectX.Text = "16";
            // 
            // chkON
            // 
            this.chkON.AutoSize = true;
            this.chkON.Checked = true;
            this.chkON.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkON.Location = new System.Drawing.Point(428, 12);
            this.chkON.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chkON.Name = "chkON";
            this.chkON.Size = new System.Drawing.Size(42, 17);
            this.chkON.TabIndex = 16;
            this.chkON.Text = "ON";
            this.chkON.UseVisualStyleBackColor = true;
            this.chkON.CheckedChanged += new System.EventHandler(this.chkON_CheckedChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 282);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(56, 13);
            this.label12.TabIndex = 35;
            this.label12.Text = "Deadzone";
            // 
            // txtDeadzone
            // 
            this.txtDeadzone.Location = new System.Drawing.Point(77, 282);
            this.txtDeadzone.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtDeadzone.Name = "txtDeadzone";
            this.txtDeadzone.Size = new System.Drawing.Size(68, 20);
            this.txtDeadzone.TabIndex = 7;
            this.txtDeadzone.Text = "0.50";
            // 
            // txtRDeadzone
            // 
            this.txtRDeadzone.Location = new System.Drawing.Point(327, 282);
            this.txtRDeadzone.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtRDeadzone.Name = "txtRDeadzone";
            this.txtRDeadzone.Size = new System.Drawing.Size(68, 20);
            this.txtRDeadzone.TabIndex = 14;
            this.txtRDeadzone.Text = "0.30";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(256, 282);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(56, 13);
            this.label13.TabIndex = 37;
            this.label13.Text = "Deadzone";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(12, 37);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(36, 13);
            this.label14.TabIndex = 39;
            this.label14.Text = "Profile";
            // 
            // selProfile
            // 
            this.selProfile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selProfile.FormattingEnabled = true;
            this.selProfile.Location = new System.Drawing.Point(54, 34);
            this.selProfile.Name = "selProfile";
            this.selProfile.Size = new System.Drawing.Size(179, 21);
            this.selProfile.TabIndex = 0;
            this.selProfile.SelectedIndexChanged += new System.EventHandler(this.selProfile_SelectedIndexChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(256, 176);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(64, 13);
            this.label15.TabIndex = 43;
            this.label15.Text = "InnerRadius";
            // 
            // txtRInnerRadius
            // 
            this.txtRInnerRadius.Location = new System.Drawing.Point(327, 176);
            this.txtRInnerRadius.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtRInnerRadius.Name = "txtRInnerRadius";
            this.txtRInnerRadius.Size = new System.Drawing.Size(68, 20);
            this.txtRInnerRadius.TabIndex = 41;
            this.txtRInnerRadius.Text = "0";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 176);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(64, 13);
            this.label16.TabIndex = 42;
            this.label16.Text = "InnerRadius";
            // 
            // txtInnerRadius
            // 
            this.txtInnerRadius.Location = new System.Drawing.Point(77, 176);
            this.txtInnerRadius.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtInnerRadius.Name = "txtInnerRadius";
            this.txtInnerRadius.Size = new System.Drawing.Size(68, 20);
            this.txtInnerRadius.TabIndex = 40;
            this.txtInnerRadius.Text = "50";
            // 
            // chkFixed
            // 
            this.chkFixed.AutoSize = true;
            this.chkFixed.Location = new System.Drawing.Point(151, 159);
            this.chkFixed.Name = "chkFixed";
            this.chkFixed.Size = new System.Drawing.Size(51, 17);
            this.chkFixed.TabIndex = 44;
            this.chkFixed.Text = "Fixed";
            this.chkFixed.UseVisualStyleBackColor = true;
            // 
            // chkRFixed
            // 
            this.chkRFixed.AutoSize = true;
            this.chkRFixed.Location = new System.Drawing.Point(401, 159);
            this.chkRFixed.Name = "chkRFixed";
            this.chkRFixed.Size = new System.Drawing.Size(51, 17);
            this.chkRFixed.TabIndex = 45;
            this.chkRFixed.Text = "Fixed";
            this.chkRFixed.UseVisualStyleBackColor = true;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(256, 63);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(57, 13);
            this.label17.TabIndex = 46;
            this.label17.Text = "Resolution";
            // 
            // selResolution
            // 
            this.selResolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selResolution.FormattingEnabled = true;
            this.selResolution.Location = new System.Drawing.Point(319, 60);
            this.selResolution.Name = "selResolution";
            this.selResolution.Size = new System.Drawing.Size(86, 21);
            this.selResolution.TabIndex = 47;
            this.selResolution.SelectedIndexChanged += new System.EventHandler(this.selResolution_SelectedIndexChanged);
            // 
            // chkPressed
            // 
            this.chkPressed.AutoSize = true;
            this.chkPressed.Location = new System.Drawing.Point(151, 176);
            this.chkPressed.Name = "chkPressed";
            this.chkPressed.Size = new System.Drawing.Size(92, 17);
            this.chkPressed.TabIndex = 48;
            this.chkPressed.Text = "Keep Pressed";
            this.chkPressed.UseVisualStyleBackColor = true;
            // 
            // chkRPressed
            // 
            this.chkRPressed.AutoSize = true;
            this.chkRPressed.Location = new System.Drawing.Point(400, 178);
            this.chkRPressed.Name = "chkRPressed";
            this.chkRPressed.Size = new System.Drawing.Size(92, 17);
            this.chkRPressed.TabIndex = 49;
            this.chkRPressed.Text = "Keep Pressed";
            this.chkRPressed.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(320, 33);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 50;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnReload
            // 
            this.btnReload.Location = new System.Drawing.Point(401, 33);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(75, 23);
            this.btnReload.TabIndex = 51;
            this.btnReload.Text = "Reload";
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(8, 63);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(67, 13);
            this.label18.TabIndex = 52;
            this.label18.Text = "Profile Name";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(77, 60);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(156, 20);
            this.txtName.TabIndex = 53;
            // 
            // btnPlus
            // 
            this.btnPlus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlus.Location = new System.Drawing.Point(410, 59);
            this.btnPlus.Name = "btnPlus";
            this.btnPlus.Size = new System.Drawing.Size(23, 23);
            this.btnPlus.TabIndex = 54;
            this.btnPlus.Text = "+";
            this.btnPlus.UseVisualStyleBackColor = true;
            this.btnPlus.Click += new System.EventHandler(this.btnPlus_Click);
            // 
            // chkTwinstickMode
            // 
            this.chkTwinstickMode.AutoSize = true;
            this.chkTwinstickMode.Location = new System.Drawing.Point(327, 87);
            this.chkTwinstickMode.Name = "chkTwinstickMode";
            this.chkTwinstickMode.Size = new System.Drawing.Size(105, 17);
            this.chkTwinstickMode.TabIndex = 55;
            this.chkTwinstickMode.Text = "Virtual Aim Mode";
            this.chkTwinstickMode.UseVisualStyleBackColor = true;
            // 
            // btnAbout
            // 
            this.btnAbout.Location = new System.Drawing.Point(239, 33);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(75, 23);
            this.btnAbout.TabIndex = 56;
            this.btnAbout.Text = "About";
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // lstButtons
            // 
            this.lstButtons.FormattingEnabled = true;
            this.lstButtons.Location = new System.Drawing.Point(11, 313);
            this.lstButtons.Name = "lstButtons";
            this.lstButtons.Size = new System.Drawing.Size(120, 108);
            this.lstButtons.TabIndex = 57;
            this.lstButtons.SelectedIndexChanged += new System.EventHandler(this.lstButtons_SelectedIndexChanged);
            // 
            // pnlButtonConfig
            // 
            this.pnlButtonConfig.Controls.Add(this.chkToggle);
            this.pnlButtonConfig.Controls.Add(this.txtNotes);
            this.pnlButtonConfig.Controls.Add(this.lblModifier);
            this.pnlButtonConfig.Controls.Add(this.lblMouse);
            this.pnlButtonConfig.Controls.Add(this.lblKeyboard);
            this.pnlButtonConfig.Controls.Add(this.cboModifier);
            this.pnlButtonConfig.Controls.Add(this.cboMouse);
            this.pnlButtonConfig.Controls.Add(this.cboKey);
            this.pnlButtonConfig.Location = new System.Drawing.Point(137, 313);
            this.pnlButtonConfig.Name = "pnlButtonConfig";
            this.pnlButtonConfig.Size = new System.Drawing.Size(341, 111);
            this.pnlButtonConfig.TabIndex = 58;
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(211, 3);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.ReadOnly = true;
            this.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNotes.Size = new System.Drawing.Size(127, 105);
            this.txtNotes.TabIndex = 6;
            // 
            // lblModifier
            // 
            this.lblModifier.AutoSize = true;
            this.lblModifier.Location = new System.Drawing.Point(3, 66);
            this.lblModifier.Name = "lblModifier";
            this.lblModifier.Size = new System.Drawing.Size(65, 13);
            this.lblModifier.TabIndex = 5;
            this.lblModifier.Text = "Modifier Key";
            // 
            // lblMouse
            // 
            this.lblMouse.AutoSize = true;
            this.lblMouse.Location = new System.Drawing.Point(3, 40);
            this.lblMouse.Name = "lblMouse";
            this.lblMouse.Size = new System.Drawing.Size(39, 13);
            this.lblMouse.TabIndex = 4;
            this.lblMouse.Text = "Mouse";
            // 
            // lblKeyboard
            // 
            this.lblKeyboard.AutoSize = true;
            this.lblKeyboard.Location = new System.Drawing.Point(3, 12);
            this.lblKeyboard.Name = "lblKeyboard";
            this.lblKeyboard.Size = new System.Drawing.Size(52, 13);
            this.lblKeyboard.TabIndex = 3;
            this.lblKeyboard.Text = "Keyboard";
            // 
            // cboModifier
            // 
            this.cboModifier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboModifier.FormattingEnabled = true;
            this.cboModifier.Location = new System.Drawing.Point(75, 63);
            this.cboModifier.Name = "cboModifier";
            this.cboModifier.Size = new System.Drawing.Size(121, 21);
            this.cboModifier.TabIndex = 2;
            this.cboModifier.SelectedIndexChanged += new System.EventHandler(this.cboModifier_SelectedIndexChanged);
            // 
            // cboMouse
            // 
            this.cboMouse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMouse.FormattingEnabled = true;
            this.cboMouse.Location = new System.Drawing.Point(75, 36);
            this.cboMouse.Name = "cboMouse";
            this.cboMouse.Size = new System.Drawing.Size(121, 21);
            this.cboMouse.TabIndex = 1;
            this.cboMouse.SelectedIndexChanged += new System.EventHandler(this.cboMouse_SelectedIndexChanged);
            // 
            // cboKey
            // 
            this.cboKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKey.FormattingEnabled = true;
            this.cboKey.Location = new System.Drawing.Point(75, 9);
            this.cboKey.Name = "cboKey";
            this.cboKey.Size = new System.Drawing.Size(121, 21);
            this.cboKey.TabIndex = 0;
            this.cboKey.SelectedIndexChanged += new System.EventHandler(this.cboKey_SelectedIndexChanged);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(256, 227);
            this.label19.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(52, 13);
            this.label19.TabIndex = 62;
            this.label19.Text = "Keyboard";
            // 
            // cbRMouseKeyboard
            // 
            this.cbRMouseKeyboard.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRMouseKeyboard.FormattingEnabled = true;
            this.cbRMouseKeyboard.Items.AddRange(new object[] {
            "None",
            "LeftClick",
            "MiddleClick",
            "RightClick"});
            this.cbRMouseKeyboard.Location = new System.Drawing.Point(327, 227);
            this.cbRMouseKeyboard.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbRMouseKeyboard.Name = "cbRMouseKeyboard";
            this.cbRMouseKeyboard.Size = new System.Drawing.Size(82, 21);
            this.cbRMouseKeyboard.TabIndex = 60;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(6, 227);
            this.label20.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(52, 13);
            this.label20.TabIndex = 61;
            this.label20.Text = "Keyboard";
            // 
            // cbMouseKeyboard
            // 
            this.cbMouseKeyboard.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMouseKeyboard.FormattingEnabled = true;
            this.cbMouseKeyboard.Items.AddRange(new object[] {
            "None",
            "LeftClick",
            "MiddleClick",
            "RightClick"});
            this.cbMouseKeyboard.Location = new System.Drawing.Point(77, 227);
            this.cbMouseKeyboard.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbMouseKeyboard.Name = "cbMouseKeyboard";
            this.cbMouseKeyboard.Size = new System.Drawing.Size(82, 21);
            this.cbMouseKeyboard.TabIndex = 59;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(256, 253);
            this.label21.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(44, 13);
            this.label21.TabIndex = 66;
            this.label21.Text = "Modifier";
            // 
            // cbRMouseModifier
            // 
            this.cbRMouseModifier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRMouseModifier.FormattingEnabled = true;
            this.cbRMouseModifier.Items.AddRange(new object[] {
            "None",
            "LeftClick",
            "MiddleClick",
            "RightClick"});
            this.cbRMouseModifier.Location = new System.Drawing.Point(327, 253);
            this.cbRMouseModifier.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbRMouseModifier.Name = "cbRMouseModifier";
            this.cbRMouseModifier.Size = new System.Drawing.Size(82, 21);
            this.cbRMouseModifier.TabIndex = 64;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(6, 253);
            this.label22.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(44, 13);
            this.label22.TabIndex = 65;
            this.label22.Text = "Modifier";
            // 
            // cbMouseModifier
            // 
            this.cbMouseModifier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMouseModifier.FormattingEnabled = true;
            this.cbMouseModifier.Items.AddRange(new object[] {
            "None",
            "LeftClick",
            "MiddleClick",
            "RightClick"});
            this.cbMouseModifier.Location = new System.Drawing.Point(77, 253);
            this.cbMouseModifier.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbMouseModifier.Name = "cbMouseModifier";
            this.cbMouseModifier.Size = new System.Drawing.Size(82, 21);
            this.cbMouseModifier.TabIndex = 63;
            // 
            // chkSpringMode
            // 
            this.chkSpringMode.AutoSize = true;
            this.chkSpringMode.Location = new System.Drawing.Point(77, 82);
            this.chkSpringMode.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chkSpringMode.Name = "chkSpringMode";
            this.chkSpringMode.Size = new System.Drawing.Size(86, 17);
            this.chkSpringMode.TabIndex = 67;
            this.chkSpringMode.Text = "Spring Mode";
            this.chkSpringMode.UseVisualStyleBackColor = true;
            // 
            // chkToggle
            // 
            this.chkToggle.AutoSize = true;
            this.chkToggle.Location = new System.Drawing.Point(75, 90);
            this.chkToggle.Name = "chkToggle";
            this.chkToggle.Size = new System.Drawing.Size(89, 17);
            this.chkToggle.TabIndex = 7;
            this.chkToggle.Text = "Toggle Mode";
            this.chkToggle.UseVisualStyleBackColor = true;
            this.chkToggle.CheckedChanged += new System.EventHandler(this.chkToggle_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(494, 455);
            this.Controls.Add(this.chkSpringMode);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.cbRMouseModifier);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.cbMouseModifier);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.cbRMouseKeyboard);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.cbMouseKeyboard);
            this.Controls.Add(this.pnlButtonConfig);
            this.Controls.Add(this.lstButtons);
            this.Controls.Add(this.btnAbout);
            this.Controls.Add(this.chkTwinstickMode);
            this.Controls.Add(this.btnPlus);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.btnReload);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.chkRPressed);
            this.Controls.Add(this.chkPressed);
            this.Controls.Add(this.selResolution);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.chkRFixed);
            this.Controls.Add(this.chkFixed);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.txtRInnerRadius);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.txtInnerRadius);
            this.Controls.Add(this.selProfile);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txtRDeadzone);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtDeadzone);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.chkON);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cbRMouseButton);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtRRadius);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtROffsetY);
            this.Controls.Add(this.txtROffsetX);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtRAspectY);
            this.Controls.Add(this.txtRAspectX);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbMouseButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtRadius);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtOffsetY);
            this.Controls.Add(this.txtOffsetX);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtAspectY);
            this.Controls.Add(this.txtAspectX);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtHeightResolution);
            this.Controls.Add(this.txtWidthResolution);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.lblController);
            this.Controls.Add(this.selGamepad);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "ARPG GamePad Controller";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.pnlButtonConfig.ResumeLayout(false);
            this.pnlButtonConfig.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblController;
        private System.Windows.Forms.ComboBox selGamepad;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.TextBox txtWidthResolution;
        private System.Windows.Forms.TextBox txtHeightResolution;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAspectX;
        private System.Windows.Forms.TextBox txtAspectY;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtOffsetY;
        private System.Windows.Forms.TextBox txtOffsetX;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtRadius;
        private System.Windows.Forms.ComboBox cbMouseButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbRMouseButton;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtRRadius;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtROffsetY;
        private System.Windows.Forms.TextBox txtROffsetX;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtRAspectY;
        private System.Windows.Forms.TextBox txtRAspectX;
        private System.Windows.Forms.CheckBox chkON;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtDeadzone;
        private System.Windows.Forms.TextBox txtRDeadzone;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox selProfile;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtRInnerRadius;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtInnerRadius;
        private System.Windows.Forms.CheckBox chkFixed;
        private System.Windows.Forms.CheckBox chkRFixed;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox selResolution;
        private System.Windows.Forms.CheckBox chkPressed;
        private System.Windows.Forms.CheckBox chkRPressed;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button btnPlus;
        private System.Windows.Forms.CheckBox chkTwinstickMode;
        private System.Windows.Forms.Button btnAbout;
        private System.Windows.Forms.ListBox lstButtons;
        private System.Windows.Forms.Panel pnlButtonConfig;
        private System.Windows.Forms.ComboBox cboModifier;
        private System.Windows.Forms.ComboBox cboMouse;
        private System.Windows.Forms.ComboBox cboKey;
        private System.Windows.Forms.Label lblModifier;
        private System.Windows.Forms.Label lblMouse;
        private System.Windows.Forms.Label lblKeyboard;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ComboBox cbRMouseKeyboard;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ComboBox cbMouseKeyboard;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ComboBox cbRMouseModifier;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.ComboBox cbMouseModifier;
        private System.Windows.Forms.CheckBox chkSpringMode;
        private System.Windows.Forms.CheckBox chkToggle;
    }
}

