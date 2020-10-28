using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PomodoroTimerForm
{
    public partial class LogForm : Form
    {
        private MainForm _parentForm;

        public LogForm(MainForm parent)
        {
            InitializeComponent();
            _parentForm = parent;

            // print log contents onto the textbox
            _parentForm.PeriodLog.ForEach(UpdateLog);
        }

        public void UpdateLog(string entry)
        {
            logTextBox.AppendText(entry + Environment.NewLine);
        }

        private void LogForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _parentForm.RunningLogForm = null;
        }
    }
}
