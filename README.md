# ARPGGamepad
Gamepad controller simulator for Action RPGs
ARPG Gamepad
Version 2.0

Developed by Roberto Julián Rodríguez Tapia, part of Cute Kick Studio.

I enjoy playing ARPG games (Diablo2, Diablo3, Titan Quest, Path of Exile, etc), 
after having some Carpal Tunnel problems, I had to stop playing exclusively with 
the mouse, rather than stop playing, I started looking at apps that would help me 
translate a gamepad to mouse & keyboard.

No app was comfortable to use, specially for movement, until I found a simple app 
made for Diablo 3, this app however, lacked a way to aim properly and after it 
stopped being updated, I decided it was time to make my own and thus, created this app.
I've had a lot of fun working on and using this app and I plan to continue my work on it.


HELP

1. Requirements.

   You need a gamepad with a driver that supports XInput, an Xbox gamepad should work directly,
you can use a ps3 or ps4 controller provided you install a driver to emulate the Xbox gamepad.

2. How to use

Right now its split into 2 apps, the profile editor for edit/creation of profiles and the main
ARPGGamepad executable where you select a profile and aim mode and does the actual input translation
The intention is that in the next version, the ProfileEditor app will no longer be needed and all 
profile operations can be done in the main app.

Profile Editor

- Use the Reload button in case you made changes to a profile outside of the app while the
app is running, you could also close/open the app.
Analog Setup
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
Buttons Setup
- Just select the the gamepad button on the bottom left list, and then the keyboard key or 
mouse click you want that button to translate to and if it should also apply a modifier key
- Toggle mode checked means the key/click will remain pressed after you release the button, and
it will release the next time you press the button


ARPGGamepad App

- Run the app, select the gamepad, profile and aim mode and click start, 
it should start translating the selected gamepad input into mouse/keyboard inputs according to the selected profile
- Virtual Aim creates a transparent window overlay and uses that to draw a yellow reticle when you aim
with the right analog, only sets the mouse on that position when you press a button, this allows to aim and
move at the same time, though once you press a button movement will stop while the button is pressed.
- Mouse Aim moves the mouse directly to where you aim, so you cannot move while aiming, movement also has
higher priority over aiming.

F. A. Q.

- Can you support other controller drivers?
Since version 2, its going to be a lot easier to support other gamepad drivers rather than just XInput,
however, my priority right now is to add Profile Edit capabilities to the main app

- Can you support other Operating System like linux or OsX?
Again, version 2 makes this possible, but not planned right now

- Can you add a profile for <GameName>?
Not really, unless I have the game and play it. But that's what the profile editor is for.



One note, 2 games I couldn't use this app with are:
Sacred 2, the way the game handles keyboard and mouse input seems to be lower level that the windows event I use,
so it doesn't recognize any inputs created by this app.

Wolcen, it can technically work, but the game already sets up mouse movement on gamepads that cannot be disabled,
so it messes up our mouse movements.