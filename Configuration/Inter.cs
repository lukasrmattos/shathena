using System;
using System.Collections.Generic;

namespace Configuration
{
    public class InterData
    {
        public List<Inter> InterList;

    }
    public class Inter
    {
        public string Configuracao { get; set; }
        public string Nome { get; set; }
        public string Ip { get; set; }
        public int Port { get; set; }

        public InterDatabase Database { get; set; }
    }

    public class InterDatabase
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Server { get; set; }
        public string Database { get; set; }
    }
}
