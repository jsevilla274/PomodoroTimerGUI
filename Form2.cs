using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PomodoroTimerForm
{
    public partial class Form2 : Form
    {
        private DateTime _workPeriodSaved, _restPeriodSaved;
        private Form1 _parentForm;
        private bool _globalStartEdit = false;
        private Keys _globalStartKeyCurrent;

        public Form2(Form1 parent)
        {
            _parentForm = parent;
            InitializeComponent();
            _workPeriodSaved = new DateTime(2000, 1, 1, 1,
                Properties.Settings.Default.WorkPeriodMinutes,
                Properties.Settings.Default.WorkPeriodSeconds);
            _restPeriodSaved = new DateTime(2000, 1, 1, 1,
                Properties.Settings.Default.RestPeriodMinutes,
                Properties.Settings.Default.RestPeriodSeconds);
            _globalStartKeyCurrent = Properties.Settings.Default.GlobalStartKey;
            InitializeDefaults();
        }

        /// <summary>
        /// Initializes form fields with user defaults. Note that the default state of the form
        /// is for all text input fields to be empty, checkboxes unchecked, and subfields to be
        /// disabled
        /// </summary>
        private void InitializeDefaults()
        {
            workPeriodSetting.Value = _workPeriodSaved;
            restPeriodSetting.Value = _restPeriodSaved;
            periodEndSoundSetting.Checked = Properties.Settings.Default.PeriodEndSound;

            if (Properties.Settings.Default.PeriodEndStop)
            {
                periodEndStopSetting.Checked = true;
                remindSetting.Enabled = true;
                globalStartSetting.Enabled = true;
            }

            if (Properties.Settings.Default.Remind)
            {
                remindSetting.Checked = true;
                remindSecondsLabel1.Enabled = true;
                remindSecondsSetting.Enabled = true;
                remindSecondsLabel2.Enabled = true;
            }
            remindSecondsSetting.Value = Properties.Settings.Default.RemindSeconds;
            globalStartSetting.Checked = Properties.Settings.Default.GlobalStart;
            globalStartKeySetting.Text = _globalStartKeyCurrent.ToString();
            windowFlashSetting.Checked = Properties.Settings.Default.WindowFlash;
        }

        private void UpdateSettings()
        { 
            if (workPeriodSetting.Value != _workPeriodSaved)
            {
                Properties.Settings.Default.WorkPeriodMinutes = workPeriodSetting.Value.Minute;
                _parentForm.WorkPeriodMinutes = workPeriodSetting.Value.Minute;
                Properties.Settings.Default.WorkPeriodSeconds = workPeriodSetting.Value.Second;
                _parentForm.WorkPeriodSeconds = workPeriodSetting.Value.Second;
            }

            if (restPeriodSetting.Value != _restPeriodSaved)
            {
                Properties.Settings.Default.RestPeriodMinutes = restPeriodSetting.Value.Minute;
                _parentForm.RestPeriodMinutes = restPeriodSetting.Value.Minute;
                Properties.Settings.Default.RestPeriodSeconds = restPeriodSetting.Value.Second;
                _parentForm.RestPeriodSeconds = restPeriodSetting.Value.Second;
            }

            if (periodEndSoundSetting.Checked != Properties.Settings.Default.PeriodEndSound)
            {
                Properties.Settings.Default.PeriodEndSound = periodEndSoundSetting.Checked;
                _parentForm.PeriodEndSoundEnabled = periodEndSoundSetting.Checked;
            }

            if (periodEndStopSetting.Checked != Properties.Settings.Default.PeriodEndStop)
            {
                Properties.Settings.Default.PeriodEndStop = periodEndStopSetting.Checked;
                _parentForm.PeriodEndStopEnabled = periodEndStopSetting.Checked;
            }

            if (remindSetting.Checked != Properties.Settings.Default.Remind)
            {
                Properties.Settings.Default.Remind = remindSetting.Checked;
                _parentForm.RemindEnabled = remindSetting.Checked;
            }

            if ((int)remindSecondsSetting.Value != Properties.Settings.Default.RemindSeconds)
            {
                Properties.Settings.Default.RemindSeconds = (int)remindSecondsSetting.Value;
                _parentForm.RemindSecondsDefault = (int)remindSecondsSetting.Value;
            }

            if (globalStartSetting.Checked != Properties.Settings.Default.GlobalStart)
            {
                Properties.Settings.Default.GlobalStart = globalStartSetting.Checked;
                _parentForm.GlobalStartEnabled = globalStartSetting.Checked;
            }

            if (_globalStartKeyCurrent != Properties.Settings.Default.GlobalStartKey)
            {
                Properties.Settings.Default.GlobalStartKey = _globalStartKeyCurrent;
                _parentForm.GlobalStartKey = _globalStartKeyCurrent;
            }

            if (windowFlashSetting.Checked != Properties.Settings.Default.WindowFlash)
            {
                Properties.Settings.Default.WindowFlash = windowFlashSetting.Checked;
                _parentForm.WindowFlashEnabled = windowFlashSetting.Checked;
            }
        }

        private void periodEndStopSetting_CheckedChanged(object sender, EventArgs e)
        {
            bool enable = periodEndStopSetting.Checked;
            remindSetting.Enabled = enable;
            remindSecondsLabel1.Enabled = enable && remindSetting.Checked;
            remindSecondsSetting.Enabled = enable && remindSetting.Checked;
            remindSecondsLabel2.Enabled = enable && remindSetting.Checked;
            globalStartSetting.Enabled = enable;
            globalStartKeyLabel.Enabled = enable && globalStartSetting.Checked;
            globalStartKeySetting.Enabled = enable && globalStartSetting.Checked;
        }

        private void remindSetting_CheckedChanged(object sender, EventArgs e)
        {
            bool enable = remindSetting.Checked;
            remindSecondsLabel1.Enabled = enable;
            remindSecondsSetting.Enabled = enable;
            remindSecondsLabel2.Enabled = enable;
        }

        private void globalStartSetting_CheckedChanged(object sender, EventArgs e)
        {
            bool enable = globalStartSetting.Checked;
            globalStartKeyLabel.Enabled = enable;
            globalStartKeySetting.Enabled = enable;
        }

        private void saveSetting_Click(object sender, EventArgs e)
        {
            UpdateSettings();
            this.Close();
            Properties.Settings.Default.Save();
        }

        private void globalStartKeySetting_Click(object sender, EventArgs e)
        {
            _globalStartEdit = true;
            globalStartKeySetting.Text = "Press any key";
        }

        private void globalStartKeySetting_KeyDown(object sender, KeyEventArgs e)
        {
            if (_globalStartEdit)
            {
                _globalStartEdit = false;
                _globalStartKeyCurrent = e.KeyCode;
                globalStartKeySetting.Text = e.KeyCode.ToString();
            }
        }

        private void cancelSetting_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
