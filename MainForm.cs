using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace PomodoroTimer
{
    public partial class MainForm : Form
    {
        // Defaults
        public readonly string PeriodEndSoundName = "lingeringbells.wav";
        public readonly string RemindSoundName = "notify.wav";
        private const int _maxLogEntries = 100;

        private enum Period
        {
            Work,
            Rest,
        }

        private enum TimerState
        {
            Start,
            End,
            Pause,
            Restart,
        }

        // Runtime
        public SoundPlayer PeriodEndSound, RemindSound;
        public LogForm RunningLogForm = null;
        public List<string> PeriodLog;
        public string StartDate = null;
        private uint _periodSeconds, _remindSeconds, _totalWorkSeconds = 0;
        private Period _periodCurrent;
        private bool _timerRunning = false;
        private bool _hiddenInSystemTray = false;

        public MainForm()
        {
            InitializeComponent();

            // set icon defaults for form and notifyicon
            Icon appIcon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            this.Icon = appIcon;
            mainNotifyIcon.Icon = appIcon;
            mainNotifyIcon.Visible = Properties.Settings.Default.NotifyIcon;

            ChangePeriodTo(Period.Work);
            InitializeSoundPlayers();
            PeriodLog = new List<string>();

            if (Properties.Settings.Default.StartTimerOnStartup)
            {
                startpauseButton_Click(null, null);
            }
        }

        /// <summary>
        /// Shows start and end times in h:mm format on the GUI
        /// </summary>
        private void DisplayStartEndTimes()
        {
            DateTime startTime = DateTime.Now;
            DateTime endTime = startTime.AddSeconds(_periodSeconds);
            startTimeDisplay.Text = "Start: " + startTime.ToString("h:mm tt");
            endTimeDisplay.Text = "End: " + endTime.ToString("h:mm tt");
            startTimeDisplay.Visible = true;
            endTimeDisplay.Visible = true;
        } 

        /// <summary>
        /// Formats the seconds into a more human-readible format (MM:SS)
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns>a string of the passed seconds in MM:SS format</returns>
        private string FormatTime(uint seconds)
        {
            uint displayMins = seconds / 60;
            uint displaySecs = seconds % 60;

            string minStr = displayMins.ToString();
            string secStr = (displaySecs < 10) ? "0" + displaySecs.ToString() : displaySecs.ToString();
            return minStr + ':' + secStr;
        }

        /// <summary>
        /// Changes to the given period, updating internal variables as well as certain GUI
        /// elements specific to the given period.
        /// </summary>
        /// <param name="p">Period to change to</param>
        private void ChangePeriodTo(Period p)
        {
            if (p == Period.Work)
            {
                _periodCurrent = p;
                _periodSeconds = Properties.Settings.Default.WorkSeconds;

                // update GUI elements to reflect period
                periodLabelDisplay.Text = "- " + Period.Work.ToString() + " -";
                timeDisplay.Text = FormatTime(Properties.Settings.Default.WorkSeconds);
                restartTimeInput.Value = SecondsToDateTimePicker(Properties.Settings.Default.WorkSeconds);
            }
            else if (p == Period.Rest)
            {
                _periodCurrent = p;
                _periodSeconds = Properties.Settings.Default.RestSeconds;

                // update GUI elements to reflect period
                periodLabelDisplay.Text = "- " + Period.Rest.ToString() + " -";
                timeDisplay.Text = FormatTime(Properties.Settings.Default.RestSeconds);
                restartTimeInput.Value = SecondsToDateTimePicker(Properties.Settings.Default.RestSeconds);
            }
            else
            {
                Debug.WriteLine("ERROR: Bad period passed to ChangePeriodTo");
            }
        }

        /// <summary>
        /// Creates a unique entry in the period log depending on the actual time and TimerState.
        /// Also creates StartDate which indicates when the timer first started
        /// </summary>
        /// <param name="state">State representing when the timer starts, pauses, restarts, or ends 
        /// its period</param>
        private void LogPeriod(TimerState state)
        {
            // set start date if not yet set
            if (StartDate == null)
            {
                StartDate = DateTime.Now.ToString();

                if (RunningLogForm != null)
                {
                    RunningLogForm.SetStartDate(StartDate);
                }
            }

            // determine log entry contents
            string stateMsg = "";
            if (state == TimerState.Start)
            {
                uint defaultSeconds = 0;
                if (_periodCurrent == Period.Work)
                {
                    defaultSeconds = Properties.Settings.Default.WorkSeconds;

                }
                else if (_periodCurrent == Period.Rest)
                {
                    defaultSeconds = Properties.Settings.Default.RestSeconds;
                }

                if (_periodSeconds < defaultSeconds)
                {
                    stateMsg = "period resumed";
                }
                else
                {
                    stateMsg = "period started";
                }

                stateMsg += " (" + FormatTime(_periodSeconds) + ")";
            }
            else if (state == TimerState.Restart)
            {
                stateMsg = "period restarted (" + FormatTime(_periodSeconds) + ")";
            }
            else if (state == TimerState.End)
            {
                stateMsg = "period ended";
            }
            else if (state == TimerState.Pause)
            {
                stateMsg = "period paused";
            }

            // cull the log if exceeding max, leave ten old entries
            if (PeriodLog.Count > _maxLogEntries)
            {
                PeriodLog.RemoveRange(0, _maxLogEntries - 10);
            }

            // add the entry to the log
            string entry = DateTime.Now.ToString("h:mm:ss tt") + " " + _periodCurrent.ToString() +
                " " + stateMsg;
            PeriodLog.Add(entry);
            if (RunningLogForm != null)
            {
                RunningLogForm.UpdateLog(entry);
            }
        }

        /// <summary>
        /// Starts the period timer, updates elements of the GUI related the the timer, and finally
        /// stops resources for background tasks like the remind sound and the global start key
        /// </summary>
        private void StartAndDisplayTimer()
        {
            periodTimer.Start();
            startpauseButton.Text = "Pause";
            _timerRunning = true;

            // Only components capable of calling this function are able to advance to the next
            // period, so it makes sense they are the only ones that can stop the remindSound
            remindSoundTimer.Stop();

            // Remove hook for global "start" key if necessary
            InterceptKeys.Stop();

            // Display start and end time of the period on the GUI
            DisplayStartEndTimes();
        }

        /// <summary>
        /// Stops the period timer and updates elements of the GUI related the the timer
        /// </summary>
        private void StopAndDisplayTimer()
        {
            periodTimer.Stop();
            startpauseButton.Text = "Start";
            _timerRunning = false;

            // remove start and end time of period from GUI
            startTimeDisplay.Visible = false;
            endTimeDisplay.Visible = false;
        }

        /// <summary>
        /// Used by KeyIntercept.Start as the callback method executed when the user presses any
        /// key
        /// </summary>
        /// <param name="keyCode">Keycode of the pressed key</param>
        private void OnKeyIntercept(int keyCode)
        {
            if (keyCode == (int)Properties.Settings.Default.GlobalStartKey)
            {
                if (!_timerRunning)
                {
                    LogPeriod(TimerState.Start);
                    StartAndDisplayTimer(); // implicitly removes keyboard hook,
                                            // and therefore "disables" this method
                    RemindSound.Play();
                    // WinFlash.StopFlashingWindow(this.Handle); // Does not lower taskbar
                }
            }
        }

        /// <summary>
        /// Converts input seconds into a special DateTime object used solely for displaying
        /// period time in DateTimePicker controls
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public DateTime SecondsToDateTimePicker(uint seconds)
        {
            int hours = (int)seconds / 3600;
            int mins = (int)seconds % 3600 / 60;
            int secs = (int)seconds % 60;

            return new DateTime(2000, 1, 1, hours, mins, secs);
        }

        /// <summary>
        /// Converts input DateTime object (from a DateTimePicker used specifically only for user
        /// period time input) into seconds
        /// </summary>
        /// <param name="input">a DateTimePicker's DateTime object</param>
        /// <returns></returns>
        public uint DateTimePickerToSeconds(DateTime input)
        {
            return (uint)input.Hour * 3600 + (uint)input.Minute * 60 + (uint)input.Second;
        }

        private void InitializeSoundPlayers()
        {
            string defaultPESPath = Properties.Settings.Default.PeriodEndSoundPath;
            if (defaultPESPath == PeriodEndSoundName)
            {
                PeriodEndSound = new SoundPlayer(Properties.Resources.LingeringBells);
            }
            else
            {
                if (File.Exists(defaultPESPath))
                {
                    PeriodEndSound = new SoundPlayer(defaultPESPath);
                }
                else
                {
                    // reset the bad/nonexistant path to the default
                    Properties.Settings.Default.PeriodEndSoundPath = PeriodEndSoundName;
                    Properties.Settings.Default.Save();
                    PeriodEndSound = new SoundPlayer(Properties.Resources.LingeringBells);
                    Debug.WriteLine("ERROR: Bad/nonexistant path, resetting period end sound to default");
                }
            }

            string defaultRSPath = Properties.Settings.Default.RemindSoundPath;
            if (defaultRSPath == RemindSoundName)
            {
                RemindSound = new SoundPlayer(Properties.Resources.Notify);
            }
            else
            {
                if (File.Exists(defaultRSPath))
                {
                    RemindSound = new SoundPlayer(defaultRSPath);
                }
                else
                {
                    // reset the bad/nonexistant path to the default
                    Properties.Settings.Default.RemindSoundPath = RemindSoundName;
                    Properties.Settings.Default.Save();
                    RemindSound = new SoundPlayer(Properties.Resources.Notify);
                    Debug.WriteLine("ERROR: Bad/nonexistant path, resetting remind sound to default");
                }
            }
        }

        public void SetNotifyIconVisibility(bool visible)
        {
            mainNotifyIcon.Visible = visible;
            if (_hiddenInSystemTray && !visible)
            {
                // show form to avoid both it and the notifyicon being hidden
                Show();
                this.WindowState = FormWindowState.Normal;
                _hiddenInSystemTray = false;
            }
        }

        private void startpauseButton_Click(object sender, EventArgs e)
        {
            if (_timerRunning)
            {
                // log pause before stopping timer
                LogPeriod(TimerState.Pause);
                StopAndDisplayTimer();
            }
            else
            {
                LogPeriod(TimerState.Start);
                StartAndDisplayTimer();
            }
        }

        /// <summary>
        /// When the restart button is clicked, the timer is reset to the input time until the
        /// end of the period
        /// </summary>
        private void restartButton_Click(object sender, EventArgs e)
        {
            periodTimer.Stop();
            _periodSeconds = DateTimePickerToSeconds(restartTimeInput.Value);
            timeDisplay.Text = FormatTime(_periodSeconds);
            LogPeriod(TimerState.Restart);
            StartAndDisplayTimer();
        }

        private void periodTimer_Tick(object sender, EventArgs e)
        {
            if (_periodSeconds > 0)
            {
                _periodSeconds--;
                timeDisplay.Text = FormatTime(_periodSeconds);

                if (_periodCurrent == Period.Work)
                {
                    _totalWorkSeconds++;
                    if (RunningLogForm != null)
                    {
                        RunningLogForm.UpdateTotalWorkDisplay(_totalWorkSeconds);
                    }
                }
            }
            else
            {
                if (Properties.Settings.Default.PeriodEndStop)
                {
                    // we need to log the period end before changing the period
                    LogPeriod(TimerState.End);
                    StopAndDisplayTimer();
                    startpauseButton.Select();  // highlight the startpause button
                }

                if (_periodCurrent == Period.Work)
                {
                    ChangePeriodTo(Period.Rest);
                }
                else if (_periodCurrent == Period.Rest)
                {
                    ChangePeriodTo(Period.Work);
                }
                else
                {
                    Debug.WriteLine("ERROR: Current period not recognized");
                    periodTimer.Stop();
                }

                if (!Properties.Settings.Default.PeriodEndStop)
                {
                    // "start" the following period
                    LogPeriod(TimerState.Start);
                    DisplayStartEndTimes();
                }

                if (Properties.Settings.Default.GlobalStart) // start keyboard hook for start key
                {
                    InterceptKeys.Start(OnKeyIntercept);
                }

                if (Properties.Settings.Default.WindowFlash) 
                {
                    if (_hiddenInSystemTray) // unhide window from system tray
                    {
                        Show();
                        _hiddenInSystemTray = false;
                    }

                    // flash taskbar icon orange
                    WinFlash.FlashWindow(this.Handle, WinFlash.FlashWindowFlags.FLASHW_ALL |
                        WinFlash.FlashWindowFlags.FLASHW_TIMERNOFG);
                }

                if (Properties.Settings.Default.PeriodEndSound)
                {
                    PeriodEndSound.Play();
                }

                if (Properties.Settings.Default.Remind)  
                {
                    // activate remind sound timer
                    _remindSeconds = Properties.Settings.Default.RemindSeconds;
                    remindSoundTimer.Start();
                }
            }
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            new SettingsForm(this).ShowDialog();
        }

        private void logButton_Click(object sender, EventArgs e)
        {
            RunningLogForm = new LogForm(this, _totalWorkSeconds);
            RunningLogForm.Show();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            // note: we check if the notify icon is visible to ensure the user is not stuck
            // in a state where they can hide the form and not have a way to restore it
            if (Properties.Settings.Default.NotifyIconMinimize && mainNotifyIcon.Visible
                && this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                _hiddenInSystemTray = true;
            }
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            if (_periodCurrent == Period.Work)
            {
                ChangePeriodTo(Period.Rest);
            }
            else if (_periodCurrent == Period.Rest)
            {
                ChangePeriodTo(Period.Work);
            }
            else
            {
                periodTimer.Stop();
            }

            if (_timerRunning)
            {
                LogPeriod(TimerState.Start);
                DisplayStartEndTimes();
            }
        }

        /// <summary>
        /// Play a reminder sound every DefaultRemindSeconds ad infinitum until manually
        /// stopped somehow
        /// </summary>
        private void remindSoundTimer_Tick(object sender, EventArgs e)
        {
            if (_remindSeconds > 0)
            {
                _remindSeconds--;
            }
            else
            {
                RemindSound.Play();
                _remindSeconds = Properties.Settings.Default.RemindSeconds;
            }
        }

        /// <summary>
        /// Show/hides the form on click
        /// </summary>
        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (_hiddenInSystemTray)
            {
                Show();
                this.WindowState = FormWindowState.Normal;
                this.Activate();
                _hiddenInSystemTray = false;
            }
            else
            {
                Hide();
                _hiddenInSystemTray = true;
            }
        }
    }
}
