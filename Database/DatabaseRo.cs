using Configuration;
using System;

namespace Database
{
    public static class DatabaseRo
    {
        public static InterDatabase Inter;
        public static string ConString;

        public static void Configure(InterDatabase dbInter)
        {
            Inter = dbInter;

            ConString =
                string.Format("server={0};" +
                              "uid={1};" +
                              "password={2};" +
                              "database={3};", Inter.Server, Inter.Login, Inter.Password, Inter.Database);
        }

        public static MySqlRo Sql()
        {
            return new MySqlRo(ConString);
        }
    }
}
