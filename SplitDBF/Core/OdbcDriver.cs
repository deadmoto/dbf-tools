
using System;
using System.Data.Odbc;
using System.IO;

namespace SplitDBF
{
    /// <summary>
    /// Provides ODBC connections
    /// </summary>
    static class OdbcDriver
    {
        /// <summary>
        /// Provides Visual FoxPro connection
        /// </summary>
        public static OdbcCommand VFPCommand(string CommandText = "", string SourceDB = "")
        {
            if (Directory.Exists(SourceDB))
            {
                Environment.CurrentDirectory = SourceDB;
            }

            OdbcConnectionStringBuilder ConnectionStringBuilder = new OdbcConnectionStringBuilder();
            ConnectionStringBuilder.Driver = "Microsoft Visual FoxPro Driver";
            ConnectionStringBuilder.Add("BackgroundFetch", "No");
            ConnectionStringBuilder.Add("Collate", "Machine");
            ConnectionStringBuilder.Add("Deleted", "Yes");
            ConnectionStringBuilder.Add("Exclusive", "No");
            ConnectionStringBuilder.Add("Null", "Yes");
            ConnectionStringBuilder.Add("SourceDB", Environment.CurrentDirectory);
            ConnectionStringBuilder.Add("SourceType", "DBF");
            OdbcConnection Connection = new OdbcConnection(ConnectionStringBuilder.ConnectionString);
            Connection.Open();
            OdbcCommand Command = new OdbcCommand(CommandText, Connection);
            return Command;
        }

        /// <summary>
        /// Provides Microsoft Excel connection
        /// </summary>
        public static OdbcCommand XLSCommand(string SourceDB, string SQLText)
        {
            OdbcConnectionStringBuilder ConnectionStringBuilder = new OdbcConnectionStringBuilder();
            ConnectionStringBuilder.Driver = "Microsoft Excel Driver (*.xls)";
            ConnectionStringBuilder.Add("DriverId", "790");
            ConnectionStringBuilder.Add("ReadOnly", "False");
            ConnectionStringBuilder.Add("Dbq", Path.GetFileName(SourceDB));
            ConnectionStringBuilder.Add("DefaultDir", Path.GetDirectoryName(SourceDB));
            OdbcConnection Connection = new OdbcConnection(ConnectionStringBuilder.ConnectionString);
            Connection.Open();
            OdbcCommand Command = new OdbcCommand(SQLText, Connection);
            return Command;
        }
    }
}