using System;

namespace CheckDBF.Core
{
    class PersonData
    {
        public string FAMIL;
        public string IMJA;
        public string OTCH;
        public DateTime DROG;
        public string NPSS;
        public string PY;
        public string NNASP;
        public string NYLIC;
        public int NDOM;
        public string LDOM;
        public int KORP;
        public int NKW;
        public string LKW;
        public int PVID;
        public string PSR;
        public string PNM;
        public int KSS;
        public string KOD;
        public DateTime SROKS;
        public DateTime SROKPO;
        public int KDOMVL;
        public double ROPL;
        public int KCHLS;
        public int K_POL;
        public int KKOM;
        public DateTime DATE_VIGR;
        public string PRIM;
        public Service[] Services = new Service[9];
    }
}