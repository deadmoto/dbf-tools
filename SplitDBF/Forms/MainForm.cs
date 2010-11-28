using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using SplitDBF.Data;

namespace SplitDBF
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

        private List<OutputFile> GetOutputFileList(string FileName)
        {
            List<OutputFile> OutputFileList = new List<OutputFile>();
            OleDbCommand Command = FoxPro.OleDbCommand();

            for (int i = 1; i < 10; i++)
            {
                Command.CommandText = string.Format("SELECT DISTINCT PRED{0} FROM {1} WHERE NOT EMPTY(PRED{0})", i, FileName);

                try
                {
                    OleDbDataReader Reader = Command.ExecuteReader();
                    while (Reader.Read())
                    {
                        string PRED = Reader[0].ToString();
                        if (OutputFileList.Contains(PRED) == false)
                        {
                            OutputFile OutputFile = PRED;
                            OutputFileList.Add(PRED);
                        }
                    }
                    Reader.Close();
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message);
                }
            }

            Command.Connection.Close();
            return OutputFileList;
        }

        private void ProcessFile(string FileName)
        {
            string ReportDate = GetReportDate(FileName);
            List<RECP> RECPList = new List<RECP>();

            string CommandText = string.Format("SELECT * FROM {0}", FileName);
            OleDbCommand Command = FoxPro.OleDbCommand(CommandText);
            OleDbDataReader Reader = Command.ExecuteReader();

            int Cursor = 0;

            while (Reader.Read())
            {
                Cursor++;
                RECP Item = new RECP();
                Item.FAMIL = Reader["FAMIL"].ToString().Trim();
                Item.IMJA = Reader["IMJA"].ToString().Trim();
                Item.OTCH = Reader["OTCH"].ToString().Trim();
                Item.DROG = Reader.GetDateTime(Reader.GetOrdinal("DROG"));
                Item.NPSS = Reader["NPSS"].ToString().Trim();
                Item.PY = Reader["PY"].ToString().Trim();
                Item.NNASP = Reader["NNASP"].ToString().Trim();
                Item.NYLIC = Reader["NYLIC"].ToString().Trim();
                Item.NDOM = int.Parse(Reader["NDOM"].ToString());
                Item.LDOM = Reader["LDOM"].ToString().Trim();
                Item.KORP = int.Parse(Reader["KORP"].ToString());
                Item.NKW = int.Parse(Reader["NKW"].ToString());
                Item.LKW = Reader["LKW"].ToString().Trim();
                Item.PVID = int.Parse(Reader["PVID"].ToString());
                Item.PSR = Reader["PSR"].ToString().Trim();
                Item.PNM = Reader["PNM"].ToString().Trim();
                Item.KSS = int.Parse(Reader["KSS"].ToString());
                Item.KOD = Reader["KOD"].ToString().Trim();
                Item.SROKS = Reader.GetDateTime(Reader.GetOrdinal("SROKS"));
                Item.SROKPO = Reader.GetDateTime(Reader.GetOrdinal("SROKPO"));
                Item.KDOMVL = int.Parse(Reader["KDOMVL"].ToString());
                Item.ROPL = double.Parse(Reader["ROPL"].ToString());
                Item.KCHLS = int.Parse(Reader["KCHLS"].ToString());
                Item.K_POL = int.Parse(Reader["K_POL"].ToString());
                Item.KKOM = int.Parse(Reader["KKOM"].ToString());
                Item.DATE_VIGR = Reader.GetDateTime(Reader.GetOrdinal("DATE_VIGR"));
                Item.PRIM = Reader["PRIM"].ToString().Trim();

                for (int i = 1; i < 10; i++)
                {
                    RECPPREDDATA PREDDATA = new RECPPREDDATA();
                    PREDDATA.PRED = int.Parse(Reader[string.Format("PRED{0}", i)].ToString());
                    PREDDATA.VID = Reader[string.Format("VID{0}", i)].ToString().Trim();
                    PREDDATA.LSH = Reader[string.Format("LSH{0}", i)].ToString().Trim();
                    PREDDATA.VOL = double.Parse(Reader[string.Format("VOL{0}", i)].ToString());
                    PREDDATA.TARIF = double.Parse(Reader[string.Format("TARIF{0}", i)].ToString());
                    PREDDATA.SUMLN = double.Parse(Reader[string.Format("SUMLN{0}", i)].ToString());
                    PREDDATA.SUMLD = double.Parse(Reader[string.Format("SUMLD{0}", i)].ToString());
                    PREDDATA.SUMLF = double.Parse(Reader[string.Format("SUMLF{0}", i)].ToString());
                    PREDDATA.KOD_T = int.Parse(Reader[string.Format("KOD_T{0}", i)].ToString());
                    PREDDATA.KOD_N = int.Parse(Reader[string.Format("KOD_N{0}", i)].ToString());
                    PREDDATA.S_ = int.Parse(Reader[string.Format("S_{0}", i)].ToString());
                    Item.PREDDATA[i - 1] = PREDDATA;
                }

                foreach (int PRED in Item.PREDList())
                {
                    Item.ExecuteCommand(Path.GetDirectoryName(FileName), PRED);
                    MainGrid.Invoke(new MethodInvoker(delegate { MainGrid.Rows[0].Cells[4].Value = string.Format("Processing {0}", Cursor); }));
                }
            }

            MainGrid.Invoke(new MethodInvoker(delegate { MainGrid.Rows[0].Cells[4].Value = "Finished!"; }));
            Reader.Close();
            Command.Connection.Close();
        }

        private void ProcessFiles(List<string> FileNames)
        {
            foreach (string FileName in FileNames)
            {
                List<OutputFile> OutputFileList = GetOutputFileList(FileName);
                string Directory = Path.GetDirectoryName(FileName);
                string RegionCode = GetRegionCode(FileName);
                string ReportDate = GetReportDate(FileName);

                foreach (OutputFile OutputFile in OutputFileList)
                {
                    OutputFile.Prepare(Directory, ReportDate, RegionCode);
                }

                ProcessFile(FileName);
            }
        }

        private string GetRegionCode(string FileName)
        {
            string RegionCode = string.Empty;
            OleDbCommand Command = FoxPro.OleDbCommand(string.Format("SELECT PY FROM '{0}'", FileName));
            OleDbDataReader Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                RegionCode = Reader["PY"].ToString().Trim();
                break;
            }
            return RegionCode;
        }

        string GetReportDate(string InputFileName)
        {
            DateTime ReportDate = new DateTime();
            try
            {
                OleDbCommand Command = FoxPro.OleDbCommand(string.Format("SELECT DATE_VIGR FROM '{0}'", InputFileName));
                OleDbDataReader Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    ReportDate = Reader.GetDateTime(Reader.GetOrdinal("DATE_VIGR"));
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