using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MongoDBBulkInsert.Models;
namespace MongoDBBulkInsert
{
    public class SQLDBContext : DbContext
    {
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=Deepak\MSSQLSERVERNEW;Database=TESTDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");
        }
        public DbSet<NotesLog> NotesLogs { get; set; }
    }
}
