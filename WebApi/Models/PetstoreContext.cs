using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Models;

public partial class PetstoreContext : DbContext
{
    public PetstoreContext()
    {
    }

    public PetstoreContext(DbContextOptions<PetstoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Pet> Pets { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=136503-LHQUAN;Database=petstore;Trusted_Connection=true;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("category");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("text")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("order");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Complete).HasColumnName("complete");
            entity.Property(e => e.PetId).HasColumnName("petId");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.ShipDate)
                .HasColumnType("datetime")
                .HasColumnName("shipDate");
            entity.Property(e => e.Status)
                .HasColumnType("text")
                .HasColumnName("status");

            entity.HasOne(d => d.Pet).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PetId)
                .HasConstraintName("FK_order_pet");
        });

        modelBuilder.Entity<Pet>(entity =>
        {
            entity.ToTable("pet");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CategoryId).HasColumnName("categoryId");
            entity.Property(e => e.Name)
                .HasColumnType("text")
                .HasColumnName("name");
            entity.Property(e => e.Status)
                .HasColumnType("text")
                .HasColumnName("status");

            entity.HasOne(d => d.Category).WithMany(p => p.Pets)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_pet_category");

            entity.HasMany(d => d.Tags).WithMany(p => p.Pets)
                .UsingEntity<Dictionary<string, object>>(
                    "PetTag",
                    r => r.HasOne<Tag>().WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_pet_tag_tag"),
                    l => l.HasOne<Pet>().WithMany()
                        .HasForeignKey("PetId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_pet_tag_pet"),
                    j =>
                    {
                        j.HasKey("PetId", "TagId");
                        j.ToTable("pet_tag");
                        j.IndexerProperty<int>("PetId").HasColumnName("petId");
                        j.IndexerProperty<int>("TagId").HasColumnName("tagId");
                    });
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.ToTable("tag");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("text")
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("user");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasColumnType("text")
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasColumnType("text")
                .HasColumnName("firstName");
            entity.Property(e => e.LastName)
                .HasColumnType("text")
                .HasColumnName("lastName");
            entity.Property(e => e.Password)
                .HasColumnType("text")
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasColumnType("text")
                .HasColumnName("phone");
            entity.Property(e => e.UserStatus)
                .HasColumnType("text")
                .HasColumnName("userStatus");
            entity.Property(e => e.Username)
                .HasColumnType("text")
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
