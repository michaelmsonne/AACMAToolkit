using System;
using System.Windows.Forms;

namespace AACMAToolkit.Forms
{
    public partial class RegionInput : Form
    {
        public string SelectedRegion { get; set; }

        public RegionInput()
        {
            InitializeComponent();

            object[] regions = {
                new RegionItem { Display = "West Europe", Value = "westeurope" },
                new RegionItem { Display = "North Europe", Value = "northeurope" },
                new RegionItem { Display = "East US", Value = "eastus" },
                new RegionItem { Display = "East US 2", Value = "eastus2" },
                new RegionItem { Display = "West US", Value = "westus" },
                new RegionItem { Display = "Central US", Value = "centralus" },
                new RegionItem { Display = "South Central US", Value = "southcentralus" },
                new RegionItem { Display = "Brazil South", Value = "brazilsouth" },
                new RegionItem { Display = "Southeast Asia", Value = "southeastasia" },
                new RegionItem { Display = "East Asia", Value = "eastasia" },
                new RegionItem { Display = "Australia East", Value = "australiaeast" },
                new RegionItem { Display = "Australia Southeast", Value = "australiasoutheast" },
                new RegionItem { Display = "UK South", Value = "uksouth" },
                new RegionItem { Display = "UK West", Value = "ukwest" },
                new RegionItem { Display = "France Central", Value = "francecentral" },
                new RegionItem { Display = "Germany West Central", Value = "germanywestcentral" },
                new RegionItem { Display = "Norway East", Value = "norwayeast" },
                new RegionItem { Display = "Switzerland North", Value = "switzerlandnorth" },
                new RegionItem { Display = "UAE North", Value = "uaenorth" },
                new RegionItem { Display = "South Africa North", Value = "southafricanorth" }
            };
            cmbChooseRegion.Items.AddRange(regions);
            cmbChooseRegion.SelectedIndex = 0;
        }

        private class RegionItem
        {
            public string Display { get; set; }
            public string Value { get; set; }
            public override string ToString() => Display;
        }

        private void btnChooseRegion_Click(object sender, EventArgs e)
        {
            SelectedRegion = ((RegionItem)cmbChooseRegion.SelectedItem).Value;
            Close();
        }
    }
}