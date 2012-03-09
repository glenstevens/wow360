namespace DavidNikdel
{
    partial class PrefsDlg
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
          this.Ok = new System.Windows.Forms.Button();
          this.Cancel = new System.Windows.Forms.Button();
          this.m_mute = new System.Windows.Forms.CheckBox();
          this.m_lookSense = new System.Windows.Forms.TrackBar();
          this.m_mouseSense = new System.Windows.Forms.TrackBar();
          this.label1 = new System.Windows.Forms.Label();
          this.label2 = new System.Windows.Forms.Label();
          this.m_swapTabShift = new System.Windows.Forms.ComboBox();
          this.label3 = new System.Windows.Forms.Label();
          this.m_SwapSticks = new System.Windows.Forms.ComboBox();
          this.label4 = new System.Windows.Forms.Label();
          this.m_autorunUseTilde = new System.Windows.Forms.CheckBox();
          this.m_StickyBars = new System.Windows.Forms.CheckBox();
          this.m_AssistMode = new System.Windows.Forms.CheckBox();
          this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
          this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
          this.toolTip3 = new System.Windows.Forms.ToolTip(this.components);
          this.m_PetControls = new System.Windows.Forms.CheckBox();
          this.m_UseAltModifier = new System.Windows.Forms.CheckBox();
          this.m_useMouseModeToggle = new System.Windows.Forms.CheckBox();
          ((System.ComponentModel.ISupportInitialize)(this.m_lookSense)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.m_mouseSense)).BeginInit();
          this.SuspendLayout();
          // 
          // Ok
          // 
          this.Ok.Location = new System.Drawing.Point(333, 171);
          this.Ok.Name = "Ok";
          this.Ok.Size = new System.Drawing.Size(72, 23);
          this.Ok.TabIndex = 0;
          this.Ok.Text = "OK";
          this.Ok.UseVisualStyleBackColor = true;
          this.Ok.Click += new System.EventHandler(this.Ok_Click);
          // 
          // Cancel
          // 
          this.Cancel.Location = new System.Drawing.Point(255, 171);
          this.Cancel.Name = "Cancel";
          this.Cancel.Size = new System.Drawing.Size(72, 23);
          this.Cancel.TabIndex = 1;
          this.Cancel.Text = "Cancel";
          this.Cancel.UseVisualStyleBackColor = true;
          this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
          // 
          // m_mute
          // 
          this.m_mute.AutoSize = true;
          this.m_mute.Location = new System.Drawing.Point(120, 155);
          this.m_mute.Name = "m_mute";
          this.m_mute.Size = new System.Drawing.Size(89, 17);
          this.m_mute.TabIndex = 2;
          this.m_mute.Text = "Mute Sounds";
          this.m_mute.UseVisualStyleBackColor = true;
          // 
          // m_lookSense
          // 
          this.m_lookSense.LargeChange = 20;
          this.m_lookSense.Location = new System.Drawing.Point(12, 25);
          this.m_lookSense.Maximum = 100;
          this.m_lookSense.Name = "m_lookSense";
          this.m_lookSense.Size = new System.Drawing.Size(196, 45);
          this.m_lookSense.SmallChange = 5;
          this.m_lookSense.TabIndex = 4;
          this.m_lookSense.TickFrequency = 5;
          this.m_lookSense.Value = 50;
          // 
          // m_mouseSense
          // 
          this.m_mouseSense.LargeChange = 20;
          this.m_mouseSense.Location = new System.Drawing.Point(209, 25);
          this.m_mouseSense.Maximum = 100;
          this.m_mouseSense.Name = "m_mouseSense";
          this.m_mouseSense.Size = new System.Drawing.Size(196, 45);
          this.m_mouseSense.SmallChange = 5;
          this.m_mouseSense.TabIndex = 5;
          this.m_mouseSense.TickFrequency = 5;
          this.m_mouseSense.Value = 50;
          // 
          // label1
          // 
          this.label1.AutoSize = true;
          this.label1.Location = new System.Drawing.Point(9, 9);
          this.label1.Name = "label1";
          this.label1.Size = new System.Drawing.Size(119, 13);
          this.label1.TabIndex = 6;
          this.label1.Text = "Mouse-Look Sensitivity:";
          // 
          // label2
          // 
          this.label2.AutoSize = true;
          this.label2.Location = new System.Drawing.Point(206, 9);
          this.label2.Name = "label2";
          this.label2.Size = new System.Drawing.Size(120, 13);
          this.label2.TabIndex = 7;
          this.label2.Text = "Cursor-Move Sensitivity:";
          // 
          // m_swapTabShift
          // 
          this.m_swapTabShift.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
          this.m_swapTabShift.FormattingEnabled = true;
          this.m_swapTabShift.Items.AddRange(new object[] {
            "Left Shoulder Targets, Right Shoulder Mouse Mode",
            "Left Shoulder Mouse Mode, Right Shoulder Targets"});
          this.m_swapTabShift.Location = new System.Drawing.Point(92, 72);
          this.m_swapTabShift.Name = "m_swapTabShift";
          this.m_swapTabShift.Size = new System.Drawing.Size(314, 21);
          this.m_swapTabShift.TabIndex = 8;
          // 
          // label3
          // 
          this.label3.AutoSize = true;
          this.label3.Location = new System.Drawing.Point(9, 75);
          this.label3.Name = "label3";
          this.label3.Size = new System.Drawing.Size(76, 13);
          this.label3.TabIndex = 9;
          this.label3.Text = "Button Layout:";
          // 
          // m_SwapSticks
          // 
          this.m_SwapSticks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
          this.m_SwapSticks.FormattingEnabled = true;
          this.m_SwapSticks.Items.AddRange(new object[] {
            "Left Stick Moves/Mouse, Right Stick Looks",
            "Right Stick Moves/Mouse, Left Stick Looks"});
          this.m_SwapSticks.Location = new System.Drawing.Point(93, 99);
          this.m_SwapSticks.Name = "m_SwapSticks";
          this.m_SwapSticks.Size = new System.Drawing.Size(313, 21);
          this.m_SwapSticks.TabIndex = 11;
          // 
          // label4
          // 
          this.label4.AutoSize = true;
          this.label4.Location = new System.Drawing.Point(9, 102);
          this.label4.Name = "label4";
          this.label4.Size = new System.Drawing.Size(69, 13);
          this.label4.TabIndex = 12;
          this.label4.Text = "Stick Layout:";
          // 
          // m_autorunUseTilde
          // 
          this.m_autorunUseTilde.AccessibleDescription = "";
          this.m_autorunUseTilde.AutoSize = true;
          this.m_autorunUseTilde.Location = new System.Drawing.Point(12, 132);
          this.m_autorunUseTilde.Name = "m_autorunUseTilde";
          this.m_autorunUseTilde.Size = new System.Drawing.Size(97, 17);
          this.m_autorunUseTilde.TabIndex = 13;
          this.m_autorunUseTilde.Text = "Auto Run Tilde";
          this.toolTip1.SetToolTip(this.m_autorunUseTilde, "Useful on laptops that do not have a numlock key.\r\nThis requires the keybinding t" +
                  "o be set in WoW.");
          this.m_autorunUseTilde.UseVisualStyleBackColor = true;
          // 
          // m_StickyBars
          // 
          this.m_StickyBars.AutoSize = true;
          this.m_StickyBars.Location = new System.Drawing.Point(120, 132);
          this.m_StickyBars.Name = "m_StickyBars";
          this.m_StickyBars.Size = new System.Drawing.Size(79, 17);
          this.m_StickyBars.TabIndex = 14;
          this.m_StickyBars.Text = "Sticky Bars";
          this.toolTip2.SetToolTip(this.m_StickyBars, "When enabled, scrolling through action bars with the\r\nd-pad will require both tri" +
                  "ggers to be pulled.");
          this.m_StickyBars.UseVisualStyleBackColor = true;
          // 
          // m_AssistMode
          // 
          this.m_AssistMode.AutoSize = true;
          this.m_AssistMode.Location = new System.Drawing.Point(12, 155);
          this.m_AssistMode.Name = "m_AssistMode";
          this.m_AssistMode.Size = new System.Drawing.Size(83, 17);
          this.m_AssistMode.TabIndex = 15;
          this.m_AssistMode.Text = "Assist Mode";
          this.toolTip3.SetToolTip(this.m_AssistMode, "Enabling will cause the in party targeting of group\r\nmembers to automaticly choos" +
                  "e their target.\r\n\r\nRecommended for Ranged classes.\r\n\r\nDisable for easier healing" +
                  ".");
          this.m_AssistMode.UseVisualStyleBackColor = true;
          // 
          // toolTip1
          // 
          this.toolTip1.Tag = "";
          this.toolTip1.ToolTipTitle = "Use tilde for auto run.";
          // 
          // toolTip2
          // 
          this.toolTip2.ToolTipTitle = "Sticky Bars";
          // 
          // toolTip3
          // 
          this.toolTip3.ToolTipTitle = "Assist Mode";
          // 
          // m_PetControls
          // 
          this.m_PetControls.AutoSize = true;
          this.m_PetControls.Location = new System.Drawing.Point(12, 175);
          this.m_PetControls.Name = "m_PetControls";
          this.m_PetControls.Size = new System.Drawing.Size(83, 17);
          this.m_PetControls.TabIndex = 16;
          this.m_PetControls.Text = "Pet Controls";
          this.m_PetControls.UseVisualStyleBackColor = true;
          // 
          // m_UseAltModifier
          // 
          this.m_UseAltModifier.AutoSize = true;
          this.m_UseAltModifier.Location = new System.Drawing.Point(120, 175);
          this.m_UseAltModifier.Name = "m_UseAltModifier";
          this.m_UseAltModifier.Size = new System.Drawing.Size(100, 17);
          this.m_UseAltModifier.TabIndex = 17;
          this.m_UseAltModifier.Text = "Use Alt Modifier";
          this.m_UseAltModifier.UseVisualStyleBackColor = true;
          // 
          // m_useMouseModeToggle
          // 
          this.m_useMouseModeToggle.AutoSize = true;
          this.m_useMouseModeToggle.Location = new System.Drawing.Point(255, 132);
          this.m_useMouseModeToggle.Name = "m_useMouseModeToggle";
          this.m_useMouseModeToggle.Size = new System.Drawing.Size(146, 17);
          this.m_useMouseModeToggle.TabIndex = 18;
          this.m_useMouseModeToggle.Text = "Use Mouse Mode Toggle";
          this.m_useMouseModeToggle.UseVisualStyleBackColor = true;
          // 
          // PrefsDlg
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.ClientSize = new System.Drawing.Size(417, 204);
          this.Controls.Add(this.m_useMouseModeToggle);
          this.Controls.Add(this.m_UseAltModifier);
          this.Controls.Add(this.m_PetControls);
          this.Controls.Add(this.m_AssistMode);
          this.Controls.Add(this.m_StickyBars);
          this.Controls.Add(this.m_autorunUseTilde);
          this.Controls.Add(this.label4);
          this.Controls.Add(this.m_SwapSticks);
          this.Controls.Add(this.label3);
          this.Controls.Add(this.m_swapTabShift);
          this.Controls.Add(this.label2);
          this.Controls.Add(this.label1);
          this.Controls.Add(this.m_mouseSense);
          this.Controls.Add(this.m_lookSense);
          this.Controls.Add(this.m_mute);
          this.Controls.Add(this.Cancel);
          this.Controls.Add(this.Ok);
          this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
          this.MaximizeBox = false;
          this.MinimizeBox = false;
          this.Name = "PrefsDlg";
          this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
          this.Text = "Preferences";
          this.toolTip1.SetToolTip(this, "Use tilde for auto run. Useful on laptops.");
          this.Load += new System.EventHandler(this.PrefsDlg_Load);
          ((System.ComponentModel.ISupportInitialize)(this.m_lookSense)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.m_mouseSense)).EndInit();
          this.ResumeLayout(false);
          this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Ok;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.CheckBox m_mute;
        private System.Windows.Forms.TrackBar m_lookSense;
        private System.Windows.Forms.TrackBar m_mouseSense;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox m_swapTabShift;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox m_SwapSticks;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox m_autorunUseTilde;
        private System.Windows.Forms.CheckBox m_StickyBars;
        private System.Windows.Forms.CheckBox m_AssistMode;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolTip toolTip2;
        private System.Windows.Forms.ToolTip toolTip3;
        private System.Windows.Forms.CheckBox m_PetControls;
        private System.Windows.Forms.CheckBox m_UseAltModifier;
      private System.Windows.Forms.CheckBox m_useMouseModeToggle;

    }
}