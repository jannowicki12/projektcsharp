using aplikacja.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace aplikacja.Data
{
    public class CrudDbContext : DbContext
    {
        public CrudDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
    }
}
