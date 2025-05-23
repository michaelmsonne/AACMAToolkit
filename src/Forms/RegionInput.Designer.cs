
namespace AACMAToolkit.Forms
{
    partial class RegionInput
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegionInput));
            this.btnChooseRegion = new System.Windows.Forms.Button();
            this.cmbChooseRegion = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnChooseRegion
            // 
            this.btnChooseRegion.Location = new System.Drawing.Point(311, 8);
            this.btnChooseRegion.Margin = new System.Windows.Forms.Padding(2);
            this.btnChooseRegion.Name = "btnChooseRegion";
            this.btnChooseRegion.Size = new System.Drawing.Size(50, 23);
            this.btnChooseRegion.TabIndex = 1;
            this.btnChooseRegion.Text = "Check";
            this.btnChooseRegion.UseVisualStyleBackColor = true;
            this.btnChooseRegion.Click += new System.EventHandler(this.btnChooseRegion_Click);
            // 
            // cmbChooseRegion
            // 
            this.cmbChooseRegion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbChooseRegion.FormattingEnabled = true;
            this.cmbChooseRegion.Location = new System.Drawing.Point(8, 9);
            this.cmbChooseRegion.Margin = new System.Windows.Forms.Padding(2);
            this.cmbChooseRegion.Name = "cmbChooseRegion";
            this.cmbChooseRegion.Size = new System.Drawing.Size(299, 21);
            this.cmbChooseRegion.TabIndex = 2;
            // 
            // RegionInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(369, 41);
            this.Controls.Add(this.cmbChooseRegion);
            this.Controls.Add(this.btnChooseRegion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RegionInput";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Choose your region to check connectivity for";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RegionInput_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnChooseRegion;
        private System.Windows.Forms.ComboBox cmbChooseRegion;
    }
}