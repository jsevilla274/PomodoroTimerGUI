using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PomodoroTimerForm
{
    public partial class Form1 : Form
    {
        // Work default values
        public int WorkPeriodMinutes = Properties.Settings.Default.WorkPeriodMinutes;
        public int WorkPeriodSeconds = Properties.Settings.Default.WorkPeriodSeconds;
        const string WorkPeriodLabel = "Work";

        // Rest default values
        public int RestPeriodMinutes = Properties.Settings.Default.RestPeriodMinutes;
        public int RestPeriodSeconds = Properties.Settings.Default.RestPeriodSeconds;
        const string RestPeriodLabel = "Rest";

        // Other defaults
        public int RemindSecondsDefault = Properties.Settings.Default.RemindSeconds;
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

        int PeriodMinutes, PeriodSeconds, RemindSeconds;
        Period PeriodCurrent;
        bool TimerRunning = false;
        bool HiddenInSystemTray = false;
        SoundPlayer PeriodEndSound, RemindSound;

        public Form1()
        {
            InitializeComponent();
            ChangePeriodTo(Period.Work);

            // initialize sound resources
            PeriodEndSound = new SoundPlayer();
            PeriodEndSound.Stream = Properties.Resources.LingeringBells;
            RemindSound = new SoundPlayer();
            RemindSound.Stream = Properties.Resources.Ding;
        }

        private void UpdateStartEndTimeDisplay()
        {
            DateTime startTime = DateTime.Now;
            DateTime endTime = startTime.AddMinutes(PeriodMinutes).AddSeconds(PeriodSeconds);
            startTimeDisplay.Text = "Start: " + startTime.ToString("h:mm tt");
            endTimeDisplay.Text = "End: " + endTime.ToString("h:mm tt");
        }

        private string FormatPeriodTime(int minutes, int seconds)
        {
            string minStr = (minutes < 10) ? "0" + minutes.ToString() : minutes.ToString();
            string secStr = (seconds < 10) ? "0" + seconds.ToString() : seconds.ToString();
            return minStr + ':' + secStr;
        }

        private void ChangePeriodTo(Period p)
        {
            if (p == Period.Work)
            {
                PeriodCurrent = p;
                PeriodMinutes = WorkPeriodMinutes;
                PeriodSeconds = WorkPeriodSeconds;

                // update GUI elements
                periodLabelDisplay.Text = "- " + WorkPeriodLabel + " -";
                timeDisplay.Text = FormatPeriodTime(WorkPeriodMinutes, WorkPeriodSeconds);
                restartTimeInput.Value = new DateTime(2000, 1, 1, 1, WorkPeriodMinutes, WorkPeriodSeconds);
                UpdateStartEndTimeDisplay();
            }
            else if (p == Period.Rest)
            {
                PeriodCurrent = p;
                PeriodMinutes = RestPeriodMinutes;
                PeriodSeconds = RestPeriodSeconds;

                // update GUI elements
                periodLabelDisplay.Text = "- " + RestPeriodLabel + " -";
                timeDisplay.Text = FormatPeriodTime(RestPeriodMinutes, RestPeriodSeconds);
                restartTimeInput.Value = new DateTime(2000, 1, 1, 1, RestPeriodMinutes, RestPeriodSeconds);
                UpdateStartEndTimeDisplay();
            }
            else
            {
                Console.WriteLine("ERROR: Bad period passed to ChangePeriodTo");
            }
        }

        private void StartTimerAndDisplay()
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
            UpdateStartEndTimeDisplay();
            startTimeDisplay.Visible = true;
            endTimeDisplay.Visible = true;
        }

        private void StopTimerAndDisplay()
        {
            periodTimer.Stop();
            startpauseButton.Text = "Start";
            TimerRunning = false;
        }

        private void OnKeyIntercept(int keyCode)
        {
            if (keyCode == (int)GlobalStartKey)
            {
                if (!TimerRunning)
                {
                    StartTimerAndDisplay();
                    RemindSound.Play();
                    // WinFlash.StopFlashingWindow(this.Handle); // Does not lower taskbar
                }
            }
        }

        private void startpauseButton_Click(object sender, EventArgs e)
        {
            if (TimerRunning)
            {
                StopTimerAndDisplay();
            }
            else
            {
                StartTimerAndDisplay();
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
            PeriodMinutes = input.Minute;
            PeriodSeconds = input.Second;
            timeDisplay.Text = FormatPeriodTime(PeriodMinutes, PeriodSeconds);
            StartTimerAndDisplay();
        }

        private void periodTimer_Tick(object sender, EventArgs e)
        {
            if (PeriodSeconds > 0)
            {
                PeriodSeconds--;
                timeDisplay.Text = FormatPeriodTime(PeriodMinutes, PeriodSeconds);
            }
            else if (PeriodMinutes > 0)
            {
                PeriodMinutes--;
                PeriodSeconds = 59;
                timeDisplay.Text = FormatPeriodTime(PeriodMinutes, PeriodSeconds);
            }
            else
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
                    Console.WriteLine("ERROR: Current period not recognized");
                    periodTimer.Stop();
                }

                if (PeriodEndStopEnabled)
                {
                    StopTimerAndDisplay();
                    startpauseButton.Select();
                }

                if (HiddenInSystemTray) // unhide window from system tray
                {
                    Show();
                    HiddenInSystemTray = false;
                }

                if (GlobalStartEnabled) // start keyboard hook for start key
                {
                    InterceptKeys.Start(OnKeyIntercept);
                }

                if (WindowFlashEnabled) // flash taskbar icon orange
                {
                    WinFlash.FlashWindow(this.Handle, WinFlash.FlashWindowFlags.FLASHW_ALL |
                        WinFlash.FlashWindowFlags.FLASHW_TIMERNOFG);
                }

                if (PeriodEndSoundEnabled)
                {
                    PeriodEndSound.Play();
                }

                if (RemindEnabled)  // start Remind sound timer
                {
                    RemindSeconds = RemindSecondsDefault;
                    remindSoundTimer.Start();
                }
            }
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            Form settingsForm = new Form2(this);
            settingsForm.ShowDialog();
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
