using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;

using DBEnv = GenshinDB_Core.DBEnvironment;

namespace GenshinDB_Core
{
    public class Character
    {
        public enum ElementTypes { Pyro, Hydro, Dendro, Electro, Anemo, Cryo, Geo, Adaptive }

        public string Name { get; private set; }
        public ElementTypes ElementType { get; private set; }
        public List<string> TalentItem { get; private set; }

        public Character(DataRow dr)
        {
            string talent = 

            Name = dr["Name"] as string;
            TalentItem = new List<string>((dr["TalentItem"] as string).Split(','));

            SetElementType(dr["ElementType"] as string);
        }

        private void SetElementType(string data)
        {
            ElementType = data switch
            {
                "Pyro" => ElementTypes.Pyro,
                "Hydro" => ElementTypes.Hydro,
                "Dendro" => ElementTypes.Dendro,
                "Electro" => ElementTypes.Electro,
                "Anemo" => ElementTypes.Anemo,
                "Cryo" => ElementTypes.Cryo,
                "Geo" => ElementTypes.Geo,
                _ => ElementTypes.Adaptive
            };
        }
    }

    public class TalentItem
    {
        public string ItemName { get; private set; }
        public GenshinDB.Locations Location { get; private set; }

        public List<DayOfWeek> AvailableDayOfWeeks { get; private set; }
        
        public TalentItem(GenshinDB.Locations location, DayOfWeek[] dayOfWeeks)
        {
            ItemName = "All";
            Location = location;
            AvailableDayOfWeeks = new List<DayOfWeek>(dayOfWeeks);
        }

        public TalentItem(DataRow dr)
        {
            ItemName = dr["Name"] as string;

            SetLocation(dr["Location"] as string);
            SetAvailable(dr["DayOfWeek"] as string);
        }

        private void SetLocation(string data)
        {
            Location = data switch
            {
                "Mondstadt" => GenshinDB.Locations.Mondstadt,
                "Liyue" => GenshinDB.Locations.Liyue,
                _ => GenshinDB.Locations.Mondstadt
            };
        }

        private void SetAvailable(string data)
        {
            string[] temp = data.Split(',');

            AvailableDayOfWeeks = new List<DayOfWeek>();

            for (int i = 0; i < temp.Length; ++i)
            {
                AvailableDayOfWeeks.Add((DayOfWeek)int.Parse(temp[i]));
            }
        }
    }

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
