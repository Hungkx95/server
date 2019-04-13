using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Server1.Models
{
    public partial class QLNVContext : DbContext
    {
        public QLNVContext()
        {
        }

        public QLNVContext(DbContextOptions<QLNVContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CaLamViec> CaLamViec { get; set; }
        public virtual DbSet<GioLamViec> GioLamViec { get; set; }
        public virtual DbSet<LoaiNhanVien> LoaiNhanVien { get; set; }
        public virtual DbSet<LoaiTaiKhoan> LoaiTaiKhoan { get; set; }
        public virtual DbSet<Nghi> Nghi { get; set; }
        public virtual DbSet<NhanVien> NhanVien { get; set; }
        public virtual DbSet<PhongBan> PhongBan { get; set; }
        public virtual DbSet<Taikhoan> Taikhoan { get; set; }
        public virtual DbSet<ViTri> ViTri { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=AITD201904013\\SQLEXPRESS;Database=QLNV;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CaLamViec>(entity =>
            {
                entity.HasKey(e => e.MaCa);

                entity.Property(e => e.MaCa).ValueGeneratedNever();

                entity.Property(e => e.GioKetThuc).HasColumnType("time(6)");

                entity.Property(e => e.GiobatDau).HasColumnType("time(6)");

                entity.Property(e => e.Ten).HasMaxLength(50);
            });

            modelBuilder.Entity<GioLamViec>(entity =>
            {
                entity.HasKey(e => new { e.Idnv, e.MaCa, e.Ngay });

                entity.Property(e => e.Idnv)
                    .HasColumnName("IDnv")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Ngay).HasColumnType("date");

                entity.Property(e => e.GioBatDau)
                    .HasColumnName("gioBatDau")
                    .HasColumnType("time(6)");

                entity.Property(e => e.GioKetThuc)
                    .HasColumnName("gioKetThuc")
                    .HasColumnType("time(6)");

                entity.HasOne(d => d.IdnvNavigation)
                    .WithMany(p => p.GioLamViec)
                    .HasForeignKey(d => d.Idnv)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GioLamViec__IDnv__09A971A2");

                entity.HasOne(d => d.MaCaNavigation)
                    .WithMany(p => p.GioLamViec)
                    .HasForeignKey(d => d.MaCa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GioLamViec__MaCa__0A9D95DB");
            });

            modelBuilder.Entity<LoaiNhanVien>(entity =>
            {
                entity.HasKey(e => e.MaLoai);

                entity.Property(e => e.MaLoai)
                    .HasColumnName("maLoai")
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.TenLoai).HasMaxLength(50);
            });

            modelBuilder.Entity<LoaiTaiKhoan>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Tenloai).HasMaxLength(50);
            });

            modelBuilder.Entity<Nghi>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasDefaultValueSql("([DBO].[AUTO_Nghi]())");

                entity.Property(e => e.IdNv)
                    .HasColumnName("IdNV")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.NgayNghi).HasColumnType("date");

                entity.HasOne(d => d.IdNvNavigation)
                    .WithMany(p => p.Nghi)
                    .HasForeignKey(d => d.IdNv)
                    .HasConstraintName("FK__Nghi__IdNV__534D60F1");
            });

            modelBuilder.Entity<NhanVien>(entity =>
            {
                entity.HasKey(e => e.Idnv);

                entity.Property(e => e.Idnv)
                    .HasColumnName("IDNV")
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasDefaultValueSql("([DBO].[AUTO_IDNV]())");

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.Brithday).HasColumnType("date");

                entity.Property(e => e.Cmnd)
                    .HasColumnName("CMND")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.MaThue)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(13);

                entity.HasOne(d => d.CaLamNavigation)
                    .WithMany(p => p.NhanVien)
                    .HasForeignKey(d => d.CaLam)
                    .HasConstraintName("FK__NhanVien__CaLam__4F7CD00D");
            });

            modelBuilder.Entity<PhongBan>(entity =>
            {
                entity.HasKey(e => e.Idpb);

                entity.Property(e => e.Idpb)
                    .HasColumnName("IDPB")
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasDefaultValueSql("([DBO].[AUTO_IDPB]())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Taikhoan>(entity =>
            {
                entity.HasKey(e => e.Username);

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.IdNv)
                    .HasColumnName("idNV")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.LoaiTk)
                    .HasColumnName("LoaiTK")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Passwork)
                    .HasColumnName("passwork")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdNvNavigation)
                    .WithMany(p => p.Taikhoan)
                    .HasForeignKey(d => d.IdNv)
                    .HasConstraintName("FK__Taikhoan__idNV__5812160E");

                entity.HasOne(d => d.LoaiTkNavigation)
                    .WithMany(p => p.Taikhoan)
                    .HasForeignKey(d => d.LoaiTk)
                    .HasConstraintName("FK__Taikhoan__LoaiTK__59063A47");
            });

            modelBuilder.Entity<ViTri>(entity =>
            {
                entity.HasKey(e => new { e.IdPb, e.IdNv });

                entity.Property(e => e.IdPb)
                    .HasColumnName("IdPB")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.IdNv)
                    .HasColumnName("IdNV")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.MaLoai)
                    .IsRequired()
                    .HasColumnName("maLoai")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdNvNavigation)
                    .WithMany(p => p.ViTri)
                    .HasForeignKey(d => d.IdNv)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ViTri__IdNV__73BA3083");

                entity.HasOne(d => d.IdPbNavigation)
                    .WithMany(p => p.ViTri)
                    .HasForeignKey(d => d.IdPb)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ViTri__IdPB__72C60C4A");

                entity.HasOne(d => d.MaLoaiNavigation)
                    .WithMany(p => p.ViTri)
                    .HasForeignKey(d => d.MaLoai)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ViTri__maLoai__71D1E811");
            });
        }
    }
}
