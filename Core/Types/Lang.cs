using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace GenshinDB_Core.Types
{
    public class Lang
    {
        public string Name { get; private set; }
        public Dictionary<string, string> Dic { get; private set; }

        public Lang(DataRow dr)
        {
            Name = dr["Name"] as string;

            Dic = new Dictionary<string, string>
            {
                { "Default", dr["Default"] as string },
                { "ko", dr["ko"] as string }
            };
        }
    }
}
