namespace SplitDBF
{
    /// <summary>
    /// Provides region information
    /// </summary>
    static class Region
    {
        /// <summary>
        /// Returns region name
        /// </summary>
        public static string GetRegionName(string Code)
        {
            switch (Code)
            {
                case "03": return "Центральный";
                case "04": return "Советский";
                case "05": return "Кировский";
                case "06": return "Ленинский";
                case "07": return "Октябрьский";
                case "09": return "Азовский";
                case "10": return "Большереченский";
                case "11": return "Большеуковский";
                case "12": return "Горьковский";
                case "13": return "Знаменский";
                case "14": return "Исилькульский";
                case "15": return "Калачинский";
                case "16": return "Колосовский";
                case "17": return "Кормиловский";
                case "18": return "Крутинский";
                case "19": return "Любинский";
                case "20": return "Марьяновский";
                case "21": return "Москаленский";
                case "22": return "Муромцевский";
                case "23": return "Называевский";
                case "24": return "Нижнеомский";
                case "25": return "Нововаршавский";
                case "26": return "Одесский";
                case "27": return "Оконешниковский";
                case "28": return "Омский";
                case "29": return "Павлоградский";
                case "30": return "Полтавский";
                case "31": return "Русско-Полянский";
                case "32": return "Саргатский";
                case "33": return "Седельниковский";
                case "34": return "Таврический";
                case "35": return "Тарский";
                case "36": return "Тевризский";
                case "37": return "Тюкалинский";
                case "38": return "Усть-Ишимский";
                case "39": return "Черлакский";
                case "40": return "Шербакульский";
                default: return Code;
            }
        }
    }
}