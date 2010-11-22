using System;
using System.Collections.Generic;
using System.Data.Odbc;

namespace CheckDBF.Core
{
    static class Check
    {
        private static List<Person> PersonList = new List<Person>();
        private static List<string> FieldList = new List<string>();

        public static bool CheckLSEnabled = true;
        public static bool CheckKDOMVLEnabled = true;
        public static bool CheckROPLEnabled = true;

        private static bool FieldExist(string FieldName)
        {
            return FieldList.FindAll(delegate(string Item) { return Item.ToUpper() == FieldName.ToUpper(); }).Count > 0;
        }

        public static void GetPersonList(string SupplierFileName, EventHandler Handler)
        {
            PersonList.Clear();
            FieldList.Clear();

            string CommandText = string.Format("SELECT * FROM '{0}'", SupplierFileName);
            OdbcCommand Command = OdbcDriver.VFPCommand(CommandText, "");
            OdbcDataReader Reader = Command.ExecuteReader();

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

                Person.KDOMVL = int.Parse(Reader["KDOMVL"].ToString());
                Person.ROPL = float.Parse(Reader["ROPL"].ToString());
                Person.KCHLS = int.Parse(Reader["KCHLS"].ToString());
                try { Person.K_POL = int.Parse(Reader["K_POL"].ToString()); }
                catch { }

                for (int i = 1; i < 10; i++)
                {
                    Service Service = new Service();
                    try
                    {
                        Service.PRED = int.Parse(Reader[string.Format("PRED{0}", i)].ToString());
                        Service.VID = Reader[string.Format("VID{0}", i)].ToString().Trim();
                        Service.VOL = float.Parse(Reader[string.Format("VOL{0}", i)].ToString());
                        Service.SUMLN = float.Parse(Reader[string.Format("SUMLN{0}", i)].ToString());

                        if (Service.FILLED())
                        {
                            if (FieldExist(string.Format("LSH{0}", i))) { Service.LSH = Reader[string.Format("LSH{0}", i)].ToString().Trim(); }
                            try { Service.K_POL = int.Parse(Reader[string.Format("K_POL{0}", i)].ToString()); }
                            catch { Service.K_POL = Person.K_POL; }
                            Service.TARIF = float.Parse(Reader[string.Format("TARIF{0}", i)].ToString());
                            Service.SUMLD = float.Parse(Reader[string.Format("SUMLD{0}", i)].ToString());
                            Service.SUMLF = float.Parse(Reader[string.Format("SUMLF{0}", i)].ToString());
                            Service.KOD_T = int.Parse(Reader[string.Format("KOD_T{0}", i)].ToString());
                            Service.KOD_N = int.Parse(Reader[string.Format("KOD_N{0}", i)].ToString());
                            Service.S_ = int.Parse(Reader[string.Format("S_{0}", i)].ToString());
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

        public static int CheckNPSS(string SupplierFileName, string PaymentFileName, EventHandler Handler)
        {
            int Result = 0;
            try
            {
                string CommandText = string.Format("SELECT ALLTRIM(a.NPSS), ALLTRIM(a.FAMIL), ALLTRIM(a.IMJA), ALLTRIM(a.OTCH) FROM '{0}' as a WHERE a.NPSS NOT IN (SELECT NPSS FROM '{1}')", SupplierFileName, PaymentFileName);
                OdbcCommand Command = OdbcDriver.VFPCommand(CommandText, "");
                OdbcDataReader Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    string S = "СНИЛС не найден в исходном файле;";
                    for (int i = 0; i < Reader.FieldCount; i++) { S += Reader[i].ToString().Trim() + ";"; }
                    Log.Messages.Add(string.Format("{0}; {1} {2} {3}; СНИЛС не найден в исходном файле", Reader[0].ToString().Trim(), Reader[1].ToString().Trim(), Reader[2].ToString().Trim(), Reader[3].ToString().Trim()));
                    Result++;
                    Handler("2/12", null);
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