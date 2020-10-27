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
    public partial class Form3 : Form
    {
        public Form3(List<string> log)
        {
            InitializeComponent();

            // print log contents onto the textbox
            log.ForEach((string entry) =>
            {
                logTextBox.AppendText(entry + Environment.NewLine);
            });
        }
    }
}
