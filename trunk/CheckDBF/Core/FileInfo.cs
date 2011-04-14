using System;
using System.Data.OleDb;

namespace CheckDBF.Core
{
    static class FileInfo
    {
        public static int GetRecordCount(string FileName)
        {
            OleDbCommand Command = FoxPro.OleDbCommand(string.Format("SELECT RECCOUNT() FROM '{0}'", FileName));
            int Result = 0;

            try
            {
                Result = int.Parse(Command.ExecuteScalar().ToString());
                Command.Connection.Close();
            }
            catch (Exception E)
            {
                Log.Errors.Add(E.Message);
            }

            Command.Connection.Close();
            return Result;
        }

        public static string GetPaymentDate(string FileName)
        {
            OleDbCommand Command = FoxPro.OleDbCommand(string.Format("SELECT DATE_VIGR FROM '{0}'", FileName));
            DateTime Month = DateTime.Now;

            try
            {
                OleDbDataReader Reader = Command.ExecuteReader();
                if (Reader.Read())
                {
                    Month = Reader.GetDateTime(Reader.GetOrdinal("DATE_VIGR"));
                }
            }
            catch (Exception E)
            {
                Log.Errors.Add(E.Message);
            }

            Command.Connection.Close();
            return Month.ToString("MMMM");
        }

        public static void SetPaymentDate(string FileName, DateTime Date)
        {
            OleDbCommand Command = FoxPro.OleDbCommand(string.Format("UPDATE '{0}' SET DATE_VIGR = {1}", FileName, Date.ToString("{^yyyy/MM/dd}")));

            try
            {
                Command.ExecuteNonQuery();
            }
            catch (Exception E)
            {
                Log.Errors.Add(E.Message);
            }

            Command.Connection.Close();
        }

        public static int GetSupplierCode(string FileName)
        {
            int Result = 0;

            for (int i = 9; i > 0; i--)
            {
                OleDbCommand Command = FoxPro.OleDbCommand(string.Format("SELECT PRED{1} FROM '{0}' WHERE PRED{1} > 0 GROUP BY PRED{1}", FileName, i));

                try
                {
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

                Command.Connection.Close();
            }

            return Result;
        }
    }
}