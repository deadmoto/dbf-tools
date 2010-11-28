using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data.OleDb;
using System.Windows.Forms;

namespace SplitDBF.Data
{
    class OutputFile
    {
        public string PRED;
        public string NAME;

        OutputFile(string PRED)
        {
            this.PRED = PRED;
            NAME = Supplier.GetSupplierName(PRED);
        }

        public static implicit operator OutputFile(string PRED)
        {
            return new OutputFile(PRED);
        }

        public void Prepare(string InputDirectory, string ReportDate, string RegionCode)
        {
            string OutputDirectory = InputDirectory + '\\' + NAME;
            string OutputFileName = OutputDirectory + "\\payment-" + ReportDate + ".dbf";
            Directory.CreateDirectory(OutputDirectory);

            if (File.Exists(OutputFileName))
            {
                string CommandText = string.Format("DELETE FROM '{0}' WHERE PY = '{1}'", OutputFileName, RegionCode);
                OleDbCommand Command = FoxPro.OleDbCommand(CommandText);
                Command.ExecuteNonQuery();
                Command.Connection.Close();

                CommandText = string.Format("PACK '{0}'", OutputFileName);
                Command = FoxPro.OleDbCommand(CommandText);
                Command.ExecuteNonQuery();
                Command.Connection.Close();
            }
            else
            {
                File.Copy(Application.StartupPath + "\\Data\\Payment.dbf", OutputFileName, true);
            }
        }
    }
}
