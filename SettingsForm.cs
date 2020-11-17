using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PomodoroTimer
{
    public partial class SettingsForm : Form
    {
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
            _workPeriodSaved = _parentForm.SecondsToDateTimePicker(Properties.Settings.Default.WorkSeconds);
            _restPeriodSaved = _parentForm.SecondsToDateTimePicker(Properties.Settings.Default.RestSeconds);
            _globalStartKeyCurrent = Properties.Settings.Default.GlobalStartKey;

            // set GUI elements to default
            workPeriodSetting.Value = _workPeriodSaved;
            restPeriodSetting.Value = _restPeriodSaved;

            startTimerOnStartupSetting.Checked = Properties.Settings.Default.StartTimerOnStartup;
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

        /// <summary>
        /// Initializes a "sound" ComboBox such that the first option is the default sound, the 
        /// middle option is a custom user sound (if exists in settings) and the last option opens a 
        /// file selector
        /// </summary>
        /// <param name="cbox">the given "sound" ComboBox</param>
        private void InitializeSoundComboBox(ComboBox cbox)
        {
            string currentSoundPath = "";
            string defaultSound = "";
            if (cbox == periodEndSoundComboBox)
            {
                currentSoundPath = Properties.Settings.Default.PeriodEndSoundPath;
                _periodEndSoundPathCurrent = currentSoundPath;
                defaultSound = _parentForm.PeriodEndSoundName;
            }
            else if (cbox == remindSoundComboBox)
            {
                currentSoundPath = Properties.Settings.Default.RemindSoundPath;
                _remindSoundPathCurrent = currentSoundPath;
                defaultSound = _parentForm.RemindSoundName;
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

        /// <summary>
        /// Sets the Enabled property of its "suboptions"
        /// </summary>
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

        /// <summary>
        /// Sets the Enabled property of its "suboptions"
        /// </summary>
        private void remindSetting_CheckedChanged(object sender, EventArgs e)
        {
            bool enable = remindSetting.Checked;
            remindSecondsLabel1.Enabled = enable;
            remindSecondsSetting.Enabled = enable;
            remindSecondsLabel2.Enabled = enable;
            remindSoundLabel.Enabled = enable;
            remindSoundComboBox.Enabled = enable;
        }

        /// <summary>
        /// Sets the Enabled property of its "suboptions"
        /// </summary>
        private void globalStartSetting_CheckedChanged(object sender, EventArgs e)
        {
            bool enable = globalStartSetting.Checked;
            globalStartKeyLabel.Enabled = enable;
            globalStartKeySetting.Enabled = enable;
        }

        /// <summary>
        /// Overwrites Defaults both in memory and in storage if changed
        /// </summary>
        private void saveSetting_Click(object sender, EventArgs e)
        {
            if (workPeriodSetting.Value != _workPeriodSaved)
            {
                Properties.Settings.Default.WorkSeconds = 
                    _parentForm.DateTimePickerToSeconds(workPeriodSetting.Value);
            }

            if (restPeriodSetting.Value != _restPeriodSaved)
            {
                Properties.Settings.Default.RestSeconds =
                    _parentForm.DateTimePickerToSeconds(restPeriodSetting.Value);
            }

            Properties.Settings.Default.StartTimerOnStartup = startTimerOnStartupSetting.Checked;
            Properties.Settings.Default.PeriodEndSound = periodEndSoundSetting.Checked;
            Properties.Settings.Default.PeriodEndStop = periodEndStopSetting.Checked;
            Properties.Settings.Default.Remind = remindSetting.Checked;
            Properties.Settings.Default.RemindSeconds = (uint)remindSecondsSetting.Value;
            Properties.Settings.Default.GlobalStart = globalStartSetting.Checked;
            Properties.Settings.Default.GlobalStartKey = _globalStartKeyCurrent;
            Properties.Settings.Default.WindowFlash = windowFlashSetting.Checked;   

            // check if PeriodEndSound is changed, ensuring a valid sound file (File.Exists)
            if (periodEndSoundComboBox.SelectedIndex == 0 
                && Properties.Settings.Default.PeriodEndSoundPath != _parentForm.PeriodEndSoundName)
            {
                Properties.Settings.Default.PeriodEndSoundPath = _parentForm.PeriodEndSoundName;
                _parentForm.PeriodEndSound.Stream = Properties.Resources.LingeringBells;
            }
            else if (periodEndSoundComboBox.SelectedIndex == 1 
                && Properties.Settings.Default.PeriodEndSoundPath != _periodEndSoundPathCurrent
                && File.Exists(_periodEndSoundPathCurrent))
            {
                Properties.Settings.Default.PeriodEndSoundPath = _periodEndSoundPathCurrent;
                _parentForm.PeriodEndSound.SoundLocation = _periodEndSoundPathCurrent;
                _parentForm.PeriodEndSound.Load();
            }

            // check if RemindSound is changed, ensuring a valid sound file (File.Exists)
            if (remindSoundComboBox.SelectedIndex == 0 
                && Properties.Settings.Default.RemindSoundPath != _parentForm.RemindSoundName)
            {
                Properties.Settings.Default.RemindSoundPath = _parentForm.RemindSoundName;
                _parentForm.RemindSound.Stream = Properties.Resources.Notify;
            }
            else if (remindSoundComboBox.SelectedIndex == 1 
                && Properties.Settings.Default.RemindSoundPath != _remindSoundPathCurrent
                && File.Exists(_remindSoundPathCurrent))
            {
                Properties.Settings.Default.RemindSoundPath = _remindSoundPathCurrent;
                _parentForm.RemindSound.SoundLocation = _remindSoundPathCurrent;
                _parentForm.RemindSound.Load();
            }

            if (notifyIconSetting.Checked != Properties.Settings.Default.NotifyIcon)
            {
                Properties.Settings.Default.NotifyIcon = notifyIconSetting.Checked;
                _parentForm.SetNotifyIconVisibility(notifyIconSetting.Checked);
            }

            Properties.Settings.Default.NotifyIconMinimize = notifyIconMinimizeSetting.Checked;

            // close form and save settings to file
            this.Close();
            Properties.Settings.Default.Save(); /* Interesting note: Save does not overwrite the 
                                                config file, but rather a user.config in the program's
                                                AppData folder */
        }

        private void globalStartKeySetting_Click(object sender, EventArgs e)
        {
            _globalStartEditing = true;
            globalStartKeySetting.Text = "Press any key";
        }

        /// <summary>
        /// Edits the global start key after having clicked the globalStartKeySetting TextBox
        /// and pressing down on a key (see: globalStartKeySetting_Click)
        /// </summary>
        private void globalStartKeySetting_KeyDown(object sender, KeyEventArgs e)
        {
            if (_globalStartEditing)
            {
                _globalStartEditing = false;
                _globalStartKeyCurrent = e.KeyCode;
                globalStartKeySetting.Text = e.KeyCode.ToString();
            }
        }


        /// <summary>
        /// Checks if the last option in a "sound" ComboBox was selected, creating an OpenFileDialog
        /// so that the user may select a wav file as a new sound for the specified timer sound
        /// </summary>
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

        /// <summary>
        /// Sets the Enabled property of its "suboptions"
        /// </summary>
        private void periodEndSoundSetting_CheckedChanged(object sender, EventArgs e)
        {
            bool enable = periodEndSoundSetting.Checked;
            periodEndSoundLabel.Enabled = enable;
            periodEndSoundComboBox.Enabled = enable;
        }

        /// <summary>
        /// Sets the Enabled property of its "suboptions"
        /// </summary>
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
