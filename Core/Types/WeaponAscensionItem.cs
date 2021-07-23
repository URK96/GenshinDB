using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace GenshinDB_Core.Types
{
    public class WeaponAscensionItem
    {
        public string ItemName { get; private set; }
        public GenshinDB.Locations Location { get; private set; }

        public List<DayOfWeek> AvailableDayOfWeeks { get; private set; }

        public WeaponAscensionItem(GenshinDB.Locations location, DayOfWeek[] dayOfWeeks)
        {
            ItemName = "All";
            Location = location;
            AvailableDayOfWeeks = new List<DayOfWeek>(dayOfWeeks);
        }

        public WeaponAscensionItem(DataRow dr)
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
                "Inazuma" => GenshinDB.Locations.Inazuma,
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
}
