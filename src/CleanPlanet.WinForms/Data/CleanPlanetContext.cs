using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using CleanPlanet.WinForms; 

namespace CleanPlanet.WinForms.Data;

public partial class CleanPlanetContext : DbContext
{
    public CleanPlanetContext()
    {
    }

    public CleanPlanetContext(DbContextOptions<CleanPlanetContext> options)
        : base(options)
    {
    }

    public virtual DbSet<labor_rates> labor_rates { get; set; }

    public virtual DbSet<labor_roles> labor_roles { get; set; }

    public virtual DbSet<material_prices> material_prices { get; set; }

    public virtual DbSet<material_types> material_types { get; set; }

    public virtual DbSet<partner_service_history> partner_service_history { get; set; }

    public virtual DbSet<partner_types> partner_types { get; set; }

    public virtual DbSet<partners> partners { get; set; }

    public virtual DbSet<service_labor_norms> service_labor_norms { get; set; }

    public virtual DbSet<service_material_norms> service_material_norms { get; set; }

    public virtual DbSet<service_types> service_types { get; set; }

    public virtual DbSet<services> services { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(AppConfig.ConnectionString);
        }
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<labor_rates>(entity =>
        {
            entity.Property(e => e.currency).HasDefaultValue("RUB");
            entity.Property(e => e.updated_at).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.labor_role).WithOne(p => p.labor_rates)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_labor_rates_roles_role_id");
        });

        modelBuilder.Entity<labor_roles>(entity =>
        {
            entity.Property(e => e.created_at).HasDefaultValueSql("(sysutcdatetime())");
        });

        modelBuilder.Entity<material_prices>(entity =>
        {
            entity.Property(e => e.currency).HasDefaultValue("RUB");
            entity.Property(e => e.updated_at).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.material_type).WithOne(p => p.material_prices).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<material_types>(entity =>
        {
            entity.Property(e => e.created_at).HasDefaultValueSql("(sysutcdatetime())");
        });

        modelBuilder.Entity<partner_service_history>(entity =>
        {
            entity.Property(e => e.created_at).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.partner).WithMany(p => p.partner_service_history)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_psh_partners_partner_id");

            entity.HasOne(d => d.service).WithMany(p => p.partner_service_history)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_psh_services_service_id");
        });

        modelBuilder.Entity<partner_types>(entity =>
        {
            entity.Property(e => e.created_at).HasDefaultValueSql("(sysutcdatetime())");
        });

        modelBuilder.Entity<partners>(entity =>
        {
            entity.HasIndex(e => e.inn, "UIX_partners_inn")
                .IsUnique()
                .HasFilter("([inn] IS NOT NULL)");

            entity.Property(e => e.created_at).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.partner_type).WithMany(p => p.partners).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<service_labor_norms>(entity =>
        {
            entity.Property(e => e.created_at).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.labor_role).WithMany(p => p.service_labor_norms)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_sln_labor_roles_role_id");

            entity.HasOne(d => d.service).WithMany(p => p.service_labor_norms)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_sln_services_service_id");
        });

        modelBuilder.Entity<service_material_norms>(entity =>
        {
            entity.Property(e => e.created_at).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.material_type).WithMany(p => p.service_material_norms)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_smn_material_types_material_type_id");

            entity.HasOne(d => d.service).WithMany(p => p.service_material_norms)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_smn_services_service_id");
        });

        modelBuilder.Entity<service_types>(entity =>
        {
            entity.Property(e => e.created_at).HasDefaultValueSql("(sysutcdatetime())");
        });

        modelBuilder.Entity<services>(entity =>
        {
            entity.Property(e => e.created_at).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.service_type).WithMany(p => p.services).OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
