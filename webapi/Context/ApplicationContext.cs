using Microsoft.EntityFrameworkCore;
using webapi.Models;

namespace webapi.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
        public DbSet<Usuarios> Usuarios { get; set; }
    }
}
