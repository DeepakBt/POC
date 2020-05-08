using Microsoft.SqlServer.Server;
using System;

namespace MSSQLCORECLR
{
    public class CoreCLR
    {
        [SqlFunction]
        public string MyUpperClr(string str)
        {
            return str.ToUpper();
        }
    }
}
