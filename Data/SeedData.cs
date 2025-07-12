using Sheraton.Models;
using Microsoft.EntityFrameworkCore;

namespace Sheraton.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new SheratonDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<SheratonDbContext>>());

            if (!context.DichVus.Any())
            {
                context.DichVus.AddRange(
                    new DichVu { MaDV = Guid.NewGuid(), TenDV = "Sinh Nhật", DonGia = 1500000, MoTa = "Trang trí sinh nhật" },
                    new DichVu { MaDV = Guid.NewGuid(), TenDV = "Tiệc cưới", DonGia = 2500000, MoTa = "Trang trí tiệc cưới" },
                    new DichVu { MaDV = Guid.NewGuid(), TenDV = "Hội nghị", DonGia = 800000, MoTa = "Trang trí hội nghị" }
                    
                );
            }

            if (!context.MonAns.Any())
            {
                context.MonAns.AddRange(
                    new MonAn { MaMon = Guid.NewGuid(), TenMon = "Gỏi ngó sen tôm thịt", DonGia = 80000, MoTa = "Món khai vị hấp dẫn" },
                    new MonAn { MaMon = Guid.NewGuid(), TenMon = "Súp cua trứng bắc thảo", DonGia = 70000, MoTa = "Món súp nóng hổi" },
                    new MonAn { MaMon = Guid.NewGuid(), TenMon = "Tôm chiên xù", DonGia = 100000, MoTa = "Tôm chiên giòn thơm ngon" },
                    new MonAn { MaMon = Guid.NewGuid(), TenMon = "Lẩu hải sản", DonGia = 200000, MoTa = "Món chính thơm ngon" },
                    new MonAn { MaMon = Guid.NewGuid(), TenMon = "Chè hạt sen", DonGia = 50000, MoTa = "Món tráng miệng thanh mát" }
                );
            }

            if (!context.SanhTiecs.Any())
            {
                context.SanhTiecs.AddRange(
                    new SanhTiec { MaSanh = Guid.NewGuid(), TenSanh = "Sài Gòn - Đà Lạt", SucChua = 00, MoTa = "Sảnh lớn sang trọng", Gia = 20000000, TrangThai = "Trống" },
                    new SanhTiec { MaSanh = Guid.NewGuid(), TenSanh = "Đà Nẵng - Huế", SucChua = 150, MoTa = "Sảnh nhỏ ấm cúng", Gia = 12000000, TrangThai = "Trống" },
                    new SanhTiec { MaSanh = Guid.NewGuid(), TenSanh = "Sài Gòn - Hà Nội", SucChua = 200, MoTa = "Phong cách hoàng gia", Gia = 15000000, TrangThai = "Trống" },
                    new SanhTiec { MaSanh = Guid.NewGuid(), TenSanh = "Nha Trang - Đà Lạt", SucChua = 100, MoTa = "Phù hợp tiệc thân mật", Gia = 9000000, TrangThai = "Trống" },
                    new SanhTiec { MaSanh = Guid.NewGuid(), TenSanh = "Sài Gòn - Vũng Tàu", SucChua = 250, MoTa = "Đẹp và hiện đại", Gia = 18000000, TrangThai = "Trống" }
                );
            }

            if (!context.NhanViens.Any())
            {
                context.NhanViens.AddRange(
                    new NhanVien { MaNV = Guid.Parse("1C015EC9-17C7-41E0-A3B8-890B88A82C1A"), TenNV = "Nguyễn Văn A", SDT = "0901234567", Email = "a@sheraton.com", GioiTinh = true, ChucVu = "Sale", TK = "A", MK ="1" },
                    new NhanVien { MaNV = Guid.Parse("5A54F1DF-EAC9-437A-AAAA-5265598096C8"), TenNV = "Trần Thị B", SDT = "0912345678", Email = "b@sheraton.com", GioiTinh = false, ChucVu = "Sale", TK = "B", MK = "1" },
                    new NhanVien { MaNV = Guid.Parse("CAE75845-6F32-42DE-BFD8-5D9ABD576EF3"), TenNV = "Lê Văn C", SDT = "0923456789", Email = "c@sheraton.com", GioiTinh = true, ChucVu = "Sale", TK = "C", MK = "1" },
                    new NhanVien { MaNV = Guid.Parse("9DF281FD-7D78-4E7C-A777-6BA527C68721"), TenNV = "Phạm Thị D", SDT = "0934567890", Email = "d@sheraton.com", GioiTinh = false, ChucVu = "Sale", TK = "D", MK = "1" },
                    new NhanVien { MaNV = Guid.Parse("C605C053-B124-4311-8D4D-33C50A56648A"), TenNV = "Đặng Văn E", SDT = "0945678901", Email = "e@sheraton.com", GioiTinh = true, ChucVu = "Sale", TK = "E", MK = "1" }
                );
            }

            if (!context.KhachHangs.Any())
            {
                context.KhachHangs.AddRange(
                    new KhachHangg { MaKH = Guid.NewGuid(), TenKH = "Nguyễn Hoàng Phúc", SDT = "0909090909", GioiTinh = true, Email = "nhphuc300105@gmail.com" },
                    new KhachHangg { MaKH = Guid.NewGuid(), TenKH = "Nguyễn Hương", SDT = "0919191919", GioiTinh = false, Email = "huong@gmail.com" },
                    new KhachHangg { MaKH = Guid.NewGuid(), TenKH = "Trần Quốc", SDT = "0929292929", GioiTinh = true, Email = "quoc@gmail.com" },
                    new KhachHangg { MaKH = Guid.NewGuid(), TenKH = "Phạm Hoa", SDT = "0939393939", GioiTinh = false, Email = "hoa@gmail.com" },
                    new KhachHangg { MaKH = Guid.NewGuid(), TenKH = "Đỗ Long", SDT = "0949494949", GioiTinh = true, Email = "long@gmail.com" }
                );
            }

            context.SaveChanges();
        }
    }
}
