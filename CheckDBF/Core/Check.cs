using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;

namespace CheckDBF.Core
{
    static class Check
    {
        private static List<Person> PersonList = new List<Person>();
        private static List<string> FieldList = new List<string>();

        public static bool CheckLSEnabled = true;
        public static bool CheckKDOMVLEnabled = true;
        public static bool CheckROPLEnabled = true;

        private static bool Exist(string FieldName)
        {
            return FieldList.FindAll(delegate(string Item) { return Item.ToUpper() == FieldName.ToUpper(); }).Count > 0;
        }

        public static void GetPersonList(string SupplierFileName, EventHandler Handler)
        {
            string Output = Path.GetDirectoryName(SupplierFileName) + "\\" + Path.GetFileNameWithoutExtension(SupplierFileName) + "-valid.dbf";
            File.Copy(Application.StartupPath + "\\Data\\Payment.dbf", Output, true);

            PersonList.Clear();
            FieldList.Clear();

            string CommandText = string.Format("SELECT * FROM '{0}'", SupplierFileName);
            OleDbCommand Command = FoxPro.OleDbCommand(CommandText);
            OleDbDataReader Reader = Command.ExecuteReader();

            for (int i = 0; i < Reader.FieldCount; i++)
            {
                FieldList.Add(Reader.GetName(i));
            }

            while (Reader.Read())
            {
                Person Person = new Person();

                Person.FAMIL = Reader["FAMIL"].ToString().Trim();
                Person.IMJA = Reader["IMJA"].ToString().Trim();
                Person.OTCH = Reader["OTCH"].ToString().Trim();
                Person.DROG = DateTime.Parse(Reader["DROG"].ToString());
                Person.NPSS = Reader["NPSS"].ToString().Trim();
                Person.PY = Reader["PY"].ToString().Trim();
                Person.NNASP = Reader["NNASP"].ToString().Trim();
                Person.NYLIC = Reader["NYLIC"].ToString().Trim();
                Person.NDOM = int.Parse(Reader["NDOM"].ToString());
                Person.LDOM = Reader["LDOM"].ToString().Trim();
                Person.KORP = int.Parse(Reader["KORP"].ToString());
                Person.NKW = int.Parse(Reader["NKW"].ToString());
                Person.LKW = Reader["LKW"].ToString().Trim();
                Person.PVID = int.Parse(Reader["PVID"].ToString());
                Person.PSR = Reader["PSR"].ToString().Trim();
                Person.PNM = Reader["PNM"].ToString().Trim();
                Person.KSS = int.Parse(Reader["KSS"].ToString());
                Person.KOD = Reader["KOD"].ToString().Trim();
                Person.SROKS = Reader.GetDateTime(Reader.GetOrdinal("SROKS"));
                Person.SROKPO = Reader.GetDateTime(Reader.GetOrdinal("SROKPO"));
                Person.KDOMVL = int.Parse(Reader["KDOMVL"].ToString());
                Person.ROPL = float.Parse(Reader["ROPL"].ToString());
                Person.KCHLS = int.Parse(Reader["KCHLS"].ToString());
                if (Exist("K_POL")) { Person.K_POL = int.Parse(Reader["K_POL"].ToString()); }
                Person.KKOM = int.Parse(Reader["KKOM"].ToString());
                try { DateTime.TryParse(Reader["DATE_VIGR"].ToString(), out Person.DATE_VIGR); }
                catch { }
                Person.PRIM = Reader["PRIM"].ToString().Trim();

                if (Exist("KDOMVL")) { int.TryParse(Reader["KDOMVL"].ToString(), out Person.KDOMVL); }
                double.TryParse(Reader["ROPL"].ToString(), out Person.ROPL);
                int.TryParse(Reader["KCHLS"].ToString(), out Person.KCHLS);
                if (Exist("K_POL")) { int.TryParse(Reader["K_POL"].ToString(), out Person.K_POL); }

                for (int i = 1; i < 10; i++)
                {
                    Service Service = new Service();
                    try
                    {
                        int.TryParse(Reader[string.Format("PRED{0}", i)].ToString(), out Service.PRED);
                        Service.VID = Reader[string.Format("VID{0}", i)].ToString().Trim();
                        double.TryParse(Reader[string.Format("VOL{0}", i)].ToString(), out Service.VOL);
                        double.TryParse(Reader[string.Format("SUMLN{0}", i)].ToString(), out Service.SUMLN);

                        if (Service.FILLED())
                        {
                            if (Exist(string.Format("LSH{0}", i))) { Service.LSH = Reader[string.Format("LSH{0}", i)].ToString().Trim(); }
                            try { Service.K_POL = int.Parse(Reader[string.Format("K_POL{0}", i)].ToString()); }
                            catch { Service.K_POL = Person.K_POL; }
                            double.TryParse(Reader[string.Format("TARIF{0}", i)].ToString(), out Service.TARIF);
                            double.TryParse(Reader[string.Format("SUMLD{0}", i)].ToString(), out Service.SUMLD);
                            double.TryParse(Reader[string.Format("SUMLF{0}", i)].ToString(), out Service.SUMLF);
                            int.TryParse(Reader[string.Format("KOD_T{0}", i)].ToString(), out Service.KOD_T);
                            int.TryParse(Reader[string.Format("KOD_N{0}", i)].ToString(), out Service.KOD_N);
                            int.TryParse(Reader[string.Format("S_{0}", i)].ToString(), out Service.S_);
                        }
                        Service.GetConformData();
                        Person.Services[i - 1] = Service;
                    }
                    catch (Exception E)
                    {
                        Log.Errors.Add(E.Message);
                        Person.Services[i - 1] = Service;
                    }
                }

                if (Person.GetErrorEMPTY() == false)
                {
                    ProcessPreload(Person.GetCommandText(Output));
                    PersonList.Add(Person);
                }
            }
            Reader.Close();
            Command.Connection.Close();

            Delete(Output);
            Pack(Output);
        }

        private static void ProcessPreload(string CommandText)
        {
            OleDbCommand Command = FoxPro.OleDbCommand(CommandText);
            Command.ExecuteNonQuery();
            Command.Connection.Close();
        }

        private static int Delete(string FileName)
        {
            int Result = 0;
            string CommandText = string.Format("DELETE FROM '{0}' WHERE PRED1 = 0 AND PRED2 = 0 AND PRED3 = 0 AND PRED4 = 0 AND PRED5 = 0 AND PRED6 = 0 AND PRED7 = 0 AND PRED8 = 0 AND PRED9 = 0", FileName);
            OleDbCommand Command = FoxPro.OleDbCommand(CommandText);
            Result = Command.ExecuteNonQuery();
            Command.Connection.Close();
            return Result;
        }

        private static void Pack(string FileName)
        {
            string CommandText = string.Format("PACK '{0}'", FileName);
            OleDbCommand Command = FoxPro.OleDbCommand(CommandText);
            Command.ExecuteNonQuery();
            Command.Connection.Close();
        }

        public static int CheckFAMIL(string SupplierFileName, string PaymentFileName, EventHandler Handler)
        {
            int Result = 0;
            try
            {
                string ErrorString = "{0}; {1} {2} {3}; Запись отличается от исходного файла (фамилия {4})";
                string CommandText = string.Format("SELECT a.NPSS, a.FAMIL, a.IMJA, a.OTCH, b.FAMIL FROM '{0}' as a INNER JOIN '{1}' as b ON a.NPSS = b.NPSS WHERE NOT EMPTY(a.NPSS) AND NOT EMPTY(b.NPSS) AND UPPER(a.FAMIL) <> UPPER(b.FAMIL)", SupplierFileName, PaymentFileName);
                OleDbCommand Command = FoxPro.OleDbCommand(CommandText);
                OleDbDataReader Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    Log.Messages.Add(string.Format(ErrorString, Reader[0].ToString().Trim(), Reader[1].ToString().Trim(), Reader[2].ToString().Trim(), Reader[3].ToString().Trim(), Reader[4].ToString().Trim()));
                    Result++;
                    Handler("FAMIL", null);
                }
            }
            catch (Exception E)
            {
                Log.Errors.Add(E.Message);
            }
            return Result;
        }

        public static int CheckIMJA(string SupplierFileName, string PaymentFileName, EventHandler Handler)
        {
            int Result = 0;
            try
            {
                string ErrorString = "{0}; {1} {2} {3}; Запись отличается от исходного файла (имя {4})";
                string CommandText = string.Format("SELECT a.NPSS, a.FAMIL, a.IMJA, a.OTCH, b.IMJA FROM '{0}' as a INNER JOIN '{1}' as b ON a.NPSS = b.NPSS WHERE NOT EMPTY(a.NPSS) AND NOT EMPTY(b.NPSS) AND UPPER(a.IMJA) <> UPPER(b.IMJA)", SupplierFileName, PaymentFileName);
                OleDbCommand Command = FoxPro.OleDbCommand(CommandText);
                OleDbDataReader Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    Log.Messages.Add(string.Format(ErrorString, Reader[0].ToString().Trim(), Reader[1].ToString().Trim(), Reader[2].ToString().Trim(), Reader[3].ToString().Trim(), Reader[4].ToString().Trim()));
                    Result++;
                    Handler("IMJA", null);
                }
            }
            catch (Exception E)
            {
                Log.Errors.Add(E.Message);
            }
            return Result;
        }

        public static int CheckOTCH(string SupplierFileName, string PaymentFileName, EventHandler Handler)
        {
            int Result = 0;
            try
            {
                string ErrorString = "{0}; {1} {2} {3}; Запись отличается от исходного файла (отчество {4})";
                string CommandText = string.Format("SELECT a.NPSS, a.FAMIL, a.IMJA, a.OTCH, b.IMJA FROM '{0}' as a INNER JOIN '{1}' as b ON a.NPSS = b.NPSS WHERE NOT EMPTY(a.NPSS) AND NOT EMPTY(b.NPSS) AND UPPER(a.OTCH) <> UPPER(b.OTCH)", SupplierFileName, PaymentFileName);
                OleDbCommand Command = FoxPro.OleDbCommand(CommandText);
                OleDbDataReader Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    Log.Messages.Add(string.Format(ErrorString, Reader[0].ToString().Trim(), Reader[1].ToString().Trim(), Reader[2].ToString().Trim(), Reader[3].ToString().Trim(), Reader[4].ToString().Trim()));
                    Result++;
                    Handler("OTCH", null);
                }
            }
            catch (Exception E)
            {
                Log.Errors.Add(E.Message);
            }
            return Result;
        }

        public static int CheckDROG(string SupplierFileName, string PaymentFileName, EventHandler Handler)
        {
            int Result = 0;
            try
            {
                string ErrorString = "{0}; {1} {2} {3}; Запись отличается от исходного файла (дата рождения {4})";
                string CommandText = string.Format("SELECT a.NPSS, a.FAMIL, a.IMJA, a.OTCH, b.DROG FROM '{0}' as a INNER JOIN '{1}' as b ON a.NPSS = b.NPSS WHERE NOT EMPTY(a.NPSS) AND NOT EMPTY(b.NPSS) AND a.DROG <> b.DROG", SupplierFileName, PaymentFileName);
                OleDbCommand Command = FoxPro.OleDbCommand(CommandText);
                OleDbDataReader Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    Log.Messages.Add(string.Format(ErrorString, Reader[0].ToString().Trim(), Reader[1].ToString().Trim(), Reader[2].ToString().Trim(), Reader[3].ToString().Trim(), Reader.GetDateTime(Reader.GetOrdinal("DROG")).ToString("yyyy-MM-dd")));
                    Result++;
                    Handler("DROG", null);
                }
            }
            catch (Exception E)
            {
                Log.Errors.Add(E.Message);
            }
            return Result;
        }

        public static int CheckNPSS(string SupplierFileName, string PaymentFileName, EventHandler Handler)
        {
            int Result = 0;
            try
            {
                string CommandText = string.Format("SELECT a.NPSS, a.FAMIL, a.IMJA, a.OTCH FROM '{0}' as a WHERE a.NPSS NOT IN (SELECT NPSS FROM '{1}')", SupplierFileName, PaymentFileName);
                OleDbCommand Command = FoxPro.OleDbCommand(CommandText);
                OleDbDataReader Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    string ErrorString = "{0}; {1} {2} {3}; Запись не найдена в исходном файле";
                    Log.Messages.Add(string.Format(ErrorString, Reader[0].ToString().Trim(), Reader[1].ToString().Trim(), Reader[2].ToString().Trim(), Reader[3].ToString().Trim()));
                    Result++;
                    Handler("NPSS", null);
                }
            }
            catch (Exception E)
            {
                Log.Errors.Add(E.Message);
            }
            return Result;
        }

        public static int CheckLS(string SupplierFileName, EventHandler Handler)
        {
            int Result = 0;
            if (CheckLSEnabled)
            {
                foreach (Person Person in PersonList)
                {
                    Result += Person.GetErrorLSH();
                    Handler("3/12", null);
                }
            }
            return Result;
        }

        public static int CheckKDOMVL(string SupplierFileName, EventHandler Handler)
        {
            int Result = 0;
            if (CheckKDOMVLEnabled)
            {
                foreach (Person Person in PersonList)
                {
                    Result += Person.GetErrorKDOMVL();
                    Handler("4/12", null);
                }
            }
            return Result;
        }

        public static int CheckROPL(string SupplierFileName, EventHandler Handler)
        {
            int Result = 0;
            foreach (Person Person in PersonList)
            {
                Result += Person.GetErrorROPL();
                Handler("5/12", null);
            }
            return Result;
        }

        public static int CheckKCHLS(string SupplierFileName, EventHandler Handler)
        {
            int Result = 0;
            foreach (Person Person in PersonList)
            {
                Result += Person.GetErrorKCHLS();
                Handler("6/12", null);
            }
            return Result;
        }

        public static int CheckS_(string SupplierFileName, EventHandler Handler)
        {
            int Result = 0;
            foreach (Person Person in PersonList)
            {
                Result += Person.GetErrorS_();
                Handler("7/12", null);
            }
            return Result;
        }

        public static int CheckKOD_T(string SupplierFileName, EventHandler Handler)
        {
            int Result = 0;
            foreach (Person Person in PersonList)
            {
                Result += Person.GetErrorKOD_T();
                Handler("8/12", null);
            }
            return Result;
        }

        public static int CheckSUMLN(EventHandler Handler)
        {
            int Result = 0;
            foreach (Person Person in PersonList)
            {
                Result += Person.GetErrorSUMLN();
                Handler("9/12", null);
            }
            return Result;
        }

        public static int CheckVOL(EventHandler Handler)
        {
            int Result = 0;
            foreach (Person Person in PersonList)
            {
                Result += Person.GetErrorVOL();
                Handler("10/12", null);
            }
            return Result;
        }

        public static int CheckTARIF(EventHandler Handler)
        {
            int Result = 0;
            foreach (Person Person in PersonList)
            {
                Result += Person.GetErrorTARIF();
                Handler("11/12", null);
            }
            return Result;
        }

        public static int CheckK_POL(string SupplierFileName, EventHandler Handler)
        {
            int Result = 0;
            foreach (Person Person in PersonList)
            {
                Result += Person.GetErrorK_POL();
                Handler("12/12", null);
            }
            return Result;
        }

        public static int CheckKKOM(EventHandler Handler) { return 0; }
    }
}