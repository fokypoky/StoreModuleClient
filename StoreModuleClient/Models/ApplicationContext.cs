using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StoreModuleClient.Models;

namespace StoreModuleClient;

public partial class ApplicationContext : DbContext
{
    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<EmployeeProductCountView> EmployeeProductCountViews { get; set; }
    public virtual DbSet<ProductsView> ProductsViews { get; set; }
    public virtual DbSet<ProductManufacturerView> ProductManufacturerViews { get; set; }
    public virtual DbSet<Store> Stores { get; set; }
    public virtual DbSet<PersonPassportsByAmount> PassportsByAmounts { get; set; }

    public static string ConnectionString { get; set; } =
        "Server=DESKTOP-VJDHRQC;Database=StoreModuleDB;User id = user; Password=admin;TrustServerCertificate=True";
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConnectionString);
        optionsBuilder.LogTo(l => Debug.WriteLine(l));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PersonPassportsByAmount>().HasNoKey();
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC27C6C24355");

            entity.HasIndex(e => e.Passport, "EMPLOYEE_PASSPORT_UNIQUE_INDEX");

            entity.HasIndex(e => e.Passport, "UQ__Employee__208C1D4DB61A54DA").IsUnique();
            
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.EmployeeName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Passport)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Manufact__3214EC277A6B3B06");

            entity.HasIndex(e => new { e.Title, e.Country }, "MANUFACTURERS_TITLE_COUNTRY_UNIQUE_INDEX").IsUnique();

            entity.HasIndex(e => e.Title, "UQ__Manufact__2CB664DC2B358351").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Products__3214EC278E49C021");

            entity.ToTable(tb =>
                {
                    tb.HasTrigger("PRODUCT_ADD_TRIGGER");
                    tb.HasTrigger("PRODUCT_DELETE_TRIGGER");
                    tb.HasTrigger("PRODUCT_UPDATE_TRIGGER");
                });

            entity.HasIndex(e => e.SerialNumber, "PRODUCTS_SERIALNUMBER_UNIQUE_INDEX").IsUnique();

            entity.HasIndex(e => e.SerialNumber, "UQ__Products__048A00081FFB6B18").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ProductTitle)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SerialNumber)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Manufacturer).WithMany(p => p.Products)
                .HasForeignKey(d => d.ManufacturerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Products__Manufa__3B75D760");
        });
        modelBuilder.Entity<EmployeeProductCountView>(
            entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("Employee_product_count_view");

                entity
                    .Property(e => e.Employee)
                    .HasMaxLength(50)
                    .HasColumnName("Employee")
                    .IsUnicode(false);
                entity
                    .Property(e => e.Birthday)
                    .HasColumnName("Birthday");
                entity
                    .Property(e => e.Count)
                    .HasColumnName("Count");
            }
        );
        modelBuilder.Entity<ProductManufacturerView>(
            entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("Products_Manufacturer_VIEW");
                
                entity
                    .Property(e => e.Product)
                    .HasMaxLength(50)
                    .HasColumnName("Product")
                    .IsUnicode(false);
                entity
                    .Property(e => e.Cost)
                    .HasColumnName("Cost");
                entity
                    .Property(e => e.Manufacturer)
                    .HasMaxLength(50)
                    .HasColumnName("Manufacturer")
                    .IsUnicode(false);
            }
        );
        modelBuilder.Entity<ProductsView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Products_view");

            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DateOfUpdate).HasColumnName("Date_of_update");
            entity.Property(e => e.Manufacturer)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Product)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Serial)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Store__3214EC277C95DDA4");

            entity.ToTable("Store");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.HasOne(d => d.Employee).WithMany(p => p.Stores)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Store__EmployeeI__4222D4EF");

            entity.HasOne(d => d.Product).WithMany(p => p.Stores)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Store__ProductId__412EB0B6");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
