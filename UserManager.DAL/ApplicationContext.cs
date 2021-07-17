using Microsoft.EntityFrameworkCore;
using System;
using UserManager.DAL.Entities;

namespace UserManager.DAL
{
    public class ApplicationContext : DbContext
    {
        public DbSet<UserInfo> UserInfo { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;user=root;password=1234;database=new_schema;",
                new MySqlServerVersion(new Version(8, 0, 25)));
        }
    }
}
