using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;

namespace SplitDBF
{
    class RECPPREDDATA
    {
        public int PRED;
        public string VID;
        public string LSH;
        public double VOL;
        public double TARIF;
        public double SUMLN;
        public double SUMLD;
        public double SUMLF;
        public int KOD_T;
        public int KOD_N;
        public int S_;

        public string CommandText(int PRED)
        {
            if (this.PRED == PRED)
            {
                return string.Format("{0},'{1}','{2}',{3},{4},{5},{6},{7},{8},{9},{10}", PRED, VID, LSH, VOL, TARIF, SUMLN, SUMLD, SUMLF, KOD_T, KOD_N, S_);
            }
            else
            {
                return "0,'','',0,0,0,0,0,0,0,0";
            }
        }
    }

    class RECP
    {
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
        public int PVID;
        public string PSR;
        public string PNM;
        public int KSS;
        public string KOD;
        public DateTime SROKS;
        public DateTime SROKPO;
        public int KDOMVL;
        public double ROPL;
        public int KCHLS;
        public int K_POL;
        public int KKOM;
        public DateTime DATE_VIGR;
        public string PRIM;
        public RECPPREDDATA[] PREDDATA = new RECPPREDDATA[9];

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
            CommandText += "('" + FAMIL;
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
            CommandText += ",'" + LKW;
            CommandText += "'," + PVID;
            CommandText += ",'" + PSR;
            CommandText += "','" + PNM;
            CommandText += "'," + KSS;
            CommandText += ",'" + KOD;
            CommandText += "'," + SROKS.ToString("{^yyyy/MM/dd}");
            CommandText += "," + SROKPO.ToString("{^yyyy/MM/dd}");
            CommandText += "," + KDOMVL;
            CommandText += "," + ROPL;
            CommandText += "," + KCHLS;
            CommandText += "," + K_POL;
            CommandText += "," + KKOM;

            for (int i = 0; i < 9; i++)
            {
                CommandText += "," + PREDDATA[i].CommandText(PRED);
            }

            CommandText += "," + DATE_VIGR.ToString("{^yyyy/MM/dd}");
            CommandText += ",'" + PRIM + "')";
            CommandText = CommandText.Replace(",0,", ",NULL,");
            CommandText = CommandText.Replace(",0,", ",NULL,");
            CommandText = CommandText.Replace(",{^1899.12.30},", ",NULL,");
            return CommandText;
        }

        public void ExecuteCommand(string InputDirectory, int PRED)
        {
            string OutputDirectory = string.Format("{0}\\{1}", InputDirectory, Supplier.GetSupplierName(PRED.ToString()));
            string FileName = string.Format("{0}\\payment-{1}.dbf", OutputDirectory, DATE_VIGR.ToString("yyyy-MM-dd"));
            OleDbCommand Command = FoxPro.OleDbCommand(CommandText(FileName, PRED));
            Command.ExecuteNonQuery();
            Command.Connection.Close();
        }
    }
}
