﻿using System.Collections.Generic;
using System.Data.Odbc;
using System.Windows.Forms;

namespace CheckDBF
{
    class ConformData
    {
        public int PRED;
        public string VID;
        public int KOD_T;
        public int KOD_N;
        public float TARIF;
        public float VOL;
    }

    static class Conform
    {
        private static List<ConformData> ConformList = GetConformList();

        private static List<ConformData> GetConformList()
        {
            List<ConformData> List = new List<ConformData>();

            OdbcCommand Command = OdbcDriver.VFPCommand(string.Format("SELECT * FROM '{0}'", Application.StartupPath + "\\Data\\Conform.dbf"));
            OdbcDataReader Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                ConformData ConformData = new ConformData();
                ConformData.PRED = int.Parse(Reader["PRED"].ToString());
                ConformData.VID = Reader["VID"].ToString().Trim();
                ConformData.KOD_T = int.Parse(Reader["KOD_T"].ToString());
                ConformData.KOD_N = int.Parse(Reader["KOD_N"].ToString());
                ConformData.TARIF = float.Parse(Reader["TARIF"].ToString());
                ConformData.VOL = float.Parse(Reader["VOL"].ToString());
                List.Add(ConformData);
            }

            return List;
        }

        public static ConformData GetConformData(int PRED, string VID, int KOD_T, int KOD_N)
        {
            ConformData Result = ConformList.Find(delegate(ConformData Item) { return Item.PRED == PRED && Item.VID == VID && Item.KOD_T == KOD_T && Item.KOD_N == KOD_N; });
            if (Result == null)
            {
                PRED = Replace.ReplacePREDK(PRED, VID);
                Result = ConformList.Find(delegate(ConformData Item) { return Item.PRED == PRED && Item.VID == VID && Item.KOD_T == KOD_T && Item.KOD_N == KOD_N; });
            }
            //if (Result.PRED == 1621 && Result.VID == "0300")
            //{
            //    Result.TARIF = Result.TARIF * Result.VOL;
            //    Result.VOL = 1;
            //}
            return Result;
        }
    }
}
