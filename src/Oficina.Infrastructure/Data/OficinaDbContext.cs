using Microsoft.EntityFrameworkCore;
using Oficina.Domain.Entities;

namespace Oficina.Infrastructure.Data;

public class OficinaDbContext : DbContext
{
    public OficinaDbContext(DbContextOptions<OficinaDbContext> options)
        : base(options)
    {
    }

    public DbSet<Area> Areas => Set<Area>();
    public DbSet<Empleado> Empleados => Set<Empleado>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Area>(entity =>
        {
            entity.ToTable("area");
            entity.HasKey(a => a.Id);
            entity.Property(a => a.Id).HasColumnName("ID");
            entity.Property(a => a.Description)
                  .HasColumnName("DESCRIPTION")
                  .HasMaxLength(30)
                  .IsRequired();
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.ToTable("empleado");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                  .HasColumnName("NAME")
                  .HasMaxLength(30)
                  .IsRequired();
            entity.Property(e => e.Age)
                  .HasColumnName("AGE");
            entity.Property(e => e.Email)
                  .HasColumnName("EMAIL")
                  .HasMaxLength(30)
                  .IsRequired();
            entity.Property(e => e.AreaId)
                  .HasColumnName("AREA");

            entity.HasOne(e => e.Area)
                  .WithMany(a => a.Empleados)
                  .HasForeignKey(e => e.AreaId);
        });
    }
}
