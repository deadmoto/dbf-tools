using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using CheckDBF.Core;
using Word = Microsoft.Office.Interop.Word;

namespace CheckDBF
{
    public partial class MainForm : Form
    {
        private event EventHandler ProcessBarEventHandler;
        private Thread NotifyIconThread;
        private Thread ProcessFilesThread;

        public MainForm()
        {
            InitializeComponent();
            if (Environment.GetCommandLineArgs().Length > 1)
            {
                OpenSupplierFile(Environment.GetCommandLineArgs()[1]);
                OpenPaymentFile(Environment.GetCommandLineArgs()[1]);
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
                MonthTextBox.Text = Core.FileInfo.GetPaymentDate(FileName);
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

            NotifyIconThread = new Thread(new ThreadStart(delegate { NotifyIconAnimate(); }));
            NotifyIconThread.Start();

            ProcessFilesThread = new Thread(new ThreadStart(delegate { ProcessFiles(); }));
            ProcessFilesThread.Start();
        }

        private void NotifyIconAnimate()
        {
            List<System.Drawing.Icon> IconList = new List<System.Drawing.Icon>();
            IconList.Add(Properties.Icons.Rotate1);
            IconList.Add(Properties.Icons.Rotate2);
            IconList.Add(Properties.Icons.Rotate3);
            IconList.Add(Properties.Icons.Rotate4);
            IconList.Add(Properties.Icons.Rotate5);
            IconList.Add(Properties.Icons.Rotate6);
            IconList.Add(Properties.Icons.Rotate7);
            IconList.Add(Properties.Icons.Rotate8);

            int IconIndex = 0;

            NotifyIcon.Visible = true;

            while (ProcessFilesThread.IsAlive)
            {
                NotifyIcon.Icon = IconList[IconIndex];
                IconIndex++;
                if (IconIndex == IconList.Count) { IconIndex = 0; }
                Thread.Sleep(100);
            }

            NotifyIcon.Visible = false;
        }

        private void ProcessFiles()
        {
            string SupplierFileName = SupplierFileTextBox.Text;
            string PaymentFileName = PaymentFileTextBox.Text;

            Dictionary<string, int> Changes = new Dictionary<string, int>();
            int ChangesTotal = 0;

            Changes.Add("RECORDS", Core.FileInfo.GetRecordCount(SupplierFileName));

            foreach (string Key in Template.Items.Keys)
            {
                try
                {
                    OdbcCommand Command = OdbcDriver.VFPCommand(string.Format(Template.Items[Key], SupplierFileName, PaymentFileName), "");
                    Changes.Add(Key, int.Parse(Command.ExecuteScalar().ToString()));
                    ChangesTotal += Changes[Key];
                }
                catch (Exception E)
                {
                    Changes.Add(Key, 0);
                    Log.Errors.Add(E.Message);
                }
            }

            Changes.Add("NPSS", Check.CheckNPSS(SupplierFileName, PaymentFileName, ProcessBarEventHandler));
            Changes.Add("CHANGES", ChangesTotal + Changes["NPSS"]);

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

            Changes.Add("ERRORS", Changes["LSH"] + Changes["KDOMVL"] + Changes["ROPL"] + Changes["KCHLS"] + Changes["S_"] + Changes["KOD_T"] + Changes["SUMLN"] + Changes["VOL"] + Changes["TARIF"] + Changes["K_POL"] + Changes["KKOM"]);

            ProcessBar.Invoke(new MethodInvoker(delegate { ProcessBar.Visible = false; }));
            ProcessTextBox.Invoke(new MethodInvoker(delegate { ProcessTextBox.Visible = false; }));

            SaveReport(SupplierFileName, Changes);

            ProcessButton.Invoke(new MethodInvoker(delegate { ProcessButton.Enabled = true; }));
        }

        private void SaveReport(string FileName, Dictionary<string, int> Changes)
        {
            string Template = Path.GetDirectoryName(Application.ExecutablePath) + "\\Data\\Report.doc";
            string Report = string.Format("{0}\\{1}-{2}.doc", Path.GetDirectoryName(FileName), CodeTextBox.Text, MonthTextBox.Text);
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
            }
            catch (Exception E)
            {
                Log.Errors.Add(E.Message);
                WordApplication.Visible = true;
                return;
            }

            switch (MessageBox.Show("Просмотреть протокол перед печатью?", "", MessageBoxButtons.YesNoCancel))
            {
                case System.Windows.Forms.DialogResult.Yes:
                    WordApplication.Visible = true;
                    break;
                case System.Windows.Forms.DialogResult.No:
                    try { WordApplication.PrintOut(); }
                    finally { Document.Close(); }
                    break;
                case System.Windows.Forms.DialogResult.Cancel:
                    Document.Close();
                    break;
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

        private void ExitMenuItemClick(object sender, EventArgs e)
        {
            Close();
        }
    }
}