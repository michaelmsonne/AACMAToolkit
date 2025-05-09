using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AACMAToolkit.Forms
{
    public partial class RegionInput : Form
    {
        public string SelectedRegion { get; set; }

        public RegionInput()
        {
            InitializeComponent();
        }

        private void btnChooseRegion_Click(object sender, EventArgs e)
        {
            SelectedRegion = cmbChooseRegion.SelectedItem.ToString();
            Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
