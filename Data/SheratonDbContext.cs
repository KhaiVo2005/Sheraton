using Microsoft.EntityFrameworkCore;
using Sheraton.Models;

namespace Sheraton.Data
{
    public class SheratonDbContext: DbContext
    {
        public SheratonDbContext(DbContextOptions<SheratonDbContext> options) : base(options)
        {
        }
        public DbSet<Models.HoaDon> HoaDons { get; set; }
        public DbSet<Models.NhanVien> NhanViens { get; set; }
        public DbSet<Models.HopDong> HopDongs { get; set; }
        public DbSet<Models.KhachHang> KhachHangs { get; set; }
        public DbSet<Models.DatHang> DatHangs { get; set; }
        public DbSet<Models.LichDatSanh> LichDatSanhs { get; set; }
        public DbSet<Models.ChiTietDatTiec> ChiTietDatTiecs { get; set; }
        public DbSet<Models.ChiTietDichVu> ChiTietDichVus { get; set; }
        public DbSet<Models.PhanCong> PhanCongs { get; set; }
        public DbSet<Models.SanhTiec> SanhTiecs { get; set; }
        public DbSet<Models.DichVu> DichVus { get; set; }
        public DbSet<Models.MonAn> MonAns { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // HoaDon -> HopDong
            modelBuilder.Entity<HoaDon>()
                .HasOne(h => h.HopDong)
                .WithMany(hd => hd.HoaDons)
                .HasForeignKey(h => h.MaHopDong)
                .OnDelete(DeleteBehavior.Restrict);

            // HoaDon -> DonHang
            modelBuilder.Entity<HoaDon>()
                .HasOne(h => h.DatHang)
                .WithMany(dh => dh.HoaDons)
                .HasForeignKey(h => h.MaDH)
                .OnDelete(DeleteBehavior.Restrict);

            // HoaDon -> NhanVien
            modelBuilder.Entity<HoaDon>()
                .HasOne(h => h.NhanVien)
                .WithMany(nv => nv.HoaDons)
                .HasForeignKey(h => h.MaNV)
                .OnDelete(DeleteBehavior.Restrict);

            // ChiTietDichVu -> HopDong
            modelBuilder.Entity<ChiTietDichVu>()
                .HasOne(ct => ct.HopDong)
                .WithMany(hd => hd.ChiTietDichVus)
                .HasForeignKey(ct => ct.MaHD)
                .OnDelete(DeleteBehavior.Restrict);

            // ChiTietDichVu -> DichVu
            modelBuilder.Entity<ChiTietDichVu>()
                .HasOne(ct => ct.DichVu)
                .WithMany(dv => dv.ChiTietDichVus)
                .HasForeignKey(ct => ct.MaDV)
                .OnDelete(DeleteBehavior.Restrict);

            // HopDong -> NhanVien
            modelBuilder.Entity<HopDong>()
                .HasOne(hd => hd.NhanVien)
                .WithMany(nv => nv.HopDongs)
                .HasForeignKey(hd => hd.MaNV)
                .OnDelete(DeleteBehavior.Restrict);

            // HopDong -> KhachHang
            modelBuilder.Entity<HopDong>()
                .HasOne(hd => hd.KhachHang)
                .WithMany(kh => kh.HopDongs)
                .HasForeignKey(hd => hd.MaKH)
                .OnDelete(DeleteBehavior.Restrict);

            // LichDatSanh -> HopDong
            modelBuilder.Entity<LichDatSanh>()
                .HasOne(lds => lds.HopDong)
                .WithMany(hd => hd.LichDatSanhs)
                .HasForeignKey(lds => lds.MaHD)
                .OnDelete(DeleteBehavior.Restrict);

            // LichDatSanh -> SanhTiec
            modelBuilder.Entity<LichDatSanh>()
                .HasOne(lds => lds.Sanh)
                .WithMany(st => st.LichDatSanhs)
                .HasForeignKey(lds => lds.MaSanh)
                .OnDelete(DeleteBehavior.Restrict);

            // PhanCong -> NhanVien
            modelBuilder.Entity<PhanCong>()
                .HasOne(pc => pc.NhanVien)
                .WithMany(nv => nv.PhanCongs)
                .HasForeignKey(pc => pc.MaNV)
                .OnDelete(DeleteBehavior.Restrict);

            // PhanCong -> LichDatSanh
            modelBuilder.Entity<PhanCong>()
                .HasOne(pc => pc.LichDatSanh)
                .WithMany(lds => lds.PhanCongs)
                .HasForeignKey(pc => pc.MaLDS)
                .OnDelete(DeleteBehavior.Restrict);

            // Khai báo các entity không có khóa chính nếu cần
            modelBuilder.Entity<ChiTietDichVu>()
                .HasKey(ct => new { ct.MaHD, ct.MaDV });
            modelBuilder.Entity<ChiTietDatTiec>()
                .HasKey(ct => new { ct.MaHD, ct.MaMon });
            modelBuilder.Entity<PhanCong>()
                .HasKey(ct => new { ct.MaLDS, ct.MaNV });

            // Precision cho các decimal
            modelBuilder.Entity<DichVu>().Property(dv => dv.DonGia).HasPrecision(18, 2);
            modelBuilder.Entity<HopDong>().Property(hd => hd.TienCoc).HasPrecision(18, 2);
            modelBuilder.Entity<MonAn>().Property(ma => ma.DonGia).HasPrecision(18, 2);
            modelBuilder.Entity<SanhTiec>().Property(st => st.Gia).HasPrecision(18, 2);


        }

    }
}
