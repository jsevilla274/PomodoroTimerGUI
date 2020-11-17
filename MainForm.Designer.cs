namespace PomodoroTimer
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timeDisplay = new System.Windows.Forms.Label();
            this.nextButton = new System.Windows.Forms.Button();
            this.startpauseButton = new System.Windows.Forms.Button();
            this.restartButton = new System.Windows.Forms.Button();
            this.settingsButton = new System.Windows.Forms.Button();
            this.periodTimer = new System.Windows.Forms.Timer(this.components);
            this.restartTimeInput = new System.Windows.Forms.DateTimePicker();
            this.mainNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.startTimeDisplay = new System.Windows.Forms.Label();
            this.endTimeDisplay = new System.Windows.Forms.Label();
            this.periodLabelDisplay = new System.Windows.Forms.Label();
            this.remindSoundTimer = new System.Windows.Forms.Timer(this.components);
            this.logButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // timeDisplay
            // 
            this.timeDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 64F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeDisplay.Location = new System.Drawing.Point(32, 30);
            this.timeDisplay.Name = "timeDisplay";
            this.timeDisplay.Size = new System.Drawing.Size(324, 97);
            this.timeDisplay.TabIndex = 0;
            this.timeDisplay.Text = "MM:SS";
            this.timeDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nextButton
            // 
            this.nextButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nextButton.Location = new System.Drawing.Point(208, 141);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(142, 38);
            this.nextButton.TabIndex = 2;
            this.nextButton.Text = "Next";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // startpauseButton
            // 
            this.startpauseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startpauseButton.Location = new System.Drawing.Point(35, 141);
            this.startpauseButton.Name = "startpauseButton";
            this.startpauseButton.Size = new System.Drawing.Size(142, 38);
            this.startpauseButton.TabIndex = 1;
            this.startpauseButton.Text = "Start";
            this.startpauseButton.UseVisualStyleBackColor = true;
            this.startpauseButton.Click += new System.EventHandler(this.startpauseButton_Click);
            // 
            // restartButton
            // 
            this.restartButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.restartButton.Location = new System.Drawing.Point(35, 190);
            this.restartButton.Name = "restartButton";
            this.restartButton.Size = new System.Drawing.Size(142, 38);
            this.restartButton.TabIndex = 3;
            this.restartButton.Text = "Restart";
            this.restartButton.UseVisualStyleBackColor = true;
            this.restartButton.Click += new System.EventHandler(this.restartButton_Click);
            // 
            // settingsButton
            // 
            this.settingsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.settingsButton.Location = new System.Drawing.Point(209, 241);
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Size = new System.Drawing.Size(142, 38);
            this.settingsButton.TabIndex = 6;
            this.settingsButton.Text = "Settings";
            this.settingsButton.UseVisualStyleBackColor = true;
            this.settingsButton.Click += new System.EventHandler(this.settingsButton_Click);
            // 
            // periodTimer
            // 
            this.periodTimer.Interval = 1000;
            this.periodTimer.Tick += new System.EventHandler(this.periodTimer_Tick);
            // 
            // restartTimeInput
            // 
            this.restartTimeInput.CustomFormat = "HH:mm:ss";
            this.restartTimeInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.restartTimeInput.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.restartTimeInput.Location = new System.Drawing.Point(208, 190);
            this.restartTimeInput.Name = "restartTimeInput";
            this.restartTimeInput.ShowUpDown = true;
            this.restartTimeInput.Size = new System.Drawing.Size(142, 38);
            this.restartTimeInput.TabIndex = 4;
            this.restartTimeInput.Value = new System.DateTime(2020, 10, 13, 0, 20, 0, 0);
            // 
            // mainNotifyIcon
            // 
            this.mainNotifyIcon.Text = "PomodoroTimer";
            this.mainNotifyIcon.Visible = true;
            this.mainNotifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
            // 
            // startTimeDisplay
            // 
            this.startTimeDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startTimeDisplay.Location = new System.Drawing.Point(2, 2);
            this.startTimeDisplay.Name = "startTimeDisplay";
            this.startTimeDisplay.Size = new System.Drawing.Size(120, 23);
            this.startTimeDisplay.TabIndex = 7;
            this.startTimeDisplay.Text = "Start: h:mm";
            this.startTimeDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.startTimeDisplay.Visible = false;
            // 
            // endTimeDisplay
            // 
            this.endTimeDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.endTimeDisplay.Location = new System.Drawing.Point(262, 2);
            this.endTimeDisplay.Name = "endTimeDisplay";
            this.endTimeDisplay.Size = new System.Drawing.Size(120, 23);
            this.endTimeDisplay.TabIndex = 8;
            this.endTimeDisplay.Text = "End: h:mm";
            this.endTimeDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.endTimeDisplay.Visible = false;
            // 
            // periodLabelDisplay
            // 
            this.periodLabelDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.periodLabelDisplay.Location = new System.Drawing.Point(143, 12);
            this.periodLabelDisplay.Name = "periodLabelDisplay";
            this.periodLabelDisplay.Size = new System.Drawing.Size(100, 23);
            this.periodLabelDisplay.TabIndex = 9;
            this.periodLabelDisplay.Text = "- Work -";
            this.periodLabelDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // remindSoundTimer
            // 
            this.remindSoundTimer.Interval = 1000;
            this.remindSoundTimer.Tick += new System.EventHandler(this.remindSoundTimer_Tick);
            // 
            // logButton
            // 
            this.logButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logButton.Location = new System.Drawing.Point(35, 241);
            this.logButton.Name = "logButton";
            this.logButton.Size = new System.Drawing.Size(142, 38);
            this.logButton.TabIndex = 5;
            this.logButton.Text = "Log";
            this.logButton.UseVisualStyleBackColor = true;
            this.logButton.Click += new System.EventHandler(this.logButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 296);
            this.Controls.Add(this.logButton);
            this.Controls.Add(this.periodLabelDisplay);
            this.Controls.Add(this.endTimeDisplay);
            this.Controls.Add(this.startTimeDisplay);
            this.Controls.Add(this.restartTimeInput);
            this.Controls.Add(this.settingsButton);
            this.Controls.Add(this.restartButton);
            this.Controls.Add(this.startpauseButton);
            this.Controls.Add(this.nextButton);
            this.Controls.Add(this.timeDisplay);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.Text = "Pomodoro Timer";
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label timeDisplay;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.Button startpauseButton;
        private System.Windows.Forms.Button restartButton;
        private System.Windows.Forms.Button settingsButton;
        private System.Windows.Forms.Timer periodTimer;
        private System.Windows.Forms.DateTimePicker restartTimeInput;
        private System.Windows.Forms.Label startTimeDisplay;
        private System.Windows.Forms.Label endTimeDisplay;
        private System.Windows.Forms.Label periodLabelDisplay;
        private System.Windows.Forms.Timer remindSoundTimer;
        private System.Windows.Forms.Button logButton;
        private System.Windows.Forms.NotifyIcon mainNotifyIcon;
    }
}

