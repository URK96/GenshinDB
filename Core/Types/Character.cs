using System;
using System.Collections.Generic;
using System.Data;

namespace GenshinDB_Core.Types
{
    public class Character
    {
        public enum WeaponTypes { Bow = 0, Catalyst, Claymore, Polearm, Sword }

        public string Name { get; private set; }
        public GenshinDB.ElementTypes ElementType { get; private set; }
        public List<string> TalentItem { get; private set; }
        public (WeaponTypes Type, string Name) WeaponType { get; private set; }

        public Character(DataRow dr)
        {
            Name = dr["Name"] as string;
            TalentItem = new List<string>((dr["TalentItem"] as string).Split(','));

            string weaponName = dr["WeaponType"] as string;
            WeaponType = ((WeaponTypes)Array.FindIndex(Enum.GetNames(typeof(GenshinDB.ElementTypes)), x => x.Equals(weaponName)), weaponName);

            SetElementType(dr["ElementType"] as string);
        }

        private void SetElementType(string data)
        {
            ElementType = data switch
            {
                "Pyro" => GenshinDB.ElementTypes.Pyro,
                "Hydro" => GenshinDB.ElementTypes.Hydro,
                "Dendro" => GenshinDB.ElementTypes.Dendro,
                "Electro" => GenshinDB.ElementTypes.Electro,
                "Anemo" => GenshinDB.ElementTypes.Anemo,
                "Cryo" => GenshinDB.ElementTypes.Cryo,
                "Geo" => GenshinDB.ElementTypes.Geo,
                _ => GenshinDB.ElementTypes.Pyro
            };
        }
    }
}
