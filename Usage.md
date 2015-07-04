# Requirements #
  * [.NET Framework v2.0](http://www.microsoft.com/downloads/details.aspx?FamilyID=0856eacb-4362-4b0d-8edd-aab15c5e04f5&displaylang=en)
  * XBox 360 wireless or wired controller
  * Windows XP/Vista
  * World of Warcraft (Any Version)

If you have problems try installing [DirectX update](http://www.microsoft.com/downloads/details.aspx?FamilyId=2DA43D38-DB71-4C1B-BC6A-9B6652CD92A3&displaylang=en)

Note that Vista support is not fully tested and not really a recommended by the developers as a primary operating system until fully updated, with say Vista SP3.

# Usage #
The program assumes that your main action bar is configured as default.

## Button mapping ##
```
1 = A
2 = B
3 = X
4 = Y
5 = Left Trigger + A
6 = Left Trigger + B
7 = Left Trigger + X
8 = Left Trigger + Y
9 = Right Trigger + A
0 = Right Trigger + B
- = Right Trigger + X
= = Right Trigger + Y
Left bumper cycles through enemy targets
Right Trigger + Left bumper cycles friendly targets (rev 21)
```
## D-Pad ##
```
Up = Previous Action Bar
Down = Next Action Bar
Left = Map
Right = Bags
```
## Thumb Sticks ##
```
Left thumb is movement
Right Thumb is look
Left thumb button is auto run
Right thumb button is jump
```
## Mouse Mode ##
Right bumper when held places the controller into mouse mode. In mouse mode the right thumb stick will move the mouse cursor, clicking the left thumb stick will left click, clicking the right thumb stick will right click.

The mouse cursor will appear centered above your character. If you flash the mouse cursor by double clicking the right bumper, it will usually interact with the object directly in front of you.
## Preferences ##
http://wow360.googlecode.com/svn/wiki/images/wow360Preferences.JPG
### Button Layout ###
Allows you to choose the function of the left and right bumpers
### Stick Layout ###
Choose which analog stick moves or looks.
### Auto Run Tilde ###
Added the option to set the ~ button as Auto Run. This is useful on laptops, since they don't have the full keyboard. When enabled, you'll need to bind ~ to Auto Run.
### Sticky Bars ###
By default, using the up and down on the D-Pad will scroll through your action bars. Enabling Sticky Bars will require you to pull both triggers in order to scroll through the bars.

An added ability when sticky bars is enabled, is that the D-Pad up and down simulate a pressed Shift and Ctrl key while pressed.  This allows through the use of macros to effectively setup 3 action bars at once, by using the modifier tag.
For example: Button 1 could be used by a hunter, to stop attacking, auto attack and pet attack.
```
/stopattack [modifier:shift]
/autoattack [nomodifier]
/petattack [modifier:ctrl]
```
### Pet Controls ###
By default, holding the left or right trigger and pressing a direction on the DPad will correspond to a pet control (Ctrl + 1-8).  Especially when using sticky keys you may find yourself holding a trigger, and pressing up or down on the DPad to cast something like Shift+5.  Due to the DPad sensitivity this may inadvertently trigger a Ctrl+1, etc.  For this reason (or if you do not control a pet) this option will turn off this ability.
### Assist Mode ###
When enabled, and in a party, pulling both triggers and using the A, B, X, Y buttons will target your party member and assist them. When not enabled, you will simply target your party member.