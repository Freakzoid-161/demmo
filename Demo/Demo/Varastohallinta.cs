using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    public class Varastohallinta : DbContext
    {
        public DbSet<Tuotteet>? tuotteet { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connection = "Data Source=DEMOMAN-TF2\\MRRSQL ;" +
                "Initial Catalog=Varastohallinta;" +
                "Integrated Security=True;" +
                "MultipleActiveResultSets=True;" +
                "TrustServerCertificate=True;";
            optionsBuilder.UseSqlServer(connection);
        }
    }
}
