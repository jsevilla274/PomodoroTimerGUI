﻿namespace PomodoroTimer
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
            this.components = new System.ComponentModel.Container();
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
            this.periodEndSoundLabel = new System.Windows.Forms.Label();
            this.periodEndSoundComboBox = new System.Windows.Forms.ComboBox();
            this.remindSoundComboBox = new System.Windows.Forms.ComboBox();
            this.remindSoundLabel = new System.Windows.Forms.Label();
            this.notifyIconSetting = new System.Windows.Forms.CheckBox();
            this.notifyIconMinimizeSetting = new System.Windows.Forms.CheckBox();
            this.notifyIconSettingToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.globalStartSettingToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.startTimerOnStartupSetting = new System.Windows.Forms.CheckBox();
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
            this.periodEndSoundSetting.Location = new System.Drawing.Point(15, 120);
            this.periodEndSoundSetting.Name = "periodEndSoundSetting";
            this.periodEndSoundSetting.Size = new System.Drawing.Size(201, 21);
            this.periodEndSoundSetting.TabIndex = 4;
            this.periodEndSoundSetting.Text = "Play a sound on period end";
            this.periodEndSoundSetting.UseVisualStyleBackColor = true;
            this.periodEndSoundSetting.CheckedChanged += new System.EventHandler(this.periodEndSoundSetting_CheckedChanged);
            // 
            // remindSetting
            // 
            this.remindSetting.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.remindSetting.Enabled = false;
            this.remindSetting.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.remindSetting.Location = new System.Drawing.Point(36, 215);
            this.remindSetting.Name = "remindSetting";
            this.remindSetting.Size = new System.Drawing.Size(242, 38);
            this.remindSetting.TabIndex = 6;
            this.remindSetting.Text = "Play a reminder sound until a new period starts";
            this.remindSetting.UseVisualStyleBackColor = true;
            this.remindSetting.CheckedChanged += new System.EventHandler(this.remindSetting_CheckedChanged);
            // 
            // remindSecondsLabel1
            // 
            this.remindSecondsLabel1.AutoSize = true;
            this.remindSecondsLabel1.Enabled = false;
            this.remindSecondsLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.remindSecondsLabel1.Location = new System.Drawing.Point(52, 266);
            this.remindSecondsLabel1.Name = "remindSecondsLabel1";
            this.remindSecondsLabel1.Size = new System.Drawing.Size(95, 17);
            this.remindSecondsLabel1.TabIndex = 10;
            this.remindSecondsLabel1.Text = "Remind every";
            // 
            // remindSecondsSetting
            // 
            this.remindSecondsSetting.Enabled = false;
            this.remindSecondsSetting.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.remindSecondsSetting.Location = new System.Drawing.Point(150, 264);
            this.remindSecondsSetting.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.remindSecondsSetting.Name = "remindSecondsSetting";
            this.remindSecondsSetting.Size = new System.Drawing.Size(45, 23);
            this.remindSecondsSetting.TabIndex = 7;
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
            this.remindSecondsLabel2.Location = new System.Drawing.Point(198, 266);
            this.remindSecondsLabel2.Name = "remindSecondsLabel2";
            this.remindSecondsLabel2.Size = new System.Drawing.Size(61, 17);
            this.remindSecondsLabel2.TabIndex = 12;
            this.remindSecondsLabel2.Text = "seconds";
            // 
            // windowFlashSetting
            // 
            this.windowFlashSetting.AutoSize = true;
            this.windowFlashSetting.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.windowFlashSetting.Location = new System.Drawing.Point(15, 400);
            this.windowFlashSetting.Name = "windowFlashSetting";
            this.windowFlashSetting.Size = new System.Drawing.Size(250, 21);
            this.windowFlashSetting.TabIndex = 9;
            this.windowFlashSetting.Text = "Taskbar icon flashes on period end";
            this.windowFlashSetting.UseVisualStyleBackColor = true;
            // 
            // globalStartSetting
            // 
            this.globalStartSetting.AutoSize = true;
            this.globalStartSetting.Enabled = false;
            this.globalStartSetting.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.globalStartSetting.Location = new System.Drawing.Point(36, 339);
            this.globalStartSetting.Name = "globalStartSetting";
            this.globalStartSetting.Size = new System.Drawing.Size(183, 21);
            this.globalStartSetting.TabIndex = 8;
            this.globalStartSetting.Text = "Enable \"global\" Start key";
            this.globalStartSetting.UseVisualStyleBackColor = true;
            this.globalStartSetting.CheckedChanged += new System.EventHandler(this.globalStartSetting_CheckedChanged);
            // 
            // globalStartKeyLabel
            // 
            this.globalStartKeyLabel.AutoSize = true;
            this.globalStartKeyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.globalStartKeyLabel.Location = new System.Drawing.Point(52, 369);
            this.globalStartKeyLabel.Name = "globalStartKeyLabel";
            this.globalStartKeyLabel.Size = new System.Drawing.Size(113, 17);
            this.globalStartKeyLabel.TabIndex = 15;
            this.globalStartKeyLabel.Text = "Global Start key:";
            // 
            // globalStartKeySetting
            // 
            this.globalStartKeySetting.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.globalStartKeySetting.Location = new System.Drawing.Point(173, 366);
            this.globalStartKeySetting.Name = "globalStartKeySetting";
            this.globalStartKeySetting.ReadOnly = true;
            this.globalStartKeySetting.Size = new System.Drawing.Size(100, 23);
            this.globalStartKeySetting.TabIndex = 30;
            this.globalStartKeySetting.TabStop = false;
            this.globalStartKeySetting.Click += new System.EventHandler(this.globalStartKeySetting_Click);
            this.globalStartKeySetting.KeyDown += new System.Windows.Forms.KeyEventHandler(this.globalStartKeySetting_KeyDown);
            // 
            // saveSetting
            // 
            this.saveSetting.Location = new System.Drawing.Point(25, 498);
            this.saveSetting.Name = "saveSetting";
            this.saveSetting.Size = new System.Drawing.Size(116, 23);
            this.saveSetting.TabIndex = 12;
            this.saveSetting.Text = "Save";
            this.saveSetting.UseVisualStyleBackColor = true;
            this.saveSetting.Click += new System.EventHandler(this.saveSetting_Click);
            // 
            // cancelSetting
            // 
            this.cancelSetting.Location = new System.Drawing.Point(159, 498);
            this.cancelSetting.Name = "cancelSetting";
            this.cancelSetting.Size = new System.Drawing.Size(116, 23);
            this.cancelSetting.TabIndex = 13;
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
            this.workPeriodSetting.TabIndex = 1;
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
            this.restPeriodSetting.TabIndex = 2;
            this.restPeriodSetting.Value = new System.DateTime(2020, 10, 13, 0, 0, 0, 0);
            // 
            // periodEndStopSetting
            // 
            this.periodEndStopSetting.AutoSize = true;
            this.periodEndStopSetting.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.periodEndStopSetting.Location = new System.Drawing.Point(15, 183);
            this.periodEndStopSetting.Name = "periodEndStopSetting";
            this.periodEndStopSetting.Size = new System.Drawing.Size(183, 21);
            this.periodEndStopSetting.TabIndex = 5;
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
            // periodEndSoundLabel
            // 
            this.periodEndSoundLabel.AutoSize = true;
            this.periodEndSoundLabel.Enabled = false;
            this.periodEndSoundLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.periodEndSoundLabel.Location = new System.Drawing.Point(33, 152);
            this.periodEndSoundLabel.Name = "periodEndSoundLabel";
            this.periodEndSoundLabel.Size = new System.Drawing.Size(75, 17);
            this.periodEndSoundLabel.TabIndex = 24;
            this.periodEndSoundLabel.Text = "Sound file:";
            // 
            // periodEndSoundComboBox
            // 
            this.periodEndSoundComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.periodEndSoundComboBox.Enabled = false;
            this.periodEndSoundComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.periodEndSoundComboBox.FormattingEnabled = true;
            this.periodEndSoundComboBox.Location = new System.Drawing.Point(114, 147);
            this.periodEndSoundComboBox.Name = "periodEndSoundComboBox";
            this.periodEndSoundComboBox.Size = new System.Drawing.Size(140, 24);
            this.periodEndSoundComboBox.TabIndex = 25;
            this.periodEndSoundComboBox.TabStop = false;
            // 
            // remindSoundComboBox
            // 
            this.remindSoundComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.remindSoundComboBox.Enabled = false;
            this.remindSoundComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.remindSoundComboBox.FormattingEnabled = true;
            this.remindSoundComboBox.Location = new System.Drawing.Point(133, 301);
            this.remindSoundComboBox.Name = "remindSoundComboBox";
            this.remindSoundComboBox.Size = new System.Drawing.Size(140, 24);
            this.remindSoundComboBox.TabIndex = 27;
            this.remindSoundComboBox.TabStop = false;
            // 
            // remindSoundLabel
            // 
            this.remindSoundLabel.AutoSize = true;
            this.remindSoundLabel.Enabled = false;
            this.remindSoundLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.remindSoundLabel.Location = new System.Drawing.Point(52, 304);
            this.remindSoundLabel.Name = "remindSoundLabel";
            this.remindSoundLabel.Size = new System.Drawing.Size(75, 17);
            this.remindSoundLabel.TabIndex = 26;
            this.remindSoundLabel.Text = "Sound file:";
            // 
            // notifyIconSetting
            // 
            this.notifyIconSetting.AutoSize = true;
            this.notifyIconSetting.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.notifyIconSetting.Location = new System.Drawing.Point(15, 431);
            this.notifyIconSetting.Name = "notifyIconSetting";
            this.notifyIconSetting.Size = new System.Drawing.Size(177, 21);
            this.notifyIconSetting.TabIndex = 10;
            this.notifyIconSetting.Text = "Enable system tray icon";
            this.notifyIconSetting.UseVisualStyleBackColor = true;
            this.notifyIconSetting.CheckedChanged += new System.EventHandler(this.notifyIconSetting_CheckedChanged);
            // 
            // notifyIconMinimizeSetting
            // 
            this.notifyIconMinimizeSetting.AutoSize = true;
            this.notifyIconMinimizeSetting.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.notifyIconMinimizeSetting.Location = new System.Drawing.Point(36, 461);
            this.notifyIconMinimizeSetting.Name = "notifyIconMinimizeSetting";
            this.notifyIconMinimizeSetting.Size = new System.Drawing.Size(212, 21);
            this.notifyIconMinimizeSetting.TabIndex = 11;
            this.notifyIconMinimizeSetting.Text = "Hide timer in tray on minimize";
            this.notifyIconMinimizeSetting.UseVisualStyleBackColor = true;
            // 
            // notifyIconSettingToolTip
            // 
            this.notifyIconSettingToolTip.AutoPopDelay = 32000;
            this.notifyIconSettingToolTip.InitialDelay = 500;
            this.notifyIconSettingToolTip.ReshowDelay = 100;
            // 
            // globalStartSettingToolTip
            // 
            this.globalStartSettingToolTip.AutoPopDelay = 32000;
            this.globalStartSettingToolTip.InitialDelay = 500;
            this.globalStartSettingToolTip.ReshowDelay = 100;
            // 
            // startTimerOnStartupSetting
            // 
            this.startTimerOnStartupSetting.AutoSize = true;
            this.startTimerOnStartupSetting.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startTimerOnStartupSetting.Location = new System.Drawing.Point(15, 88);
            this.startTimerOnStartupSetting.Name = "startTimerOnStartupSetting";
            this.startTimerOnStartupSetting.Size = new System.Drawing.Size(188, 21);
            this.startTimerOnStartupSetting.TabIndex = 3;
            this.startTimerOnStartupSetting.Text = "Start timer on app startup";
            this.startTimerOnStartupSetting.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 528);
            this.Controls.Add(this.startTimerOnStartupSetting);
            this.Controls.Add(this.notifyIconMinimizeSetting);
            this.Controls.Add(this.notifyIconSetting);
            this.Controls.Add(this.remindSoundComboBox);
            this.Controls.Add(this.remindSoundLabel);
            this.Controls.Add(this.periodEndSoundComboBox);
            this.Controls.Add(this.periodEndSoundLabel);
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
        private System.Windows.Forms.Label periodEndSoundLabel;
        private System.Windows.Forms.ComboBox periodEndSoundComboBox;
        private System.Windows.Forms.ComboBox remindSoundComboBox;
        private System.Windows.Forms.Label remindSoundLabel;
        private System.Windows.Forms.CheckBox notifyIconSetting;
        private System.Windows.Forms.CheckBox notifyIconMinimizeSetting;
        private System.Windows.Forms.ToolTip notifyIconSettingToolTip;
        private System.Windows.Forms.ToolTip globalStartSettingToolTip;
        private System.Windows.Forms.CheckBox startTimerOnStartupSetting;
    }
}