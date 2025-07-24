using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TestPHE.Models;

public partial class DbBarangContext : DbContext
{
    public DbBarangContext()
    {
    }

    public DbBarangContext(DbContextOptions<DbBarangContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblBarang> TblBarangs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=DB_BARANG;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblBarang>(entity =>
        {
            entity.ToTable("TBL_BARANG");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Harga)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("HARGA");
            entity.Property(e => e.Nama)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("NAMA");
            entity.Property(e => e.Stok).HasColumnName("STOK");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
