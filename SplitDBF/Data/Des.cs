using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;

namespace SplitDBF
{
    class DESPREDDATA
    {
        public int PRED;
        public string VID;
        public string LSH;
        public double SUMM_DES;
        public double SUMM_PER;

        public string CommandText(int PRED)
        {
            if (this.PRED == PRED)
            {
                return string.Format("{0},'{1}','{2}',{3},{4}", PRED, VID, LSH, SUMM_DES.ToString().Replace(',', '.'), SUMM_PER.ToString().Replace(',', '.'));
            }
            else
            {
                return "0,'','',0,0";
            }
        }
    }

    class DES
    {
        public string PCOUID;
        public string FAMIL;
        public string IMJA;
        public string OTCH;
        public DateTime DROG;
        public string NPSS;
        public string PY;
        public string NNASP;
        public string NYLIC;
        public int NDOM;
        public string LDOM;
        public int KORP;
        public int NKW;
        public string LKW;
        public DESPREDDATA[] PREDDATA = new DESPREDDATA[9];

        public List<int> PREDList()
        {
            List<int> PREDList = new List<int>();
            for (int i = 0; i < 9; i++)
            {
                if (PREDDATA[i].PRED > 0 && PREDList.Contains(PREDDATA[i].PRED) == false)
                {
                    PREDList.Add(PREDDATA[i].PRED);
                }
            }
            return PREDList;
        }

        public string CommandText(string FileName, int PRED)
        {
            string CommandText = "INSERT INTO '" + FileName + "' VALUES ";
            CommandText += "('" + PCOUID;
            CommandText += "','" + FAMIL;
            CommandText += "','" + IMJA;
            CommandText += "','" + OTCH;
            CommandText += "'," + DROG.ToString("{^yyyy/MM/dd}");
            CommandText += ",'" + NPSS;
            CommandText += "','" + PY;
            CommandText += "','" + NNASP;
            CommandText += "','" + NYLIC;
            CommandText += "'," + NDOM;
            CommandText += ",'" + LDOM;
            CommandText += "'," + KORP;
            CommandText += "," + NKW;
            CommandText += ",'" + LKW + "'";

            for (int i = 0; i < 9; i++)
            {
                CommandText += "," + PREDDATA[i].CommandText(PRED);
            }

            CommandText += ")";
            CommandText = CommandText.Replace(",{^1899.12.30},", ",CTOT(''),");
            return CommandText;
        }

        public void ExecuteCommand(string InputDirectory, string ReportDate, int PRED)
        {
            string OutputDirectory = string.Format("{0}\\{1}", InputDirectory, Supplier.GetSupplierName(PRED.ToString()));
            string FileName = string.Format("{0}\\des-{1}.dbf", OutputDirectory, ReportDate);
            OleDbCommand Command = FoxPro.OleDbCommand(CommandText(FileName, PRED));
            Command.ExecuteNonQuery();
            Command.Connection.Close();
        }
    }
}
