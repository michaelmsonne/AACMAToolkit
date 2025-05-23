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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.manuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restartAsAdministratorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changelogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblStatus = new System.Windows.Forms.Label();
            this.tabControlMainForm = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lblGetAutomaticUpgradeConfig = new System.Windows.Forms.Label();
            this.lblGetFullDetails = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lblAllowAllUseOfExtentions = new System.Windows.Forms.Label();
            this.lblDisableAllUseOfExtentions = new System.Windows.Forms.Label();
            this.lblManageExtentions = new System.Windows.Forms.Label();
            this.lblChangeTier0 = new System.Windows.Forms.Label();
            this.lblChangeModeToMonitor = new System.Windows.Forms.Label();
            this.lblChangeModeToFull = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.lblRestartService = new System.Windows.Forms.Label();
            this.lblCheckAgentConnection = new System.Windows.Forms.Label();
            this.lblCheckPrivateEndpoints = new System.Windows.Forms.Label();
            this.lblCheckPublicEndpoints = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.tabControlMainForm.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
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
            this.lblUpdateArcAgent.Location = new System.Drawing.Point(12, 533);
            this.lblUpdateArcAgent.Name = "lblUpdateArcAgent";
            this.lblUpdateArcAgent.Size = new System.Drawing.Size(315, 45);
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
            this.lblExportLogs.Location = new System.Drawing.Point(12, 492);
            this.lblExportLogs.Name = "lblExportLogs";
            this.lblExportLogs.Size = new System.Drawing.Size(315, 41);
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
            this.changelogToolStripMenuItem,
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
            // changelogToolStripMenuItem
            // 
            this.changelogToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.changelogToolStripMenuItem.Name = "changelogToolStripMenuItem";
            this.changelogToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.changelogToolStripMenuItem.Text = "Changelog";
            this.changelogToolStripMenuItem.Click += new System.EventHandler(this.changelogToolStripMenuItem_Click);
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
            this.lblStatus.Location = new System.Drawing.Point(1147, 5);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 13);
            this.lblStatus.TabIndex = 11;
            // 
            // tabControlMainForm
            // 
            this.tabControlMainForm.Controls.Add(this.tabPage1);
            this.tabControlMainForm.Controls.Add(this.tabPage2);
            this.tabControlMainForm.Controls.Add(this.tabPage3);
            this.tabControlMainForm.Location = new System.Drawing.Point(12, 25);
            this.tabControlMainForm.Name = "tabControlMainForm";
            this.tabControlMainForm.SelectedIndex = 0;
            this.tabControlMainForm.Size = new System.Drawing.Size(315, 464);
            this.tabControlMainForm.TabIndex = 13;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lblGetAutomaticUpgradeConfig);
            this.tabPage1.Controls.Add(this.lblGetFullDetails);
            this.tabPage1.Controls.Add(this.lblCheckVersion);
            this.tabPage1.Controls.Add(this.lblCheckAgentError);
            this.tabPage1.Controls.Add(this.lblShowAgentConfig);
            this.tabPage1.Controls.Add(this.lblShowAgentMode);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(307, 438);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Basic tasks";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lblGetAutomaticUpgradeConfig
            // 
            this.lblGetAutomaticUpgradeConfig.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblGetAutomaticUpgradeConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblGetAutomaticUpgradeConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGetAutomaticUpgradeConfig.Location = new System.Drawing.Point(3, 203);
            this.lblGetAutomaticUpgradeConfig.Name = "lblGetAutomaticUpgradeConfig";
            this.lblGetAutomaticUpgradeConfig.Size = new System.Drawing.Size(298, 40);
            this.lblGetAutomaticUpgradeConfig.TabIndex = 14;
            this.lblGetAutomaticUpgradeConfig.Text = "Get automatic upgrade status";
            this.lblGetAutomaticUpgradeConfig.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblGetAutomaticUpgradeConfig.Click += new System.EventHandler(this.lblGetAutomaticUpgradeConfig_Click);
            //
            // lblChangeTier0
            // 
            this.lblChangeTier0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblChangeTier0.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblChangeTier0.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChangeTier0.Location = new System.Drawing.Point(3, 83);
            this.lblChangeTier0.Name = "lblChangeTier0";
            this.lblChangeTier0.Size = new System.Drawing.Size(298, 40);
            this.lblChangeTier0.TabIndex = 11;
            this.lblChangeTier0.Text = "Change 2 Tier0 config";
            this.lblChangeTier0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblChangeTier0.Click += new System.EventHandler(this.lblChangeTier0_Click);
            // 
            // lblGetFullDetails
            // 
            this.lblGetFullDetails.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblGetFullDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblGetFullDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGetFullDetails.Location = new System.Drawing.Point(3, 163);
            this.lblGetFullDetails.Name = "lblGetFullDetails";
            this.lblGetFullDetails.Size = new System.Drawing.Size(298, 40);
            this.lblGetFullDetails.TabIndex = 13;
            this.lblGetFullDetails.Text = "Retrieve machine/agent status";
            this.lblGetFullDetails.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblGetFullDetails.Click += new System.EventHandler(this.lblGetFullDetails_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lblAllowAllUseOfExtentions);
            this.tabPage2.Controls.Add(this.lblDisableAllUseOfExtentions);
            this.tabPage2.Controls.Add(this.lblManageExtentions);
            this.tabPage2.Controls.Add(this.lblChangeTier0);
            this.tabPage2.Controls.Add(this.lblChangeModeToMonitor);
            this.tabPage2.Controls.Add(this.lblChangeModeToFull);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(307, 438);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Configuration tasks";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lblAllowAllUseOfExtentions
            // 
            this.lblAllowAllUseOfExtentions.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAllowAllUseOfExtentions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblAllowAllUseOfExtentions.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAllowAllUseOfExtentions.Location = new System.Drawing.Point(3, 203);
            this.lblAllowAllUseOfExtentions.Name = "lblAllowAllUseOfExtentions";
            this.lblAllowAllUseOfExtentions.Size = new System.Drawing.Size(298, 40);
            this.lblAllowAllUseOfExtentions.TabIndex = 14;
            this.lblAllowAllUseOfExtentions.Text = "Allow use of extentions";
            this.lblAllowAllUseOfExtentions.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAllowAllUseOfExtentions.Click += new System.EventHandler(this.lblAllowAllUseOfExtentions_Click);
            // 
            // lblDisableAllUseOfExtentions
            // 
            this.lblDisableAllUseOfExtentions.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDisableAllUseOfExtentions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblDisableAllUseOfExtentions.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDisableAllUseOfExtentions.Location = new System.Drawing.Point(3, 163);
            this.lblDisableAllUseOfExtentions.Name = "lblDisableAllUseOfExtentions";
            this.lblDisableAllUseOfExtentions.Size = new System.Drawing.Size(298, 40);
            this.lblDisableAllUseOfExtentions.TabIndex = 13;
            this.lblDisableAllUseOfExtentions.Text = "Disable use of extentions";
            this.lblDisableAllUseOfExtentions.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDisableAllUseOfExtentions.Click += new System.EventHandler(this.lblDisableAllUseOfExtentions_Click);
            // 
            // lblManageExtentions
            // 
            this.lblManageExtentions.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblManageExtentions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblManageExtentions.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblManageExtentions.Location = new System.Drawing.Point(3, 123);
            this.lblManageExtentions.Name = "lblManageExtentions";
            this.lblManageExtentions.Size = new System.Drawing.Size(298, 40);
            this.lblManageExtentions.TabIndex = 12;
            this.lblManageExtentions.Text = "Manage Extentions";
            this.lblManageExtentions.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblManageExtentions.Click += new System.EventHandler(this.lblManageExtentions_Click);
            // 
            // lblChangeTier0
            // 
            this.lblChangeTier0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblChangeTier0.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblChangeTier0.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChangeTier0.Location = new System.Drawing.Point(3, 83);
            this.lblChangeTier0.Name = "lblChangeTier0";
            this.lblChangeTier0.Size = new System.Drawing.Size(298, 40);
            this.lblChangeTier0.TabIndex = 11;
            this.lblChangeTier0.Text = "Change 2 Tier0 config";
            this.lblChangeTier0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblChangeTier0.Click += new System.EventHandler(this.lblChangeTier0_Click);
            // 
            // lblChangeModeToMonitor
            // 
            this.lblChangeModeToMonitor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblChangeModeToMonitor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblChangeModeToMonitor.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChangeModeToMonitor.Location = new System.Drawing.Point(3, 43);
            this.lblChangeModeToMonitor.Name = "lblChangeModeToMonitor";
            this.lblChangeModeToMonitor.Size = new System.Drawing.Size(298, 40);
            this.lblChangeModeToMonitor.TabIndex = 10;
            this.lblChangeModeToMonitor.Text = "Change 2 Monitor mode";
            this.lblChangeModeToMonitor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblChangeModeToMonitor.Click += new System.EventHandler(this.lblChangeModeToMonitor_Click);
            // 
            // lblChangeModeToFull
            // 
            this.lblChangeModeToFull.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblChangeModeToFull.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblChangeModeToFull.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChangeModeToFull.Location = new System.Drawing.Point(3, 3);
            this.lblChangeModeToFull.Name = "lblChangeModeToFull";
            this.lblChangeModeToFull.Size = new System.Drawing.Size(298, 40);
            this.lblChangeModeToFull.TabIndex = 9;
            this.lblChangeModeToFull.Text = "Change 2 Full mode";
            this.lblChangeModeToFull.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblChangeModeToFull.Click += new System.EventHandler(this.lblChangeModeToFull_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.lblCheckPrivateEndpoints);
            this.tabPage3.Controls.Add(this.lblCheckPublicEndpoints);
            this.tabPage3.Controls.Add(this.lblRestartService);
            this.tabPage3.Controls.Add(this.lblCheckAgentConnection);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(307, 438);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Troubleshooting";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // lblCheckPrivateEndpoints
            // 
            this.lblCheckPrivateEndpoints.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCheckPrivateEndpoints.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblCheckPrivateEndpoints.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheckPrivateEndpoints.Location = new System.Drawing.Point(3, 123);
            this.lblCheckPrivateEndpoints.Name = "lblCheckPrivateEndpoints";
            this.lblCheckPrivateEndpoints.Size = new System.Drawing.Size(298, 40);
            this.lblCheckPrivateEndpoints.TabIndex = 18;
            this.lblCheckPrivateEndpoints.Text = "Connectivity Private endpoints";
            this.lblCheckPrivateEndpoints.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCheckPublicEndpoints
            // 
            this.lblCheckPublicEndpoints.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCheckPublicEndpoints.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblCheckPublicEndpoints.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheckPublicEndpoints.Location = new System.Drawing.Point(3, 83);
            this.lblCheckPublicEndpoints.Name = "lblCheckPublicEndpoints";
            this.lblCheckPublicEndpoints.Size = new System.Drawing.Size(298, 40);
            this.lblCheckPublicEndpoints.TabIndex = 17;
            this.lblCheckPublicEndpoints.Text = "Connectivity Public endpoints";
            this.lblCheckPublicEndpoints.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCheckPublicEndpoints.Click += new System.EventHandler(this.lblCheckPublicEndpoints_Click);
            // 
            // lblRestartService
            // 
            this.lblRestartService.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRestartService.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblRestartService.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRestartService.Location = new System.Drawing.Point(3, 3);
            this.lblRestartService.Name = "lblRestartService";
            this.lblRestartService.Size = new System.Drawing.Size(298, 40);
            this.lblRestartService.TabIndex = 16;
            this.lblRestartService.Text = "Restart Azure Arc Service";
            this.lblRestartService.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRestartService.Click += new System.EventHandler(this.lblRestartService_Click);
            // 
            // lblCheckAgentConnection
            // 
            this.lblCheckAgentConnection.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCheckAgentConnection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblCheckAgentConnection.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheckAgentConnection.Location = new System.Drawing.Point(3, 43);
            this.lblCheckAgentConnection.Name = "lblCheckAgentConnection";
            this.lblCheckAgentConnection.Size = new System.Drawing.Size(298, 40);
            this.lblCheckAgentConnection.TabIndex = 15;
            this.lblCheckAgentConnection.Text = "Run connectivity checks";
            this.lblCheckAgentConnection.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCheckAgentConnection.Click += new System.EventHandler(this.lblCheckAgentConnection_Click);
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
            this.Text = "Azure Connected Machine Agent v. x.x.x.x";
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControlMainForm.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
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
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem manuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restartAsAdministratorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TabControl tabControlMainForm;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label lblGetFullDetails;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label lblCheckAgentConnection;
        private System.Windows.Forms.Label lblRestartService;
        private System.Windows.Forms.Label lblChangeModeToFull;
        private System.Windows.Forms.Label lblChangeModeToMonitor;
        private System.Windows.Forms.Label lblChangeTier0;
        private System.Windows.Forms.Label lblGetAutomaticUpgradeConfig;
        private System.Windows.Forms.Label lblManageExtentions;
        private System.Windows.Forms.Label lblDisableAllUseOfExtentions;
        private System.Windows.Forms.Label lblAllowAllUseOfExtentions;
        private System.Windows.Forms.ToolStripMenuItem changelogToolStripMenuItem;
        private System.Windows.Forms.Label lblCheckPublicEndpoints;
        private System.Windows.Forms.Label lblCheckPrivateEndpoints;
    }
}

