using System;
using System.Collections.Generic;
using System.Data.OleDb;

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
                Person.NPSS = Reader["NPSS"].ToString().Trim();

                if (Exist("KDOMVL")) { int.TryParse(Reader["KDOMVL"].ToString(), out Person.KDOMVL); }
                float.TryParse(Reader["ROPL"].ToString(), out Person.ROPL);
                int.TryParse(Reader["KCHLS"].ToString(), out Person.KCHLS);
                if (Exist("K_POL")) { int.TryParse(Reader["K_POL"].ToString(), out Person.K_POL); }

                for (int i = 1; i < 10; i++)
                {
                    Service Service = new Service();
                    try
                    {
                        int.TryParse(Reader[string.Format("PRED{0}", i)].ToString(), out Service.PRED);
                        Service.VID = Reader[string.Format("VID{0}", i)].ToString().Trim();
                        float.TryParse(Reader[string.Format("VOL{0}", i)].ToString(), out Service.VOL);
                        float.TryParse(Reader[string.Format("SUMLN{0}", i)].ToString(), out Service.SUMLN);

                        if (Service.FILLED())
                        {
                            if (Exist(string.Format("LSH{0}", i))) { Service.LSH = Reader[string.Format("LSH{0}", i)].ToString().Trim(); }
                            try { Service.K_POL = int.Parse(Reader[string.Format("K_POL{0}", i)].ToString()); }
                            catch { Service.K_POL = Person.K_POL; }
                            float.TryParse(Reader[string.Format("TARIF{0}", i)].ToString(), out Service.TARIF);
                            float.TryParse(Reader[string.Format("SUMLD{0}", i)].ToString(), out Service.SUMLD);
                            float.TryParse(Reader[string.Format("SUMLF{0}", i)].ToString(), out Service.SUMLF);
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
                    PersonList.Add(Person);
                }

                Handler("1/12", null);
            }
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