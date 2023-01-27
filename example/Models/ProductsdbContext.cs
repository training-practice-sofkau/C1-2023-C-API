﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace example.Models;

public partial class ProductsdbContext : DbContext
{
    public ProductsdbContext()
    {
    }

    public ProductsdbContext(DbContextOptions<ProductsdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Product> Products { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder);
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
  //      => optionsBuilder.UseSqlServer("Server=DESKTOP-35I32D9;Database=PRODUCTSDB;Integrated Security=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PRODUCTS__3214EC0734DC9557");

            entity.ToTable("PRODUCTS");

            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
