using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using CheckDBF.Core;
using Word = Microsoft.Office.Interop.Word;
using System.Net;

namespace CheckDBF.Forms
{
    public partial class MainForm : Form
    {
        private event EventHandler ProcessBarEventHandler;
        private Thread NotifyIconThread;
        private Thread ProcessFilesThread;

        public MainForm()
        {
            InitializeComponent();
            Text = "CheckDBF выпуск " + Version.Value;
            if (Environment.GetCommandLineArgs().Length > 1)
            {
                OpenSupplierFile(Environment.GetCommandLineArgs()[1]);
            }
            CheckVPNAddress();
        }

        private void CheckVPNAddress()
        {
            Check.IsVPNAddress = false;
            IPAddress[] IPAddresses = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress IP in IPAddresses)
            {
                Check.IsVPNAddress = Check.IsVPNAddress || IP.ToString().StartsWith("10.");
            }
        }

        private void OpenSupplierFileClick(object sender, EventArgs e)
        {
            OpenFileDialog OpenFile = new OpenFileDialog();
            OpenFile.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath);
            OpenFile.Filter = "DBF Files (*.dbf) | *.dbf";
            if (OpenFile.ShowDialog() == DialogResult.OK)
            {
                OpenSupplierFile(OpenFile.FileName);
            }
        }

        private void OpenPaymentFileClick(object sender, EventArgs e)
        {
            OpenFileDialog OpenFile = new OpenFileDialog();
            OpenFile.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath);
            OpenFile.Filter = "DBF Files (*.dbf) | *.dbf";
            if (OpenFile.ShowDialog() == DialogResult.OK)
            {
                OpenPaymentFile(OpenFile.FileName);
            }
        }

        private void OpenSupplierFile(string FileName)
        {
            if (FileName.Contains("!"))
            {
                MessageBox.Show("Путь к файлу не должен содержать \"!\"");
            }
            else
            {
                SupplierFileTextBox.Text = FileName;
                ProcessBar.Maximum = Core.FileInfo.GetRecordCount(FileName);
                RecordsTextBox.Text = Core.FileInfo.GetRecordCount(FileName).ToString();
                int SupplierCode = Core.FileInfo.GetSupplierCode(FileName);
                CodeTextBox.Text = SupplierCode.ToString();
                NameTextBox.Text = Supplier.GetSupplierName(SupplierCode.ToString());
                MonthTextBox.Text = Core.FileInfo.GetPaymentDate(FileName);
            }
        }

        private void OpenPaymentFile(string FileName)
        {
            if (FileName.Contains("!"))
            {
                MessageBox.Show("Путь к файлу не должен содержать \"!\"");
            }
            else
            {
                PaymentFileTextBox.Text = FileName;
            }
        }

        private void ProcessButtonClick(object sender, EventArgs e)
        {
            ProcessBarEventHandler = ProcessBarEvent;

            ProcessButton.Enabled = false;

            ProcessTextBox.Text = string.Empty;
            ProcessTextBox.Visible = true;

            ProcessBar.Value = ProcessBar.Minimum;
            ProcessBar.Visible = true;

            Check.CheckLSEnabled = CheckLSMenuItem.Checked;
            Check.CheckKDOMVLEnabled = CheckKDOMVLMenuItem.Checked;
            Check.CheckROPLEnabled = CheckROPLMenuItem.Checked;
            Check.ShowMessages = ShowMessagesMenuItem.Checked;
            Check.SupplierCode = CodeTextBox.Text;


            NotifyIconThread = new Thread(new ThreadStart(delegate { NotifyIconAnimate(); }));
            NotifyIconThread.Start();

            ProcessFilesThread = new Thread(new ThreadStart(delegate { ProcessFiles(); }));
            ProcessFilesThread.Start();
        }

        private void NotifyIconAnimate()
        {
            List<System.Drawing.Icon> IconList = new List<System.Drawing.Icon>();

            for (int i = 1; i < 9; i++)
            {
                string FileName = string.Format("{0}\\Icons\\Rotate{1}.ico", Application.StartupPath, i);
                IconList.Add(new System.Drawing.Icon(FileName));
            }

            int IconIndex = 0;

            NotifyIcon.Visible = true;

            while (ProcessFilesThread.IsAlive)
            {
                NotifyIcon.Icon = IconList[IconIndex];
                IconIndex++;
                if (IconIndex == IconList.Count) { IconIndex = 0; }
                Thread.Sleep(250);
            }

            NotifyIcon.Visible = false;
        }

        private void ProcessFiles()
        {
            string SupplierFileName = SupplierFileTextBox.Text;
            string PaymentFileName = PaymentFileTextBox.Text;

            Dictionary<string, int> Changes = new Dictionary<string, int>();

            Changes.Add("RECORDS", Core.FileInfo.GetRecordCount(SupplierFileName));

            Changes.Add("FAMIL", Check.CheckFAMIL(SupplierFileName, PaymentFileName, ProcessBarEventHandler));
            Changes.Add("IMJA", Check.CheckIMJA(SupplierFileName, PaymentFileName, ProcessBarEventHandler));
            Changes.Add("OTCH", Check.CheckOTCH(SupplierFileName, PaymentFileName, ProcessBarEventHandler));
            Changes.Add("DROG", Check.CheckDROG(SupplierFileName, PaymentFileName, ProcessBarEventHandler));
            Changes.Add("NPSS", Check.CheckNPSS(SupplierFileName, PaymentFileName, ProcessBarEventHandler));
            Changes.Add("CHANGES", Changes["FAMIL"] + Changes["IMJA"] + Changes["OTCH"] + Changes["DROG"] + Changes["NPSS"]);

            Check.GetPersonList(SupplierFileName, ProcessBarEventHandler);
            Changes.Add("LSH", Check.CheckLS(SupplierFileName, ProcessBarEventHandler));
            Changes.Add("KDOMVL", Check.CheckKDOMVL(SupplierFileName, ProcessBarEventHandler));
            Changes.Add("ROPL", Check.CheckROPL(SupplierFileName, ProcessBarEventHandler));
            Changes.Add("KCHLS", Check.CheckKCHLS(SupplierFileName, ProcessBarEventHandler));
            Changes.Add("S_", Check.CheckS_(SupplierFileName, ProcessBarEventHandler));
            Changes.Add("KOD_T", Check.CheckKOD_T(SupplierFileName, ProcessBarEventHandler));
            Changes.Add("SUMLN", Check.CheckSUMLN(ProcessBarEventHandler));
            Changes.Add("VOL", Check.CheckVOL(ProcessBarEventHandler));
            Changes.Add("TARIF", Check.CheckTARIF(ProcessBarEventHandler));
            Changes.Add("K_POL", Check.CheckK_POL(SupplierFileName, ProcessBarEventHandler));
            Changes.Add("KKOM", Check.CheckKKOM(ProcessBarEventHandler));
            Changes.Add("INVALID", Check.CheckINVALID(SupplierFileName, ProcessBarEventHandler));
            Changes.Add("ERRORS", Changes["LSH"] + Changes["KDOMVL"] + Changes["ROPL"] + Changes["KCHLS"] + Changes["S_"] + Changes["KOD_T"] + Changes["SUMLN"] + Changes["VOL"] + Changes["TARIF"] + Changes["K_POL"] + Changes["KKOM"]);

            ProcessBar.Invoke(new MethodInvoker(delegate { ProcessBar.Visible = false; }));
            ProcessTextBox.Invoke(new MethodInvoker(delegate { ProcessTextBox.Visible = false; }));

            SaveErrors(SupplierFileName);

            if (Check.IsVPNAddress)
            {
                if (MessageBox.Show("Сформировать протокол?", Text, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    SaveReport(SupplierFileName, Changes);
                }
            }

            ProcessButton.Invoke(new MethodInvoker(delegate { ProcessButton.Enabled = true; }));

            MessageBox.Show("Обработка завершена!", Text);
        }

        private void SaveErrors(string FileName)
        {
            string LogFile = string.Format("{0}\\{1}-{2}.csv", Path.GetDirectoryName(FileName), CodeTextBox.Text, MonthTextBox.Text);

            try
            {
                StreamWriter Writer = new StreamWriter(LogFile, false, Encoding.GetEncoding(1251));
                foreach (string Message in Log.Messages)
                {
                    Writer.WriteLine(Message);
                }
                Log.Messages.Clear();
                Writer.Close();
            }
            catch (Exception E)
            {
                Log.Errors.Add(E.Message);
            }
        }

        private void SaveReport(string FileName, Dictionary<string, int> Changes)
        {
            string Template = Path.GetDirectoryName(Application.ExecutablePath) + "\\Data\\Report.doc";
            string Report = string.Format("{0}\\{1}-{2}.doc", Path.GetDirectoryName(FileName), CodeTextBox.Text, MonthTextBox.Text);


            try
            {
                File.Copy(Template, Report, true);
            }
            catch (Exception E)
            {
                Log.Errors.Add(E.Message);
                MessageBox.Show(E.Message, Text);
                return;
            }

            Word._Application WordApplication = new Word.Application();
            Word._Document Document = new Word.Document();

            try
            {
                Document = WordApplication.Documents.Open(FileName: Report);
                Document.Activate();
                WordApplication.Selection.WholeStory();
                WordApplication.Selection.Find.Execute(FindText: "CODE", ReplaceWith: CodeTextBox.Text);
                WordApplication.Selection.WholeStory();
                WordApplication.Selection.Find.Execute(FindText: "NAME", ReplaceWith: NameTextBox.Text);
                WordApplication.Selection.WholeStory();
                WordApplication.Selection.Find.Execute(FindText: "MONTH", ReplaceWith: MonthTextBox.Text);
                WordApplication.Selection.WholeStory();
                WordApplication.Selection.Find.Execute(FindText: "YEAR", ReplaceWith: DateTime.Today.Year.ToString());

                foreach (string Key in Changes.Keys)
                {
                    WordApplication.Selection.WholeStory();
                    WordApplication.Selection.Find.Execute(FindText: Key, ReplaceWith: Changes[Key].ToString());
                }

                Document.Save();
                Document.Close();
            }
            catch (Exception E)
            {
                Log.Errors.Add(E.Message);
                WordApplication.Visible = true;
                return;
            }
        }

        private void ProcessBarEvent(object sender = null, EventArgs e = null)
        {
            if (sender == null || sender.ToString() == ProcessTextBox.Text)
            {
                ProcessBar.Invoke(new MethodInvoker(delegate { ProcessBar.Value++; }));
            }
            else
            {
                ProcessTextBox.Invoke(new MethodInvoker(delegate { ProcessTextBox.Text = sender.ToString(); }));
                ProcessBar.Invoke(new MethodInvoker(delegate { ProcessBar.Value = ProcessBar.Minimum; }));
            }
        }

        private void ShowLogMenuItemClick(object sender, EventArgs e)
        {
            LogTextBox.Text = string.Empty;
            foreach (string Error in Log.Errors)
            {
                LogTextBox.Text += Error + "\r\n";
            }
            Log.Errors.Clear();

            LogTextBox.Visible = ShowLogMenuItem.Checked;
            AutoSize = !LogTextBox.Visible;
        }

        private void ReplaceMenuItemClick(object sender, EventArgs e)
        {
            new ReplaceForm().ShowDialog();
        }

        private void ConformMenuItemClick(object sender, EventArgs e)
        {
            new ConformForm().ShowDialog();
        }

        private void ExitMenuItemClick(object sender, EventArgs e)
        {
            Close();
        }
    }
}