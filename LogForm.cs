﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace PomodoroTimer
{
    public partial class LogForm : Form
    {
        private MainForm _parentForm;

        public LogForm(MainForm parent, uint currentTotalWorkSecs)
        {
            InitializeComponent();
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            _parentForm = parent;
            
            // set start date, if applicable
            if (parent.StartDate != null)
            {
                timerStartDate.Text = parent.StartDate;
            }

            UpdateTotalWorkDisplay(currentTotalWorkSecs);

            // print log contents onto the textbox
            parent.PeriodLog.ForEach(UpdateLog);
        }

        public void UpdateLog(string entry)
        {
            logTextBox.AppendText(entry + Environment.NewLine);
        }

        private void LogForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _parentForm.RunningLogForm = null;
        }

        /// <summary>
        /// Updates the "Total Work Time" in the log form
        /// </summary>
        /// <param name="totalWorkSeconds"></param>
        public void UpdateTotalWorkDisplay(uint totalWorkSeconds)
        {
            uint hours = totalWorkSeconds / 3600;
            uint mins = (totalWorkSeconds % 3600) / 60;
            uint secs = totalWorkSeconds % 60;

            string hrStr = hours.ToString();
            string minStr = (mins < 10) ? '0' + mins.ToString() : mins.ToString();
            string secStr = (secs < 10) ? '0' + secs.ToString() : secs.ToString();
            totalWorkDisplay.Text = hrStr + ':' + minStr + ':' + secStr;
        }

        /// <summary>
        /// Updates the "Started" time in the log form
        /// </summary>
        /// <param name="dateStr"></param>
        public void SetStartDate(string dateStr)
        {
            timerStartDate.Text = dateStr;
        }
    }
}
