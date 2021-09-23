using System.Data;
using System.Globalization;
using System.Linq;

namespace GenshinDB_Core
{
    public static class DBEnvironment
    {
        public static CultureInfo dbCultureInfo;

        public static DataRow FindDataRow<T>(DataTable dt, string columnIndex, T value)
        {
            return (from DataRow dr in dt.Rows
                    where ((T)dr[columnIndex]).Equals(value)
                    select dr).First();
        }
    }
}
