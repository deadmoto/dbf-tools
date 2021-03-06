﻿using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;

namespace CheckDBF
{
    class ConformData
    {
        public int PRED;
        public string VID;
        public int KOD_T;
        public int KOD_N;
        public double TARIF;
        public double VOL;
    }

    static class Conform
    {
        private static List<ConformData> ConformList = GetConformList();

        private static List<ConformData> GetConformList()
        {
            List<ConformData> List = new List<ConformData>();

            OleDbCommand Command = FoxPro.OleDbCommand(string.Format("SELECT * FROM '{0}' ORDER BY PRED, VID, KOD_T, KOD_N", Application.StartupPath + "\\Data\\Conform.dbf"));
            OleDbDataReader Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                ConformData ConformData = new ConformData();
                ConformData.PRED = int.Parse(Reader["PRED"].ToString());
                ConformData.VID = Reader["VID"].ToString().Trim();
                ConformData.KOD_T = int.Parse(Reader["KOD_T"].ToString());
                ConformData.KOD_N = int.Parse(Reader["KOD_N"].ToString());
                ConformData.TARIF = double.Parse(Reader["TARIF"].ToString());
                ConformData.VOL = double.Parse(Reader["VOL"].ToString());
                List.Add(ConformData);
            }

            return List;
        }

        public static List<ConformData> GetConformList(string PRED, string VID, string KOD_T, string KOD_N)
        {
            return ConformList.FindAll(delegate(ConformData Item) { return Item.PRED.ToString().Contains(PRED) && Item.VID.Contains(VID) && Item.KOD_T.ToString().Contains(KOD_T) && Item.KOD_N.ToString().Contains(KOD_N); });
        }

        public static ConformData GetConformData(int PRED, string VID, int KOD_T, int KOD_N)
        {
            ConformData Result = ConformList.Find(delegate(ConformData Item) { return Item.PRED == PRED && Item.VID == VID && Item.KOD_T == KOD_T && Item.KOD_N == KOD_N; });

            if (Result == null)
            {
                PRED = Replace.ReplacePREDK(PRED, VID);
                Result = ConformList.Find(delegate(ConformData Item) { return Item.PRED == PRED && Item.VID == VID && Item.KOD_T == KOD_T && Item.KOD_N == KOD_N; });
            }

            //if (Result != null && PRED == 1621 && VID == "0300")
            //{
            //    Result.TARIF = System.Math.Round(Result.TARIF * Result.VOL, 2);
            //    Result.VOL = 1;
            //}

            return Result;
        }
    }
}
