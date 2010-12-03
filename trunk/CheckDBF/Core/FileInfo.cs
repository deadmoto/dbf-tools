using System;
using System.Data.OleDb;

namespace CheckDBF.Core
{
    static class FileInfo
    {
        public static int GetRecordCount(string FileName)
        {
            int Result = 0;
            try
            {
                OleDbCommand Command = FoxPro.OleDbCommand(string.Format("SELECT RECCOUNT() FROM '{0}'", FileName));
                return int.Parse(Command.ExecuteScalar().ToString());
            }
            catch (Exception E)
            {
                Log.Errors.Add(E.Message);
            }
            return Result;
        }

        public static string GetPaymentDate(string FileName)
        {
            DateTime Month = DateTime.Now;
            try
            {
                OleDbCommand Command = FoxPro.OleDbCommand(string.Format("SELECT DATE_VIGR FROM '{0}'", FileName));
                OleDbDataReader Reader = Command.ExecuteReader();
                if (Reader.Read())
                {
                    Month = Reader.GetDateTime(Reader.GetOrdinal("DATE_VIGR"));
                }
                return Month.ToString("MMMM");
            }
            catch (Exception E)
            {
                Log.Errors.Add(E.Message);
                return Month.ToString("MMMM");
            }
        }

        public static int GetSupplierCode(string FileName)
        {
            int Result = 0;
            for (int i = 9; i > 0; i--)
            {
                try
                {
                    OleDbCommand Command = FoxPro.OleDbCommand(string.Format("SELECT PRED{1} FROM '{0}' WHERE PRED{1} > 0 GROUP BY PRED{1}", FileName, i));
                    OleDbDataReader Reader = Command.ExecuteReader();
                    if (Reader.Read())
                    {
                        Result = int.Parse(Reader[string.Format("PRED{0}", i)].ToString().Trim());
                    }
                }
                catch (Exception E)
                {
                    Log.Errors.Add(E.Message);
                }
            }
            return Result;
        }
    }
}