using Harmonia.API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Harmonia.API.Context;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Instrumento> Instrumentos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Instrumento>().HasKey(i => i.InstrumentoId);
        modelBuilder.Entity<Instrumento>().Property(i => i.Nome).HasMaxLength(50).IsRequired();
        modelBuilder.Entity<Instrumento>().Property(i => i.Marca).HasMaxLength(20).IsRequired();
        modelBuilder.Entity<Instrumento>().Property(i => i.Modelo).HasMaxLength(20).IsRequired();
        modelBuilder.Entity<Instrumento>().Property(i => i.Descricao).HasMaxLength(300);
        modelBuilder.Entity<Instrumento>().Property(i => i.Cor).HasMaxLength(20);
        modelBuilder.Entity<Instrumento>().Property(i => i.Material).HasMaxLength(20);
        modelBuilder.Entity<Instrumento>().Property(i => i.AnoFabricacao);

        modelBuilder.Entity<Categoria>().HasKey(c => c.CategoriaId);
        modelBuilder.Entity<Categoria>().Property(c => c.Nome).HasMaxLength(30).IsRequired();
        modelBuilder.Entity<Categoria>().Property(c => c.Descricao).HasMaxLength(300);

        modelBuilder.Entity<Categoria>()
            .HasMany(c => c.Instrumentos)
            .WithOne(i => i.Categoria)
            .HasForeignKey(i => i.CategoriaId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
