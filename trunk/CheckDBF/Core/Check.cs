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

        public static bool CheckLSEnabled = true;
        public static bool CheckKDOMVLEnabled = true;
        public static bool CheckROPLEnabled = true;
        public static bool ShowMessages = false;

        private static bool IsNotNull(OleDbDataReader Reader, string FieldName)
        {
            try
            {
                int Ordinal = Reader.GetOrdinal(FieldName);

                if (Reader.IsDBNull(Ordinal) == false)
                {
                    return Reader[Ordinal].ToString().Trim().Length > 0;
                }

                return false;
            }
            catch (IndexOutOfRangeException E)
            {
                return false;
            }
            catch (Exception E)
            {
                if (ShowMessages == true)
                {
                    MessageBox.Show(E.Message, FieldName);
                }
                return false;
            }
        }

        private static Person GetPerson(OleDbDataReader Reader)
        {
            Person Person = new Person();

            try
            {
                if (IsNotNull(Reader, "FAMIL")) { Person.FAMIL = Reader["FAMIL"].ToString().Trim(); }
                if (IsNotNull(Reader, "IMJA")) { Person.IMJA = Reader["IMJA"].ToString().Trim(); }
                if (IsNotNull(Reader, "OTCH")) { Person.OTCH = Reader["OTCH"].ToString().Trim(); }
                if (IsNotNull(Reader, "DROG")) { Person.DROG = DateTime.Parse(Reader["DROG"].ToString()); }
                if (IsNotNull(Reader, "NPSS")) { Person.NPSS = Reader["NPSS"].ToString().Trim(); }
                if (IsNotNull(Reader, "PY")) { Person.PY = Reader["PY"].ToString().Trim(); }
                if (IsNotNull(Reader, "NNASP")) { Person.NNASP = Reader["NNASP"].ToString().Trim(); }
                if (IsNotNull(Reader, "NYLIC")) { Person.NYLIC = Reader["NYLIC"].ToString().Trim(); }
                if (IsNotNull(Reader, "NDOM")) { Person.NDOM = int.Parse(Reader["NDOM"].ToString()); }
                if (IsNotNull(Reader, "LDOM")) { Person.LDOM = Reader["LDOM"].ToString().Trim(); }
                if (IsNotNull(Reader, "KORP")) { Person.KORP = int.Parse(Reader["KORP"].ToString()); }
                if (IsNotNull(Reader, "NKW")) { Person.NKW = int.Parse(Reader["NKW"].ToString()); }
                if (IsNotNull(Reader, "LKW")) { Person.LKW = Reader["LKW"].ToString().Trim(); }
                if (IsNotNull(Reader, "PVID")) { Person.PVID = int.Parse(Reader["PVID"].ToString()); }
                if (IsNotNull(Reader, "PSR")) { Person.PSR = Reader["PSR"].ToString().Trim(); }
                if (IsNotNull(Reader, "PNM")) { Person.PNM = Reader["PNM"].ToString().Trim(); }
                if (IsNotNull(Reader, "KSS")) { Person.KSS = int.Parse(Reader["KSS"].ToString()); }
                if (IsNotNull(Reader, "KOD")) { Person.KOD = Reader["KOD"].ToString().Trim(); }
                if (IsNotNull(Reader, "SROKS")) { Person.SROKS = Reader.GetDateTime(Reader.GetOrdinal("SROKS")); }
                if (IsNotNull(Reader, "SROKPO")) { Person.SROKPO = Reader.GetDateTime(Reader.GetOrdinal("SROKPO")); }
                if (IsNotNull(Reader, "KDOMVL")) { Person.KDOMVL = int.Parse(Reader["KDOMVL"].ToString()); }
                if (IsNotNull(Reader, "ROPL")) { Person.ROPL = double.Parse(Reader["ROPL"].ToString()); }
                if (IsNotNull(Reader, "KCHLS")) { Person.KCHLS = int.Parse(Reader["KCHLS"].ToString()); }
                if (IsNotNull(Reader, "K_POL")) { Person.K_POL = int.Parse(Reader["K_POL"].ToString()); }
                if (IsNotNull(Reader, "KKOM")) { Person.KKOM = int.Parse(Reader["KKOM"].ToString()); }
                if (IsNotNull(Reader, "DATE_VIGR")) { Person.DATE_VIGR = DateTime.Parse(Reader["DATE_VIGR"].ToString()); }
                if (IsNotNull(Reader, "PRIM")) { Person.PRIM = Reader["PRIM"].ToString().Trim(); }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }

            return Person;
        }

        private static Service GetService(OleDbDataReader Reader, int i, int K_POL)
        {
            Service Service = new Service();

            try
            {
                if (IsNotNull(Reader, "PRED" + i)) { Service.PRED = int.Parse(Reader["PRED" + i].ToString()); }
                if (Service.PRED == 0) { return new Service(); };
                if (IsNotNull(Reader, "VID" + i)) { Service.VID = Reader["VID" + i].ToString().Trim(); }
                if (Service.VID.Length != 4) { return new Service(); };
                if (IsNotNull(Reader, "VOL" + i)) { Service.VOL = double.Parse(Reader["VOL" + i].ToString()); }
                if (IsNotNull(Reader, "SUMLN" + i)) { Service.SUMLN = double.Parse(Reader["SUMLN" + i].ToString()); }
                if (IsNotNull(Reader, "LSH" + i)) { Service.LSH = Reader["LSH" + i].ToString().Trim(); }
                if (IsNotNull(Reader, "K_POL" + i)) { Service.K_POL = int.Parse(Reader["K_POL" + i].ToString()); }
                else { Service.K_POL = K_POL; }
                if (IsNotNull(Reader, "TARIF" + i)) { Service.TARIF = double.Parse(Reader["TARIF" + i].ToString()); }
                if (IsNotNull(Reader, "SUMLD" + i)) { Service.SUMLD = double.Parse(Reader["SUMLD" + i].ToString()); }
                if (IsNotNull(Reader, "SUMLF" + i)) { Service.SUMLF = double.Parse(Reader["SUMLF" + i].ToString()); }
                if (IsNotNull(Reader, "KOD_T" + i)) { Service.KOD_T = int.Parse(Reader["KOD_T" + i].ToString()); }
                if (IsNotNull(Reader, "KOD_N" + i)) { Service.KOD_N = int.Parse(Reader["KOD_N" + i].ToString()); }
                if (IsNotNull(Reader, "S_" + i)) { Service.S_ = int.Parse(Reader["S_" + i].ToString()); }
                Service.GetConformData();
                return Service;
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
                return new Service();
            }
        }

        public static void GetPersonList(string SupplierFileName, EventHandler Handler)
        {
            Handler("Reading", null);

            string ErrorFileName = Path.GetDirectoryName(SupplierFileName) + "\\" + Path.GetFileNameWithoutExtension(SupplierFileName) + "-error.dbf";
            string ValidFileName = Path.GetDirectoryName(SupplierFileName) + "\\" + Path.GetFileNameWithoutExtension(SupplierFileName) + "-valid.dbf";
            File.Copy(Application.StartupPath + "\\Data\\Payment.dbf", ErrorFileName, true);
            File.Copy(Application.StartupPath + "\\Data\\Payment.dbf", ValidFileName, true);

            PersonList.Clear();

            string CommandText = string.Format("SELECT * FROM '{0}'", SupplierFileName);
            OleDbCommand Command = FoxPro.OleDbCommand(CommandText);
            OleDbDataReader Reader = Command.ExecuteReader();

            while (Reader.Read())
            {
                Person Person = GetPerson(Reader);

                for (int i = 1; i < 10; i++)
                {
                    Person.Services[i - 1] = GetService(Reader, i, Person.K_POL);
                }

                if (Person.GetErrorEMPTY() == false)
                {
                    ProcessPreload(Person.GetCommandText(ErrorFileName, false));
                    ProcessPreload(Person.GetCommandText(ValidFileName, true));
                    PersonList.Add(Person);
                }

                Handler("Reading", null);
            }

            Reader.Close();
            Command.Connection.Close();

            Delete(ErrorFileName);
            Delete(ValidFileName);
        }

        public static int CheckFAMIL(string SupplierFileName, string PaymentFileName, EventHandler Handler)
        {
            int Result = 0;

            try
            {
                if (PaymentFileName.Length > 0)
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
                if (PaymentFileName.Length > 0)
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
                if (PaymentFileName.Length > 0)
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
                if (PaymentFileName.Length > 0)
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
                if (PaymentFileName.Length > 0)
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

        public static int CheckINVALID(string SupplierFileName, EventHandler Handler)
        {
            string ErrorFileName = Path.GetDirectoryName(SupplierFileName) + "\\" + Path.GetFileNameWithoutExtension(SupplierFileName) + "-error.dbf";
            return FileInfo.GetRecordCount(ErrorFileName);
        }

        private static void ProcessPreload(string CommandText)
        {
            OleDbCommand Command = FoxPro.OleDbCommand(CommandText);
            Command.ExecuteNonQuery();
            Command.Connection.Close();
        }

        private static void Delete(string FileName)
        {
            string CommandText;
            OleDbCommand Command;

            CommandText = string.Format("DELETE FROM '{0}' WHERE PRED1 = 0 AND PRED2 = 0 AND PRED3 = 0 AND PRED4 = 0 AND PRED5 = 0 AND PRED6 = 0 AND PRED7 = 0 AND PRED8 = 0 AND PRED9 = 0", FileName);
            Command = FoxPro.OleDbCommand(CommandText);
            Command.ExecuteNonQuery();
            Command.Connection.Close();

            CommandText = string.Format("PACK '{0}'", FileName);
            Command = FoxPro.OleDbCommand(CommandText);
            Command.ExecuteNonQuery();
            Command.Connection.Close();
        }
    }
}