namespace Sheraton.Models.ViewModel
{
    public class HoaDon
    {
        public Guid MaHD { get; set; }
        public string TenKhachHang { get; set; }
        public string TenNhanVien { get; set; }
        public string TenDichVu { get; set; }
        public DateTime NgayKy { get; set; }
        public decimal TienCoc { get; set; }
        public decimal TienSanh { get; set; }
        public decimal TienMonAn { get; set; }
        public decimal TienDichVu { get; set; }

        public HopDong hopDong { get; set; }
        public List<ChiTietDatTiec> chiTietDatTiecs { get; set; }

        public string TrangThai { get; set; }
        public decimal TongTien()
        {
            return TienCoc + TienSanh + TienMonAn + TienDichVu;
        }
    }
}
