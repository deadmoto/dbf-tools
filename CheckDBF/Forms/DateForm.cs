using System;
using System.Windows.Forms;

namespace CheckDBF.Forms
{
    public partial class DateForm : Form
    {
        public DateForm()
        {
            InitializeComponent();
            MonthComboBox.SelectedIndex = DateTime.Now.Month - 1;
        }

        public DateTime GetDate()
        {
            DateTime Date = DateTime.MinValue;

            if (ShowDialog() == DialogResult.OK)
            {
                Date = new DateTime((int)YearUpDown.Value, MonthComboBox.SelectedIndex + 1, 1);
            }

            return Date;
        }
    }
}