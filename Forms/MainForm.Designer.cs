namespace AACMAToolkit.Forms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.spltButtonScreen = new System.Windows.Forms.Splitter();
            this.lblCheckVersion = new System.Windows.Forms.Label();
            this.txtOutput = new System.Windows.Forms.RichTextBox();
            this.lblCheckAgentError = new System.Windows.Forms.Label();
            this.lblUpdateArcAgent = new System.Windows.Forms.Label();
            this.lblExportLogs = new System.Windows.Forms.Label();
            this.lblShowAgentMode = new System.Windows.Forms.Label();
            this.lblShowAgentConfig = new System.Windows.Forms.Label();
            this.lblChangeMode2Full = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.manuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restartAsAdministratorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnRestartService = new System.Windows.Forms.Label();
            this.tabControlMainForm = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.menuStrip1.SuspendLayout();
            this.tabControlMainForm.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // spltButtonScreen
            // 
            this.spltButtonScreen.Location = new System.Drawing.Point(0, 24);
            this.spltButtonScreen.Name = "spltButtonScreen";
            this.spltButtonScreen.Size = new System.Drawing.Size(336, 566);
            this.spltButtonScreen.TabIndex = 0;
            this.spltButtonScreen.TabStop = false;
            // 
            // lblCheckVersion
            // 
            this.lblCheckVersion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCheckVersion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblCheckVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheckVersion.Location = new System.Drawing.Point(3, 3);
            this.lblCheckVersion.Name = "lblCheckVersion";
            this.lblCheckVersion.Size = new System.Drawing.Size(298, 40);
            this.lblCheckVersion.TabIndex = 1;
            this.lblCheckVersion.Text = "Check Agent Version";
            this.lblCheckVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCheckVersion.Click += new System.EventHandler(this.lblCheckVersion_Click);
            // 
            // txtOutput
            // 
            this.txtOutput.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOutput.Location = new System.Drawing.Point(342, 24);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.Size = new System.Drawing.Size(886, 556);
            this.txtOutput.TabIndex = 2;
            this.txtOutput.Text = "";
            // 
            // lblCheckAgentError
            // 
            this.lblCheckAgentError.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCheckAgentError.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblCheckAgentError.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheckAgentError.Location = new System.Drawing.Point(3, 43);
            this.lblCheckAgentError.Name = "lblCheckAgentError";
            this.lblCheckAgentError.Size = new System.Drawing.Size(298, 40);
            this.lblCheckAgentError.TabIndex = 3;
            this.lblCheckAgentError.Text = "Check Agent Error";
            this.lblCheckAgentError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCheckAgentError.Click += new System.EventHandler(this.lblCheckAgentError_Click);
            // 
            // lblUpdateArcAgent
            // 
            this.lblUpdateArcAgent.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblUpdateArcAgent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblUpdateArcAgent.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUpdateArcAgent.Location = new System.Drawing.Point(12, 513);
            this.lblUpdateArcAgent.Name = "lblUpdateArcAgent";
            this.lblUpdateArcAgent.Size = new System.Drawing.Size(315, 65);
            this.lblUpdateArcAgent.TabIndex = 6;
            this.lblUpdateArcAgent.Text = "Update Arc Agent";
            this.lblUpdateArcAgent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblUpdateArcAgent.Click += new System.EventHandler(this.lblUpdateArcAgent_Click);
            // 
            // lblExportLogs
            // 
            this.lblExportLogs.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblExportLogs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblExportLogs.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExportLogs.Location = new System.Drawing.Point(12, 448);
            this.lblExportLogs.Name = "lblExportLogs";
            this.lblExportLogs.Size = new System.Drawing.Size(315, 65);
            this.lblExportLogs.TabIndex = 7;
            this.lblExportLogs.Text = "Export Logs";
            this.lblExportLogs.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblExportLogs.Click += new System.EventHandler(this.lblExportLogs_Click);
            // 
            // lblShowAgentMode
            // 
            this.lblShowAgentMode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblShowAgentMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblShowAgentMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShowAgentMode.Location = new System.Drawing.Point(3, 123);
            this.lblShowAgentMode.Name = "lblShowAgentMode";
            this.lblShowAgentMode.Size = new System.Drawing.Size(298, 40);
            this.lblShowAgentMode.TabIndex = 5;
            this.lblShowAgentMode.Text = "Show Agent Mode";
            this.lblShowAgentMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblShowAgentMode.Click += new System.EventHandler(this.lblShowAgentMode_Click);
            // 
            // lblShowAgentConfig
            // 
            this.lblShowAgentConfig.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblShowAgentConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblShowAgentConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShowAgentConfig.Location = new System.Drawing.Point(3, 83);
            this.lblShowAgentConfig.Name = "lblShowAgentConfig";
            this.lblShowAgentConfig.Size = new System.Drawing.Size(298, 40);
            this.lblShowAgentConfig.TabIndex = 4;
            this.lblShowAgentConfig.Text = "Show Agent Config";
            this.lblShowAgentConfig.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblShowAgentConfig.Click += new System.EventHandler(this.lblShowAgentConfig_Click);
            // 
            // lblChangeMode2Full
            // 
            this.lblChangeMode2Full.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblChangeMode2Full.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblChangeMode2Full.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChangeMode2Full.Location = new System.Drawing.Point(3, 163);
            this.lblChangeMode2Full.Name = "lblChangeMode2Full";
            this.lblChangeMode2Full.Size = new System.Drawing.Size(298, 40);
            this.lblChangeMode2Full.TabIndex = 8;
            this.lblChangeMode2Full.Text = "Change 2 Full mode";
            this.lblChangeMode2Full.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblChangeMode2Full.Click += new System.EventHandler(this.lblChangeMode2Full_Click);
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 203);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(298, 40);
            this.label1.TabIndex = 9;
            this.label1.Text = "Change 2 Monitor mode";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.White;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.manuToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1237, 24);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStripTop";
            // 
            // manuToolStripMenuItem
            // 
            this.manuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.restartAsAdministratorToolStripMenuItem,
            this.aboutToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.manuToolStripMenuItem.Name = "manuToolStripMenuItem";
            this.manuToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.manuToolStripMenuItem.Text = "Menu";
            // 
            // restartAsAdministratorToolStripMenuItem
            // 
            this.restartAsAdministratorToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.restartAsAdministratorToolStripMenuItem.Name = "restartAsAdministratorToolStripMenuItem";
            this.restartAsAdministratorToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.restartAsAdministratorToolStripMenuItem.Text = "Restart as Administrator";
            this.restartAsAdministratorToolStripMenuItem.Click += new System.EventHandler(this.restartAsAdministratorToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(1154, 5);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 13);
            this.lblStatus.TabIndex = 11;
            // 
            // btnRestartService
            // 
            this.btnRestartService.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.btnRestartService.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestartService.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRestartService.Location = new System.Drawing.Point(3, 243);
            this.btnRestartService.Name = "btnRestartService";
            this.btnRestartService.Size = new System.Drawing.Size(298, 40);
            this.btnRestartService.TabIndex = 12;
            this.btnRestartService.Text = "Restart Azure Arc Service";
            this.btnRestartService.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnRestartService.Click += new System.EventHandler(this.btnRestartService_Click);
            // 
            // tabControlMainForm
            // 
            this.tabControlMainForm.Controls.Add(this.tabPage1);
            this.tabControlMainForm.Controls.Add(this.tabPage2);
            this.tabControlMainForm.Location = new System.Drawing.Point(12, 25);
            this.tabControlMainForm.Name = "tabControlMainForm";
            this.tabControlMainForm.SelectedIndex = 0;
            this.tabControlMainForm.Size = new System.Drawing.Size(315, 408);
            this.tabControlMainForm.TabIndex = 13;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lblCheckVersion);
            this.tabPage1.Controls.Add(this.btnRestartService);
            this.tabPage1.Controls.Add(this.lblCheckAgentError);
            this.tabPage1.Controls.Add(this.lblShowAgentConfig);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.lblShowAgentMode);
            this.tabPage1.Controls.Add(this.lblChangeMode2Full);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(307, 382);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Basic tasks";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(307, 102);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Configuration tasks";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(1237, 590);
            this.Controls.Add(this.tabControlMainForm);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblExportLogs);
            this.Controls.Add(this.lblUpdateArcAgent);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.spltButtonScreen);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.SystemColors.Highlight;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(1);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1000, 500);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControlMainForm.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Splitter spltButtonScreen;
        private System.Windows.Forms.Label lblCheckVersion;
        private System.Windows.Forms.RichTextBox txtOutput;
        private System.Windows.Forms.Label lblCheckAgentError;
        private System.Windows.Forms.Label lblUpdateArcAgent;
        private System.Windows.Forms.Label lblExportLogs;
        private System.Windows.Forms.Label lblShowAgentMode;
        private System.Windows.Forms.Label lblShowAgentConfig;
        private System.Windows.Forms.Label lblChangeMode2Full;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem manuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restartAsAdministratorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label btnRestartService;
        private System.Windows.Forms.TabControl tabControlMainForm;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
    }
}

