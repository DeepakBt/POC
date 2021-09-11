using System;
using System.Data;
using System.Data.SqlClient;
namespace TableValuedProc
{
    class Program
    {
        static void Main(string[] args)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(Int32));
            dt.Columns.Add("City", typeof(string));
            dt.Columns.Add("State", typeof(string));
            dt.Columns.Add("Country", typeof(string));

            dt.Rows.Add(1, "Mumbai","Maharashtra","India");
            dt.Rows.Add(2, "Manhattan", "NYC", "US");

            SqlConnection connection = new SqlConnection("Data Source= DESKTOP-MRNOIMN;Initial Catalog=TestDB;Integrated Security=true");
            connection.Open();
            SqlCommand cmd = new SqlCommand("USP_COUNTRY", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sqlParam = cmd.Parameters.AddWithValue("@MyData", dt);
            sqlParam.SqlDbType = SqlDbType.Structured;
            SqlParameter sqlP2 = cmd.Parameters.AddWithValue("@SecParam","Yes");
            sqlP2.SqlDbType = SqlDbType.VarChar;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);
            connection.Close();
            Console.WriteLine("Value from Table 1 are below");
            Console.WriteLine("ID\tCity\t\tState\t\tCountry");
            foreach(DataRow dr in ds.Tables[0].Rows)
            {
                Console.WriteLine($"{dr[0].ToString()}\t{dr[1].ToString()}\t\t{dr[2].ToString()}\t{dr[3].ToString()}");
            }
            Console.WriteLine("Value from Table 2 are below");
            foreach(DataRow dr in ds.Tables[1].Rows)
            {
                Console.WriteLine($"{dr[0].ToString()}");
            }
            Console.ReadLine();
        }
    }
}
