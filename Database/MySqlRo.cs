using MySql.Data.MySqlClient;
using SqlKata.Compilers;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database
{
    public class MySqlRo : IDisposable
    {
        public MySqlConnection Con;
        public MySqlCompiler Compiler = new MySqlCompiler();

        public MySqlRo(string connection)
        {
            Con = new MySqlConnection(connection);
            Con.Open();
        }

        public QueryFactory NewQuery()
        {
            return new QueryFactory(Con, Compiler);
        }

        public void Close()
        {
            if (Con != null)
                Con.Close();
        }

        public void Dispose()
        {
            Close();
            return;
        }
    }
}
