# Pomodoro Timer GUI
A Pomodoro timer desktop application with a GUI for Windows. Serves as a major update to my previous [console-based pomodoro timer](https://github.com/jsevilla274/PomodoroTimer).

## What is a "pomodoro timer"?
Directly quoted from [Wikipedia](https://en.wikipedia.org/wiki/Pomodoro_Technique):

> The **Pomodoro Technique** is a time management method developed by Francesco Cirillo in the late 1980s. The technique uses a timer to break down work into intervals, traditionally 25 minutes in length, separated by short breaks. Each interval is known as a _pomodoro_, from the Italian word for tomato, after the tomato-shaped kitchen timer that Cirillo used as a university student.

A pomodoro timer is an extension of this technique, serving as a productivity tool that keeps track of these intervals and notifies the user when one has completed.

## Features

 - Set custom **Work** and **Rest** period times
 - Skip to next period any time
 - **Restart** a period with a specified time
 - Log timer events with timestamps
- Set a **global "Start" timer key** that lets you start the next period from any application
 - Enable **sounds** to notify you of a period's end
 - Use custom user sound files (.wavs)
 - Hide application window by clicking on the system tray icon

## Usage
Currently the only method of running the application is to clone and build on Visual Studio. The base timer functionality is largely self-explanatory and should work without needing to modify any settings.

### Custom Sounds
To set custom "period end" or "reminder" sounds, you will want to go to the "Settings" and access the last option of the respective combo box. This will open a File Picker dialog which will allow you to pick any Wave Sound File (.wav) to use. To commit your changes in the Settings window, always click the "Save" button.

### Global "Start" Key
The global start key allows you to proceed to the next period **after a period has ended** from any application. This works on a keyboard hook that is set on a period's end if enabled. In order to modify the key, click on the textbox next to the "Global Start Key" setting and press the desired key (the default is "Insert"). It's recommended you pick an innocuous key which has a low chance of colliding with other open applications. To commit your change, click the "Save" button.

## Credits
- FreeSound user [InfiniteLifespan](https://freesound.org/people/InfiniteLifespan/) for the default timer sounds 
- Artist [Double-J Design](https://iconarchive.com/artist/double-j-design.html) for the clock icon
