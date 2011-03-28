using System;
using System.Windows.Forms;

namespace CheckDBF.Forms
{
    public partial class ConformForm : Form
    {
        public ConformForm()
        {
            InitializeComponent();
        }

        private void SearchButtonClick(object sender, EventArgs e)
        {
            GridView.Rows.Clear();

            foreach (ConformData ConformData in Conform.GetConformList(PREDTextBox.Text, VIDTextBox.Text, KOD_TTextBox.Text, KOD_NTextBox.Text))
            {
                GridView.Rows.Add(ConformData.PRED, ConformData.VID, ConformData.KOD_T, ConformData.TARIF, ConformData.KOD_N, ConformData.VOL, Supplier.GetSupplierName(ConformData.PRED.ToString()));
            }
        }
    }
}
