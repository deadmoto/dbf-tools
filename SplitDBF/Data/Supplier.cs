using System.Collections.Generic;
using System.Data.Odbc;
using System.Windows.Forms;


namespace SplitDBF
{
    /// <summary>
    /// Provides supplier Code-Name dictionary
    /// </summary>
    static class Supplier
    {
        /// <summary>
        /// Contains supplier Code-Name dictionary
        /// </summary>
        static Dictionary<string, string> SupplierDictionary = GetSupplierDictionary();

        /// <summary>
        /// Returns Code-Name collection from supplier.dbf
        /// </summary>
        private static Dictionary<string, string> GetSupplierDictionary()
        {
            Dictionary<string, string> SupplierName = new Dictionary<string, string>();
            OdbcCommand Command = OdbcDriver.VFPCommand(string.Format("SELECT * FROM '{0}'", Application.StartupPath + "\\Data\\Supplier.dbf"), "");
            OdbcDataReader Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                string Code = Reader.GetInt32(Reader.GetOrdinal("Code")).ToString();
                string Name = Reader.GetString(Reader.GetOrdinal("ShortName")).Trim();
                Name = Name.Replace('\\', ' ');
                Name = Name.Replace('/', ' ');
                Name = Name.Replace(':', ' ');
                Name = Name.Replace('*', ' ');
                Name = Name.Replace('?', ' ');
                Name = Name.Replace('"', ' ');
                Name = Name.Replace('<', ' ');
                Name = Name.Replace('>', ' ');
                Name = Name.Replace('|', ' ');
                Name = Name.Replace("  ", " ");
                Name = Name.Trim();
                SupplierName.Add(Code, Name);
            }
            return SupplierName;
        }

        /// <summary>
        /// Returns supplier Name by its Code
        /// </summary>
        public static string GetSupplierName(string Code)
        {
            if (SupplierDictionary.ContainsKey(Code))
            {
                return SupplierDictionary[Code];
            }
            else
            {
                return Code;
            }
        }
    }
}