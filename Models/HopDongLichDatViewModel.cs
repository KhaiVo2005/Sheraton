namespace Sheraton.Models
{
    public class HopDongLichDatViewModel
    {
        public HopDong HopDong { get; set; } = new HopDong();
        public LichDatSanh LichDatSanh { get; set; } = new LichDatSanh();

        // Gợi ý mở rộng: VD lấy số lượng khách từ người dùng (để lọc sảnh đủ chỗ)
        public int SoLuongKhach { get; set; }

        // Có thể bổ sung thêm ngày tổ chức để lọc sảnh theo ngày
        public DateTime NgayTiec => LichDatSanh.BatDau.Date;
    }
}
