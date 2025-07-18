using Microsoft.EntityFrameworkCore;
using Sheraton.Models;

namespace Sheraton.Data
{
    public class SheratonDbContext: DbContext
    {
        public SheratonDbContext(DbContextOptions<SheratonDbContext> options) : base(options)
        {
        }
        public DbSet<Models.NhanVien> NhanViens { get; set; }
        public DbSet<Models.HopDong> HopDongs { get; set; }
        public DbSet<Models.KhachHangg> KhachHangs { get; set; }
        public DbSet<Models.DatHang> DatHangs { get; set; }
        public DbSet<Models.LichDatSanh> LichDatSanhs { get; set; }
        public DbSet<Models.ChiTietDatTiec> ChiTietDatTiecs { get; set; }
        public DbSet<Models.PhanCong> PhanCongs { get; set; }
        public DbSet<Models.SanhTiec> SanhTiecs { get; set; }
        public DbSet<Models.DichVu> DichVus { get; set; }
        public DbSet<Models.MonAn> MonAns { get; set; }
        public DbSet<Models.BangLuong> BangLuongs { get; set; }
        public DbSet<Models.Rating> Ratings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BangLuong>()
                .HasOne(bl => bl.NhanVien)
                .WithMany(nv => nv.BangLuongs)
                .HasForeignKey(bl => bl.MaNV);
            // ChiTietDatTiec -> MonAn
            modelBuilder.Entity<ChiTietDatTiec>()
                .HasOne(ct => ct.MonAn)
                .WithMany(ma => ma.ChiTietDatTiecs)
                .HasForeignKey(ct => ct.MaMon)
                .OnDelete(DeleteBehavior.Restrict);

            // ChiTietDatTiec -> HopDong
            modelBuilder.Entity<ChiTietDatTiec>()
                .HasOne(ct => ct.HopDong)
                .WithMany(hd => hd.ChiTietDatTiecs) 
                .HasForeignKey(ct => ct.MaHD)
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

            //HopDong -> DichVu
            modelBuilder.Entity<HopDong>()
                .HasOne(hd => hd.DichVu)
                .WithMany(dv => dv.HopDongs)
                .HasForeignKey(hd => hd.MaDV)
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
            modelBuilder.Entity<ChiTietDatTiec>()
                .HasKey(ct => new { ct.MaHD, ct.MaMon });
            modelBuilder.Entity<PhanCong>()
                .HasKey(ct => new { ct.MaLDS, ct.MaNV });

            modelBuilder.Entity<Rating>()
                .HasOne(r => r.HopDong)
                .WithMany(hd => hd.Ratings)
                .HasForeignKey(r => r.MaHD)
                .OnDelete(DeleteBehavior.SetNull);

            // Precision cho các decimal
            modelBuilder.Entity<DichVu>().Property(dv => dv.DonGia).HasPrecision(18, 2);
            modelBuilder.Entity<HopDong>().Property(hd => hd.TienCoc).HasPrecision(18, 2);
            modelBuilder.Entity<MonAn>().Property(ma => ma.DonGia).HasPrecision(18, 2);
            modelBuilder.Entity<SanhTiec>().Property(st => st.Gia).HasPrecision(18, 2);
            modelBuilder.Entity<BangLuong>().Property(st => st.TongGioLam).HasPrecision(18, 2);
            modelBuilder.Entity<BangLuong>().Property(st => st.TongLuong).HasPrecision(18, 2);
        }

    }
}
