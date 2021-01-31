using System;

using GenshinDB_Core;

namespace GenshinDB_Core_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Enum.GetValues(typeof(GenshinDB.Locations)).GetValue(0));
        }
    }
}
