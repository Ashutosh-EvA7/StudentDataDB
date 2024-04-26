using Microsoft.EntityFrameworkCore;
using StudentDataDB.Models;

namespace StudentDataDB.Data
{
    public class StudentDbContext(DbContextOptions dbContextOptions) : DbContext(dbContextOptions)
    {
        public DbSet<Models.Student> Students { get; set; }
        public DbSet<Marksheet> Marksheets { get; set; }
    }
}