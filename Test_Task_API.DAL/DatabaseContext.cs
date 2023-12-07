using Microsoft.EntityFrameworkCore;

namespace Test_Task_API.DAL
{
    public class DatabaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string ConnectionString = "Data Source=DESKTOP-TH5C59L\\SQLEXPRESS;Initial Catalog=Test_Task_API;Integrated Security=True;Trust Server Certificate=True";
            optionsBuilder.UseSqlServer(ConnectionString);
        }
        public DbSet<Tbl_User>? Tbl_Users { get; set; }
        public DbSet<Tbl_Task>? Tbl_Tasks { get; set; }
    }
}
