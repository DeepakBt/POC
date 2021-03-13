using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;

namespace SignalRDB
{
    public class MyRep: IMyRep
    {
        private readonly IHubContext<SignalServer> _context;
        string connectionString = "";
        public MyRep(IConfiguration configuration, IHubContext<SignalServer> context)
        {
            connectionString = "Server=DESKTOP-MRNOIMN;Database =TestDB; Integrated Security = true;";
            _context = context;
        }

        public void MyNotificaton()
        {
            try
            {
                MyData ObjC = new MyData();
                List<MyData> ObjData = new List<MyData>();
                ObjData = GetAllEmployees();
                 _context.Clients.All.SendAsync("transferchartdata", ObjData);
            }
            catch (Exception ex)
            {
                 _context.Clients.All.SendAsync("transferchartdata", ex.Message);
                throw ex;
            }

        }

        public List<MyData> GetAllEmployees()
        {
            var employees = new List<MyData>();

            using(SqlConnection conn = new SqlConnection(connectionString)){
                conn.Open();

                SqlDependency.Start(connectionString);

                string commandText = "Select ID,Name from DBO.TBL_CLIENT";

                SqlCommand cmd = new SqlCommand(commandText,conn);

                SqlDependency dependency = new SqlDependency(cmd);

                dependency.OnChange+=new OnChangeEventHandler(dbChangeNotification);

                var reader = cmd.ExecuteReader();

                while(reader.Read()){
                    var employee = new MyData
                    {
                        Id = Convert.ToInt32(reader["ID"]),
                        Name = reader["Name"].ToString()
                    };
                    employees.Add(employee);
                }
            }
            return employees;
        }
        private void dbChangeNotification(object sender, SqlNotificationEventArgs e)
        {
            MyNotificaton();
        }
    }

    public class MyData
    {
        public int Count { get; set; }
        public string Test { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
    }
}
