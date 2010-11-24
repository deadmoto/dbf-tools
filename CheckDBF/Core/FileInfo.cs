﻿using System;
using System.Data.Odbc;

namespace CheckDBF.Core
{
    static class FileInfo
    {
        public static int GetRecordCount(string FileName)
        {
            int Result = 0;
            try
            {
                OdbcCommand Command = OdbcDriver.VFPCommand(string.Format("SELECT RECCOUNT() FROM '{0}'", FileName), "");
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
                OdbcCommand Command = OdbcDriver.VFPCommand(string.Format("SELECT DATE_VIGR FROM '{0}'", FileName), "");
                OdbcDataReader Reader = Command.ExecuteReader();
                if (Reader.Read())
                {
                    Month = Reader.GetDate(Reader.GetOrdinal("DATE_VIGR"));
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
                    OdbcCommand Command = OdbcDriver.VFPCommand(string.Format("SELECT PRED{1} FROM '{0}' WHERE NOT EMPTY(PRED{1}) GROUP BY PRED{1}", FileName, i), "");
                    OdbcDataReader Reader = Command.ExecuteReader();
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