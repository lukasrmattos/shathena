using EnumStringValues;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared
{
    public enum ShowType
    {
        Info,
        Status,
        Error,
        Warn,
        Exception,
        Debug
    }

    public enum QueryType
    {
        SELECT,
        COUNT,
        INSERT,
        UPDATE,
        DELETE
    }

    public enum ServerType
    {
        ALL,

        [StringValue("Login-Server")]
        LOGIN,

        [StringValue("Char-Server")]
        CHAR,

        [StringValue("Map-Server")]
        MAP
    }

    public enum ServerPort
    {
        LoginPort = 6900,
        CharPort = 6121,
        MapPort = 5121
    }
}
