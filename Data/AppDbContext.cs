using Microsoft.EntityFrameworkCore;
using Sistema_Contactos.Models;

namespace Sistema_Contactos.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Contacto> Contactos { get; set; }
}
