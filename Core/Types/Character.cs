using System;
using System.Collections.Generic;
using System.Data;

namespace GenshinDB_Core.Types
{
    public class Character
    {
        public enum ElementTypes { Pyro, Hydro, Dendro, Electro, Anemo, Cryo, Geo }
        public enum WeaponTypes { Bow, Catalyst, Claymore, Polearm, Sword }

        public string Name { get; private set; }
        public ElementTypes ElementType { get; private set; }
        public List<string> TalentItem { get; private set; }
        public (WeaponTypes Type, string Name) WeaponType { get; private set; }

        public Character(DataRow dr)
        {
            Name = dr["Name"] as string;
            TalentItem = new List<string>((dr["TalentItem"] as string).Split(','));

            string weaponName = dr["WeaponType"] as string;
            WeaponType = ((WeaponTypes)Array.FindIndex(Enum.GetNames(typeof(ElementTypes)), x => x.Equals(weaponName)), weaponName);

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
                _ => ElementTypes.Pyro
            };
        }
    }
}
