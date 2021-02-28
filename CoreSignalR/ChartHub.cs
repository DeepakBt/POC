using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSignalR
{
    public class ChartHub : Hub
    {
        string connString = "Server=DESKTOP-MRNOIMN;Database =TestDB; Integrated Security = true;";
        //SqlDependency.Start(connString);
        //public ChartHub(){
        //    SqlDependency.Start(connString);
        //}
        public static int count { get; set; } =0;
        public async Task MyNotificaton()
        {
            try
            {
                count = count + 1;
                MyData ObjC = new MyData();
                ObjC.Count = count;
                ObjC.Test = "Test No " + count.ToString();
                List<MyData> ObjData = new List<MyData>();
                ObjData = GetAllMessages();
                //await Clients.All.SendAsync("transferchartdata", DataManager.GetData());

                await Clients.All.SendAsync("transferchartdata", ObjData);
            }
            catch (Exception ex)
            {
                await Clients.All.SendAsync("transferchartdata", ex.Message);
                throw ex;
            }
           
        }
        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                ChartHub ObjHub = new ChartHub();
                ObjHub.MyNotificaton();
            }
        }
        public List<MyData> GetAllMessages()
        {
            var messages = new List<MyData>();
            //string connString = "Server=DESKTOP-MRNOIMN;Database =TestDB; Integrated Security = true;";
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                using (var command = new SqlCommand("TestClient", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Notification = null;

                    var dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        MyData MyMsg = new MyData();
                        MyMsg.Address1 = reader["Address1"].ToString();
                        MyMsg.Name = reader["Name"].ToString();
                        MyMsg.Id = (int)reader["Id"];
                        messages.Add(MyMsg);
                    }
                }
            }
            return messages;
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
