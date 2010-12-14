using System;

namespace CheckDBF.Core
{
    class PersonData
    {
        public string FAMIL = string.Empty;
        public string IMJA = string.Empty;
        public string OTCH = string.Empty;
        public DateTime DROG;
        public string NPSS = string.Empty;
        public string PY = string.Empty;
        public string NNASP = string.Empty;
        public string NYLIC = string.Empty;
        public int NDOM;
        public string LDOM = string.Empty;
        public int KORP;
        public int NKW;
        public string LKW = string.Empty;
        public int PVID;
        public string PSR = string.Empty;
        public string PNM = string.Empty;
        public int KSS;
        public string KOD = string.Empty;
        public DateTime SROKS;
        public DateTime SROKPO;
        public int KDOMVL;
        public double ROPL;
        public int KCHLS;
        public int K_POL;
        public int KKOM;
        public DateTime DATE_VIGR;
        public string PRIM = string.Empty;
        public Service[] Services = new Service[9];
    }
}