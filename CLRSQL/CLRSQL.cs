using Microsoft.SqlServer.Server;
namespace CLRSQL
{
    
    public static class CLRSQL
    {
        [SqlFunction]
        public static string MyUpperClr(string str)
        {
            return str.ToUpper();
        }
    }
}
