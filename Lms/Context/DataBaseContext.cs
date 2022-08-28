using Lms.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lms.Context
{
    public class DataBaseContext:DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options):base(options)
        {
                
        }

        public DbSet<Course> Courses { get; set; }
    }
}
