using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace SplitDBF
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Opens OpenFileDialog with *.dbf filter
        /// </summary>
        private void OpenMenuItemClick(object sender, EventArgs e)
        {
            OpenFileDialog OpenFile = new OpenFileDialog();
            OpenFile.Filter = "DBF Files (*.dbf) | *.dbf";
            OpenFile.Multiselect = true;
            if (OpenFile.ShowDialog() == DialogResult.OK)
            {
                GetFileInfo(OpenFile.FileNames);
            }
        }

        private void GetFileInfo(string[] FileNames)
        {
            foreach (string FileName in FileNames)
            {
                MainGrid.Rows.Add(true, FileName, SplitDBF.Region.GetRegionName(GetRegionCode(FileName)), GetReportDate(FileName), "Waiting");
            }
        }

        private void ProcessMenuItemClick(object sender, EventArgs e)
        {
            List<string> FileNames = new List<string>();

            foreach (DataGridViewRow Row in MainGrid.Rows)
            {
                FileNames.Add((string)Row.Cells[1].Value);
            }

            new Thread(new ThreadStart(delegate { ProcessFiles(FileNames); })).Start();
        }

        private void ProcessFiles(List<string> FileNames)
        {
            foreach (string InputFileName in FileNames)
            {
                int RowIndex = FileNames.FindIndex(delegate(string Item) { return Item == InputFileName; });

                List<string> ListPred = new List<string>();

                OdbcCommand Command = OdbcDriver.VFPCommand();
                OdbcDataReader Reader;

                string RegionCode = GetRegionCode(InputFileName);
                string ReportDate = GetReportDate(InputFileName);

                for (int i = 1; i < 10; i++)
                {
                    Command.CommandText = string.Format("SELECT DISTINCT PRED{0} FROM '{1}' WHERE PRED{0} > 0", i, InputFileName);
                    Reader = Command.ExecuteReader();

                    while (Reader.Read())
                    {
                        string Post = Reader["PRED" + i].ToString();
                        if (!ListPred.Contains(Post))
                        {
                            ListPred.Add(Post);
                        }
                    }
                    Reader.Close();
                }

                foreach (string Code in ListPred)
                {
                    int CodeIndex = ListPred.FindIndex(delegate(string Item) { return Item == Code; });
                    MainGrid.Invoke(new MethodInvoker(delegate { MainGrid.Rows[RowIndex].Cells[4].Value = string.Format("Processing {0} of {1}", CodeIndex.ToString(), ListPred.Count.ToString()); Refresh(); }));
                    string subFolder = string.Empty;
                    if (InputFileName.Contains("payment"))
                        subFolder = "payment";
                    else
                        subFolder = "des";

                    string OutputDirectory = string.Format("{0}\\{1}", Path.GetDirectoryName(InputFileName), Supplier.GetSupplierName(Code));//, SplitDBF.Region.GetRegionName(RegionCode));
                    string OutputFileName = string.Format("{0}\\{2}{1}.dbf", OutputDirectory, ReportDate + "_" + RegionCode, subFolder);
                    Directory.CreateDirectory(OutputDirectory);
                    Command = OdbcDriver.VFPCommand(OutputDirectory, "");

                    File.Copy(InputFileName, OutputFileName, true);

                    for (int i = 1; i < 10; i++)
                    {
                        Command.CommandText = string.Format("UPDATE '{0}' SET PRED{1} = 0, VID{1} = '', LSH{1} = '' WHERE PRED{1} <> {2}", OutputFileName, i.ToString(), Code);
                        Command.ExecuteNonQuery();
                    }
                    Command.CommandText = string.Format("DELETE FROM '{0}' WHERE PRED1 <> {1} AND PRED2 <> {1} AND PRED3 <> {1} AND PRED4 <> {1} AND PRED5 <> {1} AND PRED6 <> {1} AND PRED7 <> {1} AND PRED8 <> {1} AND PRED9 <> {1}", OutputFileName, Code);
                    Command.ExecuteNonQuery();
                    Command.Connection.Close();
                    Command.Connection.Open();
                    Command.CommandText = string.Format("PACK '{0}'", OutputFileName);
                    Command.ExecuteNonQuery();
                }

                MainGrid.Invoke(new MethodInvoker(delegate { MainGrid.Rows[RowIndex].Cells[4].Value = "Ready"; Refresh(); }));
            }

            MessageBox.Show(string.Format("{0} files processed!", FileNames.Count), Text);
        }

        /// <summary>
        /// Returns region code from file
        /// </summary>
        private string GetRegionCode(string FileName)
        {
            string RegionCode = string.Empty;
            OdbcCommand Command = OdbcDriver.VFPCommand(CommandText: string.Format("SELECT PY FROM '{0}'", FileName));
            OdbcDataReader Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                RegionCode = Reader["PY"].ToString().Trim();
                break;
            }
            return RegionCode;
        }

        /// <summary>
        /// Returns report date from file
        /// </summary>
        string GetReportDate(string InputFileName)
        {
            DateTime ReportDate = new DateTime();
            try
            {
                OdbcCommand Command = OdbcDriver.VFPCommand(CommandText: string.Format("SELECT DATE_VIGR FROM '{0}'", InputFileName));
                OdbcDataReader Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    ReportDate = Reader.GetDate(Reader.GetOrdinal("DATE_VIGR"));
                    break;
                }
            }
            catch
            {
                ReportDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 01);
            }
            return ReportDate.ToString("yyyy-MM-dd");
        }

        private void MainGridDragDrop(object sender, DragEventArgs e)
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

            GetFileInfo(FileNames.ToArray());
        }

        private void MainGridDragEnter(object sender, DragEventArgs e)
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
