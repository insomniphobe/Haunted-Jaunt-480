Team Members:

*Weston*: Lerp and Sound

*Orion*: Dot Product and Particles




***

Sound: Added a radio right next to where you start the game that plays a cool beat i made on a loop

Lerp: When you walk over / away from the radio in the main room, the radio volume (and ambience volume!) gets set based off a Lerp between 0 and the max volume (the control of the lerp being how close/far you are from the radio)



Dot Product: Ghosts and gargoyles now have their shared Observer script on an incrementing alert meter as opposed to instant detection. The rate and distance of detection is different for ghosts and gargoyles, the former being farther distance and quicker detection whereas the latter is the opposite. The detection speed is a function of a set parameter constant speed multiplied with a clamped dot product of the observer's line of sight with the player. Direct sight (dot product of 1) fills the bar much faster than peripheral vision would.

Additionally, for enhanced gameplay experience, a canvas with filling progress bar was added to all observer enemies to visually display how close that enemy is to detecting the player. The bar is color coded from green/yellow/red to indicate danger levels as it fills.

Particles: Added a music note particle system that emits from the radio next to the spawn area.
