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
            this.btnChooseRegion = new System.Windows.Forms.Button();
            this.cmbChooseRegion = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnChooseRegion
            // 
            this.btnChooseRegion.Location = new System.Drawing.Point(12, 46);
            this.btnChooseRegion.Name = "btnChooseRegion";
            this.btnChooseRegion.Size = new System.Drawing.Size(75, 35);
            this.btnChooseRegion.TabIndex = 1;
            this.btnChooseRegion.Text = "Ok";
            this.btnChooseRegion.UseVisualStyleBackColor = true;
            this.btnChooseRegion.Click += new System.EventHandler(this.btnChooseRegion_Click);
            // 
            // cmbChooseRegion
            // 
            this.cmbChooseRegion.FormattingEnabled = true;
            this.cmbChooseRegion.Items.AddRange(new object[] {
            "---Europe---",
            "",
            "westeurope"});
            this.cmbChooseRegion.Location = new System.Drawing.Point(12, 12);
            this.cmbChooseRegion.Name = "cmbChooseRegion";
            this.cmbChooseRegion.Size = new System.Drawing.Size(447, 28);
            this.cmbChooseRegion.TabIndex = 2;
            // 
            // RegionInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 92);
            this.Controls.Add(this.cmbChooseRegion);
            this.Controls.Add(this.btnChooseRegion);
            this.Name = "RegionInput";
            this.Text = "Choose your region";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnChooseRegion;
        private System.Windows.Forms.ComboBox cmbChooseRegion;
    }
}