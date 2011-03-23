using System;
using System.Windows.Forms;

namespace CheckDBF.Forms
{
    public partial class ReplaceForm : Form
    {
        public ReplaceForm()
        {
            InitializeComponent();
        }

        private void SearchClick(object sender, EventArgs e)
        {
            GridView.Rows.Clear();

            foreach (ReplaceData ReplaceData in Replace.GetReplaceList(SearchTextBox.Text))
            {
                GridView.Rows.Add(ReplaceData.PREDK, ReplaceData.VID, ReplaceData.PREDU, Supplier.GetSupplierName(ReplaceData.PREDU.ToString()));
            }
        }
    }
}
