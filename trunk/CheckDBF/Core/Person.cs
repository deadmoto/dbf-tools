namespace CheckDBF.Core
{
    class Person
    {
        public string FAMIL;
        public string IMJA;
        public string OTCH;
        public string NPSS;

        public int KDOMVL;
        public float ROPL;
        public int KCHLS;
        public int K_POL;

        public Service[] Services = new Service[9];

        public bool GetErrorEMPTY()
        {
            bool Result = true;
            for (int i = 0; i < 9; i++)
            {
                if (Services[i].FILLED())
                {
                    Result = false;
                }
            }
            return Result;
        }

        public int GetErrorLSH()
        {
            int Result = 0;
            for (int i = 0; i < 9; i++)
            {
                if (Services[i].GetErrorLSH())
                {
                    string ErrorString = "{0}; {1} {2} {3}; Не заполнен номер лицевого счета (услуга {4})";
                    Log.Messages.Add(string.Format(ErrorString, NPSS, FAMIL, IMJA, OTCH, Services[i].VID));
                    Result++;
                }
            }
            return Result;
        }

        public int GetErrorKDOMVL()
        {
            int Result = 0;
            for (int i = 0; i < 9; i++)
            {
                if (KDOMVL > 0 && Services[i].VID == "0100" && Services[i].FILLED())
                {
                    string ErrorString = "{0}; {1} {2} {3}; Не проставлен вид жилого фонда";
                    Log.Messages.Add(string.Format(ErrorString, NPSS, FAMIL, IMJA, OTCH));
                    Result++;
                }
            }
            return Result;
        }

        public int GetErrorROPL()
        {
            int Result = 0;
            for (int i = 0; i < 9; i++)
            {
                if (ROPL <= 0 && Services[i].VID == "0100" && Services[i].FILLED())
                {
                    string ErrorString = "{0}; {1} {2} {3}; Не проставлена фактически занимаемая площадь";
                    Log.Messages.Add(string.Format(ErrorString, NPSS, FAMIL, IMJA, OTCH));
                    Result++;
                }
            }
            return Result;
        }

        public int GetErrorKCHLS()
        {
            if ((KCHLS > 0) == false)
            {
                string ErrorString = "{0}; {1} {2} {3}; Не проставлено количество лиц, зарегистрированных в жилом помещении";
                Log.Messages.Add(string.Format(ErrorString, NPSS, FAMIL, IMJA, OTCH));
                return 1;
            }
            return 0;
        }

        public int GetErrorS_()
        {
            int Result = 0;
            for (int i = 0; i < 9; i++)
            {
                if (Services[i].GetErrorS_())
                {
                    string ErrorString = "{0}; {1} {2} {3}; Не проставлено наличие счетчика (услуга {4})";
                    Log.Messages.Add(string.Format(ErrorString, NPSS, FAMIL, IMJA, OTCH, Services[i].VID));
                    Result++;
                }
            }
            return Result;
        }

        public int GetErrorKOD_T()
        {
            int Result = 0;
            for (int i = 0; i < 9; i++)
            {
                if (Services[i].GetErrorKOD_T())
                {
                    string ErrorString = "{0}; {1} {2} {3}; Запись по услуге не найдена в справочнике соответствия (услуга {4} предприятие {5} код тарифа {6} код норматива {7})";
                    Log.Messages.Add(string.Format(ErrorString, NPSS, FAMIL, IMJA, OTCH, Services[i].VID, Services[i].PRED, Services[i].KOD_T, Services[i].KOD_N));
                    Result++;
                }
            }
            return Result;
        }

        public int GetErrorSUMLN()
        {
            int Result = 0;
            for (int i = 0; i < 9; i++)
            {
                if (Services[i].GetErrorSUMLN())
                {
                    string ErrorString = "{0}; {1} {2} {3}; Сумма прогнозного начисления не соответствует объему услуги и ее тарифу на {4} (услуга {5} объем {6} тариф {7} сумма {8})";
                    Log.Messages.Add(string.Format(ErrorString, NPSS, FAMIL, IMJA, OTCH, Services[i].SUMLND, Services[i].VID, Services[i].VOL, Services[i].TARIF, Services[i].SUMLN));
                    Result++;
                }
            }
            return Result;
        }

        public int GetErrorVOL()
        {
            int Result = 0;
            for (int i = 0; i < 9; i++)
            {
                if (Services[i].GetErrorVOL(ROPL, KCHLS))
                {
                    string ErrorString = "{0}; {1} {2} {3}; Объем услуги не совпадает с нормативным при отсутствии счетчика (услуга {4} объем {5} норматив {6} проживающих {7})";
                    Log.Messages.Add(string.Format(ErrorString, NPSS, FAMIL, IMJA, OTCH, Services[i].VID, Services[i].VOL, Services[i].VOL_E, KCHLS));
                    Result++;
                }
            }
            return Result;
        }

        public int GetErrorTARIF()
        {
            int Result = 0;
            for (int i = 0; i < 9; i++)
            {
                if (Services[i].GetErrorTARIF())
                {
                    string ErrorString = "{0}; {1} {2} {3}; Значение тарифа {4} на услугу {5} не совпадает со значением из cправочника {6}";
                    Log.Messages.Add(string.Format(ErrorString, NPSS, FAMIL, IMJA, OTCH, Services[i].TARIF, Services[i].VID, Services[i].TARIF_E));
                    Result++;
                }
            }
            return Result;
        }

        public int GetErrorK_POL()
        {
            int Result = 0;
            for (int i = 0; i < 9; i++)
            {
                if (Services[i].GetErrorK_POL(KCHLS))
                {
                    string ErrorString = "{0}; {1} {2} {3}; Указано количество граждан {4}, совместно пользующихся льготой по услуге {5}, превышающее количество зарегистрированных {6}";
                    Log.Messages.Add(string.Format(ErrorString, NPSS, FAMIL, IMJA, OTCH, Services[i].K_POL, Services[i].VID, KCHLS));
                    Result++;
                }
            }
            return Result;
        }

        public int GetErrorKKOM() { return 0; }
    }
}