using System;
using System.Collections.Generic;

namespace ARPGGamepadCore
{


    public class GamepadProfile
    {
        public const int DefaultWidth = 1920;
        public const int DefaultHeight = 1080;

        public string Name { get; set; }
        public List<ResolutionConfig> Resolutions { get; init; }
        public Dictionary<GamepadProfileButtons, ButtonConfig> Buttons { get; init; }

        public ButtonConfig X => Buttons[GamepadProfileButtons.X];
        public ButtonConfig Y => Buttons[GamepadProfileButtons.Y];
        public ButtonConfig A => Buttons[GamepadProfileButtons.A];
        public ButtonConfig B => Buttons[GamepadProfileButtons.B];
        public ButtonConfig DUp => Buttons[GamepadProfileButtons.DUp];
        public ButtonConfig DDown => Buttons[GamepadProfileButtons.DDown];
        public ButtonConfig DLeft => Buttons[GamepadProfileButtons.DLeft];
        public ButtonConfig DRight => Buttons[GamepadProfileButtons.DRight];
        public ButtonConfig LB => Buttons[GamepadProfileButtons.LB];
        public ButtonConfig RB => Buttons[GamepadProfileButtons.RB];
        //Might need more options since they are actually analog
        public ButtonConfig LT => Buttons[GamepadProfileButtons.LT];
        public ButtonConfig RT => Buttons[GamepadProfileButtons.RT];
        //Pressing the analog button
        public ButtonConfig LC => Buttons[GamepadProfileButtons.LC];
        public ButtonConfig RC => Buttons[GamepadProfileButtons.RC];
        public ButtonConfig Select => Buttons[GamepadProfileButtons.Select];
        public ButtonConfig Start => Buttons[GamepadProfileButtons.Start];

        public ResolutionConfig SelectedResolution { get; set; }

        public GamepadProfile(string name, List<ResolutionConfig> resolutions, Dictionary<GamepadProfileButtons, ButtonConfig> buttons) => 
            (Name, Resolutions, Buttons) = (name, resolutions, buttons);

        public GamepadProfile() : this(DefaultWidth, DefaultHeight)
        {
        }

        public GamepadProfile(int screenWidth, int screenHeight)
        {
            Name = "Default";
            Resolutions = new List<ResolutionConfig>
            {
                new ResolutionConfig(screenWidth, screenHeight)
            };
            Buttons = new Dictionary<GamepadProfileButtons, ButtonConfig>();
            Buttons.Add(GamepadProfileButtons.X, new ButtonConfig("X", string.Empty, MouseClick.MiddleClick, Modifiers.Shift, false));
            Buttons.Add(GamepadProfileButtons.Y, new ButtonConfig("Y", string.Empty, MouseClick.RightClick, Modifiers.Shift, false));
            Buttons.Add(GamepadProfileButtons.A, new ButtonConfig("A", string.Empty, MouseClick.None, Modifiers.Alt, false));
            Buttons.Add(GamepadProfileButtons.B, new ButtonConfig("B", "Q", MouseClick.None, Modifiers.Shift, false));
            Buttons.Add(GamepadProfileButtons.Select, new ButtonConfig("Select", "Escape", MouseClick.None, Modifiers.None, false));
            Buttons.Add(GamepadProfileButtons.Start, new ButtonConfig("Start", "D1", MouseClick.None, Modifiers.None, false));
            Buttons.Add(GamepadProfileButtons.DUp, new ButtonConfig("DUp", "D3", MouseClick.None, Modifiers.None, false));
            Buttons.Add(GamepadProfileButtons.DRight, new ButtonConfig("DRight", "D4", MouseClick.None, Modifiers.None, false));
            Buttons.Add(GamepadProfileButtons.DDown, new ButtonConfig("DDown", "D5", MouseClick.None, Modifiers.None, false));
            Buttons.Add(GamepadProfileButtons.DLeft, new ButtonConfig("DLeft", "D2", MouseClick.None, Modifiers.None, false));
            Buttons.Add(GamepadProfileButtons.LB, new ButtonConfig("LB", "W", MouseClick.None, Modifiers.Shift, false));
            Buttons.Add(GamepadProfileButtons.RB, new ButtonConfig("RB", "R", MouseClick.None, Modifiers.Shift, false));
            Buttons.Add(GamepadProfileButtons.LT, new ButtonConfig("LT", "E", MouseClick.None, Modifiers.Shift, false));
            Buttons.Add(GamepadProfileButtons.RT, new ButtonConfig("RT", "T", MouseClick.None, Modifiers.Shift, false));
            Buttons.Add(GamepadProfileButtons.LC, new ButtonConfig("LC", string.Empty, MouseClick.None, Modifiers.Control, false));
            Buttons.Add(GamepadProfileButtons.RC, new ButtonConfig("RC", string.Empty, MouseClick.LeftClick, Modifiers.None, false));
        }

        public void UpdateButtonConfig(ButtonConfig newConfig)
        {
            var key = Enum.Parse<GamepadProfileButtons>(newConfig.Button);
            if (Buttons.ContainsKey(key))
            {
                Buttons[key] = newConfig;
            }
            else
            {
                Buttons.Add(key, newConfig);
            }
        }

    }
}
