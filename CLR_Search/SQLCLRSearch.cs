using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;
namespace CLR_Search
{
    public static class SQLCLRSearch
    {
        static DataTable dt;
        static DataTable dt2;
        static DataTable dt3;
        static bool isFound1 = false;
        static bool isFound2 = false;
        static string Str1 = string.Empty;
        static string Str2 = string.Empty;

        [SqlFunction]
        public static DataTable FillData(int Number)
        {
            try
            {
                int no = Number;
                int Partition = no / 2;
                if (no <= 0 || no > 1000000)
                {
                    throw new Exception("Fill Data Can Not Be Greated in 1000000");
                }
                else
                {
                    dt = new DataTable();
                    dt.Columns.Add(new DataColumn("Id", typeof(int)));
                    dt.Columns.Add(new DataColumn("Name", typeof(string)));
                    dt2 = new DataTable();
                    dt2.Columns.Add(new DataColumn("Id", typeof(int)));
                    dt2.Columns.Add(new DataColumn("Name", typeof(string)));
                    for (int i = 1; i <= no; i++)
                    {
                        if (i <= Partition)
                        {
                            DataRow dr = dt.NewRow();
                            dr["Id"] = i;
                            dr["Name"] = Path.GetRandomFileName().Replace(".", "");
                            dt.Rows.Add(dr);
                        }
                        else
                        {
                            DataRow dr = dt2.NewRow();
                            dr["Id"] = i;
                            dr["Name"] = Path.GetRandomFileName().Replace(".", "");
                            dt2.Rows.Add(dr);
                        }
                    }
                    dt3 = new DataTable();
                    dt3 = dt.Copy();
                    dt3.Merge(dt2);
                    return dt3;
                }
            }
            catch (Exception)
            {
                throw new Exception("Please enter number between 1 to 1000000");
            }
        }
        [SqlFunction]
        public static string SearchText(string SearchText)
        {
            try
            {
                string data = string.Empty;
                isFound1 = false;
                isFound2 = false;
                Str1 = string.Empty;
                Str2 = string.Empty;
                data=Search2(SearchText);
                return data;
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }
        public static string Search2(string SearchText)
        {
            if (SearchText.Length == 0)
            {
                return "Search text can not be blank";
            }
            else if (dt.Rows.Count <= 0)
            {
                return "Please fill the data first";
            }
            else
            {
                {
                    Thread thread = new Thread(() => searchThread1(SearchText));
                    thread.Start();
                    Thread thread2 = new Thread(() => searchThread2(SearchText));
                    thread2.Start();
                    thread.Join();
                    thread2.Join();

                    if (isFound1)
                    {
                        return Str1;
                    }
                    else if (isFound2)
                    {
                        return Str2;
                    }
                    else
                    {
                       return string.Format("Text {0} Not Found", SearchText);
                    }
                }
            }
        }
        public static void searchThread1(string txtSearch)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            var results = from myRow in dt.AsEnumerable()
                          where myRow.Field<string>("Name") == txtSearch
                          select myRow;
            if (results.ToList().Count > 0)
            {
                isFound1 = true;
                watch.Stop();
                Str1 = string.Format("Text {0} Found at position {1} in ms {2}", txtSearch, results.ToList()[0].ItemArray[0].ToString(), watch.ElapsedMilliseconds);
            }
        }
        public static void searchThread2(string txtSearch)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            var results = from myRow in dt2.AsEnumerable()
                          where myRow.Field<string>("Name") == txtSearch
                          select myRow;
            if (results.ToList().Count > 0)
            {
                isFound2 = true;
                watch.Stop();
                Str2 = string.Format("Text {0} Found at position {1} in ms {2}", txtSearch, results.ToList()[0].ItemArray[0].ToString(), watch.ElapsedMilliseconds);
            }
        }
    }
}
