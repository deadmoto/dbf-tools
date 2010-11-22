using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.Odbc;
using System.IO;
using System.Threading;

namespace PrintDBF
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void OpenMenuItemClick(object sender, EventArgs e)
        {
            OpenFileDialog OpenFile = new OpenFileDialog();
            OpenFile.Filter = "DBF Files (*.dbf) | *.dbf";
            OpenFile.Multiselect = true;
            if (OpenFile.ShowDialog() == DialogResult.OK)
            {
                textBox1.Lines = OpenFile.FileNames;
                OpenFiles(new List<string>(OpenFile.FileNames));
            }
        }

        private void PrintMenuItemClick(object sender, EventArgs e)
        {
            for (int i = 0; i < DataGridView.Rows.Count; i++)
            {
                string FileName = DataGridView[1, i].Value.ToString();
                OdbcCommand Command = OdbcDriver.VFPCommand(string.Format("SELECT * FROM '{0}'", FileName), "");
                OdbcDataReader Reader = Command.ExecuteReader();

                int FieldCount = Reader.FieldCount;

                Reader.Close();
                Reader.Dispose();

                if (FieldCount == 8)
                {
                    Command.CommandText = string.Format("SELECT SUM(VAL(TRIM(F))) AS MONEY FROM '{0}' WHERE VAL(A) > 0", FileName);
                }
                else
                //if (FieldCount == 27 || FieldCount == 12)
                {
                    continue;
                    Command.CommandText = string.Format("SELECT SUM(VAL(TRIM(ALLAMOUNT))) AS MONEY FROM '{0}'", FileName);
                }

                Reader = Command.ExecuteReader();

                double Sum = 0;

                if (Reader.Read())
                {
                    Sum = Math.Round(Reader.GetDouble(Reader.GetOrdinal("MONEY")), 2);
                }
                Reader.Close();

                if (FieldCount == 8)
                {
                    Command.CommandText = string.Format("SELECT CPCONVERT(866, 1251, TRIM(C) + ' ' + TRIM(D) + ' ' + TRIM(E)) AS FULLNAME, TRIM(B) AS ACCOUNT, TRIM(F) AS MONEY FROM '{0}' WHERE VAL(A) > 0", FileName);
                }
                else
                //if (FieldCount == 27 || FieldCount == 12)
                {
                    Command.CommandText = string.Format("SELECT CPCONVERT(866, 1251, TRIM(FIO)) AS FULLNAME, TRIM(ORGNAME) AS ACCOUNT, TRIM(ALLAMOUNT) AS MONEY FROM '{0}'", FileName);
                }

                Reader = Command.ExecuteReader();

                DataSet.Tables["Table"].Rows.Clear();
                while (Reader.Read())
                {
                    string FULLNAME1 = Reader.GetString(Reader.GetOrdinal("FULLNAME"));
                    string ACCOUNT1 = Reader.GetString(Reader.GetOrdinal("ACCOUNT"));
                    string MONEY1 = Reader.GetString(Reader.GetOrdinal("MONEY"));
                    string FULLNAME2 = "";
                    string ACCOUNT2 = "";
                    string MONEY2 = "";
                    if (Reader.Read())
                    {
                        FULLNAME2 = Reader.GetString(Reader.GetOrdinal("FULLNAME"));
                        ACCOUNT2 = Reader.GetString(Reader.GetOrdinal("ACCOUNT"));
                        MONEY2 = Reader.GetString(Reader.GetOrdinal("MONEY"));
                    }
                    DataSet.Tables["Table"].Rows.Add(FULLNAME1, ACCOUNT1, MONEY1, FULLNAME2, ACCOUNT2, MONEY2);
                }

                FastReport.SetParameterValue("FileName", FileName);
                FastReport.SetParameterValue("Sum", Sum);

                if (ShowReportMenuItem.Checked)
                {
                    FastReport.Show();
                }
                else
                {
                    FastReport.PrintSettings.ShowDialog = false;
                    FastReport.FileName = FileName;
                    FastReport.Print();
                }
            }
        }

        private void OpenFiles(List<string> FileNames)
        {
            foreach (string FileName in FileNames)
            {
                DataGridView.Invoke(new MethodInvoker(delegate { DataGridView.Rows.Add(false, FileName, "0.00"); }));
            }
        }

        private void openListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenFile = new OpenFileDialog();
            OpenFile.Filter = "Text files (*.txt) | *.txt";
            OpenFile.Multiselect = false;
            if (OpenFile.ShowDialog() == DialogResult.OK)
            {
                List<string> FileNames = new List<string>();
                StreamReader r = new StreamReader(OpenFile.FileName);
                while (!r.EndOfStream)
                {
                    FileNames.Add(r.ReadLine());
                }
                textBox1.Lines = FileNames.ToArray();
                OpenFiles(FileNames);
            }
        }

        private void DataGridViewDragDrop(object sender, DragEventArgs e)
        {
            string[] DropFileNames = (string[])e.Data.GetData(DataFormats.FileDrop);
            List<string> FileNames = new List<string>();

            foreach (string FileName in DropFileNames)
            {
                if (Directory.Exists(FileName))
                {
                    FileNames.AddRange(Directory.GetFiles(FileName, "*.dbf", SearchOption.AllDirectories));
                }
                else
                {
                    FileNames.Add(FileName);
                }
            }

            OpenFiles(FileNames);
        }

        private void DataGridViewDragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void ExitMenuItemClick(object sender, EventArgs e)
        {
            Close();
        }
    }
}