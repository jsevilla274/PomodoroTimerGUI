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
        private DateTime _workPeriodSaved, _restPeriodSaved;
        private MainForm _parentForm;
        private bool _globalStartEditing = false;
        private string _periodEndSoundPathCurrent;
        private const string _periodEndSoundDefault = "lingeringbells.wav";
        private Keys _globalStartKeyCurrent;

        public SettingsForm(MainForm parent)
        {
            InitializeComponent();
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            _parentForm = parent;
            InitializeDefaults();

            // init periodend combobox
            periodEndSoundComboBox.Items.Add(_periodEndSoundDefault);
            _periodEndSoundPathCurrent = Properties.Settings.Default.PeriodEndSoundPath;
            if (_periodEndSoundPathCurrent != _periodEndSoundDefault)
            { 
                if (File.Exists(_periodEndSoundPathCurrent))
                {
                    // add custom sound item and select it 
                    periodEndSoundComboBox.Items.Add(Path.GetFileName(_periodEndSoundPathCurrent));
                    periodEndSoundComboBox.SelectedIndex = 1;
                }
                else
                {
                    // select default sound item & remove invalid path from settings
                    periodEndSoundComboBox.SelectedIndex = 0;
                    Properties.Settings.Default.PeriodEndSoundPath = _periodEndSoundDefault;
                    Properties.Settings.Default.Save();
                }
            }
            else
            {
                // select default sound item
                periodEndSoundComboBox.SelectedIndex = 0;
            }
            periodEndSoundComboBox.Items.Add("Select a different file...");
            
        }

        /// <summary>
        /// Initializes form fields with saved user defaults. Note that the default state of the form
        /// is for all text input fields to be empty, checkboxes unchecked, and subfields to be
        /// disabled
        /// </summary>
        private void InitializeDefaults()
        {
            // set private variables to default
            _workPeriodSaved = MainForm.SecondsToDateTimePicker(Properties.Settings.Default.WorkSeconds);
            _restPeriodSaved = MainForm.SecondsToDateTimePicker(Properties.Settings.Default.RestSeconds);
            _globalStartKeyCurrent = Properties.Settings.Default.GlobalStartKey;

            // set GUI elements to default
            workPeriodSetting.Value = _workPeriodSaved;
            restPeriodSetting.Value = _restPeriodSaved;
            periodEndSoundSetting.Checked = Properties.Settings.Default.PeriodEndSound;

            remindSetting.Checked = Properties.Settings.Default.Remind;
            remindSecondsSetting.Value = Properties.Settings.Default.RemindSeconds;
            globalStartSetting.Checked = Properties.Settings.Default.GlobalStart;
            globalStartKeySetting.Text = _globalStartKeyCurrent.ToString();
            periodEndStopSetting.Checked = Properties.Settings.Default.PeriodEndStop;
            periodEndStopSetting_CheckedChanged(null, null);
           
            windowFlashSetting.Checked = Properties.Settings.Default.WindowFlash;
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
                _parentForm.RemindSecondsDefault = (uint)remindSecondsSetting.Value;
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

            if (periodEndSoundComboBox.SelectedIndex == 0 &&
                Properties.Settings.Default.PeriodEndSoundPath != _periodEndSoundDefault)
            {
                Properties.Settings.Default.PeriodEndSoundPath = _periodEndSoundDefault;
                //MainForm.ChangePeriodEndSound();
            }
            else if (periodEndSoundComboBox.SelectedIndex == 1 &&
                Properties.Settings.Default.PeriodEndSoundPath != _periodEndSoundPathCurrent)
            { 
                Properties.Settings.Default.PeriodEndSoundPath = _periodEndSoundPathCurrent;
                //MainForm.ChangePeriodEndSound(_periodEndSoundPathNew);
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

        private void periodEndSoundComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // the last item of the combobox will always be the open file option
            if (periodEndSoundComboBox.SelectedIndex == periodEndSoundComboBox.Items.Count - 1)
            {
                OpenFileDialog ofdialog = new OpenFileDialog();
                ofdialog.Filter = "Wave Files (*.wav)|*.wav";
                if (ofdialog.ShowDialog() == DialogResult.OK)
                {
                    _periodEndSoundPathCurrent = ofdialog.FileName;

                    // second (1th) item will always be where the "custom" sound is placed
                    if (periodEndSoundComboBox.Items.Count > 2)
                    {
                        // remove existing custom sound item
                        periodEndSoundComboBox.Items.RemoveAt(1);
                    }
                    periodEndSoundComboBox.Items.Insert(1, ofdialog.SafeFileName);
                }
                // select item before last
                periodEndSoundComboBox.SelectedIndex = periodEndSoundComboBox.Items.Count - 2;
            }
        }

        private void cancelSetting_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
