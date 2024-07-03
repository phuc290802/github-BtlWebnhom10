using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


namespace TestWeb.Models;

public partial class QuanLiBanSachContext : DbContext
{
    public QuanLiBanSachContext()
    {
    }

    public QuanLiBanSachContext(DbContextOptions<QuanLiBanSachContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }

    public virtual DbSet<ChuDe> ChuDes { get; set; }

    public virtual DbSet<DonHang> DonHangs { get; set; }

    public virtual DbSet<KhachHang> KhachHangs { get; set; }

    public virtual DbSet<NhaXuatBan> NhaXuatBans { get; set; }

    public virtual DbSet<NhanVien> NhanViens { get; set; }

    public virtual DbSet<Sach> Saches { get; set; }

    public virtual DbSet<TacGium> TacGia { get; set; }

    public virtual DbSet<ThamGium> ThamGia { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-P1V09R1\\SQLEXPRESS;Initial Catalog=QuanLiBanSach;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChiTietDonHang>(entity =>
        {
            entity.HasKey(e => new { e.MaDonHang, e.MaSach });

            entity.ToTable("ChiTietDonHang");

            entity.Property(e => e.DonGia)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        modelBuilder.Entity<ChuDe>(entity =>
        {
            entity.HasKey(e => e.MaChuDe);

            entity.ToTable("ChuDe");

            entity.Property(e => e.TenChuDe).HasMaxLength(50);
        });

        modelBuilder.Entity<DonHang>(entity =>
        {
            entity.HasKey(e => new { e.MaDonHang, e.MaKh, e.MaNhanVien }).HasName("PK_DonHang_1");

            entity.ToTable("DonHang");

            entity.HasIndex(e => e.MaDonHang, "IX_DonHang");

            entity.HasIndex(e => e.MaDonHang, "IX_DonHang_1");

            entity.Property(e => e.MaDonHang).ValueGeneratedOnAdd();
            entity.Property(e => e.MaKh).HasColumnName("MaKH");
            entity.Property(e => e.NgayDat).HasColumnType("datetime");
            entity.Property(e => e.NgayGiao).HasColumnType("datetime");

            entity.HasOne(d => d.MaKhNavigation).WithMany(p => p.DonHangs)
                .HasForeignKey(d => d.MaKh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DonHang_KhachHang");
        });

        modelBuilder.Entity<KhachHang>(entity =>
        {
            entity.HasKey(e => e.MaKh);

            entity.ToTable("KhachHang");

            entity.Property(e => e.MaKh).HasColumnName("MaKH");
            entity.Property(e => e.DiaChi).HasMaxLength(100);
            entity.Property(e => e.DienThoai)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.GioiTinh)
                .HasMaxLength(3)
                .IsFixedLength();
            entity.Property(e => e.MatKhau).HasMaxLength(50);
            entity.Property(e => e.NgaySinh).HasColumnType("datetime");
            entity.Property(e => e.TaiKhoan)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<NhaXuatBan>(entity =>
        {
            entity.HasKey(e => e.MaNxb);

            entity.ToTable("NhaXuatBan");

            entity.Property(e => e.MaNxb).HasColumnName("MaNXB");
            entity.Property(e => e.DiaChi).HasMaxLength(200);
            entity.Property(e => e.DienThoai).HasMaxLength(50);
            entity.Property(e => e.TenNxb)
                .HasMaxLength(50)
                .HasColumnName("TenNXB");
        });

        modelBuilder.Entity<NhanVien>(entity =>
        {
            entity.HasKey(e => e.MaNhanVien);

            entity.ToTable("NhanVien");

            entity.Property(e => e.MaNhanVien).ValueGeneratedNever();
            entity.Property(e => e.AnhDaiDien)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ChucVu).HasMaxLength(100);
            entity.Property(e => e.DiaChi).HasMaxLength(150);
            entity.Property(e => e.NgaySinh).HasColumnType("date");
            entity.Property(e => e.SoDienThoai)
                .HasMaxLength(15)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.TenNhanVien).HasMaxLength(100);
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("username");
        });

        modelBuilder.Entity<Sach>(entity =>
        {
            entity.HasKey(e => e.MaSach);

            entity.ToTable("Sach");

            entity.Property(e => e.AnhBia).HasMaxLength(50);
            entity.Property(e => e.GiaBan).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.MaNxb).HasColumnName("MaNXB");
            entity.Property(e => e.NgayCapNhat).HasColumnType("datetime");
            entity.Property(e => e.TenChuDe).HasMaxLength(50);
            entity.Property(e => e.TenSach).HasMaxLength(50);

            entity.HasOne(d => d.MaChuDeNavigation).WithMany(p => p.Saches)
                .HasForeignKey(d => d.MaChuDe)
                .HasConstraintName("FK_Sach_ChuDe");

            entity.HasOne(d => d.MaNxbNavigation).WithMany(p => p.Saches)
                .HasForeignKey(d => d.MaNxb)
                .HasConstraintName("FK_Sach_NhaXuatBan1");
        });

        modelBuilder.Entity<TacGium>(entity =>
        {
            entity.HasKey(e => e.MaTacGia);

            entity.Property(e => e.DiaChi).HasMaxLength(200);
            entity.Property(e => e.DienThoai)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TenTacGia).HasMaxLength(50);
        });

        modelBuilder.Entity<ThamGium>(entity =>
        {
            entity.HasKey(e => new { e.MaSach, e.MaTacGia });

            entity.Property(e => e.VaiTro).HasMaxLength(50);
            entity.Property(e => e.ViTri).HasMaxLength(50);

            entity.HasOne(d => d.MaTacGiaNavigation).WithMany(p => p.ThamGia)
                .HasForeignKey(d => d.MaTacGia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ThamGia_TacGia");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
