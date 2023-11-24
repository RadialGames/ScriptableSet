ScriptableSet
===========

## Concept

This is a Unity Package Manager (UPM)-compatible repository for generating a set of ScriptableObjects in Unity.

The class contains several helper functions for cycling through the set, randomizing the set, and other common list-
like functions.

For example, this class could be used to store a list of several "footstep" sound effects, which you could randomly
pull from to play a different sound effect each time a character takes a step. You could easily swap out this set
for different effects for different floor types or characters.

## Requirements

- Tested in Unity 2019.4.0f1, should work in anything newer.

## Installation

Install it via the Unity Package Manager by:
- Opening your project in Unity
- Open the Package Manager window (`Window > Package Manager`)
- Click the `+` button in the top left corner of the window
- Select `Add package from git URL...`
- Enter the following url, and you'll be up to date: `https://github.com/RadialGames/ScriptableSet.git`

## Usage

All files in this package are in the `Radial.ScriptableSet` namespace. Access them by adding the following to the top of your
files:

```c#
using Radial.ScriptableSet;
```

It's common that you want to create a set of items in a ScriptableObject. This library provides a `ScriptableSet<T>`
class that you can inherit from that simplifies this process.

Lets say you want to provide a set of Unity sound effects, provided as a list of `AudioClip`s. You would typically
create this as a MonoBehaviour component, where you could use the inspector to edit a public field:

```C#
public class MyClass : MonoBehaviour { 
    public List<AudioClip> soundEffects;
}
```

But what if you want to swap out these effects dynamically, or store the configuration of the clips in a
ScriptableObject? Now you have a bunch of overhead code to write! Instead, you can use this class `ScriptableSet<T>`:

```C#
[CreateAssetMenu(fileName = "CustomAudioClips", menuName = "Audio/CustomAudioClips")]
public class CustomAudioClips : ScriptableSet<AudioClip> { } 
```

Now you can create a ScriptableObject and it will come with several helper functions:

- `Add()` - Add an item to the set
- `Remove()` - Remove an item from the set
- `GetNext()` - Gets the next item from the set

`GetNext()` will cycle through the set, looping to the start when complete. You can optionally configure the
ScriptableObject to be Randomized, Random with No Repeats, or Sequential.

## Credits

Inspired by Unite Austin 2017 talk by [Ryan Hipple](https://github.com/roboryantron):
https://www.youtube.com/watch?v=raQ3iHhE_Kk

Source code originally forked from: https://github.com/roboryantron/Unite2017