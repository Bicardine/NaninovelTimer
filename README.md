# Naninovel Timer
Allows you to set countdown/visual timer to make choice

https://www.youtube.com/watch?v=45EBQyfsXC8&ab_channel=Bicardine

![Bars](https://github.com/Bicardine/NaninovelTimer/assets/83329675/18a34f1c-4cf8-4127-b468-a9b0b88b81fa)

![TimerUIDemo](https://github.com/Bicardine/NaninovelTimer/assets/83329675/74933481-3e18-499d-92bb-1bc2fbffbfc7)


## Requirement
- Naninovel v1.18 or later

## Usage
1. Manage TimerUI in UI Resources:
![Pic1](https://github.com/Bicardine/NaninovelTimer/assets/83329675/e7481e03-981b-49a3-979e-0793e5e90c01)

2. Enabled bars in TimerUI prefab:
![Pic2](https://github.com/Bicardine/NaninovelTimer/assets/83329675/22a64d81-428d-4c2e-89fa-a746920f88d9)


3. Use **@setTimer** command where the **unnamed argument** is the number of seconds to choice and **goto** is the script (scriptName) or part of the script (.scriptName) where you need to go in case of time spent.
![Pic3](https://github.com/Bicardine/NaninovelTimer/assets/83329675/61c4df67-4ff6-4d4b-be6b-f305920bcf81)


4. TimerUI has an optional LerpColorComponent that changes the color of the bar depending on the remaining time.

![Pic4](https://github.com/Bicardine/NaninovelTimer/assets/83329675/b4abee37-b226-42ca-81d0-6951b4f41cfe)


# Adding a new TimerBarRenderer
You can create different bars to display information about the remaining time. It can be either a graph or a number, depending on the implementation.

1. Inherit TimerBarRenderer<T> where T is the value to Render() used for output.
![Pic5](https://github.com/Bicardine/NaninovelTimer/assets/83329675/0b2bad00-54d6-4feb-83e8-ffe8cbd1bc24)


2. Then add the prefab of the new bar in TimerUI and set the previously created class.
![Pic6](https://github.com/Bicardine/NaninovelTimer/assets/83329675/6fd2c85a-ad80-4a15-9678-ea90d860a52e)

