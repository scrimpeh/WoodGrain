### WoodGrain

WinForms based utility for generating a wood grain pattern when 3D printing with Wood-based materials using Simplify3D

The program works by taking a profile configuration file (`*.fff`) as input. It then generates a random wood-grain pattern by adding extruder temperature setpoints every couple of layers. The resulting pattern is then inserted back into the config file and saved.

Currently, only Simplify3D configuration is supported. Additionally, other printing patterns (such as gradients) may be supported in the feature.

**Notes:**
* This program modifies the profile configuration settings of the printer, and can write arbitrary temperate settings into the profile. **Use it at your own risk!**
* Make sure your primary extruder is named "Primary Extruder" in the Simplify3D configuration
* Material and quality presets within the profile can interfere with the program - to be on the safe side, remove all the presets before exporting the profile. This can be done by pressing the "minus" button besides the presets until they're all gone.
