namespace PomodoroTimerForm
{
    partial class SettingsForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.periodEndSoundSetting = new System.Windows.Forms.CheckBox();
            this.remindSetting = new System.Windows.Forms.CheckBox();
            this.remindSecondsLabel1 = new System.Windows.Forms.Label();
            this.remindSecondsSetting = new System.Windows.Forms.NumericUpDown();
            this.remindSecondsLabel2 = new System.Windows.Forms.Label();
            this.windowFlashSetting = new System.Windows.Forms.CheckBox();
            this.globalStartSetting = new System.Windows.Forms.CheckBox();
            this.globalStartKeyLabel = new System.Windows.Forms.Label();
            this.globalStartKeySetting = new System.Windows.Forms.TextBox();
            this.saveSetting = new System.Windows.Forms.Button();
            this.cancelSetting = new System.Windows.Forms.Button();
            this.workPeriodSetting = new System.Windows.Forms.DateTimePicker();
            this.restPeriodSetting = new System.Windows.Forms.DateTimePicker();
            this.periodEndStopSetting = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.remindSecondsSetting)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Work period";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "Rest period";
            // 
            // periodEndSoundSetting
            // 
            this.periodEndSoundSetting.AutoSize = true;
            this.periodEndSoundSetting.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.periodEndSoundSetting.Location = new System.Drawing.Point(15, 89);
            this.periodEndSoundSetting.Name = "periodEndSoundSetting";
            this.periodEndSoundSetting.Size = new System.Drawing.Size(201, 21);
            this.periodEndSoundSetting.TabIndex = 8;
            this.periodEndSoundSetting.Text = "Play a sound on period end";
            this.periodEndSoundSetting.UseVisualStyleBackColor = true;
            // 
            // remindSetting
            // 
            this.remindSetting.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.remindSetting.Enabled = false;
            this.remindSetting.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.remindSetting.Location = new System.Drawing.Point(36, 151);
            this.remindSetting.Name = "remindSetting";
            this.remindSetting.Size = new System.Drawing.Size(242, 38);
            this.remindSetting.TabIndex = 9;
            this.remindSetting.Text = "Play a reminder sound until a new period starts";
            this.remindSetting.UseVisualStyleBackColor = true;
            this.remindSetting.CheckedChanged += new System.EventHandler(this.remindSetting_CheckedChanged);
            // 
            // remindSecondsLabel1
            // 
            this.remindSecondsLabel1.AutoSize = true;
            this.remindSecondsLabel1.Enabled = false;
            this.remindSecondsLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.remindSecondsLabel1.Location = new System.Drawing.Point(52, 198);
            this.remindSecondsLabel1.Name = "remindSecondsLabel1";
            this.remindSecondsLabel1.Size = new System.Drawing.Size(95, 17);
            this.remindSecondsLabel1.TabIndex = 10;
            this.remindSecondsLabel1.Text = "Remind every";
            // 
            // remindSecondsSetting
            // 
            this.remindSecondsSetting.Enabled = false;
            this.remindSecondsSetting.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.remindSecondsSetting.Location = new System.Drawing.Point(150, 196);
            this.remindSecondsSetting.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.remindSecondsSetting.Name = "remindSecondsSetting";
            this.remindSecondsSetting.Size = new System.Drawing.Size(45, 23);
            this.remindSecondsSetting.TabIndex = 11;
            this.remindSecondsSetting.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // remindSecondsLabel2
            // 
            this.remindSecondsLabel2.AutoSize = true;
            this.remindSecondsLabel2.Enabled = false;
            this.remindSecondsLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.remindSecondsLabel2.Location = new System.Drawing.Point(198, 198);
            this.remindSecondsLabel2.Name = "remindSecondsLabel2";
            this.remindSecondsLabel2.Size = new System.Drawing.Size(61, 17);
            this.remindSecondsLabel2.TabIndex = 12;
            this.remindSecondsLabel2.Text = "seconds";
            // 
            // windowFlashSetting
            // 
            this.windowFlashSetting.AutoSize = true;
            this.windowFlashSetting.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.windowFlashSetting.Location = new System.Drawing.Point(15, 291);
            this.windowFlashSetting.Name = "windowFlashSetting";
            this.windowFlashSetting.Size = new System.Drawing.Size(250, 21);
            this.windowFlashSetting.TabIndex = 13;
            this.windowFlashSetting.Text = "Taskbar icon flashes on period end";
            this.windowFlashSetting.UseVisualStyleBackColor = true;
            // 
            // globalStartSetting
            // 
            this.globalStartSetting.AutoSize = true;
            this.globalStartSetting.Enabled = false;
            this.globalStartSetting.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.globalStartSetting.Location = new System.Drawing.Point(36, 230);
            this.globalStartSetting.Name = "globalStartSetting";
            this.globalStartSetting.Size = new System.Drawing.Size(183, 21);
            this.globalStartSetting.TabIndex = 14;
            this.globalStartSetting.Text = "Enable \"global\" Start key";
            this.globalStartSetting.UseVisualStyleBackColor = true;
            this.globalStartSetting.CheckedChanged += new System.EventHandler(this.globalStartSetting_CheckedChanged);
            // 
            // globalStartKeyLabel
            // 
            this.globalStartKeyLabel.AutoSize = true;
            this.globalStartKeyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.globalStartKeyLabel.Location = new System.Drawing.Point(52, 260);
            this.globalStartKeyLabel.Name = "globalStartKeyLabel";
            this.globalStartKeyLabel.Size = new System.Drawing.Size(113, 17);
            this.globalStartKeyLabel.TabIndex = 15;
            this.globalStartKeyLabel.Text = "Global Start key:";
            // 
            // globalStartKeySetting
            // 
            this.globalStartKeySetting.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.globalStartKeySetting.Location = new System.Drawing.Point(179, 257);
            this.globalStartKeySetting.Name = "globalStartKeySetting";
            this.globalStartKeySetting.ReadOnly = true;
            this.globalStartKeySetting.Size = new System.Drawing.Size(100, 23);
            this.globalStartKeySetting.TabIndex = 16;
            this.globalStartKeySetting.Click += new System.EventHandler(this.globalStartKeySetting_Click);
            this.globalStartKeySetting.KeyDown += new System.Windows.Forms.KeyEventHandler(this.globalStartKeySetting_KeyDown);
            // 
            // saveSetting
            // 
            this.saveSetting.Location = new System.Drawing.Point(25, 329);
            this.saveSetting.Name = "saveSetting";
            this.saveSetting.Size = new System.Drawing.Size(116, 23);
            this.saveSetting.TabIndex = 17;
            this.saveSetting.Text = "Save";
            this.saveSetting.UseVisualStyleBackColor = true;
            this.saveSetting.Click += new System.EventHandler(this.saveSetting_Click);
            // 
            // cancelSetting
            // 
            this.cancelSetting.Location = new System.Drawing.Point(159, 329);
            this.cancelSetting.Name = "cancelSetting";
            this.cancelSetting.Size = new System.Drawing.Size(116, 23);
            this.cancelSetting.TabIndex = 18;
            this.cancelSetting.Text = "Cancel";
            this.cancelSetting.UseVisualStyleBackColor = true;
            this.cancelSetting.Click += new System.EventHandler(this.cancelSetting_Click);
            // 
            // workPeriodSetting
            // 
            this.workPeriodSetting.CustomFormat = "HH:mm:ss ";
            this.workPeriodSetting.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.workPeriodSetting.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.workPeriodSetting.Location = new System.Drawing.Point(170, 17);
            this.workPeriodSetting.Name = "workPeriodSetting";
            this.workPeriodSetting.ShowUpDown = true;
            this.workPeriodSetting.Size = new System.Drawing.Size(107, 23);
            this.workPeriodSetting.TabIndex = 19;
            this.workPeriodSetting.Value = new System.DateTime(2020, 10, 13, 0, 0, 0, 0);
            // 
            // restPeriodSetting
            // 
            this.restPeriodSetting.CustomFormat = "HH:mm:ss";
            this.restPeriodSetting.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.restPeriodSetting.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.restPeriodSetting.Location = new System.Drawing.Point(170, 53);
            this.restPeriodSetting.Name = "restPeriodSetting";
            this.restPeriodSetting.ShowUpDown = true;
            this.restPeriodSetting.Size = new System.Drawing.Size(107, 23);
            this.restPeriodSetting.TabIndex = 20;
            this.restPeriodSetting.Value = new System.DateTime(2020, 10, 13, 0, 0, 0, 0);
            // 
            // periodEndStopSetting
            // 
            this.periodEndStopSetting.AutoSize = true;
            this.periodEndStopSetting.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.periodEndStopSetting.Location = new System.Drawing.Point(15, 120);
            this.periodEndStopSetting.Name = "periodEndStopSetting";
            this.periodEndStopSetting.Size = new System.Drawing.Size(183, 21);
            this.periodEndStopSetting.TabIndex = 21;
            this.periodEndStopSetting.Text = "Stop timer on period end";
            this.periodEndStopSetting.UseVisualStyleBackColor = true;
            this.periodEndStopSetting.CheckedChanged += new System.EventHandler(this.periodEndStopSetting_CheckedChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(93, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 19);
            this.label2.TabIndex = 22;
            this.label2.Text = "(hh:mm:ss)";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(89, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 19);
            this.label3.TabIndex = 23;
            this.label3.Text = "(hh:mm:ss)";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 362);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.periodEndStopSetting);
            this.Controls.Add(this.restPeriodSetting);
            this.Controls.Add(this.workPeriodSetting);
            this.Controls.Add(this.cancelSetting);
            this.Controls.Add(this.saveSetting);
            this.Controls.Add(this.globalStartKeySetting);
            this.Controls.Add(this.globalStartKeyLabel);
            this.Controls.Add(this.globalStartSetting);
            this.Controls.Add(this.windowFlashSetting);
            this.Controls.Add(this.remindSecondsLabel2);
            this.Controls.Add(this.remindSecondsSetting);
            this.Controls.Add(this.remindSecondsLabel1);
            this.Controls.Add(this.remindSetting);
            this.Controls.Add(this.periodEndSoundSetting);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this.remindSecondsSetting)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox periodEndSoundSetting;
        private System.Windows.Forms.CheckBox remindSetting;
        private System.Windows.Forms.Label remindSecondsLabel1;
        private System.Windows.Forms.NumericUpDown remindSecondsSetting;
        private System.Windows.Forms.Label remindSecondsLabel2;
        private System.Windows.Forms.CheckBox windowFlashSetting;
        private System.Windows.Forms.CheckBox globalStartSetting;
        private System.Windows.Forms.Label globalStartKeyLabel;
        private System.Windows.Forms.TextBox globalStartKeySetting;
        private System.Windows.Forms.Button saveSetting;
        private System.Windows.Forms.Button cancelSetting;
        private System.Windows.Forms.DateTimePicker workPeriodSetting;
        private System.Windows.Forms.DateTimePicker restPeriodSetting;
        private System.Windows.Forms.CheckBox periodEndStopSetting;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}