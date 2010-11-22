using System;

namespace CheckDBF.Core
{
    class Service
    {
        public string LSH;
        public int PRED;
        public string VID;
        public int K_POL;
        public float VOL;
        public float TARIF;
        public float SUMLN;
        public float SUMLND;
        public float SUMLD;
        public float SUMLF;
        public int KOD_T;
        public int KOD_N;
        public int S_;

        public float TARIF_E = -2;
        public float VOL_E = -2;

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
            return S_ == 0 && VID != "0100" && FILLED();
        }

        public bool GetErrorKOD_T()
        {
            return (TARIF_E == -1 || VOL_E == -1) && FILLED();
        }

        public bool GetErrorSUMLN()
        {
            SUMLND = (float)Math.Round(TARIF * VOL - SUMLN, 2);
            return (SUMLND < -0.01 || SUMLND > 0.01) && FILLED();
        }

        public bool GetErrorVOL(float ROPL, int KCHLS)
        {
            if (S_ == 2 && FILLED())
            {
                if (VID == "0100" || VID == "0204")
                {
                    return ROPL != VOL && PRED > 0;
                }
                float VOL_K = (float)Math.Round(VOL_E * KCHLS, 2);
                return VOL_K != VOL && VOL_E != -1;
            }
            return false;
        }

        public bool GetErrorTARIF()
        {
            return TARIF != 0 && TARIF_E != -1 && TARIF != TARIF_E && FILLED();
        }

        public bool GetErrorK_POL(int KCHLS)
        {
            return K_POL > KCHLS && FILLED();
        }
    }
}