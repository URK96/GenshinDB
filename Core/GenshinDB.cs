using GenshinDB_Core.Types;

using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

using DBEnv = GenshinDB_Core.DBEnvironment;

namespace GenshinDB_Core
{
    public class GenshinDB
    {
        public enum Locations { Mondstadt = 0, Liyue, Inazuma, Sumeru }
        public enum ElementTypes { Pyro = 0, Hydro, Dendro, Electro, Anemo, Cryo, Geo }


        const string EMBED_NAMESPACE = "GenshinDB_Core.DB.";

        public List<Character> characters;
        public List<TalentItem> talentItems;
        public List<WeaponAscensionItem> weaponAscensionItems;
        public List<Lang> langs;

        public DataTable langDT;

        public GenshinDB(CultureInfo cultureInfo = null)
        {
            DBEnv.dbCultureInfo = cultureInfo ?? CultureInfo.CurrentCulture;

            characters = new List<Character>();
            talentItems = new List<TalentItem>();
            weaponAscensionItems = new List<WeaponAscensionItem>();
            langs = new List<Lang>();

            LoadDB();
        }

        private void LoadDB()
        {
            using DataTable characterDT = new DataTable();
            using DataTable talentItemDT = new DataTable();
            using DataTable weaponAscensionDT = new DataTable();
            using DataTable langDT = new DataTable();

            var assembly = Assembly.GetExecutingAssembly();

            using Stream characterStream = assembly.GetManifestResourceStream($"{EMBED_NAMESPACE}Character.xml");
            using Stream talentItemStream = assembly.GetManifestResourceStream($"{EMBED_NAMESPACE}Item_Talent.xml");
            using Stream weaponAscensionStream = assembly.GetManifestResourceStream($"{EMBED_NAMESPACE}Item_Weapon_Ascension.xml");
            using Stream langStream = assembly.GetManifestResourceStream($"{EMBED_NAMESPACE}Lang.xml");

            characterDT.ReadXml(characterStream);
            talentItemDT.ReadXml(talentItemStream);
            weaponAscensionDT.ReadXml(weaponAscensionStream);
            langDT.ReadXml(langStream);

            foreach (DataRow dr in characterDT.Rows)
            {
                characters.Add(new Character(dr));
            }

            foreach (DataRow dr in talentItemDT.Rows)
            {
                talentItems.Add(new TalentItem(dr));
            }
            foreach (DataRow dr in weaponAscensionDT.Rows)
            {
                weaponAscensionItems.Add(new WeaponAscensionItem(dr));
            }
            foreach (Locations location in Enum.GetValues(typeof(Locations)))
            {
                talentItems.Add(new TalentItem(location, new[] { DayOfWeek.Sunday }));
                weaponAscensionItems.Add(new WeaponAscensionItem(location, new[] { DayOfWeek.Sunday }));
            }

            foreach (DataRow dr in langDT.Rows)
            {
                langs.Add(new Lang(dr));
            }
        }

        public static string[] GetResources()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            return assembly.GetManifestResourceNames();
        }

        public string GetLocationName(Locations location) =>
            location switch
            {
                Locations.Mondstadt => "Mondstadt",
                Locations.Liyue => "Liyue",
                Locations.Inazuma => "Inazuma",
                Locations.Sumeru => "Sumeru",
                _ => string.Empty
            };

        public List<string> GetAllLocations()
        {
            string langIndex = GetLangIndex();
            List<string> locationNames = new();

            foreach (Locations location in Enum.GetValues(typeof(Locations)))
            {
                locationNames.Add((from lang in langs
                                   where lang.Name == GetLocationName(location)
                                   select lang).First().Dic[langIndex]);
            }

            return locationNames;

            //return new List<string>
            //{
            //    (from lang in langs where lang.Name.Equals("Mondstadt") select lang).First().Dic[langIndex],
            //    (from lang in langs where lang.Name.Equals("Liyue") select lang).First().Dic[langIndex],
            //    (from lang in langs where lang.Name.Equals("Inazuma") select lang).First().Dic[langIndex]
            //};
        }

        public string FindLangDic(string name)
        {
            string result = string.Empty;
            Dictionary<string, string> langDic = langs.Find(x => x.Name.Equals(name))?.Dic;

            if (langDic is not null)
            {
                result = langDic[GetLangIndex()];
            }
            else
            {
                result = name;
            }

            return result;
        }

        private string GetLangIndex() => 
            DBEnv.dbCultureInfo.Name switch
            {
                "ko-KR" => "ko",
                _ => "Default"
            };
    }
}
