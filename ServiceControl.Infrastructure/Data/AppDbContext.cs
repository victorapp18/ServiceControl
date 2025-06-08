using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using ServiceControl.Domain.Entities;
using System.Collections.Generic;

namespace ServiceControl.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Registro> Registros { get; set; }
    }
}
