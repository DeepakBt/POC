using MongoDBBulkInsert.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
namespace MongoDBBulkInsert
{
    internal class Program 
    {
        static void Main(string[] args)
        {
            List<NotesLog> LstLogs = new List<NotesLog>();
             Console.WriteLine("Sql Server EntityFM To MongoDB");
            using (var context = new SQLDBContext())
            {
                LstLogs = context.NotesLogs.ToList();

                foreach (var Log in LstLogs)
                {
                    Console.WriteLine($"LogId: {Log.ID}");
                }
            }
            Program ObjP = new Program();
            ObjP.MongoInsert(LstLogs);
            Console.ReadKey();
        }
        public string MongoInsert(List<NotesLog> Logs)
        {
            string StrData = "";
            var connectionString = "mongodb://localhost:27017";
            var databaseName = "MyLogs";
            var collectionName = "LogsCollection";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            var collection = database.GetCollection<NotesLog>(collectionName);

            var writeModels = new List<WriteModel<NotesLog>>();

            foreach (var log in Logs)
            {
                writeModels.Add(new InsertOneModel<NotesLog>(log));
            }

            var result = collection.BulkWrite(writeModels);
            return StrData;
        }
    }
    
}
