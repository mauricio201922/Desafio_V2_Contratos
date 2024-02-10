using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using webapi.Models;

namespace webapi.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Contratos> Contratos { get; set; }
        public DbSet<Files> Files { get; set; }
    }
}
