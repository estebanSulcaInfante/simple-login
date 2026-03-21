using Microsoft.EntityFrameworkCore;
using LoginFrontend.Models;

namespace LoginFrontend.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.TipoDocumento, e.NumeroDocumento }).IsUnique();
        });
    }
}
