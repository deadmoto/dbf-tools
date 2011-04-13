using System;

namespace CheckDBF.Core
{
    class Service : ServiceData
    {
        public bool FILLED()
        {
            return PRED > 0 && VID.Length == 4 && VOL > 0 && SUMLN > 0;
        }

        public void GetConformData()
        {
            ConformData ConformData = Conform.GetConformData(PRED, VID, KOD_T, KOD_N);
            if (ConformData == null)
            {
                TARIF_E = -1;
                VOL_E = -1;
            }
            else
            {
                TARIF_E = ConformData.TARIF;
                VOL_E = ConformData.VOL;
            }
        }

        public bool GetErrorLSH()
        {
            return LSH == string.Empty && FILLED();
        }

        public bool GetErrorS_()
        {
            switch (VID)
            {
                case "0100": return Array.IndexOf(new int[] { 0, 2 }, S_) == -1 && FILLED();
                case "0204": return Array.IndexOf(new int[] { 1, 2, 3, 4, 5 }, S_) == -1 && FILLED();
                case "0205": return Array.IndexOf(new int[] { 1, 2, 3, 4, 5 }, S_) == -1 && FILLED();
                case "0206": return Array.IndexOf(new int[] { 1, 2, 3, 4, 5 }, S_) == -1 && FILLED();
                case "0207": return Array.IndexOf(new int[] { 1, 2, 3, 4, 5 }, S_) == -1 && FILLED();
                default: return Array.IndexOf(new int[] { 1, 2 }, S_) == -1 && FILLED();
            }
        }

        public bool GetErrorKOD_T()
        {
            return (TARIF_E == -1 || VOL_E == -1) && FILLED();
        }

        public bool GetErrorSUMLN()
        {
            SUMLND = Math.Round(SUMLN - TARIF * VOL, 2);
            return Math.Abs(SUMLND) * 100 / SUMLN > 1 && FILLED();
        }

        public bool GetErrorVOL(double ROPL, int KCHLS)
        {
            if (FILLED())
            {
                switch (VID)
                {
                    case "0100": return VOL != ROPL;
                    case "0204": return VOL != ROPL && S_ == 2;
                    case "0205": return VOL != ROPL && S_ == 2;
                    case "0206": return VOL != ROPL && S_ == 2;
                    case "0207": return VOL != ROPL && S_ == 2;
                    default: return Math.Abs(VOL - VOL_E * KCHLS) * 100 > 1 && VOL_E != -1 && S_ == 2;
                }
            }

            return false;
        }

        public bool GetErrorTARIF()
        {
            return VID != "0100" && TARIF != 0 && TARIF_E != -1 && Math.Abs(TARIF - TARIF_E) * 100 > 1 && FILLED();
        }

        public bool GetErrorK_POL(int KCHLS)
        {
            return K_POL > KCHLS && FILLED();
        }

        public bool GetErrorPreload(double ROPL, int KCHLS)
        {
            if (Math.Abs(Math.Round(VOL * TARIF, 2) / SUMLN - 1) * 100 < 1 && FILLED())
            {
                if (VID == "0100") { return VOL != ROPL; }
                if (S_ == 1 || S_ > 2) { return Math.Abs(Math.Round(VOL * TARIF_E, 2) / SUMLN - 1) * 100 > 1; }
                if (S_ == 2)
                {
                    if (VID == "0204") { return Math.Abs(Math.Round(ROPL * TARIF_E, 2) / SUMLN - 1) * 100 > 1; }
                    else { return Math.Abs(Math.Round(KCHLS * VOL_E * TARIF_E, 2) / SUMLN - 1) * 100 > 1; }
                }
            }

            return true;
        }

        public string GetCommandText(double ROPL, int KCHLS, bool Valid)
        {
            if (GetErrorPreload(ROPL, KCHLS) == Valid)
            {
                return "0,'','',0,0,0,0,0,0,0,0";
            }
            else
            {
                return string.Format("{0},'{1}','{2}',{3},{4},{5},{6},{7},{8},{9},{10}", PRED, VID, LSH, VOL.ToString().Replace(',', '.'), TARIF.ToString().Replace(',', '.'), SUMLN.ToString().Replace(',', '.'), SUMLD.ToString().Replace(',', '.'), SUMLF.ToString().Replace(',', '.'), KOD_T, KOD_N, S_);
            }
        }
    }
}