﻿ARPG Gamepad Controller
Version 1.2.0

Developed by Roberto Julián Rodríguez Tapia, part of Cute Kick Studio.

Check us at www.cutekickstudio.com

I enjoy playing ARPG games (Diablo2, Diablo3, Titan Quest, Path of Exile, etc), 
after having some Carpal Tunnel problems, I had to stop playing exclusively with 
the mouse, rather than stop playing, I started looking at apps that would help me 
translate a gamepad to mouse & keyboard.

No app was comfortable to use, specially for movement, until I found a simple app 
made for Diablo 3, this app however, lacked a way to aim properly and after it 
stopped being updated, I decided it was time to make my own and thus, created this app.
I've had a lot of fun working on and using this app and I plan to continue my work on it.


This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
See the GNU General Public License for more details.
You should have received a copy of the GNU General Public License along with this program.  
If not, see <http://www.gnu.org/licenses/>.



HELP

1. Requirements.

   You need a gamepad with a driver that supports XInput, an Xbox gamepad should work directly,
you can use a ps3 or ps4 controller provided you install a driver to emulate the Xbox gamepad.
   DirectX for Virtual Aim Mode, this uses a directx window overlay, at this point, no one should
have any problems with this.

2. How to use

   There's a controller combobox, choose the controller you want to translate its input, this
is just in case there's multiple gamepads on windows so you can choose which one.
   While the ON checkbox is checked, the app will be translating the gamepad input using the
current profile, any changes in the Analogs configuration require a reset of the ON checkbox.
   Changing the profile will automatically load its settings, this doesn't require to reset
the ON checkbox.
   Use the Reload button in case you make changes to a profile outside of the app while the
app is running, you could also close/open the app.
   
   Analog Configuration

- Aspect Ratio. For aiming this should map the aspect raito of the current resolution, but it 
can be customized to the user needs, this indicates the shape of the ellipsis described 
while moving the analog.
- Offset. Set the X & Y offset for the center of the ellipsis described by the Analog, this is
in case the game has the player offcenter (usually a bit higher than the actual center).
- Radius. The Radius of the ellipsis in pixels for the max value of the analog. Relative to screen 
height, which then applies the aspect ratio to calculate the value on the width.
- Fixed. This indicates if the ellipsis uses the Radius for any value of the Analog or if it
should translate the Analog current axis value into a proportional value of the Radius.
- Inner Radius. When Fixed is unchecked, indicates the minimum value of the radius when
calculating the Analog current value.
- Keep Pressed. If checked the mouse button assigned to the analog will remain pressed
while you move the analog, otherwise it will do continuous clicks.
- Mouse Btn. The button in the mouse to press while the analog is above the Deadzone value.
- Deadzone. A value from 0 t 1, to indicate the minimum the analog needs to move to start
translating its value to Mouse.


F. A. Q.

Initial release, so there are no frequently asked questions yet.




Notes

There's currently 2 profiles with the app, one for Path of Exile and another one for Heroes of
the Storm, which are the games I am currently playing with this app, note that for 
Heroes of the Storm I highly recommend using a PS4 gamepad, as you'll need the trackpad
to select talents, other than that, plays great with virtual aim mode active.