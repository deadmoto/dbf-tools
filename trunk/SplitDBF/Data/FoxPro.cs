using System.Windows.Forms;

namespace System.Data.OleDb
{
    static class FoxPro
    {
        public static OleDbCommand OleDbCommand(string CommandText = "", string DataSource = "")
        {
            if (DataSource == "")
            {
                DataSource = Application.StartupPath;
            }

            OleDbConnectionStringBuilder ConnectionStringBuilder = new OleDbConnectionStringBuilder();
            ConnectionStringBuilder.Provider = "vfpoledb";
            ConnectionStringBuilder.Add("Collating Sequence", "Machine");
            ConnectionStringBuilder.Add("Data Source", DataSource);
            OleDbConnection Connection = new OleDbConnection(ConnectionStringBuilder.ConnectionString);
            Connection.Open();
            OleDbCommand Command = new OleDbCommand(CommandText, Connection);
            return Command;
        }
    }
}
