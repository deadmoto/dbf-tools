using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;

namespace CheckDBF
{
    class ReplaceData
    {
        public int PREDK;
        public int PREDU;
        public string VID;
    }

    static class Replace
    {
        private static List<ReplaceData> ReplaceList = GetReplaceList();

        private static List<ReplaceData> GetReplaceList()
        {
            List<ReplaceData> Items = new List<ReplaceData>();
            string FileName = Application.StartupPath + "\\Data\\Replace.dbf";

            try
            {
                OleDbCommand Command = FoxPro.OleDbCommand(string.Format("SELECT * FROM '{0}' ORDER BY PREDK, VID", FileName));
                OleDbDataReader Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    ReplaceData Item = new ReplaceData();
                    Item.PREDK = int.Parse(Reader["PREDK"].ToString());
                    Item.PREDU = int.Parse(Reader["PREDU"].ToString());
                    Item.VID = Reader["VID"].ToString();
                    Items.Add(Item);
                }
                Reader.Close();
                Command.Connection.Close();
            }
            catch
            {
                MessageBox.Show("Не удалось открыть файл " + FileName);
            }

            return Items;
        }

        public static List<ReplaceData> GetReplaceList(string Pattern)
        {
            return ReplaceList.FindAll(delegate(ReplaceData Item) { return Item.PREDK.ToString().Contains(Pattern); });
        }

        public static int ReplacePREDK(int PREDK, string VID)
        {
            ReplaceData Result = ReplaceList.Find(delegate(ReplaceData Item) { return Item.PREDK == PREDK & Item.VID == VID; });

            if (Result != null)
            {
                return Result.PREDU;
            }

            return PREDK;
        }
    }
}
