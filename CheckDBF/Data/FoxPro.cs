using System.Windows.Forms;
using System;

namespace System.Data.OleDb
{
    /// <summary>
    /// Provides OleDbCommand for Visual FoxPro DBF files
    /// </summary>
    static class FoxPro
    {
        /// <summary>
        /// Returns OleDbCommand
        /// </summary>
        public static OleDbCommand OleDbCommand(string CommandText = "")
        {
            OleDbConnectionStringBuilder ConnectionStringBuilder = new OleDbConnectionStringBuilder();
            ConnectionStringBuilder.Provider = "vfpoledb";
            ConnectionStringBuilder.Add("Collating Sequence", "Russian");
            ConnectionStringBuilder.Add("Data Source", Environment.CurrentDirectory);
            OleDbConnection Connection = new OleDbConnection(ConnectionStringBuilder.ConnectionString);
            Connection.Open();
            OleDbCommand Command = new OleDbCommand(CommandText, Connection);
            return Command;
        }
    }
}
