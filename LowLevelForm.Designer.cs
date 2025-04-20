namespace AACMAToolkit
{
    partial class AACMAToolkit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AACMAToolkit));
            this.spltButtonScreen = new System.Windows.Forms.Splitter();
            this.lblCheckVersion = new System.Windows.Forms.Label();
            this.txtOutput = new System.Windows.Forms.RichTextBox();
            this.lblCheckAgentError = new System.Windows.Forms.Label();
            this.lblOpenConfigMode = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // spltButtonScreen
            // 
            this.spltButtonScreen.Location = new System.Drawing.Point(0, 0);
            this.spltButtonScreen.Name = "spltButtonScreen";
            this.spltButtonScreen.Size = new System.Drawing.Size(336, 590);
            this.spltButtonScreen.TabIndex = 0;
            this.spltButtonScreen.TabStop = false;
            // 
            // lblCheckVersion
            // 
            this.lblCheckVersion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCheckVersion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblCheckVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheckVersion.Location = new System.Drawing.Point(12, 9);
            this.lblCheckVersion.Name = "lblCheckVersion";
            this.lblCheckVersion.Size = new System.Drawing.Size(298, 65);
            this.lblCheckVersion.TabIndex = 1;
            this.lblCheckVersion.Text = "Check Agent Version";
            this.lblCheckVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCheckVersion.Click += new System.EventHandler(this.lblCheckVersion_Click);
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(342, 9);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(895, 569);
            this.txtOutput.TabIndex = 2;
            this.txtOutput.Text = "";
            // 
            // lblCheckAgentError
            // 
            this.lblCheckAgentError.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCheckAgentError.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblCheckAgentError.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheckAgentError.Location = new System.Drawing.Point(12, 74);
            this.lblCheckAgentError.Name = "lblCheckAgentError";
            this.lblCheckAgentError.Size = new System.Drawing.Size(298, 65);
            this.lblCheckAgentError.TabIndex = 3;
            this.lblCheckAgentError.Text = "Check Agent Error";
            this.lblCheckAgentError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCheckAgentError.Click += new System.EventHandler(this.lblCheckAgentError_Click);
            // 
            // lblOpenConfigMode
            // 
            this.lblOpenConfigMode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblOpenConfigMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblOpenConfigMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOpenConfigMode.Location = new System.Drawing.Point(12, 513);
            this.lblOpenConfigMode.Name = "lblOpenConfigMode";
            this.lblOpenConfigMode.Size = new System.Drawing.Size(298, 65);
            this.lblOpenConfigMode.TabIndex = 6;
            this.lblOpenConfigMode.Text = "Open Configurator (elevated)";
            this.lblOpenConfigMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblOpenConfigMode.Click += new System.EventHandler(this.lblOpenConfigMode_Click);
            // 
            // AACMAToolkit
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(1237, 590);
            this.Controls.Add(this.lblOpenConfigMode);
            this.Controls.Add(this.lblCheckAgentError);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.lblCheckVersion);
            this.Controls.Add(this.spltButtonScreen);
            this.ForeColor = System.Drawing.SystemColors.Highlight;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(1);
            this.MinimumSize = new System.Drawing.Size(1000, 500);
            this.Name = "AACMAToolkit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.Load += new System.EventHandler(this.AACMAToolkit_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Splitter spltButtonScreen;
        private System.Windows.Forms.Label lblCheckVersion;
        private System.Windows.Forms.RichTextBox txtOutput;
        private System.Windows.Forms.Label lblCheckAgentError;
        private System.Windows.Forms.Label lblOpenConfigMode;
    }
}

