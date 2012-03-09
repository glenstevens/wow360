namespace DavidNikdel
{
    partial class WoW360Router
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WoW360Router));
            this.m_status = new System.Windows.Forms.StatusStrip();
            this.m_statusTxt = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_timer = new System.Windows.Forms.Timer(this.components);
            this.m_menu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.preferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_cMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.m_c1Menu = new System.Windows.Forms.ToolStripMenuItem();
            this.m_c2Menu = new System.Windows.Forms.ToolStripMenuItem();
            this.m_c3Menu = new System.Windows.Forms.ToolStripMenuItem();
            this.m_c4Menu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.scanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.keyMappingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutX360RouterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_wowStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_pauseMenu = new System.Windows.Forms.ToolStripSplitButton();
            this.pauseOn = new System.Windows.Forms.ToolStripMenuItem();
            this.pauseOff = new System.Windows.Forms.ToolStripMenuItem();
            this.m_status.SuspendLayout();
            this.m_menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_status
            // 
            this.m_status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_wowStatus,
            this.m_pauseMenu,
            this.m_statusTxt});
            this.m_status.Location = new System.Drawing.Point(0, 233);
            this.m_status.Name = "m_status";
            this.m_status.Size = new System.Drawing.Size(344, 22);
            this.m_status.TabIndex = 0;
            this.m_status.Text = "statusStrip1";
            // 
            // m_statusTxt
            // 
            this.m_statusTxt.Name = "m_statusTxt";
            this.m_statusTxt.Size = new System.Drawing.Size(56, 17);
            this.m_statusTxt.Text = "Status...";
            // 
            // m_timer
            // 
            this.m_timer.Interval = 5000;
            this.m_timer.Tick += new System.EventHandler(this.OnTimer);
            // 
            // m_menu
            // 
            this.m_menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.m_cMenu,
            this.helpToolStripMenuItem});
            this.m_menu.Location = new System.Drawing.Point(0, 0);
            this.m_menu.Name = "m_menu";
            this.m_menu.Size = new System.Drawing.Size(344, 24);
            this.m_menu.TabIndex = 4;
            this.m_menu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.preferencesToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // preferencesToolStripMenuItem
            // 
            this.preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
            this.preferencesToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.preferencesToolStripMenuItem.Text = "&Preferences";
            this.preferencesToolStripMenuItem.Click += new System.EventHandler(this.preferencesToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(148, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // m_cMenu
            // 
            this.m_cMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_c1Menu,
            this.m_c2Menu,
            this.m_c3Menu,
            this.m_c4Menu,
            this.toolStripSeparator1,
            this.scanToolStripMenuItem});
            this.m_cMenu.Name = "m_cMenu";
            this.m_cMenu.Size = new System.Drawing.Size(78, 20);
            this.m_cMenu.Text = "&Controller";
            // 
            // m_c1Menu
            // 
            this.m_c1Menu.Name = "m_c1Menu";
            this.m_c1Menu.Size = new System.Drawing.Size(199, 22);
            this.m_c1Menu.Text = "Controller #&1";
            this.m_c1Menu.Click += new System.EventHandler(this.m_c1Menu_Click);
            // 
            // m_c2Menu
            // 
            this.m_c2Menu.Name = "m_c2Menu";
            this.m_c2Menu.Size = new System.Drawing.Size(199, 22);
            this.m_c2Menu.Text = "Controller #&2";
            this.m_c2Menu.Click += new System.EventHandler(this.m_c2Menu_Click);
            // 
            // m_c3Menu
            // 
            this.m_c3Menu.Name = "m_c3Menu";
            this.m_c3Menu.Size = new System.Drawing.Size(199, 22);
            this.m_c3Menu.Text = "Controller #&3";
            this.m_c3Menu.Click += new System.EventHandler(this.m_c3Menu_Click);
            // 
            // m_c4Menu
            // 
            this.m_c4Menu.Name = "m_c4Menu";
            this.m_c4Menu.Size = new System.Drawing.Size(199, 22);
            this.m_c4Menu.Text = "Controller #&4";
            this.m_c4Menu.Click += new System.EventHandler(this.m_c4Menu_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(196, 6);
            // 
            // scanToolStripMenuItem
            // 
            this.scanToolStripMenuItem.Name = "scanToolStripMenuItem";
            this.scanToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.scanToolStripMenuItem.Text = "&Scan for Controllers";
            this.scanToolStripMenuItem.Click += new System.EventHandler(this.scanToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.keyMappingsToolStripMenuItem,
            this.toolStripSeparator3,
            this.aboutX360RouterToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // keyMappingsToolStripMenuItem
            // 
            this.keyMappingsToolStripMenuItem.Name = "keyMappingsToolStripMenuItem";
            this.keyMappingsToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.keyMappingsToolStripMenuItem.Text = "&Key Mappings";
            this.keyMappingsToolStripMenuItem.Click += new System.EventHandler(this.keyMappingsToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(195, 6);
            // 
            // aboutX360RouterToolStripMenuItem
            // 
            this.aboutX360RouterToolStripMenuItem.Name = "aboutX360RouterToolStripMenuItem";
            this.aboutX360RouterToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.aboutX360RouterToolStripMenuItem.Text = "&About X360 Router";
            this.aboutX360RouterToolStripMenuItem.Click += new System.EventHandler(this.aboutX360RouterToolStripMenuItem_Click);
            // 
            // m_wowStatus
            // 
            this.m_wowStatus.Image = global::DavidNikdel.Properties.Resources.wowlogo;
            this.m_wowStatus.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.m_wowStatus.Name = "m_wowStatus";
            this.m_wowStatus.Size = new System.Drawing.Size(32, 16);
            this.m_wowStatus.Visible = false;
            // 
            // m_pauseMenu
            // 
            this.m_pauseMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_pauseMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pauseOn,
            this.pauseOff});
            this.m_pauseMenu.Image = global::DavidNikdel.Properties.Resources.controller_off;
            this.m_pauseMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_pauseMenu.Name = "m_pauseMenu";
            this.m_pauseMenu.Size = new System.Drawing.Size(32, 20);
            // 
            // pauseOn
            // 
            this.pauseOn.Image = global::DavidNikdel.Properties.Resources.controller_off;
            this.pauseOn.Name = "pauseOn";
            this.pauseOn.Size = new System.Drawing.Size(135, 22);
            this.pauseOn.Text = "&Pause";
            this.pauseOn.Click += new System.EventHandler(this.pauseOn_Click);
            // 
            // pauseOff
            // 
            this.pauseOff.Image = global::DavidNikdel.Properties.Resources.controller;
            this.pauseOff.Name = "pauseOff";
            this.pauseOff.Size = new System.Drawing.Size(135, 22);
            this.pauseOff.Text = "&Unpause";
            this.pauseOff.Click += new System.EventHandler(this.pauseOff_Click);
            // 
            // WoW360Router
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::DavidNikdel.Properties.Resources.background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(344, 255);
            this.Controls.Add(this.m_status);
            this.Controls.Add(this.m_menu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.m_menu;
            this.MaximizeBox = false;
            this.Name = "WoW360Router";
            this.Text = "WoW-360 Router";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnClosing);
            this.Load += new System.EventHandler(this.OnLoad);
            this.m_status.ResumeLayout(false);
            this.m_status.PerformLayout();
            this.m_menu.ResumeLayout(false);
            this.m_menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip m_status;
        private System.Windows.Forms.ToolStripStatusLabel m_statusTxt;
        private System.Windows.Forms.Timer m_timer;
        private System.Windows.Forms.MenuStrip m_menu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_cMenu;
        private System.Windows.Forms.ToolStripMenuItem m_c1Menu;
        private System.Windows.Forms.ToolStripMenuItem m_c2Menu;
        private System.Windows.Forms.ToolStripMenuItem m_c3Menu;
        private System.Windows.Forms.ToolStripMenuItem m_c4Menu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem scanToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem keyMappingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem aboutX360RouterToolStripMenuItem;
        private System.Windows.Forms.ToolStripSplitButton m_pauseMenu;
        private System.Windows.Forms.ToolStripMenuItem pauseOn;
        private System.Windows.Forms.ToolStripMenuItem pauseOff;
        private System.Windows.Forms.ToolStripStatusLabel m_wowStatus;
        private System.Windows.Forms.ToolStripMenuItem preferencesToolStripMenuItem;
    }
}

