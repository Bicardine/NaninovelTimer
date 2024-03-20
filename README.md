# Naninovel Timer
Allows you to set countdown/visual timer to make choice

![Bars](https://github.com/Bicardine/NaninovelTimer/assets/83329675/54e0217d-c858-44dc-942a-5b3db337baaa)

[https://www.youtube.com/watch?v=45EBQyfsXC8&](https://www.youtube.com/watch?v=45EBQyfsXC8&)

## Requirement
- Naninovel v1.18 or later

## Usage
1. Manage TimerUI in UI Resources:
![Pic1](https://github.com/Bicardine/NaninovelTimer/assets/83329675/fe90c57c-574f-49ab-aed3-e0383ffc4737)

2. Enabled bars in TimerUI prefab:
![Pic2](https://github.com/Bicardine/NaninovelTimer/assets/83329675/aa1bf17b-7d20-4842-b32c-26e47c464b35)

4. Use **@setTimer** command where the **unnamed argument** is the number of seconds to choice and **goto** is the script (scriptName) or part of the script (.scriptName) where you need to go in case of time spent.
![Pic3](https://github.com/Bicardine/NaninovelTimer/assets/83329675/e4d11025-d90d-4b4f-9fab-30183b4a9725)

5. TimerUI has an optional LerpColorComponent that changes the color of the bar depending on the remaining time.
![Pic4](https://github.com/Bicardine/NaninovelTimer/assets/83329675/eb4e4bfe-3126-457e-b531-f03614d74081)

# Adding a new TimerBarRenderer.
You can create different bars to display information about the remaining time. It can be either a graph or a number, depending on the implementation.

1. Inherit TimerBarRenderer<T> where T is the value to Render() used for output.
![Pic5](https://github.com/Bicardine/NaninovelTimer/assets/83329675/94173eed-b33e-4cfe-a455-5a4a7cf4c0f5)

2. Then add the prefab of the new bar in TimerUI and set the previously created class.
![Pic7](https://github.com/Bicardine/NaninovelTimer/assets/83329675/9c145a1d-3937-492d-a0e3-06d5fa2c5a68)
