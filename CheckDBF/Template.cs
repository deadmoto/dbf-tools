using System.Collections.Generic;

namespace CheckDBF
{
    static class Template
    {
        public static Dictionary<string, string> Items = GetDictionary();

        private static Dictionary<string, string> GetDictionary()
        {
            Dictionary<string, string> Items = new Dictionary<string, string>();
            Items.Add("FAMIL", "SELECT COUNT(a.NPSS) FROM '{0}' as a INNER JOIN '{1}' as b ON a.NPSS = b.NPSS WHERE NOT EMPTY(a.NPSS) AND NOT EMPTY(b.NPSS) AND ALLTRIM(a.FAMIL) <> ALLTRIM(b.FAMIL)");
            Items.Add("IMJA", "SELECT COUNT(a.NPSS) FROM '{0}' as a INNER JOIN '{1}' as b ON a.NPSS = b.NPSS WHERE NOT EMPTY(a.NPSS) AND NOT EMPTY(b.NPSS) AND ALLTRIM(a.IMJA) <> ALLTRIM(b.IMJA)");
            Items.Add("OTCH", "SELECT COUNT(a.NPSS) FROM '{0}' as a INNER JOIN '{1}' as b ON a.NPSS = b.NPSS WHERE NOT EMPTY(a.NPSS) AND NOT EMPTY(b.NPSS) AND ALLTRIM(a.OTCH) <> ALLTRIM(b.OTCH)");
            Items.Add("DROG", "SELECT COUNT(a.NPSS) FROM '{0}' as a INNER JOIN '{1}' as b ON a.NPSS = b.NPSS WHERE NOT EMPTY(a.NPSS) AND NOT EMPTY(b.NPSS) AND a.DROG <> b.DROG");
            return Items;
        }
    }
}