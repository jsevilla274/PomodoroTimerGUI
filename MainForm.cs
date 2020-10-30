using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PomodoroTimerForm
{
    public partial class MainForm : Form
    {
        // Work default values
        public uint WorkSeconds = Properties.Settings.Default.WorkSeconds;
        const string WorkLabel = "Work";

        // Rest default values
        public uint RestSeconds = Properties.Settings.Default.RestSeconds;
        const string RestLabel = "Rest";

        // Other defaults
        public uint RemindSecondsDefault = Properties.Settings.Default.RemindSeconds;
        public bool RemindEnabled = Properties.Settings.Default.Remind;
        public bool PeriodEndSoundEnabled = Properties.Settings.Default.PeriodEndSound;
        public bool PeriodEndStopEnabled = Properties.Settings.Default.PeriodEndStop;
        public bool GlobalStartEnabled = Properties.Settings.Default.GlobalStart;
        public Keys GlobalStartKey = Properties.Settings.Default.GlobalStartKey;
        public bool WindowFlashEnabled = Properties.Settings.Default.WindowFlash;

        enum Period
        {
            Work,
            Rest,
        }

        enum TimerState
        {
            Start,
            End,
            Pause,
            Restart,
        }

        uint PeriodSeconds, RemindSeconds;
        // TODO int CumulativeWork
        Period PeriodCurrent;
        bool TimerRunning = false;
        bool HiddenInSystemTray = false;
        SoundPlayer PeriodEndSound, RemindSound;
        public List<string> PeriodLog;
        public LogForm RunningLogForm = null;

        public MainForm()
        {
            InitializeComponent();
            ChangePeriodTo(Period.Work);

            // initialize sound resources
            PeriodEndSound = new SoundPlayer();
            PeriodEndSound.Stream = Properties.Resources.LingeringBells;
            RemindSound = new SoundPlayer();
            RemindSound.Stream = Properties.Resources.Ding;

            PeriodLog = new List<string>();
        }

        private void DisplayStartEndTimes()
        {
            DateTime startTime = DateTime.Now;
            DateTime endTime = startTime.AddSeconds(PeriodSeconds);
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

        // what if changeperiodto also had the ability to displaystartendtimes? better, what if
        // we decoupled the GUI changing abilities of changeperiodto and madea new function with
        // display startendtimes that was in charge of only updating the gui after a period change?
        private void ChangePeriodTo(Period p)
        {
            if (p == Period.Work)
            {
                PeriodCurrent = p;
                PeriodSeconds = WorkSeconds;

                // update GUI elements
                periodLabelDisplay.Text = "- " + WorkLabel + " -";
                timeDisplay.Text = FormatTime(WorkSeconds);
                restartTimeInput.Value = new DateTime(2000, 1, 1, 1, (int)(WorkSeconds / 60),
                    (int)(WorkSeconds % 60));
            }
            else if (p == Period.Rest)
            {
                PeriodCurrent = p;
                PeriodSeconds = RestSeconds;

                // update GUI elements
                periodLabelDisplay.Text = "- " + RestLabel + " -";
                timeDisplay.Text = FormatTime(RestSeconds);
                restartTimeInput.Value = new DateTime(2000, 1, 1, 1, (int)(RestSeconds / 60), 
                    (int)(RestSeconds % 60));
            }
            else
            {
                Debug.WriteLine("ERROR: Bad period passed to ChangePeriodTo");
            }
        }

        private void LogPeriod(TimerState state)
        {
            string stateMsg = "";
            if (state == TimerState.Start)
            {
                uint defaultSeconds = 0;
                if (PeriodCurrent == Period.Work)
                {
                    defaultSeconds = WorkSeconds;

                }
                else if (PeriodCurrent == Period.Rest)
                {
                    defaultSeconds = RestSeconds;
                }

                if (PeriodSeconds < defaultSeconds)
                {
                    stateMsg = "period resumed";
                }
                else
                {
                    stateMsg = "period started";
                }

                stateMsg += " (" + FormatTime(PeriodSeconds) + ")";
            }
            else if (state == TimerState.Restart)
            {
                stateMsg = "period restarted (" + FormatTime(PeriodSeconds) + ")";
            }
            else if (state == TimerState.End)
            {
                stateMsg = "period ended";
            }
            else if (state == TimerState.Pause)
            {
                stateMsg = "period paused";
            }

            // cull the log if exceeding some specified amount of entries
            if (PeriodLog.Count > 100)
            {
                PeriodLog.RemoveRange(0, 90);
            }

            // add the entry to the log
            string entry = DateTime.Now.ToString("h:mm:ss tt") + " " + PeriodCurrent.ToString() +
                " " + stateMsg;
            PeriodLog.Add(entry);
            if (RunningLogForm != null)
            {
                RunningLogForm.UpdateLog(entry);
            }
        }

        private void StartAndDisplayTimer()
        {
            periodTimer.Start();
            startpauseButton.Text = "Pause";
            TimerRunning = true;

            // Only components capable of calling this function are able to advance to the next
            // period, so it makes sense they are the only ones that can stop the remindSound
            remindSoundTimer.Stop();

            // Remove hook for global "start" key if necessary
            InterceptKeys.Stop();

            // Display start and end time of the period on the GUI
            DisplayStartEndTimes();
        }

        private void StopAndDisplayTimer()
        {
            periodTimer.Stop();
            startpauseButton.Text = "Start";
            TimerRunning = false;

            // remove start and end time of period from GUI
            startTimeDisplay.Visible = false;
            endTimeDisplay.Visible = false;
        }

        private void OnKeyIntercept(int keyCode)
        {
            if (keyCode == (int)GlobalStartKey)
            {
                if (!TimerRunning)
                {
                    LogPeriod(TimerState.Start);
                    StartAndDisplayTimer();
                    RemindSound.Play();
                    // WinFlash.StopFlashingWindow(this.Handle); // Does not lower taskbar
                }
            }
        }

        private void startpauseButton_Click(object sender, EventArgs e)
        {
            if (TimerRunning)
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
            DateTime input = restartTimeInput.Value;
            PeriodSeconds = ((uint)input.Minute * 60) + (uint)input.Second;
            timeDisplay.Text = FormatTime(PeriodSeconds);
            LogPeriod(TimerState.Restart);
            StartAndDisplayTimer();
        }

        private void periodTimer_Tick(object sender, EventArgs e)
        {
            if (PeriodSeconds > 0)
            {
                PeriodSeconds--;
                timeDisplay.Text = FormatTime(PeriodSeconds);
            }
            else
            {
                if (PeriodEndStopEnabled)
                {
                    // we need to log the period end before changing the period
                    LogPeriod(TimerState.End);
                    StopAndDisplayTimer();
                    startpauseButton.Select();  // highlight the startpause button
                }

                if (PeriodCurrent == Period.Work)
                {
                    ChangePeriodTo(Period.Rest);
                }
                else if (PeriodCurrent == Period.Rest)
                {
                    ChangePeriodTo(Period.Work);
                }
                else
                {
                    Debug.WriteLine("ERROR: Current period not recognized");
                    periodTimer.Stop();
                }

                if (!PeriodEndStopEnabled)
                {
                    // "start" the following period
                    LogPeriod(TimerState.Start);
                    DisplayStartEndTimes();
                }

                if (GlobalStartEnabled) // start keyboard hook for start key
                {
                    InterceptKeys.Start(OnKeyIntercept);
                }

                if (WindowFlashEnabled) 
                {
                    if (HiddenInSystemTray) // unhide window from system tray
                    {
                        Show();
                        HiddenInSystemTray = false;
                    }

                    // flash taskbar icon orange
                    WinFlash.FlashWindow(this.Handle, WinFlash.FlashWindowFlags.FLASHW_ALL |
                        WinFlash.FlashWindowFlags.FLASHW_TIMERNOFG);
                }

                if (PeriodEndSoundEnabled)
                {
                    PeriodEndSound.Play();
                }

                if (RemindEnabled)  
                {
                    // activate remind sound timer
                    RemindSeconds = RemindSecondsDefault;
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
            RunningLogForm = new LogForm(this);
            RunningLogForm.Show();
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            if (PeriodCurrent == Period.Work)
            {
                ChangePeriodTo(Period.Rest);
            }
            else if (PeriodCurrent == Period.Rest)
            {
                ChangePeriodTo(Period.Work);
            }
            else
            {
                periodTimer.Stop();
            }

            if (TimerRunning)
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
            if (RemindSeconds > 0)
            {
                RemindSeconds--;
            }
            else
            {
                RemindSound.Play();
                RemindSeconds = RemindSecondsDefault;
            }
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (HiddenInSystemTray)
            {
                Show();
                this.WindowState = FormWindowState.Normal;
                this.Activate();
                HiddenInSystemTray = false;
            }
            else
            {
                Hide();
                HiddenInSystemTray = true;
            }
        }
    }
}
