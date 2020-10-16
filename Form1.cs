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
        int workPeriodMinutes = 1;//20
        int workPeriodSeconds = 5; //0
        string workPeriodLabel = "Work";

        // Rest default values
        int restPeriodMinutes = 0;//20
        int restPeriodSeconds = 5; //0
        string restPeriodLabel = "Rest";

        // Other defaults
        int DefaultRemindSeconds = 30;

        enum Period
        {
            Work,
            Rest,
        }

        int PeriodMinutes, PeriodSeconds, RemindSeconds;
        Period PeriodCurrent;
        bool TimerRunning = false;
        bool HaltTimerOnPeriodEnd = true;
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
                PeriodMinutes = workPeriodMinutes;
                PeriodSeconds = workPeriodSeconds;

                // update GUI elements
                periodLabelDisplay.Text = "- " + workPeriodLabel + " -";
                timeDisplay.Text = FormatPeriodTime(PeriodMinutes, PeriodSeconds);
                restartTimeInput.Value = new DateTime(2000, 1, 1, 1, workPeriodMinutes, workPeriodSeconds);
                UpdateStartEndTimeDisplay();
            }
            else if (p == Period.Rest)
            {
                PeriodCurrent = p;
                PeriodMinutes = restPeriodMinutes;
                PeriodSeconds = restPeriodSeconds;

                // update GUI elements
                periodLabelDisplay.Text = "- " + restPeriodLabel + " -";
                timeDisplay.Text = FormatPeriodTime(PeriodMinutes, PeriodSeconds);
                restartTimeInput.Value = new DateTime(2000, 1, 1, 1, restPeriodMinutes, restPeriodSeconds);
                UpdateStartEndTimeDisplay();
            }
            else
            {
                Console.WriteLine("ERROR, Bad period passed to ChagePeriodTo");
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

            // display start and end time of the period on the GUI
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
                    // This is an error state
                    periodTimer.Stop();
                }

                if (HaltTimerOnPeriodEnd)
                {
                    StopTimerAndDisplay();
                    if (this.WindowState != FormWindowState.Normal)
                    {
                        // unhide window from system tray
                        this.WindowState = FormWindowState.Minimized;
                        Show();
                        notifyIcon.Visible = false;
                    }

                    // Make notify user of period end (Taskbar icon flash & sound)
                    WinFlash.FlashWindow(this.Handle, WinFlash.FlashWindowFlags.FLASHW_ALL |
                            WinFlash.FlashWindowFlags.FLASHW_TIMERNOFG); //TODO: Make window flash optional later
                    PeriodEndSound.Play();
                    // CONSIDER: bring extra attention startend button by selecting it?

                    // Activate Reminder Sound
                    RemindSeconds = DefaultRemindSeconds;
                    remindSoundTimer.Start();
                }
            }
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
                RemindSeconds = DefaultRemindSeconds;
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon.Visible = true;
            }
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
        }
    }
}
