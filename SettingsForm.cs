using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PomodoroTimerForm
{
    public partial class SettingsForm : Form
    {
        public static readonly string PeriodEndSoundDefault = "lingeringbells.wav";
        public static readonly string RemindSoundDefault = "notify.wav";
        private DateTime _workPeriodSaved, _restPeriodSaved;
        private MainForm _parentForm;
        private bool _globalStartEditing = false;
        private string _periodEndSoundPathCurrent;
        private string _remindSoundPathCurrent;
        private Keys _globalStartKeyCurrent;

        public SettingsForm(MainForm parent)
        {
            InitializeComponent();
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            _parentForm = parent;
            InitializeFormDefaults();
        }

        /// <summary>
        /// Initializes form fields with saved user defaults. Note that the default state of the form
        /// is for all text input fields to be empty, checkboxes unchecked, and subfields to be
        /// disabled
        /// </summary>
        private void InitializeFormDefaults()
        {
            // set private variables to default
            _workPeriodSaved = MainForm.SecondsToDateTimePicker(Properties.Settings.Default.WorkSeconds);
            _restPeriodSaved = MainForm.SecondsToDateTimePicker(Properties.Settings.Default.RestSeconds);
            _globalStartKeyCurrent = Properties.Settings.Default.GlobalStartKey;

            // set GUI elements to default
            workPeriodSetting.Value = _workPeriodSaved;
            restPeriodSetting.Value = _restPeriodSaved;

            periodEndSoundSetting.Checked = Properties.Settings.Default.PeriodEndSound;
            InitializeSoundComboBox(periodEndSoundComboBox);
            periodEndSoundSetting_CheckedChanged(null, null); // enable PESound suboptions

            periodEndStopSetting.Checked = Properties.Settings.Default.PeriodEndStop;
            remindSetting.Checked = Properties.Settings.Default.Remind;
            remindSecondsSetting.Value = Properties.Settings.Default.RemindSeconds;
            InitializeSoundComboBox(remindSoundComboBox);
            globalStartSetting.Checked = Properties.Settings.Default.GlobalStart;
            globalStartSettingToolTip.SetToolTip(globalStartSetting, "Allows you to start the " +
                "timer on period end by\npressing a specified key from any application");
            globalStartKeySetting.Text = _globalStartKeyCurrent.ToString();
            periodEndStopSetting_CheckedChanged(null, null);    // enable PES suboptions
           
            windowFlashSetting.Checked = Properties.Settings.Default.WindowFlash;
            notifyIconSetting.Checked = Properties.Settings.Default.NotifyIcon;
            notifyIconSettingToolTip.SetToolTip(notifyIconSetting, "Clicking on the system tray " +
                "icon allows you to hide/unhide\nthe timer, making it disappear/reappear on the taskbar");
            notifyIconMinimizeSetting.Checked = Properties.Settings.Default.NotifyIconMinimize;
            notifyIconSetting_CheckedChanged(null, null);   // enable notifyIconSetting suboptions
        }

        private void InitializeSoundComboBox(ComboBox cbox)
        {
            string currentSoundPath = "";
            string defaultSound = "";
            if (cbox == periodEndSoundComboBox)
            {
                currentSoundPath = Properties.Settings.Default.PeriodEndSoundPath;
                _periodEndSoundPathCurrent = currentSoundPath;
                defaultSound = PeriodEndSoundDefault;
            }
            else if (cbox == remindSoundComboBox)
            {
                currentSoundPath = Properties.Settings.Default.RemindSoundPath;
                _remindSoundPathCurrent = currentSoundPath;
                defaultSound = RemindSoundDefault;
            }
            cbox.Items.Add(defaultSound);
            if (currentSoundPath == defaultSound)
            {
                // select default sound item
                cbox.SelectedIndex = 0;
            }
            else
            {
                // add the custom sound's name as the second item
                cbox.Items.Add(Path.GetFileName(currentSoundPath));
                cbox.SelectedIndex = 1;
            }

            // add open file item and add event handler for it
            cbox.Items.Add("Select a different file...");
            cbox.SelectedIndexChanged += new EventHandler(soundComboBox_SelectedIndexChanged);
        }

        private void periodEndStopSetting_CheckedChanged(object sender, EventArgs e)
        {
            bool enable = periodEndStopSetting.Checked;
            remindSetting.Enabled = enable;
            remindSecondsLabel1.Enabled = enable && remindSetting.Checked;
            remindSecondsSetting.Enabled = enable && remindSetting.Checked;
            remindSecondsLabel2.Enabled = enable && remindSetting.Checked;
            remindSoundLabel.Enabled = enable && remindSetting.Checked;
            remindSoundComboBox.Enabled = enable && remindSetting.Checked;
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
            remindSoundLabel.Enabled = enable;
            remindSoundComboBox.Enabled = enable;
        }

        private void globalStartSetting_CheckedChanged(object sender, EventArgs e)
        {
            bool enable = globalStartSetting.Checked;
            globalStartKeyLabel.Enabled = enable;
            globalStartKeySetting.Enabled = enable;
        }

        private void saveSetting_Click(object sender, EventArgs e)
        {
            if (workPeriodSetting.Value != _workPeriodSaved)
            {
                uint newSeconds = MainForm.DateTimePickerToSeconds(workPeriodSetting.Value);
                Properties.Settings.Default.WorkSeconds = newSeconds;
                _parentForm.WorkSeconds = newSeconds;
            }

            if (restPeriodSetting.Value != _restPeriodSaved)
            {
                uint newSeconds = MainForm.DateTimePickerToSeconds(restPeriodSetting.Value);
                Properties.Settings.Default.RestSeconds = newSeconds;
                _parentForm.RestSeconds = newSeconds;
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
                Properties.Settings.Default.RemindSeconds = (uint)remindSecondsSetting.Value;
                _parentForm.RemindSecondsSaved = (uint)remindSecondsSetting.Value;
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

            // TODO: use public soundplayer properties to set soundplayers instead of cluttering
            // mainform with this logic

            string savedPESPath = Properties.Settings.Default.PeriodEndSoundPath;
            if (periodEndSoundComboBox.SelectedIndex == 0 && savedPESPath != PeriodEndSoundDefault)
            {
                Properties.Settings.Default.PeriodEndSoundPath = PeriodEndSoundDefault;
                _parentForm.SetPeriodEndSound();
            }
            else if (periodEndSoundComboBox.SelectedIndex == 1 && savedPESPath != _periodEndSoundPathCurrent)
            {
                Properties.Settings.Default.PeriodEndSoundPath = _periodEndSoundPathCurrent;
                _parentForm.SetPeriodEndSound(_periodEndSoundPathCurrent);
            }

            string savedRSPath = Properties.Settings.Default.RemindSoundPath;
            if (remindSoundComboBox.SelectedIndex == 0 && savedRSPath != RemindSoundDefault)
            {
                Properties.Settings.Default.RemindSoundPath = RemindSoundDefault;
                _parentForm.SetRemindSound();
            }
            else if (remindSoundComboBox.SelectedIndex == 1 && savedRSPath != _remindSoundPathCurrent)
            {
                Properties.Settings.Default.RemindSoundPath = _remindSoundPathCurrent;
                _parentForm.SetRemindSound(_remindSoundPathCurrent);
            }

            if (notifyIconSetting.Checked != Properties.Settings.Default.NotifyIcon)
            {
                Properties.Settings.Default.NotifyIcon = notifyIconSetting.Checked;
                _parentForm.SetNotifyIconVisibility(notifyIconSetting.Checked);
            }

            if (notifyIconMinimizeSetting.Checked != Properties.Settings.Default.NotifyIconMinimize)
            {
                Properties.Settings.Default.NotifyIconMinimize = notifyIconMinimizeSetting.Checked;
            }

            // close form and save settings to file
            this.Close();
            Properties.Settings.Default.Save();
        }

        private void globalStartKeySetting_Click(object sender, EventArgs e)
        {
            _globalStartEditing = true;
            globalStartKeySetting.Text = "Press any key";
        }

        private void globalStartKeySetting_KeyDown(object sender, KeyEventArgs e)
        {
            if (_globalStartEditing)
            {
                _globalStartEditing = false;
                _globalStartKeyCurrent = e.KeyCode;
                globalStartKeySetting.Text = e.KeyCode.ToString();
            }
        }

        private void soundComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbox = (ComboBox)sender;
            // the last item of the combobox will always be the open file option
            if (cbox.SelectedIndex == cbox.Items.Count - 1)
            {
                OpenFileDialog ofdialog = new OpenFileDialog();
                ofdialog.Filter = "Wave Files (*.wav)|*.wav";
                if (ofdialog.ShowDialog() == DialogResult.OK)
                {
                    if (cbox == periodEndSoundComboBox)
                    {
                        _periodEndSoundPathCurrent = ofdialog.FileName;
                    }
                    else if (cbox == remindSoundComboBox)
                    {
                        _remindSoundPathCurrent = ofdialog.FileName;
                    }

                    // second (1th) item will always be where the "custom" sound is placed
                    if (cbox.Items.Count > 2)
                    {
                        // remove existing custom sound item
                        cbox.Items.RemoveAt(1);
                    }
                    cbox.Items.Insert(1, ofdialog.SafeFileName);
                }
                // select item before last
                cbox.SelectedIndex = cbox.Items.Count - 2;
            }
        }

        private void periodEndSoundSetting_CheckedChanged(object sender, EventArgs e)
        {
            bool enable = periodEndSoundSetting.Checked;
            periodEndSoundLabel.Enabled = enable;
            periodEndSoundComboBox.Enabled = enable;
        }

        private void notifyIconSetting_CheckedChanged(object sender, EventArgs e)
        {
            notifyIconMinimizeSetting.Enabled = notifyIconSetting.Checked;
        }

        private void cancelSetting_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
